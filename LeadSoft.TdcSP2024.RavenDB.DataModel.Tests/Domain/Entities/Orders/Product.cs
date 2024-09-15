namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Representa um produto dentro de um pedido, incluindo o identificador, nome e a categoria associada.
    /// </summary>
    public partial class Product : Validation
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Identificador da categoria à qual o produto pertence.
        /// </summary>
        public string CategoryId { get; private set; }

        /// <summary>
        /// Nome da categoria do produto.
        /// </summary>
        public string Category { get; private set; }
    }


}
