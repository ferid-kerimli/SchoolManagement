using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.DTO.SchoolDTOs;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchool()
        {
            var school = await _schoolService.GetAllSchool();
            return StatusCode(school.StatusCode, school);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchool(SchoolCreateDTO schoolCreateDto)
        {
            var school = await _schoolService.AddSchool(schoolCreateDto);
            return StatusCode(school.StatusCode, school);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchool(SchoolUpdateDTO schoolUpdateDto)
        {
            var school = await _schoolService.UpdateSchool(schoolUpdateDto);
            return StatusCode(school.StatusCode, school);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _schoolService.DeleteSchool(id);
            return StatusCode(school.StatusCode, school);
        }
    }
}
