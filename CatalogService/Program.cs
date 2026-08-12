using Asp.Versioning;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Marten for PostgreSQL document storage
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgres")
        ?? builder.Configuration.GetConnectionString("BookOrderManagementDB")
        ?? "Host=postgres;Port=5432;Database=catalogdb;Username=postgres;Password=PostgrePassword123!";
    options.Connection(connectionString);
}).UseLightweightSessions();

// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
});

// Configure MediatR with LoggingBehavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Register CustomExceptionHandler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add Endpoints API Explorer & Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"),
        new QueryStringApiVersionReader("api-version")
    );
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseExceptionHandler(options => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
