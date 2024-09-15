using LeadSoft.Common.Library.Exceptions;
using LeadSoft.Common.Library.Extensions;

using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    public partial class Customer
    {
        #region [ Constructors ]

        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Customer"/>.
        /// </summary>
        public Customer()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Customer"/> com nome e data de nascimento.
        /// </summary>
        /// <param name="name">Nome do cliente.</param>
        /// <param name="birthdate">Data de nascimento do cliente.</param>
        public Customer(string name, DateTime birthdate)
        {
            SetName(name).SetBirthdate(birthdate);
            Activate();
        }

        #endregion

        /// <summary>
        /// Define o nome do cliente.
        /// </summary>
        /// <param name="name">Nome do cliente.</param>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer SetName(string name)
        {
            Name = name.Trim();
            return this;
        }

        /// <summary>
        /// Define a data de nascimento do cliente.
        /// </summary>
        /// <param name="birthdate">Data de nascimento do cliente.</param>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        /// <exception cref="BadRequestAppException">Lançado se a data de nascimento for maior que a data atual.</exception>
        public Customer SetBirthdate(DateTime birthdate)
        {
            if (birthdate.Date > DateTime.Now)
                throw new BadRequestAppException("Data de nascimento não pode ser maior que a data atual.");

            Birthdate = birthdate.Date;
            return this;
        }

        /// <summary>
        /// Desativa o cliente, alterando seu status para inativo.
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer Deactivate()
        {
            IsActive = false;
            return this;
        }

        /// <summary>
        /// Ativa o cliente, alterando seu status para ativo.
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer Activate()
        {
            IsActive = true;
            return this;
        }

        /// <summary>
        /// Obtém o contato de e-mail principal do cliente.
        /// </summary>
        /// <returns>Retorna um par chave/valor representando o endereço de e-mail principal e seu contato associado.</returns>
        public KeyValuePair<string, EmailContact> GetPrimaryEmail()
        {
            var email = EmailContacts.FirstOrDefault(ec => ec.Value.IsPrimary == true);

            return email.IsNotNull() ? email : EmailContacts.FirstOrDefault();
        }

        /// <summary>
        /// Adiciona um novo e-mail ao cliente.
        /// </summary>
        /// <param name="address">Endereço de e-mail a ser adicionado.</param>
        /// <param name="isPrimary">Indica se o e-mail é o principal.</param>
        /// <param name="type">Tipo de e-mail (opcional).</param>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer AddEmail(string address, bool isPrimary = true, EmailType? type = null)
        {
            address = address.Trim().ToLower();

            if (EmailContacts.Any(ec => ec.Key.Equals(address)))
            {
                if (EmailContacts.TryGetValue(address, out EmailContact contact)) ;
                contact = new(isPrimary, type);
            }
            else
            {
                EmailContacts.Add(address, new(isPrimary, type));
            }

            if (isPrimary)
            {
                var emailContact = EmailContacts.FirstOrDefault(ec => !ec.Key.Equals(address) && ec.Value.IsPrimary);
                if (emailContact.Key.IsNotNull())
                    emailContact.Value.UnsetPrimary();
            };

            return this;
        }

        /// <summary>
        /// Remove um endereço de e-mail da lista de contatos do cliente.
        /// </summary>
        /// <param name="address">Endereço de e-mail a ser removido.</param>
        /// <returns>Retorna um valor booleano indicando se o e-mail foi removido com sucesso.</returns>
        public bool RemoveEmail(string address)
        {
            bool result = EmailContacts.Remove(address.Trim().ToLower());

            var emailContact = EmailContacts.FirstOrDefault();
            if (emailContact.IsNotNull())
                emailContact.Value.SetPrimary();

            return result;
        }

        /// <summary>
        /// Adiciona um novo endereço ao cliente.
        /// </summary>
        /// <param name="title">Título do endereço (exemplo: "Residencial" ou "Comercial").</param>
        /// <param name="address">Objeto <see cref="Address"/> contendo as informações do endereço.</param>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer AddAddress(string title, Address address)
        {
            title = title.Trim();

            if (!Addresses.Any(a => a.Title.Equals(title)))
                Addresses.Add(address.SetTitle(title));

            return this;
        }

        /// <summary>
        /// Remove um endereço da lista de endereços do cliente.
        /// </summary>
        /// <param name="title">Título do endereço a ser removido.</param>
        /// <returns>Retorna a própria instância de <see cref="Customer"/> para encadeamento de métodos.</returns>
        public Customer RemoveAddress(string title)
        {
            title = title.Trim();
            Address address = Addresses.FirstOrDefault(a => a.Title.Equals(title));

            if (address.IsNotNull())
                Addresses.Remove(address);

            return this;
        }

        public bool IsBirthday() => Birthdate.Date.Month == DateTime.Now.Date.Month &&
                                    Birthdate.Date.Day == DateTime.Now.Date.Day;
        public bool IsBirthdayWeek() => Birthdate.Date.Month == DateTime.Now.Date.Month &&
                                        (Birthdate.Date.Day - DateTime.Now.Date.Day) <= 7;
    }

}
