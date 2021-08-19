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






        // GET by id
        [HttpGet]
        [Route("api/Student/{Student_id}")]
        public HttpResponseMessage Get(int Student_id)
        {
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                    ("SELECT * FROM Student WHERE Student_id = @Student_id;", connection);

                SqlParameter id_parameter = new SqlParameter("@Student_id", System.Data.SqlDbType.Int);

                command.Parameters.Add(id_parameter).Value = Student_id;



                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.student_id = Student_id;
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(Student_id));
                }
            }
        }



        // POST 
        [HttpPost]
        [Route("api/Student/{student_id}/{first_name}/{last_name}/{adress_id}")]
        public HttpResponseMessage Post(int student_id, string first_name, string last_name, int adress_id)
        {
            using (connection)
            {
                connection.Open();
                string queryString = "INSERT INTO Student VALUES (@Student_id, @First_name, @Last_name, @Adress_id);";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Student_id", System.Data.SqlDbType.Int).Value = student_id;
                command.Parameters.Add("@First_name", System.Data.SqlDbType.VarChar, 50).Value = first_name;
                command.Parameters.Add("@Last_name", System.Data.SqlDbType.VarChar, 50).Value = last_name;
                command.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                SqlCommand command_check = new SqlCommand
                   ("SELECT Adress_id FROM Student WHERE adress_id = @Adress_id;", connection);

                SqlCommand command_check_pk = new SqlCommand
                    ("SELECT Student_id FROM Student WHERE student_id = @Student_id;", connection);

                command_check.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;
                command_check_pk.Parameters.Add("@Student_id", System.Data.SqlDbType.Int).Value = student_id;


                SqlDataReader reader = command_check.ExecuteReader();


                if (reader.HasRows)
                {
                    connection.Close();
                    connection.Open();
                    SqlDataReader reader_pk = command_check_pk.ExecuteReader();
                    if (!reader_pk.HasRows)
                    {
                        connection.Close();
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = command;
                        adapter.InsertCommand.ExecuteNonQuery();
                        connection.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, "Student is successfully added");
                    }
                    else
                    {
                        connection.Close();
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You can not add student with already existing id.");

                    }
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You can not add student to non-existing adress.");

                }
            }

        }



        // PUT for given id modifies adress of student
        [HttpPut]
        [Route("api/Student/{student_id}")]
        public HttpResponseMessage Put(int student_id, [FromBody] int adress_id)
        {
            try
            {
                using (connection)
                {

                    connection.Open();

                    SqlCommand command_check_pk = new SqlCommand
                            ("SELECT Student_id FROM Student WHERE student_id = @Student_id;", connection);

                    command_check_pk.Parameters.Add("@Student_id", System.Data.SqlDbType.Int).Value = student_id;

                    SqlDataReader reader_pk = command_check_pk.ExecuteReader();

                    if (reader_pk.HasRows)
                    {
                        connection.Close();
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT Student_id, Adress_id FROM Student", connection);
                        adapter.UpdateCommand = new SqlCommand("UPDATE Student SET Adress_id = @Adress_id WHERE Student_id = @Student_id", connection);

                        adapter.UpdateCommand.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                        SqlParameter parameter = adapter.UpdateCommand.Parameters.Add("@Student_id", System.Data.SqlDbType.Int);
                        parameter.SourceColumn = "Student_id";
                        parameter.SourceVersion = System.Data.DataRowVersion.Original;

                        System.Data.DataTable categoryTable = new System.Data.DataTable();
                        adapter.Fill(categoryTable);

                        System.Data.DataRow categoryRow = categoryTable.Rows[1];
                        categoryRow["Adress_id"] = adress_id;



                        adapter.Update(categoryTable);

                        connection.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, "Student's adress is successfully modified");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
                    }
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
            }
        }


        // DELETE 
        [HttpDelete]
        [Route("api/Student/{student_id}")]
        public HttpResponseMessage Delete(int student_id)
        {
            using (connection)
            {
                connection.Open();

                SqlCommand command_check_pk = new SqlCommand
                        ("SELECT Student_id FROM Student WHERE student_id = @Student_id;", connection);

                command_check_pk.Parameters.Add("@Student_id", System.Data.SqlDbType.Int).Value = student_id;

                SqlDataReader reader_pk = command_check_pk.ExecuteReader();

                if (reader_pk.HasRows)
                {
                    connection.Close();
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.DeleteCommand = new SqlCommand("DELETE FROM Student WHERE Student_id = @student_id ", connection);

                    adapter.DeleteCommand.Parameters.Add("@student_id", System.Data.SqlDbType.Int).Value = student_id;

                    adapter.DeleteCommand.UpdatedRowSource = System.Data.UpdateRowSource.None;

                    adapter.DeleteCommand.ExecuteNonQuery();

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Student with id " + Convert.ToString(student_id) + " deleted.");
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with id " + Convert.ToString(student_id));
                }

            }
        }
    }
}

        