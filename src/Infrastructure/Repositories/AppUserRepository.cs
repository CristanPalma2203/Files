using Domain.Specifications;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Extenciones;
using Domain.Repositories.Extensiones;
using Infrastructure.Data;
using Infrastructure.Repositories.Extenciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AutenticationContext dbContext) : base(dbContext)
        {
        }
    }

}
