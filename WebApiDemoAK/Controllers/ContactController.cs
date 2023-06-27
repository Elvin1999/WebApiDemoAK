using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemoAK.Dtos;

namespace WebApiDemoAK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpGet("")]
        public List<ContactDTO> GetContacts()
        {
            return new List<ContactDTO>
            {
                new ContactDTO
                {
                    Id=1,Firstname="Tural",Lastname="Eliyev",PAN="4169855878789596"
                },
                new ContactDTO
                {
                    Id=2,Firstname="Mike",Lastname="Mammadli",PAN="4169225878789596"
                }
            };
        }
        //localhost:5050/Contact/First
        [HttpGet("First")]
        public ContactDTO GetFirst()
        {
            return new ContactDTO
            {
                Id = 1,
                Firstname = "Tural",
                Lastname = "Eliyev",
                PAN = "4169855878789596"
            };
        }
    }
}
