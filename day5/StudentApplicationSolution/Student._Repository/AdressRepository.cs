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


namespace Student._Repository
{

    public class AdressRepository: IAdressRepository<Adress>


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");


        public async Task<List<Adress>> GetAdressesAsync()
        {

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Adress;", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Adress> list_of_adresses = new List<Adress>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Adress adress = new Adress();
                    adress.Adress_id = reader.GetInt32(0);
                    adress.Street = reader.GetString(1);
                    adress.Number = reader.GetInt32(2);
                    adress.City = reader.GetString(3);
                    adress.Zipcode = reader.GetString(4);


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


        public async Task<Adress> GetAdressByIdAsync(int adress_id)
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Adress WHERE Adress_id = @adress_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@adress_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = adress_id;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                Adress adress = new Adress();
                while (await reader.ReadAsync())
                {
                    adress.Adress_id = adress_id;
                    adress.Street = reader.GetString(1);
                    adress.Number = reader.GetInt32(2);
                    adress.City = reader.GetString(3);
                    adress.Zipcode = reader.GetString(4);


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

        public async Task<bool> CreateAdressAsync(Adress adress)        
        {
            Adress _adress = new Adress();
            _adress.Adress_id = adress.Adress_id;
            _adress.Street = adress.Street;
            _adress.Number = adress.Number;
            _adress.City = adress.City;
            _adress.Zipcode = adress.Zipcode;

            SqlCommand command = new SqlCommand("INSERT INTO Adress (Adress_id, Street , Number , City, Zipcode )" +
                                                "VALUES (@Adress_id, @Street, @Number, @City, @Zipcode)", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Adress WHERE Adress_id =@Adress_id ", connection);
            command_id.Parameters.Add("@Adress_id", SqlDbType.Int).Value = _adress.Adress_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();      //provjeravamo postoji li vec dani id

            if (reader.HasRows)
            {
                return false;
            }

            reader.Close();

            command.Parameters.Add("@Adress_id", SqlDbType.Int).Value = _adress.Adress_id;
            command.Parameters.Add("@Street", SqlDbType.VarChar).Value = _adress.Street;
            command.Parameters.Add("@Number", SqlDbType.Int).Value = _adress.Number;
            command.Parameters.Add("@City", SqlDbType.VarChar).Value = _adress.City;
            command.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = _adress.Zipcode;


            await command.ExecuteNonQueryAsync();
            connection.Close();

            return true;

        }


        public async Task<bool> UpdateAdressAsync(int adress_id, Adress adress)
        {
            Adress _adress = new Adress();
            _adress.Adress_id = adress_id;
            _adress.Street = adress.Street;
            _adress.Number = adress.Number;
            _adress.City = adress.City;
            _adress.Zipcode = adress.Zipcode;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Adress WHERE Adress_id =@Adress_id ", connection);
            command_id.Parameters.Add("@Adress_id", SqlDbType.Int).Value = adress_id;
            await connection.OpenAsync();

            SqlDataReader reader = await command_id.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Adress SET Street = @Street, Number = @Number, " +
                                                 "City= @City, Zipcode= @Zipcode WHERE Adress_id= @Adress_id", connection);

            command.Parameters.Add("Street", SqlDbType.VarChar).Value = _adress.Street;
            command.Parameters.Add("Number", SqlDbType.Int).Value = _adress.Number;
            command.Parameters.Add("City", SqlDbType.VarChar).Value = _adress.City;
            command.Parameters.Add("Zipcode", SqlDbType.VarChar).Value = _adress.Zipcode;
            command.Parameters.Add("Adress_id", SqlDbType.Int).Value = adress_id;

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteAdressAsync(int adress_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Adress WHERE Adress_id=@adress_id; ", connection);
            SqlCommand command_check = new SqlCommand("SELECT * FROM Student WHERE Adress_id = @adress_id; ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();


            commandId.Parameters.Add("@adress_id", SqlDbType.Int).Value = adress_id;
            command_check.Parameters.Add("@adress_id", SqlDbType.Int).Value = adress_id;
            
            SqlDataReader reader_check = await command_check.ExecuteReaderAsync();

            if (reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            SqlDataReader reader = await commandId.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                   
                  
                        command = new SqlCommand("DELETE FROM Adress WHERE Adress_id=@adress_id; ", connection);
                        command.Parameters.Add("@adress_id", SqlDbType.Int).Value = adress_id;
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