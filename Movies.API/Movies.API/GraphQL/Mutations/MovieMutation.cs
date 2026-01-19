using GraphQL;
using GraphQL.Types;
using Movies.API.Data;
using Movies.API.GraphQL.Types;
using Movies.API.GraphQL.Types.Inputs;
using Movies.Models;

namespace Movies.API.GraphQL.Mutations
{
    public class MovieMutation : ObjectGraphType
    {
        public MovieMutation(MovieDbContent db)
        {
            Field<MovieType>("addMovie")
                .Arguments(
                    new QueryArgument<NonNullGraphType<MovieInputType>>()
                    {
                        Name = "movie",
                        Description = "Movie Input Parameter",
                    }
                )
                .ResolveAsync(async context =>
                {
                    var movie = context.GetArgument<Movie>("movie");
                    db.Movies.AddAsync(movie);
                    await db.SaveChangesAsync();

                    return movie;
                });

            Field<MovieType>("updateMovie")
                .Arguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name="id",
                        Description="Movie Id parameter"

                    },
                    new QueryArgument<NonNullGraphType<MovieInputType>>()
                    {
                        Name="movie",
                        Description="Movie input parameter of update"
                    }
                )
                 .ResolveAsync(async context =>
                 {
                     var id = context.GetArgument<int>("id");

                     var movie = context.GetArgument<Movie>("movie");
                     movie.Id = id;
                     db.Movies.Update(movie);
                     await db.SaveChangesAsync();

                     return movie;
                 });

            Field<BooleanGraphType>("deleteMovie")
                .Arguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id",
                        Description = "Movie Id parameter"

                    })
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var movie = await db.Movies.FindAsync(id);  
                    if(movie is not null)
                    {
                        db.Movies.Remove(movie);
                        await db.SaveChangesAsync();

                        return true;
                    }
                    return false;
                });
            

        }
    }
}
