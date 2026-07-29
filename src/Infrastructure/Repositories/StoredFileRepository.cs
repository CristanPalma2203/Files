using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class StoredFileRepository: GenericRepository<StoredFile>, IStoredFileRepository
    {
        private readonly AutenticationContext dbContext;

        public StoredFileRepository(AutenticationContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
