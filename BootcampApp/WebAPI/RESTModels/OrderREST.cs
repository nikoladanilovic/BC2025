using BootcampApp.Model;

namespace WebAPI.RESTModels
{
    public class OrderREST
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }    //connection 1 to many
        public DateTime OrderDate { get; set; }
        public Guid StaffId { get; set; }  // connection 1 to many
        public int TableNumber { get; set; }
        public CustomerREST? Customer { get; set; } // Assuming you have a CustomerREST model
        public StaffREST? Staff { get; set; }

        public OrderREST(Guid id, Guid customerId, DateTime orderDate, Guid staffId, int tableNumber, CustomerModel customer, StaffModel staff)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            StaffId = staffId;
            TableNumber = tableNumber;
            Customer = new CustomerREST(customer.Id, customer.Name, customer.Phone, customer.Email);
            Staff = new StaffREST(staff.Id, staff.Name, staff.Role, staff.HireDate, staff.Salary);
        }
    }
}
