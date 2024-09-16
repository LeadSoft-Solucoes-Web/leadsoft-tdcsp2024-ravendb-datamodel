using LeadSoft.Common.Library.Extensions;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders;
using LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories;

using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

using System.Reflection;

using static LeadSoft.Common.Library.Enumerators.Enums;

using static LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Orders.Enums;

using Product = LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Product;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests
{
    /// <summary>
    /// Classe responsável por configurar e gerenciar a conexão com o banco de dados RavenDB.
    /// Esta implementação será apresentada na TDC 2024 SP pela LeadSoft, destacando a eficiência do RavenDB.
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        /// <summary>
        /// Singleton para gerenciar a conexão com o RavenDB.
        /// </summary>
        public IDocumentStore Store { get; private set; }

        /// <summary>
        /// Construtor que inicializa o <see cref="IDocumentStore"/> com as configurações do RavenDB.
        /// Utiliza o certificado de segurança para autenticação e define a URL do banco de dados.
        /// </summary>
        public DatabaseFixture() => Store = new DocumentStore
        {
            Urls =
                [
                    "https://a.leadsoft.ravendb.community/" // URL do servidor RavenDB da LeadSoft
                ],
            Database = "LeadSoft", // Nome da base de dados
            Certificate = Assembly.GetExecutingAssembly()
                                  .GetEmbeddedResourceStream("LeadSoft_TDC_2024.pfx") // Certificado embutido no assembly
                                  .ReadCertificate("weloveravendb") // Senha do certificado
        }.Initialize();

        /// <summary>
        /// Método responsável por liberar os recursos alocados pela conexão com o banco de dados RavenDB.
        /// </summary>
        public void Dispose() => Store.Dispose();
    }

    /// <summary>
    /// Unidade de testes criada para demonstração durante a apresentação no TDC 2024 SP, junto ao RavenDB.
    /// O foco é apresentar a eficiência na modelagem e no desempenho com o uso do RavenDB.
    /// </summary>
    public class UnitTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture databaseFixture;

        /// <summary>
        /// Construtor que recebe o fixture do banco de dados para garantir o reuso da conexão entre os testes.
        /// </summary>
        /// <param name="fixture">Instância de <see cref="DatabaseFixture"/> que gerencia a conexão com o banco de dados.</param>
        public UnitTest(DatabaseFixture fixture)
        {
            databaseFixture = fixture;
        }

        /// <summary>
        /// Teste responsável por realizar operações em massa de inserção no RavenDB.
        /// Insere categorias, clientes e produtos em grandes quantidades de maneira eficiente.
        /// </summary>
        [Fact]
        public async Task BulkInsert()
        {
            BulkInsertOperation bulkInsert = null;
            try
            {
                bulkInsert = databaseFixture.Store.BulkInsert(); // Inicia a operação de inserção em massa

                foreach (Category category in Category.GetSamples())
                    if (category.IsValid(out _))
                        await bulkInsert.StoreAsync(category); // Insere as categorias no banco

                foreach (Customer customer in Customer.GetSamples())
                    if (customer.IsValid(out _))
                        await bulkInsert.StoreAsync(customer); // Insere os clientes no banco

                foreach (Product product in Product.GetSamples())
                    if (product.IsValid(out _))
                        await bulkInsert.StoreAsync(product); // Insere os produtos no banco
            }
            finally
            {
                if (bulkInsert != null)
                {
                    await bulkInsert.DisposeAsync(); // Finaliza e libera a operação de inserção em massa
                }
            }
        }

        /// <summary>
        /// Teste responsável por atualizar em massa os produtos no banco de dados, associando-os às suas respectivas categorias.
        /// </summary>
        [Fact]
        public async Task BulkUpdate()
        {
            BulkInsertOperation bulkInsert = null;
            try
            {
                using IAsyncDocumentSession readSession = databaseFixture.Store.OpenAsyncSession();

                IList<Category> categories = await readSession.Query<Category>().ToListAsync(); // Carrega as categorias do banco
                IList<Product> products = await readSession.Query<Product>().ToListAsync(); // Carrega os produtos do banco

                bulkInsert = databaseFixture.Store.BulkInsert();

                foreach (Product product in products)
                {
                    // Associa a categoria correta ao produto
                    Category category = categories.FirstOrDefault(c => c.Name.Equals(Category.GetNameByProduct(product.Name)));

                    if (category.IsNotNull())
                    {
                        product.SetCategory(category); // Atualiza a categoria do produto

                        if (product.IsValid(out _))
                            await bulkInsert.StoreAsync(product, product.Id); // Armazena o produto atualizado no banco
                    }
                }
            }
            finally
            {
                if (bulkInsert != null)
                {
                    await bulkInsert.DisposeAsync(); // Finaliza a operação de atualização em massa
                }
            }
        }

        /// <summary>
        /// Teste responsável por gerar pedidos em massa para diferentes clientes e produtos.
        /// </summary>
        [Fact]
        public async Task BulkOrders()
        {
            using IAsyncDocumentSession session = databaseFixture.Store.OpenAsyncSession();

            IList<Customer> customers = await session.Query<Customer>()
                                                     .Where(c => c.IsActive)
                                                     .ToListAsync(); // Carrega os clientes ativos
            IList<Product> products = await session.Query<Product>()
                                                   .ToListAsync(); // Carrega todos os produtos

            // Gera pedidos para diversos personagens, simulando ordens de compra de armas de Star Wars
            await GetOrder_DarthVader(session, customers, products);
            await GetOrder_LandoCalrissian(session, customers, products);
            await GetOrder_ObiWanKenobi(session, customers, products);
            await GetOrder_LukeSkywalker(session, customers, products);
            await GetOrder_LeiaOrgana(session, customers, products);
            await GetOrder_HanSolo(session, customers, products);
            await GetOrder_QuiGonJinn(session, customers, products);
            await GetOrder_MaceWindu(session, customers, products);
            await GetOrder_R2D2(session, customers, products);
            await GetOrder_C3PO(session, customers, products);
            await GetOrder_CondeDookan(session, customers, products);
            await GetOrder_PadmeAmidala(session, customers, products);
            await GetOrder_EmperorPalpatine(session, customers, products);
            await GetOrder_Yoda(session, customers, products);

            await session.SaveChangesAsync(); // Salva todas as alterações no banco de dados
        }

        #region [ Orders ]

        /// <summary>
        /// Gera um pedido para o personagem Darth Vader com base nos produtos selecionados.
        /// </summary>
        private async Task GetOrder_DarthVader(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Darth"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("Force Pike") ||
                                                            c.Name.Contains("Thermal Detonator")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_LandoCalrissian(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Lando Calrissian"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer)
                                     .SetDiscount(10);

            foreach (Product product in products.Where(c => c.Name.Contains("SE-14R light") ||
                                                            c.Name.Contains("DL-44 heavy") ||
                                                            c.Name.Contains("Merr-Sonn")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_ObiWanKenobi(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Obi-Wan"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("DC-17") ||
                                                            c.Name.Contains("Ion Cannon")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_HanSolo(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Han Solo"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Cheap, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("DL-44") ||
                                                            c.Name.Contains("Thermal Detonator") ||
                                                            c.Name.Contains("Merr-Sonn")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_LeiaOrgana(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Leia"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer)
                                     .SetDiscount(15);

            foreach (Product product in products.Where(c => c.Name.Contains("DH-17") ||
                                                            c.Name.Contains("Thermal Detonator") ||
                                                            c.Name.Contains("Sporting blaster")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_LukeSkywalker(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Luke"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("DL-44") ||
                                                            c.Name.Contains("Proton Torpedo")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_QuiGonJinn(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Qui-Gon Jinn"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("DC-15S blaster rifle")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_MaceWindu(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Mace"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("DC-17") ||
                                                            c.Name.Contains("Ion Cannon")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_R2D2(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("R2-D2"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Carbonite") ||
                                                            c.Name.Contains("Electric")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_C3PO(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("C-3PO"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Carbonite") ||
                                                            c.Name.Contains("Electric")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }


        private async Task GetOrder_CondeDookan(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Conde Dookan"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("Force Pike") ||
                                                            c.Name.Contains("Ion Cannon")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_EmperorPalpatine(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Imperador"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("Force Pike") ||
                                                            c.Name.Contains("Ion Cannon")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_PadmeAmidala(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Padmé"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Regular, customer.Addresses.First()))
                                     .SetConsumer(customer)
                                     .SetDiscount(5);

            foreach (Product product in products.Where(c => c.Name.Contains("Sporting blaster") ||
                                                            c.Name.Contains("ELG-3A") ||
                                                            c.Name.Contains("Thermal Detonator")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus()
                          .MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        private async Task GetOrder_Yoda(IAsyncDocumentSession session, IList<Customer> customers, IList<Product> products)
        {
            Customer customer = customers.FirstOrDefault(c => c.Name.Contains("Mestre Yoda"));

            if (customer.IsNull())
                return;

            Order order = new Order().SetShipping(new Shipping(OrderShippingType.Express, customer.Addresses.First()))
                                     .SetConsumer(customer);

            foreach (Product product in products.Where(c => c.Name.Contains("Lightsaber") ||
                                                            c.Name.Contains("Force Pike")))
            {
                order.Items.Add(new(product, new Random().Next(1, 5), product.GetPrice().Value));
            }

            order.Shipping.MoveToNextStatus();

            if (order.IsValid(out _))
                await session.StoreAsync(order);
        }

        #endregion

        /// <summary>
        /// Gera um relatório de pedidos baseado em critérios específicos, como desconto zero e envio em determinados estados.
        /// </summary>
        [Fact]
        public async Task GetOrderReportAsync()
        {
            using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();

            // Query para selecionar pedidos com critérios específicos
            var orders = await asyncSession.Query<Order>()
                                           .Include(o => o.Consumer.Id)
                                           .Include(o => o.Items.Select(i => i.Product.Id))
                                           .Include(o => o.Items.Select(i => i.Product.CategoryId))
                                           .Where(o => o.Shipping != null &&
                                                       o.Shipping.Type != OrderShippingType.PickUp &&
                                                       o.Shipping.Address != null &&
                                                       o.Shipping.Address.UF.In(UF.RJ, UF.MG, UF.SP, UF.ES) &&
                                                       o.Discount == 0)
                                           .OrderByDescending(o => o.When)
                                           .ToListAsync();

            // Seleciona os dados relevantes do relatório
            var select = orders.Select(o => new
            {
                o.Number,
                o.When,
                o.Items.Count,
                o.TotalItemsCurrency,
                o.DiscountPercent,
                o.TotalCurrency,
                o.Consumer.Name,
                o.Shipping?.Type,
                o.Shipping?.Status,
                o.Shipping?.Address?.UF
            });
        }
    }
}