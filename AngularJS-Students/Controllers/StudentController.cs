using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AngularJSStudentGrid.Models;

namespace AngularJSStudentGrid.Controllers
{
    public class StudentController : ApiController
    {
        private StudentContext db = new StudentContext();

        // GET api/Student
        public IEnumerable<Student> GetStudents()
        {
            return db.Students.AsEnumerable();
        }

        // GET api/Student/5
        public Student GetStudent(int id)
        {
            Student Student = db.Students.Find(id);
            if (Student == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Student;
        }

        // PUT api/Student/5
        public HttpResponseMessage PutStudent(int id, Student Student)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != Student.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(Student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Student
        public HttpResponseMessage PostStudent(Student Student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(Student);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Student);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = Student.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Student/5
        public HttpResponseMessage DeleteStudent(int id)
        {
            Student Student = db.Students.Find(id);
            if (Student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Students.Remove(Student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}