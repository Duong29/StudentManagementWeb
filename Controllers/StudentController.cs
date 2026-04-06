using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentManagementWeb.Data;
using StudentManagementWeb.Models;
using StudentManagementWeb.Services;
using System.Text;

namespace StudentManagementWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        //Constructor
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // Hiển thị danh sách sv
        public IActionResult Index(int? searchId, string sortOrder)
        {
            var students = _studentService.GetAll(searchId, sortOrder);
            
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            _studentService.Add(student);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);
            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _studentService.Update(student);
            return RedirectToAction("Index");
        }
        // Xuất file
        public IActionResult ExportCsv()
        {
            var fileBytes = _studentService.ExportCsv();

            return File(fileBytes,
                        "text/csv",
                        "Students.csv");
        }

    }
}
