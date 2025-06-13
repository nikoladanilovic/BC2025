namespace WebAPI.RESTModels
{
    public class StaffREST
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime HireDate { get; set; }
        public double Salary { get; set; }

        public StaffREST(Guid id, string name, string role, DateTime hireDate, double salary)
        {
            Id = id;
            Name = name;
            Role = role;
            HireDate = hireDate;
            Salary = salary;
        }
    }
}
