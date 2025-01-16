using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ProiectMedii.Models;

namespace ProiectMedii.Data
{
    public class ServiceListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ServiceListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ServiceList>().Wait();
            _database.CreateTableAsync<Service>().Wait();
            _database.CreateTableAsync<ListService>().Wait();
            _database.CreateTableAsync<Shop>().Wait();
        }
        public Task<int> SaveServiceAsync(Service service)
        {
            if (service.ID != 0)
            {
                return _database.UpdateAsync(service);
            }
            else
            {
                return _database.InsertAsync(service);
            }
        }
        public Task<int> DeleteServiceAsync(Service service)
        {
            return _database.DeleteAsync(service);
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return _database.Table<Service>().ToListAsync();
        }

        public Task<List<ServiceList>> GetServiceListsAsync()
        {
            return _database.Table<ServiceList>().ToListAsync();
        }

        public Task<ServiceList> GetServiceListAsync(int id)
        {
            return _database.Table<ServiceList>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceListAsync(ServiceList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }

        public Task<int> DeleteServiceListAsync(ServiceList slist)
        {
            return _database.DeleteAsync(slist);
        }

        public Task<int> SaveListServiceAsync(ListService listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }

        public Task<List<Service>> GetListServicesAsync(int servicelistid)
        {
            return _database.QueryAsync<Service>(
         "select S.ID, S.Description from Service S"
         + " inner join ListService LS"
         + " on S.ID = LS.ServiceID where LS.ServiceListID = ?",
         servicelistid);

        }

        public Task<List<Shop>> GetShopsAsync()
        {
            return _database.Table<Shop>().ToListAsync();
        }

        public Task<int> SaveShopAsync(Shop shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }
    }
}
