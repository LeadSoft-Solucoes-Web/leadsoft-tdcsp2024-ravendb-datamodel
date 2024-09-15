using LeadSoft.Common.Library.Exceptions;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products
{
    public partial class Price
    {
        /// <summary>
        /// Construtor padrão que inicializa uma instância vazia de <see cref="Price"/>.
        /// </summary>
        public Price()
        {
        }

        /// <summary>
        /// Construtor que inicializa uma instância de <see cref="Price"/> com os valores fornecidos para título, valor, custo e datas.
        /// </summary>
        /// <param name="title">Título ou descrição do preço.</param>
        /// <param name="value">Valor do preço.</param>
        /// <param name="cost">Custo associado ao preço.</param>
        /// <param name="when">Data opcional em que o preço foi definido. Se não fornecido, usa a data atual.</param>
        /// <param name="until">Data opcional até quando o preço é válido.</param>
        public Price(string title, decimal value, decimal cost, DateTime? when = null, DateTime? until = null)
        {
            Title = title;
            Value = value;
            Cost = cost;
            When = when.HasValue ? when.Value : DateTime.Now;
            Until = until;
        }

        /// <summary>
        /// Define o valor e o custo do preço.
        /// </summary>
        /// <param name="value">Valor do preço.</param>
        /// <param name="cost">Custo associado ao preço. O valor padrão é zero.</param>
        /// <returns>Retorna a própria instância de <see cref="Price"/> para encadeamento de métodos.</returns>
        /// <exception cref="BadRequestAppException">Lançado se o custo for maior que o valor, ou se o custo for negativo.</exception>
        public Price SetPrice(decimal value, decimal cost = 0)
        {
            if (cost > value)
                throw new BadRequestAppException("Você não deve inserir um valor que vai causar prejuízo.");

            if (cost < 0)
                throw new BadRequestAppException("Valor de custo não deve ser negativo.");

            Value = value;
            Cost = cost;

            return this;
        }

        /// <summary>
        /// Define o título do preço.
        /// </summary>
        /// <param name="title">Título ou descrição do preço.</param>
        /// <returns>Retorna a própria instância de <see cref="Price"/> para encadeamento de métodos.</returns>
        public Price SetTitle(string title)
        {
            Title = title.Trim();
            return this;
        }
    }

}
