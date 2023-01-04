using connect_s_events_domain.EventActivities;
using connect_s_events_domain.EventActivities.Models;
using connect_s_events_infrastructure.DataAccess;

namespace connect_s_events_infrastructure.EventActivities;
public class EventActivityRepository : IEventActivityRepository
{
    private readonly IDapperContext _context;

    public EventActivityRepository(IDapperContext dapperContext)
    {
        _context = dapperContext;
    }
    public async Task Add(EventActivity entity)
    {

        string sqlQuery = "INSERT into dbo.EventActivities (Id, Name, Description, Owner, OwnerEmail, BeginAt, EndAt, CreatedAt) " +
            "values (@Id, @Name, @Description, @Owner, @OwnerEmail, @BeginAt, @EndAt, @CreatedAt)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id, DbType.Guid);
        parameters.Add("Name", entity.Name, DbType.String);
        parameters.Add("Description", entity.Description, DbType.String);
        parameters.Add("Owner", entity.Owner, DbType.Guid);
        parameters.Add("OwnerEmail", entity.OwnerEmail, DbType.String);
        parameters.Add("BeginAt", entity.BeginAt, DbType.DateTimeOffset);
        parameters.Add("EndAt", entity.EndAt, DbType.DateTimeOffset);
        parameters.Add("CreatedAt", entity.CreatedAt, DbType.DateTimeOffset);

        await _context.SaveData(sqlQuery, parameters);
    }

    public async Task AddParticipant(Participant entity)
    {
        var getActivityQuery = "Select * from dbo.EventActivities e  where e.Id  = @Id";
        var getActivityQueryParams = new DynamicParameters();
        getActivityQueryParams.Add("Id", entity.EventActivityId, DbType.Guid);
        var activity = await _context.Connection.QuerySingleOrDefaultAsync<EventActivity>(getActivityQuery, getActivityQueryParams);
        if (activity is null)
            throw new Exception("no Actitvity with this id");

        var createParticipantQuery = "INSERT into dbo.Participants (UserId, EventActivityId, Name, Email, ParticipatedAt) " +
            "values (@UserId, @EventActivityId, @Name, @Email, @ParticipatedAt)";
        var createParticipantQueryParams = new DynamicParameters();
        createParticipantQueryParams.Add("UserId", entity.UserId, DbType.Guid);
        createParticipantQueryParams.Add("EventActivityId", entity.EventActivityId, DbType.Guid);
        createParticipantQueryParams.Add("Name", entity.Name, DbType.String);
        createParticipantQueryParams.Add("Email", entity.Email, DbType.String);
        createParticipantQueryParams.Add("ParticipatedAt", entity.ParticipatedAt, DbType.DateTimeOffset);

        await _context.SaveData(createParticipantQuery, createParticipantQueryParams);
    }

    public async Task Delete(Guid eventActivityId, CancellationToken cancellationToken)
    {
        try
        {
            var deleteParticipantsCommand = "DELETE FROM dbo.Participants WHERE EventActivityId=@Id";
            var deleteEventActivityCommand = "DELETE FROM dbo.EventActivities WHERE Id=@Id";
            var commandParams = new DynamicParameters();
            commandParams.Add("Id", eventActivityId, DbType.Guid);
            _context.BeginTransaction();
            await _context.Connection.ExecuteAsync(deleteParticipantsCommand, commandParams, _context.Transaction);
            await _context.Connection.ExecuteAsync(deleteEventActivityCommand, commandParams, _context.Transaction);
            _context.CommitTransaction();
        }
        catch (Exception)
        {
            _context.RollbackTransaction();
        }

    }

    public Task<IEnumerable<EventActivity>> GetAll()
    {
        var query = "Select * from dbo.EventActivities";
        return _context.LoadData<EventActivity, object>(query, new { });
    }
}
