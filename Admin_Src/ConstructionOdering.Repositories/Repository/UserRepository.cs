using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ConstructionOdering.Repositories.Repository
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string GetUsernameById(int userId)
        {
            string username = null;
            string query = "SELECT Username FROM Users WHERE id = @UserId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    username = reader["Username"].ToString();
                }
                reader.Close();
            }
            return username;
        }
    }
}
