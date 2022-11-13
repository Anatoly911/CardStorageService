using Unit_of_Work;
using Unity;
using UsingUnitOfWork.Dal;
using UsingUnitOfWork.Dal.Business.UnitOfWorks.Interfaces;
using UsingUnitOfWork.Dal.Business.UnitOfWorks.Services;

namespace UsingUnitOfWork.Sample
{
    internal class Program
    {
        private static void EntityFrameworkUnitOfWork()
        {
            using (var context = new ShopContext())
            {
                var customer = new Customer();
                customer.FirstName = "Aleks";
                customer.Age = 27;
                context.Customers.Add(customer);
                var prod1 = new Product
                {
                    Description = "Big soap",
                    Name = "soap",
                    Price = 24
                };
                var prod2 = new Product
                {
                    Description = "butter",
                    Name = "butter",
                    Price = 3.5
                };
                context.Products.Add(prod1);
                context.Products.Add(prod2);
                var saleItem1 = new SaleItem
                {
                    Comment = "It's gift",
                    Customer = customer,
                    Product = prod1
                };
                context.SaleItems.Add(saleItem1);
                var sale = new Sale
                {
                    Customer = customer,
                    Discount = 0.2,
                    SaleItems = new List<SaleItem>(new[] { saleItem1 }),
                    TotalCost = 10
                };
                context.Sales.Add(sale);
                context.SaveChanges();
            }
        }
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>()
                        .RegisterType<ITransactionUnitOfWork, TransactionUnitOfWork>();
            var productName = Guid.NewGuid().ToString();
            var customerName = Guid.NewGuid().ToString();
            try
            {
                using (var writeUnitOfWork = container.Resolve<ITransactionUnitOfWork>())
                {
                    var newProduct = writeUnitOfWork.CreateNew<Product>();
                    newProduct.Name = productName;
                    newProduct.Price = 35;
                    newProduct.Description = new string('*', 5000);
                    var customer = writeUnitOfWork.CreateNew<Customer>();
                    customer.FirstName = customerName;
                    customer.Age = 27;
                    writeUnitOfWork.Add(newProduct);
                    writeUnitOfWork.Add(customer);
                    writeUnitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : {0}", ex.Message);
            }
            using (var unitOfWork = container.Resolve<IUnitOfWork>())
            {
                var product =
                unitOfWork.GetProductRepository().GetAll().FirstOrDefault(x => x.Name ==
                productName);
                if (product == null)
                {
                    Console.WriteLine("Product not found!!!");
                }
                else
                {
                    Console.WriteLine("Product Id: {0}, Name: {1}", product.Id,
                    product.Name);
                }
                var customer = unitOfWork.GetCustomerRepository().GetAll().FirstOrDefault(x
                => x.FirstName == customerName);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found!!!");
                }
                else
                {
                    Console.WriteLine("Customer Id: {0}, Name: {1}", customer.Id, customer.FirstName);
                }
            }
        }
    }
}