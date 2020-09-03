using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserWebServiceProject2.Models;

namespace UserWebServiceProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> users = new List<User>();
        private static int currentId = 101;
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            // return new string[] { "value1", "value2" };
            return users;
        }

        // GET: api/<UserController>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return new OkObjectResult(user);
        }

        // POST: api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            value.Id = currentId++;
            value.DateCreated = DateTime.Now;

            users.Add(value);

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT: api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(); // 404
            }

            user.Id = id;
            user.Email = value.Email;
            user.Password = value.Password;

            return Ok(user);
        }

        // DELETE: api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usersRemoved = users.RemoveAll(u => u.Id == id);

            if (usersRemoved == 0)
            {
                return NotFound(); // 404
            }

            return Ok();
        }
    }
}
