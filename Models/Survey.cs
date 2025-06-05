using System.ComponentModel.DataAnnotations;

namespace LifestyleSurveyApp.Models
{
    public class Survey : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number must be exactly 10 digits")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must contain only digits")]
        [Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber { get; set; }

        [Range(5, 120)]
        public int Age { get; set; }

        // Foods
        public bool LikesPizza { get; set; }
        public bool LikesPasta { get; set; }
        public bool LikesPapAndWors { get; set; }
        public bool LikesOther { get; set; }

        // Ratings (1-5)
        [Range(1, 5)]
        public int EatOutRating { get; set; }

        [Range(1, 5)]
        public int MovieRating { get; set; }

        [Range(1, 5)]
        public int TVRating { get; set; }

        [Range(1, 5)]
        public int RadioRating { get; set; }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!LikesPizza && !LikesPasta && !LikesPapAndWors && !LikesOther)
            {
                yield return new ValidationResult(
                    "Please select at least one food preference.",
                    new[] { nameof(LikesPizza), nameof(LikesPasta), nameof(LikesPapAndWors), nameof(LikesOther) }
                );
            }
        }
    }
}
