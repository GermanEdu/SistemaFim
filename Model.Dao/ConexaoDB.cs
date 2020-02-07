using System.Data.SqlClient;

namespace Model.Dao
{
    public class ConexaoDB
    {
        private static ConexaoDB objConexaoDB = null;
        private SqlConnection con;

        private ConexaoDB()
        {
            // conexão local casa
            //  con = new SqlConnection("Data Source=DESKTOP-HRK38ER; Initial Catalog=SistemaCamada; Integrated Security=True");
            
            // trabalho local
             con = new SqlConnection("Data Source=C-E0803\\SQLEXPRESS; Initial Catalog=SistemaFim; Integrated Security=True");
        }

        public static ConexaoDB saberEstado()
        {
            if (objConexaoDB == null)
            {
                objConexaoDB = new ConexaoDB();
            }

            return objConexaoDB;
        }


        public SqlConnection getCon()
        {
            return con;
        }

        public void CloseDB()
        {
            objConexaoDB = null;
        }
    }
}
