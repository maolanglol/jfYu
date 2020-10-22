using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jfYu.Core.Data
{
    public class Service<T> : IService<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> ExprTrue = q => true;

        public DbContext Master { get; }

        public DbContext Slave { get; }

        public List<DbContext> Slaves { get; }

        public Service(DbContextService<DbContext> contextService)
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

        public virtual bool Remove(int id)
        {
            if (IsExist(id))
            {
                var entity = Master.Find<T>(id);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return Master.SaveChanges() > 0;
            }
            return false;
        }

        public virtual async Task<bool> RemoveAsync(int id)
        {
            if (await IsExistAsync(id))
            {
                var entity = await Master.FindAsync<T>(id);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public virtual bool HardRemove(int id)
        {
            if (IsExist(id))
            {
                var entity = Master.Find<T>(id);
                Master.Remove(entity);
                return Master.SaveChanges() > 0;
            }
            return false;
        }

        public virtual async Task<bool> HardRemoveAsync(int id)
        {
            if (await IsExistAsync(id))
            {
                var entity = await Master.FindAsync<T>(id);
                Master.Remove(entity);
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }
        public virtual bool IsExist(int id)
        {
            return Slave.Set<T>().Any(q => q.Id.Equals(id));
        }
        public virtual async Task<bool> IsExistAsync(int id)
        {
            return await Slave.Set<T>().AnyAsync(q => q.Id.Equals(id));
        }

        public virtual T GetById(int id)
        {
            return Slave.Find<T>(id);
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Slave.FindAsync<T>(id);
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

        public virtual List<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return Slave.Set<T>().Where(predicate).ToList();
        }
        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return await Slave.Set<T>().Where(predicate).ToListAsync();
        }
        public virtual List<T1> GetList<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null)
        {
            predicate ??= ExprTrue;
            if (scalar == null)
                return new List<T1>();
            return Slave.Set<T>().Where(predicate).Select(scalar).ToList();
        }

        public virtual async Task<List<T1>> GetListAsync<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null)
        {
            predicate ??= ExprTrue;
            if (scalar == null)
                return new List<T1>();
            return await Slave.Set<T>().Where(predicate).Select(scalar).ToListAsync();
        }

    }
}
