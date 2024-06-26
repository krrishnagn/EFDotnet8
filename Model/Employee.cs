using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProj.Model
{
    public class Empolyee 
    {
      [Key]
      public int EmpolyeeID { get; set; }
      public string? EmployeeNumber { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? Gender { get; set; }
      public DateTime? DOB { get; set; }
      public DateOnly? JoiningDate { get; set; }
      public string? Position { get; set; }
      public string? Email { get; set; }
      public string? Password { get; set; }
    }
    public class RtnEmpolyee 
    {
      [Key]
      public int EmpolyeeID { get; set; }
      public string? EmployeeNumber { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? Gender { get; set; }
      public DateTime? DOB { get; set; }
      public DateOnly? JoiningDate { get; set; }
      public string? Position { get; set; }
      public string? Email { get; set; }
      public string? Password { get; set; }
    }
    public class PositionList
    {
      public int PositionID { get; set; }
      public string? Position { get; set;}
      public string? Description { get; set; }
      public int ParentPositionID { get; set; }
      public List<PositionCategoriesList>? ChildPosition { get; set; }
    }
    public class PositionCategoriesList
    {
      public int PositionID { get; set; }
      public string? Position { get; set;}
      public string? Description { get; set; }
      public int ParentPositionID { get; set; }
    }
}