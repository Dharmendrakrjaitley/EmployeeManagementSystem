using Dapper;
using EmployeeManagementSystem.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(IDbConnection dbConnection, ILogger<EmployeeRepository> logger)
        {
            _dbConnection = dbConnection;
            _logger= logger;
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            try 
            {
                var query = "Insert into Employee_Table (Id, Name, Position, Salary) VALUES(@Id, @Name, @Position, @Salary)";
                return await _dbConnection.ExecuteAsync(query, employee);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while creating new employee");
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try 
            {
                var query = "Delete from Employee_Table where Id=@Id";
                return await _dbConnection.ExecuteAsync(query, new { Id = id });
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Could not delete {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try 
            {
                var query = "select * from Employee_Table";
                return await _dbConnection.QueryAsync<Employee>(query);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees");
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                var query = "select * from Employee_Table where Id=@Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching the employee with the Id");
                throw;
            }
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            try
            {
                var query = "update Employee_Table set Name=@Name, Position=@Position, Salary=@Salary where Id=@Id";
                return await _dbConnection.ExecuteAsync(query, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating the employee");
                throw;
            }
        }
    }
}
