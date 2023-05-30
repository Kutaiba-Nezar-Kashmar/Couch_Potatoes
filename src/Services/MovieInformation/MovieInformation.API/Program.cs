using MovieInformation.API.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallControllers();
builder.Services.InstallMiddlewareServices();


// Register HTTP client here: 
builder.Services.AddHttpClient("HTTP_CLIENT")
    .ConfigureHttpClient((_, client) =>
    {
        client.BaseAddress =
            new Uri(builder.Configuration["TMDB:MovieEndpoint"] ?? string.Empty);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.Timeout = TimeSpan.FromSeconds(30);
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
