using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WordShopExcersice.Data.DatabaseContext;
using WordShopExcersice.Services.Services.PostalCodeService;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(configuration.GetConnectionString("PostalCodeConnection")));


builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(ops =>
    {
        ops.JsonSerializerOptions.IgnoreNullValues = true;
        ops.JsonSerializerOptions.WriteIndented = true;
        ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostalCode, SqlPostalCode>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

builder.Services.AddScoped<IPostalCode, SqlPostalCode>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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