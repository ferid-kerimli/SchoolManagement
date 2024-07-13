using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Abstraction;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.DTO.StudentDTOs;
using SchoolManagement.Entities;
using SchoolManagement.Models;

namespace SchoolManagement.Implementation.Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId)
        {
            var response = new GenericResponseModel<bool>();
            try
            {
                var student = await _unitOfWork.GetRepository<Student>().GetByIdAsync(studentId);
                var newSchool = await _unitOfWork.GetRepository<School>().GetByIdAsync(newSchoolId);

                if (student == null || newSchool == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    return response;
                }

                student.School = newSchool;

                _unitOfWork.GetRepository<Student>().Update(student);
                var result = await _unitOfWork.Commit();

                if (result > 0)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Data = false;
                    response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.StatusCode = 500;
                Console.WriteLine(ex.Message);

            }

            return response;
        }

        public async Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO changeSchoolDTO)
        {
            var response = new GenericResponseModel<bool>();
            try
            {
                var student = await _unitOfWork.GetRepository<Student>().GetByIdAsync(changeSchoolDTO.StudentID);
                var newSchool = await _unitOfWork.GetRepository<School>().GetByIdAsync(changeSchoolDTO.NewSchoolID);

                if (student == null || newSchool == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    return response;
                }

                student.School = newSchool;

                _unitOfWork.GetRepository<Student>().Update(student);
                var result = await _unitOfWork.Commit();

                if (result > 0)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Data = false;
                    response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.StatusCode = 500;
                Console.WriteLine(ex.Message);

            }

            return response;
        }

        public async Task<GenericResponseModel<bool>> CreateStudent(StudentCreateDTO studentCreateDTO)
        {
            var response = new GenericResponseModel<bool>();
            try
            {
                if (studentCreateDTO == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    return response;
                }
                var studentRepository = _unitOfWork.GetRepository<Student>();
                var mappedStudent = _mapper.Map<Student>(studentCreateDTO);

                await studentRepository.AddAsync(mappedStudent);
                var result = await _unitOfWork.Commit();
                if (result > 0)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.StatusCode = 500;
                Console.WriteLine(ex.Message);

            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteStudent(int Id)
        {
            var response = new GenericResponseModel<bool>();
            try
            {
                var studentRepository = _unitOfWork.GetRepository<Student>();
                var deletedStudent = await studentRepository.GetByIdAsync(Id);
                if (deletedStudent == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                }
                else
                {
                    studentRepository.Remove(deletedStudent);
                }
                var result = await _unitOfWork.Commit();
                if (result > 1)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.StatusCode = 500;
                Console.WriteLine(ex.Message);

            }
            return response;
        }

        public async Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudents()
        {
            var response = new GenericResponseModel<List<StudentGetDTO>>()
            {
                Data = null,
                StatusCode = 400
            };
            try
            {
                var repository = _unitOfWork.GetRepository<Student>();
                var students = await repository.GetAll().Include(m => m.School).ToListAsync();
                var listStudents = _mapper.Map<List<StudentGetDTO>>(students);
                if (listStudents.Count == 0)
                {
                    response.Data = null;
                    response.StatusCode = 404;
                }
                response.Data = listStudents;
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.StatusCode = 500;
                Console.WriteLine(ex.Message);

            }
            return response;
        }

        public async Task<GenericResponseModel<StudentGetDTO>> GetStudentById(int Id)
        {
            var response = new GenericResponseModel<StudentGetDTO>();
            try
            {
                var Studentrepository = _unitOfWork.GetRepository<Student>();
                var Schoolrepository = _unitOfWork.GetRepository<School>();

                var student = await Studentrepository.GetByIdAsync(Id);
                await Schoolrepository.GetByIdAsync(student.Id);
                var mappedStudent = _mapper.Map<StudentGetDTO>(student);
                if (mappedStudent == null)
                {
                    response.Data = null;
                    response.StatusCode = 404;
                }
                response.Data = mappedStudent;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Data = null;
                Console.WriteLine(ex.Message);

            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> UpdateStudent(StudentUpdateDTO studentUpdateDTO, int Id)
        {
            var response = new GenericResponseModel<bool>();
            try
            {
                var studentRepository = _unitOfWork.GetRepository<Student>();
                if (studentUpdateDTO == null)
                {
                    response.StatusCode = 404;
                    response.Data = false;
                }
                var student = await studentRepository.GetByIdAsync(Id);
                _mapper.Map(studentUpdateDTO, student);
                studentRepository.Update(student);
                var result = await _unitOfWork.Commit();
                if (result > 0)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Data = false;
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}