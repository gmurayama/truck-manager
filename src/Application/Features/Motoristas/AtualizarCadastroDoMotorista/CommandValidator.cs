using FluentValidation;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class AtualizarCadastroDoMotorista
    {
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.MotoristaId)
                    .NotEmpty()
                        .WithMessage("MotoristaId não pode ser vazio ou nulo");

                RuleFor(x => x.Idade)
                    .GreaterThanOrEqualTo(18)
                        .WithMessage("Idade precisa ser igual ou maior que 18");

                RuleFor(x => x.Sexo)
                    .NotEmpty()
                        .WithMessage("Sexo deve ser preenchido");

                RuleFor(x => x.TipoCnh)
                    .NotEmpty()
                        .WithMessage("O tipo de CNH não pode ser vazio ou nulo");

                RuleFor(x => x.PossuiVeiculoProprio)
                    .NotEmpty()
                        .WithMessage("Deve ser informado se o motorista possuí veículo próprio ou não");
            }
        }
    }
}
