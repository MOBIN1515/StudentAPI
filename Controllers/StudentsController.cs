/////*using AutoMappe*/r;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.DTO;
using StudentAPI.Repository;
using StudentAPI.Unit;
using StudentAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DTO;
using System.Threading.Tasks;
using StudentAPI.Application.Services;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var x = await _service.GetAllAsync();
        return Ok(x);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _service.GetByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateStudentDto dto)
    {
        var createdStudent = await _service.CreateAsync(dto);
        return CreatedAtAction(
            nameof(Get),            
            new { id = createdStudent.Id }, 
            createdStudent        
        );
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStudentDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] StudentQueryDto query)
    {
        var result = await _service.GetPagedAsync(query);
        return Ok(result);
    }

}
