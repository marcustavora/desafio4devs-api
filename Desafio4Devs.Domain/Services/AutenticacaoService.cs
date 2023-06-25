using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Dto.Usuarios;
using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Enums;
using Desafio4Devs.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Desafio4Devs.Domain.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AutenticacaoService(UserManager<AppIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Autenticar(string username, string senha)
        {
            var userIdentity = await _userManager.FindByNameAsync(username);

            if (userIdentity != null && await _userManager.CheckPasswordAsync(userIdentity, senha))
            {
                var usuarioNiveis = await _userManager.GetRolesAsync(userIdentity);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userIdentity.Email),
                    new Claim(ClaimTypes.Name, userIdentity.UserName),
                    new Claim(ClaimTypes.GivenName, userIdentity.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var usuarioNivel in usuarioNiveis)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, usuarioNivel));
                }

                var accessToken = GenerateAccessToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                userIdentity.RefreshToken = refreshToken;
                userIdentity.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                await _userManager.UpdateAsync(userIdentity);

                return new AuthResponseDto(accessToken, refreshToken);
            }

            return null;
        }

        public async Task<ResponseDto<bool>> CriarUsuario(UsuarioDto usuario)
        {
            var usuarioExistente = await _userManager.FindByNameAsync(usuario.Username);

            if (usuarioExistente != null)
                return new ResponseDto<bool>(StatusResponse.Erro, "Usuário já cadastrado!", false);

            var usuarioIdentity = new AppIdentityUser
            {
                Email = usuario.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = usuario.Username,
                Name = usuario.Nome,
            };

            var result = await _userManager.CreateAsync(usuarioIdentity, usuario.Senha);

            if (!result.Succeeded)
                return new ResponseDto<bool>(StatusResponse.Erro, "Falha na criação do usuário!", false);


            if (usuario.Nivel == "Admin")
                await AssociarNivelAdmin(usuarioIdentity);

            if (usuario.Nivel == "Operador")
                await AssociarNivelOperador(usuarioIdentity);

            return new ResponseDto<bool>(StatusResponse.Sucesso, "Usuário criado com sucesso!", true);
        }

        public async Task<AuthResponseDto> AtualizarToken(TokenDto token)
        {
            var principal = GetPrincipalFromExpiredToken(token.AccessToken);

            var username = principal.Identity.Name;

            var userIdentity = await _userManager.FindByNameAsync(username);

            if (userIdentity is null || userIdentity.RefreshToken != token.RefreshToken || userIdentity.RefreshTokenExpiryTime <= DateTime.Now)
                return null;

            var novoAccessToken = GenerateAccessToken(principal.Claims.ToList());
            var novoRefreshToken = GenerateRefreshToken();

            userIdentity.RefreshToken = novoRefreshToken;
            await _userManager.UpdateAsync(userIdentity);

            return new AuthResponseDto(novoAccessToken, novoRefreshToken);
        }

        public async Task<bool> RevogarToken(string username)
        {
            var userIdentity = await _userManager.FindByNameAsync(username);

            if (userIdentity == null)
                return false;

            userIdentity.RefreshToken = null;
            await _userManager.UpdateAsync(userIdentity);

            return true;
        }

        private async Task AssociarNivelAdmin(AppIdentityUser usuarioIdentity)
        {
            if (!await _roleManager.RoleExistsAsync(UsuarioNivelDto.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UsuarioNivelDto.Admin));

            await _userManager.AddToRoleAsync(usuarioIdentity, UsuarioNivelDto.Admin);
        }

        private async Task AssociarNivelOperador(AppIdentityUser usuarioIdentity)
        {
            if (!await _roleManager.RoleExistsAsync(UsuarioNivelDto.Operador))
                await _roleManager.CreateAsync(new IdentityRole(UsuarioNivelDto.Operador));

            await _userManager.AddToRoleAsync(usuarioIdentity, UsuarioNivelDto.Operador);
        }

        private string GenerateAccessToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signinCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: signinCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateLifetime = false //here we are saying that we dont care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token inválido");

            return principal;
        }
    }
}