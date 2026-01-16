using GraphQL.Types;
using Movies.API.Models;

namespace Movies.API.GraphQL.Types
{
    public class MovieReviewType:ObjectGraphType<MovieReview>
    {
        public MovieReviewType()
        {
            Field(a => a.Id).Description("Id of review");
            Field(a => a.Rate).Description("Rate of review");
            Field(a => a.Comment).Description("Commet of review");
            Field(a => a.CreateAt).Description("Date of Create of review");
            Field(a => a.MovieId).Description("Id of movie");
          }
    }
}
