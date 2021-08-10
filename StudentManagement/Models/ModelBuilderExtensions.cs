using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                    new Student
                    {
                        Id = 1,
                        Name = "Shun",
                        ClassName = ClassNameEnum.None,
                        Email = "shun@example.com",
                    },
                    new Student
                    {
                        Id = 2,
                        Name = "test",
                        ClassName = ClassNameEnum.FirstGrade,
                        Email = "test@example.com",
                    }
                );
        }
    }
}
