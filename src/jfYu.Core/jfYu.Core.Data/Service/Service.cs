using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jfYu.Core.Data
{
    public class Service<T, Q> : IService<T> where T : BaseEntity
        where Q : DbContext
    {

        public Expression<Func<T, bool>> ExprTrue = q => true;

        public Q Master { get; }

        public Q Slave { get; }

        public List<Q> Slaves { get; }

        public Service(IDbContextService<Q> contextService)
        {

            Master = contextService.Master;
            Slave = contextService.Slave;
            Slaves = contextService.Slaves;
        }
        public virtual bool Add(T entity)
        {
            Master.Add(entity);
            return Master.SaveChanges() > 0;
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await Master.AddAsync(entity);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual bool AddRange(List<T> list)
        {
            Master.AddRange(list);
            return Master.SaveChanges() > 0;
        }

        public virtual async Task<bool> AddRangeAsync(List<T> list)
        {
            await Master.AddRangeAsync(list);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual bool Update(T entity)
        {
            entity.UpdateTime = DateTime.Now;
            Master.Update(entity);
            return Master.SaveChanges() > 0;
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdateTime = DateTime.Now;
            Master.Update(entity);
            return (await Master.SaveChangesAsync()) > 0;
        }
        public virtual bool UpdateRange(List<T> list)
        {
            list.ForEach(q =>
            {
                q.UpdateTime = DateTime.Now;
            });
            Master.UpdateRange(list);
            return Master.SaveChanges() > 0;
        }

        public virtual async Task<bool> UpdateRangeAsync(List<T> list)
        {
            list.ForEach(q =>
            {
                q.UpdateTime = DateTime.Now;
            });
            Master.UpdateRange(list);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual bool Remove(Guid guid)
        {
            if (IsExist(guid))
            {
                var entity = Master.Find<T>(guid);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return Master.SaveChanges() > 0;
            }
            return false;
        }
        public virtual bool Remove(string guid)
        {
            if (IsExist(guid))
            {
                var entity = Master.Find<T>(guid);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return Master.SaveChanges() > 0;
            }
            return false;
        }
        public virtual async Task<bool> RemoveAsync(Guid guid)
        {
            if (await IsExistAsync(guid))
            {
                var entity = await Master.FindAsync<T>(guid);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }
        public virtual async Task<bool> RemoveAsync(string guid)
        {
            if (await IsExistAsync(guid))
            {
                var entity = await Master.FindAsync<T>(guid);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public virtual bool HardRemove(Guid guid)
        {
            if (IsExist(guid))
            {
                var entity = Master.Find<T>(guid);
                Master.Remove(entity);
                return Master.SaveChanges() > 0;
            }
            return false;
        }
        public virtual bool HardRemove(string guid)
        {
            if (IsExist(guid))
            {
                var entity = Master.Find<T>(guid);
                Master.Remove(entity);
                return Master.SaveChanges() > 0;
            }
            return false;
        }

        public virtual async Task<bool> HardRemoveAsync(Guid guid)
        {
            if (await IsExistAsync(guid))
            {
                var entity = await Master.FindAsync<T>(guid);
                Master.Remove(entity);
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }
        public virtual async Task<bool> HardRemoveAsync(string guid)
        {
            if (await IsExistAsync(guid))
            {
                var entity = await Master.FindAsync<T>(guid);
                Master.Remove(entity);
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }
        public virtual bool IsExist(Guid guid)
        {
            return Slave.Set<T>().Any(q => q.Guid.Equals(guid));
        }
        public virtual bool IsExist(string guid)
        {
            return Slave.Set<T>().Any(q => q.Guid.Equals(guid));
        }
        public virtual async Task<bool> IsExistAsync(Guid guid)
        {
            return await Slave.Set<T>().AnyAsync(q => q.Guid.Equals(guid));
        }
        public virtual async Task<bool> IsExistAsync(string guid)
        {
            return await Slave.Set<T>().AnyAsync(q => q.Guid.Equals(guid));
        }
        public virtual T GetById(Guid guid)
        {
            return Slave.Find<T>(guid);
        }
        public virtual T GetById(string guid)
        {
            return Slave.Find<T>(guid);
        }
        public virtual async Task<T> GetByIdAsync(Guid guid)
        {
            return await Slave.FindAsync<T>(guid);
        }
        public virtual async Task<T> GetByIdAsync(string guid)
        {
            return await Slave.FindAsync<T>(guid);
        }

        public virtual T GetOne(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return Slave.Set<T>().Where(predicate).SingleOrDefault();
        }
        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return await Slave.Set<T>().Where(predicate).SingleOrDefaultAsync();
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return Slave.Set<T>().Where(predicate).AsQueryable();
        }
        public virtual async Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return await Task.Run(() => Slave.Set<T>().Where(predicate).AsQueryable());
        }
        public virtual IQueryable<T1> GetList<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null)
        {
            predicate ??= ExprTrue;
            if (scalar == null)
                return new List<T1>().AsQueryable();
            return Slave.Set<T>().Where(predicate).Select(scalar).AsQueryable();
        }

        public virtual async Task<IQueryable<T1>> GetListAsync<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null)
        {
            predicate ??= ExprTrue;
            if (scalar == null)
                return new List<T1>().AsQueryable();
            return await Task.Run(() => Slave.Set<T>().Where(predicate).Select(scalar).AsQueryable());
        }

    }
}
