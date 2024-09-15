using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    public partial class Consumer
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Consumer"/>.
        /// </summary>
        public Consumer()
        {
        }

        /// <summary>
        /// Conversão implícita de um objeto <see cref="Customer"/> para <see cref="Consumer"/>.
        /// </summary>
        /// <param name="customer">Objeto <see cref="Customer"/> a ser convertido.</param>
        /// <returns>
        /// Uma nova instância de <see cref="Consumer"/> com os dados do cliente, ou <see langword="null"/> se o cliente for nulo.
        /// </returns>
        public static implicit operator Consumer(Customer customer)
        {
            if (customer.IsNull())
                return null;

            return new Consumer
            {
                Id = customer.Id,
                Name = customer.Name,
                EmailAddress = customer.GetPrimaryEmail().Key
            };
        }
    }


}
