using System.ComponentModel;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    /// <summary>
    /// Contém enumeradores relacionados ao pedido, como tipo e status do envio.
    /// </summary>
    public static partial class Enums
    {
        /// <summary>
        /// Tipos de envio de pedido.
        /// </summary>
        public enum OrderShippingType
        {
            [Description("Retirada")]
            PickUp,
            [Description("Econômico")]
            Cheap,
            [Description("Normal")]
            Regular,
            [Description("Expresso")]
            Express
        }

        /// <summary>
        /// Status do envio de pedido.
        /// </summary>
        public enum OrderShippingStatus
        {
            [Description("Embalando")]
            Packing,
            [Description("Em trânsito")]
            InTransit,
            [Description("Entregue")]
            Delivered,
            [Description("Cancelado")]
            Cancelled
        }
    }

}
