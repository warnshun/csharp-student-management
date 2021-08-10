using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{

    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }
                
        public IActionResult Index()
        {
            IEnumerable<Student> Students = _studentRepository.GetAllStudents();

            return View(Students);
        }
                
        public IActionResult Details(int id)
        {
            // throw new Exception("此異常發生在於 Details View 中");

            Student student = _studentRepository.GetStudent(id);

            if (student == null)
            {
                Response.StatusCode = 404;

                return View("StudentNotFound", id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = student,
                PageTitle = "學生詳情"
            };


            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            // 驗證提供的資料是否有效，如未通過驗證，需重新編輯學生資料
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Avatars != null && model.Avatars.Count > 0)
                {
                    uniqueFileName = ProcessUploadFile(model);
                    
                }

                Student newStudent = new Student()
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    AvatarPath = uniqueFileName,
                };

                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }

            return View();
        }
    
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);

           // if (student != null)
           // {
                StudentEditViewModel studentEditViewModel = new StudentEditViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    ExistingAvatarPath = student.AvatarPath
                };

                return View(studentEditViewModel);
           // }
           // throw new Exception("查不到該學生資料");

           
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);

                student.Email = model.Email;
                student.Name = model.Name;
                student.ClassName = model.ClassName;
                student.AvatarPath = model.ExistingAvatarPath;

                if (model.Avatars.Count > 0)
                {
                    if (model.ExistingAvatarPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", model.ExistingAvatarPath);

                        System.IO.File.Delete(filePath);
                    }

                    string uniqueFileName = ProcessUploadFile(model);

                    if (uniqueFileName != null) student.AvatarPath = uniqueFileName;

                }

                Student updateStudent = _studentRepository.Update(student);

                return RedirectToAction("Index");
            }
            return View();
        }

        private string ProcessUploadFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Avatars.Count > 0)
            {
                // 通過 IWebHostEnvironment 服務取得 wwwroot資料夾 路徑，並套上 img資料夾
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                foreach (var avatar in model.Avatars)
                {
                    // 確保檔案名是唯一，在檔案名後面加上 "_" 和 "GUID"
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;

                    // 合併 "儲存位置" 與 "檔案名"
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        // 使用 IFormFile介面 提供的 CopyTo()方法，將檔案複製到 /wwwroot/img 資料夾
                        avatar.CopyTo(fileStream);
                    }
                    
                    
                }
            }

            return uniqueFileName;
        }
    }
}
