using GraphQL;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL;
using Movies.API.GraphQL.Mutations;
using Movies.API.GraphQL.Queries;
using Movies.API.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

//luis add conection banco
builder.Services.AddDbContext<MovieDbContent>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    ));

//luis add dependencia para class do schema do movie
builder.Services.AddScoped<MovieQuery>();
builder.Services.AddScoped<MovieMutation>();
builder.Services.AddScoped<MovieSchema>();

builder.Services.AddGraphQL(options => 
                          options.AddGraphTypes().AddSystemTextJson().AddDataLoader());

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

//luis add use graph
app.UseGraphQL<MovieSchema>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
