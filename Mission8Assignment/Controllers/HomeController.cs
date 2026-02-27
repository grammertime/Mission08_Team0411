using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0411.Models;
using System.Linq;

namespace Mission08_Team0411.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _repo;

        // Constructor Injection for the Repository Pattern
        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quadrants()
        {
            // Only display tasks that have not been completed
            var tasks = _repo.Tasks
                .Include(t => t.Category) // Load the related Category data
                .Where(t => t.Completed == false)
                .ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            // Pass the categories to the ViewBag for the dropdown
            ViewBag.Categories = _repo.Categories.ToList();

            if (id == 0) // Add new task
            {
                return View("EditTask", new TaskItem());
            }
            else // Edit existing task
            {
                var task = _repo.Tasks.SingleOrDefault(t => t.TaskId == id);
                return View("EditTask", task);
            }
        }

        [HttpPost]
        public IActionResult EditTask(TaskItem t)
        {
            if (ModelState.IsValid)
            {
                if (t.TaskId == 0)
                {
                    _repo.AddTask(t);
                }
                else
                {
                    _repo.UpdateTask(t);
                }
                return RedirectToAction("Quadrants");
            }

            // If model is invalid, reload the form and categories
            ViewBag.Categories = _repo.Categories.ToList();
            return View(t);
        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var task = _repo.Tasks.SingleOrDefault(t => t.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult DeleteTask(TaskItem t)
        {
            _repo.DeleteTask(t);
            return RedirectToAction("Quadrants");
        }
    }
}