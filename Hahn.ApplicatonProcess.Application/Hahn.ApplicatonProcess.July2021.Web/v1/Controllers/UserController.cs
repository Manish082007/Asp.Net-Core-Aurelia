using System;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicatonProcess.July2021.Web.v1.Controllers
{
  [Produces("application/json")]
  [ApiVersion("1.0")]
  [Route("api/v{version:ApiVersion}/[controller]")]
  [ApiController]
  public class UserController : Controller
  {
    // GET: User/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
    public IActionResult Get(int id)
    {
      return Ok(new User() { Id = id });
    }

    // POST: User
    [HttpPost]
    [ProducesResponseType(typeof(Asset), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] User user)
    {
      return Created("api/User", user);
    }

    // PUT: User/5
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Edit(int id, [FromBody] User user)
    {
      throw new NotImplementedException();
    }

    // DELETE: User/5
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Asset), StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
      throw new NotImplementedException();
    }
  }
}
