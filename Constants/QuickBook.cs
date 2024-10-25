namespace QuickBookApi.Constants;

public static class QuickBook
{
    public record Route(string route) 
    {
        //public static readonly Route Base = new("https://appcenter.intuit.com/connect/oauth2");
        public static readonly Route Auth = new("https://appcenter.intuit.com/connect/oauth2");
        public static readonly Route Token = new("https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer");
        public static readonly Route AuthToken = new("https://oauth.platform.intuit.com/op/v1");

    }

    public record Scope(string scope)
    {
        public static readonly Scope Accounting = new("com.intuit.quickbooks.accounting");

        public static readonly Scope Payment = new("com.intuit.quickbooks.payment");

        public static readonly Scope OpenId = new("Openid");

        public static readonly Scope Profile = new("Profile");

        public static readonly Scope Address = new("Address");

        public static readonly Scope Email = new("Email");

        public static readonly Scope Phone = new("Phone");
    }
}
