using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    /// <summary>
    /// Representa um contato de e-mail, com a indicação se é o e-mail principal e o tipo de e-mail.
    /// </summary>
    public partial class EmailContact : Validation
    {
        /// <summary>
        /// Indica se este é o e-mail principal do cliente.
        /// </summary>
        public bool IsPrimary { get; private set; }

        /// <summary>
        /// Tipo do e-mail (ex: pessoal, comercial). O valor padrão é <see cref="EmailType.Personal"/>.
        /// </summary>
        public EmailType Type { get; set; } = EmailType.Personal;
    }
}
