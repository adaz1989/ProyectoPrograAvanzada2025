using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace ProyectoDeportivoCR.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IEncriptacionService _encriptacion;
        private readonly IUsuarioRepositorie _usuarioRepositorie;

        public UsuarioService(IEncriptacionService encriptacion, IUsuarioRepositorie usuarioRepositorie) 
        {
            _encriptacion = encriptacion;
            _usuarioRepositorie = usuarioRepositorie;
        }

        public async Task<Respuesta2Model<UsuarioModel>> IniciarSesion(UsuarioModel model)
        {
            // *** cuando registro este listo se puede descomentar ***
            //model.Contrasenna = _encriptacion.Encriptar(model.Contrasenna!);

            var response = await _usuarioRepositorie.IniciarSesion(model);

            if (response.IsSuccessStatusCode)
            {
                // Primero, deserializamos como objeto base
                var resultJson = await response.Content.ReadFromJsonAsync<Respuesta2Model<JsonElement>>();

                if (resultJson != null && resultJson.Exito)
                {
                    // Ahora sí, solo deserializamos los datos si Exito es true
                    var datos = JsonSerializer.Deserialize<UsuarioModel>(resultJson.Datos!.ToString()!);
                    return new Respuesta2Model<UsuarioModel>
                    {
                        Exito = true,
                        Datos = datos,
                        Mensaje = resultJson.Mensaje
                    };
                }

                // Exito == false, devolvemos solo mensaje
                return new Respuesta2Model<UsuarioModel>
                {
                    Exito = false,
                    Mensaje = resultJson?.Mensaje
                };
            }

            // Si la API no respondió bien
            return new Respuesta2Model<UsuarioModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }


        //public async Task<bool> RegistrarUsuario(UsuarioModel model)
        //{
        //    model.Contrasenna = _encriptacion.Encriptar(model.Contrasenna!);

        //    return await _usuarioRepositorie.RegistrarUsuario(model);
        //}
    }
}
