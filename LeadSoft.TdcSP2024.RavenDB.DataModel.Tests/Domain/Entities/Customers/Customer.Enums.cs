using System.ComponentModel;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    public static partial class Enums
    {
        public enum AddressType
        {
            [Description("Padrão")]
            Standard,
            [Description("Entrega")]
            Shipping,
            [Description("Fatura")]
            Billing,
            [Description("Residencial")]
            Home,
            [Description("Trabalho")]
            Work
        }

        public enum EmailType
        {
            [Description("Residencial")]
            Personal,
            [Description("Trabalho")]
            Professional,
            [Description("Outro")]
            Other
        }
    }
}
