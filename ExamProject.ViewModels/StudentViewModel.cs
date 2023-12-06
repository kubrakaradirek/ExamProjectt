using ExamProject.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamProject.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
                
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Contect No")]
        public string Contact { get; set; }
        [Display(Name = "CV")]
        public string CvFileName { get; set; }
        public string PictureFileName { get; set; }
        public int GroupId { get; set; }
        public IFormFile PictureFile { get; set; }
        public IFormFile CvFile { get; set; }
        public int TotalCount { get; set; }
        public List<StudentViewModel> StudentList { get; set; }
        public StudentViewModel(Student model)
        {
            Id = model.Id;
            FullName = model.FullName ?? "";
            UserName = model.UserName;
            Password = model.Password;
            Contact = model.Contact ?? "";
            CvFileName = model.CvFileName ?? "";
            PictureFileName = model.PictureFileName ?? "";
            GroupId = model.GroupId;

        }
        public Student ConvertViewModel(StudentViewModel vm)
        {
            return new Student
            {
                Id = vm.Id,
                FullName = vm.FullName ?? "",
                UserName = vm.UserName,
                Password = vm.Password,
                Contact = vm.Contact ?? "",
                CvFileName = vm.CvFileName ?? "",
                PictureFileName = vm.PictureFileName ?? "",
                GroupId = vm.GroupId
            };
        }

    }
}
