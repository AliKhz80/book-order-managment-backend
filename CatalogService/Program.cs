using CatalogService;
using CatalogService.Application.Common.CurrentUser;
using CatalogService.Application.DI;
using CatalogService.Infrastructure.DI;
using CatalogService.Presentation.Extentions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Add HttpContextAccessor and CurrentUser implementation
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// Register Layers
builder.Services.ConfigureInfrustructorLayer(builder.Configuration);
builder.Services.ConfigureApplicationLayer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
