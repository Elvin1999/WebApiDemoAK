using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDemoAK.Data;
using WebApiDemoAK.Dtos;
using WebApiDemoAK.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemoAK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var students = await _context.Students.ToListAsync();
            var dtos = students.Select(s =>
            {
                return new StudentDto
                {
                    Fullname = s.Name + " " + s.Surname,
                    Age = s.Age
                };
            });
            return dtos;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (student != null)
            {
                var dto = new StudentDto
                {
                    Fullname = student.Name + " " + student.Surname,
                    Age = student.Age
                };
                return Ok(dto);
            }
            return BadRequest();
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentAddDto dto)
        {
            try
            {
                var student = new Student
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Age = dto.Age
                };
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentUpdateDto dto)
        {
            try
            {
                var item = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (item != null)
                {
                    item.Name = dto.Name;
                    item.Surname = dto.Surname;
                    item.Age = dto.Age;

                    _context.Students.Update(item);
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (item != null)
                {
                    _context.Remove(item);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
