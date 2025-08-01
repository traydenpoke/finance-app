using MyPostgresApi.Services;
using MyPostgresApi.External;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend",
      policy =>
      {
        policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
});

// Register services
builder.Services.AddSingleton<PostgresService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddHttpClient<GoogleFinanceClient>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map your existing endpoints
MyPostgresApi.Endpoints.AccountEndpoints.MapAccountEndpoints(app);
MyPostgresApi.Endpoints.AssetEndpoints.MapAssetEndpoints(app);


// Run table creation as part of startup
using (var scope = app.Services.CreateScope())
{
  var dbService = scope.ServiceProvider.GetRequiredService<PostgresService>();
  await dbService.CreateAllTablesAsync();
}

app.Run();