using EmployeeDataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDataService.ViewModels
{
    public class EmployeeFileViewModel
    {
        /// <summary>
        /// ViewModel Constructor
        /// </summary>
        public EmployeeFileViewModel()
        {

            this.Emp = new EmployeeModels();
            this.File = new FileUpload();

        }

        #region === [ Object Properties ] ===========================================================
        /// <summary>
        /// Model Employee
        /// </summary>
        public EmployeeModels Emp { get; set; }


        /// <summary>
        /// Model File upload
        /// </summary>
        public FileUpload File { get; set; }
        #endregion
    }
}
