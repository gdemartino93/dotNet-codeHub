using codeHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace codeHub.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //seeding database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Java", Description = "Java World", DisplayOrder = 1, IsVisible = true },
                new Category() { Id = 2, Name = "C#", Description = "C# World", DisplayOrder = 1, IsVisible = true },
                new Category() { Id = 3, Name = "CSS", Description = "CSS World", DisplayOrder = 1, IsVisible = true }
                );
            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    Id = 1,
                    CreatedAt = new DateTime(2022, 1, 10),
                    Description = "Learn the fundamentals of computer science with Python",
                    LastUpdatedAt = null,
                    Level = "Intermediate",
                    Price = 50,
                    Text = "This course is designed to teach you the basics of computer science using Python. Topics include: algorithms, data structures, recursion, sorting, and searching. By the end of the course, you will have a strong foundation in computer science and be able to write basic programs in Python.",
                    Title = "Introduction to Computer Science with Python",
                    CategoryId = 1,
                },

                new Course()
                {
                    Id = 2,
                    CreatedAt = new DateTime(2022, 2, 20),
                    Description = "Learn how to build scalable and performant web applications with React",
                    LastUpdatedAt = new DateTime(2022, 3, 5),
                    Level = "Advanced",
                    Price = 100,
                    Text = "In this course, you will learn how to build modern web applications with React. Topics include: React components, state management with Redux, server-side rendering, and performance optimization. By the end of the course, you will be able to build scalable and performant web applications with React.",
                    Title = "Mastering React",
                    CategoryId = 2
                },

                new Course()
                {
                    Id = 3,
                    CreatedAt = new DateTime(2022, 4, 1),
                    Description = "Learn how to build full-stack web applications with Node.js and MongoDB",
                    LastUpdatedAt = null,
                    Level = "Intermediate",
                    Price = 75,
                    Text = "This course teaches you how to build full-stack web applications using Node.js and MongoDB. Topics include: RESTful APIs, authentication and authorization, database design, and deployment. By the end of the course, you will be able to build and deploy a full-stack web application with Node.js and MongoDB.",
                    Title = "Full-Stack Web Development with Node.js and MongoDB",
                    CategoryId = 3
                },

                new Course()
                {
                    Id = 4,
                    CreatedAt = new DateTime(2022, 3, 15),
                    Description = "Learn how to use machine learning to analyze and visualize data",
                    LastUpdatedAt = new DateTime(2022, 4, 10),
                    Level = "Advanced",
                    Price = 150,
                    Text = "In this course, you will learn how to use machine learning techniques to analyze and visualize data. Topics include: data preprocessing, dimensionality reduction, clustering, classification, and regression. You will also learn how to use popular machine learning libraries such as scikit-learn and TensorFlow. By the end of the course, you will be able to apply machine learning techniques to real-world data analysis and visualization problems.",
                    Title = "Machine Learning for Data Analysis and Visualization",
                    CategoryId = 1
                }
            );
        }
    }
}
