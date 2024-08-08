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
                var result = _projectDBContext.EmpolyeesTable.OrderByDescending(x => x.EmpolyeeID).ToList();
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
                if(input.EmpolyeeID == 0)
                {
                    var empList = _projectDBContext.EmpolyeesTable.Where(x => x.Email == input.Email).ToList();
                    if(empList.Count == 0)
                    {
                        var result = _projectDBContext.EmpolyeesTable.Add(input);
                        _projectDBContext.SaveChanges();
                        int generatedId = result.Entity.EmpolyeeID;
                        response = new Response<int>(generatedId , "Employee Added successfully");
                    }
                    else
                    {
                        response = new Response<int>(0 , "Email is Already Exists");
                    }  
                }
                else
                {
                    var result = _projectDBContext.EmpolyeesTable.Update(input);
                    _projectDBContext.SaveChanges();
                    int generatedId = result.Entity.EmpolyeeID;
                    response = new Response<int>(generatedId , "Employee Updated successfully");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteEmployee/{EmployeeID}")]
        public ActionResult<Response<int>> DeleteEmployee(int EmployeeID)
        {
            Response<int> response;
            try
            {
                var employee = _projectDBContext.EmpolyeesTable.Where(x => x.EmpolyeeID == EmployeeID).FirstOrDefault();
                if(employee != null)
                {
                    var result = _projectDBContext.EmpolyeesTable.Remove(employee);
                    _projectDBContext.SaveChanges();
                    response = new Response<int>(EmployeeID , "Employee Deleted successfully");
                }
                else
                {
                    response = new Response<int>(EmployeeID , "Employee Not Found");
                }
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

        [HttpGet("GetMenu")]
        public ActionResult<Response<List<MainMenu>>> GetMenuList()
        {
            Response<List<MainMenu>> response;
            try
            {
                List<MainMenu> Position = new();
                Position.AddRange(new List<MainMenu>
                {
                    new MainMenu
                    {
                        MainMenuID = 1,
                        MenuName = "Home",
                        Url="/Home",
                        Icon="fa-fa-Home",
                        subMenu =null
                    },
                    new MainMenu
                    {
                        MainMenuID = 2,
                        MenuName = "Employee",
                        Url="",
                        Icon="fa-solid fa-person",
                        subMenu =
                        [
                            new SubMenu
                            {
                                SubMenuID = 1,
                                MainMenuID = 2,
                                MenuName = "Empoyee List",
                                Url = "/Home/EmployeeList",
                                Icon="fa-solid fa-briefcase"
                            },
                            new SubMenu
                            {
                                SubMenuID = 2,
                                MainMenuID = 2,
                                MenuName = "Add Employee",
                                Url = "/Home/AddEmployee",
                                Icon="fa-solid fa-plus"
                            }
                        ]
                    },
                    new MainMenu
                    {
                        MainMenuID = 3,
                        MenuName = "Profile",
                        Url="",
                        Icon="fa-solid fa-tag",
                        subMenu =
                        [
                            new SubMenu
                            {
                                SubMenuID = 3,
                                MainMenuID = 3,
                                MenuName = "Edit Profile",
                                Url = "/Home/EmployeeList",
                                Icon="fa-solid fa-book"
                            }
                        ]
                    }
                });
                response = new Response<List<MainMenu>>(Position , "Data Retrived");
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}