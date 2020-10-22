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

        public virtual async Task<bool> Add(T entity)
        {
            await Master.AddAsync(entity);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual async Task<bool> AddRange(List<T> list)
        {
            await Master.AddRangeAsync(list);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual async Task<bool> Update(T entity)
        {
            entity.UpdateTime = DateTime.Now;
            Master.Update(entity);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual async Task<bool> UpdateRange(List<T> list)
        {
            list.ForEach(q =>
            {
                q.UpdateTime = DateTime.Now;
            });
            Master.UpdateRange(list);
            return (await Master.SaveChangesAsync()) > 0;
        }

        public virtual async Task<bool> Remove(int id)
        {
            if (await IsExist(id))
            {
                var entity = await Master.FindAsync<T>(id);
                entity.UpdateTime = DateTime.Now;
                entity.State = 1;
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public virtual async Task<bool> HardRemove(int id)
        {
            if (await IsExist(id))
            {
                var entity = await Master.FindAsync<T>(id);
                Master.Remove(entity);
                return (await Master.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public virtual async Task<bool> IsExist(int id)
        {
            return await Slave.Set<T>().AnyAsync(q => q.Id.Equals(id));
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Slave.FindAsync<T>(id);
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= ExprTrue;
            return await Slave.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<T1>> GetList<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null)
        {
            predicate ??= ExprTrue;
            if (scalar == null)
                return new List<T1>();
            return await Slave.Set<T>().AsNoTracking().Where(predicate).Select(scalar).ToListAsync();
        }

    }
}
