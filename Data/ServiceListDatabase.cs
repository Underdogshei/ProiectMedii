﻿using System;
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
    }
}
