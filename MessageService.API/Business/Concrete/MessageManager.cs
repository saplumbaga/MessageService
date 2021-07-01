using AutoMapper;
using MessageService.API.Business.Abstract;
using MessageService.API.Business.Consts;
using MessageService.API.Business.Utilities;
using MessageService.API.Business.Validation;
using MessageService.API.Config;
using MessageService.API.Data;
using MessageService.API.Data.Dto;
using MessageService.API.Data.Entities;
using MessageService.API.Data.Repository;
using MessageService.API.Data.Results;
using MessageService.API.HttpService;
using MessageService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MessageService.API.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly MessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpService _httpService;
        private readonly IOptionsMonitor<AppConfig> _optionsMonitor;
        public MessageManager(MessageRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IHttpService httpService, IOptionsMonitor<AppConfig> optionsMonitor)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpService = httpService;
            _optionsMonitor = optionsMonitor;
        }

        public async Task<IDataResult<MessageDto>> Add(MessageDto dto)
        {
            FluentValidationTool.Validate(new MessageDtoValidator(), dto);

            if (string.IsNullOrEmpty(dto.IpAddr))
            {
                dto.IpAddr = await _httpService.GetRequestIpAddress();
            }


            var entityToAdd = _mapper.Map<Message>(dto);

            _repository.Add(entityToAdd);

            await _unitOfWork.CompleteAsync();

            return new SuccessDataResult<MessageDto>(_mapper.Map<MessageDto>(entityToAdd), HttpStatusCode.Created);
        }

        public async Task<IDataResult<int>> Count(MessageQueryParameters parameters)
        {
            var query = await GetDefaultQueryForParameters(parameters);

            var count = await query.CountAsync();

            return new SuccessDataResult<int>(count);
        }

        public async Task<IResult> Delete(int id)
        {
            if (id < 1)
            {
                return new ErrorResult(Messages.Id_Less_Zero_Error, HttpStatusCode.BadRequest);
            }

            var entityToDelete = await _repository.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();

            if (entityToDelete == null)
            {
                return new ErrorResult(Messages.Message_Not_Found, HttpStatusCode.NotFound);
            }

            entityToDelete.IsDeleted = true;

            await _unitOfWork.CompleteAsync();

            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<MessageDto>> GetDtoById(int id)
        {
            if (id < 1)
            {
                return new ErrorDataResult<MessageDto>(Messages.Id_Less_Zero_Error, HttpStatusCode.BadRequest);
            }

            var query = _repository.Where(x => x.Id == id);


            var data = await MapToDto(query).FirstOrDefaultAsync();

            if (data == null)
            {
                return new ErrorDataResult<MessageDto>(Messages.Message_Not_Found, HttpStatusCode.NotFound);
            }

            return new SuccessDataResult<MessageDto>(data);
        }

        public async Task<IDataResult<List<MessageListDto>>> List(MessageQueryParameters parameters)
        {
            var query = await GetDefaultQueryForParameters(parameters);


            var count = await query.CountAsync();

            var isOrderAscending = parameters.MessageDirection?.Equals(Directions.ASCENDING, StringComparison.OrdinalIgnoreCase) ?? false;

            if (parameters.LastId.HasValue)
            {
                if (isOrderAscending)
                {
                    query = query.OrderBy(x => x.Id);
                    query = query.Where(x => x.Id > parameters.LastId.Value);
                    query = query.Take(parameters.Take);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                    query = query.Where(x => x.Id < parameters.LastId.Value);
                    query = query.Take(parameters.Take);
                    query = query.OrderBy(x => x.Id);
                }
            }
            else
            {
                query = isOrderAscending ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                query = query.Skip(parameters.Skip);
                query = query.Take(parameters.Take);
            }

            var data = await MapToListDto(query).ToListAsync();

            return new SuccessDataResult<List<MessageListDto>>(data, count);
        }

        private async Task<IQueryable<Message>> GetDefaultQueryForParameters(MessageQueryParameters parameters)
        {
            var query = _repository.Where(x => true).AsNoTracking();


            if (parameters.ShowDeleted != 1)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            if (parameters.RelatedId > 0)
            {
                query = query.Where(x => x.RelatedId == parameters.RelatedId);
            }

            if (parameters.FromId > 0)
            {
                query = query.Where(x => x.FromId == parameters.FromId);
            }

            if (parameters.ToId > 0)
            {
                query = query.Where(x => x.ToId == parameters.ToId);
            }

            if (!string.IsNullOrEmpty(parameters.Term))
            {
                query = query.Where(x => x.MessageContent.Contains(parameters.Term, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(parameters.Ip))
            {
                query = query.Where(x => x.IpAddr.Equals(parameters.Ip, StringComparison.OrdinalIgnoreCase));
            }

            if (parameters.StartDate.HasValue)
            {
                query = query.Where(x => x.DateAdded.Date >= parameters.StartDate.Value.Date);
            }

            if (parameters.EndDate.HasValue)
            {
                query = query.Where(x => x.DateAdded.Date <= parameters.EndDate.Value.Date);
            }

            return query;

        }

        private IQueryable<MessageListDto> MapToListDto(IQueryable<Message> query)
        {
            return query.Select(x => new MessageListDto
            {
                Id = x.Id,
                FromId = x.FromId,
                DateAdded = x.DateAdded,
                IpAddr = x.IpAddr,
                RelatedId = x.RelatedId,
                IsDeleted = x.IsDeleted,
                ReplyCount = x.Replies.Where(y => !y.IsDeleted).Count(),
                ToId = x.ToId,
                DateLastActivity = x.Replies.Where(y => !y.IsDeleted).Count() > 0 ? x.Replies.Max(y => y.DateAdded) : x.DateAdded
            });
        }

        private IQueryable<MessageDto> MapToDto(IQueryable<Message> query)
        {

            return query.Select(x => new MessageDto
            {
                Id = x.Id,
                FromId = x.FromId,
                DateAdded = x.DateAdded,
                IpAddr = x.IpAddr,
                RelatedId = x.RelatedId,
                ReplyCount = x.Replies.Where(y => !y.IsDeleted).Count(),
                ToId = x.ToId,
                MessageContent = x.MessageContent
            });
        }

        public async Task<IDataResult<MessageBasicInfoDto>> GetBasicInfoDtoById(int id)
        {
            if (id < 1)
            {
                return new ErrorDataResult<MessageBasicInfoDto>(Messages.Id_Less_Zero_Error, HttpStatusCode.BadRequest);
            }

            var query = _repository.Where(x => x.Id == id);


            var data = _mapper.Map<MessageBasicInfoDto>(await query.FirstOrDefaultAsync());

            if (data == null)
            {
                return new ErrorDataResult<MessageBasicInfoDto>(Messages.Message_Not_Found, HttpStatusCode.NotFound);

            }

            return new SuccessDataResult<MessageBasicInfoDto>(data);
        }

        public async Task<IDataResult<List<MessageTopSenderDto>>> GetTopSendersBetweenDates(DefaultParams parameters)
        {
            if (parameters == null)
            {
                return new ErrorDataResult<List<MessageTopSenderDto>>(Messages.Parameters_Cannot_Null, HttpStatusCode.BadRequest);
            }

            var query = _repository.Where(x => true).AsNoTracking();


            if (parameters.ShowDeleted != 1)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            if (parameters.StartDate.HasValue)
            {
                query = query.Where(x => x.DateAdded.Date >= parameters.StartDate.Value.Date);
            }

            if (parameters.EndDate.HasValue)
            {
                query = query.Where(x => x.DateAdded.Date <= parameters.EndDate.Value.Date);
            }

            var groupedQuery = query.GroupBy(x => x.FromId)
               .Select(o => new MessageTopSenderDto
               {
                   FromId = o.Key.Value,
                   Count = o.Count(),
               }).OrderByDescending(x => x.Count);

            if (parameters.Take <= 0)
                parameters.Take = 100;

            var data = await groupedQuery.Take(parameters.Take).ToListAsync();

            return new SuccessDataResult<List<MessageTopSenderDto>>(data);
        }
    }
}
