using FluentValidation;
using MessageService.API.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Business.Validation
{
    public class MessageDtoValidator : AbstractValidator<MessageDto>
    {
        public MessageDtoValidator()
        {
            RuleFor(x => x.MessageContent).NotEmpty();
            RuleFor(x => x.RelatedId).GreaterThan(0);
        }
    }
}
