using Microsoft.EntityFrameworkCore;
using WebApiDemoAK.Data;
using WebApiDemoAK.Entities;
using WebApiDemoAK.Repositories.Asbtract;

namespace WebApiDemoAK.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _studentContext;

        public StudentRepository(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public void Add(Student entity)
        {
            _studentContext.Students.Add(entity);
            _studentContext.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _studentContext.Students.Remove(entity);
            _studentContext.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            var students = _studentContext.Students;
            return students;
        }

        public Student GetById(int id)
        {
            var student = _studentContext.Students.FirstOrDefault(s => s.Id == id);
            return student;
        }

        public void Update(Student entity)
        {
            _studentContext.Entry(entity).State = EntityState.Modified;
            _studentContext.SaveChanges();
        }
    }
}
