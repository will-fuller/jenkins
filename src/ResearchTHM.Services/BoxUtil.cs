using Box.V2;
using Box.V2.Config;
using Box.V2.Exceptions;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Models.RequestModels;
using ResearchTHM.Core.Services;
using ResearchTHM.Core.TrkdModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResearchTHM.Services
{
    public class BoxUtilService : IBoxUtilService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ResearchMktContext _context;
        BoxClient Client;
        BoxClient boxClient => this.Client ?? (this.Client = GenerateBoxClient());
        public BoxUtilService(IConfiguration configuration, IMemoryCache cache, ILogger logger, ResearchMktContext context)
        {
            _cache = cache;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public Task<Stream> DownloadFile(string fileId)
        {
            return boxClient.FilesManager.DownloadAsync(fileId);
        }

        //public Task<BoxFile> UploadFileAsync(Stream file,string filename, string parentId = "0")
        public async Task<BoxFile> UploadFileAsync(Stream file, string filename, string parentId = "0")
        {
            BoxFileRequest request = new BoxFileRequest()
            {
                Name = filename,
                Parent = new BoxRequestEntity() { Id = parentId }
            };
            try
            {
                return await boxClient.FilesManager.UploadAsync(request, file);
            }
            catch (BoxPreflightCheckConflictException<BoxFile> f)
            {
                return await boxClient.FilesManager.GetInformationAsync(f.ConflictingItem.Id);
            }

        }

        private BoxClient GetClientFromCache()
        {
            BoxClient client;

            try
            {
                // Look for cache key.
                if (!_cache.TryGetValue("boxClient", out client))
                {
                    client = GenerateBoxClient();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_configuration.GetValue<double>("Box:ClientTimeout")));

                    _cache.Set("boxClient", client, cacheEntryOptions);
                }
                return client;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Box Client not retrieved from cache");
                return GenerateBoxClient();
            }

        }

        private BoxClient GenerateBoxClient()
        {
            string boxConfigString, pkey;

            boxConfigString = _configuration.GetValue<string>("boxSettings");

            var boxConfig = BoxConfig.CreateFromJsonString(boxConfigString);
            BoxJWTAuth boxJWT = new BoxJWTAuth(boxConfig);

            try
            {
                string adminToken = boxJWT.AdminToken();
                return boxJWT.AdminClient(adminToken);
            }
            catch
            {
                _logger.Error("Error in generating box client");
                return new BoxClient(boxConfig);
            }
        }

        public async Task<object> CreateFileMetadata(DocDownloadRequest request, string boxFileId, string TemplateName)
        {
            string Company = request.PTkr?.Value.ToString();
            string Industry = String.Join(',', request.Ind?.Select(e => e?.Value ?? "") ?? new string[] { "" });
            string Contributor = request.CompanyName?.ToString();
            string Region = String.Join(',', request.Reg?.Select(e => e?.Value ?? "") ?? new string[] { "" });
            string Country = String.Join(',', request.Cntry?.Select(e => e?.Value ?? "") ?? new string[] { "" });
            string Author = String.Join(',', request.Author?.Select(e => e?.Value ?? "") ?? new string[] { "" });
            string Title = request.Headline?.ToString();
            string DocumentId = request.DocId.ToString();
            string Pages = request.Pages.ToString();
            string ReportStyle = String.Join(',', request.RptStylesResp?.Select(e => e?.Value ?? "") ?? new string[] { "" });
            string ReleaseDate = request.ReleaseDate.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
            string json = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore });
            string secondaryTickers = String.Join(',', request.Tkr?.Select(e => e?.Ric ?? "") ?? new string[] { "" });
            string PrimaryTicker = request.PTkr?.Ric ?? "";
            //request.Tkr?.Select(e=> e?.Ric)
            var md = new Dictionary<string, object>()
                                {
                                    { "Company", string.IsNullOrEmpty(Company) ? "" : Company },
                                    { "Industry", string.IsNullOrEmpty(Industry) ? "" : Industry  },
                                    { "Contributor", string.IsNullOrEmpty(Contributor) ? "" : Contributor  },
                                    { "Region", string.IsNullOrEmpty(Region) ? "" : Region },
                                    { "Country", string.IsNullOrEmpty(Country) ? "" : Country },
                                    { "Author", string.IsNullOrEmpty(Author) ? "" : Author },
                                    { "Title", string.IsNullOrEmpty(Title) ? "" : Title },
                                    { "DocumentId", string.IsNullOrEmpty(DocumentId) ? "" : DocumentId },
                                    { "Pages",  string.IsNullOrEmpty(Pages) ? "" : Pages},
                                    { "ReportStyle", string.IsNullOrEmpty(ReportStyle) ? "" : ReportStyle },
                                    { "ReleaseDate", string.IsNullOrEmpty(ReleaseDate) ? "" : ReleaseDate},
                                    { "LazardDownloadDate", DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ssZ")},
                                    { "SecondaryTickers", string.IsNullOrEmpty(secondaryTickers) ? "" : secondaryTickers},
                                    { "MetadataJson", json},
                                    { "PrimaryTicker", string.IsNullOrEmpty(PrimaryTicker) ? "" : PrimaryTicker}
            };
            Box.V2.Managers.BoxMetadataManager mm = boxClient.MetadataManager;
            var createMD = await mm.CreateFileMetadataAsync(boxFileId.ToString(), md, "enterprise", TemplateName);
            return "OK";
        }

        public async Task<BoxMetadataTemplate> createBoxTemplate(string templatekey)
        {
            var templateParams = new BoxMetadataTemplate()
            {
                TemplateKey = templatekey,
                DisplayName = templatekey,
                Scope = "enterprise",
                Fields = new List<BoxMetadataTemplateField>()
                        {
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Company",
                                DisplayName = "Company",
                                Hidden = false

                            },
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Industry",
                                DisplayName = "Industry",
                                Hidden = false
                              },
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Contributor",
                                DisplayName = "Contributor",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Region",
                                DisplayName = "Region",
                                Hidden = false
                            },
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Country",
                                DisplayName = "Country",
                                Hidden = false
                            },
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Author",
                                DisplayName = "Author",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Title",
                                DisplayName = "Title",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "DocumentId",
                                DisplayName = "DocumentId",
                                Hidden = false
                            },
                            new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "Pages",
                                DisplayName = "Pages",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "ReportStyle",
                                DisplayName = "ReportStyle",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "date",
                                Key = "ReleaseDate",
                                DisplayName = "ReleaseDate",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "date",
                                Key = "LazardDownloadDate",
                                DisplayName = "LazardDownloadDate",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "SecondaryTickers",
                                DisplayName = "SecondaryTickers",
                                Hidden = false
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "MetadataJson",
                                DisplayName = "MetadataJson",
                                Hidden = true
                            },
                             new BoxMetadataTemplateField()
                            {
                                Type = "string",
                                Key = "PrimaryTicker",
                                DisplayName = "PrimaryTicker",
                                Hidden = false
                            }
                      }
            };
            var createdTemplate = await boxClient.MetadataManager.CreateMetadataTemplate(templateParams);
            return createdTemplate;

        }

        public string RetrieveFolderIdOrCreateNew(DocDownloadRequest request)
        {
            string folderName = "";
            string parentType = "", parentId = "";

            if (request.PTkr != null && request.PTkr.PrtId != null && request.PTkr.Ric != null)
            {
                parentType = BoxFolderParents.Company.ToString();
                folderName = request.PTkr.Ric;
            }
            else if (request.RptStylesResp.Any(e => e.Uid == 150000007))
            {
                parentType = BoxFolderParents.Fixed_Income.ToString();
                folderName = "Fixed Income";
            }
            else if (request.Ind?.FirstOrDefault()?.Value != null && request.RptStylesResp.Any(e => e.Uid == 150000009))
            {
                parentType = BoxFolderParents.Industry.ToString();
                folderName = request.Ind.FirstOrDefault().Value;
            }
            else if (request.Reg?.FirstOrDefault().Value != null && request.RptStylesResp.Any(e => e.Uid == 150000011))
            {
                parentType = BoxFolderParents.Geographic.ToString();
                folderName = request.Reg.FirstOrDefault().Value;
            }
            else if (request.RptStylesResp.Any(e => (e.Uid == 150000006 || e.Uid == 150000017 || e.Uid == 150000018)))
            {
                parentType = BoxFolderParents.Economic.ToString();
                folderName = request.RptStylesResp.FirstOrDefault().Value;
            }
            else
            {
                parentType = BoxFolderParents.Others.ToString();
                folderName = "others";
            }

            parentId = _configuration.GetValue<string>($"BoxFolders:{parentType}");

            if (string.IsNullOrEmpty(parentId))
            {
                Log.Error("NOT CRITICAL: Folder Id for the Parent {0} was not found in config. Please add it to the configuration. Retrieving from Database for now.", parentType);
                parentId = _context.BoxFolderInfo.Where(e => e.FolderName == parentType && e.Parent == "root").FirstOrDefault()?.BoxFolderId.ToString();
                if (parentId is null)
                {
                    string rootFolderId = _configuration.GetValue<string>("BoxRootFolderId");
                    parentId = CreateFolderAndAddToDb(parentType, rootFolderId, "root").Id;
                }
            }

            //In case of others and Fixed Income, return the parent ID directly. For others,create/Retrieve the subfolder inside the parent folder
            if (parentType != BoxFolderParents.Others.ToString() && parentType != BoxFolderParents.Fixed_Income.ToString())
            {
                string folderId = _context.BoxFolderInfo.Where(e => e.Parent == parentType && e.FolderName == folderName).FirstOrDefault()?.BoxFolderId.ToString();

                if (folderId is null)
                {
                    folderId = CreateFolderAndAddToDb(folderName, parentId, parentType).Id;
                }

                return folderId;
            }
            else
                return parentId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BoxFolder CreateFolderAndAddToDb(string folderName, string parentId, string parentName)
        {
            int retryCount = 0;
            try
            {
                if (retryCount > 2)
                {
                    throw new Exception($"BoxFolder: {folderName}, ParentId: {parentId}, ParentName: {parentName} Not created. Retry Count Exceeded.");
                }
                var x = CreateFolder(folderName, parentId).Result;
                if (x.Id != null)
                {
                    BoxFolderInfo fi = new BoxFolderInfo()
                    {
                        BoxFolderId = x.Id,
                        FolderName = folderName,
                        Parent = parentName,
                        ParentId = parentId
                    };

                    _context.Add(fi);
                    _context.SaveChanges();
                }
                return x;
            }
            catch (BoxConflictException<BoxFolder> f)
            {

                if (f.ConflictingItems.Any())
                {
                    return f.ConflictingItems.FirstOrDefault();
                }
                else if (f.Error.Name == "name_temporarily_reserved")
                {
                    retryCount++;
                    return CreateFolderAndAddToDb(folderName, parentId, parentName); //Recursive call to Box to create will return the box folder from the conflicting items from location ^^ (Line# - 4)
                }
                else
                    throw f;
            }
            catch (AggregateException ae)
            {

                var inner = ae.InnerExceptions.FirstOrDefault();
                if (inner is BoxConflictException<BoxFolder>)
                {
                    var f = inner as BoxConflictException<BoxFolder>;
                    _logger.Warning($"BOX Conflict Error: {JsonConvert.SerializeObject(f)}");
                    if (f.ConflictingItems != null && f.ConflictingItems.Any())
                    {
                        _logger.Warning($"Conflict Found for folder: {f.ConflictingItems.First().Name}. Conflicting Items => {String.Join(',', f.ConflictingItems.Select(e => e.Name + "," + e.Id))}");
                        return f.ConflictingItems.FirstOrDefault();
                    }
                    else if (f.Error.Name == "name_temporarily_reserved" || f.Error.Code == "name_temporarily_reserved")
                    {
                        return CreateFolderAndAddToDb(folderName, parentId, parentName); //Recursive call to Box to create will return the box folder from the conflicting items from location ^^ (Line# - 4)
                    }
                    throw f;
                }
                else
                {
                    throw ae;
                }
            }
        }

        public Task<BoxFolder> CreateFolder(string folderName, string parentId)
        {
            // Create a new folder in the user's root folder
            var folderParams = new BoxFolderRequest()
            {
                Name = folderName,
                Parent = new BoxRequestEntity()
                {
                    Id = parentId
                }
            };

            return boxClient.FoldersManager.CreateAsync(folderParams);
        }

        public Task<BoxFile> GetMetadata(string id)
        {
            return boxClient.FilesManager.GetInformationAsync(id, new string[] { $"metadata.enterprise_326689317.brcachetmp1" });
        }


        enum BoxFolderParents { Company, Industry, Geographic, Economic, Fixed_Income, Others }
    }
}
