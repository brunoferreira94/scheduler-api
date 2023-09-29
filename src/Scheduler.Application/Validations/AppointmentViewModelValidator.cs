using FluentValidation;
using Scheduler.Application.ViewModels;

namespace Scheduler.Application.Validations
{
    public class AppointmentViewModelValidator : AbstractValidator<AppointmentViewModel>
    {
        public AppointmentViewModelValidator()
        {
            RuleFor(appointment => appointment.ClientId)
                .NotEmpty().When(appointment => appointment.Client == null).WithMessage("É obrigatório informar o clienteId.");

            RuleFor(appointment => appointment.Client)
                .NotEmpty().When(appointment => appointment.ClientId.Equals(Guid.Empty)).WithMessage("É obrigatório informar o cliente.");

            RuleFor(appointment => appointment.ScheduledDate)
                .NotEmpty().WithMessage("É obrigatório informar a data do agendamento.")
                .GreaterThan(DateTime.Now).WithMessage("A data informada deve ser futura.");

            RuleFor(appointment => appointment.Services)
                .NotEmpty().WithMessage("É obrigatório informar algum serviço.");
        }
    }
}