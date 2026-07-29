using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public interface ITokenService
    {
        string GetTokenFromRequest();
        bool VerifyToken();

        string GetUserIdentifier();
        int GetUserId();
        int GetIdUsuarioDelegado();
        bool EsSuarioDelegado();
        List<Permission> GetPermissions();
    }
}
