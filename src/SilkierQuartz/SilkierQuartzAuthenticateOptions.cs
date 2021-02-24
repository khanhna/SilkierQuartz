namespace SilkierQuartz
{
    public class SilkierQuartzAuthenticateOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsPersist { get; set; }
    }

    public class SilkierQuartzAuthenticateConfig
    {
        public static string VirtualPathRoot = string.Empty;
        public const string AuthScheme = "SilkierQuartzAuth";
        public const string SilkierQuartzSpecificClaim = "SilkierQuartzManage";
    }
}