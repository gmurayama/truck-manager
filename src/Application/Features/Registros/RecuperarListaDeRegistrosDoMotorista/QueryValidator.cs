using FluentValidation;

namespace TruckManager.Application.Features.Registros
{
    public partial class RecuperarListaDeRegistrosDoMotorista
    {
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                When(x => x.Page.HasValue, () =>
                {
                    RuleFor(x => x.Page)
                        .GreaterThan(0)
                            .WithMessage("Página deve ser um número positivo");
                });

                When(x => x.PageSize.HasValue, () =>
                {
                    RuleFor(x => x.PageSize)
                        .GreaterThan(0)
                            .WithMessage("O número de itens a serem retornados deve ser maior que zero");
                });

                RuleFor(x => x.MotoristaId)
                    .NotNull()
                        .WithMessage("Deve ser informado o ID do motorista");
            }
        }
    }
}
