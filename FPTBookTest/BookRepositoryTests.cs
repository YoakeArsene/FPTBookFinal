using BookStore.Data;
using BookStore.Models;
using FPTBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FPTBookTest
{
    public class BookRepositoryTests
    {
        private DbContextOptions<UserContext> dbContextOptions;

        public BookRepositoryTests()
        {
            var dbName = $"BookDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GetBooks_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var bookList = repository.GetBooks();

            //Assert
            Assert.Equal(2, bookList.Count());
        }

        [Fact]
        public async Task GetBookById_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var book = repository.GetBookById("abc");

            //Assert
            Assert.NotNull(book);
            Assert.Equal("Meh", book.Title);
            Assert.Equal(1, book.Price);
        }

        [Fact]
        public async Task CreateBook_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.CreateBook(new Book()
            {
                Author = "Lmao", 
                Category = "Lmao", 
                Desc = "Lmao", 
                Isbn = "def", 
                Pages = 3, 
                Price = 3,
                Title = "Lmao"
            });
            repository.Save();

            //Assert
            var bookList = repository.GetBooks();
            Assert.Equal(3, bookList.Count());
        }

        [Fact]
        public async Task EditBook_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            var book = repository.GetBookById("abc");
            book.Title = "Changed Title";
            repository.EditBook(book);

            //Assert
            var changedBook = repository.GetBookById("abc");
            Assert.Equal("Changed Title", changedBook.Title);
        }

        [Fact]
        public async Task DeleteBook_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            //Act
            repository.DeleteBook(repository.GetBookById("xyz"));
            repository.Save();

            //Assert
            var bookList = repository.GetBooks();
            Assert.Equal(1, bookList.Count());
        }

        private async Task<BookRepository> CreateRepositoryAsync()
        {
            UserContext context = new UserContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new BookRepository(context);
        }

        private async Task PopulateDataAsync(UserContext context)
        {
            var store = new Store()
                {
                    Name = "Store",
                    UId = "1",
                    Address = "Aincrad",
                    Slogan = "Meh",
                    Books = new List<Book>()
                    {
                        new Book()
                        {
                            Author = "Meh", 
                            Category = "Meh", 
                            Desc = "Meh", 
                            Isbn = "abc", 
                            Pages = 1, 
                            Price = 1,
                            Title = "Meh"
                        },
                        new Book()
                        {
                            Author = "Bruh", 
                            Category = "Bruh", 
                            Desc = "Bruh", 
                            Isbn = "xyz", 
                            Pages = 2, 
                            Price = 2,
                            Title = "Bruh"
                        },
                    }
                };
            await context.Store.AddAsync(store);
            await context.SaveChangesAsync();
        }


    }
}