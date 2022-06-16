using BookStore.Data;
using BookStore.Models;

namespace FPTBook.Repositories
{
    public class CartRepository: ICartRepository
    {
        private UserContext context;
        public CartRepository(UserContext context)
        {
            this.context = context;
        }
        public IEnumerable<Cart> GetCarts()
        {
            return context.Cart;
        }
        public Cart GetCartById(string id, string isbn)
        {
            return context.Cart.FirstOrDefault(c => c.UId == id && c.BookIsbn == isbn);
        }
        public void EditCart(Cart cart)
        {
            context.Update(cart);
        }
        public void DeleteCart(Cart cart)
        {
            context.Remove(cart);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
