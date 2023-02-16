using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;    

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        ///     Adds a movie in database
        /// </summary>
        /// <param name="movieDTO">Object with the needed fields for the creation of a movie</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">In case insertion is successful</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ReturnMovieById), new {id = movie.Id}, movie);
        }

        /// <summary>
        ///     Returns all the movies in database
        /// </summary>
        /// <param name="skip">Skips certain amount of movies</param>
        /// <param name="take">How many movies will be returned</param>
        /// <returns>IEnumerable</returns>
        /// <response code="200">In case any movie was found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadMovieDTO> ReturnMovies([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            return _mapper.Map<List<ReadMovieDTO>>(_context.Movies.Skip(skip).Take(take));
        }

        /// <summary>
        ///     Returns one movie in database with the specified ID
        /// </summary>
        /// <param name="id">ID of the movie that is being searched</param>
        /// <returns>IActionResult</returns>
        /// /// <response code="200">In case the movie exists in the database</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ReturnMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            var movieDTO = _mapper.Map<ReadMovieDTO>(movie);
            return Ok(movieDTO);
        }

        /// <summary>
        ///     Updates one movie with the specified ID
        /// </summary>
        /// <param name="id">ID of the movie that will be updated</param>
        /// <param name="movieDTO">Object with all the fields needed to update a movie</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">In case the update was successful</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO movieDTO) {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            _mapper.Map(movieDTO, movie);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        ///     Updates *parcially* one movie with the specified ID
        /// </summary>
        /// <param name="id">ID of the movie that will be updated</param>
        /// <param name="patch">Object used to update parcially the specified movie</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">In case the update was successful</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateMovieParcially(int id, JsonPatchDocument<UpdateMovieDTO> patch) {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();

            var movieToUpdate = _mapper.Map<UpdateMovieDTO>(movie);
            patch.ApplyTo(movieToUpdate, ModelState);
            if (!TryValidateModel(movieToUpdate)) return ValidationProblem(ModelState);
            _mapper.Map(movieToUpdate, movie);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        ///     Deletes one movie with the specified ID
        /// </summary>
        /// <param name="id">ID of the movie that will be deleted</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">In case the delete was successful</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            _context.Remove(movie);
            _context.SaveChanges();
            return NoContent(); 
        }
    }
}
