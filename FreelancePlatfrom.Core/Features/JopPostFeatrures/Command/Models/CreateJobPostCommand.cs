using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models 
{
    public class CreateJobPostCommand : IRequest<ApiResponse<string>>
    {
        private string _title;
        private string _description;
        private decimal _price;

        public string UserId { get; set; } 

        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(Title));
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(Description));
        }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentException("Price cannot be negative.");
        }

        public DateTime? DurationTime { get; set; }

        public int CategoryId { get; set; }

        public List<int> SkillIds { get; set; } = new List<int>();

        public static CreateJobPostCommand FromDto(CreateJobPostDto dto)
        {
            return new CreateJobPostCommand
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                DurationTime = dto.DurationTime,
                CategoryId = dto.CategoryId,
                SkillIds = dto.SkillIds
            };
        }
    }
}