using FluentValidation;
using Truckmanager.Domain;

namespace TruckManager.Application.Features.Registros
{
    public partial class RegistrarPassagemPeloTerminal
    {
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.MotoristaId)
                    .NotEmpty()
                        .WithMessage("O ID do motorista não pode ser vazio");

                RuleFor(x => x.EstaCarregado)
                    .NotEmpty()
                        .WithMessage("Deve ser informado se o caminhão está carregado ou não");

                RuleFor(x => x.Origem)
                    .NotEmpty()
                    .SetValidator(new LocalValidator());

                RuleFor(x => x.Destino)
                    .NotEmpty()
                    .SetValidator(new LocalValidator());
            }
        }

        public class LocalValidator : AbstractValidator<Local>
        {
            public LocalValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                        .WithMessage("Nome do local não pode ser vazio");

                RuleFor(x => x.Localizacao)
                    .NotEmpty()
                        .WithMessage("A localização deve ser informada");

                When(x => x.Localizacao != null, () =>
                {
                    RuleFor(x => x.Localizacao.Type)
                        .NotEmpty()
                            .WithMessage("Tipo do local precisa ser especificado");

                    RuleFor(x => x.Localizacao.Coordinates)
                        .NotEmpty()
                            .WithMessage("Coordenadas precisam estar preenchidas")
                        .DependentRules(() =>
                            RuleFor(x => x.Localizacao.Coordinates)
                                .Must(x => x[0] >= -180 && x[0] <= 180)
                                    .WithMessage("Longitude inválida")
                                .Must(x => x[1] >= -90 && x[1] <= 90)
                                    .WithMessage("Latitude inválida")
                        );
                });
            }
        }
    }
}
