using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using API.Models;
using BLL.Infrastructure;
using BLL.Services;

using Marvin.JsonPatch;
using System.Web.Http.Routing;
using System.Web;

namespace API.Controllers
{
    [RoutePrefix("api")]
    public class TasksController : ApiController
    {
        const int maxPageSize = 3;
        static string connection = "defaultdb";
        ITaskService taskService = new TaskService(connection);
        public TasksController()
        {

        }
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        [AllowAnonymous]
        [Route("tasks", Name = "TasksList")]
        [HttpGet]

        public IHttpActionResult GetTasks(string sort = "id", string label = null, string fields = null, int page = 1, int pageSize = 5)
        {
            try
            {
                var tasks = taskService.GetTasks(sort, label);

                int totalCount = tasks.Count();
                var paginationHeader = Pagination.GetPaginationHeader(pageSize, maxPageSize,totalCount, page,Request,sort,label);

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                   Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                return Ok(tasks
                    .Skip(pageSize*(page-1)).Take(pageSize));
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        [Route("tasks/{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var task = taskService.GetTask(id);
                return Ok(task);
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }


        }

        [Route("tasks")]
        [HttpPost]
        public IHttpActionResult PostTask([FromBody] TaskDTO task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                var result = taskService.AddTask(task);

                return Created(Request.RequestUri
                    + "/" + result.Id.ToString(), result);
            }
            catch (DatabaseException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("tasks/{id}")]
        [HttpPut]
        public IHttpActionResult PutTask(int id, [FromBody] TaskDTO task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                TaskDTO result = taskService.UpdateTask(id, task);
                return Ok(result);
            }
            catch (DatabaseException)
            {
                return BadRequest();
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }


        }
        [Route("tasks/{id}")]
        [HttpPatch]
        public IHttpActionResult TaskPatch(int id, [FromBody] JsonPatchDocument<TaskDTO> taskPatchDocument)
        {
            try
            {
                if (taskPatchDocument == null)
                {
                    return BadRequest();
                }
                var resultDTO = taskService.PatchTask(id, taskPatchDocument);
                return Ok(resultDTO);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return BadRequest();
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
        [Route("tasks/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            try
            {
                taskService.DeleteTask(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DatabaseException)
            {
                return BadRequest();
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }
    }
}