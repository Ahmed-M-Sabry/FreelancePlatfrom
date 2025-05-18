using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Models
{
    public class EditJobPostCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
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

        public static EditJobPostCommand FromDto(EditJobPostDto dto)
        {
            return new EditJobPostCommand
            {
                Id = dto.Id,
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
