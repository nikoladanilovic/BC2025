namespace WebAPI.RESTModels
{
    public class CustomerREST
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public CustomerREST(Guid id, string name, string phone, string email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
