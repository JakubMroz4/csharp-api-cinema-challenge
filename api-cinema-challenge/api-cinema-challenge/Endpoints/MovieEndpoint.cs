using api_cinema_challenge.DTOs.Customer;
using api_cinema_challenge.DTOs.Movie;
using api_cinema_challenge.DTOs.Screening;
using api_cinema_challenge.DTOs.Ticket;
using api_cinema_challenge.Factories;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository.Interfaces;
using api_cinema_challenge.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints
{
    public static class MovieEndpoint
    {
        public static void ConfigureCustomerEndpoint(this WebApplication app)
        {
            const string groupName = "movies";
            const string contentType = "application/json";

            var moviesGroup = app.MapGroup(groupName);

            moviesGroup.MapGet("/", GetAllMovies);
            moviesGroup.MapPost("/", CreateMovie).Accepts<MoviePostDto>(contentType);
            moviesGroup.MapPut("/{movieId}", UpdateMovie).Accepts<MoviePutDto>(contentType);
            moviesGroup.MapDelete("/{movieId}", DeleteMovie);

            moviesGroup.MapPost("/{movieId}/screenings", CreateScreening).Accepts<ScreeningPostDto>(contentType);
            moviesGroup.MapGet("/{movieId}/screenings", GetScreenings);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAllMovies(IMovieRepository repository)
        {
            var movie = await repository.GetAllAsync();

            List<MovieDto> dtos = new();
            foreach (var customer in movie)
            {
                dtos.Add(MovieFactory.DtoFromMovie(customer));
            }

            return TypedResults.Ok(new { Status = "success", Data = dtos });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> CreateMovie(IMovieRepository repository, HttpRequest request)
        {
            MoviePostDto inDto = await Utility.ValidateFromRequest<MoviePostDto>(request);
            if (inDto is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }

            var added = await repository.CreateMovie(MovieFactory.MovieFromPostDto(inDto));
            if (added is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }

            var outDto = MovieFactory.DtoFromMovie(added);

            var url = $"{request.Scheme}://{request.Host}{request.Path}/{outDto.Id}";

            return TypedResults.Created(url, new { status = "success", data = outDto });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateMovie(IMovieRepository repository, HttpRequest request, int movieId)
        {
            MoviePutDto inDto = await Utility.ValidateFromRequest<MoviePutDto>(request);
            if (inDto is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }

            var entity = await repository.GetByIdAsync(movieId);
            if (entity is null)
            {
                return TypedResults.NotFound(new { status = "failure" });
            }

            var updated = await repository.UpdateMovie(MovieFactory.MovieFromPutDto(inDto, entity));
            if (updated is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }


            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteMovie(IMovieRepository repository, int movieId)
        {
            var movie = await repository.DeleteMovie(movieId);
            if (movie is null)
            {
                return TypedResults.NotFound(new { status = "failure" });
            }

            var dto = MovieFactory.DtoFromMovie(movie);
            return TypedResults.Ok(new { status = "success", data = dto });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> CreateScreening(IScreeningRepository repository, HttpRequest request, int movieId)
        {
            ScreeningPostDto inDto = await Utility.ValidateFromRequest<ScreeningPostDto>(request);
            if (inDto is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }

            var ticket = await repository.CreateScreening(ScreeningFactory.ScreeningFromPostDto(inDto, movieId));
            if (ticket is null)
            {
                return TypedResults.BadRequest(new { status = "failure" });
            }

            var dto = ScreeningFactory.DtoFromScreening(ticket);
            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetScreenings(IScreeningRepository repository, HttpRequest request, int movieId)
        {
            var screenings = await repository.GetByIdAsync(movieId);
            if (screenings is null)
            {
                return TypedResults.NotFound(new { status = "failure" });
            }

            List<ScreeningDto> dtos = new();
            foreach (var screening in screenings)
            {
                dtos.Add(ScreeningFactory.DtoFromScreening(screening));
            }
            // TODO fix url
            var url = $"{request.Scheme}://{request.Host}{request.Path}/";
            return TypedResults.Created(url, new { status = "success", data = dtos });
        }
    }
}
