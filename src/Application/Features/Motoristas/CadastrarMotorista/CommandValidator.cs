using FluentValidation;
using Truckmanager.Domain;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class CadastrarMotorista
    {
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                        .WithMessage("Nome não pode ser vazio");

                RuleFor(x => x.Idade)
                    .GreaterThanOrEqualTo(18)
                        .WithMessage("Idade precisa ser igual ou maior que 18");

                RuleFor(x => x.Cpf)
                    .NotEmpty()
                        .WithMessage("CPF deve ser preenchido");

                RuleFor(x => x.Origem)
                    .NotEmpty()
                    .SetValidator(new LocalValidator());

                RuleFor(x => x.Destino)
                    .NotEmpty()
                    .SetValidator(new LocalValidator());

                RuleFor(x => x.PossuiVeiculoProprio)
                    .NotEmpty()
                        .WithMessage("Deve ser informado se o motorista possuí veículo próprio ou não");
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
