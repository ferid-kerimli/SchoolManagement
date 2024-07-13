using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.DTO.StudentDTOs;

namespace SchoolManagement.Controllers;

[Route("api/[controller][action]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(StudentCreateDTO studentCreateDto)
    {
        var result = await _studentService.CreateStudent(studentCreateDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var result = await _studentService.GetAllStudents();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var result = await _studentService.GetStudentById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(StudentUpdateDTO studentUpdateDto, int id)
    {
        var result = await _studentService.UpdateStudent(studentUpdateDto, id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStudent(int Id)
    {
        var result = await _studentService.DeleteStudent(Id);
        return StatusCode(result.StatusCode, result);
    }
}