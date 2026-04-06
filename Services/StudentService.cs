using StudentManagementWeb.Data;
using StudentManagementWeb.Models;
using System.Text;

namespace StudentManagementWeb.Services
{
    public class StudentService: IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Student> GetAll(int? searchId, string sortOrder)
        {
            var students = _context.Students.AsQueryable();
            if (searchId != null)
            {
                students = students.Where(s => s.Id == searchId);
            }
            switch (sortOrder)
            {
                case "gpa_desc":
                    students = students.OrderByDescending(s => s.Gpa);
                    break;
                case "gpa_asc":
                    students = students.OrderBy(s => s.Gpa);
                    break;
                case "name_asc":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "gpa_gt_3":
                    students = students.Where(s => s.Gpa > 3);
                    break;
                default:
                    students = students.OrderBy(s => s.Id);
                    break;

            }
            return students.ToList();
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public byte[] ExportCsv()
        {
            var students = _context.Students.ToList();
            var csv = new StringBuilder();

            csv.AppendLine("Id,Name,Class,Age,Gpa");

            foreach (var s in students)
            {
                csv.AppendLine($"{s.Id},{s.Name},{s.ClassName},{s.Age},{s.Gpa}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
    }
}
