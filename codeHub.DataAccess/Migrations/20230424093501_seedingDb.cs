using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace codeHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "IsVisible", "Name" },
                values: new object[,]
                {
                    { 1, "Java World", 1, true, "Java" },
                    { 2, "C# World", 1, true, "C#" },
                    { 3, "CSS World", 1, true, "CSS" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "LastUpdatedAt", "Level", "Price", "Text", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn the fundamentals of computer science with Python", null, "Intermediate", 50.0, "This course is designed to teach you the basics of computer science using Python. Topics include: algorithms, data structures, recursion, sorting, and searching. By the end of the course, you will have a strong foundation in computer science and be able to write basic programs in Python.", "Introduction to Computer Science with Python" },
                    { 2, 2, new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to build scalable and performant web applications with React", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced", 100.0, "In this course, you will learn how to build modern web applications with React. Topics include: React components, state management with Redux, server-side rendering, and performance optimization. By the end of the course, you will be able to build scalable and performant web applications with React.", "Mastering React" },
                    { 3, 3, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to build full-stack web applications with Node.js and MongoDB", null, "Intermediate", 75.0, "This course teaches you how to build full-stack web applications using Node.js and MongoDB. Topics include: RESTful APIs, authentication and authorization, database design, and deployment. By the end of the course, you will be able to build and deploy a full-stack web application with Node.js and MongoDB.", "Full-Stack Web Development with Node.js and MongoDB" },
                    { 4, 1, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to use machine learning to analyze and visualize data", new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced", 150.0, "In this course, you will learn how to use machine learning techniques to analyze and visualize data. Topics include: data preprocessing, dimensionality reduction, clustering, classification, and regression. You will also learn how to use popular machine learning libraries such as scikit-learn and TensorFlow. By the end of the course, you will be able to apply machine learning techniques to real-world data analysis and visualization problems.", "Machine Learning for Data Analysis and Visualization" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
