namespace QBid.Helpers
{
    public class APIHttpHelper
    {
        #region client staging environment
        //public const string BaseURL = "https://staging.qbidnow.com/api";
        //public const string DocBaseURL = "https://staging.qbidnow.com";
        //public const string BaseImageURL = "https://staging.qbidnow.com/";
        //public const string BaseImageURLForQbidList = "https://staging.qbidnow.com/uploads/profile/";

        //public const string AppCenterKey = "android=61c232fe-9d53-4bbe-a705-5f754adfcaf1;" + "ios=47b94691-1735-436b-922c-92395076c950;";

        //public const string BaseURL = "http://18.117.48.24/api";
        //public const string DocBaseURL = "http://18.117.48.24";
        //public const string BaseImageURL = "http://18.117.48.24/";
        //public const string BaseImageURLForQbidList = "http://18.117.48.24/uploads/profile/";



        #endregion


        #region client Live Server environment

        public const string BaseURL = "https://qbidnow.com/api";
        public const string DocBaseURL = "https://qbidnow.com";
        public const string BaseImageURL = "https://qbidnow.com/";
        public const string BaseImageURLForQbidList = "https://qbidnow.com/uploads/profile/";

        public const string AppCenterKey = "android=d4681091-25a5-4dbd-89a2-17e075d76193;" + "ios=74aab34a-6b24-4eb3-846d-63fc919f4151;";

        #endregion

        #region Chetu QA environment
        //public const string BaseURL = "https://chrisnevins-qa.chetu.com/api";
        //public const string DocBaseURL = "https://chrisnevins-qa.chetu.com";
        //public const string BaseImageURL = "https://chrisnevins-qa.chetu.com/";
        //public const string BaseImageURLForQbidList = "https://chrisnevins-qa.chetu.com/uploads/profile/";

        //public const string BaseURL = "https://10.0.0.49/api";
        //public const string DocBaseURL = "https://10.0.0.49";
        //public const string BaseImageURL = "https://10.0.0.49/";
        //public const string BaseImageURLForQbidList = "https://10.0.0.49/uploads/profile/";


        //public const string BaseURL = "https://chrisnevins.chetu.com/api";
        //public const string DocBaseURL = "https://chrisnevins.chetu.com";
        //public const string BaseImageURL = "https://chrisnevins.chetu.com/";
        //public const string BaseImageURLForQbidList = "https://chrisnevins.chetu.com/uploads/profile/";



       // public const string AppCenterKey = "android=26d75f5c-d68d-49de-b557-ee726f1ccc10;" + "ios=7d6a7813-cd96-4ab8-8d70-b04ffeff8733;";

        #endregion


        public const string UserRegistrationURL = "/user/userRegistration";
        public const string NegotiatorRegistrationURL = "/negotiator/negotiatorRegistration";
        public const string UserLoginURL = "/user/userLogin";
        public const string NegotiatorLoginURL = "/negotiator/negotiatorLogin";
        public const string SelectServiceURL = "/getQbidServices";
        public const string LanguageURL = "/getQbidLanguage";
        public const string AcceptRightsAndProvisionNegotiatorURL = "/negotiator/acceptRightsAndProvision";
        public const string AcceptRightsAndProvisionUserURL = "/user/acceptRightsAndProvision";
        public const string SendNewQuatationURL = "/user/sendQuote";
        public const string QuatationListURL = "/user/getUserQuotations?page={0}";
        public const string NegotiatorQuatationListURL = "/negotiator/getNegotiatorQuotations";
        public const string ForgotPasswordURL = "/forgotPassword";
        public const string HireNegotiatorURL = "/user/hireNegotiator";
        public const string ChangeNegotiatorURL = "/user/changeNegotiator";
        public const string QBiddetailsURL = "/user/getSingleQuotation/";
        public const string GetUserQuotationQbidStatusURL = "/user/getUserQuotationQbidStatus";
        public const string AcceptQBidByNegotiatorURL = "/negotiator/acceptQuotationByNegotiator";
      
        public const string ApproveQuotationByMemberURL = "/user/approveNegotiatedPrice";
        public const string DeclinedQuotationByMemberURL = "/user/declinedQuotationByUser";
        public const string GetUserDataURL = "/user/getUserDetails";
        public const string GetNegotiatorDataURL = "/negotiator/getNegotiatorDetails";
        public const string LogOutMemberURL = "/user/userLogout";
        public const string UpdateNegotiatorProfilePictureURL = "/negotiator/updateProfilePicture";
        public const string UpdateUserProfilePictureURL = "/user/updateProfilePicture";
        public const string DeleteNegotiatorProfilePictureURL = "/negotiator/deleteProfilePicture";
        public const string DeleteUserProfilePictureURL = "/user/deleteProfilePicture";
        public const string NegotiatorSubmitQuotationURL = "/negotiator/submitQuotationByNegotiator";
        public const string FilterPageURL = "/user/getUserQuotations?status={0}";
        public const string FilterNegotiatorQBidPageURL = "/negotiator/getNegotiatorQuotations?status={0}";
        public const string CreateNegotiatorRatingURL = "/user/createNegotiatorRating";
        public const string getRatingQuestionsURL = "/negotiator/getRatingQuestions";
        public const string ReSendNewQuatationURL = "/user/resendQbidLink";
        public const string CancelQbidURL = "/user/cancelQbid";
        public const string DeleteCardAPIUrl = "/user/deleteCard";
        public const string SaveCardURL = "/user/storeCard";
        public const string SetDefaultCardURL = "/user/setDefaultCard";
        public const string AddNegotiatorBankAccountURL = "/negotiator/addBankAccount";
        public const string UpdateNegotiatorBankAccountURL = "/negotiator/updateBankAccount";
        public const string SuggestionsForQbidURL = "/negotiator/suggestionsForQbid";
        public const string GetNegotiatorBankAccountURL = "/negotiator/getBankAccount";
        public const string GetSuggestionsForQbidURL = "/negotiator/getSuggestionsForQbid";
        public const string HoldQuotationByNegotiatorURL = "/negotiator/holdQuotationByNegotiator";
        public const string GetStripeKeyURL = "/getStripeKey";
        public const string GetStateNameURL = "/getState";
        public const string UpdateUserRegistrationURL = "/user/updateUserDetails";
        public const string UpdateNegotiatorRegistrationURL = "/negotiator/updateNegotiatorDetails";
        public const string DeleteUserAccountURL = "/user/deleteUserAccount";
        public const string UserRolesURL = "/getRoles";
        public const string QuotationFormPageURL = "/user/submitQuote";



    }
}
