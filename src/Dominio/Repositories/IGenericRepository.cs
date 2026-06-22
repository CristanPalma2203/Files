
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Repositories
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

        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, ISpecification<TEntity> busqueda);
        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters);

    }
}
