using LeadSoft.Common.Library.Exceptions;
using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders
{
    public partial class Order
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Order"/>.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Define o desconto do pedido, garantindo que o valor esteja entre 0% e 80%.
        /// </summary>
        /// <param name="discount">Desconto a ser aplicado (em porcentagem).</param>
        /// <returns>Retorna a própria instância do pedido com o desconto aplicado.</returns>
        public Order SetDiscount(decimal discount)
        {
            if (discount < 0)
                Discount = 0;
            else
                Discount = discount > 80 ? 0.80m : discount / 100;

            return this;
        }

        /// <summary>
        /// Define o consumidor (cliente) associado ao pedido.
        /// </summary>
        /// <param name="customer">Objeto <see cref="Customer"/> que representa o consumidor.</param>
        /// <returns>Retorna a própria instância do pedido com o cliente associado.</returns>
        public Order SetConsumer(Customer customer)
        {
            Consumer = customer;
            return this;
        }

        /// <summary>
        /// Define as informações de envio para o pedido.
        /// </summary>
        /// <param name="shipping">Objeto <see cref="Shipping"/> contendo as informações de envio.</param>
        /// <returns>Retorna a instância do pedido com a entrega definida.</returns>
        public Order SetShipping(Shipping shipping)
        {
            Shipping = shipping;
            return this;
        }

        /// <summary>
        /// Fecha o pedido, verificando se há cliente, itens e informações de entrega.
        /// </summary>
        /// <returns>Retorna a própria instância do pedido após o fechamento.</returns>
        /// <exception cref="BadRequestAppException">Lançado se o pedido já estiver fechado ou se faltar cliente, itens ou informações de entrega.</exception>
        public Order Close()
        {
            if (IsClosed())
                throw new BadRequestAppException("Não é possível fechar um pedido já fechado anteriormente.");

            if (Consumer.IsNull())
                throw new BadRequestAppException("Não é possível fechar um pedido sem um cliente.");

            if (!Items.Any())
                throw new BadRequestAppException("Não é possível fechar um pedido sem itens.");

            if (Shipping.IsNull())
                throw new BadRequestAppException("Não é possível fechar um pedido sem um tipo de entrega.");

            ClosedAt = DateTime.UtcNow;

            return this;
        }
    }
}
