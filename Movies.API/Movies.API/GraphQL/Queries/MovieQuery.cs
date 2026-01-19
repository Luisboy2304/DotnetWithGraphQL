using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL.Types;

namespace Movies.API.GraphQL.Queries
{
    public class MovieQuery : ObjectGraphType
    {
        public MovieQuery(MovieDbContent db)
        {
            //lista tudo
            Field<ListGraphType<MovieType>>("movies")
                .Resolve( a =>  db.Movies.ToList());

            //movies com paginacao e tamanho pagina
            Field<ListGraphType<MovieType>>("moviesPage")
             .Arguments(
                new QueryArguments(new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 10 })
                )
            .ResolveAsync(async context =>
            {
                var page = context.GetArgument<int>("page");
                var pageSize = context.GetArgument<int>("pageSize");

                //trata valores
                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 10;

                return await db.Movies
                                    .Include(a => a.Reviews)
                                    .OrderBy(m => m.Id)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            });


            // by id
            Field<MovieType>("movie")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id" }))
                .Resolve( context =>
                {
                    var id = context.GetArgument<int>("id");
                    return  db.Movies.Include(a => a.Reviews).Single(a => a.Id == id);


                });
        }
    }
}
