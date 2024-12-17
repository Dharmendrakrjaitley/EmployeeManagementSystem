using Dapper;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;
        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            var query = "Insert into Employee_Table (Id, Name, Position, Salary) VALUES(@Id, @Name, @Position, @Salary)";
            return await _dbConnection.ExecuteAsync(query,employee);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "Delete from Employee_Table where Id=@Id";
            return await _dbConnection.ExecuteAsync(query, new {Id=id});
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = "select * from Employee_Table";
            return await _dbConnection.QueryAsync<Employee>(query);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var query = "select * from Employee_Table where Id=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new {Id=id});
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            var query = "update Employee_Table set Name=@Name, Position=@Position, Salary=@Salary where Id=@Id";
            return await _dbConnection.ExecuteAsync(query, employee);
        }
    }
}
