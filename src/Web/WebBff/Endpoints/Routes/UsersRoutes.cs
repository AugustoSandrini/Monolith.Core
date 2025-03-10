namespace WebBff.Endpoints.Routes
{
    internal class UsersRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Users";

        internal const string UserId = "UserId";
        internal const string OrderId = "orderId";
        internal const string Token = "idToken";

        internal const string DeleteUser = $"{BaseUri}/{{{UserId}:guid}}";
        internal const string ValidateUserDocumentPhone = $"{BaseUri}/credit-consultation/validate";
        internal const string CreatePaymentProfile = $"{BaseUri}/payment-profile";
        internal const string UpdateProfile = $"{BaseUri}/profile";
        internal const string UpsertAddress = $"{BaseUri}/address";
        internal const string UpdateCreditConsultationStatus = $"{BaseUri}/credit-consultation/decision";

        internal const string SendToken = $"{BaseUri}/sms/token/send";
        internal const string VerifyToken = $"{BaseUri}/sms/token/verify";

        internal const string GetCreditConsultationByOrderId = $"{BaseUri}/credit-consultation/order/{{{OrderId}:guid}}";
        internal const string GetUserById = $"{BaseUri}";
        internal const string PagedCreditConsultations = $"{BaseUri}/credit-consultation/history";
        internal const string ListEstablishmentWithOrderInProgress = $"{BaseUri}/establishment/order";
        internal const string ListOrderWithEstablishmentInProgress = $"{BaseUri}/order/establishment";
        internal const string GetPurchasedOrder = $"{BaseUri}/order-purchased";
    }
}
