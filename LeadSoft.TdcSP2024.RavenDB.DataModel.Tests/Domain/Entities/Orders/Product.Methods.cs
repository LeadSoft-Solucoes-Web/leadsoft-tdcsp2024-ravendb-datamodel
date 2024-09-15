using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    public partial class Product
    {
        /// <summary>
        /// Construtor padrão que inicializa uma nova instância de <see cref="Product"/>.
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Define a categoria do produto, atribuindo o identificador e o nome da categoria.
        /// </summary>
        /// <param name="category">Objeto <see cref="Category"/> representando a categoria do produto.</param>
        /// <returns>Retorna a instância do produto com a categoria atribuída.</returns>
        public Product SetCategory(Category category)
        {
            CategoryId = category.Id;
            Category = category.Name;
            return this;
        }

        /// <summary>
        /// Converte implicitamente um objeto <see cref="Products.Product"/> para <see cref="Product"/>.
        /// </summary>
        /// <param name="product">Objeto <see cref="Products.Product"/> a ser convertido.</param>
        /// <returns>Uma nova instância de <see cref="Product"/> com os dados do produto fornecido.</returns>
        public static implicit operator Product(Products.Product product)
        {
            if (product.IsNull())
                return null;

            return new Product
            {
                Id = product.Id,
                Name = product.Name
            };
        }
    }


}
