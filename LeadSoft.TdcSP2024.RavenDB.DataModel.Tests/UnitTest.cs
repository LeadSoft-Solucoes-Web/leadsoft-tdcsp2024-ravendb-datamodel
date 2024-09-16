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
    public class DatabaseFixture : IDisposable
    {
        // Singleton
        public IDocumentStore Store { get; private set; }

        public DatabaseFixture() => Store = new DocumentStore
        {
            Urls =
                [
                    "https://a.leadsoft.ravendb.community/"
                ],
            Database = "LeadSoft",
            Certificate = Assembly.GetExecutingAssembly()
                                               .GetEmbeddedResourceStream("LeadSoft_TDC_2024.pfx")
                                               .ReadCertificate("weloveravendb")
        }.Initialize();

        public void Dispose() => Store.Dispose();
    }

    public class UnitTest : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture databaseFixture;

        public UnitTest(DatabaseFixture fixture)
        {
            databaseFixture = fixture;
        }

        //[Fact]
        //public async Task Storing()
        //{
        //    using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();
        //    Category category = new Category
        //    {
        //        Name = "Database Category",
        //    };

        //    await asyncSession.StoreAsync(category);                        // Assign an 'Id' and collection (Categories)
        //                                                                    // and start tracking an entity

        //    Product product = new Product
        //    {
        //        Name = "RavenDB database",
        //        Category = category.Id,
        //        UnitsInStock = 10
        //    };

        //    await asyncSession.StoreAsync(product);                         // Assign an 'Id' and collection (Products)
        //                                                                    // and start tracking an entity


        //    await asyncSession.SaveChangesAsync();                          // Send to the Server
        //                                                                    // one request processed in one transaction
        //}

        [Fact]
        public async Task BulkInsert()
        {
            BulkInsertOperation bulkInsert = null;
            try
            {
                bulkInsert = databaseFixture.Store.BulkInsert();

                foreach (Category category in Category.GetSamples())
                    if (category.IsValid(out _))
                        await bulkInsert.StoreAsync(category);

                foreach (Customer customer in Customer.GetSamples())
                    if (customer.IsValid(out _))
                        await bulkInsert.StoreAsync(customer);

                foreach (Product product in Product.GetSamples())
                    if (product.IsValid(out _))
                        await bulkInsert.StoreAsync(product);
            }
            finally
            {
                if (bulkInsert != null)
                {
                    await bulkInsert.DisposeAsync();
                }
            }
        }

        [Fact]
        public async Task BulkUpdate()
        {
            BulkInsertOperation bulkInsert = null;
            try
            {
                using IAsyncDocumentSession readSession = databaseFixture.Store.OpenAsyncSession();

                IList<Category> categories = await readSession.Query<Category>().ToListAsync();
                IList<Product> products = await readSession.Query<Product>().ToListAsync();

                bulkInsert = databaseFixture.Store.BulkInsert();

                foreach (Product product in products)
                {
                    Category category = categories.FirstOrDefault(c => c.Name.Equals(Category.GetNameByProduct(product.Name)));

                    if (category.IsNotNull())
                    {
                        product.SetCategory(category);

                        if (product.IsValid(out _))
                            await bulkInsert.StoreAsync(product, product.Id);
                    }
                }
            }
            finally
            {
                if (bulkInsert != null)
                {
                    await bulkInsert.DisposeAsync();
                }
            }
        }

        [Fact]
        public async Task BulkOrders()
        {
            using IAsyncDocumentSession session = databaseFixture.Store.OpenAsyncSession();

            IList<Customer> customers = await session.Query<Customer>()
                                                     .Where(c => c.IsActive)
                                                     .ToListAsync();
            IList<Product> products = await session.Query<Product>()
                                                   .ToListAsync();


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

            await session.SaveChangesAsync();
        }

        #region [ Orders ]

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

        [Fact]
        public async Task GetOrderReportAsync()
        {
            using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();

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

        //[Fact]
        //public async Task ApplyChanges()
        //{
        //    using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();
        //    string productId = "products/1-A";

        //    Product product = await asyncSession
        //        .Include<Product>(x => x.Category)                          // Include Category
        //        .LoadAsync(productId);                                      // Load the Product and start tracking

        //    Category category = await asyncSession
        //        .LoadAsync<Category>(product.Category);                     // No remote calls,
        //                                                                    // Session contains this entity from .Include

        //    product.Name = "RavenDB";                                       // Apply changes
        //    category.Name = "Database";

        //    await asyncSession.SaveChangesAsync();                          // Synchronize with the Server
        //                                                                    // one request processed in one transaction

        //    Assert.Equal("RavenDB", product.Name);
        //    Assert.Equal("Database", category.Name);
        //}

        //[Fact]
        //public async Task QueryingAndPaging()
        //{
        //    using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();
        //    List<string> productNames = await asyncSession                  // Query for Products
        //        .Query<Product>()
        //        .Statistics(out QueryStatistics stats)
        //        .Where(x => x.UnitsInStock > 5 && x.UnitsInStock < 11)      // Filter
        //        .Skip(0).Take(10)                                           // Page
        //        .Select(x => x.Name)                                        // Project
        //        .ToListAsync();                                             // Materialize query

        //    Assert.True("RavenDB".In(productNames));
        //    Assert.True(stats.TotalResults == 10050);                       // Will hold the total number of matching documents (without paging)
        //}

        //[Fact]
        //public async Task StoringAttachment()
        //{
        //    using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();
        //    using var file1 = File.Open("rook-suit.png", FileMode.Open);
        //    Product product = await asyncSession
        //        .Include<Product>(x => x.Category)
        //        .LoadAsync("products/1-A");

        //    asyncSession.Advanced.Attachments.Store(product.Id, "rook-suit", file1, "image/png");

        //    await asyncSession.SaveChangesAsync();
        //}

        //[Fact]
        //public async Task FullTextSearch()
        //{
        //    using IAsyncDocumentSession asyncSession = databaseFixture.Store.OpenAsyncSession();
        //    // Single or multiples terms
        //    List<Product> products = await asyncSession
        //        .Query<Product>()
        //        .Search(x => x.Name, "#999996 #999995", @operator: SearchOperator.Or)          // OR new[] { "#999996 #999995" }
        //        .Search(x => x.Category, "categories/47-A")                                    // Second Field
        //                                                                                       //.Search(x => x.Infos, "USA Brasil")                                          //   the term 'USA' OR 'London' in any field within the complex 'Address' object                                                                               // Search in all complex object fields
        //        .ToListAsync();

        //    Assert.True(products.Any(p => p.Name.Contains("#999996")));
        //    Assert.True(products.Any(p => p.Name.Contains("#999995")));
        //    Assert.True(products.Any(p => p.Category.Equals("categories/47-A")));
        //}
    }
}