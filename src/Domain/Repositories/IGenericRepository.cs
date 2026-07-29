
using Domain.Specifications;
using Domain.Models;
using Domain.Repositories.Extenciones;
using Domain.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Create(TEntity entity);
        void SaveAll(IList<TEntity> entity);
        TEntity Update(int id, TEntity entity);
        TEntity Delete(int id);
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> Filter(ISpecification<TEntity> especificaciones);

        IPagina<TEntity> GetPaged(IConsulta ownerParameters, ISpecification<TEntity> busqueda);
        IPagina<TEntity> GetPaged(IConsulta ownerParameters);

    }
}
