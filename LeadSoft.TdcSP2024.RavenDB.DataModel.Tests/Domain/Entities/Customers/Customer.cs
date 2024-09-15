namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    /// <summary>
    /// Representa um cliente com informações básicas, incluindo identificador, nome, data de nascimento, status de atividade, contatos de e-mail e endereços.
    /// </summary>
    public partial class Customer : Validation
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Data de nascimento do cliente.
        /// </summary>
        public DateTime Birthdate { get; private set; }

        /// <summary>
        /// Indica se o cliente está ativo ou inativo.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Lista de contatos de e-mail do cliente. A chave é o endereço de e-mail, e o valor é um objeto <see cref="EmailContact"/>.
        /// </summary>
        public virtual IDictionary<string, EmailContact> EmailContacts { get; private set; } = new Dictionary<string, EmailContact>();

        /// <summary>
        /// Lista de endereços do cliente.
        /// </summary>
        public virtual IList<Address> Addresses { get; private set; } = [];
    }

}
