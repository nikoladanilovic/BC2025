using Npgsql;

namespace WebAPI
{
    public class DataAccess
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        public DataAccess()
        {
            //_connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";
        }

        public List<RestaurantOrder> GetDishes()
        {

            var dishes = new List<RestaurantOrder>();
            

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Menu\"", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RestaurantOrder newDish = new RestaurantOrder();
                        newDish.Id = reader.GetGuid(0);
                        newDish.DishName = reader.GetString(1);
                        newDish.PriceOfDish = reader.GetDouble(2);
                        dishes.Add(newDish);
                    }
                }
            }

            return dishes;
        }
    }
}
