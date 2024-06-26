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
        public ActionResult<Response<int>> Save(Empolyee input)
        {
            Response<int> response;
            try
            {
                var result = _projectDBContext.EmpolyeesTable.Add(input);
                _projectDBContext.SaveChanges();
                int generatedId = result.Entity.EmpolyeeID;
                response = new Response<int>(generatedId , "Employee Added successfully");
                return Ok(response);
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
        [HttpGet("GetPositionList")]
        public ActionResult<Response<List<PositionList>>> GetPositionList()
        {
            Response<List<PositionList>> response;
            try
            {
                List<PositionList> Position = new();
                Position.AddRange(new List<PositionList>
                {
                    new PositionList
                    {
                        PositionID = 1,
                        Position = "Manager",
                        Description = "Chief Executive Officer",
                        ParentPositionID = 0,
                        ChildPosition =
                        [
                            new PositionCategoriesList
                            {
                                PositionID = 2,
                                Position = "Manager Director",
                                Description = "CEO",
                                ParentPositionID = 1
                            },
                            new PositionCategoriesList
                            {
                                PositionID = 11,
                                Position = "Assistant Manager",
                                Description = "Assistant to the CEO",
                                ParentPositionID = 1
                            }
                        ]
                    },
                    new PositionList
                    {
                        PositionID = 3,
                        Position = "CFO",
                        Description = "Chief Financial Officer",
                        ParentPositionID = 0,
                        ChildPosition =
                        [
                            new PositionCategoriesList
                            {
                                PositionID = 4,
                                Position = "Finance Manager",
                                Description = "Manager of Finance",
                                ParentPositionID = 3
                            },
                            new PositionCategoriesList
                            {
                                PositionID = 5,
                                Position = "Accountant",
                                Description = "Senior Accountant",
                                ParentPositionID = 3
                            }
                        ]
                    },
                    new PositionList
                    {
                        PositionID = 6,
                        Position = "Employees",
                        Description = "Daily Staffs",
                        ParentPositionID = 0,
                        ChildPosition =
                        [
                            new PositionCategoriesList
                            {
                                PositionID = 7,
                                Position = "Trainee",
                                Description = "Begginers",
                                ParentPositionID = 6
                            },
                            new PositionCategoriesList
                            {
                                PositionID = 9,
                                Position = "Software Engineers",
                                Description = "Developers",
                                ParentPositionID = 6
                            },
                            new PositionCategoriesList
                            {
                                PositionID = 10,
                                Position = "Project Manager",
                                Description = "Googling and OP",
                                ParentPositionID = 6
                            },
                            new PositionCategoriesList
                            {
                                PositionID = 11,
                                Position = "Reporting Manager",
                                Description = "Full Time OP",
                                ParentPositionID = 6
                            }
                        ]
                    }
                });
                response = new Response<List<PositionList>>(Position , "Data Retrived");
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}