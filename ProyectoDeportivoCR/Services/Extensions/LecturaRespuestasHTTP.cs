using System.Text.Json;

namespace ProyectoDeportivoCR.Services.Extensions
{
    public static class LecturaRespuestasHTTP
    {
        public static async Task<Respuesta2Model<T>> LeerRespuesta2Model<T>(this HttpResponseMessage response)
        {
            var resultJson = await response.Content.ReadFromJsonAsync<Respuesta2Model<JsonElement>>();

            if (resultJson != null && resultJson.Exito)
            {
                T? datos = default;
                if (resultJson.Datos.ValueKind != JsonValueKind.Null && resultJson.Datos.ValueKind != JsonValueKind.Undefined)
                {
                    try
                    {
                        datos = JsonSerializer.Deserialize<T>(resultJson.Datos.ToString());
                    }
                    catch
                    {
                        datos = default;
                    }
                }

                return new Respuesta2Model<T>
                {
                    Exito = true,
                    Datos = datos,
                    Mensaje = resultJson.Mensaje
                };
            }

            return new Respuesta2Model<T>
            {
                Exito = false,
                Mensaje = resultJson?.Mensaje
            };
        }
    }
}