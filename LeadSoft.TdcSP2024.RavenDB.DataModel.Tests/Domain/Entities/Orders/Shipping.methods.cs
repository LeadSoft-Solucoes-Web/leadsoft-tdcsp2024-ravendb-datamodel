using LeadSoft.Common.Library.Exceptions;
using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers;

using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    public partial class Shipping
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Shipping"/>.
        /// </summary>
        public Shipping()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Shipping"/> com o tipo de envio e, opcionalmente, o endereço.
        /// </summary>
        /// <param name="type">Tipo de envio.</param>
        /// <param name="address">Endereço de entrega, se aplicável.</param>
        public Shipping(OrderShippingType type, Address address = null)
        {
            if (type != OrderShippingType.PickUp && address.IsNull())
                throw new BadRequestAppException("Endereço não pode ser nulo para delivery.");

            Type = type;
            Address = type != OrderShippingType.PickUp ? address : null;
        }

        /// <summary>
        /// Cancela o envio do pedido, alterando o status para "Cancelado".
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="Shipping"/> para encadeamento de métodos.</returns>
        public Shipping Cancel()
        {
            Status = OrderShippingStatus.Cancelled;
            return this;
        }

        /// <summary>
        /// Avança o status do envio para o próximo estágio, até que seja marcado como "Entregue".
        /// </summary>
        /// <returns>Retorna a própria instância de <see cref="Shipping"/> para encadeamento de métodos.</returns>
        public Shipping MoveToNextStatus()
        {
            if (Type == OrderShippingType.PickUp)
                Status = OrderShippingStatus.Delivered;
            else if (Status != OrderShippingStatus.Delivered && Status != OrderShippingStatus.Cancelled)
                Status++;

            return this;
        }
    }

}
