using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;


namespace StudentApplication.Controllers
{
    public class Student
    {
        public int student_id, adress_id;
        public string first_name, last_name;
    }


    public class StudentController : ApiController
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");

        List<Student> list_of_students = new List<Student>();

        // GET 
        [HttpGet]
        [Route("api/Student")]
        public HttpResponseMessage Get()
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand
                    ("SELECT * FROM Student;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.student_id = reader.GetInt32(0);
                        student.first_name = reader.GetString(1);
                        student.last_name = reader.GetString(2);
                        student.adress_id = reader.GetInt32(3);


                        list_of_students.Add(student);
                    }
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, list_of_students);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no students");
                }
                
            }
        }






        // GET with id
        [HttpGet]
        [Route("api/Student/{Student_id}")]
        public HttpResponseMessage Get(int Student_id)
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand
                    ("SELECT * FROM Student WHERE Student_id = @Student_id;", connection);

                SqlParameter id_parameter = new SqlParameter("@Student_id", System.Data.SqlDbType.Int);

                command.Parameters.Add(id_parameter);
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    Student student = new Student();
                    student.student_id = reader.GetInt32(0);
                    student.first_name = reader.GetString(1);
                    student.last_name = reader.GetString(2);
                    student.adress_id = reader.GetInt32(3);

                    connection.Close();

                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id " + Student_id);
                }
            }
        }

    }
}
        /*



        // POST 
        [HttpPost]
        [Route("api/Student")]
        public HttpResponseMessage Post([FromBody] string value)
        {
            using (connection)
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand();
                SqlCommand command = new SqlCommand
                    ("INSERT INTO Student VALUES (@Student_id, @First_name, @Last_name, @Adress_id);", connection);

                    command.Parameters.Add("@student_id", student_id);
                    command.Parameters.Add("@First_name", first_name);
                    command.Parameters.Add("@Last_name", last_name);
                    command.Parameters.Add("@Adress_id", adress_id);
                connection.Open();

                adapter.Fill(data)
                    
            }
        
                return Request.CreateResponse(HttpStatusCode.OK, "Student is successfully added");

            }
         
        }
  }
    }
        // PUT 
        [HttpPut]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            if (DictionaryofStudents.students.Count() < id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id: " + Convert.ToString(id));
            }
            else
            {
                DictionaryofStudents.students[id] = value;
                return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(id) + " is modified");
            }
        }

        // DELETE 
        [HttpDelete]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (DictionaryofStudents.students.Count() < id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id: " + Convert.ToString(id));
            }
            else
            {
                DictionaryofStudents.students.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(id) + " is deleted");
            }
        }
    }




}

        */