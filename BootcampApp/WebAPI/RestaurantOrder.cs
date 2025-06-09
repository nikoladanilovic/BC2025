using System.ComponentModel.DataAnnotations;

namespace WebAPI
{
    public class RestaurantOrder
    {
        public Guid Id { get; set; }
        //public int Id { get; set; }
        // kasnije treba nesto dodati tipa [required] ...

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string DishName { get; set; }

        [Required]
        [Range(1.0, 120.0)] // restrict age range
        public double PriceOfDish { get; set; } 

    }
}
