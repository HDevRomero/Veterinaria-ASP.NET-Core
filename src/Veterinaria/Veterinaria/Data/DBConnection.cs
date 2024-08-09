using System.Data;
using System.Data.SqlClient;


namespace Veterinaria.Data
{
    public class DBConnection
    {
        private string? ConnStringSQL;
        protected SqlConnection? Conn;
        protected SqlCommand? Cmd;
        protected string? Query;

        protected SqlConnection Open()
        {
            try
            {
                // Construir la configuración y obtener la cadena de conexión desde appsettings.json
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                ConnStringSQL = builder.GetSection("ConnectionStrings:SqlConnectString").Value;

                // Verifica si la cadena de conexión SQL es nula
                if (ConnStringSQL == null)
                    throw new Exception("No se encontró la cadena de conexión en el archivo de configuración.");

                // Crea una nueva conexión SQL y luego la abre
                Conn = new SqlConnection(ConnStringSQL);
                Conn.Open();

                return Conn;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error al conectar: {ex.Message}");
            }
        }
        protected void Close()
        {
            try
            {
                //verifica si la conexión no es nula y si está abierta
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            catch(Exception ex)
            {
                throw new Exception("Error al intentar cerrar la conexión con la base de datos", ex);
            }
        }
    }
}
