
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoApi.Services
{

    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly SymmetricSecurityKey _key;
        private readonly SigningCredentials _credentials;
        private readonly int _tokenExpiration = 20;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("Variables:llaveToken").Value!;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
        }

        public string GenerarToken(long usuarioId, string tipoUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim("UsuarioId", usuarioId.ToString()),
                new Claim("TipoUsuario", tipoUsuario.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenExpiration),
                signingCredentials: _credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public long ObtenerUsuarioJwt(IEnumerable<Claim> valores)
        {
            if (valores.Any())
            {
                var UsuarioId = valores.FirstOrDefault(x => x.Type == "UsuarioId")?.Value;
                return long.Parse(UsuarioId!);
            }
            return 0;
        } 
        
        public bool ValidarUsuarioAdmin(IEnumerable<Claim> valores)
        {
            var tipoUsuario = valores.FirstOrDefault(x => x.Type == "TipoUsuario")?.Value;
            return tipoUsuario == "Administrador";
        }
    }
}