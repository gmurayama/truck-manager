using FluentValidation;
using System;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class ListarQuantidadeDeCaminhoesCarregados
    {
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.DataInicial)
                    .GreaterThanOrEqualTo(new DateTime(1753, 1, 1)) // SQL Server DateTime MinValue
                        .WithMessage("Necessário especificar uma data válida maior que 1 de Janeiro de 1753");

                RuleFor(x => x.DataFinal)
                   .GreaterThanOrEqualTo(new DateTime(1753, 1, 1)) // SQL Server DateTime MinValue
                       .WithMessage("Necessário especificar uma data válida maior que 1 de Janeiro de 1753")
                   .GreaterThan(x => x.DataInicial)
                       .WithMessage("Informar uma data final maior que a data inicial");
            }
        }
    }
}
