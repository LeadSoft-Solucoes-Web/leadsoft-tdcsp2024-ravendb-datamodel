namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products
{
    /// <summary>
    /// Representa um produto, incluindo identificador, nome, categoria e uma lista de preços associados.
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
        /// Identificador da categoria associada ao produto.
        /// </summary>
        public string CategoryId { get; private set; }

        /// <summary>
        /// Lista de preços associados ao produto, onde a chave é o identificador único e o valor é o objeto <see cref="Price"/>.
        /// </summary>
        public IDictionary<Guid, Price> Prices { get; private set; } = new Dictionary<Guid, Price>();
    }

}
