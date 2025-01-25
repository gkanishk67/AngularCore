using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmployeeDataService.Data;
using EmployeeDataService.Models;
using EmployeeDataService.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace EmployeeDataService.Manager.Employee
{
    public class EmployeeManager : IEmployeeManager
    {
        #region === [ Default Parameters ] ===========================================================
        /// <summary>
        /// Dbcontext instance
        /// </summary>
        EmployeesAppDBContext db = new EmployeesAppDBContext();
        private HttpContext Current => new HttpContextAccessor().HttpContext;

        /// <summary>
        /// Constructor Employee manager
        /// </summary>
        public EmployeeManager()
        {
        }
        #endregion
        //########################################################################


        #region === [ Add Employee ] ===========================================================
        /// <summary>
        /// Add employee on Post request
        /// </summary>
        /// <param name="viewModel"></param>
        public void AddEmployee(EmployeeFileViewModel viewModel)
        {
            EmployeeModels emp = viewModel.Emp;
            FileUpload fileUpload = viewModel.File;
            if (fileUpload.files != null)
            {
                emp.FileName = fileUpload.files.FileName;
                emp.FileMimeType = fileUpload.files.ContentType;
                emp.FileSize = fileUpload.files.Length; // ContentLength;
                using (var fs = fileUpload.files.OpenReadStream()) //InputStream
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        emp.FileData = br.ReadBytes((Int32)fs.Length);
                    }
                }
            }
            //emp.IpCreated = GetIp();
            emp.UserCreated = Environment.UserName;
            emp.MachineCreated = Environment.MachineName;
            emp.DateCreated = DateTime.Now.ToString();
            db.EmployeeModels.Add(emp);
            db.SaveChanges();
        }
        #endregion
        //########################################################################


        #region === [ Custom Paging ] ===========================================================
        /// <summary>
        /// Custom Paging on Index Page
        /// </summary>
        /// <param name="PageNo"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public List<EmployeeModels> CustomPaging(int PageNo, List<EmployeeModels> employees)
        {
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(employees.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            //ViewBag.PageNo = PageNo;
            //ViewBag.NoOfPages = NoOfPages;
            employees = employees.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return employees;
        }
        #endregion
        //########################################################################


        #region === [ Edit Employee ] ===========================================================
        /// <summary>
        /// Updation of Employee on Post request
        /// </summary>
        /// <param name="viewModel"></param>
        public void EditEmployee(EmployeeFileViewModel viewModel)
        {
            EmployeeModels emp = viewModel.Emp;
            FileUpload fileUpload = viewModel.File;
            EmployeeModels Employee = db.EmployeeModels.Where(temp => temp.Id == emp.Id).FirstOrDefault();
            Employee.FirstName = emp.FirstName;
            Employee.MiddleName = emp.MiddleName;
            Employee.LastName = emp.LastName;
            Employee.Father = emp.Father;
            Employee.Email = emp.Email;
            Employee.DOB = emp.DOB;
            Employee.Program = emp.Program;
            Employee.Region = emp.Region;
            Employee.Address = emp.Address;
            Employee.Address2 = emp.Address2;
            Employee.City = emp.City;
            Employee.State = emp.State;
            Employee.Zip = emp.Zip;
            Employee.Contact = emp.Contact;
            Employee.Gender = emp.Gender;
            Employee.Reporting = emp.Reporting;
            Employee.IpModified = emp.IpModified;
            Employee.UserModified = Environment.UserName;
            Employee.MachineModified = Environment.MachineName;
            Employee.DateModified = DateTime.Now.ToString();
            if (fileUpload.files != null)
            {
                Employee.FileName = fileUpload.files.FileName;
                Employee.FileMimeType = fileUpload.files.ContentType;
                Employee.FileSize = fileUpload.files.Length;
                using (Stream fs = fileUpload.files.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Employee.FileData = br.ReadBytes((Int32)fs.Length);
                    }
                }
            }


            db.SaveChanges();
        }
        #endregion
        //########################################################################


        #region === [ DDL reporting selected value ] ===========================================================
        /// <summary>
        /// Selected value at dropdown 
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public EmployeeModels EmployeeDDLselected(EmployeeModels Employee)
        {
            return db.EmployeeModels.Where(temp => temp.Id == Employee.Reporting).FirstOrDefault();
        }
        #endregion
        //########################################################################


        #region === [ Email Validation ] ===========================================================
        /// <summary>
        /// Email check if alread exists
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool EmployeeEmailVal(string Email)
        {
            return db.EmployeeModels.Any(a => a.Email.ToLower() == Email.ToLower());
        }
        #endregion
        //########################################################################


        #region === [ KeyWord Search ] ===========================================================

        /// <summary>
        /// Employee Search with Keyword
        /// </summary>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public List<EmployeeModels> EmployeeKeywordSearch(string Keyword)
        {
            if (Keyword == null) { Keyword = ""; }
            return db.EmployeeModels.Where(temp => temp.FirstName.Contains(Keyword)).ToList();
        }
        #endregion
        //########################################################################


        #region === [ Reporting Employee List ] ===========================================================
        /// <summary>
        /// Returns List of reporting Employee
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public List<EmployeeModels> EmployeeReporting(int EmployeeId)
        {
            return db.EmployeeModels.Where(temp => temp.Reporting == EmployeeId).ToList();
        }
        #endregion
        //########################################################################


        #region === [ Reporting DDL ] ===========================================================
        /// <summary>
        /// Dropdown List binding
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public List<EmployeeModels> EmployeeReportingDDL(int? EmployeeId)
        {
            if (EmployeeId == null)
            {
                return db.EmployeeModels.Where(temp => temp.FirstName != null).ToList();
            }
            else
            {
                return db.EmployeeModels.Where(temp => temp.Id != EmployeeId).ToList();
            }
        }
        #endregion
        //########################################################################


        #region === [ Employee Sorting ] ===========================================================
        /// <summary>
        /// Return sorted columns 
        /// </summary>
        /// <param name="SortColumn"></param>
        /// <param name="IconClass"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public List<EmployeeModels> EmployeeSorting(string SortColumn, string IconClass, List<EmployeeModels> employees)
        {
            switch (SortColumn)
            {
                case "FirstName":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.FirstName).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.FirstName).ToList(); }
                    break;
                case "Email":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.Email).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.Email).ToList(); }
                    break;
                case "DOB":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.DOB).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.DOB).ToList(); }
                    break;
                case "Contact":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.Contact).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.Contact).ToList(); }
                    break;
                case "Gender":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.Gender).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.Gender).ToList(); }
                    break;
                case "Program":
                    if (IconClass == "fa-sort-asc")
                    { employees = employees.OrderBy(temp => temp.Program).ToList(); }
                    else
                    { employees = employees.OrderByDescending(temp => temp.Program).ToList(); }
                    break;

            }

            return employees;
        }
        #endregion
        //########################################################################


        #region === [ Gets Employee Details ] ===========================================================
        /// <summary>
        /// Returns object for specific ID
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public EmployeeModels GetEmployeeDetails(int EmployeeId)
        {

            return db.EmployeeModels.Where(temp => temp.Id == EmployeeId).FirstOrDefault();
        }
        #endregion
        //########################################################################


        #region === [Delete Employee ] ===========================================================
        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="Employee"></param>
        public void RemoveEmployee(EmployeeModels Employee)
        {
            db.EmployeeModels.Remove(Employee);
            db.SaveChanges();
        }
        #endregion
        //########################################################################

        #region === [Get Ip] ===========================================================
        /// <summary>
        /// Gets Ip of User
        /// </summary>
        /// <returns></returns>
        public string GetIp()
        {

            //var remoteIpAddress = HttpContext.GetFeature<IHttpConnectionFeature>()?.RemoteIpAddress;
            var ip =    Current.Connection.RemoteIpAddress.ToString();    //Current.Request.HttpContext.se .Request.HttpContext.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = Current.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
        #endregion
        //########################################################################

        #region === [Get API] ===========================================================
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetEmployeesAPI()
        {
            return db.EmployeeModels.Where(temp => temp.FirstName.Contains(string.Empty)).Select(temp =>            
                temp.FirstName
            ).ToList();
        }
        #endregion
    }
}
