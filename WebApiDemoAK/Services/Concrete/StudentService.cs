using WebApiDemoAK.Entities;
using WebApiDemoAK.Repositories.Asbtract;
using WebApiDemoAK.Services.Asbtract;

namespace WebApiDemoAK.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Add(Student entity)
        {
            _studentRepository.Add(entity);
        }

        public void Delete(int id)
        {
            var item = _studentRepository.GetById(id);
            _studentRepository.Delete(item);
        }

        public Student Get(int id)
        {
            var item = _studentRepository.GetById(id);
            return item;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public void Update(Student entity)
        {
            _studentRepository.Update(entity);
        }
    }
}
