﻿namespace SilkierQuartz
{
    public class SilkierQuartzAuthenticateOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsPersist { get; set; }
    }

    internal class SilkierQuartzAuthenticateConfig
    {
        internal static string VirtualPathRoot = string.Empty;
        internal static bool IsPersist;
        internal const string AuthScheme = "SilkierQuartzAuth";
        internal const string SilkierQuartzSpecificClaim = "SilkierQuartzManage";
    }
}