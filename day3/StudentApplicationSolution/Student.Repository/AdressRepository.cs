using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;

namespace Student.Repository
{

    public class AdressRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");


        public List<Adress> GetAdresses()
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
                reader.Close();
                connection.Close();
                return list_of_adresses;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public Adress GetAdressById(adress_id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Adress WHERE Adress_id = @adress_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@adress_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = adress_id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Adress adress = new Adress();
                    adress.street = reader.GetString(1);
                    adress.number = reader.GetInt32(2);
                    adress.city = reader.GetString(3);
                    adress.zipcode = reader.GetString(4);


                }
                connection.Close();
                return adress;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public void CreateAdress(Adress adress)
        {
            Adress _adress = new Adress();
            _adress.adress_id = adress.adress_id;
            _adress.street = adress.street;
            _adress.number = adress.number;
            _adress.city = adress.city;
            _adress.zipcode = adress.zipcode;

            SqlCommand command = new SqlCommand("INSERT INTO Adress (Adress_id, Street , Number , City, Zipcode )" +
                                                "VALUES ({_adress.adress_id},'{_adress.street}',{_adress.number},'{_adress.city}', '{_adress.zipcode}')", connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }


        public bool UpdateAdress(int adress_id, Adress adress)
        {
            Adress _adress = new Adress();
            _adress.adress_id = adress.adress_id;
            _adress.street = adress.street;
            _adress.number = adress.number;
            _adress.city = adress.city;
            _adress.zipcode = adress.zipcode;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Adress WHERE Adress_id ={adress_id} ", connection);
            connection.Open();

            SqlDataReader reader = command_id.ExecuteReader();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Adress SET Street = '{_adress.street}', Number={_adress.number}, " +
                                                 "City='{_adress.city}', Zipcode?'{_adress.zipcode}' WHERE Adress_id={adress_id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeleteAdress(int adress_id)
        {
            SqlCommand command_id = new SqlCommand("SELECT * FROM Adress WHERE Adress_id={adress_id} ", connection);
            SqlCommand command = new SqlCommand();
            connection.Open();

            SqlDataReader reader = command_id.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    command = new SqlCommand("DELETE FROM Adress WHERE Adress_id={adress_id}; ", connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
    }
}