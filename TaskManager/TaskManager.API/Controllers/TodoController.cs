using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts.Service;
using TaskManager.Application.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        /// <summary>
        /// Single Todo
        /// </summary>
        /// <param name="id">Todo Identifier</param>
        /// <returns>TodoForGettingDto</returns>
        [HttpGet("{id:guid}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            try
            {
                var result = todoService.GetSingleTodo(id);

                return result is null
                    ? NotFound($"Todo with id: {id} not found.")
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message
                );
            }
        }

        /// <summary>
        /// All Todos of specific user
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <returns>IEnumerable TodoForGettingDto</returns>
        [HttpGet("user/{userId:guid}")]
        public IActionResult GetMultiple([FromRoute] Guid userId)
        {
            try
            {
                var result = todoService.GetAllTodosOfSpecificUser(userId);

                return result.Count() == 0
                    ? NotFound($"No todos found for user with id: {userId}.")
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message
                );
            }
        }

        /// <summary>
        /// All Todos
        /// </summary>
        /// <returns>IEnumerable TodoForGettingDto</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = todoService.GetAllTodos();

                return result.Count() == 0
                    ? NotFound($"No todos found.")
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message
                );
            }
        }

        /// <summary>
        /// New Todo
        /// </summary>
        /// <param name="input">Input Dto</param>
        /// <returns>Guid</returns>
        [HttpPost]
        public IActionResult Add([FromBody] TodoForCreatingDto input)
        {
            try
            {
                var result = todoService.AddNewTodo(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message
                );
            }
        }

        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id">Todo Identifier</param>
        /// <returns>Guid</returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = todoService.DeleteExistedTodo(id);
                return result == Guid.Empty
                    ? NotFound($"Problem while deleting todo with id: {id}.")
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message
                );
            }
        }

    }
}
