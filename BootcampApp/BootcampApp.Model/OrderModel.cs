using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Model
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }    //connection 1 to many
        public DateTime OrderDate { get; set; }
        public Guid StaffId { get; set; }  // connection 1 to many
        public int TableNumber { get; set; }
        public CustomerModel? Customer { get; set; } 
        public StaffModel? Staff { get; set; } 
        
    }
}
