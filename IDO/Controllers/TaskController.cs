using IDO.Data;
using IDO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDO.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;

        public TaskController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtSecret = configuration["Jwt:Secret"];
        }


        [HttpGet]
        public async Task<ActionResult<object>> GetTasks(int userId)
        {

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return Unauthorized("Missing authorization token");
            }

            try
            {
              
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret); 
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, 
                    ValidateAudience = false,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return BadRequest("No user ID provided");
                }
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                if (_context.Tasks == null)
                {
                    return NotFound();
                }

                var tasks = await _context.Tasks
                   .Include(t => t.TaskImportance)
                   .Include(t => t.Status)
                   .Where(t => t.userId == userId)
                   .Select(t => new ToDoTaskDto
                   {
                       toDoId = t.Id,
                       toDoTitle = t.taskTitle,
                       toDoStatus = t.Status.StatusName,
                       toDoImportance = t.TaskImportance.taskImportanceName,
                       toDoCategory = t.taskCategory,
                       toDoDueDate = t.taskDueDate,
                       toDoEstimate = t.taskEstimate
                   })
                   .ToListAsync();

                var response = new { status = "success", data = tasks };
                return response;
            }
            catch (SecurityTokenException)
            {
                
                return Unauthorized("Invalid authorization token");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTaskDto>> GetTask(int id)
        {

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return Unauthorized("Missing authorization token");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
            }
            catch (SecurityTokenException)
            {

                return Unauthorized("Invalid authorization token");
            }

            var task = await _context.Tasks
                .Include(t => t.TaskImportance)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.Id == id);


            if (task == null)
            {
                return NotFound("Task not found");
            }

            var taskDto = new ToDoTaskDto
            {
                toDoId = task.Id,
                toDoTitle = task.taskTitle,
                toDoStatus = task.Status.StatusName,
                toDoImportance = task.TaskImportance.taskImportanceName,
                toDoCategory = task.taskCategory,
                toDoDueDate = task.taskDueDate,
                toDoEstimate = task.taskEstimate
            };

            return taskDto;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, ToDoTaskDto taskDto)
        {

                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return Unauthorized("Missing authorization token");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
            }
            catch (SecurityTokenException)
            {

                return Unauthorized("Invalid authorization token");
            }





            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid inputs");
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            task.taskTitle = taskDto.toDoTitle;
            task.taskCategory = taskDto.toDoCategory;
            task.taskDueDate = taskDto.toDoDueDate;
            task.taskEstimate = taskDto.toDoEstimate;

           
            var importance = await _context.TaskImportances.FirstOrDefaultAsync(i => i.taskImportanceName == taskDto.toDoImportance);
            if (importance == null)
            {
                return BadRequest("Invalid importance name");
            }
            task.taskImportanceId = importance.taskImportanceId; 

            var status = await _context.ToDoTaskStatuses.FirstOrDefaultAsync(i => i.StatusName == taskDto.toDoStatus);
            if(status == null)
            {
                return BadRequest("Invalid status name");
            }
            task.StatusId = status.StatusId;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { status = "success" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound("Task not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoTaskDto>> PostItem(int userId , ToDoTaskDto taskDto)
        {

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return Unauthorized("Missing authorization token");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
            }
            catch (SecurityTokenException)
            {

                return Unauthorized("Invalid authorization token");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid inputs");
            }

            if (_context.Tasks == null)
            {
                return Problem("Entity set is null.");
            }


            var taskTitle = taskDto.toDoTitle;
            var taskCategory = taskDto.toDoCategory;
            var taskStatus = taskDto.toDoStatus;
            var taskDueDate = taskDto.toDoDueDate;
            var taskEstimate = taskDto.toDoEstimate;
            var taskImportanceName = taskDto.toDoImportance;


            var taskImportance = await _context.TaskImportances
                .FirstOrDefaultAsync(ti => ti.taskImportanceName == taskImportanceName);

            if (taskImportance == null)
            {
                return NotFound("Task importance not found");
            }

            var status = await _context.ToDoTaskStatuses.FirstOrDefaultAsync(i => i.StatusName == taskStatus);
            if (status == null)
            {
                return BadRequest("Invalid status name");
            }
             var StatusId = status.StatusId;

            var newTask = new ToDoTask
            {
                taskTitle = taskTitle,
                taskCategory = taskCategory,
                taskDueDate = taskDueDate,
                taskEstimate = taskEstimate,
                StatusId = StatusId,
                taskImportanceId = taskImportance.taskImportanceId,
                userId = userId,
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = newTask.Id }, new
            {
                status = "success",
                data = new ToDoTaskDto
                {
                    toDoId = newTask.Id,
                    toDoTitle = taskTitle,
                    toDoCategory = taskCategory,
                    toDoDueDate = taskDueDate,
                    toDoEstimate = taskEstimate,
                    toDoStatus = status.StatusName,
                    toDoImportance = taskImportance.taskImportanceName,

                }
            });
        }

    }
}
