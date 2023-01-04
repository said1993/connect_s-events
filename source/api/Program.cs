using connect_s_events_api.Helpers;
using connect_s_events_domain.EventActivities;
using connect_s_events_infrastructure.DataAccess;
using connect_s_events_infrastructure.EventActivities;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var elsConfigs = new ELSConfigurations();
builder.Configuration.GetSection("ELKConfigurations").Bind(elsConfigs);
builder.Host.ConfigureLogs(elsConfigs);

builder.Services.AddControllers();
builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IDapperContext, DapperContext>()
    .AddScoped<IEventActivityRepository, EventActivityRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var x = builder.Configuration[""];
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
