using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Infrastructure;
using BLL.DTO;
using AutoMapper;
using TaskTrackingSystem.Models;

namespace TaskTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        ITaskService taskService;

        public HomeController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public ActionResult Index()
        {
            IEnumerable<TaskDTO> taskDTOs = taskService.GetTasks();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, TaskViewModel>()).CreateMapper();
            var tasks = mapper.Map<IEnumerable<TaskDTO>, List<TaskViewModel>>(taskDTOs);
            return View(taskDTOs);
        }
        public ActionResult AddSubTask(int? id)
        {
            try
            {
                TaskDTO task = taskService.GetTask(id);
                var subtask = new SubTaskViewModel { TaskId = task.Id };

                return View(subtask);
            }
            catch (ValidationException e)
            {
                return Content(e.Message);
            }
        }
        [HttpPost]
        public ActionResult AddSubTask(SubTaskViewModel subTask)
        {
            try
            {
                var subTaskDto = new SubTaskDTO
                {
                    TaskId = subTask.TaskId,
                    Description = subTask.Description,
                    Name = subTask.Name
                };
                taskService.AddSubTask(subTaskDto);
                return Content("<h2> subtask added</h2>");
            }
            catch (ValidationException e)
            {

                throw;
            }
        }
        protected override void Dispose(bool disposing)
        {
            taskService.Dispose();
            base.Dispose(disposing);
        }
    }
}
