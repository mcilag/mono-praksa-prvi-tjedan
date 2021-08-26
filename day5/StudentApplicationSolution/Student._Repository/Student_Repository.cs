using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Student.Model;
using System.Threading.Tasks;
using StudentRepository.Common;
using StudentApplication.Common;

namespace Student._Repository
{

    public class Student_Repository: IStudentRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");


        public async Task<List<Students>> GetStudentsAsync(Sorter sorter=null, Pager pager=null, StudentFilter studentFilter=null)
        {

           
            Sorter Sorting = new Sorter();
            string sort = Sorting.SortBy(sorter.Order_by, sorter.Sort_Order);

            Pager Paging = new Pager();
            string page = Paging.Page(pager.Page_number, pager.Page_size);

            StudentFilter Filter = new StudentFilter();
            string filter = Filter.FilterLike(studentFilter.Filter);
            

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student " + filter + sort + page + " ;", connection);
            await connection.OpenAsync();

            
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Students> list_of_students = new List<Students>();
            
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Students student = new Students();
                    student.Student_id = reader.GetInt32(0);
                    student.First_name = reader.GetString(1);
                    student.Last_name = reader.GetString(2);
                    student.Adress_id = reader.GetInt32(3);


                    list_of_students.Add(student);
                }
                reader.Close();
                connection.Close();
                return list_of_students;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public async Task<Students> GetStudentByIdAsync(int Student_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student WHERE Student_id = @Student_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Student_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Student_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                Students student = new Students();
                while (await reader.ReadAsync())
                {
                    
                    student.Student_id = Student_id;
                    student.First_name = reader.GetString(1);
                    student.Last_name = reader.GetString(2);
                    student.Adress_id = reader.GetInt32(3);


                }
                connection.Close();
                return student;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public async Task<bool> CreateStudentAsync(Students student)          
        {

            Students _student = new Students();
            _student.Student_id = student.Student_id;
            _student.First_name = student.First_name;
            _student.Last_name = student.Last_name;
            _student.Adress_id = student.Adress_id;

            SqlCommand command = new SqlCommand("INSERT INTO Student (Student_id, First_name , Last_name , Adress_id )" +
                                                "VALUES (@Student_id, @First_name, @Last_name, @Adress_id); ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id =@Student_id ", connection);
            command_id.Parameters.Add("Student_id", SqlDbType.Int).Value = _student.Student_id;


            SqlCommand command_check = new SqlCommand("SELECT * FROM Student WHERE Adress_id = @adress_id; ", connection);
            command_check.Parameters.Add("@adress_id", SqlDbType.Int).Value = _student.Adress_id;

            await connection.OpenAsync();

            SqlDataReader reader_check_unique = await command_id.ExecuteReaderAsync();    //provjerava postoji li vec student s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            SqlDataReader reader_check = await command_check.ExecuteReaderAsync();    //provjerava je li adresa postojeca, inace ju ne moze dodati

            if (!reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            command.Parameters.Add("@Student_id", SqlDbType.Int).Value = _student.Student_id;
            command.Parameters.Add("@First_name", SqlDbType.VarChar).Value = _student.First_name;
            command.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = _student.Last_name;
            command.Parameters.Add("@Adress_id", SqlDbType.Int).Value = _student.Adress_id;

          
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }


        public async Task<bool> UpdateStudentAsync(int student_id, Students student)     
        {
            Students _student = new Students();
            _student.Student_id = student_id;
            _student.First_name = student.First_name;
            _student.Last_name = student.Last_name;
            _student.Adress_id = student.Adress_id;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id =@Student_id ", connection);
            command_id.Parameters.Add("Student_id", SqlDbType.Int).Value = student_id;

            SqlCommand command_check = new SqlCommand("SELECT * FROM Student WHERE Adress_id = @adress_id; ", connection);
            command_check.Parameters.Add("@adress_id", SqlDbType.Int).Value = _student.Adress_id;

            await connection.OpenAsync();

            SqlDataReader reader_check = await command_check.ExecuteReaderAsync();        //provjerava je li adresa postojeca, inace ju ne moze promijeniti

            if (!reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Student SET First_name = @First_name, Last_name=@Last_name, " +
                                                 "Adress_id=@Adress_id WHERE Student_id=@Student_id", connection);

            command.Parameters.Add("First_name", SqlDbType.VarChar).Value = _student.First_name;
            command.Parameters.Add("Last_name", SqlDbType.VarChar).Value = _student.Last_name;
            command.Parameters.Add("Adress_id", SqlDbType.Int).Value = _student.Adress_id;
            command.Parameters.Add("Student_id", SqlDbType.Int).Value = student_id;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int student_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Student WHERE Student_id=@student_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            commandId.Parameters.Add("@student_id", SqlDbType.Int).Value = student_id;
            SqlDataReader reader = await commandId.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand("DELETE FROM Student WHERE Student_id=@student_id; ", connection);
                    command.Parameters.Add("@student_id", SqlDbType.Int).Value = student_id;
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }
    }
}