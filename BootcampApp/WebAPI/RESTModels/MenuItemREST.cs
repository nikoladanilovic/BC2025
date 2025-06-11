using BootcampApp.Model;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.RESTModels
{
    public class MenuItemREST
    {
        public Guid Id { get; set; }

        public string DishName { get; set; }

        public double PriceOfDish { get; set; }

        public Guid CategoryId { get; set; }

        public MenuCategoryModel Category { get; set; }

        public MenuItemREST(Guid Id, string DishName, double PriceOfDish, Guid CategoryId , MenuCategoryModel category)
        {
            this.Id = Id;
            this.DishName = DishName;
            this.PriceOfDish = PriceOfDish;
            this.CategoryId = CategoryId;
            this.Category = category;
        }
    }
}
