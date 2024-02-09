using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace apiexamen
{
    public class Examen
    {
        public int IdExamen { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
    }

    public static class ClsExamen
    {
        private static SqlConnection _connection;
        public static readonly List<string> Errors = new List<string>();

        private static void InitConnection()
        {
            string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=BdiExamen;Integrated Security=True;TrustServerCertificate=True";
            _connection = new SqlConnection(ConnectionString);
        }

        public enum DAL_PROVIDER_ENUM
        {
            STORED_PROCEDURES = 0,
            WEB_SERVICE = 1
        }

        private static DAL_PROVIDER_ENUM DalProvider = DAL_PROVIDER_ENUM.STORED_PROCEDURES;

        public static void SetDalProvider(DAL_PROVIDER_ENUM provider)
        {
            DalProvider = provider;
        }

        private static bool AgregarExamenWithStoredProcedures(int Id, string Nombre, string Descripcion)
        {
            InitConnection();

            _connection.Open();
            SqlTransaction sqlTran = _connection.BeginTransaction();

            SqlCommand command = new SqlCommand("spAgregar", _connection, sqlTran);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
            command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                da.Fill(ds, "result_name");

                DataTable dt = ds.Tables["result_name"];

                int returnCode = int.Parse(dt.Rows[0][0].ToString());

                if (returnCode > 0)
                {
                    Errors.Add(dt.Rows[0][1].ToString());
                }
            }

            command.Transaction.Commit();
            _connection.Close();
            
            return true;
        }

        private static bool AgregarExamenWithWebService(int Id, string Nombre, string Descripcion)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7092/Examen/AgregarExamen");

            var request = new { IdExamen = Id, Nombre = Nombre, Descripcion = Descripcion }; 

            HttpContent content = new StringContent(JsonConvert.SerializeObject(request), UnicodeEncoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PutAsync("", content).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result= response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0}", result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Errors.Add("Error al consultar el examen");
            }


            return true;
        }

        private static IEnumerable<Examen> ConsultarExamenWithStoredProcedures(string Id, string Nombre, string Descripcion)
        {
            InitConnection();

            _connection.Open();
            SqlTransaction sqlTran = _connection.BeginTransaction();

            SqlCommand command = new SqlCommand("spConsultar", _connection, sqlTran);

            command.CommandType = CommandType.StoredProcedure;

            if (Id.IsNullOrEmpty()) command.Parameters.Add("@Id", SqlDbType.VarChar).Value = DBNull.Value;
            else command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Id;
            if (Nombre.IsNullOrEmpty()) command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = DBNull.Value;
            else command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
            if (Descripcion.IsNullOrEmpty()) command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = DBNull.Value;
            else command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;

            List<Examen> examenes = new List<Examen> { };

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                da.Fill(ds, "result_name");

                DataTable dt = ds.Tables["result_name"];

                bool hasReturnCode = dt.Columns[0].ColumnName == "ReturnCode";
                int returnCode = int.Parse(dt.Rows[0][0].ToString());

                if (hasReturnCode && returnCode > 0)
                {
                    Errors.Add(dt.Rows[0][1].ToString());
                }

                foreach (DataRow row in dt.Rows)
                {
                    Examen examen= new Examen
                    {
                        IdExamen = (int)row[0],
                        Nombre = (string)row[1],
                        Descripcion = (string)row[2]
                    };

                    examenes.Add(examen);
                }
            }

            command.Transaction.Commit();
            _connection.Close();

            return examenes;
        }

        private static IEnumerable<Examen> ConsultarExamenWithWebService(string Id, string Nombre, string Descripcion)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7092/Examen/ConsultarExamen");

            var queryParams = $"?Id={Id}&Nombre=\"{Nombre}\"&Descripcion=\"{Descripcion}\"";

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(queryParams).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Examen> examenes = JsonConvert.DeserializeObject<IEnumerable<Examen>> (result.ToString());

                return examenes;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Errors.Add("Error al consultar el examen");
            }


            return new List<Examen>{ };
        }

        private static bool ActualizarExamenWithStoredProcedures(int Id, string Nombre, string Descripcion)
        {
            InitConnection();

            _connection.Open();
            SqlTransaction sqlTran = _connection.BeginTransaction();

            SqlCommand command = new SqlCommand("spActualizar", _connection, sqlTran);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
            command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
            
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                da.Fill(ds, "result_name");

                DataTable dt = ds.Tables["result_name"];

                int returnCode = int.Parse(dt.Rows[0][0].ToString());

                if (returnCode > 0)
                {
                    Errors.Add(dt.Rows[0][1].ToString());
                }
            }

            command.Transaction.Commit();
            _connection.Close();

            return true;
        }

        private static bool ActualizarExamenWithWebService(int Id, string Nombre, string Descripcion)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7092/Examen/ActualizarExamen");

            var request = new { IdExamen = Id, Nombre = Nombre, Descripcion = Descripcion };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(request), UnicodeEncoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PutAsync("", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0}", result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Errors.Add("Error al actualizar el examen");
            }


            return true;
        }

        private static bool EliminarExamenWithStoredProcedures(int Id)
        {
            InitConnection();

            _connection.Open();
            SqlTransaction sqlTran = _connection.BeginTransaction();

            SqlCommand command = new SqlCommand("spEliminar", _connection, sqlTran);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
            
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                da.Fill(ds, "result_name");

                DataTable dt = ds.Tables["result_name"];

                int returnCode = int.Parse(dt.Rows[0][0].ToString());

                if (returnCode > 0)
                {
                    Errors.Add(dt.Rows[0][1].ToString());
                }
            }

            command.Transaction.Commit();
            _connection.Close();

            return true;
        }

        private static bool EliminarExamenWithWebService(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7092/Examen/EliminarExamen");

            var request = new { IdExamen = Id };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync($"?Id={Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0}", result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Errors.Add("Error al eliminar el examen");
            }


            return true;
        }

        public static bool AgregarExamen(int Id, string Nombre, string Descripcion)
        {
            switch (DalProvider)
            {
                case DAL_PROVIDER_ENUM.STORED_PROCEDURES: return AgregarExamenWithStoredProcedures(Id, Nombre, Descripcion);
                case DAL_PROVIDER_ENUM.WEB_SERVICE: return AgregarExamenWithWebService(Id, Nombre, Descripcion);
                default: return false;
            }
        }

        public static bool ActualizarExamen(int Id, string Nombre, string Descripcion)
        {
            switch (DalProvider)
            {
                case DAL_PROVIDER_ENUM.STORED_PROCEDURES: return ActualizarExamenWithStoredProcedures(Id, Nombre, Descripcion);
                case DAL_PROVIDER_ENUM.WEB_SERVICE: return ActualizarExamenWithWebService(Id, Nombre, Descripcion);
                default: return false;
            }
        }

        public static IEnumerable<Examen> ConsultarExamen(string Id, string Nombre, string Descripcion)
        {
            switch (DalProvider)
            {
                case DAL_PROVIDER_ENUM.STORED_PROCEDURES: return ConsultarExamenWithStoredProcedures(Id, Nombre, Descripcion);
                case DAL_PROVIDER_ENUM.WEB_SERVICE: return ConsultarExamenWithWebService(Id, Nombre, Descripcion);
                default: return null;
            }
        }

        public static bool EliminarExamen(int Id)
        {
            switch (DalProvider)
            {
                case DAL_PROVIDER_ENUM.STORED_PROCEDURES: return EliminarExamenWithStoredProcedures(Id);
                case DAL_PROVIDER_ENUM.WEB_SERVICE: return EliminarExamenWithWebService(Id);
                default: return false;
            }
        }
    }
}
