using apiProject.Application.Dtos.Request;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Subject;
using apiProject.Application.Dtos.User;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitRepository _unitRepository;


        private readonly IRequestRepository _repository;
        public RequestService(IRequestRepository repository, ISubjectRepository subjectRepository, IUnitRepository unitRepository)
        {
            _repository = repository;
            _subjectRepository = subjectRepository;
            _unitRepository = unitRepository;
        }

   
        public async Task<bool?> ChangeStatus(ChangeStatusRequestDto dto)
        {
            var subject = await _repository.GetByIdAsync(dto.RequestId);

            if (subject == null)
                return null;

         //   subject. = !subject.IsActive;
            await _repository.UpdateSubjectAsync(subject);
            await _repository.SaveChangesAsync();
            return true;

        }
        public async Task<RequestResponseDto> CreateRequestAsync(CreateRequestDto dto)
        {

            var unit = await _unitRepository.GetByIdAsync(dto.UnitId);
            if (unit == null)
                throw new Exception("واحد انتخاب شده یافت نشد.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new Exception("لطفاً متن درخواست را وارد کنید.");


            var request = new Request()
            {
                CreatedDate = DateTime.Now,
                UnitId=dto.UnitId,
                CreatedByUserId = dto.CreatedByUserId,
                Description = dto.Description ,
                SubSubjectId = dto.SubSubjectId,
                 RequestCode = "TEMP"
            };

            await _repository.AddRequestAsync(request);
            await _repository.SaveChangesAsync();

            var random = new Random();
            int randomNumber = random.Next(1, 11);
            string requestCode = request.Id.ToString() + randomNumber.ToString();
            request.RequestCode = requestCode;
            await _repository.SaveChangesAsync();


            if (dto.Photo != null)
            {
                using var ms = new MemoryStream();

                await dto.Photo.CopyToAsync(ms);

                var photo = new RequestPhoto
                {
                    ImageData = ms.ToArray(),
                    RequestId = request.Id
                };

                await _repository.AddPhotoAsync(photo);

                await _repository.SaveChangesAsync();
            }
            return new RequestResponseDto
            {
                 RequestId = request.Id,
                   RequestCode = request.RequestCode,
            };

        }

        public Task DeleteRequestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RequestWorkFlowDto>> GetRequestsByUserIdAsync(int userId)
        {
            var requests = await _repository.GetRequestsByUserIdAsync(userId);

            return requests.Select(x => new RequestWorkFlowDto
            {
                Id = x.Id,

                SubjectTitle = x.SubSubject.Parent?.Title,

                SubSubjectTitle = x.SubSubject.Title,

                Description = x.Description,

                CreatedDate = x.CreatedDate,

                UnitName = x.Unit.Name,
                RequestCode = x.RequestCode ?? ""

            }).ToList();
        }

        public async Task<List<RequestWorkFlowDto>> SearchAsync(SearchRequestDto dto)
        {
            var requests = await _repository.GetRequestsByUserIdAsync(dto.UserId);

            var query = requests.AsQueryable();

            if (dto.UnitId?.Any() == true)
            {
                query = query.Where(x => dto.UnitId.Contains(x.UnitId));
            }

            if (dto.SubjectId?.Any() == true)
            {
                query = query.Where(x => dto.SubjectId.Contains(x.SubSubject.ParentId!.Value));
            }

            if (dto.SubSubjectId?.Any() == true)
            {
                query = query.Where(x => dto.SubSubjectId.Contains(x.SubSubjectId));
            }

            if (!string.IsNullOrWhiteSpace(dto.RequestCode))
            {
                query = query.Where(x => x.RequestCode.Contains(dto.RequestCode));
            }

            var result = query
            .OrderByDescending(e => e.Id)
            .ToList();

            return result.Select(x => new RequestWorkFlowDto
            {
                Id = x.Id,
                SubjectTitle = x.SubSubject.Parent.Title,
                SubSubjectTitle = x.SubSubject.Title,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                UnitName = x.Unit.Name,
                RequestCode = x.RequestCode
            }).ToList();
        }
    }
}
