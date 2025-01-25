using System;
using System.Collections.Generic;
using System.Text;
using EmployeeDataService.Models;
using EmployeeDataService.ViewModels;

namespace EmployeeDataService.Manager.Employee
{
    public interface IEmployeeManager
    {
        #region === [ Declarations ] ===========================================================
        /// <summary>
        /// For Employee Keyword Search
        /// </summary>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        List<EmployeeModels> EmployeeKeywordSearch(string Keyword);


        /// <summary>
        /// For Employee Sorting
        /// </summary>
        /// <param name="SortColumn"></param>
        /// <param name="IconClass"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        List<EmployeeModels> EmployeeSorting(string SortColumn, string IconClass, List<EmployeeModels> employees);


        /// <summary>
        /// Employee listing custom sorting
        /// </summary>
        /// <param name="PageNo"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        List<EmployeeModels> CustomPaging(int PageNo, List<EmployeeModels> employees);


        /// <summary>
        /// For Employee reporting drowdown 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        List<EmployeeModels> EmployeeReportingDDL(int? EmployeeId);


        /// <summary>
        /// Returns Employee with specified ID
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        EmployeeModels GetEmployeeDetails(int EmployeeId);


        /// <summary>
        /// For Dropdown selected value
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        EmployeeModels EmployeeDDLselected(EmployeeModels Employee);


        /// <summary>
        /// List of Employees reporting specified ID
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        List<EmployeeModels> EmployeeReporting(int EmployeeId);


        /// <summary>
        /// Checks if email already exists or not
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        bool EmployeeEmailVal(string Email);

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="viewModel"></param>
        void AddEmployee(EmployeeFileViewModel viewModel);

        /// <summary>
        /// Edit Employee
        /// </summary>
        /// <param name="viewModel"></param>
        void EditEmployee(EmployeeFileViewModel viewModel);


        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="Employee"></param>
        void RemoveEmployee(EmployeeModels Employee);

        /// <summary>
        /// Gets Ip of user
        /// </summary>
        /// <returns></returns>
        string GetIp();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetEmployeesAPI();
        #endregion
        //########################################################################
    }
}
