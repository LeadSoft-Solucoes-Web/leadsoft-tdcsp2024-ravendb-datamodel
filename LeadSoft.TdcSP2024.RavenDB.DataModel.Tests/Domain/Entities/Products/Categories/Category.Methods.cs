namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories
{
    public partial class Category
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Category"/>.
        /// </summary>
        public Category()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Category"/> com o nome fornecido.
        /// </summary>
        /// <param name="name">Nome da categoria de produto.</param>
        public Category(string name)
        {
            SetName(name);
        }

        /// <summary>
        /// Define o nome da categoria de produto.
        /// </summary>
        /// <param name="name">Nome da categoria de produto.</param>
        /// <returns>Retorna a própria instância de <see cref="Category"/> para encadeamento de métodos.</returns>
        public Category SetName(string name)
        {
            Name = name.Trim();
            return this;
        }
    }

}
