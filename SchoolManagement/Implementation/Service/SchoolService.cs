using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.Context;
using SchoolManagement.DTO.SchoolDTOs;
using SchoolManagement.Entities;
using SchoolManagement.Models;

namespace SchoolManagement.Implementation.Service
{
    public class SchoolService : ISchoolService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public SchoolService(ApplicationDBContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }
        public async Task<GenericResponseModel<SchoolCreateDTO>> AddSchool(SchoolCreateDTO schoolCreateDTO)
        {
            var response = new GenericResponseModel<SchoolCreateDTO>()
            {
                Data = schoolCreateDTO,
                StatusCode = 400
            };

            try
            {
                if (schoolCreateDTO != null)
                {
                    var school = _mapper.Map<School>(schoolCreateDTO);
                    await _context.Schools.AddAsync(school);

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        response.StatusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Exception" + ex.InnerException);
                response.StatusCode = 500;
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteSchool(int id)
        {
            var response = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            try
            {
                var school = await _context.Schools.FindAsync(id);

                if (school != null)
                {
                    _context.Schools.Remove(school);

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        response.Data = true;
                        response.StatusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Exception: " + ex.InnerException);
                response.StatusCode = 500;
            }

            return response;
        }

        public async Task<GenericResponseModel<List<SchoolGetDTO>>> GetAllSchool()
        {
            var response = new GenericResponseModel<List<SchoolGetDTO>>()
            {
                Data = new List<SchoolGetDTO>(),
                StatusCode = 400
            };

            try
            {
                var school = await _context.Schools.ToListAsync();

                if (school != null)
                {
                    var mappedSchool = _mapper.Map<List<SchoolGetDTO>>(school);

                    response.Data = mappedSchool;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.StatusCode = 500;
            }
            return response;
        }

        public async Task<GenericResponseModel<SchoolGetDTO>> GetSchoolById(int id)
        {
            var response = new GenericResponseModel<SchoolGetDTO>()
            {
                Data = new SchoolGetDTO(),
                StatusCode = 400
            };

            try
            {
                var school = await _context.Schools.FindAsync(id);

                if (school != null)
                {
                    var mappedSchool = _mapper.Map<SchoolGetDTO>(school);

                    response.Data = mappedSchool;
                    response.StatusCode = 200;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.StatusCode = 500;
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> UpdateSchool(SchoolUpdateDTO schoolUpdateDTO)
        {
            var response = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            try
            {
                var school = await _context.Schools.FindAsync(schoolUpdateDTO.ID);

                if (school != null)
                {
                    var mappedSchool = _mapper.Map(schoolUpdateDTO, school);
                    _context.Schools.Update(mappedSchool);

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        response.Data = true;
                        response.StatusCode = 200;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.StatusCode = 500;
            }
            return response;
        }
    }
}
