using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Handler
{
    public class GetApplyTaskByIdHandler : ResponseHandler , IRequestHandler<GetApplyTaskByIdQuery, ApiResponse<GetApplyTaskByIdDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetApplyTaskByIdHandler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetApplyTaskByIdDto>> Handle(GetApplyTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<GetApplyTaskByIdDto>();

            // Get All Apply Tasks
            var applyTasks = await _applyTaskService.GetApplyTaskById(userId , request.Id);
            if (applyTasks is null)
                return BadRequest<GetApplyTaskByIdDto>("Apply Tasks Not Found by This id To Show");
             

            // Map Apply Tasks to DTOs
            var applyTaskDtos = _mapper.Map<GetApplyTaskByIdDto>(applyTasks);
            if (applyTaskDtos == null)
                return BadRequest<GetApplyTaskByIdDto>("Can't Map Apply Tasks to DTOs");

            // Success Response
            return Success<GetApplyTaskByIdDto>(applyTaskDtos);
        }
    }

}
