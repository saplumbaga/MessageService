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
    public class ReplyManager : IReplyService
    {
        private readonly ReplyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpService _httpService;
        private readonly IOptionsMonitor<AppConfig> _optionsMonitor;

        public ReplyManager(ReplyRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IHttpService httpService, IOptionsMonitor<AppConfig> optionsMonitor)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpService = httpService;
            _optionsMonitor = optionsMonitor;
        }

        public async Task<IDataResult<ReplyDto>> Add(ReplyDto dto)
        {
            FluentValidationTool.Validate(new ReplyDtoValidator(), dto);

            if (string.IsNullOrEmpty(dto.IpAddr))
            {
                dto.IpAddr = await _httpService.GetRequestIpAddress();
            }

            dto.DateAdded = DateTime.Now;

            var entityToAdd = _mapper.Map<Reply>(dto);

            _repository.Add(entityToAdd);

            await _unitOfWork.CompleteAsync();

            return new SuccessDataResult<ReplyDto>(_mapper.Map<ReplyDto>(entityToAdd), HttpStatusCode.Created);
        }

        public async Task<IDataResult<List<ReplyListDto>>> List(DefaultParams parameters, int messageId)
        {
            var query = _repository.Where(x => true).AsNoTracking();
            
            if(parameters.ShowDeleted != 1)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            if (messageId > 0)
            {
                query = query.Where(x => x.MessageId == messageId);
            }


            if (!string.IsNullOrEmpty(parameters.Term))
            {
                query = query.Where(x => x.MessageContent.Contains(parameters.Term, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(parameters.Ip))
            {
                query = query.Where(x => x.IpAddr.Equals(parameters.Ip, StringComparison.OrdinalIgnoreCase));
            }


            var count = await query.CountAsync();

            var isOrderAscending = parameters.ReplyDirection?.Equals(Directions.ASCENDING, StringComparison.OrdinalIgnoreCase) ?? false;

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

            

            var data = await query.ToListAsync();

            return new SuccessDataResult<List<ReplyListDto>>(_mapper.Map<List<ReplyListDto>>(data), count);
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
                return new ErrorResult(Messages.Reply_Not_Found, HttpStatusCode.NotFound);
            }

            entityToDelete.IsDeleted = true;

            await _unitOfWork.CompleteAsync();

            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
