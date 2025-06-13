namespace WebAPI.RESTModels
{
    public class OrderREST
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }    //connection 1 to 1
        public DateTime OrderDate { get; set; }
        public Guid StaffId { get; set; }  // connection 1 to many
        public int TableNumber { get; set; }
    }
}
