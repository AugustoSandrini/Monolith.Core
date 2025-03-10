using Core.Application.Template;

namespace User.Application.Emails
{
    public static class Templates
    {
        public sealed class CreditConsultation() : EmailTemplate("cartaosimples/credit-consultation.html");
        public sealed class CreditAnalysis() : EmailTemplate("cartaosimples/credit-analysis.html");
    }
}
