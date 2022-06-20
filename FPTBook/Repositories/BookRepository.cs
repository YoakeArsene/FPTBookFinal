using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Repositories
{
    public class BookRepository : IBookRepository
    {
        private UserContext context;

        public BookRepository(UserContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Book;
        }

        public Book GetBookById(string id)
        {
            return context.Book.Include(b => b.Store).FirstOrDefault(m => m.Isbn == id);
        }

        public void CreateBook(Book book)
        {
            context.Add(book);
        }

        public void EditBook(Book book)
        {
            context.Update(book);
        }

        public void DeleteBook(Book book)
        {
            context.Book.Remove(book);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int GetNumberOfRecords()
        {
            return context.Book.Count();
        }
    }
}