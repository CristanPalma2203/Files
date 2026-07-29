using Application.Commands;
using Application.Services.Validaciones;
using Domain.Models.Rules;
using FluentValidation;
using System.Collections.Generic;

namespace Application.Validators
{
    public class UploadRegistrationFileValidator : Validator<UploadRegistrationFile>
    {
        public UploadRegistrationFileValidator(IAutenticationHelper autenticationHelper, IExtensionesPermitidas extensiones) : base(autenticationHelper)
        {
            RuleFor(x => x.File).NotNull().Must(c => extensiones.ExtensionValida(c.FileName)).WithMessage("StoredFile No permistido, Solamente, PDF e imagenes");
            RuleFor(c => c.File.Length).LessThan(5242880).WithMessage("Error. StoredFiles supera el limite de tamanio de 5 MB");
        }

        public override IList<string> RequiredPermissions => new List<string>();
    }
}
