namespace connect_s_events_infrastructure.DataAccess;

public interface IDapperContext : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    Task<IEnumerable<T>> LoadData<T, U>(string Query, U parameters);
    Task SaveData(string Query, object parameters);
}