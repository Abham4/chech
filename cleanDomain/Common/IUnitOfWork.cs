namespace cleanDomain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }
        Task SaveChanges();
        Task SaveChanges(SaveOptions saveOptions);
        Task BeginTransaction();
        Task BeginTransaction(IsolationLevel isolationLevel);
        Task RollBackTransaction();
        Task CommitTransaction();
    }
}