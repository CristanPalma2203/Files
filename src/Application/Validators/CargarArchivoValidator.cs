using Application.Commands;
using Application.Services.Validaciones;
using Domain.Models.Rules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Validators
{
    public class UploadFileValidator: Validator<UploadFile>
    {
        public UploadFileValidator(IAutenticationHelper autenticationHelper, IExtensionesPermitidas extensiones) : base(autenticationHelper) {
            RuleFor(x => x.File).NotNull().Must(c => extensiones.ExtensionValida(c.FileName)).WithMessage("StoredFile no permitido, Solo se permiten archivos tipo PDF o imágenes.");
            RuleFor(c => c.File.Length).LessThan(5242880).WithMessage("Error. StoredFiles supera el limite de tamanio de 5 MB");  
        }

        public override IList<string> RequiredPermissions => new List<string>();
    }
}
