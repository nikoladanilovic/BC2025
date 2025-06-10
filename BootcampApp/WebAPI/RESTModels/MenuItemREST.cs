using System.ComponentModel.DataAnnotations;

namespace WebAPI.RESTModels
{
    public class MenuItemREST
    {
        public Guid Id { get; set; }

        public string DishName { get; set; }

        public double PriceOfDish { get; set; }

        public Guid CategoryId { get; set; }
    }
}
