using LeadSoft.Common.Library.Exceptions;
using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products
{
    public partial class Product
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Product"/>.
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Product"/> com o nome fornecido.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        public Product(string name)
        {
            SetName(name);
        }

        /// <summary>
        /// Define o nome do produto.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        /// <returns>Retorna a própria instância de <see cref="Product"/> para encadeamento de métodos.</returns>
        public Product SetName(string name)
        {
            Name = name.Trim();
            return this;
        }

        /// <summary>
        /// Define a categoria do produto.
        /// </summary>
        /// <param name="category">Objeto <see cref="Category"/> representando a categoria do produto.</param>
        /// <returns>Retorna a própria instância de <see cref="Product"/> para encadeamento de métodos.</returns>
        /// <exception cref="BadRequestAppException">Lançado se o identificador da categoria for nulo ou inválido.</exception>
        public Product SetCategory(Category category)
        {
            if (category.IsNull() || category.Id.IsNothing())
                throw new BadRequestAppException("Id da categoria não pode ser nulo.");

            CategoryId = category.Id;
            return this;
        }

        /// <summary>
        /// Obtém o preço mais recente do produto, considerando a data de validade.
        /// </summary>
        /// <returns>Retorna o preço atual do produto.</returns>
        /// <exception cref="NotFoundAppException">Lançado se nenhum preço estiver disponível.</exception>
        public Price GetPrice()
        {
            var price = Prices.OrderByDescending(p => p.Value.When)
                              .FirstOrDefault(p => p.Value.Until == null || p.Value.Until.Value <= DateTime.Now);

            if (price.IsNull())
                throw new NotFoundAppException("Nenhum preço disponível.");

            return price.Value;
        }

        /// <summary>
        /// Obtém todos os preços do produto, ordenados pela data de definição do preço (do mais recente ao mais antigo).
        /// </summary>
        /// <returns>Retorna uma lista ordenada de preços associados ao produto.</returns>
        public IOrderedEnumerable<KeyValuePair<Guid, Price>> GetPrices()
            => Prices.OrderByDescending(p => p.Value.When);

        /// <summary>
        /// Adiciona um novo preço ao produto.
        /// </summary>
        /// <param name="price">Objeto <see cref="Price"/> representando o preço a ser adicionado.</param>
        /// <returns>Retorna a própria instância de <see cref="Product"/> para encadeamento de métodos.</returns>
        public Product AddPrice(Price price)
        {
            Prices.Add(Guid.NewGuid(), price);

            return this;
        }
    }

}
