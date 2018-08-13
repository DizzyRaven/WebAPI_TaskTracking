using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using TaskTrackingAPI.Models;
using BLL.Infrastructure;
using BLL.Services;

using Marvin.JsonPatch;

namespace TaskTrackingAPI.Controllers
{
    [RoutePrefix("api")]
    public class SubTasksController : ApiController
    {
        static string connection = "defaultdb";
        ISubTaskService subTaskService;
        public SubTasksController()
        {
            subTaskService = new SubTaskService(connection);
        }
        public SubTasksController(ISubTaskService subTaskService)
        {
            this.subTaskService = subTaskService;
        }

        [Route("tasks/{taskId}/subtasks")]
        [HttpGet]
        public IHttpActionResult GetSubTasks(int taskId)
        {
            try
            {
                var tasks = subTaskService.GetSubTasks(taskId);
                return Ok(tasks);
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("tasks/{TaskId}/subtasks/{id}")]
        [Route("subtasks/{id}")]
        [HttpGet]
        public IHttpActionResult GetSubTask(int id, int? taskId = null)
        {
            try
            {
                SubTaskDTO result = subTaskService.GetSubTask(id, taskId);
                return Ok(result);
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
        [Route("subtasks")]
        [HttpPost]
        public IHttpActionResult PostSubTask([FromBody] SubTaskDTO subtask)
        {
            try
            {
                if (subtask == null)
                    return BadRequest();
                SubTaskDTO result = subTaskService.AddSubTask(subtask);
                return Created(Request.RequestUri + "/" + result.Id.ToString(), result);
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("subtasks/{id}")]
        [HttpPut]
        public IHttpActionResult PutSubTask([FromBody] SubTaskDTO subTaskDTO, int id)
        {
            try
            {
                if (subTaskDTO == null)
                {
                    return BadRequest();
                }
                var result = subTaskService.UpdateSubTask(subTaskDTO, id);
                return Ok(result);
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (DatabaseException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("subtasks/{id}")]
        public IHttpActionResult DeleteSubTask(int id)
        {
            try
            {
                subTaskService.DeleteSubTask(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ValidationException)
            {
                return NotFound();
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
        [Route("subtasks/{id}")]
        [HttpPatch]
        public IHttpActionResult PatchSubTask(int id, [FromBody]JsonPatchDocument<SubTaskDTO> subTaskPatchDocument)
        {
            try
            {
                if (subTaskPatchDocument == null)
                    return BadRequest();
                var result = subTaskService.PatchSubTask(id, subTaskPatchDocument);
                return Ok(result);
            }
            catch (ValidationException)
            {
                return NotFound();
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
    }
}