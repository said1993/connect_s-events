using System.Diagnostics.CodeAnalysis;

namespace connect_s_events_infrastructure.DataAccess;
public class DapperContext : IDapperContext
{
    private readonly string _connectionString;

    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;
    public IDbConnection Connection => _connection;
    public IDbTransaction? Transaction => _transaction;

    [ExcludeFromCodeCoverage]
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnectionString") ??
            throw new ArgumentNullException(nameof(configuration));
        _connection = new SqlConnection(_connectionString);
    }

    public void BeginTransaction()
    {
        if (_transaction is null)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
    }

    public void CommitTransaction()
    {
        if (_transaction is not null)
        {
            _transaction.Commit();
            Connection.Close();
        }
        else
            throw new InvalidOperationException("the transcation is null");
    }

    public void RollbackTransaction()
    {
        if (_transaction is not null)
        {
            _transaction.Rollback();
            _connection.Close();
        }
        else
            throw new InvalidOperationException("the transcation is null");
    }
    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
            _transaction = null;
        }
        _connection.Dispose();
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string Query,
        U parameters)
    {
        return await Connection.QueryAsync<T>(Query, parameters, commandType: CommandType.Text);
    }

    public async Task SaveData(
        string Query,
        object parameters)
    {
        await Connection.ExecuteAsync(Query, parameters, commandType: CommandType.Text);
    }
}
