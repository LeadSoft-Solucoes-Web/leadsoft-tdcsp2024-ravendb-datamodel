using LeadSoft.Common.Library.Extensions;

using System.ComponentModel.DataAnnotations;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Representa um pedido no sistema, incluindo o consumidor, itens, entrega, descontos e datas relevantes.
    /// </summary>
    public partial class Order : Validation
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Número do pedido, calculado com base no ano atual e o identificador numérico do pedido.
        /// O número é derivado multiplicando o ano atual por 10.000 e adicionando os números contidos no identificador.
        /// </summary>
        public int Number { get => (DateTime.UtcNow.Year * 10000) + Id.OnlyNumeric().ToIntOrDefault(); }

        /// <summary>
        /// Consumidor (cliente) associado ao pedido.
        /// </summary>
        public Consumer Consumer { get; private set; } = null;

        /// <summary>
        /// Lista de itens do pedido.
        /// </summary>
        public IList<Item> Items { get; private set; } = new List<Item>();

        /// <summary>
        /// Informações de envio associadas ao pedido, como tipo de entrega e status.
        /// </summary>
        public Shipping Shipping { get; private set; } = null;

        /// <summary>
        /// Desconto aplicado ao pedido, que deve ser entre 0% e 80%.
        /// O desconto é representado como um valor decimal (0 a 0.8), onde 0 equivale a 0% e 0.8 equivale a 80%.
        /// </summary>
        [Range(0, 80, ErrorMessage = "Desconto não deve ser inferior a 0% ou superior a 80%.")]
        public decimal Discount { get; private set; } = 0;

        /// <summary>
        /// Representação do desconto em porcentagem, formatado conforme a cultura brasileira (ex.: 10,00%).
        /// </summary>
        public string DiscountPercent { get => Discount.ToString("P", new System.Globalization.CultureInfo("pt-BR")); }

        /// <summary>
        /// Data e hora em que o pedido foi criado. Por padrão, é a data e hora atuais em UTC.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime When { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora em que o pedido foi fechado, se aplicável. Se não foi fechado, será <see langword="null"/>.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? ClosedAt { get; private set; } = null;

        /// <summary>
        /// Verifica se o pedido foi fechado.
        /// </summary>
        /// <returns>Retorna <see langword="true"/> se o pedido já estiver fechado, caso contrário, <see langword="false"/>.</returns>
        public bool IsClosed() => ClosedAt.HasValue;

        /// <summary>
        /// Total do valor dos itens no pedido, calculado como a soma do valor total de todos os itens.
        /// </summary>
        public decimal TotalItems { get => Items.Sum(i => i.Total); }

        /// <summary>
        /// Representação do valor total dos itens, formatado como moeda conforme a cultura brasileira (ex.: R$ 1.000,00).
        /// </summary>
        public string TotalItemsCurrency { get => TotalItems.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }

        /// <summary>
        /// Total dos impostos aplicados aos itens do pedido, calculado como a soma dos impostos de cada item.
        /// </summary>
        public decimal Taxes { get => Items.Sum(i => i.Tax); }

        /// <summary>
        /// Representação do valor dos impostos, formatado como moeda conforme a cultura brasileira (ex.: R$ 200,00).
        /// </summary>
        public string TaxesCurrency { get => Taxes.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }

        /// <summary>
        /// Valor total do pedido, incluindo itens, impostos e desconto.
        /// O valor total é calculado como: (TotalItems + Taxes) - (TotalItems * Discount).
        /// </summary>
        public decimal Total { get => (TotalItems + Taxes) - (TotalItems * Discount); }

        /// <summary>
        /// Representação do valor total do pedido, formatado como moeda conforme a cultura brasileira (ex.: R$ 1.200,00).
        /// </summary>
        public string TotalCurrency { get => Total.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }
    }

}
