using System.Text.Json;
using westcoasteducation.api.Models;

namespace westcoasteducation.api.Data;

public static class SeedData
{
    public static async Task LoadCourseData(WestCoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Courses.Any()) return;

        var json = System.IO.File.ReadAllText("Data/Json/courses.json");
        var courses = JsonSerializer.Deserialize<List<CourseModel>>(json, options);

        if (courses is not null && courses.Count > 0)
        {
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LoadStudentData(WestCoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Students.Any()) return;

        var json = System.IO.File.ReadAllText("Data/Json/students.json");
        var students = JsonSerializer.Deserialize<List<StudentModel>>(json, options);

        if (students is not null && students.Count > 0)
        {
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LoadTeacherData(WestCoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Teachers.Any()) return;

        var json = System.IO.File.ReadAllText("Data/Json/teacher.json");
        var teachers = JsonSerializer.Deserialize<List<TeacherModel>>(json, options);

        if (teachers is not null && teachers.Count > 0)
        {
            await context.Teachers.AddRangeAsync(teachers);
            await context.SaveChangesAsync();
        }
    }
}
