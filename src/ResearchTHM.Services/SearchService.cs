using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System;
using System.Collections;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.TrkdModels.TrkdRequestModels;
using ResearchTHM.Core.TrkdModels;
using ResearchTHM.Core.Models.RequestModels;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Box.V2.Models;
using Serilog;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ResearchTHM.Core.Models.ResponseModels;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using Box.V2.Services;
using System.Net;
using Microsoft.Extensions.Primitives;
using UAParser;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using ResearchTHM.Services.ExtensionMethod;
using Microsoft.Data.SqlClient;

namespace ResearchTHM.Services
{
    public class SearchService : ISearchService
    {
        private readonly ResearchMktContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IMemoryCache _cache;
        private readonly IMktContributorService _contributorService;
        private readonly IBoxUtilService _boxService;
        private readonly IMktDownloadHistService _mktDownloadHistService;

        enum fileTypesEnum { Unspecified, activeDoc, pdf, syn, url, web, other };
        private readonly IMktUserService _userService;
        private readonly IMktContributorService _ctbService;

        public SearchService(ResearchMktContext context, IMktContributorService ctbService, IMktUserService userService, IMktContributorService contributorService,
            ILogger logger, IHttpClientFactory clientFactory, IMemoryCache cache, IConfiguration configuration, IBoxUtilService boxService,
            IMktDownloadHistService mktDownloadHistService)
        {
            _ctbService = ctbService;
            _context = context;
            _userService = userService;
            _logger = logger;
            _clientFactory = clientFactory;
            _cache = cache;
            _configuration = configuration;
            _contributorService = contributorService;
            _boxService = boxService;
            _mktDownloadHistService = mktDownloadHistService;

        }


        #region Metadata

        public async Task<object> GetMetadata(Guid groupId, Guid userId)
        {
            var geography = _context.VwCountyRegions.AsQueryable().OrderBy(e => e.CountryName);
            var industries = _context.MktIndustry.Where((ele) => ele.IsDeleted == false).Select(c => new { c.Id, c.LocalCode, c.IndustryId, c.IndustryName }).OrderBy(e => e.IndustryName);
            var contributors = (await _userService.GetUserGroupAccess(groupId)).Select(c => new { c.ContributorId, c.ContributorName, c.ContributorUid }).OrderBy(e => e.ContributorName);
            var userlanguage = _context.MktUser.Where(ele => ele.UserId == userId).Select(e => e.LanguagePreferences ?? "en");
            //var contributors = _context.MktContributor.Where((ele) => ele.IsDeleted == 0).Select(c => new { c.ContributorId, c.ContributorName, c.ContributorUid }).OrderBy(e => e.ContributorName);

            return new { geography, industries, contributors, userlanguage };
        }

        public async Task<IEnumerable<AuthorResponse>> GetAuthorsList(string authName, int rowCount)
        {
            FaultResponse fault;
            AuthorsResponse authors = null;
            var searchParams = new List<string>();

            var sp = authName.Replace(",", "").Split(' ');

            if (authName.Contains(","))
            {
                var commaSplit = authName.Split(',');
                searchParams.Add("\"lastName\": \"" + commaSplit[0].Replace(" ", "") + "\"");
                if (commaSplit.Length > 1)
                    searchParams.Add("\"firstName\": \"" + commaSplit[1].Replace(" ", "") + "\"");
            }
            else if (authName.Contains(" "))
            {
                var spaceSplit = authName.Split(" ");
                searchParams.Add("\"firstName\": \"" + spaceSplit[0].Replace(" ", "") + "\"");
                if (spaceSplit.Length > 1)
                    searchParams.Add("\"lastName\": \"" + spaceSplit[1].Replace(" ", "") + "\"");
            }
            else
            {
                searchParams.Add("\"firstName\": \"" + authName + "\"},{\"lastName\":\"" + authName + "\"");
            }

            string url = _configuration["APIEndpoints:getAuthorsList"];

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var body = "{ \"AuthorsList_Request_1\": { \"authorSearchRequiredInfo\": [ { \"authorInfoType\": \"basic\" },{ \"authorInfoType\": \"ctb\" } ]," +
                " \"authorSearchCriterion\": { \"basicSearch\": [ { <SEARCHPARAMETERS> } ] }, \"rowcount\": <ROWCOUNT>, \"startrow\": 1, \"useMasterDB\": true } } ";

            body = body.Replace("<SEARCHPARAMETERS>", String.Join(',', searchParams));
            body = body.Replace("<ROWCOUNT>", rowCount.ToString());

            request.Content = new StringContent(body);

            var response = await SendRequest(request);
            List<Author> responseData = new List<Author>();
            if (response.IsSuccessStatusCode)
            {
                authors = JsonConvert.DeserializeObject<AuthorsResponse>(await response.Content.ReadAsStringAsync());
                if (authors.AuthorsList_Response_1 != null)
                {

                    var authorObj = authors.AuthorsList_Response_1.author;

                    var contributorUids = authorObj.SelectMany(e => e.ctb ?? new List<Ctb>()).Select(ctb => ctb.ctbID).ToList();

                    var ctbs = _contributorService.Find(e => contributorUids.Contains(e.ContributorUid) && !e.IsDeleted);

                    authorObj.ForEach(auth =>
                    {
                        var authorContributorUids = auth.ctb?.Select(ctb => ctb.ctbID).ToList();

                        if (authorContributorUids != null)
                        {
                            var contributorNames = ctbs.Where(e => authorContributorUids.Contains(e.ContributorUid)).Select(e => e.ContributorName).ToList();

                            if (contributorNames.Any() && auth.ctb.Any(e => e.isCurrent))
                            {
                                auth.basic.displayableName = $"{auth.basic.firstName ?? ""} {auth.basic.lastName ?? ""} ({String.Join(',', contributorNames)})";
                                responseData.Add(auth);
                            }
                        }

                    });

                    return responseData.Select(auth => new AuthorResponse()
                    {
                        name = (auth.basic.firstName ?? "").Trim() + ' ' +
                                           (auth.basic.lastName ?? "").Trim(),
                        authorCode = auth.authorCode.Trim(),
                        uid = auth.uid,
                        displayName = auth.basic.displayableName
                    });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                fault = JsonConvert.DeserializeObject<FaultResponse>(await response.Content.ReadAsStringAsync());
                throw new Exception($"TRKD responded for getting Authors with a fault: {JsonConvert.SerializeObject(fault)}");
            }
        }

        public async Task<IEnumerable> GetOrganisations(string searchText = "", string rowCount = "20", string type = "CommonName", string match = "beginswith")
        {
            FaultResponse fault;
            SearchResponse searchResponse = null;
            string filter = "BusinessEntity in ('ORGANISATION' 'ORGS')";


            string url = _configuration["APIEndpoints:searchV2forOrg"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var body = "{ \"Search_Request_1\": { \"Collection\": \"Organisations\", \"Paging\": { \"Top\": <ROWCOUNT>, \"Skip\": 0 }, " +
                "\"Filter\": \"<ORGFILTER> \"," +
                " \"SortBy\": \"BusinessEntity\", " +
                "\"ResponseProperties\": \"BusinessEntity,PI,CommonName, LegalName,AlternateLegalName,LongName,Ticker,PrimaryRIC,MXID,PrimaryRICTickerSymbol,PrimaryRICISIN,InsertDateTime\", " +
                "\"UnentitledAccess\": true } }";

            if (string.IsNullOrEmpty(rowCount))
                rowCount = "20";

            if (type.ToLower() == "isin")
                type = "PrimaryRICISIN";

            if (type.ToUpper() == "MXID")
            {
                filter += $"and MXID in ({searchText})";
            }
            else if (type.ToLower() == "typehead")
            {
                filter += $"and (CommonName eq '{searchText}*' or Ticker eq '{searchText}*' or PrimaryRICISIN eq '{searchText}*')";
            }
            else
            {
                if (match.ToLower() == "beginswith" || match == "")
                    filter += $"and {type} eq '{searchText}*'";
                else if (match.ToLower() == "exact")
                    filter += $"and {type} eq '{searchText}'";
                else
                    filter += $"and {type} eq '*{searchText}*'";
            }

            body = body.Replace("<ROWCOUNT>", rowCount);
            body = body.Replace("<ORGFILTER>", filter);

            request.Content = new StringContent(body);

            request.Content.Headers.ContentType.MediaType = "application/json";
            var response = await SendRequest(request);
            var responsestring = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                searchResponse = JsonConvert.DeserializeObject<SearchResponse>(responsestring);

                if (searchResponse.Search_Response_1.Results != null)
                {

                    return searchResponse.Search_Response_1.Results.Result
                        .Select(res =>
                        {
                            Dictionary<string, string> obj = new Dictionary<string, string>();
                            res.Property.ForEach(ele =>
                            {
                                obj[ele.Name] = ele.Value;
                            });
                            return obj;
                        });
                }
                else
                    return null;
            }
            else
            {
                fault = JsonConvert.DeserializeObject<FaultResponse>(responsestring);
                throw new Exception($"TRKD responded for getting Organisation with a fault: {JsonConvert.SerializeObject(fault)}");
            }
        }

        #endregion

        #region DocSearch

        public async Task<DocSearchRequest> generateDocSearchObject(DocSearchUIRequest srequest, Guid userId)
        {
            _logger.Information("Search service logging in generateDocSearchObject");

            DocSearchRequest1 docSearch = new DocSearchRequest1();

            docSearch.bShowPrims = true;
            docSearch.bCP_ResponseType = true;
            docSearch.bHierIndustries = true;
            docSearch.calcPrice = true;
            docSearch.tkrEncoding = "prtID";// "prtIDOrRIC";
            docSearch.tkrPrimary = true;
            docSearch.maxRows = srequest.maxRows;
            docSearch.sort = new Sort { s_d = srequest.sortOrder, s_c = srequest.sortBy };

            if (srequest.reportNo != null && srequest.reportNo.First() != null && srequest.reportNo.First() != "")
            {
                docSearch.docID = srequest.reportNo;
            }
            else
            {
                docSearch.langID = srequest.Languages.Select(e => e.id).ToList() ?? new List<string> { "en" };
                if (srequest.contributors != null)
                {
                    docSearch.excludeCtbs = false; //Always work on Inclusion
                    docSearch.ctbs = await GetListOfEntitledContributors(userId, srequest.excludeCtb, srequest.contributors.Select(e => e.id));
                }

                if (srequest.pDocId != "0" && srequest.pDocId != null)
                    docSearch.pagingHint = new PagingHint { docID = srequest.pDocId, hintStr = srequest.pHintStr };

                docSearch.dateFrom = DateTime.Parse(srequest.dateFrom).ToString("yyyy-MM-dd");
                docSearch.dateTo = DateTime.Parse(srequest.dateTo).ToString("yyyy-MM-dd");

                if (srequest.reportStyles != null)
                    if (srequest.reportStyles.Count() < 5)
                    {
                        docSearch.rptStyles = new List<string>();
                        if (srequest.reportStyles.Contains("Company"))
                            docSearch.rptStyles.Add("150000002");
                        if (srequest.reportStyles.Contains("Industry"))
                            docSearch.rptStyles.Add("150000009");
                        if (srequest.reportStyles.Contains("Geographic"))
                            docSearch.rptStyles.Add("150000011");
                        if (srequest.reportStyles.Contains("Fixed Income"))
                            docSearch.rptStyles.Add("150000007");
                        if (srequest.reportStyles.Contains("Investing/Economic"))
                        {
                            docSearch.rptStyles.Add("150000006");
                            //docSearch.rptStyles.Add("150000011");
                            //docSearch.rptStyles.Add("150000017");
                            //docSearch.rptStyles.Add("150000018");
                        }
                    }

                if (srequest.initiatingCov)
                    docSearch.reasons = new List<string>() { "140000012" };

                if (!String.IsNullOrEmpty(srequest.PagesTo) && !String.IsNullOrWhiteSpace(srequest.PagesTo) && srequest.PagesTo != "0")
                {//Case less than
                    docSearch.NumPagesTo = (Convert.ToInt32(srequest.PagesTo) - 1).ToString();
                }

                if (!String.IsNullOrEmpty(srequest.PagesFrom) && !String.IsNullOrWhiteSpace(srequest.PagesFrom) && srequest.PagesFrom != "0")
                {
                    if (!String.IsNullOrEmpty(srequest.PagesTo) && !String.IsNullOrWhiteSpace(srequest.PagesTo) && srequest.PagesTo != "0")
                    {
                        //Equal case
                        docSearch.NumPagesTo = srequest.PagesTo;
                        docSearch.NumPagesFrom = srequest.PagesFrom;
                    }
                    else
                    {
                        //Case greater than
                        docSearch.NumPagesFrom = (Convert.ToInt32(srequest.PagesFrom) + 1).ToString(); // -1 to exclude boundary values in case of greater than
                    }
                }


                docSearch.analyst = srequest.Analyst.Select(e => e.id);
                docSearch.region = srequest.Region.Select(e => e.id);
                docSearch.industrySet = "GR#1000";
                docSearch.industry = srequest.Industry.Select(e => e.id);
                docSearch.country = srequest.Country.Select(e => e.id);
                docSearch.nRegion = srequest.ERegion.Select(e => e.id);
                docSearch.nCountry = srequest.ECountry.Select(e => e.id);

                docSearch.ticker = srequest.company.Select(e => e.id);

                if (srequest.headlineSearch != null)
                    srequest.headlineSearch = processTextSearchString(srequest.headlineSearch);
                if (srequest.textSearch != null)
                    srequest.textSearch = processTextSearchString(srequest.textSearch);
                if (srequest.tocSearch != null)
                    srequest.tocSearch = processTextSearchString(srequest.tocSearch);

                if (srequest.headlineSearch != null || srequest.textSearch != null || srequest.tocSearch != null)
                {
                    var textSearch = new TextSearch();
                    textSearch.joinStrCondByOr = srequest.searchJoinCondition;
                    if (srequest.headlineSearch != null && srequest.headlineSearch != "\"\"")
                        textSearch.matchStrHdln = new MatchStrHdln { value = srequest.headlineSearch };
                    if (srequest.textSearch != null && srequest.textSearch != "\"\"")
                        textSearch.matchStrText = new MatchStrText { value = srequest.textSearch };
                    if (srequest.tocSearch != null && srequest.tocSearch != "\"\"" && !(srequest.headlineSearch != null || srequest.textSearch != null))
                        textSearch.matchStrTOC = new MatchStrToc { value = srequest.tocSearch };

                    docSearch.textSearch = textSearch;
                }
            }


            return new DocSearchRequest() { DocSearch_Request_1 = docSearch };
        }

        private string processTextSearchString(string input)
        {
            string original = new String(input);
            input = input.Replace("(", "").Replace(")", "");
            string pattern = @"\s+(?:AND|OR|NEAR\/\d+)\s+(?=(?:[^""]*""[^""]*"")*[^""]*$)";
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;

            Regex regex = new Regex(pattern, options);
            char[] textCharsToTrim = _configuration.GetSection("Thomson:textCharsToTrim").Get<char[]>() ?? new char[] { ' ' };
            string others = _configuration["Thomson:others"];
            var array = regex.Split(input).Select(e => e.Trim(textCharsToTrim)).Where(e => !e.Contains('*'));

            array.ToList().ForEach(e =>
            {
                var substitutionRegex = new Regex($"\\b{e}\\b", options);
                original = substitutionRegex.Replace(original, $"\"{e}\"", 1);
            });
            return original;
        }

        public async Task<object> DocumentSearch(Guid userId, DocSearchUIRequest srequest)
        {
            FaultResponse fault;
            BatchResponse returnResponse;
            try
            {
                var docSearch = await generateDocSearchObject(srequest, userId);

                var docCountObj = docSearch.DocSearch_Request_1;
                docCountObj.req_id = 1;

                var docSearchObj = docCountObj.GetNewRequestInstance(docCountObj);
                docSearchObj.req_id = 2;

                var resp = new { diDef = new List<DiDef>(), count = Convert.ToInt64(0) };

                var body = new
                {
                    Batch_Request_1 = new
                    {
                        DocSearch_Request_1_typehint = new string[] { "DocSearch_Request_1", "DocCount_Request_1" },
                        DocSearch_Request_1 = new object[] { docSearchObj, docCountObj }
                    }
                };

                string url = _configuration["APIEndpoints:batchRequest"];
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                string requestContent = JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                request.Content = new StringContent(requestContent);
                request.Content.Headers.ContentType.MediaType = "application/json";

                try
                {
                    var response = await SendRequest(request);
                    var responsestring = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        returnResponse = JsonConvert.DeserializeObject<BatchResponse>(responsestring, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore });

                        if (returnResponse.Batch_Response_1.DocCount_Response_1.Count != 0)
                        {
                            //if (docSearch.DocSearch_Request_1?.analyst != null)
                            //{
                            //    if (!(docSearch.DocSearch_Request_1.analyst.Count() > 0))
                            //        resp = new { diDef = returnResponse.Batch_Response_1.DocSearch_Response_1.DiDef, count = returnResponse.Batch_Response_1.DocCount_Response_1.Count };
                            //    else
                            //        resp = new { diDef = returnResponse.Batch_Response_1.DocSearch_Response_1.DiDef.Where(r => r.Author != null ? docSearch.DocSearch_Request_1.analyst.Contains(r.Author[0].C) : false).ToList(), count = returnResponse.Batch_Response_1.DocCount_Response_1.Count };
                            //}
                            //else
                            resp = new { diDef = returnResponse.Batch_Response_1.DocSearch_Response_1.DiDef, count = returnResponse.Batch_Response_1.DocCount_Response_1.Count };
                        }
                        else
                            return null;
                    }
                    else
                    {
                        fault = JsonConvert.DeserializeObject<FaultResponse>(await response.Content.ReadAsStringAsync());
                        throw new Exception($"TRKD responded with a fault: {JsonConvert.SerializeObject(fault)}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex + $"Exception occured in TRKD Request. Headers: {JsonConvert.SerializeObject(request.Headers)}. ---------------------- Content Headers: {JsonConvert.SerializeObject(request.Content.Headers)} --------------------------- Request: {JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })} ");
                }


                var exclusionRegex = new Regex(@"[^\p{L}\d\s_-]");

                var ids = resp.diDef.Select(e => e.PTkr).Where(e => e?.PrtId != null);

                IEnumerable<Dictionary<string, string>> organisationNames = new List<Dictionary<string, string>>();
                string secPrtIds = String.Join(' ', ids.Select(e => $"'{e.PrtId}'"));
                if (secPrtIds != string.Empty)
                {
                    var c = await GetOrganisations(secPrtIds, ids.Count().ToString(), "MXID");
                    var IDS = ids.Select(e => e.PrtId.ToString()).Distinct().ToList();
                    organisationNames = c as IEnumerable<Dictionary<string, string>>;
                }

                Regex regex = new Regex(@"[\\\/:""*?<>|]+", RegexOptions.Multiline);
                List<SearchResponseResource> searchResponse = new List<SearchResponseResource>();

                if (organisationNames.Count() > 0)
                {
                    searchResponse = resp.diDef.GroupJoin(organisationNames, o => o.PTkr?.PrtId.ToString(), i => i["MXID"], (e, i) =>
                    {
                        var x = new SearchResponseResource();
                        try
                        {
                            x = new SearchResponseResource()
                            {
                                ArriveDate = e.ArriveDate,
                                Author = e.Author,
                                Cntry = e.Cntry,
                                CompanyName = e.CompanyName,
                                CtbId = e.CtbId,
                                DocId = e.DocId,
                                Headline = e.Headline,
                                FileExt = e.FileExt,
                                //FileName = $"{e.ReleaseDate.ToString("MM-dd-yyyy")}_{filename}_({e.DocId}).{(e.FileExt ?? "pdf")}",
                                FileSize = e.FileSize,
                                FileType = e.FileType,
                                HasToc = e.HasToc,
                                Ind = e.Ind,
                                Lang = e.LangDesc.Rfc1766,
                                MainAuthor = e?.MainAuthor,
                                NonBillablePages = e.NonBillablePages,
                                Pages = e.Pages,
                                Price = e.Price,
                                //PricingType = e.PricingType,
                                projectCode = e.projectCode,
                                PTkr = e.PTkr != null && e.PTkr.PrtId != null ? new Tkr { BPrim = e.PTkr.BPrim, PrtId = e.PTkr.PrtId, Ric = e.PTkr.Ric } : null,
                                ReasonsResp = e.ReasonsResp,
                                Reg = e.Reg,
                                ReleaseDate = e.ReleaseDate,
                                SubmitDate = e.SubmitDate,
                                RptStylesResp = e.RptStylesResp,

                            };

                            if (x.PTkr != null)
                            {
                                string primaryName = "";
                                i?.FirstOrDefault()?.TryGetValue("CommonName", out primaryName);
                                x.PTkr.Value = primaryName;
                            }
                            if (e.Tkr != null)
                            {
                                var secondaryMXID = e.Tkr.Select(t => t.PrtId.ToString()).ToList();

                                x.Tkr = organisationNames
                                            .Where(org => org.ContainsKey("MXID") ? secondaryMXID.Contains(org["MXID"]) : false)
                                            .Select(org => new Tkr
                                            {
                                                PrtId = Convert.ToInt64(org["MXID"]),
                                                Value = org.ContainsKey("CommonName") ? org["CommonName"] : "",
                                                Ric = org.ContainsKey("Ticker") && org["Ticker"] != null ? org["Ticker"] : org.ContainsKey("PrimaryRIC") ? org["PrimaryRIC"] : ""
                                            });
                            }

                            string filename = "";

                            if ($"{x.CompanyName}_{x?.PTkr?.Value}".Length < 200)
                            {
                                filename = $"{(x?.PTkr?.Value != null ? (x?.PTkr?.Value + "_") : String.Empty)}{x.CompanyName}";
                            }
                            else
                            {
                                filename = $"{(x?.PTkr?.Value != null ? (x?.PTkr?.Value.Substring(0, Math.Min(50, x?.PTkr?.Value?.Length ?? 0)) + "_") : String.Empty)}" +
                                    $"{x.CompanyName.Substring(0, Math.Min(50, x.CompanyName.Length))}";
                            }

                            x.FileName = exclusionRegex.Replace($"{e.ReleaseDate.ToString("MM-dd-yyyy")}_{filename}_({e.DocId})", "") + ("." + e.FileExt ?? ".pdf");

                            return x;
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }

                    }).ToList();

                }
                else
                {
                    searchResponse = resp.diDef.Select(e =>
                    {

                        string filename = exclusionRegex.Replace(e.Headline, "");
                        filename = filename.Substring(0, filename.Length < 100 ? filename.Length : 100);
                        try
                        {
                            var x = new SearchResponseResource()
                            {
                                ArriveDate = e.ArriveDate,
                                Author = e.Author,
                                Cntry = e.Cntry,
                                CompanyName = e.CompanyName,
                                CtbId = e.CtbId,
                                DocId = e.DocId,
                                Headline = e.Headline,
                                FileExt = e.FileExt,
                                FileName = $"{filename}_{e.ReleaseDate.ToString("MM-dd-yyyy")} ({e.DocId}).{(e.FileExt ?? "pdf")}",
                                FileSize = e.FileSize,
                                FileType = e.FileType,
                                HasToc = e.HasToc,
                                Ind = e.Ind,
                                MainAuthor = e?.MainAuthor,
                                NonBillablePages = e.NonBillablePages,
                                Pages = e.Pages,
                                Price = e.Price,
                                //PricingType = e.PricingType,
                                projectCode = e.projectCode,
                                PTkr = e.PTkr != null && e.PTkr.PrtId != null ? new Tkr { BPrim = e.PTkr.BPrim, PrtId = e.PTkr.PrtId, Ric = e.PTkr.Ric } : null,
                                ReasonsResp = e.ReasonsResp,
                                Reg = e.Reg,
                                ReleaseDate = e.ReleaseDate,
                                SubmitDate = e.SubmitDate,
                                RptStylesResp = e.RptStylesResp,

                            };

                            return x;
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }

                    }).ToList();
                }

                return new { diDef = searchResponse, resp.count };
            }
            catch (Exception ex)
            {
                throw new Exception(ex + $"Error in processing the DocSearchUI Request: {JsonConvert.SerializeObject(srequest)}");
            }
        }

        public async Task<string> GetListOfEntitledContributors(Guid userId, bool excludeCtb, IEnumerable<string> ctbs)
        {
            IEnumerable<string> ctb;
            //IEnumerable<MktUser> u;

            if (!excludeCtb && ctbs.Count() > 0)//Not of Exclude ==> Included these contributors ==> search only against these contributors
                ctb = ctbs;
            else
            {
                //u = _context.MktUser.Where(u => u.UserId == userId).Include(e => e.MktUserGroupAccess).ThenInclude(groupAccess => groupAccess.Group);//.ThenInclude(e => e.).ToList();//.ThenInclude(e => e.MktGroupAccess).ToList(); 

                var u = _context.VwUserInfo.Where(e => e.UserId == userId);

                if (u.Any())
                {
                    if (u.First().PreferContributorId == null || u.First().PreferContributorId == "")
                    {
                        ctb = _context.MktGroupAccess.Where(gr => gr.GroupId == u.First().GroupId).Select(e => e.ContributorId.ToString());
                    }
                    else
                    {
                        ctb = u.First().PreferContributorId.Split(';');
                    }
                    var ctbMaster = _context.MktContributor.Where(e => !e.IsExcluded && e.IsDeleted == false);

                    var x = _context.MktGroupAccess.Where(gr => gr.GroupId == u.First().GroupId)
                        .Join(ctbMaster,
                        o => o.ContributorId,
                        i => i.ContributorId,
                        (o, i) => new { i.ContributorUid, i.ContributorId }
                        );

                    ctb = await x.Where(e => ctb.Contains(e.ContributorId.ToString())).Select(e => e.ContributorUid).ToListAsync();

                    if (ctb.Count() == 0)
                    {
                        throw new Exception($"UserId: ${userId} has no entitlements. Returning error");
                    }

                    //ctb = await _context.MktContributor.Where(e => ctb.Contains(e.ContributorId.ToString()) && !e.IsExcluded && e.IsDeleted == false).Select(e => e.ContributorUid).ToListAsync();

                    if (excludeCtb)
                        ctb = ctb.Except(ctbs);
                    else
                        ctb = ctb.Concat(ctbs);

                    if (ctb.Count() == 0)
                    {
                        ctb = _context.MktGroupAccess.Where(gr => gr.GroupId == u.First().GroupId).Select(e => e.ContributorId.ToString());
                        ctb = await _context.MktContributor.Where(e => ctb.Contains(e.ContributorId.ToString()) && !e.IsExcluded && e.IsDeleted == false).Select(e => e.ContributorUid).ToListAsync();
                    }
                }
                else
                    throw new Exception("User not available in the system");
            }

            return String.Join(',', ctb);

        }

        #endregion

        #region Download
        private Task<HttpResponseMessage> DownloadDocumentAsync(string pages, string fileType, long docId)
        {
            var client = _clientFactory.CreateClient();
            string token = GetCachedToken();
            string configurl = _configuration["APIEndpoints:getDocument"];
            string appId = _configuration["trkdCredentials:ApplicationID"];

            var type = Enum.Parse<fileTypesEnum>(fileType);

            string url = $"{configurl}{docId}/{type.ToString()}";

            if (pages != null && pages != "" && pages != "all" && fileType == "pdf")
            {
                var dict = new Dictionary<string, string>();
                dict["pages"] = pages;
                url = QueryHelpers.AddQueryString(url, dict);
            }
            var httprequest = new HttpRequestMessage(HttpMethod.Get, url);

            var cookie = $"RkdAppId={appId};RkdToken={token};";
            httprequest.Headers.Add("Cookie", cookie);

            return client.SendAsync(httprequest);
        }

        private async Task<Tuple<HttpResponseMessage, bool>> DownloadDocumentWithFallback(string pages, string fileType, long docId)
        {
            IEnumerable<string> faults;
            HttpResponseMessage response = null;
            bool isNormalDownload = false;

            response = await DownloadDocumentAsync(pages, fileType, docId);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Redirect)
                isNormalDownload = true;
            else if (response.StatusCode == HttpStatusCode.InternalServerError && response.Headers.TryGetValues("RkdFaultCode", out faults) && faults.FirstOrDefault() == "DocumentRetrieval_DocumentNotAllowedToPerPageDownload")
            {
                response = await DownloadDocumentAsync("all", fileType, docId);
                isNormalDownload = false;
            }

            return new Tuple<HttpResponseMessage, bool>(response, isNormalDownload);
        }

        public async Task<bool> SaveUsageLog(StringValues userAgent, string ip, Guid userId, Guid related, string logType, string location, DateTime logDate, string loginSource = "")
        {
            string uaString = Convert.ToString(userAgent[0]);
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(uaString);
            var browser = c.UA.Family;
            var bswver = c.UA.Major + "." + c.UA.Minor + "." + c.UA.Patch;
            var device = c.Device.ToString();

            MktUsageLog mktUsageLog = new MktUsageLog();
            System.Guid logid = System.Guid.NewGuid();
            mktUsageLog.LogId = logid;
            mktUsageLog.UserId = userId;
            mktUsageLog.LogType = logType;
            mktUsageLog.Related = related.ToString();
            mktUsageLog.BrowserName = browser;
            mktUsageLog.BrowserVersion = bswver;
            mktUsageLog.Location = location;
            mktUsageLog.Device = device;
            mktUsageLog.IP = ip;

            mktUsageLog.LogDate = logDate == DateTime.MinValue ? DateTime.UtcNow : logDate;
            mktUsageLog.LoginSource = loginSource;


            await _context.MktUsageLogs.AddAsync(mktUsageLog);
            try
            {
                if (await _context.SaveChangesAsync() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to write Usage log to DB. Object: {JsonConvert.SerializeObject(mktUsageLog)}");
            }
            return false;
        }

        public Task<DownloadDocumentResponse> GetDocument(DocDownloadRequest docDownloadRequest)
        {
            string pages = docDownloadRequest.Pages;
            // DiDef request = downloadRequest.Request;
            long docId = docDownloadRequest.DocId;

            pages ??= "all"; //If pages are null, as in the case of Non PDF documents, All pages will be downloaded.

            return Download(docDownloadRequest, pages);
        }

        public Task<DownloadDocumentResponse> GetDownloadDocumentFromDownloadHistory(DocDownloadRequest docDownloadRequest)
        {
            return Download(docDownloadRequest, docDownloadRequest.Pages);
        }

        private async Task<DownloadDocumentResponse> Download(DocDownloadRequest docDownloadRequest, string pages)
        {
            //downloadhistory
            //Check if document exists in boxDocInfo
            //if yes get the box file id => make request to box => make entry in download history and Usage Log => Return
            //if no grab the document from thomson => save it to box => Add boxDocInfo => Add downloadHistory,Usage Log => return

            Guid downloadhistid = Guid.NewGuid();
            Stream file = null;
            string boxFileId = null;
            string source = "";

            bool useBox = _configuration.GetValue<bool>("UseBox");
            if (useBox)
            {
                var m = await CheckIfDocumentCached(docDownloadRequest.DocId);
                boxFileId = m.Item1;

                //This block would rarely be used only in case of selection preservation.
                //This condition allows correct download history to be written when inital download was using fallback and selection was preserved
                //This condition can be removed when Price is stored in a separate variable                
                if (!(boxFileId is null || boxFileId is "0") && pages.ToLower() != "all")
                {
                    pages = "all";
                    string numPages = docDownloadRequest.BillablePages.ToLower() == "all" ? docDownloadRequest.PageNo.ToString() : docDownloadRequest.BillablePages;
                    docDownloadRequest.Price = (docDownloadRequest.PagePrice * Int32.Parse(numPages) * 100).ToString();
                }

                if (docDownloadRequest.FileType == "url" && boxFileId != null)
                    docDownloadRequest.FileName = m.Item2; //Get Filename from records in case of URL documents because of URL document name is fetched from content-disposition
            }

            try
            {
                if (boxFileId is null || boxFileId is "0")
                {
                    try
                    {
                        HttpResponseMessage response;
                        bool isNormalDownload;
                        (response, isNormalDownload) = await DownloadDocumentWithFallback(pages, docDownloadRequest.FileType, docDownloadRequest.DocId);


                        if (!isNormalDownload)
                        {
                            //This block will redjust the price where 
                            pages = "all";
                            if (docDownloadRequest.BillablePages != null)
                            {
                                string numPages = docDownloadRequest.BillablePages.ToLower() == "all" ? docDownloadRequest.PageNo.ToString() : docDownloadRequest.BillablePages;
                                docDownloadRequest.Price = (docDownloadRequest.PagePrice * Int32.Parse(numPages) * 100).ToString();
                            }
                        }
                        docDownloadRequest.Pages = pages;
                        source = "Thomson";

                        if (response.IsSuccessStatusCode)
                        {
                            file = await response.Content.ReadAsStreamAsync();
                            string fileName = response.Content.Headers.ContentDisposition?.FileName;

                            if (fileName is null)
                            {
                                IEnumerable<string> contentDisposition;
                                if (response.Content.Headers.TryGetValues("Content-Disposition", out contentDisposition))
                                    fileName = contentDisposition.FirstOrDefault().Split("=")[1];
                                else
                                    fileName = docDownloadRequest.FileName;
                            }

                            if (!(Path.GetExtension(fileName).Equals(Path.GetExtension(docDownloadRequest.FileName), StringComparison.OrdinalIgnoreCase)))
                                docDownloadRequest.FileName = Path.GetFileNameWithoutExtension(docDownloadRequest.FileName) + Path.GetExtension(fileName);

                            if (!(pages != null && pages != "" && pages != "all") && useBox)
                            {
                                try
                                {
                                    var boxFile = await _boxService.UploadFileAsync(file, docDownloadRequest.FileName, _boxService.RetrieveFolderIdOrCreateNew(docDownloadRequest));
                                    file.Position = 0;
                                    var xx = await AddBoxDocInfo(docDownloadRequest, boxFile);
                                    boxFileId = boxFile.Id;

                                    //Attach Metadata
                                    try
                                    {
                                        if (boxFileId != null)
                                        {
                                            string templateName = _configuration["BoxMetadataTemplatename"];

                                            var createMD = await _boxService.CreateFileMetadata(docDownloadRequest, boxFileId.ToString(), templateName);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.Error(ex, "Error in attaching the metadata for file DocId: {0}. {ControllerName}", docDownloadRequest.DocId, "SearchController:GetDocument");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    file.Position = 0;
                                    _logger.Error(ex, "Error Uploading file to box. DocId: {0}. Exception: {1}, {ControllerName}", docDownloadRequest.DocId, "SearchController:GetDocument");
                                }
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Redirect)
                        {
                            return new DownloadDocumentResponse
                            {
                                ResponseCode = 201,
                                RedirectURL = response.Headers.Location.ToString(),
                                Source = source,
                                BoxFileId = boxFileId,
                                Pages = pages
                            };
                        }
                        else
                        {
                            _logger.Error($"Error response received from Thomson. \n \n Headers:{JsonConvert.SerializeObject(response.Headers)} \n ContentHeaders{JsonConvert.SerializeObject(response.Content.Headers)} \n \n DocId: {docDownloadRequest.DocId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error Downloading file From Thomson. DocId: {0}. Exception: {1}, {ControllerName}", docDownloadRequest.DocId, ex, "SearchController:DownloadDocumentFromDownloadHistory");
                    }

                }
                else
                {
                    try
                    {
                        file = await _boxService.DownloadFile(boxFileId);
                        source = "Box";
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error downloading file From Box. Trying from thomson. DocId: {0}. Exception: {1}, {ControllerName}", docDownloadRequest.DocId, ex, "SearchController:GetDocument");

                        try
                        {
                            source = "Thomson";

                            var response = await DownloadDocumentAsync(pages, docDownloadRequest.FileType, docDownloadRequest.DocId);
                            if (response.IsSuccessStatusCode)
                            {
                                file = await response.Content.ReadAsStreamAsync();

                                //contentType = response.Content.Headers.ContentType?.MediaType ?? (response.Content.Headers.GetValues("Content-Type").FirstOrDefault() ?? contentType);

                                string fileName = response.Content.Headers.ContentDisposition?.FileName;

                                if (fileName is null)
                                {
                                    IEnumerable<string> contentDisposition;
                                    if (response.Content.Headers.TryGetValues("Content-Disposition", out contentDisposition))
                                        fileName = contentDisposition.FirstOrDefault().Split("=")[1];
                                    else
                                        fileName = docDownloadRequest.FileName;
                                }

                                if (!(Path.GetExtension(fileName).Equals(Path.GetExtension(docDownloadRequest.FileName), StringComparison.OrdinalIgnoreCase)))
                                    docDownloadRequest.FileName = Path.GetFileNameWithoutExtension(docDownloadRequest.FileName) + Path.GetExtension(fileName);
                            }
                            else
                            {
                                _logger.Error($"Error response received from Thomson. \n \n Headers:{JsonConvert.SerializeObject(response.Headers)} \n ContentHeaders{JsonConvert.SerializeObject(response.Content.Headers)} \n \n DocId:{docDownloadRequest.DocId}");
                            }
                        }
                        catch (Exception ex1)
                        {
                            _logger.Error("Error while retrieving document from Thomson and Box Please contact FA Support to verify connectivity DocId: {0}. Exception: {1}, {ControllerName}", docDownloadRequest.DocId, ex1, "SearchController:GetDocument");
                        }
                    }
                }

            }
            finally
            {

            }
            return new DownloadDocumentResponse
            {
                File = file,
                FileName = docDownloadRequest.FileName,
                ResponseCode = 200,
                Source = source,
                BoxFileId = boxFileId,
                Pages = pages
            };
        }

        public DocDownloadRequest MapDiDefEntities(DiDef request, string pages)
        {
            var docDownloadRequest = new DocDownloadRequest();

            docDownloadRequest.Analyst = request.MainAuthor?.Value;
            docDownloadRequest.ArriveDate = request.ArriveDate;
            docDownloadRequest.CompanyName = request.CompanyName;
            docDownloadRequest.CtbId = request.CtbId;
            docDownloadRequest.DocClass = request.DocClass;
            docDownloadRequest.DocId = request.DocId;
            docDownloadRequest.DocName = request.Headline;
            docDownloadRequest.DocTitle = request.Headline;
            docDownloadRequest.EnhancedPdf = request.EnhancedPdf;
            docDownloadRequest.FileExt = request.FileExt;
            docDownloadRequest.FileName = request.FileName;
            docDownloadRequest.FileSize = request.FileSize;
            docDownloadRequest.FileType = request.FileType;
            docDownloadRequest.HasEarns = request.HasEarns;
            docDownloadRequest.HasFpp = request.HasFpp;
            docDownloadRequest.HasSyn = request.HasSyn;
            docDownloadRequest.HasToc = request.HasToc;
            docDownloadRequest.LocalCode = request.LocalCode;
            docDownloadRequest.Pages = pages;
            docDownloadRequest.PageNo = Convert.ToInt32(request.Pages);
            docDownloadRequest.Price = request.Price.ToString();
            docDownloadRequest.PricingType = request.PricingType;
            docDownloadRequest.ReleaseDate = request.ReleaseDate;
            docDownloadRequest.SubmitDate = request.SubmitDate;
            docDownloadRequest.TocEnd = request.TocEnd;
            docDownloadRequest.TocStart = request.TocStart;
            docDownloadRequest.TranspRpt = request.TranspRpt;
            docDownloadRequest.Headline = request.Headline;
            docDownloadRequest.PTkr = request.PTkr;
            docDownloadRequest.Tkr = request.Tkr;
            docDownloadRequest.Author = request.Author;
            docDownloadRequest.Reg = request.Reg;
            docDownloadRequest.RegG = request.RegG;
            docDownloadRequest.DocTyp = request.DocTyp;
            docDownloadRequest.Ind = request.Ind;
            docDownloadRequest.IndG = request.IndG;
            docDownloadRequest.Cntry = request.Cntry;
            docDownloadRequest.Grp = request.Grp;
            docDownloadRequest.LangDesc = request.LangDesc;
            docDownloadRequest.ReasonsResp = request.ReasonsResp;
            docDownloadRequest.RptStylesResp = request.RptStylesResp;
            docDownloadRequest.MainAuthor = request.MainAuthor;
            docDownloadRequest.RatingType = request.RatingType;
            docDownloadRequest.RecommendRating = request.RecommendRating;
            docDownloadRequest.EstimateRating = request.EstimateRating;
            docDownloadRequest.IndustryTree = request.IndustryTree;
            docDownloadRequest.NonBillablePages = request.NonBillablePages;
            docDownloadRequest.Audiences = request.Audiences;
            docDownloadRequest.CtSubjectsResp = request.CtSubjectsResp;
            docDownloadRequest.DisciplinesResp = request.DisciplinesResp;
            docDownloadRequest.Subj = request.Subj;
            docDownloadRequest.SubjG = request.SubjG;
            docDownloadRequest.ContributorName = request.CompanyName;
            docDownloadRequest.ProjectCode = request.projectCode;

            //To calculate price in case document fallback from partial to full
            docDownloadRequest.BillablePages = request.BillingPages;
            docDownloadRequest.PagePrice = request.PagePrice;

            return docDownloadRequest;
        }

        public DocDownloadRequest MapDownloadHistoryEntities(MktDownloadHist request)
        {
            if (request.DocId == 0)
            {
                //For Download from User Dashboard
                request = _mktDownloadHistService.Find(e => e.DownloadHistId == request.DownloadHistId).FirstOrDefault();
            }
            var docDownloadRequest = new DocDownloadRequest
            {
                Id = request.Id,
                RequestId = request.RequestId,
                DownloadHistId = request.DownloadHistId,
                DocId = request.DocId,
                DocName = request.DocName,
                DocTitle = request.DocTitle,
                DocReleaseDate = request.DocReleaseDate,
                UserId = request.UserId,
                Analyst = request.Analyst,
                ContributorName = request.ContributorName,
                FileName = request.FileName,
                FileSize = request.FileSize,
                FileType = request.FileType,
                PageNo = request.PageNo,
                Price = request.Price,
                Source = request.Source,
                Saved = request.Saved,
                Pages = request.Pages,
                DownloadType = request.DownloadType,
                CreatedOn = request.CreatedOn,
                DeletedById = request.DeletedById,
                DeletedOn = request.DeletedOn,
                IsDeleted = request.IsDeleted,
                BoxFileId = request.BoxFileId,
                ProjectCode = request.ProjectCode
            };

            return docDownloadRequest;
        }

        #endregion

        #region  TOC

        public async Task<TocResponse> GetToc(Guid userId, long docId)
        {
            string url = _configuration["APIEndpoints:getToc"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = new HttpResponseMessage();

            var body = new
            {
                DocTableOfContents_Request_1 = new
                {
                    docID = docId
                }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body));

            request.Content.Headers.ContentType.MediaType = "application/json";

            try
            {
                response = await SendRequest(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex + $"Exception occured in geeting TOC for DocId: {docId}");
            };


            string boxId = (await CheckIfDocumentCached(docId)).Item1;

            var ret = new TocResponse
            {
                toc = await response.Content.ReadAsStringAsync(),
                isCached = boxId != null
            };

            return ret;
        }

        public async Task<string> GetTocContext(string docId, string searchText, string searchMethod)
        {
            string url = _configuration["APIEndpoints:searchTocContext"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = new HttpResponseMessage();

            var body = new
            {
                DocSearchKeyword_Request_1 = new
                {
                    docId,
                    searchText,
                    searchMethod
                }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body));
            request.Content.Headers.ContentType.MediaType = "application/json";
            try
            {
                response = await SendRequest(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "Exception occured in geeting TOC-Context for request: {JsonConvert.SerializeObject(body)}");
            };

            return await response.Content.ReadAsStringAsync();
        }

        #endregion

        #region Database Methods

        public async Task<bool> CreateUserActivity(DocSearchUIRequest srequest, Guid userid, Guid useractiveId)
        {
            Guid useractiveid = useractiveId;
            MktUserActivity mktUserActivity = new MktUserActivity();
            mktUserActivity.UserActivityId = useractiveid;
            mktUserActivity.UserId = userid;
            mktUserActivity.UserName = "";
            mktUserActivity.reportStyles = srequest.reportStyles is null ? "" : String.Join(",", srequest.reportStyles);
            mktUserActivity.initiatingCov = srequest.initiatingCov;
            mktUserActivity.dateFrom = srequest.dateFrom;
            mktUserActivity.dateTo = srequest.dateTo;
            mktUserActivity.PagesFrom = srequest.PagesFrom;
            mktUserActivity.PagesTo = srequest.PagesTo;
            mktUserActivity.excludeCtb = srequest.excludeCtb;
            mktUserActivity.company = JsonConvert.SerializeObject(srequest.company);
            mktUserActivity.Analyst = JsonConvert.SerializeObject(srequest.Analyst);
            mktUserActivity.Industry = JsonConvert.SerializeObject(srequest.Industry);
            mktUserActivity.Country = JsonConvert.SerializeObject(srequest.Country);
            mktUserActivity.ECountry = JsonConvert.SerializeObject(srequest.ECountry);
            mktUserActivity.Region = JsonConvert.SerializeObject(srequest.Region);
            mktUserActivity.ERegion = JsonConvert.SerializeObject(srequest.ERegion);
            mktUserActivity.SearchDate = DateTime.UtcNow;
            mktUserActivity.reportNo = srequest.reportNo is null ? "" : String.Join("", srequest.reportNo);
            //mktUserActivity.docId = Convert.ToInt64(srequest.pDocId);
            mktUserActivity.CreatedById = userid.ToString();
            mktUserActivity.CreatedOn = DateTime.UtcNow;
            mktUserActivity.textSearch = srequest.textSearch;
            mktUserActivity.tocSearch = srequest.tocSearch;
            mktUserActivity.headlineSearch = srequest.headlineSearch;
            mktUserActivity.contributors = JsonConvert.SerializeObject(srequest.contributors);
            mktUserActivity.pHintStr = srequest.pHintStr;
            mktUserActivity.ProjectCode = srequest.projectCode;
            mktUserActivity.searchJoinCondition = srequest.searchJoinCondition;
            mktUserActivity.LanguagePreferences = String.Join(',', srequest.Languages.Select(e => e.id));
            mktUserActivity.Source = srequest.source;

            await _context.MktUserActivity.AddAsync(mktUserActivity);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> CreateDownloadHist(DocDownloadRequest docDownloadRequest, string downloadSource, string boxFileId, string requestId, string requestSource)
        {
            MktDownloadHist mktDownloadHist = new MktDownloadHist();
            mktDownloadHist.Id = 0;
            mktDownloadHist.DownloadHistId = docDownloadRequest.DownloadHistId;
            mktDownloadHist.DocId = docDownloadRequest.DocId;
            mktDownloadHist.DocName = docDownloadRequest.DocName;
            mktDownloadHist.DocTitle = docDownloadRequest.DocTitle;
            mktDownloadHist.DocReleaseDate = docDownloadRequest.DocReleaseDate ?? docDownloadRequest.ReleaseDate;
            mktDownloadHist.UserId = docDownloadRequest.UserId;
            mktDownloadHist.Analyst = docDownloadRequest?.Analyst;
            mktDownloadHist.ContributorName = docDownloadRequest.ContributorName;
            mktDownloadHist.FileName = docDownloadRequest.FileName;
            mktDownloadHist.FileSize = Convert.ToInt32(docDownloadRequest.FileSize);
            mktDownloadHist.FileType = docDownloadRequest.FileType.ToString();
            mktDownloadHist.CreatedOn = DateTime.UtcNow;
            mktDownloadHist.BoxFileId = Convert.ToInt64(boxFileId);
            mktDownloadHist.DownloadType = (docDownloadRequest.Pages != null && docDownloadRequest.Pages != "" && docDownloadRequest.Pages.ToLower() != "all" && docDownloadRequest.FileType == "pdf") ? "Partial" : "Full";
            mktDownloadHist.PageNo = Convert.ToInt32(docDownloadRequest.PageNo);
            mktDownloadHist.Pages = docDownloadRequest.Pages;
            mktDownloadHist.Price = docDownloadRequest?.Price.ToString();
            mktDownloadHist.ProjectCode = docDownloadRequest.ProjectCode;
            mktDownloadHist.Source = downloadSource;
            mktDownloadHist.RequestId = requestId;
            mktDownloadHist.Saved = "0";
            mktDownloadHist.RequestSource = requestSource;

            if (downloadSource == "Box")
            {
                mktDownloadHist.Saved = docDownloadRequest.Price.ToString() == "0" ? docDownloadRequest.Saved.ToString() : docDownloadRequest.Price.ToString();
                mktDownloadHist.Price = "0";
                mktDownloadHist.Pages = "all";
            }

            await _context.AddAsync(mktDownloadHist);
            try
            {
                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to write Download history to DB. Object: {JsonConvert.SerializeObject(mktDownloadHist)}");
            }
            return false;
        }

        public async Task<MktUsageLog> GetLastLogin(Guid userId)
        {
            return await _context.MktUsageLogs.Where(e => e.UserId == userId && e.LogType == "1").OrderByDescending(e => e.LogDate).FirstOrDefaultAsync();
        }

        public async Task<(string, string)> CheckIfDocumentCached(long docId)
        {
            var doc = _context.MktBoxdocInfo.Where(e => e.DocId == docId);
            if (await doc.AnyAsync())
            {
                return (doc.First().Boxid.ToString(), doc.First().FileName);
            }
            return (null, null);
        }

        public async Task<bool> AddBoxDocInfo(DocDownloadRequest request, BoxFile boxFile)
        {
            MktBoxdocInfo mktBoxdocInfo = new MktBoxdocInfo();
            mktBoxdocInfo.DocId = request.DocId;
            mktBoxdocInfo.DocType = boxFile.Type.ToString();
            mktBoxdocInfo.Boxid = Convert.ToInt64(boxFile.Id);
            mktBoxdocInfo.SequenceId = Convert.ToInt16(boxFile.SequenceId);
            mktBoxdocInfo.Etag = boxFile.ETag.ToString();
            mktBoxdocInfo.FileName = boxFile.Name.ToString();
            mktBoxdocInfo.FileSize = Convert.ToInt32(boxFile.Size);
            mktBoxdocInfo.ContentCreatedOn = Convert.ToDateTime(boxFile.ContentCreatedAt).ToUniversalTime();
            mktBoxdocInfo.OwnedByName = boxFile.OwnedBy.Name.ToString();
            mktBoxdocInfo.ParentId = Convert.ToInt64(boxFile.Parent.Id);
            mktBoxdocInfo.ParentName = boxFile.Parent.Name;
            mktBoxdocInfo.ParentEtag = boxFile.Parent.ETag;
            mktBoxdocInfo.ParentType = boxFile.Parent.Type;
            mktBoxdocInfo.ItemStatus = Convert.ToBoolean(boxFile.Parent.ItemStatus);
            mktBoxdocInfo.ParentSequenceId = boxFile.Parent.SequenceId;
            mktBoxdocInfo.CreatedOn = Convert.ToDateTime(boxFile.CreatedAt).ToUniversalTime();
            mktBoxdocInfo.CreatedByName = boxFile.CreatedBy.Name.ToString();
            await _context.MktBoxdocInfo.AddAsync(mktBoxdocInfo);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region TRKD Connection Helpers

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage req)
        {
            string token = GetCachedToken();
            var client = _clientFactory.CreateClient("trkd");
            client.DefaultRequestHeaders.Add("X-Trkd-Auth-Token", token);
            req.Content.Headers.ContentType.MediaType = "application/json";

            return await client.SendAsync(req);
        }

        private string GetCachedToken()
        {
            string token;
            if (!_cache.TryGetValue("token", out token))
            {
                try
                {
                    token = GenerateToken().Result;
                    AddTokenToCache(token);
                }

                catch (Exception ex)
                {
                    try
                    {
                        //Retry once after 1 second delay in case of throttling error
                        if (ex.Message.Contains("General_ThrottlingApplicationLimitExceeded"))
                            Task.Delay(1000).RunSynchronously();

                        token = GenerateToken().Result;
                        AddTokenToCache(token);
                    }
                    catch
                    {
                        _logger.Error(ex, $"Error in Get Token Request. Falling back on oldToken");
                        token = _cache.Get<string>("oldToken");
                    }
                }
            }

            return token;
        }

        private void AddTokenToCache(string token)
        {

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(80));
            _cache.Set("token", token, cacheEntryOptions);

            var oldCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(90));
            _cache.Set("oldToken", token, oldCacheEntryOptions);


        }

        private async Task<string> GenerateToken()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration["APIEndpoints:getToken"]);
            var response = new HttpResponseMessage();

            var body = new
            {
                CreateServiceToken_Request_1 = new
                {
                    ApplicationID = _configuration["trkdCredentials:ApplicationID"],
                    Username = _configuration["trkdCredentials:Username"],
                    Password = _configuration["trkdCredentials:Password"]
                }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body));

            request.Content.Headers.ContentType.MediaType = "application/json";

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception occured in Generate Token. Retrying once more. Request: {0}, {ControllerName}", JsonConvert.SerializeObject(body), "SearchController: GenerateToken");
                response = await client.SendAsync(request);
            };

            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CreateServiceTokenResponse>(responseString).CreateServiceToken_Response_1.Token;
            else
                throw new Exception($"Error in Generating the token. Fault Response: {responseString}");
        }

        #endregion

        public async Task<IEnumerable<string>> GetProjectCodes(string searchstring, int recordscount)
        {
            string responsestring = "";
            var client = _clientFactory.CreateClient();

            bool useProjectCodeApi = _configuration.GetValue<bool>("UseProjectCodeApi");
            if (!useProjectCodeApi)
                return _context.ProjectCode.Where(e => e.Code.StartsWith(searchstring)).Take(recordscount).Select(e => e.Code);

            string url = _configuration["ConnectLiteService"];
            if (url is null || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "other")
                return new List<string>() { "p1", "p2", "p3" };


            if (searchstring != null && recordscount != 0)
            {
                var dict = new Dictionary<string, string>();
                dict["searchstring"] = searchstring;
                dict["recordscount"] = recordscount.ToString();
                url = QueryHelpers.AddQueryString(url, dict);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = new HttpResponseMessage();

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception occured in getting Project codes from connect lite service for URL {0}", url);
                return new List<string>() { "c1", "c2", "c3" };
            };

            responsestring = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<string>>(responsestring);
            }
            //else
            //    _logger.Error(new Exception(responsestring), "Connect Lite responded with an error");

            return new List<string>() { "p1", "p2", "p3" };
        }

        #region Obsolete

        public async Task<bool> AddBoxDocInfo(MktDownloadHist downloadHist, BoxFile boxFile)
        {
            MktBoxdocInfo mktBoxdocInfo = new MktBoxdocInfo();
            mktBoxdocInfo.DocId = downloadHist.DocId;
            mktBoxdocInfo.DocType = boxFile.Type.ToString();
            mktBoxdocInfo.Boxid = Convert.ToInt64(boxFile.Id);
            mktBoxdocInfo.SequenceId = Convert.ToInt16(boxFile.SequenceId);
            mktBoxdocInfo.Etag = boxFile.ETag.ToString();
            mktBoxdocInfo.FileName = boxFile.Name.ToString();
            mktBoxdocInfo.FileSize = Convert.ToInt32(boxFile.Size);
            mktBoxdocInfo.ContentCreatedOn = Convert.ToDateTime(boxFile.ContentCreatedAt).ToUniversalTime();
            mktBoxdocInfo.OwnedByName = boxFile.OwnedBy.Name.ToString();
            mktBoxdocInfo.ParentId = Convert.ToInt64(boxFile.Parent.Id);
            mktBoxdocInfo.ParentName = boxFile.Parent.Name;
            mktBoxdocInfo.ParentEtag = boxFile.Parent.ETag;
            mktBoxdocInfo.ParentType = boxFile.Parent.Type;
            mktBoxdocInfo.ItemStatus = Convert.ToBoolean(boxFile.Parent.ItemStatus);
            mktBoxdocInfo.ParentSequenceId = boxFile.Parent.SequenceId;
            mktBoxdocInfo.CreatedOn = Convert.ToDateTime(boxFile.CreatedAt).ToUniversalTime();
            mktBoxdocInfo.CreatedByName = boxFile.CreatedBy.Name.ToString();
            await _context.MktBoxdocInfo.AddAsync(mktBoxdocInfo);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }
        public async Task<bool> saveSearchforUser(SaveSearchRequest srequest, Guid userid)
        {
            Guid searchid = Guid.NewGuid();
            MktSavedSearch mktSavedSearch = new MktSavedSearch();
            mktSavedSearch.SearchId = searchid;
            mktSavedSearch.UserId = userid;
            mktSavedSearch.reportStyles = string.Join(",", srequest.reportStyles);
            mktSavedSearch.initiatingCov = srequest.initiatingCov;
            mktSavedSearch.PagesFrom = srequest.PagesFrom;
            mktSavedSearch.PagesTo = srequest.PagesTo;
            mktSavedSearch.dateFrom = srequest.dateFrom;
            mktSavedSearch.dateTo = srequest.dateTo;
            mktSavedSearch.dateRange = srequest.dateRange;
            mktSavedSearch.excludeCtb = srequest.excludeCtb;
            mktSavedSearch.contributors = JsonConvert.SerializeObject(srequest.contributors);
            mktSavedSearch.company = JsonConvert.SerializeObject(srequest.company);
            mktSavedSearch.Analyst = JsonConvert.SerializeObject(srequest.Analyst);
            mktSavedSearch.Industry = JsonConvert.SerializeObject(srequest.Industry);
            mktSavedSearch.Country = JsonConvert.SerializeObject(srequest.Country);
            mktSavedSearch.ECountry = JsonConvert.SerializeObject(srequest.ECountry);
            mktSavedSearch.Region = JsonConvert.SerializeObject(srequest.Region);
            mktSavedSearch.ERegion = JsonConvert.SerializeObject(srequest.ERegion);
            mktSavedSearch.CreatedOn = DateTime.UtcNow;
            mktSavedSearch.CreatedById = userid.ToString();
            mktSavedSearch.SearchName = srequest.searchName;
            mktSavedSearch.reportNo = String.Join("", srequest.reportNo);

            mktSavedSearch.keyword1Search = srequest.keyword1Search;
            mktSavedSearch.keyword2Search = srequest.keyword2Search;
            mktSavedSearch.keyword1Type = srequest.keyword1Type;
            mktSavedSearch.keyword2Type = srequest.keyword2Type;

            await _context.MktSavedSearch.AddAsync(mktSavedSearch);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<bool> updateSearchforUser(SaveSearchRequest srequest, Guid userid)
        {
            MktSavedSearch mktSavedSearch = new MktSavedSearch();
            mktSavedSearch.SearchId = srequest.searchId;
            mktSavedSearch.UserId = userid;
            mktSavedSearch.reportStyles = string.Join(",", srequest.reportStyles);
            mktSavedSearch.initiatingCov = srequest.initiatingCov;
            mktSavedSearch.PagesFrom = srequest.PagesFrom;
            mktSavedSearch.PagesTo = srequest.PagesTo;
            mktSavedSearch.dateFrom = srequest.dateFrom;
            mktSavedSearch.dateTo = srequest.dateTo;
            mktSavedSearch.dateRange = srequest.dateRange;
            mktSavedSearch.excludeCtb = srequest.excludeCtb;
            mktSavedSearch.contributors = JsonConvert.SerializeObject(srequest.contributors);
            mktSavedSearch.company = JsonConvert.SerializeObject(srequest.company);
            mktSavedSearch.Analyst = JsonConvert.SerializeObject(srequest.Analyst);
            mktSavedSearch.Industry = JsonConvert.SerializeObject(srequest.Industry);
            mktSavedSearch.Country = JsonConvert.SerializeObject(srequest.Country);
            mktSavedSearch.ECountry = JsonConvert.SerializeObject(srequest.ECountry);
            mktSavedSearch.Region = JsonConvert.SerializeObject(srequest.Region);
            mktSavedSearch.ERegion = JsonConvert.SerializeObject(srequest.ERegion);
            mktSavedSearch.CreatedOn = DateTime.UtcNow;
            mktSavedSearch.CreatedById = userid.ToString();
            mktSavedSearch.SearchName = srequest.searchName;
            mktSavedSearch.reportNo = String.Join("", srequest.reportNo);

            mktSavedSearch.keyword1Search = srequest.keyword1Search;
            mktSavedSearch.keyword2Search = srequest.keyword2Search;
            mktSavedSearch.keyword1Type = srequest.keyword1Type;
            mktSavedSearch.keyword2Type = srequest.keyword2Type;
            mktSavedSearch.searchJoinCondition = srequest.searchJoinCondition;
            //mktSavedSearch.IsDeleted = false;

            _context.MktSavedSearch.Update(mktSavedSearch);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        #endregion
    }
}
