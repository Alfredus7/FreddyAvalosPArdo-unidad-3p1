using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Newtonsoft.Json;
using ICG.Corporate.Website.ApiClient.Enums;
using ICG.Corporate.Website.ApiClient.Helpers;
using ICG.Corporate.Website.ApiClient.Model;
using Blackfeather.Data.Encoding;
using Blackfeather.Security.Cryptography;
using Blackfeather.Extention;
using ICG.Corporate.Website.Models.CorporateWebApi.ICGProspect;
using ICG.Corporate.Website.Models.CorporateWebApi.ICGAsset;
using ICG.Corporate.Website.Models.CorporateWebApi.ICGLinkPage;
using ICG.Corporate.Website.Models.CorporateWebApi;
using ICG.Corporate.Website.Models.CorporateWebApi.ICGInterests;
using ICG.Corporate.Website.Models.CorporateWebApi.Supports;
using ICG.Corporate.Website.Models.CorporateWebApi.ICGWebsiteUser;
using System.IO;
using ICG.Corporate.Website.Models.CorporateWebApi.Files;
using System.Web;
using ICG.Corporate.Website.Models.CorporateWebApi.Messages;
using ICG.Corporate.Website.Models.CorporateWebApi.Faqs;
using ICG.Corporate.Website.ApiClients.Model;
using System.Collections.Specialized;
using ICG.Corporate.Website.Models.CorporateWebApi.SecureUpload;
using ICG.Corporate.Website.Models.CorporateWebApi.CorporateMenus;
using ICG.Corporate.Website.Models.CorporateWebApi.Purchases;
using System.Threading.Tasks;
using ICG.Corporate.Website.Models.CorporateWebApi.Token;

namespace ICG.Corporate.Website.ApiClient
{
    /// <summary>
    /// The Corporate Api Client
    /// </summary>
    public sealed class CorporateWebApiClient : IDisposable
    {
        private bool disposed = false;
        private string ApiBaseAddress;
        private string _username;
        private readonly WebClient _client = new WebClient();
        private readonly ApiClientMedium _transmittionMedium;
        private readonly Guid _apiId;
        private readonly byte[] _apiKey;

        /// <summary>
        /// The Corporate Api Client
        /// </summary>
        /// <param name="transmittionMedium">Type of medium you wish the api client to talk in (Default is JSON)</param>
        public CorporateWebApiClient(ApiClientMedium transmittionMedium = ApiClientMedium.Json, string username = "")
        {
            string value = string.Empty;
            ApiBaseAddress = ConfigurationManager.AppSettings["CorporateWebApiUrl"];
            _apiId = new Guid(ConfigurationManager.AppSettings["CorporateWebApiId"]);
            value = ConfigurationManager.AppSettings["CorporateWebApiKey"];

            byte[] key = value.FromHex();

            _apiKey = key;

            _username = username;
            _transmittionMedium = transmittionMedium;
            ResetWebClient();
        }

        private void ResetWebClient()
        {
            _client.Headers.Clear();
            switch (_transmittionMedium)
            {
                case ApiClientMedium.Xml:
                    _client.Headers[HttpRequestHeader.ContentType] = "application/xml";
                    break;
                case ApiClientMedium.Json:
                    _client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("transmittionMedium", _transmittionMedium, null);
            }
        }

        #region Methods

        #region PartnerCodes

        /// <summary>
        /// GetPartnerCodes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public CoorporateApiResult<T> GetPartnerCodes<T>(string code, PagingParameterModel pagerModel = null)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(code))
                qs.Add(string.Format("code={0}", code));
            if (pagerModel != null)
            {
                if (pagerModel.page != 0)
                    qs.Add(string.Format("page={0}", pagerModel.page));
                if (pagerModel.size != 0)
                    qs.Add(string.Format("size={0}", pagerModel.size));
            }

            string address = GenerateApiAddress("PartnerZone");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetDataByAddress<T>(String.Format("{0}", address));
        }

        public CoorporateApiResult<T> GetPartnerCode<T>(Guid id)
        {
            string address = GenerateApiAddress("PartnerZone");

            return GetDataByAddress<T>(String.Format("{0}/{1}", address, id));
        }

        #endregion

        #region ICGWebUser

        public CoorporateApiResult<ICGWebsiteUserModel> GetWebUser(Guid id)
        {
            string address = GenerateApiAddress("ICGWebsiteUser");

            return GetDataByAddress< ICGWebsiteUserModel>(String.Format("{0}/{1}", address, id));
        }
        public CoorporateApiResult<ICGWebsiteUserModel> GetWebUser(ICGWebsiteUserForGetModel request)
        {
            string address = GenerateApiAddress("ICGWebsiteUser/GetByEmail");

            return PostData<ICGWebsiteUserForGetModel, ICGWebsiteUserModel>(String.Format("{0}", address), request);
        }

        public CoorporateApiResult<ICGWebsiteUserModel> AddNewWebUser(ICGWebsiteUserForAddModel iCGWebsiteUser)
        {
            string address = GenerateApiAddress("ICGWebsiteUser");

            return PostData<ICGWebsiteUserForAddModel, ICGWebsiteUserModel>(String.Format("{0}", address), iCGWebsiteUser);
        }

        public CoorporateApiResult<ICGWebsiteUserModel> UpdateWebUser(Guid userGuid, ICGWebsiteUserForUpdateModel userModel)
        {
            string address = GenerateApiAddress("ICGWebsiteUser");

            return PutData<ICGWebsiteUserForUpdateModel, ICGWebsiteUserModel>(String.Format("{0}/{1}", address, userGuid), userModel);
        }

        #endregion


        #region Prospects

        public CoorporateApiResult<ICGProspectModel> GetProspect(Guid id)
        {
            string address = GenerateApiAddress("ICGProspect");

            return GetDataByAddress<ICGProspectModel>(String.Format("{0}/{1}", address, id));
        }

        public CoorporateApiResult<ICGProspectModel> AddNewProspect(ICGProspectForAddModel prospect)
        {
            string address = GenerateApiAddress("ICGProspect");

            return PostData<ICGProspectForAddModel, ICGProspectModel>(String.Format("{0}", address), prospect);
        }

        public CoorporateApiResult<ICGProspectModel> AddProspectAccess(Guid id, ICGProspectAccessModel access)
        {
            string address = GenerateApiAddress("ICGProspect");

            return PostData<ICGProspectAccessModel, ICGProspectModel>(String.Format("{0}/{1}/access", address, id), access);
        }
        
        #endregion


        #region Assets

        public CoorporateApiResult<ICGAssetModel> GetAsset(Guid id)
        {
            string address = GenerateApiAddress("asset");

            return GetDataByAddress<ICGAssetModel>(String.Format("{0}/{1}", address, id));
        }

        public CoorporateApiResult<PagedList<ICGAssetModel>> GetAssets(ICGAssetParameterModel model)
        {
            List<string> qs = new List<string>();
            if(!string.IsNullOrEmpty(model.Name))
                qs.Add(string.Format("name={0}", model.Name));
            if (model.Action.HasValue && model.Action.Value != 0)
                qs.Add(string.Format("action={0}", (int)model.Action.Value));
            if (model.Source.HasValue && model.Source.Value != 0)
                qs.Add(string.Format("Source={0}", (int)model.Source.Value));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("asset");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<ICGAssetModel>(address);
        }

        #endregion


        #region Interests

        public CoorporateApiResult<PagedList<InterestModel>> GetInterests(InterestParameterModel model)
        {
            List<string> qs = new List<string>();
            if(!string.IsNullOrEmpty(model.Name))
                qs.Add(string.Format("name={0}", model.Name));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("interest");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<InterestModel>(address);
        }

        #endregion


        #region LinkPages

        public CoorporateApiResult<ICGLinkPageModel> GenerateNewLink(ICGLinkPageForAddModel link)
        {
            string address = GenerateApiAddress("LinkPage");

            return PostData<ICGLinkPageForAddModel, ICGLinkPageModel>(String.Format("{0}", address), link);
        }

        #endregion


        #region Faq
        public CoorporateApiResult<PagedList<FaqCategoryForListModel>> GetAllFaqCategories(PagingParameterModel model)
        {
            List<string> qs = new List<string>();
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("faqcategory");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<FaqCategoryForListModel>(address);
        }


        public CoorporateApiResult<PagedList<FaqPostForListModel>> GetAllFaqPostByCategory(Guid categoryGuid, FaqPostParameterModel model)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(model.category))
                qs.Add(string.Format("category={0}", model.category));
            if (!string.IsNullOrEmpty(model.seoname))
                qs.Add(string.Format("seoname={0}", model.seoname));
            if (!string.IsNullOrEmpty(model.search))
                qs.Add(string.Format("search={0}", model.search));
            if (!string.IsNullOrEmpty(model.title))
                qs.Add(string.Format("title={0}", model.title));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("faqcategory");
            if (qs.Any())
                address = string.Format("{0}/{1}/post/?{2}", address, categoryGuid, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<FaqPostForListModel>(address);
        }


        public CoorporateApiResult<PagedList<FaqPostForListModel>> GetAllFaqPostPaged(FaqPostParameterModel model)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(model.category))
                qs.Add(string.Format("category={0}", model.category));
            if (!string.IsNullOrEmpty(model.seoname))
                qs.Add(string.Format("seoname={0}", model.seoname));
            if (!string.IsNullOrEmpty(model.search))
                qs.Add(string.Format("search={0}", model.search));
            if (!string.IsNullOrEmpty(model.title))
                qs.Add(string.Format("title={0}", model.title));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("faqpost");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<FaqPostForListModel>(address);
        }


        public CoorporateApiResult<FaqPostModel> GetPost(Guid id)
        {
            string address = GenerateApiAddress("faqpost");

            return GetDataByAddress<FaqPostModel>(String.Format("{0}/{1}", address, id));
        }

        public CoorporateApiResult<FaqPostAttachmentModel> GetPostAttachment(Guid postGuid, Guid attachmentGuid)
        {
            string address = GenerateApiAddress("faqpost");

            return GetDataByAddress<FaqPostAttachmentModel>(String.Format("{0}/{1}/attach/{2}", address, postGuid, attachmentGuid));
        }

        #endregion


        #region Support
        public CoorporateApiResult<PagedList<SupportTopicForListModel>> GetAllTopics(PagingParameterModel model, string name = null,
                        bool? active = null, bool? ispublic = null, Guid? parentTopicId = null, Guid? departmentId = null)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(name))
                qs.Add(string.Format("name={0}", name));
            if (active.HasValue)
                qs.Add(string.Format("active={0}", active.Value));
            if (ispublic.HasValue)
                qs.Add(string.Format("ispublic={0}", ispublic.Value));
            if (parentTopicId.HasValue)
                qs.Add(string.Format("parentTopicId={0}", parentTopicId.Value));
            if (departmentId.HasValue)
                qs.Add(string.Format("departmentId={0}", departmentId.Value));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("supporttopic");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<SupportTopicForListModel>(address);
        }


        public CoorporateApiResult<PagedList<SupportTicketForListModel>> GetAllTicketsByTopic(Guid topicGuid, SupportTicketParameterModel model)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(model.summary))
                qs.Add(string.Format("summary={0}", model.summary));
            if (!string.IsNullOrEmpty(model.customNumber))
                qs.Add(string.Format("customNumber={0}", model.customNumber));
            if (!string.IsNullOrEmpty(model.search))
                qs.Add(string.Format("search={0}", model.search));
            if (model.topicId.HasValue)
                qs.Add(string.Format("topicId={0}", model.topicId.Value));
            if (!string.IsNullOrEmpty(model.topic))
                qs.Add(string.Format("topic={0}", model.topic));
            if (model.departmentId.HasValue)
                qs.Add(string.Format("departmentId={0}", model.departmentId.Value));
            if (!string.IsNullOrEmpty(model.department))
                qs.Add(string.Format("department={0}", model.department));
            if (model.status > 0)
                qs.Add(string.Format("status={0}", model.status));
            if (model.externalstatus > 0)
                qs.Add(string.Format("externalstatus={0}", model.externalstatus));
            if (model.priority > 0)
                qs.Add(string.Format("priority={0}", model.priority));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("supporttopic");
            if (qs.Any())
                address = string.Format("{0}/{1}/tickets/?{2}", address, topicGuid, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<SupportTicketForListModel>(address);
        }

        public CoorporateApiResult<PagedList<SupportTicketForListModel>> GetAllTickets(SupportTicketParameterModel model)
        {
            List<string> qs = new List<string>();
            if (!string.IsNullOrEmpty(model.owner))
                qs.Add(string.Format("owner={0}", model.owner));
            if (!string.IsNullOrEmpty(model.sharedwith))
                qs.Add(string.Format("sharedwith={0}", model.sharedwith));
            if (!string.IsNullOrEmpty(model.summary))
                qs.Add(string.Format("summary={0}", model.summary));
            if (!string.IsNullOrEmpty(model.customNumber))
                qs.Add(string.Format("customNumber={0}", model.customNumber));
            if (!string.IsNullOrEmpty(model.search))
                qs.Add(string.Format("search={0}", model.search));
            if (model.topicId.HasValue)
                qs.Add(string.Format("topicId={0}", model.topicId.Value));
            if (!string.IsNullOrEmpty(model.topic))
                qs.Add(string.Format("topic={0}", model.topic));
            if (model.departmentId.HasValue)
                qs.Add(string.Format("departmentId={0}", model.departmentId.Value));
            if (!string.IsNullOrEmpty(model.department))
                qs.Add(string.Format("department={0}", model.department));
            if (model.status > 0)
                qs.Add(string.Format("status={0}", model.status));
            if (model.externalstatus > 0)
                qs.Add(string.Format("externalstatus={0}", model.externalstatus));
            if (model.priority > 0)
                qs.Add(string.Format("priority={0}", model.priority));
            if (model.page != 0)
                qs.Add(string.Format("page={0}", model.page));
            if (model.size != 0)
                qs.Add(string.Format("size={0}", model.size));

            string address = GenerateApiAddress("supportticket");
            if (qs.Any())
                address = string.Format("{0}/?{1}", address, string.Join("&", qs.ToArray()));

            return GetPagedDataByAddress<SupportTicketForListModel>(address);
        }

        public CoorporateApiResult<SupportTicketModel> GetTicket(Guid id)
        {
            string address = GenerateApiAddress("supportticket");

            return GetDataByAddress<SupportTicketModel>(String.Format("{0}/{1}", address, id));
        }

        public CoorporateApiResult<SupportTicketModel> GetTicket(string customNumber)
        {
            string address = GenerateApiAddress("supportticket");

            return GetDataByAddress<SupportTicketModel>(String.Format("{0}/{1}", address, customNumber));
        }

        public CoorporateApiResult<SupportTicketModel> AddSupportTicket (Guid topicGuid, SupportTicketForAddModel ticketModel)
        {
            string address = GenerateApiAddress("supporttopic");
            ticketModel.Description = !string.IsNullOrEmpty(ticketModel.Description) ? HttpUtility.HtmlEncode(ticketModel.Description) : string.Empty;
            return PostData<SupportTicketForAddModel, SupportTicketModel>(String.Format("{0}/{1}/ticket", address, topicGuid), ticketModel);
        }

        public CoorporateApiResult<SupportTicketModel> UpdateSupportTicket(Guid ticketGuid, SupportTicketForUpdateModel ticketModel)
        {
            string address = GenerateApiAddress("supportticket");

            return PutData<SupportTicketForUpdateModel, SupportTicketModel>(String.Format("{0}/{1}", address, ticketGuid), ticketModel);
        }

        public CoorporateApiResult<bool> DeleteSupportTicket(Guid ticketGuid)
        {
            string address = GenerateApiAddress("supportticket");

            return DeleteEntity<bool>(String.Format("{0}/{1}", address, ticketGuid));
        }
        
        public CoorporateApiResult<SupportAttachmentModel> AddTicketAttachment(Guid ticketGuid, SupportAttachmentForAddModel attachmentModel)
        {
            string address = GenerateApiAddress("supportticket");

            return PostData<SupportAttachmentForAddModel, SupportAttachmentModel>(String.Format("{0}/{1}/attach", address, ticketGuid), attachmentModel);
        }
        
        public CoorporateApiResult<SupportAttachmentModel> GetTicketAttachment(Guid ticketGuid, Guid attachmentGuid)
        {
            string address = GenerateApiAddress("supportticket");

            return GetDataByAddress<SupportAttachmentModel>(String.Format("{0}/{1}/attach/{2}", address, ticketGuid, attachmentGuid));
        }
        
        public CoorporateApiResult<SupportTicketReplyModel> AddReplyTicket(Guid ticketGuid, SupportTicketReplyForAddModel replyModel)
        {
            string address = GenerateApiAddress("supportticket");
            replyModel.Content = !string.IsNullOrEmpty(replyModel.Content) ? HttpUtility.HtmlEncode(replyModel.Content) : string.Empty;
            return PostData<SupportTicketReplyForAddModel, SupportTicketReplyModel>(String.Format("{0}/{1}/reply", address, ticketGuid), replyModel);
        }
        
        public CoorporateApiResult<SupportAttachmentModel> AddReplyAttachment(Guid replyGuid, SupportAttachmentForAddModel attachmentModel)
        {
            string address = GenerateApiAddress("supportreply");

            return PostData<SupportAttachmentForAddModel, SupportAttachmentModel>(String.Format("{0}/{1}/attach", address, replyGuid), attachmentModel);
        }

        public CoorporateApiResult<SupportAttachmentModel> GetReplyAttachment(Guid replyGuid, Guid attachmentGuid)
        {
            string address = GenerateApiAddress("supportreply");

            return GetDataByAddress<SupportAttachmentModel>(String.Format("{0}/{1}/attach/{2}", address, replyGuid, attachmentGuid));
        }

        public CoorporateApiResult<SupportTicketSharedModel> AddShareTicket(Guid ticketGuid, SupportTicketSharedForActionModel shareModel)
        {
            string address = GenerateApiAddress("supportticket");

            return PostData<SupportTicketSharedForActionModel, SupportTicketSharedModel>(String.Format("{0}/{1}/share", address, ticketGuid), shareModel);
        }

        public CoorporateApiResult<SupportTicketSharedModel> UpdateShareTicket(Guid ticketGuid, SupportTicketSharedForActionModel shareModel)
        {
            string address = GenerateApiAddress("supportticket");

            return PutData<SupportTicketSharedForActionModel, SupportTicketSharedModel>(String.Format("{0}/{1}/share", address, ticketGuid), shareModel);
        }

        public CoorporateApiResult<bool> DeleteShareTicket(Guid ticketGuid, string email)
        {
            string address = GenerateApiAddress("supportticket");

            return DeleteEntity<bool>(String.Format("{0}/{1}/share/{2}/", address, ticketGuid, email));
        }

        public CoorporateApiResult<bool> PushToOsTicket(Guid ticketGuid, SupportTicketForPushOsTicketModel modelForPush)
        {
            string address = GenerateApiAddress("supportticket");

            return PostData<SupportTicketForPushOsTicketModel, bool>(String.Format("{0}/{1}/osticket", address, ticketGuid), modelForPush);
        }

        #endregion

        #region Secure Upload

        public async Task<CoorporateApiResult<List<SecureUploadModel>>> SecureUploadUploadNewResourceFile(SecureUploadModelForAdd secureUploadModelForAdd)
        {
            string address = GenerateApiAddress("SecureUpload");
            var args = new NameValueCollection();
            if(!string.IsNullOrEmpty(secureUploadModelForAdd.Notes))
                args.Add("Notes", secureUploadModelForAdd.Notes);
            if (!string.IsNullOrEmpty(secureUploadModelForAdd.UserName))
                args.Add("UserName", secureUploadModelForAdd.UserName);
            if (!string.IsNullOrEmpty(secureUploadModelForAdd.UserEmail))
                args.Add("UserEmail", secureUploadModelForAdd.UserEmail);
            args.Add("UploadType", ((int)secureUploadModelForAdd.UploadType).ToString());

            return await UploadFiles<List<SecureUploadModel>>(String.Format("{0}", address), secureUploadModelForAdd.files, args);

        }

        #endregion

        #region Resource Files

        public CoorporateApiResult<List<ResourceFileAfterUploadModel>> UploadNewResourceFile(HttpPostedFileBase file)
        {
            string address = GenerateApiAddress("ResourceFile");

            return UploadFile<List<ResourceFileAfterUploadModel>>(String.Format("{0}", address), file);

        }

        public CoorporateApiResult<byte[]> DownloadResourceFile(Guid fileId)
        {
            string address = GenerateApiAddress("ResourceFile");

            return DownloadDataByAddress(String.Format("{0}/{1}", address, fileId));
        }

        public CoorporateApiResult<List<ResourceFileAfterUploadModel>> UploadNewSupportResourceFileS3(HttpPostedFileBase file)
        {
            string address = GenerateApiAddress("SupportResourceFileS3");

            return UploadFile<List<ResourceFileAfterUploadModel>>(String.Format("{0}", address), file);

        }

        public CoorporateApiResult<byte[]> DownloadSupportResourceFileS3(Guid fileId)
        {
            string address = GenerateApiAddress("SupportResourceFileS3");

            return DownloadDataByAddress(String.Format("{0}/{1}", address, fileId));
        }

        #endregion



        #region Email Template

        public CoorporateApiResult<EmailMessageTemplateModel> GetEmailTemplate(string name)
        {
            string address = GenerateApiAddress("EmailTemplate");

            return GetDataByAddress<EmailMessageTemplateModel>(String.Format("{0}/{1}", address, name));
        }

        #endregion



        #region Stored Payment Data

        public CoorporateApiResult<PagedList<StoredPaymentDataModel>> GetStoredPaymentDataByUserId(string userName)
        {
            string address = GenerateApiAddress("StoredPaymentData");

            return GetPagedDataByAddress<StoredPaymentDataModel>(String.Format("{0}?owner={1}", address, userName));
        }

        public CoorporateApiResult<StoredPaymentDataModel> GetStoredPaymentDataById(Guid id)
        {
            string address = GenerateApiAddress("StoredPaymentData");

            return GetDataByAddress<StoredPaymentDataModel>(String.Format("{0}/{1}", address, id));
        }

        public CoorporateApiResult<StoredPaymentDataModel> AddStoredPaymentData(StoredPaymentDataForAddModel model)
        {
            string address = GenerateApiAddress("StoredPaymentData");

            return PostData<StoredPaymentDataForAddModel, StoredPaymentDataModel>(String.Format("{0}", address), model);
        }
        public CoorporateApiResult<bool> DeleteStoredPaymentData(Guid guid)
        {
            string address = GenerateApiAddress("StoredPaymentData");

            return DeleteEntity<bool>(String.Format("{0}/{1}", address, guid));
        }

        #endregion



        #region Corporate Menu

        public CoorporateApiResult<CorporateMenuModel> GetCorporateMenuByName(string name)
        {
            string address = GenerateApiAddress("CorporateMenu");

            return GetDataByAddress<CorporateMenuModel>(String.Format("{0}/{1}", address, name));
        }

        #endregion

        #region Token

        public CoorporateApiResult<TokenModel> AddNewToken(TokeForAddModel tokenModel)
        {
            string address = GenerateApiAddress("TokenLink");

            return PostData<TokeForAddModel, TokenModel>(String.Format("{0}", address), tokenModel);
        }

        public CoorporateApiResult<bool> GetToken(string token)
        {
            string address = GenerateApiAddress("TokenLink");

            return GetDataByAddress<bool>(String.Format("{0}/{1}/Validate", address, token));
        }

        public CoorporateApiResult<TokenModel> UpdateToken(string token, TokenForUpdateModel tokenModel)
        {
            string address = GenerateApiAddress("TokenLink");
            return PutData<TokenForUpdateModel, TokenModel>(String.Format("{0}/UpdateToken/{1}", address, token), tokenModel);
        }
        #endregion

        #endregion

        #region Private Methods

        private CoorporateApiResult<T> GetData<T>(string module)
        {
            string address = GenerateApiAddress(module);

            AddAuthenticationHeader();
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();
            try
            {
                // Make the request
                var response = _client.DownloadString(address);
                result.Data = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<T>(ex);
            }
            return result;

        }

        private CoorporateApiResult<T> GetDataByAddress<T>(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();
            try
            {
                // Make the request
                var response = _client.DownloadString(address);
                result.Data = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<T>(ex);
            }
            return result;
        }

        private CoorporateApiResult<byte[]> DownloadDataByAddress(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<byte[]> result = new CoorporateApiResult<byte[]>();
            try
            {
                // Make the request
                result.Data = _client.DownloadData(address);
            }
            catch (Exception ex)
            {
                HttpResponseMessageHelper.EnsureDotSuccessStatusCode<byte[]>(ex);
            }
            return result;
        }

        private CoorporateApiResult<PagedList<T>> GetPagedDataByAddress<T>(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<PagedList<T>> result = new CoorporateApiResult<PagedList<T>>();
            try
            {
                string pagingHeaders = string.Empty;
                // Make the request
                var response = _client.DownloadString(address);
                // Get response header.
                pagingHeaders = _client.ResponseHeaders["Paging-Headers"];
                var objectT = JsonConvert.DeserializeObject<List<T>>(response);
                var pagedObject = JsonConvert.DeserializeObject<PagedData>(pagingHeaders);
                var pageList = new PagedList<T>()
                {
                    CurrentPage = pagedObject.CurrentPage,
                    TotalPages = pagedObject.TotalPages,
                    PageSize = pagedObject.PageSize,
                    TotalCount = pagedObject.TotalCount,
                    PreviousPage = pagedObject.PreviousPage,
                    NextPage = pagedObject.NextPage
                };
                pageList.AddRange(objectT);
                result.Data = pageList;
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<PagedList<T>>(ex);
            }

            return result;
        }

        private CoorporateApiResult<TU> PostData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);

            // Deserialize the response into a GUID
            return PostData<T, TU>(address, model);
        }

        private CoorporateApiResult<TU> PutData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);

            // Deserialize the response into a GUID
            return PutData<T, TU>(address, model);
        }
               
        private CoorporateApiResult<TU> PostData<T, TU>(string address, T model)
        {
            // Serialize the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();

            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<TU>(ex);
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<TU> PostToken<TU>(string address, string token)
        {
            // Serialize the data we are sending in to JSON
            //string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();

            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, token);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<TU>(ex);
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<TU> PutData<T, TU>(string address, T model)
        {
            // Serialize the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();


            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, "PUT", serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<TU>(ex);
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<bool> DeleteEntity<T>(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<bool> result = new CoorporateApiResult<bool>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, "DELETE", string.Empty);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<bool>(ex);
            }
            return result;
        }
        
        private CoorporateApiResult<T> UploadFile<T>(string address, HttpPostedFileBase file)
        {
            // Serialize the data we are sending in to JSON
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();
            try
            {
                // Make the request
                var request = WebRequest.Create(address);

                AddAuthenticationHeader(request);

                request.Method = "POST";
                var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                boundary = "--" + boundary;

                using (var requestStream = request.GetRequestStream())
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", "File", file.FileName, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    file.InputStream.CopyTo(requestStream);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);

                    var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                    requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
                }

                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var stream = new MemoryStream())
                {
                    responseStream.CopyTo(stream);
                    var responseString = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    result.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<T>(ex);
            }

            // Deserialize the response into a GUID
            return result;
        }

        private async Task<CoorporateApiResult<T>> UploadFiles<T>(string address, List<UploadFileModel> files, NameValueCollection nvc)
        {
            // Serialize the data we are sending in to JSON
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

                AddAuthenticationHeader(httpClient : httpClient);
                var form = new System.Net.Http.MultipartFormDataContent();
                foreach (var file in files)
                {
                    var fileContent = new System.Net.Http.StreamContent(file.InputStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    form.Add(fileContent, "file", file.FileName);
                }
                foreach (string key in nvc.Keys)
                    form.Add(new System.Net.Http.StringContent(nvc[key]), key);

                System.Net.Http.HttpResponseMessage response = await httpClient.PostAsync(address, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                string responseString = response.Content.ReadAsStringAsync().Result;
                result.Data = JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception ex)
            {
                result = HttpResponseMessageHelper.EnsureDotSuccessStatusCode<T>(ex);
            }

            // Deserialize the response into a GUID
            return result;
        }

        private string GenerateApiAddress(string controller)
        {
            return string.Format("{0}/{1}", ApiBaseAddress, controller);
        }

        private static string Encrypt(string plain, string password, string salt)
        {
            return CipherUtility.Encrypt<AesManaged>(plain, password, salt);
        }

        private void AddAuthenticationHeader(WebRequest webRequest = null, System.Net.Http.HttpClient httpClient = null)
        {
            ResetWebClient();

            var salt = 16.ToRandomBytes();

            var expectedKey = Math.Floor(DateTime.UtcNow.TimeOfDay.TotalHours).ToString(CultureInfo.CurrentCulture).ToHmac(_apiKey, salt).Data.Append(salt);

            string header = null;
            if (!string.IsNullOrEmpty(_username))
            {
                header = string.Format("{0}:{1}:{2}", _apiId, expectedKey.ToHex(), _username);
            }
            else
            {
                header = string.Format("{0}:{1}", _apiId, expectedKey.ToHex());
            }
            if(webRequest == null && httpClient == null)
                _client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + header);
            else if(webRequest != null)
                webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + header);
            else if (httpClient != null)
                httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), "Basic " + header);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _client.Dispose();
                }

                // Dispose unmanaged managed resources.

                disposed = true;
            }
    }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion
    }
}
