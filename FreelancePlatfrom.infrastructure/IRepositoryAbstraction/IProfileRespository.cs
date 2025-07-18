﻿using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.IRepositoryAbstraction
{
    public interface IProfileRespository : IGenericRepositoryAsync<Portfolio>
    {
        Task<List<Portfolio>> GetByFreelancerIdAsync(string freelancerId);
        Task<Portfolio> GetByIdAsync(int id);
    }
}
