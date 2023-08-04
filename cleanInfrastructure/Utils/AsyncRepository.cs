namespace cleanInfrastructure.Utils
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly DbContext _context;

        public AsyncRepository()
        {
        }

        public AsyncRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this._context);
                }
                return unitOfWork;
            }
            set
            {
                unitOfWork = new UnitOfWork(this._context);
            }
        }

        public virtual async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AttachAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _context.Set<T>().Attach(entity);
        }

        public async Task<int> CountAsync()
        {
            return await (await GetQueryAsync()).CountAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> criteria)
        {
            IEnumerable<T> records = await FindAsync(criteria);

            foreach (T record in records)
            {
                await DeleteAsync(record);
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return (await GetQueryAsync()).Where(criteria);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> criteria)
        {
            return await (await GetQueryAsync()).Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await(await GetQueryAsync()).FirstAsync(predicate);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IQueryable<T>> GetQueryAsync()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public async Task<IQueryable<T>> GetQueryAsync(Expression<Func<T, bool>> predicate)
        {
            return (await GetQueryAsync()).Where(predicate);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}