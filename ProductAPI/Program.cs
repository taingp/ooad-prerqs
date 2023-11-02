using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi;
using ProductLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IDbContext, MemoryDbContext>(optionBuilder => { optionBuilder.UseInMemoryDatabase("ProductDb"); });
builder.Services.AddTransient<ProductRepo>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<IHostedService, InitService>();

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

MapProductEndpoints(app, "Products");

app.Run();

void MapProductEndpoints(WebApplication app, string tag)
{
    app.MapGet("api/products", ([FromServices] ProductService service) => { return service.ReadAll(); }).WithTags(tag);
    app.MapGet("api/products/{key}", ([FromServices] ProductService service,string key) => { return service.Read(key); }).WithTags(tag);
    app.MapPost("api/products", ([FromServices] ProductService service, ProductCreateReq req) => { return service.Create(req); }).WithTags(tag);
    app.MapPut("api/products", ([FromServices] ProductService service, ProductUpdateReq req) => { return service.Update(req); }).WithTags(tag);
    app.MapDelete("api/products/{key}", ([FromServices] ProductService service,string key) => { return service.Delete(key); }).WithTags(tag);
}

