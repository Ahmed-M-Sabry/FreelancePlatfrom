﻿using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.RepositoryImplemention
{
    public class JobPostFavouriteRepository : GenericRepositoryAsync<FavJobPost>, IJobPostFavouriteRepository
    {
        private readonly ApplicationDbContext _context;
        public JobPostFavouriteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FavJobPost>> GetFavoriteJobPostsAsync(string freelancerId)
        {
            return await _context.FavoriteJobPost
                .Include(f => f.JobPost)
                .Where(f => f.FreelancerId == freelancerId)
                .ToListAsync();
        }

        public async Task<FavJobPost> IsJobPostFavorited(string freelancerId, int jobPostId)
        {
            return await _context.FavoriteJobPost
                .FirstOrDefaultAsync(f => f.FreelancerId == freelancerId && f.JobPostId == jobPostId);
        }
        


    }
}
