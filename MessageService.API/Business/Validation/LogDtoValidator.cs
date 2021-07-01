using FluentValidation;
using MessageService.API.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Business.Validation
{
    public class LogDtoValidator : AbstractValidator<LogDto>
    {
        public LogDtoValidator()
        {
            RuleFor(x => x.MessageId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Action).NotEmpty();
        }
    }
}
