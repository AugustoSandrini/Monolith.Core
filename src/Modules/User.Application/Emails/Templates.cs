using Core.Application.Template;

namespace User.Application.Emails
{
    public static class Templates
    {
        public sealed class CreditConsultation() : EmailTemplate("caminho-email");
        public sealed class CreditAnalysis() : EmailTemplate("caminho-email");
    }
}
