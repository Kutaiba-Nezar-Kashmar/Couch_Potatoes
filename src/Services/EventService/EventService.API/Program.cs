using EventService.API.Installers;
using EventService.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InstallServices();
builder.Services.InstallMiddlewareServices();
builder.Services.InstallMappings();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(cors =>
{
    cors.AllowAnyOrigin();
    cors.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();