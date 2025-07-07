using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _repo;
        public StudentController(IConfiguration configuration)
        {
            _repo = new StudentRepository(configuration);
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            var result = _repo.AddStudent(student);
            return Ok(result);
        }


    }
}
