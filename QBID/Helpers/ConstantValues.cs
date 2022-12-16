namespace QBid.Helpers
{
    /// <summary>
    /// This class is used for constant values to used in whole application.
    /// </summary>
    public class ConstantValues
    {
        #region App Resource Location for get the all text in resource file      
        public const string AppResourceLocation = "ASAProcessServices_LGLA.ASAResource.ASAResources";
        public const string AppResourceText = "Text";
        public const string EMAILREGEX = @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z";
        #endregion


        public const string DIGITREGEX = @"[^\d]";
        public const string CardPlaceHolder = "XXXX-XXXX-XXXX-";
        public const string StripeAPIKey = "sk_test_51K4V9qAoMzN8F8cffxu8FK3wNNn19GHD3xAEUQcMsqDz5lO4TW52EzsQFXry34bgqSE0WIQbIIE09BZFh5iRkMUt00d54gpwEQ";
        public const string Zero = "0";
        public const int zero = 0;
        public static int CountZero = 0;
        public const string One = "1";
        public const string Two = "2";
        public const int one = 1;
        public const string Three = "3";
        public const string Four = "4";
        public const int three = 3;
        public const string Six = "6";
        public const string Seven = "7";
        public const string BracketRoundFore = "(";
        public const string BracketRoundBack = ")";
        public const string Hyphen = "-";
        public const string OneSpace = " ";
        public const string DefaultValue = "0.00";
        public const string CurencySymbal = "$";
        public const string DateFormate = "MM/dd/yyyy hh:mm tt";
        public const string AccountNoHideText = "XXX-XXX-";
        public const string Vendor = "Vendor";
        public const string Negotiator = "Negotiator";
        public const string Member = "Member";
        public const int MinimumEarnByNegotiator = 15;
        public const int MinimumSaveByNegotiator = 15;
        public const int SavingPercent = 10;
        public const string TitelRegistrationPageSignUP = "Sign Up";
        public const string TitelRegistrationPageEditProfile = "Edit Profile";
        public const string TermsofUsePDF = "/QBIDdocs/QBID-TERMS-OF-SERVICE.pdf";
        public const string RightandProvisionsentirePDF = "/QBIDdocs/QBIDnow-Right-and-Provisions-entire.pdf";//"/Qbiddocs/QBIDnowRightandProvisionsentire.pdf";
        public const string UserPrivacyRightsPDF = "/QBIDdocs/QBID-PRIVACY-POLICY.pdf";   //"/Qbiddocs/APP_User_Your_Privacy_Rights.pdf";
        public const string NegotiatorTermConditionsPDF = "/QBIDdocs/QBID-Personal-Negotiator-Terms-and-Conditions.pdf"; //"/Qbiddocs/QBID_Personal_Negotiator_Terms_and_Conditions.pdf";
        





        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer ";
        public const string TokenKeyText="Token";
        public const string RequestHeaderFormat = "application/json";
        public const string RequestHeaderOctetFormat = "application/octet-stream";

        public const string AppLightGrayColor = "#F0F0F0";
        public const string AppColor = "#2CD49C";
        public const string AppBlackColor = "#000000";
        public const string AppRedColor = "#FF0000";
        public const string AppPinkColor = "#f06292";
        public const string AppLightRedColor = "#FDE5E5";
        public const string AppLightColor = "#D2F2E4";
        public const string AppBlueColor = "#1f2060";
        public const string AppStatusButtonColor = "#E5FAF4";
        public const string AppWhiteColor = "#FFFFFF";
        public const string AppLightSkyColor = "#E7F7F5";

        public const string StarBlank = "UnfilledRatingStar.png";
        public const string StarFill = "FilledRatingStar.png";
        //public static string QuationID = "QID"; 
        public static bool IsMessageRecived = false;
        public static string HowToUseAppLink = "/how-to-use-app";

        #region Preferences
        public const string IsloginPref = "IsLogin";
        public const string IsRememberPasswordPref = "IsRememberPassword";
        public const string UserTypePref = "UserType";
        public const string EmailPref = "Email";
        public const string PasswordPref = "Password";
        public const string IsRightsAndProvisionAcceptedPref =  "IsRightsAndProvisionAccepted";
        public const string CustomerStripeIdPref = "CustomerStripeId";
        public const string IsMemberCardAddedPref = "IsMemberCardAdded";
        public const string IsNegotiatorAccountAddedPref =  "IsNegotiatorAccountAdded";
        public const string IsBankVerifiedPref = "IsBankVerified";
        public const string IsBankAccountStatusPref = "IsBankAccountStatus";
        public const string RoleIdPref =  "RoleId";
        public const string EditProfilePref = "EditProfile";
        public const string QuotationIdPref = "quotationId";
        public const string IsBankAccountStatusImagePref = "IsBankAccountStatusImage";
        public const string IsReadIntroductionScreenPref = "IsReadIntroductionScreen";
        
        #endregion


        #region values

        public const string CheckedImageName = "checked.png";
        public const string UnCheckedImageName = "unchecked.png";
        public const string ShowPassword = "show_password.png";


        #endregion

        #region values
        public const string HideImageName = "hide_password.png";
        public const string ShowImageName = "show_password.png";
        public const string BankAccountActiveImage = "ActiveAccount.png";
        public const string BankAccountInActiveImage = "InActiveAccount.png";
        public const string BankAccountActiveStatus = "Active";
        public const string BankAccountInActiveStatus = "InActive";
        public const string BankAccountVerified = "Verified";
        public const string BankAccountUnVerified = "UnVerified";

        public const string UserProfileImage = "User.png";
        public const string NegotiatorProfileImage = "ProfileNew.png"; //"Negotiator.png";
        public const string AppLogo = "AppIcon.png";  //"QBidNowLogo.png";




        #endregion

        #region values
        public const string QBidProfileTitelMember = "Member";
        public const string QBidProfileTitelNegotiator = "Negotiator";
        public const string QBidNavigator = "QBid Member";
        public const string QBidNavigator2 = "QBid Negotiator";
        #endregion

        #region FCM 
        public const string FCMNotificationText = "FCM Notifications";
        public const string CloudMessagesAppearText = "Firebase Cloud Messages appear in this channel";
        public const string DriveNameText = "Vintageearth";
        public const string MessagingEventKey = "com.google.firebase.MESSAGING_EVENT";
        public const string FirebaseMessagingServiceTag = "VintageFirebaseMessagingService";
        public const string RefreshedTokenKey = "RefreshedToken";
        public const string FcmTokenPreferenceText = "fcmtoken";
        public const string BundleIdText = "BundleId";
        public const string FcmFromText = "From:";
        public const string RegisteredDeviceToken = "RegisteredDevice";
        public const string MessageText = "Message";
        public const string Commenttext = "Comment";
        public const string PurchasePrducttext = "Purchase";
        public const string Ordertext = "Order";
        public static string QuationID=  "QID";
        #endregion

    }
}
