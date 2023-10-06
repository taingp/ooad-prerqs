using System.Reflection.Metadata.Ecma335;
using BookLib;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(option =>
{
    option.AddPolicy("CORS_Policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORS_Policy");
app.UseHttpsRedirection();

MapBookEndpoints(app, "Books");
app.Run();

void MapBookEndpoints(WebApplication app, string tag)
{
    app.MapGet("api/books", ()=>{return new BookService().ReadAll();}).WithTags(tag).RequireCors("CORS_Policy");
    app.MapGet("api/books/{key}",(string key)=>{return new BookService().Read(key); }).WithTags(tag).RequireCors("CORS_Policy");
    app.MapPost("api/books", (Book req)=>{return new BookService().Create(req);}).WithTags(tag).RequireCors("CORS_Policy");
    app.MapPut("api/books", (Book req) => { return new BookService().Update(req);}).WithTags(tag).RequireCors("CORS_Policy");
    app.MapDelete("api/books/{key}", (string key)=> { return new BookService().Delete(key);}).WithTags(tag).RequireCors("CORS_Policy");
}
