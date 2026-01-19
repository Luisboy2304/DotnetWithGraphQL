using GraphQL.Types;
using Movies.API.GraphQL.Types.Enums;
using Movies.Models;

namespace Movies.API.GraphQL.Types.Inputs
{
    public class MovieInputType: InputObjectGraphType<Movie>
    {

        public MovieInputType()
        {
            //Field<StringGraphType>("Name");
            Field(a => a.Name).Description("Name of Movie");
            Field(a => a.Description).Description("Descript of Movie");
            Field(a => a.LunchDate).Description("LunchDate of Movie");
            Field<MovieGenreType>("genre").Description("Genre of Movie");


        }
    }
}
