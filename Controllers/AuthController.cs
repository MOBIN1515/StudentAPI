//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly IConfiguration _config;

//    public AuthController(IConfiguration config)
//    {
//        _config = config;
//    }

//    [HttpPost("login")]
//    public IActionResult Login([FromBody] UserLogin login)
//    {
//        if (login.Username == "admin" && login.Password == "123")
//        {
//            var key = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
//            );

//            var creds = new SigningCredentials(
//                key,
//                SecurityAlgorithms.HmacSha256
//            );

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, login.Username),
//                new Claim(ClaimTypes.Role, "Admin")
//            };

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(1),
//                signingCredentials: creds
//            );

//            return Ok(new
//            {
//                token = new JwtSecurityTokenHandler().WriteToken(token)
//            });
//        }

//        return Unauthorized();
//    }
//}


//public class UserLogin
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}

