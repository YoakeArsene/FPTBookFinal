using BookStore.Data;
using BookStore.Models;

namespace FPTBook.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private UserContext context;
        public OrderRepository(UserContext context)
        {
            this.context = context;
        }
        public IEnumerable<Order> GetOrders()
        {
            return context.Order;
        }

        public Order GetOrderById(int id)
        {
            return context.Order.FirstOrDefault(o => o.Id == id);
        }

        public void CreateOrder(Order order)
        {
            context.Add(order);
        }

        public void EditOrder(Order order)
        {
            context.Update(order);
        }

        public void DeleteOrder(Order order)
        {
            context.Remove(order);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
