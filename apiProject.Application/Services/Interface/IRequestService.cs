using apiProject.Application.Dtos.Request;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Subject;
using apiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services.Interface
{
   public interface IRequestService
    {
        Task<RequestResponseDto> CreateRequestAsync(CreateRequestDto dto);
        Task<bool?> ChangeStatus(ChangeStatusRequestDto dto);
        Task<List<RequestWorkFlowDto>> GetRequestsByUserIdAsync(int userId);

        //Task<List<RequestDto>> GetAllRequestsAsync();

        //Task<RequestDto?> GetByIdAsync(int id);

        //Task UpdateRequestAsync(UpdateRequestDto dto);

        Task DeleteRequestAsync(int id);
    }
}
