using FluentValidation;
using Scheduler.Application.ViewModels;

namespace Scheduler.Application.Validations
{
    public class ServiceViewModelValidator : AbstractValidator<ServiceViewModel>
    {
        public ServiceViewModelValidator()
        {
            RuleFor(appointment => appointment.Id)
                .NotEmpty().WithMessage("É obrigatório informar o Id.");
        }
    }
}