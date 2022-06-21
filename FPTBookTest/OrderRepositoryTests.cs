using BookStore.Areas.Identity.Data;
using BookStore.Data;
using BookStore.Models;
using FPTBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FPTBookTest
{
    public class OrderRepositoryTests
    {
        private DbContextOptions<UserContext> dbContextOptions;

        public OrderRepositoryTests()
        {
            var dbName = $"OrderDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GetOrders_Success_Test()
        {
            var repository = await CreateRepositoryAsync();
            //Act
            var orderList = repository.GetOrders();

            //Assert
            Assert.Equal(2, orderList.Count());
        }

        [Fact]
        public async Task GetOrderById_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var order = repository.GetOrderById(1);

            //Assert
            Assert.NotNull(order);
            Assert.Equal(1, order.Total);
        }

        [Fact]
        public async Task CreateOrder_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.CreateOrder(new Order()
            {
                Id = 3,
                OrderDate = DateTime.Now,
                Total = 3,
                UId = "meh"
            });
            repository.Save();

            //Assert
            var orderList = repository.GetOrders();
            Assert.Equal(3, orderList.Count());
        }

        [Fact]
        public async Task EditOrder_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var order = repository.GetOrderById(1);
            order.Total = 727;
            repository.EditOrder(order);

            //Assert
            var changedOrder = repository.GetOrderById(1);
            Assert.Equal(727, changedOrder.Total);
        }

        [Fact]
        public async Task DeleteOrder_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.DeleteOrder(repository.GetOrderById(2));
            repository.Save();

            //Assert
            var orderList = repository.GetOrders();
            Assert.Equal(1, orderList.Count());
        }

        private async Task<OrderRepository> CreateRepositoryAsync()
        {
            UserContext context = new UserContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new OrderRepository(context);
        }

        private async Task PopulateDataAsync(UserContext context)
        {
            var user = new AppUser()
            {
                Address = "Aincrad",
                Id = "1",
                UserName = "needyoverdose@gmail.com",
                NormalizedUserName = "NEEDYOVERDOSE@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEFzbd8Xj6i64bmTpjX7WEbRD9o1c780rNjEqRSvnr0tU3OcqV8u0K80H9aq42J/JtQ==",
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        Id = 1,
                        OrderDate = DateTime.Now,
                        Total = 1,
                    },
                    new Order()
                    {
                        Id = 2,
                        OrderDate = DateTime.Now,
                        Total = 2,
                    },
                }
            };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
        }
    }
}