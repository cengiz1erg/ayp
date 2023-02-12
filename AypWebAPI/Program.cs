using AypWebAPI.Extensions;
using AypWebAPI.Helpers;
using AypWebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddFluentValidationRules(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; DataGenerator.Initialize(services); }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseAuthorization();


app.MapControllers();

app.Run();
