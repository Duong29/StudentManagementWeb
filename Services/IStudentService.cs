using Microsoft.Data.SqlClient;
using StudentManagementWeb.Models;

namespace StudentManagementWeb.Services
{
    public interface IStudentService
    {
        List<Student> GetAll(int? searchId, string sortOrder);
        void Add(Student student);
        void Delete(int id);
        Student GetById(int id);
        void Update(Student student);
        List<Student> GetAllStudents();
        byte[] ExportCsv();
    }
}
