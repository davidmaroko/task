using Dapper;
using RonnieProjects.Entities;
using RonnieProjects.Storage;


namespace RonnieProjects.UserHandler
{
    public class UserService
    {
        private readonly SqlConnectionFactory _dapperFactory;
        private const string addUserQuery = "INSERT INTO UsersDetail (FirstName, LastName) VALUES (@FirstName, @LastName); SELECT SCOPE_IDENTITY();";

        public UserService(SqlConnectionFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                using (var connection = _dapperFactory.CreateConnection())
                {
                    var users = (List<User>)await connection.QueryAsync<User>("SELECT * FROM UsersDetail");
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("in user service: get all users :"+ex.Message, ex);
            }
        }

        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                using (var connection = _dapperFactory.CreateConnection())
                {
                    return await connection.ExecuteScalarAsync<int>(addUserQuery, user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("in user service: add new user :" + ex.Message, ex);
            }
        }
    }
}
