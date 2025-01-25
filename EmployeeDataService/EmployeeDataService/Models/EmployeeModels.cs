using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDataService.Models
{
    public partial class EmployeeModels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Father { get; set; }
        [Required]
        public string Region { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[Remote("IsAlreadyExists", "Default", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }
        [Required]
        [Column("DOB")]
        public string DOB { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Program { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
        public string MachineCreated { get; set; }
        public string MachineModified { get; set; }
        public string IpCreated { get; set; }
        public string IpModified { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public int? Reporting { get; set; }
        public string FileName { get; set; }
        public string FileMimeType { get; set; }
        public long? FileSize { get; set; }
        public byte[] FileData { get; set; }
        public string MiddleName { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Column("ZIP")]
        public string Zip { get; set; }
    }


    public enum Region
    {
        India,
        Russia,
        China,
        USA,
        Turkey,
        Pakistan

    }

}
