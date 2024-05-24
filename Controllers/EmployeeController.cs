using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendProj.Context;
using BackendProj.Model;
using Microsoft.AspNetCore.Mvc;

namespace BackendProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public ProjectDBContext _projectDBContext;
        public EmployeeController(ProjectDBContext projectDBContext)
        {
            _projectDBContext = projectDBContext;
        }
        [HttpGet("GetEmployees")]
        public IActionResult Get()
        {
            try
            {
                var result = _projectDBContext.EmpolyeesTable.ToList();
                return StatusCode(200 ,result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveEmployee")]
        public IActionResult Save(Empolyee input)
        {
            try
            {
                var result = _projectDBContext.EmpolyeesTable.Add(input);
                _projectDBContext.SaveChanges();
                return StatusCode(200 ,"Employee Added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Login")]
        public IActionResult GetLoginUser(string Email , string Password)
        {
            try
            {
                var result = _projectDBContext.EmpolyeesTable.Where(x => x.Email == Email).Count();
                if(result > 0)
                {
                    var UserExist = _projectDBContext.EmpolyeesTable.Where(x => x.Email == Email && x.Password == Password).ToList();
                    if(UserExist.Count > 0)
                    {
                       return Ok( UserExist[0] );
                    }
                    else
                    {
                        return Ok( "Password Incorrect");
                    }
                }
                else
                {
                   return Ok( "User Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}