using Search.API.Installers;
using Search.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InstallMiddlewareServices();
builder.Services.InstallSearchServices();

// Register HTTP client here: 
builder.Services.AddHttpClient("HTTP_CLIENT")
    .ConfigureHttpClient((_, client) =>
    {
        client.BaseAddress =
            new Uri(builder.Configuration["TMDB:BaseUri"] ??
                    string.Empty);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.Timeout = TimeSpan.FromSeconds(30);
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
