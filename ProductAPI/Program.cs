using ProductLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    app.MapGet("api/products", () => { return (new ProductService()).ReadAll(); }).WithTags(tag);
    app.MapGet("api/products/{key}", (string key) => { return (new ProductService()).Read(key); }).WithTags(tag);
    app.MapPost("api/products", (ProductCreateReq req) => { return new ProductService().Create(req); }).WithTags(tag);
    app.MapPut("api/products", (ProductUpdateReq req) => { return new ProductService().Update(req); }).WithTags(tag);
    app.MapDelete("api/products/{key}", (string key) => { return (new ProductService()).Delete(key); }).WithTags(tag);
}

