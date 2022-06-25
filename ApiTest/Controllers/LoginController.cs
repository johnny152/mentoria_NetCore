using Data;
using Helpers;
using Logica.UsuarioBD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly string CadenaConexion;
        private readonly IUsuario IUsuario;
        RespuestaAPI rpta = new();


        public LoginController(IUsuario _IUsuario) {
            this.CadenaConexion = Environment.GetEnvironmentVariable("Conexion");
            IUsuario = _IUsuario;
        }

        [HttpPost]
        [Route("LoginUsuario")]
        public async Task<ActionResult> LoginUsuario(ModeloUsuario ObjUsuario)
        {
            try
            {
                rpta = await IUsuario.LoginUsuario(CadenaConexion, ObjUsuario);

                return Ok(rpta);
            }
            catch (Exception ex) {

                rpta.Codigo = Mensajes.CodigoError;
                rpta.Mensaje = Mensajes.MensajeErrorGenerico + ex.Message;

                return BadRequest(rpta);
            }
        }
        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario(ModeloUsuario ObjUsuario)
        {
            try
            {
                rpta = await IUsuario.CrearUsuario(CadenaConexion, ObjUsuario);

                return Ok(rpta);

            }
            catch (Exception ex) {
                rpta.Codigo = Mensajes.CodigoError;
                rpta.Mensaje = Mensajes.MensajeErrorGenerico + ex.Message;

                return BadRequest(rpta);
            }
        }
        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<ActionResult> ConsultasUsuario() {

            try
            {
                rpta = await IUsuario.ListarUsuarios(CadenaConexion);

                return Ok(rpta);

            }
            catch (Exception ex) {

                return BadRequest();
            }
        }

    }
}
