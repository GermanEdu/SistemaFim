using Model.Entity;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Model.Dao
{
   public class ClienteDao : Obrigatorio<Cliente>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;

        public ClienteDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }


        public void create(Cliente objCliente)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            try
            {
                conexao = new SqlConnection(Convert.ToString(Convert.ToString(objConexaoDB.getCon().ConnectionString)));
                cmd = new SqlCommand("sp_cliente_adc", conexao);
                cmd.Parameters.AddWithValue("@nome", objCliente.Nome);
                cmd.Parameters.AddWithValue("@endereco", objCliente.Endereco);
                cmd.Parameters.AddWithValue("@telefone", objCliente.Telefone);
                cmd.Parameters.AddWithValue("@cpf", objCliente.Cpf);

                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void delete(Cliente objCliente)
        {
            string delete = "delete from cliente where idCliente = '" + objCliente.IdCliente +"' ";
            try
            {
                comando = new SqlCommand(delete, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                objCliente.Estado = 1;

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void update(Cliente objCliente)
        {
            string update = "update cliente set nome='" + objCliente.Nome + "', endereco ='" + objCliente.Endereco + "', telefone='" + objCliente.Telefone + "', cpf='" + objCliente.Cpf + "' where idCliente='" + objCliente.IdCliente + "'";
            try
            {
                comando = new SqlCommand(update, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                objCliente.Estado = 1;

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public bool find(Cliente objCliente)
        {
            bool temRegistros;
            string find = "select * from cliente where idCliente  = '" + objCliente.IdCliente + "' "; 
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    objCliente.Nome = reader[1].ToString();
                    objCliente.Endereco = reader[2].ToString();
                    objCliente.Telefone = reader[3].ToString();
                    objCliente.Cpf = reader[4].ToString();
                    objCliente.Estado = 99;

                }else
                {
                    objCliente.Estado = 1;
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return temRegistros;
        }




        // foi tracado por uma procedure de select * from cliente "STP_SEL_ALL_CLIENTE" date 2020-02-06 



        public List<Cliente> findAll()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string findAll = "STP_SEL_ALL_CLIENTE";

            try
            {
                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.IdCliente = Convert.ToInt64(reader[0].ToString());
                    objCliente.Nome = reader[1].ToString();
                    objCliente.Endereco = reader[2].ToString();
                    objCliente.Telefone = reader[3].ToString();
                    objCliente.Cpf = reader[4].ToString();
                    listaClientes.Add(objCliente);
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaClientes;
        }





        //OUTRAS IMPLEMENTAÇÕES

        public bool findClientePorcpf(Cliente objCliente)
        {
            bool temRegistros;
            string find = "select*from cliente where cpf='" + objCliente.Cpf + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                
                SqlDataReader reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    objCliente.Nome = reader[1].ToString();
                    objCliente.Endereco = reader[2].ToString();
                    objCliente.Telefone = reader[3].ToString();
                    objCliente.Cpf = reader[4].ToString();

                    objCliente.Estado = 99;

                }
                else
                {
                    objCliente.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
            return temRegistros;
        }

        public List<Cliente> findAllCliente(Cliente objCLiente)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string findAll = "select* from cliente where nome like '%" + objCLiente.Nome + "%' or cpf like '%" + objCLiente.Cpf + "%' or idCliente like '%" + objCLiente.IdCliente + "%' ";
            try
            {
               
                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.IdCliente = Convert.ToInt64(reader[0].ToString());
                    objCliente.Nome = reader[1].ToString();
                   
                    objCliente.Endereco = reader[2].ToString();
                    objCliente.Telefone = reader[3].ToString();
                    objCliente.Cpf = reader[4].ToString();
                    listaClientes.Add(objCliente);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaClientes;

        }
    }
}
