namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Representa um item dentro de um pedido, incluindo o produto, categoria, quantidade e preço.
    /// </summary>
    public partial class Item : Validation
    {
        /// <summary>
        /// Produto associado ao item.
        /// </summary>
        public Product Product { get; private set; }

        /// <summary>
        /// Quantidade do produto no item.
        /// </summary>
        public decimal Amount { get; private set; } = 0;

        /// <summary>
        /// Preço do produto no item.
        /// </summary>
        public decimal Price { get; private set; } = 0;

        /// <summary>
        /// Total do item, calculado como quantidade multiplicada pelo preço.
        /// </summary>
        public decimal Total { get => Price * Amount; }

        /// <summary>
        /// Imposto aplicado ao item, calculado como 10% do valor total.
        /// </summary>
        public decimal Tax { get => Total * 0.10m; }
    }

}
