namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    public partial class Item
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Item"/>.
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Item"/> com os valores fornecidos para produto, categoria, quantidade e preço.
        /// </summary>
        /// <param name="product">Produto associado ao item.</param>
        /// <param name="amount">Quantidade do produto.</param>
        /// <param name="price">Preço do produto.</param>
        public Item(Product product, decimal amount, decimal price)
        {
            Product = product;
            Amount = amount;
            Price = price;
        }
    }

}
