using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeePayroll_MVC.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeAge { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate {get; set; }
    }
}
