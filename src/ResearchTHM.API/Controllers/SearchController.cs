using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ResearchTHM.Core.Services;
using ResearchTHM.Core.TrkdModels;
using ResearchTHM.Core.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ResearchTHM.Core.Models.RequestModels;
using Box.V2.Models;
using Serilog;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using ResearchTHM.Core.Models.ResponseModels;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private IMemoryCache _cache;
        private readonly ISearchService _service;
        private readonly IMktSavedSearchService _savedSearchService;

        public IMapper _mapper { get; }

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IBoxUtilService _boxService;
        private readonly ILogger _logger;

        public SearchController(IMemoryCache memoryCache,
            ISearchService service,
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            IMktSavedSearchService savedSearchService,
            IBoxUtilService boxService,
            IMapper mapper,
            ILogger logger
            )
        {
            _service = service;
            _savedSearchService = savedSearchService;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _boxService = boxService;
            _cache = memoryCache;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("metadata")]
        public async Task<ActionResult<object>> GetMetadata(Guid groupId,Guid userId)
        {
            return await _service.GetMetadata(groupId, userId);
        }

        [HttpGet("authors")]
        public async Task<ActionResult<IEnumerable<NameWithCode>>> getAuthorsList(string authName, int rowCount = 10)
        {
            try
            {
                var authorList = await _service.GetAuthorsList(authName, rowCount);
                if (authorList == null) return NoContent();
                return Ok(authorList);
            }
            catch (Exception ex)
            {
                _logger.Error("{0}{ControllerName}", ex.Message, "SearchController: GetAuthorList");
                return Problem("Some Error Occured");
            }
        }

        [HttpGet("getOrganisations")]
        public async Task<ActionResult<IEnumerable<Result>>> getOrganisations(string searchText = "", string rowCount = "20", string type = "CommonName", string match = "beginswith")
        {
            try
            {
                var organisationList = await _service.GetOrganisations(searchText, rowCount, type, match);
                if (organisationList == null) return NoContent();
                return Ok(organisationList);
            }
            catch (Exception ex)
            {
                _logger.Error("{0}, {ControllerName}", ex.Message, "SearchController: GetOrganisations");
                return Problem("Some Error Occured");
            }
        }

        [HttpPost("ds")]
        public async Task<ActionResult<object>> HiddenDocSearch([FromQuery] Guid userId, DocSearchUIRequest srequest)
        {
            try
            {
                var docSearchObject = await _service.DocumentSearch(userId, srequest);
                if (docSearchObject == null) return NoContent();
                return Ok(docSearchObject);
            }
            catch (Exception ex)
            {
                _logger.Error("{0}, {ControllerName}", ex.Message, "SearchController:HiddenDocSearch");
                return Problem("Some Internal Error Occured");
            }
        }

        [HttpPost("download")]
        public async Task<IActionResult> GetDocument([FromQuery] Guid userId, string location, [FromQuery] string pages, string requestId, string source, DiDef request)
        {
            bool downloadSuccess = false;
            var mapDiDefEntities = _service.MapDiDefEntities(request, pages);
            mapDiDefEntities.UserId = userId;
            DownloadDocumentResponse document = new DownloadDocumentResponse();
            try
            {
                document = await _service.GetDocument(mapDiDefEntities);
                if (document.File != null || document.RedirectURL != null)
                {
                    downloadSuccess = true;
                }
                else
                {
                    return Problem();
                }
            }
            catch (Exception)
            {
                return Problem();
            }
            finally
            {
                if (downloadSuccess)
                {
                    var temp = Guid.NewGuid();
                    mapDiDefEntities.DownloadHistId = temp;
                    await _service.CreateDownloadHist(mapDiDefEntities, document.Source, document.BoxFileId, requestId, source);
                    await _service.SaveUsageLog(Request.Headers["User-Agent"], Request.HttpContext.Connection.RemoteIpAddress.ToString(), userId, temp, "3", location, DateTime.UtcNow);
                }
            }
            if (document.ResponseCode == 200)
                return File(document.File, "application/octet-stream", document.FileName);
            else
                return StatusCode(document.ResponseCode, document.RedirectURL);
        }

        [HttpPost("download/history")]
        public async Task<IActionResult> DownloadDocumentFromDownloadHistory([FromQuery] Guid userId, string location, MktDownloadHist downloadHist)
        {
            bool downloadSuccess = false;
            var mapDownloadHistoryEntities = _service.MapDownloadHistoryEntities(downloadHist);
            mapDownloadHistoryEntities.UserId = userId;
            DownloadDocumentResponse document = new DownloadDocumentResponse();
            try
            {
                document = await _service.GetDownloadDocumentFromDownloadHistory(mapDownloadHistoryEntities);
                if (document.File != null || document.RedirectURL != null)
                {
                    downloadSuccess = true;
                }
            }
            catch (Exception)
            {
                return Problem();
            }
            finally
            {
                var temp = Guid.NewGuid();
                mapDownloadHistoryEntities.DownloadHistId = temp;
                if (downloadSuccess)
                {
                    await _service.CreateDownloadHist(mapDownloadHistoryEntities, document.Source, document.BoxFileId, mapDownloadHistoryEntities.RequestId, "DownloadHistory");
                    await _service.SaveUsageLog(Request.Headers["User-Agent"], Request.HttpContext.Connection.RemoteIpAddress.ToString(), userId, temp, "3", location, DateTime.UtcNow);
                }
            }
            if (document.ResponseCode == 200)
                return File(document.File, "application/octet-stream", document.FileName);
            else
                return StatusCode(document.ResponseCode, document.RedirectURL);
        }

        [HttpGet("toc")]
        public async Task<IActionResult> getToc(Guid userId, long docId)
        {
            try
            {
                var getTocResponse = await _service.GetToc(userId, docId);
                if (getTocResponse == null) return NoContent();
                return Ok(getTocResponse);
            }
            catch (Exception ex)
            {
                _logger.Error("{0}, {ControllerName}", ex.Message, "SearchController: getToc");
                return Problem("Some error occured");
            }
        }

        [HttpGet("context")]
        public async Task<IActionResult> getTocContext(string docId, string searchText, string searchMethod)
        {
            try
            {
                var tocContext = await _service.GetTocContext(docId, searchText, searchMethod);
                if (tocContext == null) NoContent();
                return Ok(tocContext);
            }
            catch (Exception ex)
            {
                _logger.Error("{0}, {ControllerName}", ex.Message, "SearchController: getTocContext");
                return Problem("Some error occured");
            }
        }

        [HttpGet("isCached")]
        public async Task<IActionResult> getCachedForDoc(Guid userId, long docId)
        {
            string boxId = (await _service.CheckIfDocumentCached(docId)).Item1;

            return Ok(boxId != null);
        }

        [HttpPost("saveUserInfo")]
        public async Task<IActionResult> saveUserInfoOnLoad([FromQuery] Guid userId, string logType, string location, DocSearchUIRequest srequest)
        {

            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var userAgent = Request.Headers["User-Agent"];
            bool isSuccess = false;

            if (logType == "2")
            {
                System.Guid useractiveid = System.Guid.NewGuid();
                var saveUserActivity = await _service.CreateUserActivity(srequest, userId, useractiveid);
                isSuccess = await _service.SaveUsageLog(userAgent, ip, userId, useractiveid, logType, location, DateTime.UtcNow);
            }
            else if (logType == "1")
            {
                Guid id = new Guid();

                if (srequest.LoginTime == DateTime.MinValue)
                    isSuccess = await _service.SaveUsageLog(userAgent, ip, userId, id, logType, location, DateTime.UtcNow, "Research");
                else
                {
                    var lastLogin = await _service.GetLastLogin(userId);

                    if (lastLogin == null || lastLogin.LogDate.ToString("yyyyMMddHHMMss") != srequest.LoginTime.ToString("yyyyMMddHHMMss"))
                        isSuccess = await _service.SaveUsageLog(userAgent, ip, userId, id, logType, location, srequest.LoginTime, "ConnectLite");
                }
            }

            return Ok(isSuccess);
        }

        [HttpGet("addtemplate")]
        public async Task<ActionResult<BoxMetadataTemplate>> createBoxTemplate()
        {
            string templateName = _configuration.GetValue<string>("BoxMetadataTemplatename");
            return await _boxService.createBoxTemplate(templateName);
        }

        [HttpGet("projectCodes")]
        public async Task<ActionResult<IEnumerable<string>>> getProjectCodes(string searchstring, int recordscount = 20)
        {
            try
            {
                return Ok(await _service.GetProjectCodes(searchstring, recordscount));
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("entitlements")]
        public async Task<ActionResult<string>> getCtbEntitlementsForUser(Guid userId, string ctbs, bool excludeCtb)
        {
            return Ok(await _service.GetListOfEntitledContributors(userId, excludeCtb, ctbs is null ? new string[] { } : ctbs.Split(',')));
        }

        #region Saved Search
        [HttpGet("saved")]
        public IActionResult getSavedSearch(Guid userId)
        {
            return Ok(_savedSearchService.GetSavedSearch(userId));
        }

        [HttpPost("save")]
        public async Task<ActionResult<DocSearchUIRequest>> saveSearchforUser([FromQuery] Guid userId, SaveSearchRequest savedSearch)
        {
            //saveSearch
            var saveSearch = await _savedSearchService.SaveSearchForUser(savedSearch, userId);
            return Ok();
        }

        [HttpPost("saved/update")]
        public async Task<ActionResult<DocSearchUIRequest>> updateSearchforUser([FromQuery] Guid userId, SaveSearchRequest savedSearch)
        {
            //saveSearch
            var saveSearch = await _savedSearchService.UpdateSearchForUser(savedSearch, userId);
            return Ok();
        }

        [HttpGet("savedSearch/delete")]
        public async Task<IActionResult> DeleteSaveSearch(Guid searchid, string userId)
        {

            //Guid id = Guid.Parse(searchid);
            if (await _savedSearchService.DeleteSaveSearch(searchid, userId))
                return Ok(true);
            else
                return Problem();
        }
        #endregion

        private string GetCachedToken()
        {
            string token;
            try
            {
                // Look for cache key.
                if (!_cache.TryGetValue("token", out token))
                {
                    token = GenerateToken().Result;

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(80));

                    _cache.Set("token", token, cacheEntryOptions);
                }
            }
            catch (Exception ex)
            {
                return GenerateToken().Result;
            }
            return token;
        }

        private async Task<string> GenerateToken()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration.GetValue<string>("APIEndpoints:getToken"));
            var response = new HttpResponseMessage();

            string password = _configuration.GetValue<string>("trkdCredentials:Password");

            var body = new
            {
                CreateServiceToken_Request_1 = new
                {
                    ApplicationID = _configuration.GetValue<string>("trkdCredentials:ApplicationID"),
                    Username = _configuration.GetValue<string>("trkdCredentials:Username"),
                    Password = password
                }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body));

            request.Content.Headers.ContentType.MediaType = "application/json";

            try
            {
                //response = await SendRequest(request);
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception occured in Generate Token for Request: {0}, {ControllerName}", JsonConvert.SerializeObject(body), "SearchController: GenerateToken");
            };

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CreateServiceTokenResponse>(await response.Content.ReadAsStringAsync()).CreateServiceToken_Response_1.Token;

            //Log.Error("Error in Generate Token Error: {0}", response.Content.ReadAsStringAsync());
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage req)
        {
            string token = GetCachedToken();
            var client = _clientFactory.CreateClient("trkd");
            client.Timeout = TimeSpan.FromSeconds(150);

            client.DefaultRequestHeaders.Add("X-Trkd-Auth-Token", token);
            req.Content.Headers.ContentType.MediaType = "application/json";

            return await client.SendAsync(req);
        }
    }
}
