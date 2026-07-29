using Application.Commands;
using Application.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public class GetFileValidator : Validator<GetFile>
    {
        public GetFileValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            RuleFor(x => x.IdArchivo).NotEmpty();
        }

        public override IList<string> RequiredPermissions => new List<string>();
    }
}
