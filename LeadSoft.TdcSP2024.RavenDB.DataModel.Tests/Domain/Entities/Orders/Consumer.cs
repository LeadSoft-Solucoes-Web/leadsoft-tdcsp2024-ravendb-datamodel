using System.ComponentModel.DataAnnotations;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Representa um consumidor no sistema, com identificador, nome e endereço de e-mail.
    /// </summary>
    public partial class Consumer : Validation
    {
        /// <summary>
        /// Identificador único do consumidor.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nome do consumidor.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Endereço de e-mail do consumidor, validado pelo atributo <see cref="EmailAddressAttribute"/>.
        /// </summary>
        [EmailAddress]
        public string EmailAddress { get; private set; }
    }


}
