using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    public class ConsultarArchivoValidator : Validador<ConsultarArchivo>
    {
        public ConsultarArchivoValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            RuleFor(x => x.IdArchivo).NotEmpty();
        }

        public override IList<string> Permisos => new List<string>();
    }
}
