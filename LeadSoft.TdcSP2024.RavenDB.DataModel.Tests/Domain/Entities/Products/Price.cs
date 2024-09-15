namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products
{
    /// <summary>
    /// Representa as informações de preço, incluindo título, valor, custo, margem e datas de validade.
    /// </summary>
    public partial class Price : Validation
    {
        /// <summary>
        /// Título ou descrição do preço (exemplo: "Preço de venda", "Preço promocional").
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Valor do preço.
        /// </summary>
        public decimal Value { get; private set; } = 0;

        /// <summary>
        /// Custo associado ao produto ou serviço.
        /// </summary>
        public decimal Cost { get; private set; } = 0;

        /// <summary>
        /// Data em que o preço foi definido.
        /// </summary>
        public DateTime When { get; private set; }

        /// <summary>
        /// Data opcional até quando o preço é válido.
        /// </summary>
        public DateTime? Until { get; private set; }

        /// <summary>
        /// Margem calculada com base no valor e no custo. Retorna zero se o valor for zero.
        /// </summary>
        public decimal Margin
        {
            get => Value != 0 ? 1 - (Cost / Value) : 0;
        }
    }

}
