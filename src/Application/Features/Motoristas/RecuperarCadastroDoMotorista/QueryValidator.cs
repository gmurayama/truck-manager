using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class RecuperarCadastroDoMotorista
    {
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Cpf)
                    .NotEmpty()
                        .WithMessage("CPF deve ser preenchido");
            }
        }
    }
}
