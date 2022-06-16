using BookStore.Models;

namespace FPTBook.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCartById(string id, string isbn);
        void EditCart(Cart cart);
        void DeleteCart(Cart cart);
        void Save();
    }
}
