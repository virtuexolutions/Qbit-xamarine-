using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QBid.APILog;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.APIServices
{
    public class APIService : IDisposable
    {

        HttpClient httpClient;
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };
        public APIService()
        {


            httpClient = httpClient ?? new HttpClient(
            new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                },
            }
            , false)
            {
                BaseAddress = new Uri(APIHttpHelper.BaseURL),
            };
        }

        /// <summary>
        /// /// api call method for Get User role id
        /// </summary>
        /// <returns></returns>
        public async Task<UserRoleResponse> GetUserRolesAPI()
        {
            try
            {
                var userRolesURL = string.Empty; 
                userRolesURL = APIHttpHelper.BaseURL + APIHttpHelper.UserRolesURL;
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync((userRolesURL)).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<UserRoleResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for User registration
        /// </summary>
        /// <param name="userRegistrationRequestModel"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> UserRegistrationDetails(UserRegistrationRequestModel userRegistrationRequestModel)
        {
            try
            {
                var signUpURL = string.Empty;
                signUpURL = APIHttpHelper.BaseURL + APIHttpHelper.UserRegistrationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(userRegistrationRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((signUpURL), content);

                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UserRegistrationDetailsAPI", data);

                        var result = JsonConvert.DeserializeObject<RegistrationResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<RegistrationError>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }



        /// <summary>
        /// /// api call method for Update member registration
        /// </summary>
        /// <param name="updateNegotiatorDetailRequest"></param>
        /// <returns></returns>
        public async Task<CommonResponse> UpdateUserRegistrationDetails(UpdateUserDetailRequest updateNegotiatorDetailRequest)
        {
            try
            {
                var updateUserRegistrationURL = string.Empty;
                updateUserRegistrationURL = APIHttpHelper.BaseURL + APIHttpHelper.UpdateUserRegistrationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(updateNegotiatorDetailRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    var response = await httpClient.PostAsync((updateUserRegistrationURL), content);

                    if (response.IsSuccessStatusCode)
                    {


                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UpdateUserRegistrationDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for Negotiator registration
        /// </summary>
        /// <param name="negotiatorRegistrationRequestModel"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> NegoTiatorRegistrationDetails(NegotiatorRegistrationRequestModel negotiatorRegistrationRequestModel)
        {
            try
            {
                var signUpURL = string.Empty;
                signUpURL = APIHttpHelper.BaseURL + APIHttpHelper.NegotiatorRegistrationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(negotiatorRegistrationRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((signUpURL), content);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "NegoTiatorRegistrationDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<RegistrationResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.Savelog(ex.StackTrace, ex.Message);
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for update Negotiator detail
        /// </summary>
        /// <param name="updateNegotiatorDetailRequest"></param>
        /// <returns></returns>
        public async Task<CommonResponse> UpdateNegotiatorRegistrationDetails(UpdateNegotiatorDetailRequest updateNegotiatorDetailRequest)
        {
            try
            {
                var updateNegotiatorRegistrationURL = string.Empty;
                updateNegotiatorRegistrationURL = APIHttpHelper.BaseURL + APIHttpHelper.UpdateNegotiatorRegistrationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(updateNegotiatorDetailRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((updateNegotiatorRegistrationURL), content);

                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UpdateNegotiatorRegistrationDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for Member login
        /// </summary>
        /// <param name="userLoginRequestModel"></param>
        /// <returns></returns>

        public async Task<UserLoginResponse> UserLogin(UserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                var loginUrl = string.Empty;
                loginUrl = APIHttpHelper.BaseURL + APIHttpHelper.UserLoginURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(userLoginRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((loginUrl), content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UserLoginAPI", data);
                        var result = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content.ReadAsStringAsync().Result);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.Savelog(ex.StackTrace, ex.Message);
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for Negotiator login
        /// </summary>
        /// <param name="userLoginRequestModel"></param>
        /// <returns></returns>
        public async Task<UserLoginResponse> NegoTiatorLoginDetails(UserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                var loginURL = string.Empty;
                loginURL = APIHttpHelper.BaseURL + APIHttpHelper.NegotiatorLoginURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(userLoginRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((loginURL), content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "NegoTiatorLoginDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content.ReadAsStringAsync().Result);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// api call method for get service for negotiator
        /// </summary>
        /// <returns></returns>
        //api call method to intergrate select services
        public async Task<SelectServiceResponse> SelectService()
        {
            try
            {
                var serviceURL = string.Empty;
                serviceURL = APIHttpHelper.BaseURL + APIHttpHelper.SelectServiceURL;
                using (var httpClient = new HttpClient())
                {

                    var response = await httpClient.GetAsync((serviceURL)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<SelectServiceResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// /// api call method for ger user quatation list
        /// </summary>
        /// <returns></returns>
        public async Task<QuatationListResponse> GetQuatationList()
        {
            try
            {
                var serviceURL = string.Empty;
                serviceURL = APIHttpHelper.BaseURL + APIHttpHelper.QuatationListURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(serviceURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetQuatationListAPI");
                        var errorResult = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// api call method for ger language for negotiator
        /// </summary>
        /// <returns></returns>
        public async Task<LanguageResponse> LanguageAPI()
        {
            try
            {
                var languageURL = string.Empty;
                languageURL = APIHttpHelper.BaseURL + APIHttpHelper.LanguageURL;
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(languageURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<LanguageResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// api call method for accept right and provision negotiator
        /// </summary>
        /// <param name="acceptRightAndProvisionRequest"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public async Task<CommonResponse> AcceptRightAndProvisionNegotiator(AcceptRightAndProvisionRequest acceptRightAndProvisionRequest, int userTypeId)
        {
            try
            {
                var loginUrl = string.Empty;
                if (userTypeId == (int)UtilHelper.UserRoleType.Member)
                {
                    loginUrl = APIHttpHelper.BaseURL + APIHttpHelper.AcceptRightsAndProvisionUserURL;
                }
                else if (userTypeId == (int)UtilHelper.UserRoleType.Negotiator)
                {
                    loginUrl = APIHttpHelper.BaseURL + APIHttpHelper.AcceptRightsAndProvisionNegotiatorURL;
                }
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(acceptRightAndProvisionRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((loginUrl), content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Api call Method for send new quotation to vendor/facility
        /// </summary>
        /// <param name="sendNewQuatationRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> SendNewQuatation(SendNewQuatationRequestModel sendNewQuatationRequestModel)
        {
            try
            {
                var sendNewQuatationURL = string.Empty;
                sendNewQuatationURL = APIHttpHelper.BaseURL + APIHttpHelper.SendNewQuatationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(sendNewQuatationRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((sendNewQuatationURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "SendNewQuatationAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {

                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Api call Method for Get Member Quotation list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<QuatationListResponse> GetUserQuotation(int pageNumber)
        {
            try
            {
                var languageURL = string.Empty;
                languageURL = APIHttpHelper.BaseURL + string.Format(APIHttpHelper.QuatationListURL, pageNumber);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(languageURL).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetUserQuotationAPI");
                        var result = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Api call Method for Get Negotiator Quotation list
        /// </summary>
        /// <returns></returns>
        public async Task<NegotiatorQuotationListResponse> GetNegotiatorQuotations()
        {
            try
            {
                var quotationListURL = string.Empty;
                quotationListURL = APIHttpHelper.BaseURL + APIHttpHelper.NegotiatorQuatationListURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(quotationListURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetNegotiatorQuotationsAPI");
                        var result = JsonConvert.DeserializeObject<NegotiatorQuotationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Api call Method for forger password
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            try
            {
                var forgotPasswordURL = string.Empty;
                forgotPasswordURL = APIHttpHelper.BaseURL + APIHttpHelper.ForgotPasswordURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(forgotPasswordRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((forgotPasswordURL), content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<RegistrationError>(response.Content.ReadAsStringAsync().Result);

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Api call Method for hire Negotiator by Member
        /// </summary>
        /// <param name="hireNegotiatorRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> HierNegotiatiorDetails(HireNegotiatorRequestModel hireNegotiatorRequestModel)
        {
            try
            {
                var hireNegotiatorURL = string.Empty;
                hireNegotiatorURL = APIHttpHelper.BaseURL + APIHttpHelper.HireNegotiatorURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(hireNegotiatorRequestModel);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((hireNegotiatorURL), content);

                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "HierNegotiatiorDetailsAPi", data);

                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// API Integration for get filtered Negotitator QBid List
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public async Task<NegotiatorQuotationListResponse> GetFilterNegotiatorQBid(int statusId)
        {
            try
            {
                var FilterPageURL = string.Empty;
                FilterPageURL = string.Format((APIHttpHelper.BaseURL + APIHttpHelper.FilterNegotiatorQBidPageURL), statusId);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(FilterPageURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetFilterNegotiatorQBidAPI");
                        var result = JsonConvert.DeserializeObject<NegotiatorQuotationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<NegotiatorQuotationListResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// //api call method to intergrate GetNegotiatorQuotations
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<NegotiatorQuotationListResponse> GetNegotiatorQuotations(int pageNumber)
        {
            try
            {
                var quotationListURL = string.Empty;
                quotationListURL = APIHttpHelper.BaseURL + string.Format(APIHttpHelper.NegotiatorQuatationListURL, pageNumber);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    var response = await httpClient.GetAsync(quotationListURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetNegotiatorQuotationsAPI");
                        var result = JsonConvert.DeserializeObject<NegotiatorQuotationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// api call method to intergrate qBidStatusRequest
        /// </summary>
        /// <param name="qBidStatusRequest"></param>
        /// <returns></returns>
        public async Task<QuotationQbidStatusResponse> UserQuotationQbidStatus(QBidStatusRequest qBidStatusRequest)
        {
            try
            {
                var getUserQuotationQbidStatusURL = string.Empty;
                getUserQuotationQbidStatusURL = APIHttpHelper.BaseURL + APIHttpHelper.GetUserQuotationQbidStatusURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(qBidStatusRequest);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((getUserQuotationQbidStatusURL), content);

                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UserQuotationQbidStatusAPI", data);
                        var result = JsonConvert.DeserializeObject<QuotationQbidStatusResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QuotationQbidStatusResponse>(response.Content.ReadAsStringAsync().Result);
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// //api call method to intergrate submitQuotationByNegotiatorRequest
        /// </summary>
        /// <param name="submitQuotationByNegotiatorRequest"></param>
        /// <returns></returns>
        public async Task<CommonResponse> SubmitQuotationNegotiatorDetails(SubmitQuotationByNegotiatorRequest submitQuotationByNegotiatorRequest)
        {
            try
            {
                var submitQuotationURL = string.Empty;
                submitQuotationURL = APIHttpHelper.BaseURL + APIHttpHelper.NegotiatorSubmitQuotationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(submitQuotationByNegotiatorRequest);


                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((submitQuotationURL), content);

                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "SubmitQuotationNegotiatorDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<RegistrationError>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// API Integration for get filtered Member QBid List
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public async Task<QuatationListResponse> GetFilterData(int statusId)
        {
            try
            {
                var FilterPageURL = string.Empty;
                FilterPageURL = string.Format((APIHttpHelper.BaseURL + APIHttpHelper.FilterPageURL), statusId);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(FilterPageURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetFilterDataAPI");
                        var result = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        ///  api call method to intergrate acceptQuotationByNegotiatorRequest
        /// </summary>
        /// <param name="qBidStatusRequest"></param>
        /// <returns></returns>
        public async Task<QuotationQbidStatusResponse> AcceptQBidByNegotiator(AcceptQuotationByNegotiatorRequest acceptQuotationByNegotiatorRequest)
        {
            try
            {
                var getUserQuotationQbidStatusURL = string.Empty;
                getUserQuotationQbidStatusURL = APIHttpHelper.BaseURL + APIHttpHelper.AcceptQBidByNegotiatorURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(acceptQuotationByNegotiatorRequest);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((getUserQuotationQbidStatusURL), content);

                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "AcceptQBidByNegotiatorAPI", data);

                        var result = JsonConvert.DeserializeObject<QuotationQbidStatusResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QuotationQbidStatusResponse>(response.Content.ReadAsStringAsync().Result);
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Method for Ger single Quptation Detail by member/negotiator
        /// </summary>
        /// <returns></returns>
        public async Task<QuatationListResponse> GetQBidDetails(int QuatationId)
        {
            try
            {
                var serviceURL = string.Empty;
                serviceURL = APIHttpHelper.BaseURL + APIHttpHelper.QBiddetailsURL + QuatationId;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(serviceURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetQBidDetailsAPI", QuatationId.ToString());
                        var result = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QuatationListResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Change Negotiator by member
        /// </summary>
        /// <param name="changeNegotiatorRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ChangeNegotiatiorDetails(ChangeNegotiatorRequestModel changeNegotiatorRequestModel)
        {
            try
            {
                var changeNegotiatorURL = string.Empty;
                changeNegotiatorURL = APIHttpHelper.BaseURL + APIHttpHelper.ChangeNegotiatorURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(changeNegotiatorRequestModel);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((changeNegotiatorURL), content);

                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "ChangeNegotiatiorDetailsAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Approve QBid by Member
        /// </summary>
        /// <param name="approveQuotationRequest"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ApproveQuotationByMember(ApproveQuotationRequest approveQuotationRequest)
        {
            try
            {
                var approveQuotationByMemberURL = string.Empty;
                approveQuotationByMemberURL = APIHttpHelper.BaseURL + APIHttpHelper.ApproveQuotationByMemberURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(approveQuotationRequest);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((approveQuotationByMemberURL), content);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "ApproveQuotationByMemberAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Declined QBid by Member
        /// </summary>
        /// <param name="declinedQuotationRequest"></param>
        /// <returns></returns>
        public async Task<CommonResponse> DeclinedQuotationByMember(DeclinedQuotationRequest declinedQuotationRequest)
        {
            try
            {
                var declinedQuotationByMemberURL = string.Empty;
                declinedQuotationByMemberURL = APIHttpHelper.BaseURL + APIHttpHelper.DeclinedQuotationByMemberURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(declinedQuotationRequest);

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((declinedQuotationByMemberURL), content);
                    if (response.IsSuccessStatusCode)
                    {

                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "DeclinedQuotationByMemberAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Method for logout by member/negotiator
        /// </summary>
        /// <param name="logoutModel"></param>
        /// <returns></returns>
        public async Task<bool> LogOutByMember(LogoutModel logoutModel)
        {
            bool flag = false;
            try
            {
                var logOutMemberURL = string.Empty;
                logOutMemberURL = APIHttpHelper.BaseURL + APIHttpHelper.LogOutMemberURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(logoutModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((logOutMemberURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        Preferences.Set(ConstantValues.IsloginPref, false);
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        var errorResult = JsonConvert.DeserializeObject<LogOutByMemberResponce>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return flag;
        }
        /// <summary>
        /// Method for Get User profile Detail
        /// </summary>
        /// <returns></returns>
        public async Task<UserDetailResponse> GetUserDetail()
        {
            try
            {
                var getUserDetailURL = string.Empty;
                getUserDetailURL = APIHttpHelper.BaseURL + APIHttpHelper.GetUserDataURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(getUserDetailURL);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetUserDetailAPI");
                        var serializedresult = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<UserDetailResponse>(serializedresult);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<UserDetailResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for get negotiator profile detail
        /// </summary>
        /// <returns></returns>
        public async Task<NegotiatorDetailResponce> GetNegotiatorDetail()
        {
            try
            {
                var getNegotiatorDataURL = string.Empty;
                getNegotiatorDataURL = APIHttpHelper.BaseURL + APIHttpHelper.GetNegotiatorDataURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(getNegotiatorDataURL);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetNegotiatorDetailAPI");
                        var result = JsonConvert.DeserializeObject<NegotiatorDetailResponce>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<NegotiatorDetailResponce>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Update profile picture by Negotiator
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<UpdateProfilePictureResponseModel> UpdateProfilePictureNegotiator(Stream stream)
        {
            try
            {
                var updateProfilePictureURL = string.Empty;
                updateProfilePictureURL = APIHttpHelper.BaseURL + APIHttpHelper.UpdateNegotiatorProfilePictureURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    MultipartFormDataContent multiPart = new MultipartFormDataContent();
                    multiPart.Add(new StreamContent(stream), "profilePicture", "test.png");
                    var response = await httpClient.PostAsync(updateProfilePictureURL, multiPart);

                    if (response.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Update profile picture by Member
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<UpdateProfilePictureResponseModel> UpdateProfilePictureUser(Stream stream)
        {
            try
            {
                var updateProfilePictureURL = string.Empty;
                updateProfilePictureURL = APIHttpHelper.BaseURL + APIHttpHelper.UpdateUserProfilePictureURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    MultipartFormDataContent multiPart = new MultipartFormDataContent();
                    multiPart.Add(new StreamContent(stream), "profilePicture", "test.png");
                    var response = await httpClient.PostAsync(updateProfilePictureURL, multiPart);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// /// Method for Delete Profile picture by Member
        /// </summary>
        /// <returns></returns>
        public async Task<UpdateProfilePictureResponseModel> DeleteProfilePictureUser()
        {
            try
            {
                var updateProfilePictureURL = string.Empty;
                updateProfilePictureURL = APIHttpHelper.BaseURL + APIHttpHelper.DeleteUserProfilePictureURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    HttpContent content = new StringContent(string.Empty, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync(updateProfilePictureURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Delete Profile picture by negotiator
        /// </summary>
        /// <returns></returns>
        public async Task<UpdateProfilePictureResponseModel> DeleteProfilePictureNegotiator()
        {
            try
            {
                var updateProfilePictureURL = string.Empty;
                updateProfilePictureURL = APIHttpHelper.BaseURL + APIHttpHelper.DeleteNegotiatorProfilePictureURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    HttpContent content = new StringContent(string.Empty, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync(updateProfilePictureURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<UpdateProfilePictureResponseModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// api call method to intergrate CreateNegotiatorRating
        /// </summary>
        /// <param name="userRegistrationRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> CreateNegotiatorRating(CreateNegotiatorRatingRequest createNegotiatorRatingRequest)
        {
            try
            {
                var createNegotiatorRating = string.Empty;
                createNegotiatorRating = APIHttpHelper.BaseURL + APIHttpHelper.CreateNegotiatorRatingURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var data = JsonConvert.SerializeObject(createNegotiatorRatingRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((createNegotiatorRating), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "CreateNegotiatorRatingAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<RegistrationError>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        ///  api call method to intergrate QuestionListResponseModel
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionListResponseModel> GetUserQuestionList()
        {
            try
            {
                var getRatingQuestionsURL = string.Empty;
                getRatingQuestionsURL = APIHttpHelper.BaseURL + APIHttpHelper.getRatingQuestionsURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(getRatingQuestionsURL);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetUserQuestionListAPI");
                        var result = JsonConvert.DeserializeObject<QuestionListResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QuestionListResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for Resend Quotation to facility/vendor
        /// </summary>
        /// <param name="reSendQuatationRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ReSendNewQuatation(ReSendQuatationRequestModel reSendQuatationRequestModel)
        {
            try
            {
                var reSendNewQuatationURL = string.Empty;
                reSendNewQuatationURL = APIHttpHelper.BaseURL + APIHttpHelper.ReSendNewQuatationURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(reSendQuatationRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((reSendNewQuatationURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        /// <summary>
        /// Method for cancel QBid by memeber
        /// </summary>
        /// <param name="cancelQbidModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> CancleQbid(CancelQbidRequestModel cancelQbidModel)
        {
            try
            {
                var cancelQbidURL = string.Empty;
                cancelQbidURL = APIHttpHelper.BaseURL + APIHttpHelper.CancelQbidURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(cancelQbidModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((cancelQbidURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "CancleQbidAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }
        #region Rishabh Pandey work
        /// <summary>
        /// method to Delete card 
        /// </summary>
        /// <param name="deleteCardModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> DeleteCard(DeleteCardModel deleteCardModel)
        {
            CommonResponse result = null;
            try
            {
                string deleteCardApiURL = APIHttpHelper.BaseURL + APIHttpHelper.DeleteCardAPIUrl;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(deleteCardModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((deleteCardApiURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "DeleteCardAPI", data);
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// method to Save card to API
        /// </summary>
        /// <param name="saveCardRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> SaveCard(SaveCardRequestModel saveCardRequestModel)
        {
            CommonResponse result = null;
            try
            {
                string saveCardURL = APIHttpHelper.BaseURL + APIHttpHelper.SaveCardURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(saveCardRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((saveCardURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "SaveCardAPI", data);
                        var datad = response.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// method to Set Default card to API
        /// </summary>
        /// <param name="setDefaultCardRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> SetDefaultCardAPI(SetDefaultCardRequestModel setDefaultCardRequestModel)
        {
            CommonResponse result = null;
            try
            {
                string setDefaultCardURL = APIHttpHelper.BaseURL + APIHttpHelper.SetDefaultCardURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(setDefaultCardRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((setDefaultCardURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "SetDefaultCardAPI", data);
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// method to Set add bank account of negotiator to API
        /// </summary>
        /// <param name="AddNegotiatorBankAccount"></param>
        /// <returns></returns>
        public async Task<CommonResponse> AddNegotiatorBankAccountAPI(AddNegotiatorBankAccountRequest addNegotiatorBankAccountRequest)
        {
            CommonResponse result = null;
            try
            {
                string addNegotiatorBankAccountURL = APIHttpHelper.BaseURL + APIHttpHelper.AddNegotiatorBankAccountURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(addNegotiatorBankAccountRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((addNegotiatorBankAccountURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "AddNegotiatorBankAccountAPI", data);
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// submit Suggestion for QBid
        /// </summary>
        public async Task<CommonResponse> QueryAndComplaintAPI(QueryAndComplaintRequestModel QueryAndComplaintRequestModel)
        {
            CommonResponse result = null;
            try
            {
                string suggestionsForQbidURL = APIHttpHelper.BaseURL + APIHttpHelper.SuggestionsForQbidURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(QueryAndComplaintRequestModel);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((suggestionsForQbidURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// Get Negotiator Bank Detail api
        /// </summary>
        public async Task<GetNegotiatorBankDetailResponse> GetNegotiatorBankAccountAPI()
        {
            GetNegotiatorBankDetailResponse result = null;
            try
            {
                var getNegoBankAccountURL = string.Empty;
                getNegoBankAccountURL = APIHttpHelper.BaseURL + APIHttpHelper.GetNegotiatorBankAccountURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(getNegoBankAccountURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetNegotiatorBankAccountAPI");
                        result = JsonConvert.DeserializeObject<GetNegotiatorBankDetailResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<GetNegotiatorBankDetailResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// Get SAuggestions for QBId api
        /// </summary>
        public async Task<QueryAndComplaintResponse> GetSuggestionsForQbidAPI()
        {
            QueryAndComplaintResponse result = null;
            try
            {
                var getSuggestionsForQbidURL = string.Empty;
                getSuggestionsForQbidURL = APIHttpHelper.BaseURL + APIHttpHelper.GetSuggestionsForQbidURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.GetAsync(getSuggestionsForQbidURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        result = JsonConvert.DeserializeObject<QueryAndComplaintResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<QueryAndComplaintResponse>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }


        /// <summary>
        /// method to Set add bank account of negotiator to API
        /// </summary>
        /// <param name="UpdateNegotiatorBankAccount"></param>
        /// <returns></returns>
        public async Task<CommonResponse> UpdateNegotiatorBankAccountAPI(UpdateNegotiatorBankAccountRequest updateNegotiatorBankAccountRequest)
        {
            CommonResponse result = null;
            try
            {
                string updateNegotiatorBankAccountURL = APIHttpHelper.BaseURL + APIHttpHelper.UpdateNegotiatorBankAccountURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(updateNegotiatorBankAccountRequest);
                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync((updateNegotiatorBankAccountURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "UpdateNegotiatorBankAccountAPI", data);
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// api call method to intergrate HoldQuotationByNegotiator
        /// </summary>
        /// <param name="HoldQuotationByNegotiatorRequestModel"></param>
        /// <returns></returns>
        public async Task<CommonResponse> HoldQuotationByNegotiator(HoldQuotationByNegotiatorRequest HoldQuotationByNegotiatorRequest)
        {
            try
            {
                var holdQuotationByNegotiatorURL = string.Empty;
                holdQuotationByNegotiatorURL = APIHttpHelper.BaseURL + APIHttpHelper.HoldQuotationByNegotiatorURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(HoldQuotationByNegotiatorRequest);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync((holdQuotationByNegotiatorURL), content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Get State Name 
        /// </summary>
        public async Task<StateResponce> GetStateAPI()
        {
            StateResponce result = null;
            try
            {
                var getStateNameURL = string.Empty;
                getStateNameURL = APIHttpHelper.BaseURL + APIHttpHelper.GetStateNameURL;
                using (var httpClient = new HttpClient())
                {
                    await Task.Delay(100);
                    httpClient.DefaultRequestHeaders.Clear();
                    var response = await httpClient.GetAsync(getStateNameURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetStateAPI");
                        result = JsonConvert.DeserializeObject<StateResponce>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<StateResponce>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// Get Stripe keys
        /// </summary>
        public async Task<StripeKeyResponce> GetStripeKeyAPI()
        {
            StripeKeyResponce result = null;
            try
            {
                var getStripeKeyURL = string.Empty;
                getStripeKeyURL = APIHttpHelper.BaseURL + APIHttpHelper.GetStripeKeyURL;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    var response = await httpClient.GetAsync(getStripeKeyURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<StripeKeyResponce>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "GetStripeKeyAPI");
                        var errorResult = JsonConvert.DeserializeObject<StripeKeyResponce>(response.Content.ReadAsStringAsync().Result);
                        DependencyService.Get<IToastMessage>().LongAlert(errorResult.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return result;
        }
        /// <summary>
        /// for delete user account
        /// </summary>
        /// <returns></returns>
        public async Task<CommonResponse> DeleteUserAccountAPI(DeleteUserRequestModel deleteUserRequestModel)
        {
            try
            {

                var deleteUserAccountURL = string.Empty;
                deleteUserAccountURL = APIHttpHelper.BaseURL + APIHttpHelper.DeleteUserAccountURL;
                using (var httpClient = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(deleteUserRequestModel);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

                    HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
                    var response = await httpClient.PostAsync(deleteUserAccountURL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "DeleteUserAccountAPI", data);
                        var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                    else
                    {
                        var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }

        ///// <summary>
        ///// for delete user account
        ///// </summary>
        ///// <returns></returns>
        //public async Task<CommonResponse> DeleteUserAccountAPI(DeleteUserRequestModel deleteUserRequestModel)
        //{
        //    try
        //    {

        //        var deleteUserAccountURL = string.Empty;
        //        deleteUserAccountURL = APIHttpHelper.BaseURL + APIHttpHelper.DeleteUserAccountURL;
        //        using (var httpClient = new HttpClient())
        //        {
        //            var data = JsonConvert.SerializeObject(deleteUserRequestModel);
        //            httpClient.DefaultRequestHeaders.Clear();
        //            httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));

        //            HttpContent content = new StringContent(data, Encoding.UTF8, ConstantValues.RequestHeaderFormat);
        //            var response = await httpClient.PostAsync(deleteUserAccountURL, content);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                QBidHelper.AppCenterLogCreate(response.Content.ReadAsStringAsync().Result, "DeleteUserAccountAPI", data);
        //                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result, serializerSettings);
        //                return result;
        //            }
        //            else
        //            {
        //                var errorResult = JsonConvert.DeserializeObject<CommonResponse>(response.Content.ReadAsStringAsync().Result);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.TraceErrorLog(ex);
        //    }
        //    return null;
        //}


        /// <summary>
        /// /// api call method for User registration
        /// </summary>
        /// <param name="userRegistrationRequestModel"></param>
        /// <returns></returns>
        public async Task<QuotationResponseModel> SubmitQuotation(MultipartFormDataContent content)
        {
            try
            {
                var quotationForm = string.Empty;
                quotationForm = APIHttpHelper.BaseURL + APIHttpHelper.QuotationFormPageURL;

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add(ConstantValues.Authorization, ConstantValues.Bearer + Preferences.Get(ConstantValues.TokenKeyText, null));
                    var response = await httpClient.PostAsync(quotationForm, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<QuotationResponseModel>(response.Content.ReadAsStringAsync().Result, serializerSettings);
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return null;
        }


        //public async Task<T> PostRequestMultyPartAsync<T>(string url, MultipartFormDataContent content, bool tokenrequired) where T : new()
        //{
        //    var obj = new T();
        //    try
        //    {
        //        var current = Connectivity.NetworkAccess;
        //        if (current == Xamarin.Essentials.NetworkAccess.Internet)
        //        {
        //            string uri = ($"{APIHttpHelper.BaseUrl}{url}");
        //            using (var httpClient = new HttpClient())
        //            {
        //                if (tokenrequired)
        //                {
        //                    httpClient.DefaultRequestHeaders.Clear();
        //                    httpClient.DefaultRequestHeaders.Add(APIHttpHelper.AuthorizationText, APIHttpHelper.BearerText + Preferences.Get("Token", null));
        //                }
        //                var response = await httpClient.PostAsync(uri, content);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    obj = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        //                }
        //                else
        //                {
        //                    obj = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        //                    switch (response.StatusCode)
        //                    {
        //                        case HttpStatusCode.Unauthorized:
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Utility.ShowMessage(CommonResource.InternetError);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //        Utility.ShowMessage(CommonResource.Error);
        //    }
        //    return obj;
        //}



        public void Dispose()
        {

        }
        #endregion
    }
}
