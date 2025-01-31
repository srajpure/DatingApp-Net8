using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private DataContext _dbContext = null;
        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public  async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int Id)
        {
            var User  =await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id == Id);

            if(User == null)
            {
                return NotFound();
            }
            return Ok(User);

        }


    }
}
