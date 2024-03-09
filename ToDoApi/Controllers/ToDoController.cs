using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Packages;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        IPKG_TO_DO package;
        public ToDoController(IPKG_TO_DO package)
        {
            this.package=package;
        }

        [HttpGet]
        public IActionResult get_tasks()
        {
           List<Todo> toDos = new List<Todo>();

            toDos=package.get_tasks();

            return Ok(toDos);
        }


        [HttpPost]
        public IActionResult Save_task(Todo todo)
        {
            try 
            {
                if (string.IsNullOrEmpty(todo.task))
                {
                    return BadRequest("Task description is required!");
                }
                package.add_task(todo);

            }
            catch
            {
                StatusCode(StatusCodes.Status500InternalServerError, "System error, try again");
            }

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete_task(int id) {

            package.delete_task(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update_task(Todo todo)
        {

            package.update_task(todo);

            return Ok();
        }
        [HttpPut]
        public IActionResult Done_task(Todo todo)
        {

            package.done_task(todo);

            return Ok();
        }

    }
}
