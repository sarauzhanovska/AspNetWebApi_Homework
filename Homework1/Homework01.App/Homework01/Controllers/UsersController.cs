using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Add GET method that returns all users
        [HttpGet]
        public ActionResult<List<string>> GetAllUsers()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.Users);
        }

        // Add GET method that returns one user

        [HttpGet("{index}")]
        public ActionResult<string> GetUserByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value. Please try again.");
                }
                if (index >= StaticDb.Users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.Users[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        // Add POST method that create new user

        [HttpPost]
        public IActionResult CreateUser([FromBody] string newUser)
        {
            try
            {
                if (string.IsNullOrEmpty(newUser))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The body of the request can not be empty");
                }
                StaticDb.Users.Add(newUser);
                return StatusCode(StatusCodes.Status201Created, "New user was created!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Add DELETE method that delete user

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The body of the request can not be empty");
            }
            if (!StaticDb.Users.Contains(user))
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user was not found");
            }

            StaticDb.Users.Remove(user);
            return StatusCode(StatusCodes.Status204NoContent);
        }


    }
}
