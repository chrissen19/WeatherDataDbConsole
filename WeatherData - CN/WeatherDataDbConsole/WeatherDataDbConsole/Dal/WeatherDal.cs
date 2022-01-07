using ConsoleApp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ConsoleApp.DAL
{
    public class WeatherDAL
    {
        private string _connectionString;
        public WeatherDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
        }
        public List<WeatherModel> GetList()
        {
            var listweatherModel = new List<WeatherModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_WEATHER_GET_LIST", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listweatherModel.Add(new WeatherModel
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Datum = Convert.ToDateTime(rdr[1]),
                            Plats = rdr[2].ToString(),
                            Temperatur = Convert.ToDecimal(rdr[3]),
                            Luftfuktighet = Convert.ToInt32(rdr[4])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listweatherModel;
        }
    }
}