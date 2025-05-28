using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.Rating;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.Report;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region LanguageUserRelation
            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasKey(al => new { al.ApplicationUserId, al.LanguageId });

            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasOne(al => al.ApplicationUser)
                .WithMany(u => u.UserLanguages)
                .HasForeignKey(al => al.ApplicationUserId);

            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasOne(al => al.Language)
                .WithMany(l => l.UserLanguages)
                .HasForeignKey(al => al.LanguageId);
            #endregion

            #region UserSkillRelation
            modelBuilder.Entity<UserSkill>()
                .HasKey(a => new { a.UserId, a.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId);
            #endregion

            #region SkillCategoryRelation
            modelBuilder.Entity<SkillCategory>()
                .HasKey(sc => new { sc.SkillId, sc.CategoryId });

            modelBuilder.Entity<SkillCategory>()
                .HasOne(sc => sc.Skill)
                .WithMany(s => s.SkillCategories)
                .HasForeignKey(sc => sc.SkillId);

            modelBuilder.Entity<SkillCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SkillCategories)
                .HasForeignKey(sc => sc.CategoryId);
            #endregion

            #region JobPostUserRelation
            modelBuilder.Entity<JobPost>()
                .HasOne(j => j.ApplicationUser)
                .WithMany(u => u.JobPosts)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region PortfolioWithUser
            modelBuilder.Entity<Portfolio>()
                .HasOne(j => j.ApplicationUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region JobPostCategory
            modelBuilder.Entity<JobPost>()
                .HasOne(a => a.Category)
                .WithMany(a => a.JobPosts)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region JobPostSkill
            modelBuilder.Entity<JobPostSkill>()
                .HasKey(a => new { a.SkillId, a.JobPostId });

            modelBuilder.Entity<JobPostSkill>()
                .HasOne(a => a.Skill)
                .WithMany(a => a.JobPostSkills)
                .HasForeignKey(a => a.SkillId);

            modelBuilder.Entity<JobPostSkill>()
                .HasOne(a => a.JobPost)
                .WithMany(a => a.JobPostSkills)
                .HasForeignKey(a => a.JobPostId);
            #endregion

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Client)
                .WithMany(u => u.Reviews) 
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Freelancer)
                .WithMany() 
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Contract)
                .WithOne(c => c.Review)
                .HasForeignKey<Review>(r => r.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            #region Reports
            modelBuilder.Entity<Reports>()
                .HasOne(a => a.Client)
                .WithMany(u => u.Reports)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reports>()
                .HasOne(a => a.Freelancer)
                .WithMany()
                .HasForeignKey(a => a.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Contract
            modelBuilder.Entity<Contracts>()
                .HasOne(a => a.Client)
                .WithMany(u => u.Contracts)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contracts>()
                .HasOne(a => a.Freelancer)
                .WithMany()
                .HasForeignKey(a => a.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region ApplyTask
            modelBuilder.Entity<ApplyTask>()
                .HasOne(a => a.Freelancer)
                .WithMany()  // هنا ممكن تضيف اسم خاصية لو عندك في ApplicationUser مجموعة ApplyTasks
                .HasForeignKey(a => a.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplyTask>()
                .HasOne(a => a.Freelancer)
                .WithMany(u => u.ApplyTasksAsFreelancer)
                .HasForeignKey(a => a.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Favorites
            modelBuilder.Entity<FavoritesFreelancer>()
                .HasOne(f => f.Client)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoritesFreelancer>()
                .Ignore(f => f.Freelancer);

            modelBuilder.Entity<FavJobPost>()
                .HasOne(f => f.Freelancer)
                .WithMany(u => u.FavoritesJobPosts)
                .HasForeignKey(f => f.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavJobPost>()
                .HasOne(f => f.JobPost)
                .WithMany(j => j.FavJobPosts)  
                .HasForeignKey(f => f.JobPostId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Country
            modelBuilder.Entity<ApplicationUser>()
                 .HasOne(u => u.country)
                 .WithMany(c => c.Users)
                 .HasForeignKey(u => u.CountryId)
                 .OnDelete(DeleteBehavior.Restrict);
            #endregion

        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<ApplyTask> ApplyTasks { get; set; }
        public DbSet<FavoritesFreelancer> Favorites { get; set; }
        public DbSet<FavJobPost> FavoriteJobPost { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<JobPostSkill> JobPostSkill { get; set; }
        public DbSet<FavoritesFreelancer> favoritesFreelancers { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }

    }
}