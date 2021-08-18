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
}
 }