using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeDataService.ViewModels
{
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email for forgot password
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
