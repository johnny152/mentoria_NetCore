using Data;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.UsuarioBD
{
    public class Usuario : IUsuario
    {
        readonly RespuestaAPI rpta = new();
        public async Task<RespuestaAPI> LoginUsuario(string CadenaConexion, ModeloUsuario ObjUsuario)
        {

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new("LoginUsuario", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", ObjUsuario.UserName);
                        command.Parameters.AddWithValue("@Password", ObjUsuario.Password);
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["Codigo"].ToString() == "00")
                                {
                                    rpta.Codigo = Mensajes.CodigoExitoso;
                                    rpta.Mensaje = Mensajes.MensajeExitosoLogin;

                                }
                                else
                                {
                                    rpta.Codigo = Mensajes.CodigoError;
                                    rpta.Mensaje = Mensajes.MensajerErrorLogin;

                                }
                            }

                        }
                    }


                }
                catch (Exception ex)
                {
                    rpta.Codigo = Mensajes.CodigoError;
                    rpta.Mensaje = Mensajes.MensajeErrorGenerico + ex.Message;

                }
            }
            return rpta;
        }

         public async Task<RespuestaAPI> CrearUsuario(string CadenaConexion, ModeloUsuario ObjUsuario)
        {
            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new("CrearUsuario", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", ObjUsuario.UserName);
                        command.Parameters.AddWithValue("@Password", ObjUsuario.Password);
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["Codigo"].ToString() == "00")
                                {
                                    rpta.Codigo = Mensajes.CodigoExitoso;
                                    rpta.Mensaje = Mensajes.MensajeCreacionUsuarioOK;

                                }
                                else
                                {
                                    rpta.Codigo = Mensajes.CodigoError;
                                    rpta.Mensaje = Mensajes.MensajeCreacionUsuarioError;

                                }
                            }

                        }
                    }


                }
                catch (Exception ex)
                {
                    rpta.Codigo = Mensajes.CodigoError;
                    rpta.Mensaje = Mensajes.MensajeErrorGenerico + ex.Message;

                }
            }
            return rpta;
        }

        public async Task<RespuestaAPI> ListarUsuarios(string CadenaConexion)
        {
            List<ModeloUsuario> listUsuarios = new List<ModeloUsuario>(); 

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new("ListarUsuarios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                listUsuarios.Add(new ModeloUsuario()
                                {
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                }); 
                            }
                            if (listUsuarios.Count > 1)
                            {
                                rpta.Codigo = Mensajes.CodigoExitoso;
                                rpta.Mensaje = Mensajes.ConsultaConDatos;
                                rpta.Data = listUsuarios;

                            }
                            else {
                                rpta.Codigo = Mensajes.CodigoExitoso;
                                rpta.Mensaje = Mensajes.ConsultaConDatos;
                                rpta.Data = listUsuarios;
                            }

                        }
                    }


                }
                catch (Exception ex)
                {
                    rpta.Codigo = Mensajes.CodigoError;
                    rpta.Mensaje = Mensajes.MensajeErrorGenerico + ex.Message;

                }
            }
            return rpta;
        }

    }
}
