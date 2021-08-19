using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;


namespace AdressApplication.Controllers
{
    public class Adress
    {
        public int number, adress_id;
        public string street, city, zipcode;
    }

    public class AdressController : ApiController
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");

        List<Adress> list_of_adresses = new List<Adress>();

        // GET 
        [HttpGet]
        [Route("api/Adress")]
        public HttpResponseMessage Get()
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand
                    ("SELECT * FROM Adress;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Adress adress = new Adress();
                        adress.adress_id = reader.GetInt32(0);
                        adress.street = reader.GetString(1);
                        adress.number = reader.GetInt32(2);
                        adress.city = reader.GetString(3);
                        adress.zipcode = reader.GetString(4);


                        list_of_adresses.Add(adress);
                    }
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, list_of_adresses);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no adresses");
                }

            }
        }
        // GET by id
        [HttpGet]
        [Route("api/Adress/{Adress_id}")]
        public HttpResponseMessage Get(int Adress_id)
        {
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                    ("SELECT * FROM Adress WHERE Adress_id = @Adress_id;", connection);

                SqlParameter id_parameter = new SqlParameter("@Adress_id", System.Data.SqlDbType.Int);

                command.Parameters.Add(id_parameter).Value = Adress_id;



                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Adress adress = new Adress();
                        adress.adress_id = Adress_id;
                        adress.street = reader.GetString(1);
                        adress.number = reader.GetInt32(2);
                        adress.city = reader.GetString(3);
                        adress.zipcode = reader.GetString(4);

                        list_of_adresses.Add(adress);
                    }
                    connection.Close();

                    return Request.CreateResponse(HttpStatusCode.OK, list_of_adresses);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(Adress_id));
                }
            }
        }

        // POST 
        [HttpPost]
        [Route("api/Adress/{Adress_id}/{street}/{number}/{city}/{zipcode}")]
        public HttpResponseMessage Post(int adress_id, string street, int number, string city, string zipcode)
        {
            using (connection)
            {
                connection.Open();
                string queryString = "INSERT INTO Adress VALUES (@Adress_id, @street, @number, @city, @zipcode);";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;
                command.Parameters.Add("@street", System.Data.SqlDbType.VarChar, 50).Value = street;
                command.Parameters.Add("@number", System.Data.SqlDbType.Int).Value = number;
                command.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 50).Value = city;
                command.Parameters.Add("@zipcode", System.Data.SqlDbType.VarChar, 50).Value = zipcode;

                SqlCommand command_check = new SqlCommand
                   ("SELECT Adress_id FROM Adress WHERE adress_id = @Adress_id;", connection);

                command_check.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                SqlDataReader reader = command_check.ExecuteReader();


                if (!reader.HasRows)
                {
                    connection.Close();
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Adress is successfully added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You can not add adress with already existing id.");
                }
            }

        }

        // PUT for given id modifies city
        [HttpPut]
        [Route("api/Adress/{adress_id}")]
        public HttpResponseMessage Put(int adress_id, [FromBody] string city)
        {
            using (connection)
            {

                connection.Open();

                SqlCommand command_check_pk = new SqlCommand
                        ("SELECT Adress_id FROM Adress WHERE Adress_id = @adress_id;", connection);

                command_check_pk.Parameters.Add("@adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                SqlDataReader reader_pk = command_check_pk.ExecuteReader();

                if (reader_pk.HasRows)
                {
                    connection.Close();
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Adress_id, City FROM Adress;", connection);
                    adapter.UpdateCommand = new SqlCommand("UPDATE Adress SET City = @city WHERE Adress_id = @adress_id;", connection);

                    adapter.UpdateCommand.Parameters.Add("@city", System.Data.SqlDbType.VarChar).Value = city;
         

                    SqlParameter parameter = adapter.UpdateCommand.Parameters.Add("@adress_id", System.Data.SqlDbType.Int);
                    parameter.SourceColumn = "Adress_id";
                    parameter.SourceVersion = System.Data.DataRowVersion.Original;

                    System.Data.DataTable categoryTable = new System.Data.DataTable();
                    adapter.Fill(categoryTable);

                    System.Data.DataRow categoryRow = categoryTable.Rows[0];
                    categoryRow["City"] = city;


                    adapter.Update(categoryTable);

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Adress is successfully modified");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
                }

            }
        }
        // DELETE 
        [HttpDelete]
        [Route("api/Adress/{adress_id}")]
        public HttpResponseMessage Delete(int adress_id)
        {
            using (connection)
            {
                connection.Open();

                SqlCommand command_check_pk = new SqlCommand
                        ("SELECT Adress_id FROM Adress WHERE Adress_id = @adress_id;", connection);

                command_check_pk.Parameters.Add("@Adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                SqlDataReader reader_pk = command_check_pk.ExecuteReader();

                if (reader_pk.HasRows)
                {
                    connection.Close();
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.DeleteCommand = new SqlCommand("DELETE FROM Adress WHERE Adress_id = @adress_id ", connection);

                    adapter.DeleteCommand.Parameters.Add("@adress_id", System.Data.SqlDbType.Int).Value = adress_id;

                    adapter.DeleteCommand.UpdatedRowSource = System.Data.UpdateRowSource.None;

                    adapter.DeleteCommand.ExecuteNonQuery();

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Adress with id " + Convert.ToString(adress_id) + " deleted.");
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no adress with id " + Convert.ToString(adress_id));
                }

            }
        }
    }
}
