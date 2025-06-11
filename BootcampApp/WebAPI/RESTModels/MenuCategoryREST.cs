namespace WebAPI.RESTModels
{
    public class MenuCategoryREST
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MenuCategoryREST(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
