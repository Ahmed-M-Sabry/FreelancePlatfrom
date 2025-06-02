using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Handler
{
    public class SearchJobPostsQueryHandler : ResponseHandler, IRequestHandler<SearchJobPostsQuery, ApiResponse<List<SearchJobPostsDto>>>
    {
        private readonly IjobPostServices _jobPostService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchJobPostsQueryHandler(
            IjobPostServices jobPostService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _jobPostService = jobPostService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<SearchJobPostsDto>>> Handle(SearchJobPostsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<List<SearchJobPostsDto>>();

            var jobPosts = await _jobPostService.SearchJobPostsAsync(request.Keyword);

            if (jobPosts == null || !jobPosts.Any())
                return NotFound<List<SearchJobPostsDto>>("No job posts found matching the keyword.");

            var dto = _mapper.Map<List<SearchJobPostsDto>>(jobPosts);
            return Success(dto, new { Count = dto.Count });
        }
    }
}
