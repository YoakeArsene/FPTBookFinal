using BookStore.Data;
using BookStore.Models;

namespace FPTBook.Repositories
{
    public class StoreRepository: IStoreRepository
    {
        private UserContext context;

        public StoreRepository(UserContext context)
        {
            this.context = context;
        }
        public IEnumerable<Store> GetStores()
        {
            return context.Store;
        }

        public Store GetStoreById(int id)
        {
            return context.Store.FirstOrDefault(s => s.Id == id);
        }

        public void CreateStore(Store store)
        {
            context.Add(store);
        }

        public void EditStore(Store store)
        {
            context.Update(store);
        }

        public void DeleteStore(Store store)
        {
            context.Remove(store);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
