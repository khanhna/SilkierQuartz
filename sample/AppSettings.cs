namespace SilkierQuartz.Example
{
    public class AppSettings
    {
        public bool EnableHelloJob { get; set; }
        public bool EnableHelloSingleJob { get; set; }
    }

    public class GlobalConfig
    {
        public sealed class AuthConfig
        {
            public const string AuthScheme = "CustomCookieAuthenticate";
            public const string AuthCookieName = "UAuth";
            public const string ClaimTypeIsAdministrator = "UAuth/IsAdministrator";
            public const string ActionNeedToAuthorize = "index,add,edit,delete,export,import";
            public const int SessionIdleValid = 28800;
            public const int RememberMeValid = 7;

            public const string BasicAuthorizePolicy = "BasicCustomPolicy";
            public const int BasicAuthorizePolicyValue = 5;
            public const string AdvanceAuthorizePolicy = "AdvanceCustomPolicy";
            public const int AdvanceAuthorizePolicyValue = 10;
        }
    }
}
