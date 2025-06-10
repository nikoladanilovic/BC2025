using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Model
{
    public class MenuItemModel
    {
        public Guid Id { get; set; }

        public string DishName { get; set; }
        
        public double PriceOfDish { get; set; }

        public Guid CategoryId { get; set; }
    }
}
