using ApplicationContext;
using System.Linq.Expressions;
using Entities;
using EntityGraphQL.AspNet;
using EntityGraphQL.Schema;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<CibContext>(opt => opt.UseInMemoryDatabase("testdb"));
builder.Services.AddEndpointsApiExplorer();

// var schema = SchemaBuilder.FromObject<CibContext>();
// schema.AddMutationsFrom<CompanyMutations>();

builder.Services.AddGraphQLValidator();

// builder.Services.AddSingleton<CompanyMutations>();
builder.Services.AddSingleton<ComapnyService>();

builder.Services.AddGraphQLSchema<CibContext>(opts =>
{
    opts.ConfigureSchema = (schema) =>
    {
        // Configure your schema
        schema.Mutation().Add("AddCompany", "Adding a company mutation", CompanyMutations.AddCompany);
        // schema.Mutation().AddFrom<CompanyMutations>();

        schema.Subscription().Add("SubscribeCompany","Subscribing to the companies entity", CompanySubscriptions.OnMessage);
    };
});
// builder.Services.AddSingleton<SchemaProvider<CibContext>>();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseWebSockets();
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

app.UseGraphQLWebSockets<CibContext>();

app.UseAuthorization();
app.UseEndpoints(routeBuilder =>
{
    _ = routeBuilder.MapControllers();
    _ = routeBuilder.MapGraphQL<CibContext>(); // default url: /graphql
    _ = routeBuilder.MapGraphQLAltair("/ui/altair");
});

app.Run();

