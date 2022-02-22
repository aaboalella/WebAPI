using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using WebAPI_Docker.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configurationr;
        static List<Employee> _emps = new List<Employee>
        {
            new Employee {Id = 1 , Name = "Ahmed" , Age = 25 },
            new Employee {Id = 2 , Name = "Ali" , Age = 25 },
            new Employee {Id = 3 , Name = "Mohamed" , Age = 25 },
            new Employee {Id = 4 , Name = "Hassan" , Age = 25 },
        };


        public EmployeesController(IConfiguration configuration)
        {
            _configurationr = configuration;
        }

        /*[HttpGet]
        public JsonResult Get()
        {
            string Query = @"Select * From Employee ";
            DataTable DT = new DataTable();
            string sqlDataSource = _configurationr.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(Query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    DT.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                };
            };
            return new JsonResult(DT);
            //return _emps ;
        }*/
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string Query = @"Select * 
                            From Employee 
                            Where id = @id ";
            DataTable DT = new DataTable();
            string sqlDataSource = _configurationr.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    DT.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                };
            };
            return new JsonResult(DT);
            //return _emps.FirstOrDefault(x => x.Id == id);
        }
        [HttpGet]
        public List<Employee> Get()
        {
            return _emps;
        }
        
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string Query = @"INSERT INTO Employee(Name,Age) values (@Name,@Age);";
            DataTable DT = new DataTable();
            string sqlDataSource = _configurationr.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", emp.Name);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myReader = myCommand.ExecuteReader();
                    DT.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                };
            };
            return new JsonResult("Added Successefuly");
            //_emps.Add(emp);
            //return emp;
        }
        
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string Query = @"UPDATE Employee
                            SET Age  = @Age
                            WHERE id = @id;";
            DataTable DT = new DataTable();
            string sqlDataSource = _configurationr.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", emp.Id);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myReader = myCommand.ExecuteReader();
                    DT.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                };
            };
            return new JsonResult("Updated Successfully");
            //var employee = _emps.FirstOrDefault(x => x.Id == id);
            //employee.Name = emp.Name;
            //employee.Age = emp.Age;
            //return emp;
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string Query = @"DELETE FROM Employee
                            WHERE id = @id;";
            DataTable DT = new DataTable();
            string sqlDataSource = _configurationr.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    DT.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                };
            };
            return new JsonResult("Deleted Successfully");
            //var employee = _emps.FirstOrDefault(x => x.Id == id);
            //_emps.Remove(employee);
            //return employee;
        }
    }
}
