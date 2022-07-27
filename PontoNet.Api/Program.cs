using PontoNet.Api.Filters;
using PontoNet.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectRepositories()
                .InjectDatabase()
                .InjectMediator()
                .InjectNotification()
                .InjectUnitOfWork();

builder.Services.AddControllers(opt =>
    opt.Filters.Add(typeof(NotificationFilter))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
