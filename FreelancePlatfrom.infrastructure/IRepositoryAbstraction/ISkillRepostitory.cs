﻿using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface ISkillRepostitory : IGenericRepositoryAsync<Skill>
    {
        Task<List<int>> GetValidSkillIdsAsync(List<int> skillIds);
        Task<Skill> GetByNameAsync(string Skillname);
        Task<Skill> DeleteSkill(int id);
        Task<Skill> RestoreSkill(int id);
        Task<List<Skill>> GetAllSKillForUserAsync();
        Task<List<Skill>> GetAllSKillForAdminAsync();
    }
}
