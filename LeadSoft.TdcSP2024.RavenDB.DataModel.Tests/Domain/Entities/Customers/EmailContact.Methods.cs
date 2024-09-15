using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    public partial class EmailContact
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="EmailContact"/>.
        /// </summary>
        public EmailContact()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="EmailContact"/> com valores para o status de e-mail principal e o tipo de e-mail.
        /// </summary>
        /// <param name="isPrimary">Indica se este é o e-mail principal do cliente.</param>
        /// <param name="type">Tipo do e-mail (opcional). Se não fornecido, o tipo será definido como <see cref="EmailType.Personal"/>.</param>
        public EmailContact(bool isPrimary, EmailType? type)
        {
            IsPrimary = isPrimary;
            if (type.HasValue)
                Type = type.Value;
        }

        /// <summary>
        /// Define o e-mail como o principal.
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="EmailContact"/> para encadeamento de métodos.</returns>
        public EmailContact SetPrimary()
        {
            IsPrimary = true;
            return this;
        }

        /// <summary>
        /// Remove o status de e-mail principal, tornando-o secundário.
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="EmailContact"/> para encadeamento de métodos.</returns>
        public EmailContact UnsetPrimary()
        {
            IsPrimary = false;
            return this;
        }
    }

}
