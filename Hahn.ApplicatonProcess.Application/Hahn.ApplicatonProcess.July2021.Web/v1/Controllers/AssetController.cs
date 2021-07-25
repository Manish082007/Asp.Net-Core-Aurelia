using System.Collections.Generic;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicatonProcess.July2021.Web.v1.Controllers
{
  [Produces("application/json")]
  [ApiVersion("1.0")]
  [Route("api/v{version:ApiVersion}/[controller]")]
  [ApiController]
  public class AssetController : Controller
  {
    // GET: Asset/bitcoin
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
    public IActionResult Get(string id)
    {
      return Ok(new Asset() { Name = id });
    }

    // GET: Asset
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Asset>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
      return Ok(new Asset());
    }
  }
}
