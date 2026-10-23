using Microsoft.AspNetCore.Mvc;
using BeeMart.backend.Data;
using BeeMart.backend.Models;
using BeeMart.backend.DTO;

namespace BeeMart.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            // Validasi Password dan Re-Password
            if (request.Password != request.RePassword)
            {
                return BadRequest(new { message = "Password and Re-Password do not match" });
            }

            // Validasi Username duplikat
            bool userExists = _context.MsUsers.Any(u => u.Username == request.Username);
            if (userExists)
            {
                return BadRequest(new { message = "username has already exists" });
            }

            // Generate IdUser otomatis (misal: U0001, U0002)
            int userCount = _context.MsUsers.Count();
            string newId = "U" + (userCount + 1).ToString("D4");

            // Hashing Password menggunakan BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new MsUser
            {
                IdUser = newId,
                Username = request.Username,
                FullName = request.FullName,
                PasswordHash = hashedPassword,
                Role = request.Role,
                Stsrc = true,
                Datein = DateTime.Now,
                Userin = "SYSTEM"
            };

            _context.MsUsers.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            // Cari user berdasarkan Username dan Role
            var user = _context.MsUsers
                .FirstOrDefault(u => u.Username == request.Username && u.Role == request.Role);

            // Validasi kombinasi Username, Role, dan pencocokan Password
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid login" });
            }

            // Jika sukses, kembalikan data user (tanpa password)
            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    idUser = user.IdUser,
                    username = user.Username,
                    fullName = user.FullName,
                    role = user.Role
                }
            });
        }
    }
}