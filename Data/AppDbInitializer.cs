using EMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Data
{

    public static class AppDbInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager,
                                          RoleManager<IdentityRole> roleManager,
                                          ApplicationDbContext context)
        {
            // Create roles
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Teacher"));
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }

            // Check if the admin user exists
            if (userManager.FindByEmailAsync("sai@ems.com").Result == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    UserName = "sai123",
                    Email = "sai123.com",
                    FirstName = "sai",
                    LastName = "vamshi",
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(admin, "sai123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Sample teachers
            string[] teacherEmails = { "teacher1@ems.com", "teacher2@ems.com", "teacher3@ems.com", "teacher4@ems.com", "teacher5@ems.com" };
            string[] teacherFirstNames = { "John", "Jane", "James", "Jill", "Jack" };
            string[] teacherLastNames = { "Doe", "Smith", "Johnson", "Roberts", "Brown" };

            for (int i = 0; i < teacherEmails.Length; i++)
            {
                if (userManager.FindByEmailAsync(teacherEmails[i]).Result == null)
                {
                    ApplicationUser teacher = new ApplicationUser
                    {
                        UserName = $"teacher{i + 1}",
                        Email = teacherEmails[i],
                        FirstName = teacherFirstNames[i],
                        LastName = teacherLastNames[i],
                        EmailConfirmed = true
                    };

                    IdentityResult result = await userManager.CreateAsync(teacher, $"teacher{i + 1}Pass!");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(teacher, "Teacher");
                    }
                }
            }

            // Sample students
            string[] studentEmails = {
        "student1@ems.com", "student2@ems.com", "student3@ems.com", "student4@ems.com", "student5@ems.com",
        "student6@ems.com", "student7@ems.com", "student8@ems.com", "student9@ems.com", "student10@ems.com"
    };
            string[] studentFirstNames = { "Alan", "Beth", "Charlie", "Dana", "Evan", "Fiona", "George", "Hannah", "Ian", "Jasmine" };
            string[] studentLastNames = { "Walker", "Lee", "King", "Orton", "Miles", "Page", "Stark", "Bolt", "Kent", "Snow" };

            for (int i = 0; i < studentEmails.Length; i++)
            {
                if (userManager.FindByEmailAsync(studentEmails[i]).Result == null)
                {
                    ApplicationUser student = new ApplicationUser
                    {
                        UserName = $"student{i + 1}",
                        Email = studentEmails[i],
                        FirstName = studentFirstNames[i],
                        LastName = studentLastNames[i],
                        EmailConfirmed = true
                    };

                    IdentityResult result = await userManager.CreateAsync(student, $"student{i + 1}Pass!");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(student, "Student");
                    }
                }
            }
            // ... [Your role, admin, teacher, and student creation code] ...

            // Seed Certificates
            if (!context.Certificates.Any())
            {
                var firstStudentId = userManager.FindByEmailAsync("student1@ems.com").Result.Id;

                context.Certificates.AddRange(
                    new Certificate
                    {
                        CertificateName = "Sample Certificate 1",
                        Description = "Achieved for completing Sample Exam 1",
                        ApplicationUserId = firstStudentId
                    },
                    new Certificate
                    {
                        CertificateName = "Sample Certificate 2",
                        Description = "Achieved for completing Sample Exam 2",
                        ApplicationUserId = firstStudentId
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed Exams 
            if (!context.Exams.Any())
            {
                var firstTeacherId = userManager.FindByEmailAsync("teacher1@ems.com").Result.Id;

                context.Exams.AddRange(
                    new Exam
                    {
                        Title = "Sample Exam 1",
                        Description = "This is a sample exam 1",
                        TeacherId = firstTeacherId,
                        Prerequisites = "None",
                        PassingScore = 50,
                        TotalScore = 100
                    },
                    new Exam
                    {
                        Title = "Sample Exam 2",
                        Description = "This is a sample exam 2",
                        TeacherId = firstTeacherId,
                        Prerequisites = "Basic Knowledge",
                        PassingScore = 60,
                        TotalScore = 120
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed Questions 
            if (!context.Questions.Any())
            {
                var firstExamId = context.Exams.OrderBy(e => e.Id).First().Id;

                context.Questions.AddRange(
                    new Question
                    {
                        QuestionText = "What is 1+1?",
                        AnswerOptions = "1,2,3,4",
                        CorrectAnswer = "2",
                        Score = 10,
                        ExamId = firstExamId
                    },
                    new Question
                    {
                        QuestionText = "What is 2+2?",
                        AnswerOptions = "2,4,6,8",
                        CorrectAnswer = "4",
                        Score = 10,
                        ExamId = firstExamId
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed ExamEnrollments
            if (!context.ExamEnrollments.Any())
            {
                var firstStudentId = userManager.FindByEmailAsync("student1@ems.com").Result.Id;
                var firstExamId = context.Exams.OrderBy(e => e.Id).First().Id;

                context.ExamEnrollments.AddRange(
                    new ExamEnrollment
                    {
                        ApplicationUserId = firstStudentId,
                        ExamId = firstExamId,
                        EnrollmentDate = DateTime.Now,
                        Score = 75,
                        ExamDate = DateTime.Now.AddDays(-5)
                    },
                    new ExamEnrollment
                    {
                        ApplicationUserId = firstStudentId,
                        ExamId = firstExamId + 1,
                        EnrollmentDate = DateTime.Now,
                        Score = 85,
                        ExamDate = DateTime.Now.AddDays(-3)
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed ExamGroups
            if (!context.ExamGroups.Any())
            {
                var firstGroupId = context.Groups.OrderBy(g => g.Id).First().Id;
                var firstExamId = context.Exams.OrderBy(e => e.Id).First().Id;

                context.ExamGroups.AddRange(
                    new ExamGroup { ExamId = firstExamId, GroupId = firstGroupId },
                    new ExamGroup { ExamId = firstExamId + 1, GroupId = firstGroupId }
                );
                await context.SaveChangesAsync();
            }

        }

    }
}
