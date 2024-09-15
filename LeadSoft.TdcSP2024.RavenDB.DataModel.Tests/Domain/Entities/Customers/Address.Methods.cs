namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    public partial class Address
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Address"/>.
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Define o título do endereço.
        /// </summary>
        /// <param name="title">Título descritivo do endereço (exemplo: "Residencial" ou "Comercial").</param>
        /// <returns>Retorna a própria instância de <see cref="Address"/> para encadeamento de métodos.</returns>
        public Address SetTitle(string title)
        {
            Title = title.Trim();
            return this;
        }
    }

}
