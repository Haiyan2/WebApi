using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET api/employees Get()
        [HttpGet]
        public HttpResponseMessage Get(string gender = "All")
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                    case "male":
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(x => x.Gender.ToLower() == gender).ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "value for gender must be all, male  or female. It is invalid to use: " + gender);
                }

            }
        }

        // GET api/employees Get()
        /*
         [HttpGet]
        public IEnumerable<Employee> LoadAll()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }
        */

        // GET api/employees/id
        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(employee => employee.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id + " is not found.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }

        // Post
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id + " is not found.");
                    }
                    entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Employee with Id = " + id + " is deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /*. 
         * public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee emplyee)    
         * http://localhost:50882/api/employees?FirstName=Yan&LastName=Li&Gender=Male&Salary=90000
         * and in the body just input: 2.
         */

        /*
         * From Uri, either through route or by query string
         * Default binding, simple type through Uri complex type through body.
         * http://localhost:50882/api/employees?id=2
         * or http://localhost:50882/api/employees/2
         * with Body: {"FirstName":"Yan2","LastName":"Li","Gender":"Male","Salary":90000}
         */
        public HttpResponseMessage Put(int id, Employee emplyee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id + " is not found.");
                    }
                    entity.FirstName = emplyee.FirstName;
                    entity.LastName = emplyee.LastName;
                    entity.Gender = emplyee.Gender;
                    entity.Salary = emplyee.Salary;
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
   
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }

        }
    }
}
