using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
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

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Handler
{
    public class GetLanguageByIdHandler : ResponseHandler
        , IRequestHandler<GetLanguageByIdQuery, ApiResponse<GetLanguageByIdResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;
        public GetLanguageByIdHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languagesService = languagesService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetLanguageByIdResponse>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            var Language = await _languagesService.GetLanguageByIdAsync(request.Id);
            if (Language is null)
                return NotFound<GetLanguageByIdResponse>($"No Language Found With Id : {request.Id}");

            var existLanguabge = _mapper.Map<GetLanguageByIdResponse>(Language);

            return Success<GetLanguageByIdResponse>(existLanguabge);
        }
    }
}
