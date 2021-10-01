using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentDataAccess;
using System.Web.Http.Cors;

namespace LearningWebAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class StudentController : ApiController
    {

        [BasicAuthentication]
        public HttpResponseMessage Get(int id)
        {
            using (DemoEntities entities = new DemoEntities())
            {
                var entity = entities.Students.FirstOrDefault(s => s.Id == id);

                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Student with id="+id.ToString()+"not found");
            }
        }
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender ="all")
        {
            using (DemoEntities entities = new DemoEntities())
            {
                switch (gender)
                {
                    case "all": return Request.CreateResponse(HttpStatusCode.OK, entities.Students.ToList());
             
                    case "male": return Request.CreateResponse(HttpStatusCode.OK, entities.Students.Where(s => s.Gender.ToLower() == "male").ToList());

                    case "female": return Request.CreateResponse(HttpStatusCode.OK, entities.Students.Where(s => s.Gender.ToLower() == "female").ToList());

                    default:return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Value for gender must be Male, Female or All. " + gender + " is invalid.");
                }
            }
        }










        [BasicAuthentication]
        public HttpResponseMessage Put(int id, [FromBody]Student student)
        {
            try
            {
                using (DemoEntities entities = new DemoEntities())
                {
                    var entity = entities.Students.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Name = student.Name;
                        entity.City = student.City;
                        entity.Gender = student.Gender;
                        

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

















        public HttpResponseMessage Post([FromBody] Student student)
        {
            try
            {
                using (DemoEntities entities = new DemoEntities())
                {
                    entities.Students.Add(student);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, student);
                    message.Headers.Location = new Uri(Request.RequestUri +"/"+ student.Id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }
}
