using ExamProject.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ExamProject.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
                
        }
        public UserViewModel(User model)
        {
            Id = model.Id;
            FullName = model.FullName ?? "";
            UserName = model.UserName;
            Password = model.Password;
            Role = model.Role;
        }
        public User ConvertViewModel(UserViewModel vm)
        {
            return new User
            {
                Id = vm.Id,
                FullName = vm.FullName ?? "",
                UserName = vm.UserName,
                Password = vm.Password,
                Role = vm.Role
            };
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int Role { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public int TotalCount { get; set; }

    }
}