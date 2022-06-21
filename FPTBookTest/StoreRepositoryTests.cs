using BookStore.Areas.Identity.Data;
using BookStore.Data;
using BookStore.Models;
using FPTBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FPTBookTest
{
    public class StoreRepositoryTests
    {
        private DbContextOptions<UserContext> dbContextOptions;

        public StoreRepositoryTests()
        {
            var dbName = $"StoreDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GetStores_Success_Test()
        {
            var repository = await CreateRepositoryAsync();
            //Act
            var storeList = repository.GetStores();

            //Assert
            Assert.Equal(2, storeList.Count());
        }

        [Fact]
        public async Task GetStoreById_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var store = repository.GetStoreById(1);

            //Assert
            Assert.NotNull(store);
            Assert.Equal("Aincrad", store.Address);
        }

        [Fact]
        public async Task CreateStore_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.CreateStore(new Store()
            {
                Id = 3,
                Name = "Store3",
                UId = "3",
                Address = "Underworld",
                Slogan = "Lmao"
            });
            repository.Save();

            //Assert
            var storeList = repository.GetStores();
            Assert.Equal(3, storeList.Count());
        }

        [Fact]
        public async Task EditOrder_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var store = repository.GetStoreById(1);
            store.Slogan = "Japanese Goblin";
            repository.EditStore(store);

            //Assert
            var changedStore = repository.GetStoreById(1);
            Assert.Equal("Japanese Goblin", changedStore.Slogan);
        }

        [Fact]
        public async Task DeleteOrder_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.DeleteStore(repository.GetStoreById(2));
            repository.Save();

            //Assert
            var storeList = repository.GetStores();
            Assert.Equal(1, storeList.Count());
        }

        private async Task<StoreRepository> CreateRepositoryAsync()
        {
            UserContext context = new UserContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new StoreRepository(context);
        }

        private async Task PopulateDataAsync(UserContext context)
        {
            var store1 = new Store()
            {
                Id = 1,
                Name = "Store1",
                UId = "1",
                Address = "Aincrad",
                Slogan = "Meh",
            };
            var store2 = new Store()
            {
                Id = 2,
                Name = "Store2",
                UId = "2",
                Address = "Alfheim",
                Slogan = "Bruh",
            };
            await context.Store.AddAsync(store1);
            await context.Store.AddAsync(store2);
            await context.SaveChangesAsync();
        }
    }
}