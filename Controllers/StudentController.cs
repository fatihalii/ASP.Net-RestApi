using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Final.Controllers
{
    public class StudentController : ApiController
    {

        public IHttpActionResult GetAllStudents()
        {
            var students = new List<Student>();

            try
            {
                using (var context = new SchoolDBEntities())
                {
                    students = context.Students.ToList();
                    if (students.Count > 0)
                    {
                        return Ok(students);
                    }
                    else
                    {
                        return NotFound();

                    }
                }

            }
            catch (Exception)
            {

                throw;

            }
        }

        public IHttpActionResult PostNewStudent(Student student)
        {
         
            if(!ModelState.IsValid)
                return BadRequest("Invalid data.");

                using(var context = new SchoolDBEntities())
                {
                    context.Students.Add(student);
                    var numberOfInserted = context.SaveChanges();
                    if (numberOfInserted > 0)
                    {
                        return Ok(student);  
                    }
                    else { return BadRequest(); }
                }
            
          
        }

        public IHttpActionResult PutUpdateStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var context = new SchoolDBEntities())
            {
                var existingStudent = context.Students.Where(s => s.Id == student.Id).FirstOrDefault<Student>();

                if(existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    var numberOfInserted = context.SaveChanges();
                    if (numberOfInserted > 0)
                    {
                        return Ok(student);
                    }
                    else { return BadRequest(); }

                }
            }
            return null;
        }


        public IHttpActionResult DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using(var context = new SchoolDBEntities())
            {
                var student = context.Students.FirstOrDefault(x => x.Id == id);
                if(student != null)
                {
                    context.Students.Remove(student);
                    var numberOfDeleted = context.SaveChanges();
                    if (numberOfDeleted > 0)
                        return Ok();


                }
                return BadRequest();
            }
        }


    }
}
