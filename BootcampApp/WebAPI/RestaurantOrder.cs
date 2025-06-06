using System.ComponentModel.DataAnnotations;

namespace WebAPI
{
    public class RestaurantOrder
    {
        public int Id { get; set; }
        // kasnije treba nesto dodati tipa [required] ...
        public string DishName { get; set; }
        public double PriceOfDish { get; set; }

    }
}
