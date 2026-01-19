using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL.Types.Enums;
using Movies.Models;
using Ext = GraphQL.Types;

namespace Movies.API.GraphQL.Types
{
    public class MovieType : ObjectGraphType<Movie>
    {
        public MovieType()
        {
            //Ext.IntGraphType

            Field(a => a.Id).Description("Id of movie");
            Field(a => a.Name).Description("Name of movie");
            Field(a => a.Description).Description("derscription of movie");
            Field(a => a.LunchDate).Description("lauch of movie");

            // mapeou o tipo enum
            Field<MovieGenreType>("Genre").Description("genre of movie");

            //mapeou o tipo list objt

            // assim com include porem problema perfomace
            //Field< ListGraphType< MovieReviewType>>("Reviews").Description("review of movie");

             // assim com multi consulta banco. problema de performace
            //Field<ListGraphType<MovieReviewType>>("Reviews").Description("review of movie")
            //    .Resolve( context =>
            //    {
            //        var db = context.RequestServices.GetRequiredService<MovieDbContent>();
            //        var movieId = context.Source.Id;
            //        return db.Reviews.Where(a => a.MovieId == movieId).ToList();
            //    });


            // implementando DataLoader
            Field<ListGraphType<MovieReviewType>>("Reviews").Description("review of movie")
            .Resolve(context =>
            {
                var db = context.RequestServices.GetRequiredService<MovieDbContent>();
                
                var accessor = context.RequestServices
                                      .GetRequiredService<IDataLoaderContextAccessor>();

                var loader = accessor.Context.GetOrAddCollectionBatchLoader<int,MovieReview>("GetMovieReviewByMovieId",
                    async moviesIds =>
                    {
                     var reviews = await   db.Reviews.Where(a => moviesIds.Contains(a.MovieId)).ToListAsync();
                        return reviews.ToLookup(a=>a.MovieId);

                    });
                return loader.LoadAsync(context.Source.Id);


            });

        }
    }
}
