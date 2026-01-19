using Movies.GraphQL.Client.Services;
using Movies.Models;

var service = new MovieService();

var movie = await service.GetMovie(3);

Console.WriteLine($"Movies: {movie.Name}");

var movieNew = new Movie()
{
    Name = "The Last of us part III",
    Description = "Filme de fungos III",
    LunchDate = DateTime.Now,
    Genre = Movies.Models.Enums.MovieGenre.Action

};
//movie = await service.AddMovie(movieNew);

movie = await service.UdateMovie(5,movieNew);


Console.ReadKey();