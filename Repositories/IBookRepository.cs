using BookStore.Models;

namespace FPTBook.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(string id);
        void CreateBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        void Save();
        int GetNumberOfRecords();
    }
}