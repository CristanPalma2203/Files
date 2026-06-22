using Dominio.Models;
using Dominio.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Infraestructura.Service
{
    public class TokenService : ITokenService
    {
        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenService(IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetIdentificacionUsuario()
        {
            var clain = GetToken().Claims.Where(c => c.Type == "email").FirstOrDefault();
            return clain.Value;
        }

        public List<Permiso> TraerPermisos()
        {
            var cahe = cache.Get(GetIdCache());
            if (cahe != null) return GetObj(cahe).Permisos;
            return new List<Permiso>();
        }

        public bool VerificarToken()
        {
            var objArray = cache.Get(this.GetIdCache());
            if (objArray != null)
                return GetObj(objArray).Token.Equals(TraerTokenDeRequest());
            return false;
        }

        private CacheObj GetObj(byte[] cahe)
        {
            var json = Encoding.Default.GetString(cahe);
            var obj = JsonSerializer.Deserialize<CacheObj>(json);
            return obj;
        }

        private JwtSecurityToken GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(TraerTokenDeRequest());
        }

        public string TraerTokenDeRequest()
        {
            var tokens = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            return tokens.Replace("Bearer ", "");
        }

        public int GetIdUsuario()
        {
            var clain = GetToken().Claims.Where(c => c.Type == "nameid").FirstOrDefault();
            return int.Parse(clain.Value);
        }

        public int GetIdUsuarioDelegado()
        {
            var clain = GetToken().Claims.Where(c => c.Type == "family_name").FirstOrDefault();
            if (clain.Value.Length > 0)
            {
                return int.Parse(clain.Value);
            }
            return 0;
        }

        private string GetIdCache()
        {
            var tokeLeido = GetToken();
            var id = tokeLeido.Claims.Where(c => c.Type == "nameid").FirstOrDefault().Value;
            var idDelegado = tokeLeido.Claims.Where(c => c.Type == "family_name").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(idDelegado.Value)) id = id + "-" + idDelegado.Value;
            return id;
        }

        public bool EsSuarioDelegado()
        {
            var clain = GetIdUsuarioDelegado();
            return clain > 0;
        }
    }
}

namespace Infraestructura.Service
{
    public class CacheObj
    {
        public string Token { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}
