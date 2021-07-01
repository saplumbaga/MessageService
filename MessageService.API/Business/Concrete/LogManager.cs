using AutoMapper;
using MessageService.API.Business.Abstract;
using MessageService.API.Business.Validation;
using MessageService.API.Data;
using MessageService.API.Data.Dto;
using MessageService.API.Data.Entities;
using MessageService.API.Data.Repository;
using MessageService.API.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MessageService.API.Business.Concrete
{
    public class LogManager : ILogService
    {
        private readonly LogRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LogManager(LogRepository repository, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<LogDto>> Add(LogDto dto)
        {
            FluentValidationTool.Validate(new LogDtoValidator(), dto);

            var entity = _mapper.Map<Log>(dto);

            _repository.Add(entity);

            await _unitOfWork.CompleteAsync();

            return new SuccessDataResult<LogDto>(_mapper.Map<LogDto>(entity), HttpStatusCode.Created);
        }
    }
}
