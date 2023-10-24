using GloboTicket.TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices().ConfigurePipeline();



await app.ResetDatabaseAsync();

app.Run();


// we run migrations from persistence projects.