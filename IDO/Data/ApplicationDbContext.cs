using IDO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace IDO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<ToDoTask> Tasks { get; set; }

        public DbSet<TaskImportance> TaskImportances { get; set; }

        public DbSet<ToDoTaskStatus> ToDoTaskStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            var appUser = new AppUser
            {
                Id = 1,
                Email = "abdallahkorhani1@gmail.com",
                NormalizedEmail = "ABDALLAHKORHANI1@GMAIL.COM",
                EmailConfirmed = true,
                UserName = "abdallah",
                NormalizedUserName = "ABDALLAH"
            };

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Abdallah@123");

          
            modelBuilder.Entity<AppUser>().HasData(appUser);

            modelBuilder.Entity<ToDoTaskStatus>().HasData(
                new ToDoTaskStatus { StatusId = 1,  StatusName = "To Do" },
                new ToDoTaskStatus { StatusId = 2, StatusName = "Doing" },
                new ToDoTaskStatus { StatusId = 3, StatusName = "Done" },
                new ToDoTaskStatus { StatusId = 4, StatusName = "None" }
            );

            modelBuilder.Entity<TaskImportance>().HasData(
                new TaskImportance { taskImportanceId = 1 , taskImportanceName = "Low" },
                new TaskImportance { taskImportanceId = 2 , taskImportanceName = "Medium" },
                new TaskImportance { taskImportanceId = 3 , taskImportanceName = "High" }
            );



            modelBuilder.Entity<ToDoTask>().HasData(

                new ToDoTask { Id = 1, taskTitle = "Fixing Laptop", taskCategory = "Fixing", taskDueDate = new DateTime(), taskEstimate = "2 hours", taskImportanceId = 1, StatusId = 1, userId = 1 },
                new ToDoTask { Id = 2, taskTitle = "Assessment", taskCategory = "job opportunity", taskDueDate = new DateTime(), taskEstimate = "3 hours", taskImportanceId = 3, StatusId = 2, userId = 1 },
                new ToDoTask { Id = 3, taskTitle = "Senior project", taskCategory = "senior", taskDueDate = new DateTime(), taskEstimate = "1 hour", taskImportanceId = 2, StatusId = 3, userId = 1 },
                new ToDoTask { Id = 4, taskTitle = "Football", taskCategory = "Sport", taskDueDate = new DateTime(), taskEstimate = "2 hours", taskImportanceId = 3, StatusId = 1, userId = 1 },
                new ToDoTask { Id = 5, taskTitle = "Reading a book", taskCategory = "Reading", taskDueDate = new DateTime(), taskEstimate = "1 hour", taskImportanceId = 1, StatusId = 1, userId = 1 },
                new ToDoTask { Id = 6, taskTitle = "Vsisiting grandma", taskCategory = "idk", taskDueDate = new DateTime(), taskEstimate = "1 hour", taskImportanceId = 3, StatusId = 1, userId = 1 },
                new ToDoTask { Id = 7, taskTitle = "Fixing TV", taskCategory = "Fixing", taskDueDate = new DateTime(), taskEstimate = "2 hour", StatusId = 1, taskImportanceId = 1, userId = 1 }

            );

        }

  

    }
}
