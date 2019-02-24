using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.Data.Context;
using WEBAPI.Data.Repositories;

namespace WEBAPI.Data.UnitOfWork
{
    
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _dbContext;

        public EFUnitOfWork(ApiContext dbContext)
        {
            Database.SetInitializer<ApiContext>(null);

            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
            
            //_dbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        #region IUnitOfWork Members
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {

                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region IDisposable Members
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
