using GasApp.Extensions;
using GasApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasApp.Data
{
    public class SQLiteDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Connection => lazyInitializer.Value;

        static bool IsInitialized = false;

        async Task InitializeAsync()
        {
            if (!IsInitialized)
            {
                if (!Connection.TableMappings.Any(mbox => mbox.MappedType.Name == typeof(GasModel).Name))
                {
                    await Connection.CreateTablesAsync(CreateFlags.None, typeof(GasModel)).ConfigureAwait(false);

                }
            }
        }

        public SQLiteDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private void OnInitializeError()
        {
            throw new NotImplementedException();

        }

        public Task<List<GasModel>> GetAllGasAsync()
        {
            return Connection.Table<GasModel>().ToListAsync();
        }

        public Task<GasModel> GetGasAsync(int id)
        {
            return Connection.Table<GasModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveGasAsync(GasModel item)
        {
            if (item.ID != 0)
            {
                return Connection.UpdateAsync(item);
            }
            else
            {
                return Connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteGasAsync(GasModel item)
        {
            return Connection.DeleteAsync(item);
        }

    }
}
