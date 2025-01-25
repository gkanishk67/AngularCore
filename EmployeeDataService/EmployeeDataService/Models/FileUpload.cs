using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace EmployeeDataService.Models
{
    public class FileUpload
    {
        /// <summary>
        /// File Uploaded
        /// </summary>
        public IFormFile files { get; set; }
    }
}
