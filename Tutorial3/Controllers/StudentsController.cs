using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tutorial3.Models;

namespace Tutorial3.Controllers
{

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            var thestudents = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s16563;Integrated Security=True"))
            {
                using (var TheCommand = new SqlCommand())
                {
                    TheCommand.Connection = sqlConnection;
                    TheCommand.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                            "from Student s " +
                                            "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                            "join Studies st on st.IdStudy = e.IdStudy; ";
                    sqlConnection.Open();
                    var response = TheCommand.ExecuteReader();
                    while (response.Read())
                    {
                        //var st = new Student();
                        //st.FirstName = response["FirstName"].ToString();
                        //st.LastName = response["LastName"].ToString();
                        //st.Studies = response["Studies"].ToString();
                        //st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        //st.Semester = int.Parse(response["Semester"].ToString());

                        var st = new Student
                        {
                            FirstName = response["FirstName"].ToString(),
                            LastName = response["LastName"].ToString(),
                            Studies = response["Studies"].ToString(),
                            BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                            Semester = int.Parse(response["Semester"].ToString())
                        };

                        thestudents.Add(st);
                    }
                }
            }
            return Ok(thestudents);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            int sem = 0;

            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s16563;Integrated Security=True"))
            {


                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select Semester " +
                                            "from Enrollment e " +
                                            "join Student s on e.IdEnrollment = s.IdEnrollment " +
                                            "where s.IndexNumber =@id";
                    command.Parameters.AddWithValue("id", id);
                    sqlConnection.Open();
                    var response = command.ExecuteReader();
                    if (response.Read())
                    {
                        sem = int.Parse(response["Semester"].ToString());


                    }
                }
            }
            return Ok(sem);

        }



        //[HttpPost]
        //public IActionResult CreateStudent(Student student)
        //{
        //    student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //    // _dbService.GetStudents().ToList().Add(student);
        //    return Ok(student);
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateStudent(Student student, int id)
        //{
        //    if(student.idStudent != id)
        //    {
        //         return NotFound("Student Not Found");
        //    }
        //    // updating object 
        //    // student.FirstName = "James";
        //    // _dbService.GetStudents().ToList().Insert(id, student);
        //    return Ok("Update completed");
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteStudent(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound("Student Not Found");
        //    }
        //    // deleting object 
        //    //_dbService.GetStudents().ToList().RemoveAt(id);
        //    return Ok("Delete completed");
        //}
    }
}