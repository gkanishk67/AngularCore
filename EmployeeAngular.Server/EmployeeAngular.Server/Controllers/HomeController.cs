
namespace EmployeeAngular.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using EmployeeDataService.Manager.Employee;
    using EmployeeDataService.Models;
    using EmployeeDataService.ViewModels;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IEmployeeManager employeeManager;


        #region === [ API for Employee operations] ===========================================================

        /// <summary>
        /// Constructor with dependency
        /// </summary>
        /// <param name="_employeeManager"></param>
        public HomeController(IEmployeeManager employeeManager)
        {
            this.employeeManager = employeeManager;
        }

        /// <summary>
        /// Fetches list of employees
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Get()
        {
            List<EmployeeModels> employees = employeeManager.EmployeeKeywordSearch("");
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = null
                }
            };

            var json = JsonConvert.SerializeObject(employees, serializerSettings);
            return Content(json, "application/json");
            //return Ok(employees);
        }


        /// <summary>
        /// Add employee Post form
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Authorize]
        public ActionResult Create(EmployeeModels employee)
        {
            var req = Request;
            EmployeeFileViewModel employeeFileViewModel = new EmployeeFileViewModel();
            employeeFileViewModel.Emp = employee;
            employeeManager.AddEmployee(employeeFileViewModel);
            return Ok();

        }



        /// <summary>
        /// Gets Edit bind details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult<EmployeeModels>Edit(int Id)
        {
            try
            {
               
                //Employee Details
                EmployeeModels Employee = employeeManager.GetEmployeeDetails(Id);
                if (Employee is null)
                {

                    return NotFound();
                }


                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = null
                    }
                };

                var json = JsonConvert.SerializeObject(Employee, serializerSettings);
                return Content(json, "application/json");

                //return Ok(Employee);
            }
            catch (Exception ex)
            {
               
                return NotFound();
            }

        }


        /// <summary>
        /// Put API for Updating employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Edit")]
        public ActionResult Edit(EmployeeModels employee)
        {
            var req = Request;
            EmployeeFileViewModel viewModel = new EmployeeFileViewModel();
            viewModel.Emp = employee;
            employeeManager.EditEmployee(viewModel);
            return Ok();

        }


        /// <summary>
        /// Delete API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            EmployeeModels employee = employeeManager.GetEmployeeDetails(id);
            if (employee == null)
            {
                return NotFound();
            }

            employeeManager.RemoveEmployee(employee);

            return Ok();
        }
    }
    #endregion
        //############################################################################################

}
