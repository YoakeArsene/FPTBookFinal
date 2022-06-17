using BookStore.Models;

namespace FPTBook.Repositories
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetStores();
        Store GetStoreById(int id);
        void CreateStore(Store store);
        void EditStore(Store store);
        void DeleteStore(Store store);
        void Save();
    }
}
