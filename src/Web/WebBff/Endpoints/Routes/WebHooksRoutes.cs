namespace WebBff.Endpoints.Routes
{
    internal class WebHooksRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/webhooks";

        internal const string Status = "status";
        internal const string ExternalId = "externalId";
        internal const string OrderId = "orderId";

        internal const string UpdateUserNumberIntelligence = $"{BaseUri}/claro/number-intelligence";
        internal const string SendAuth0Email = $"{BaseUri}/auth0/email/send";
        internal const string UpdateTransactionStatus = $"{BaseUri}/caf/transactions";
        internal const string CreditAnalysis = $"{BaseUri}/b2e/credit-analysis";
        internal const string ChangeExternalOrderStatus = $"{BaseUri}/bmp/externalId/{{{ExternalId}:guid}}/orderId/{{{OrderId}:guid}}/status/{{{Status}}}";
        internal const string VindiChangeBillStatus = $"{BaseUri}/vindi/bill";
        internal const string VindiChangeChargeStatus = $"{BaseUri}/vindi/charge";
    }
}
