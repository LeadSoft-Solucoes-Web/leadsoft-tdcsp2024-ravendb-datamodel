using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers;

using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Representa os detalhes de envio de um pedido, incluindo tipo de envio, status e endereço.
    /// </summary>
    public partial class Shipping : Validation
    {
        /// <summary>
        /// Tipo de envio associado ao pedido.
        /// </summary>
        public OrderShippingType Type { get; private set; } = OrderShippingType.PickUp;

        /// <summary>
        /// Status atual do envio.
        /// </summary>
        public OrderShippingStatus Status { get; private set; } = OrderShippingStatus.Packing;

        /// <summary>
        /// Endereço de entrega do pedido, se aplicável.
        /// </summary>
        public Address Address { get; private set; }
    }

}
