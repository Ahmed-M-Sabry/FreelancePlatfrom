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
    public class GetLanguageByNameHandler : ResponseHandler
        , IRequestHandler<GetLanguageByNameQuery, ApiResponse<GetLanguageByNameResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;
        public GetLanguageByNameHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languagesService = languagesService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetLanguageByNameResponse>> Handle(GetLanguageByNameQuery request, CancellationToken cancellationToken)
        {
            var Language = await _languagesService.GetLanguageByNameAsync(request.Value);
            if (Language is null)
                return NotFound<GetLanguageByNameResponse>($"No Language Found With name : {request.Value}");

            var existLanguabge = _mapper.Map<GetLanguageByNameResponse>(Language);

            return Success<GetLanguageByNameResponse>(existLanguabge);
        }
    }
}
