using GraphQL.Types;
using Movies.API.GraphQL.Types.Enums;
using Movies.API.Models;
using Ext = GraphQL.Types;

namespace Movies.API.GraphQL.Types
{
    public class MovieType: ObjectGraphType<Movie>
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
            Field<MovieReviewType>("Reviews").Description("review of movie");
            
        }
    }
}
