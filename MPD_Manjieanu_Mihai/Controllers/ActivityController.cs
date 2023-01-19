using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPD_Manjineanu_Mihai.Models;
using MPD_Manjineanu_Mihai.Repositories;
using MPD_Manjineanu_Mihai.Repositories.Activity;
using MPD_Manjineanu_Mihai.Repositories.Category;
using MPD_Manjineanu_Mihai.ViewModels;

namespace MPD_Manjineanu_Mihai.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ILogger<ActivityController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly IHttpContextAccessor _httpContext;
        private string _user;

        public ActivityController(IActivityRepository activityRepo,
            ICategoryRepository categoryRepo,
            IUserRepository userRepo,
            IHttpContextAccessor httpContext,
            ILogger<ActivityController> logger)
        {
            _activityRepo = activityRepo;
            _categoryRepo = categoryRepo;
            _logger = logger;
            _userRepo = userRepo;
            _httpContext = httpContext;
            _user = _httpContext.HttpContext.Session.GetString("username") ?? String.Empty;
        }
        public async Task<IActionResult> Index()
        {

            var activityList = await _activityRepo.GetAllByUsername(_user);
            var categoryList = await _categoryRepo.GetAll();
            var model = new List<ActivityViewModel>();
            
            foreach (var activity in activityList)
            {
                model.Add(new ActivityViewModel
                {
                    Activity = activity,
                    Category = categoryList.Where(c => c.CategoryID == activity.CategoryID).FirstOrDefault()
                });

            }

            return View("ActivityListView", model);
        }
        public IActionResult CreateActivityView()
        {

            return View("AddActivity");
        }
        public async Task<IActionResult> LoadEditActivityView(int id)
        {
            var activity = await _activityRepo.GetActivityById(id);
            var category = await _categoryRepo.GetById(activity.CategoryID ?? 0) ;
            _httpContext.HttpContext.Session.SetInt32("activity", id);
            var activityViewModel = new ActivityViewModel
            {
                Category = activity.Category,
                Activity = activity

            };
            return View("AddActivity", activityViewModel);
        }

        public async Task<IActionResult> AddActivity(ActivityViewModel input)
        {
            var selectedActivityId = _httpContext.HttpContext.Session.GetInt32("activity") ?? 0;
            var currentUser = await _userRepo.GetUser(_user);
            input.Activity.User = currentUser;
            try
            {
                if(selectedActivityId != 0)
                {

                    input.Activity.ActivityID = selectedActivityId;
                    input.Activity.Category = input.Category;
                    await _categoryRepo.Update(input.Category);
                    await _activityRepo.Update(input.Activity);
                    return RedirectToAction("Index");
                }
                input.Activity.Category = input.Category;
                await _categoryRepo.Insert(input.Category);
                await _activityRepo.InsertActivity(input.Activity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index");
        }
        public  async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _activityRepo.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index");
        }

    }
}
