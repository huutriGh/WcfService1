using Ardalis.Specification;
using BaseProject.Interface;
using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BaseProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        async public Task<List<T>> ListAsyncSpec(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        async Task<List<T>> IRepository<T>.ListAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : class
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> GetById<TId>(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        async Task<T> IRepository<T>.Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        async Task IRepository<T>.Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        async Task<T> IRepository<T>.Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetAsyncSpec(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync();
        }

        public Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToListAsync();
            }
            else
            {
                return query.ToListAsync();
            }
        }
    }

    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

        private readonly List<IEvaluator> evaluators = new List<IEvaluator>();

        public SpecificationEvaluator()
        {
            this.evaluators.AddRange(new IEvaluator[]
            {
                WhereEvaluator.Instance,
                OrderEvaluator.Instance,
                PaginationEvaluator.Instance,

            });
        }
        public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            this.evaluators.AddRange(evaluators);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification) where T : class
        {
            query = GetQuery(query, (ISpecification<T>)specification);

            return query.Select(specification.Selector);
        }

        /// <inheritdoc/>
        public virtual IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification, bool evaluateCriteriaOnly = false) where T : class
        {
            var evaluators = evaluateCriteriaOnly ? this.evaluators.Where(x => x.IsCriteriaEvaluator) : this.evaluators;

            foreach (var evaluator in evaluators)
            {
                query = evaluator.GetQuery(query, specification);
            }

            return query;
        }
    }
}
