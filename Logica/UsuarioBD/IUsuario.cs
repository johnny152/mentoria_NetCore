using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.UsuarioBD
{
    public interface IUsuario
    {

        Task<RespuestaAPI> LoginUsuario(string CadenaConexion, ModeloUsuario ObjUsuario);

        Task<RespuestaAPI> CrearUsuario(string CadenaConexion, ModeloUsuario ObjUsuario);
        Task<RespuestaAPI> ListarUsuarios(string CadenaConexion);

    }
}
