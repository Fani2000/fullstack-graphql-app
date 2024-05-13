using ApplicationContext;
using EntityGraphQL.AspNet;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<CibContext>(opt => opt.UseInMemoryDatabase("testdb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGraphQLSchema<CibContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    var cibContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CibContext>();
    await DatabaseSeeder.SeedAsync(cibContext);
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(routeBuilder =>
{
    _ = routeBuilder.MapControllers();
    _ = routeBuilder.MapGraphQL<CibContext>(); // default url: /graphql
    _ = routeBuilder.MapGraphQLAltair("/ui/altair");
});

app.Run();
