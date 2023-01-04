using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]

public class AccountRolesController : ControllerBase
{
	//Dependency Injection
	private AccountRoleRepositories _repo;

    public AccountRolesController(AccountRoleRepositories repo)
    {
        _repo = repo;
    }

    // Get menampilkan data
    [HttpGet]
    public ActionResult GetAll()
    {
        try
        {
            var result = _repo.Get();
            return result.Count() == 0
            ? Ok(new { statusCode = 204, message = "Data Not Found!" })
            : Ok(new { statusCode = 201, message = "Success", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }

    }

    //penampilkan data Id
    [HttpGet]
    [Route("{id}")]
    public ActionResult GetById(int id)
    {
        try
        {
            var result = _repo.Get(id);
            return result == null
            ? Ok(new { statusCode = 204, message = $"Data With Id {id} Not Found!" })
            : Ok(new { statusCode = 201, message = $"Id {id}!", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

    // Input data 
    [HttpPost]
    [Authorize]
    public ActionResult Insert(AccountRole accountrole)
    {
        try
        {
            var result = _repo.Insert(accountrole);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "" });
        }
    }

    // update data 
    [HttpPut]
    public ActionResult Update(AccountRole accountrole)
    {
        try
        {
            var result = _repo.Update(accountrole);
            return result == 0 ?
            Ok(new { statusCode = 204, message = $"Id {accountrole.Id} not found!" })
          : Ok(new { statusCode = 201, message = "Update Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
        }
    }

    //hapus data 
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        try
        {
            var result = _repo.Delete(id);
            return result == 0 ? Ok(new { statusCode = 204, message = $"Id {id} Data Not Found" }) :
            Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
        }
    }
}
