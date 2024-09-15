using static LeadSoft.Common.Library.Enumerators.Enums;
using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    /// <summary>
    /// Representa um endereço, incluindo título, rua, número, estado (UF), cidade, país e tipo de endereço.
    /// </summary>
    public partial class Address : Validation
    {
        /// <summary>
        /// Título descritivo do endereço (exemplo: "Residencial" ou "Comercial").
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Nome da rua do endereço.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Número da residência ou prédio no endereço.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Unidade Federativa (UF) ou estado do endereço.
        /// </summary>
        public UF UF { get; set; }

        /// <summary>
        /// Cidade onde o endereço está localizado.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// País do endereço. O valor padrão é "Brasil".
        /// </summary>
        public string Country { get; private set; } = "Brasil";

        /// <summary>
        /// Tipo de endereço (ex: padrão, cobrança). O valor padrão é <see cref="AddressType.Standard"/>.
        /// </summary>
        public AddressType Type { get; private set; } = AddressType.Standard;
    }

}
