﻿using FluentValidation;
using Scheduler.Application.ViewModels;

namespace Scheduler.Application.Validations
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator()
        {
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do cliente não pode ter mais de 100 caracteres.");

            RuleFor(client => client.Email)
                .NotEmpty().WithMessage("O email do cliente é obrigatório.")
                .EmailAddress().WithMessage("O email do cliente não é válido.");
        }
    }
}