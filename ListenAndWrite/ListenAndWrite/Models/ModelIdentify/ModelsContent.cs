using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Windows.Forms.DataVisualization.Charting;

namespace ListenAndWrite.ModelIdentify
{
    public class ModelsContent : DbContext
    {
        public static int MAX_LEVEL = 10;
        public ModelsContent() : base("ListenAndWrite") { }

        public static string PRE_AUDIO_PATH = "Resources/Audio/";
        public static string PRE_SCRIPT_PATH = "Resources/Script/";
        public static string ROOT_ID = "1";

        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserClaim> MemberClaims { get; set; }
        public DbSet<IdentityUserLogin> MemberLogins { get; set; }
        public DbSet<IdentityUserRole> MemberRoles { get; set; }
        public DbSet<ApplicationUser> Members { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LessonCategory> LessonCateogies { get; set; }
        public DbSet<Score> Scores { get; set; }
        //public DbSet<Test> Tests { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasKey<string>(u => u.Id);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(ul => ul.UserId);
            modelBuilder.Entity<IdentityUserClaim>().HasKey(uc => uc.Id);

            modelBuilder.Entity<ApplicationUser>().ToTable("Members");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("MemberRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("MemberLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("MemberClaims");

        }

        public static ModelsContent Create()
        {
            return new ModelsContent();
        }
    }
}