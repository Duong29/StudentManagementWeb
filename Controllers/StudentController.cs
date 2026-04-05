using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentManagementWeb.Data;
using StudentManagementWeb.Models;
using System.Text;

namespace StudentManagementWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        //Constructor
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Hiển thị danh sách sv
        public IActionResult Index(int? searchId, string sortOrder)
        {
            var students = _context.Students.AsQueryable();
            if(searchId != null)
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
            return View(students.ToList());
        }
        // Hiển thị form thêm sv
        public IActionResult Create()
        {
            return View();
        }
        // Thêm sv
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View();
                
            }
            _context.Students.Add(student);
            _context.SaveChanges(); // Gửi lệnh insert xuống sql
            return RedirectToAction("Index");
        }
        
        // Xóa sv
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if(student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            _context.SaveChanges(); // Gửi lệnh delete xuống sql
            return RedirectToAction("Index");
        }
        // Hiển thị form cập nhật sv
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if(student == null)
            {
                return NotFound();
            }
            return (View(student));
        }
        // Cập nhật sv
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        // Xuất file
        public IActionResult ExportCsv()
        {
            var students = _context.Students.ToList();
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Class,Age,Gpa");

            foreach (var s in students)
            {
                csv.AppendLine($"{s.Id},{s.Name},{s.ClassName},{s.Age},{s.Gpa}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()),
                        "text/csv", "Students.csv");
        }

    }
}
