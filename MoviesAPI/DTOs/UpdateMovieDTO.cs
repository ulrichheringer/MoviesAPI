using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class UpdateMovieDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        [StringLength(50, ErrorMessage = "The genre size can not exceed 50 characters")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Faltando duration")]
        [Range(60, 540, ErrorMessage = "The duration must be greater than 60 minutes and less than 540 minutes.")]
        public int Duration { get; set; }
    }
}
