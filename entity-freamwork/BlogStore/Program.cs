using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BlogMysql.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// mysql db
builder.Services.AddDbContext<BlogDataContext>();

// swagger
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "` API",
         Description = "Making the blogs you love",
         Version = "v1" });
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog store API V1");
});


app.MapGet("/", () => "Hello World!");
app.MapGet("/authors", async (BlogDataContext db) => await db.Authors.ToListAsync());
app.MapPost("/author", async (BlogDataContext db, Author author) =>
{
    await db.Authors.AddAsync(author);
    await db.SaveChangesAsync();
    return Results.Created($"/author/{author.AuthorId}", author);
}).WithOpenApi();
app.MapGet("/author/{id}", async (BlogDataContext db, int id) => await db.Authors.FindAsync(id));
app.MapPut("/author/{id}", async (BlogDataContext db, Author updateauthor, int id) =>
{
      var author = await db.Authors.FindAsync(id);
      if (author is null) return Results.NotFound();
      author.Name = updateauthor.Name;
      author.Email = updateauthor.Email;
      await db.SaveChangesAsync();
      return Results.NoContent();
});
app.MapDelete("/author/{id}", async (BlogDataContext db, int id) =>
{
   var author = await db.Authors.FindAsync(id);
   if (author is null)
   {
      return Results.NotFound();
   }
   db.Authors.Remove(author);
   await db.SaveChangesAsync();
   return Results.Ok();
});
app.Run();
