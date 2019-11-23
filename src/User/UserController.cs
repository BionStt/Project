using Microsoft.AspNetCore.Mvc;

namespace Project
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            _userService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            var result = _userService.Get(id);

            if (result == default)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _userService.List();

            if (result == default)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPatch("{id}/email")]
        public IActionResult PatchEmail(int id, UserModel model)
        {
            if (id == default || model == default)
            {
                return BadRequest();
            }

            _userService.UpdateEmail(model);

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(UserModel model)
        {
            if (model == default)
            {
                return BadRequest();
            }

            _userService.Add(model);

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserModel model)
        {
            if (id == default || model == default)
            {
                return BadRequest();
            }

            model.Id = id;

            _userService.Update(model);

            return NoContent();
        }
    }
}
