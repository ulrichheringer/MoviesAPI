using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class ReadMovieDTO
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Duration { get; set; }
        public DateTime ConsultedTime { get; set; } = DateTime.Now;
    }
}
