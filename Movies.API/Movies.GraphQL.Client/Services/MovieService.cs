using GraphQL;
using Movies.GraphQL.Client.DTO;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.GraphQL.Client.Services
{
    public class MovieService : GraphQLClient
    {
        public MovieService()
        {

        }

        public async Task<Movie> GetMovie(int id)
        {
            var query = new GraphQLRequest()
            {
                //Query = "{movie(id:"+id+ "){id name description lunchDate genre}}"

                //with variables
                Query = "query getMovie($movieId:Int!){movie(id:$movieId){id name description lunchDate genre}}",
                Variables = new { movieId = id }
            };
            var response = await Client.SendQueryAsync<MovieResponse>(query);

            return response.Data.Movie;
        }

        public async Task<List<Movie>> GetMovieAll()
        {
            var query = new GraphQLRequest()
            {
                //Query = "{movie(id:"+id+ "){id name description lunchDate genre}}"

                Query = "{movies {id name description lunchDate genre}}"
            };
            var response = await Client.SendQueryAsync<MoviesResponse>(query);

            return response.Data.Movies;
        }
        public async Task<Movie> AddMovie(Movie movieNew)
        {
            var query = new GraphQLRequest()
            {

                //with variables
                Query = "mutation AddMovie($movie:MovieInput!){movie:addMovie(movie:$movie){id name description}}",
                Variables = new { movie = new { movieNew.Name, movieNew.Description, movieNew.LunchDate, movieNew.Genre } }
            };
            var response = await Client.SendQueryAsync<MovieResponse>(query);

            return response.Data.Movie;
        }
        public async Task<Movie> UdateMovie(int MovieId,Movie movieNew)
        {
            var query = new GraphQLRequest()
            {

                //with variables
                Query = "mutation UpdateMovie($movieId:ID! $movie:MovieInput!){movie:updateMovie(id: $movieId,movie:$movie){id name description genre lunchDate}}",
                Variables = new { movieId = MovieId ,movie = new { movieNew.Name, movieNew.Description, movieNew.LunchDate, movieNew.Genre } }
            };
            var response = await Client.SendQueryAsync<MovieResponse>(query);

            return response.Data.Movie;
        }
        public async Task<Boolean> DeleteMovie(int MovieId)
        {
            var query = new GraphQLRequest()
            {

                //with variables
                Query = "mutation DeleteMovie {deleteMovie(id: $movieId)}",
                Variables = new { movieId = MovieId }
            };
            var response = await Client.SendQueryAsync<MovieDeleteResponse>(query);

            return response.Data.Deleted;
        }


        
}
}
