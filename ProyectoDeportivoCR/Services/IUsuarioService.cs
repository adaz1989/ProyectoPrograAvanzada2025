
using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Models;

namespace ProyectoDeportivoCR.Services
{
    public interface IUsuarioService
    {
        public Task<Respuesta2Model<UsuarioModel>> IniciarSesion(UsuarioModel model);
        public Task<Respuesta2Model<UsuarioModel>> RegistrarUsuario(UsuarioModel model);
        public Task<Respuesta2Model<UsuarioModel>> ObtenerInformacionUsuario();
        public Task<Respuesta2Model<UsuarioModel>> ActualizarInformacionUsuario(UsuarioModel model);
    }
}
