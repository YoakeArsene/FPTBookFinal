using BookStore.Areas.Identity.Data;
using BookStore.Data;
using BookStore.Models;
using FPTBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FPTBookTest
{
    public class CartRepositoryTests
    {
        private DbContextOptions<UserContext> dbContextOptions;

        public CartRepositoryTests()
        {
            var dbName = $"BookDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GetCarts_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var cartList = repository.GetCarts();

            //Assert
            Assert.Equal(1, cartList.Count());
        }

        [Fact]
        public async Task GetCartById_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var cart = repository.GetCartById("1");

            //Assert
            Assert.NotNull(cart);
            Assert.Equal("Meh", cart.Book.Title);
            Assert.Equal(1, cart.Quantity);
        }

        [Fact]
        public async Task EditCart_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var cart = repository.GetCartById("1");
            cart.Quantity = 2;
            repository.EditCart(cart);

            //Assert
            var changedCart = repository.GetCartById("1");
            Assert.Equal(2, changedCart.Quantity);
        }

        [Fact]
        public async Task DeleteCart_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.DeleteCart(repository.GetCartById("1"));

            //Assert
            var cartList = repository.GetCarts();
            Assert.Equal(0, cartList.Count());
        }

        private async Task<CartRepository> CreateRepositoryAsync()
        {
            UserContext context = new UserContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new CartRepository(context);
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
                Carts = new List<Cart>()
                {
                    new Cart()
                    {
                        Book = new Book()
                        {
                            Author = "Meh",
                            Category = "Meh",
                            Desc = "Meh",
                            Isbn = "abc",
                            Pages = 1,
                            Price = 1,
                            Title = "Meh"
                        },
                        BookIsbn = "abc",
                        Quantity = 1,
                    }
                }
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }


    }
}