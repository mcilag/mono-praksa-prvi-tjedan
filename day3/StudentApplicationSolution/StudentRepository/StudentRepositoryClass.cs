using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace StudentRepository
{
    public class Student
    {
        public int student_id, adress_id;
        public string first_name, last_name;
    }

    public class StudentRepositoryClass
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");
       
        public bool Get()
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


                       
                    }
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }

            }
        }
            

    }
}
