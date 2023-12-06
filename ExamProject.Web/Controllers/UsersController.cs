using ExamProject.BLL.Services;
using ExamProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;
        public UsersController(IAccountService accountService)
        {
                _accountService = accountService;
        }
        public IActionResult Index(int pagaNumber=1,int pageSize=10)
        {
            return View(_accountService.GetAllTeachers(pagaNumber,pageSize));
        }
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.AddTeacher(userViewModel);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}
