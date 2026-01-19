using GraphQL.Types;
using Movies.API.GraphQL.Mutations;
using Movies.API.GraphQL.Queries;

namespace Movies.API.GraphQL
{
    public class MovieSchema: Schema
    {
        public MovieSchema(IServiceProvider service)
        {
            Query = service.GetRequiredService<MovieQuery>();
            Mutation = service.GetRequiredService<MovieMutation>();
        }
    }
}
