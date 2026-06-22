using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Service
{
    public interface ITokenService
    {
        string TraerTokenDeRequest();
        bool VerificarToken();

        string GetIdentificacionUsuario();
        int GetIdUsuario();
        int GetIdUsuarioDelegado();
        bool EsSuarioDelegado();
        List<Permiso> TraerPermisos();
    }
}
