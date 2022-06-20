using BookStore.Models;

namespace FPTBook.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order order);
        void EditOrder(Order order);
        void DeleteOrder(Order order);
        void Save();
    }
}
