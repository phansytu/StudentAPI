using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;
using StudentAPI.DTO;
using StudentAPI.Service;
using System.Collections.Generic;
namespace StudentAPI.Controller
{
    [ApiController]
    [Route("api/students")] // api/student
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        

        //1. Get: api/student (lay toan bo danh sach sinh vien)
        [HttpGet]
        public ActionResult<IEnumerable<StudentResponseDTO>> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var response = _studentService.GetStudents(paginationRequest);
            return Ok(new ApiResponse<PageResponse<StudentResponseDTO>>(response));
        }
                
        //2. Get: api/student/1 (lay sinh vien theo id)
        [HttpGet("{id}")] // api/student/1
        public ActionResult<StudentResponseDTO> GetById([FromRoute] int id)
        {
            var student_ = _studentService.GetStudentById(id);
            if (student_ == null)
            {
                return NotFound(new ApiResponse<string>("Student not found"));
            }
            
            return Ok(student_);
        }
        //3 . Post: api/student (them moi sinh vien)
        [HttpPost]
        public ActionResult<StudentResponseDTO> Add([FromBody] StudentDTO dto)
        {
            if(dto == null)
            {
                return BadRequest("Du lieu khoong hop le");
            }
            var createdStudent = _studentService.AddStudent(dto);

    // Trả về 201 Created cùng đường dẫn GetById của sinh viên mới
         return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
        }
        //4. Put: api/student/1 (cap nhat thong tin sinh vien theo id)
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] StudentResponseDTO updatedStudentDTO)
        {
            if(updatedStudentDTO == null)
            {
                return NotFound($"khong tim thay sinh vien co ID = {id} de cap nhat");
            }
            var success = _studentService.UpdateStudent(id, updatedStudentDTO);
            if(!success){
                return NotFound(new ApiResponse<string>("Student not found"));
            }
            return Ok(new ApiResponse<string>("Student updated successfully"));
        }
        //5. Delete: api/student/1 (xoa sinh vien theo id)
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student_ = _studentService.Remove(id);
            if (!student_)
            {
                return NotFound(new ApiResponse<string>("Student not found"));
            }
           
            return Ok(new ApiResponse<string>("Student deleted successfully"));
        }
 
        [HttpGet("search")]
        public ActionResult<IEnumerable<StudentResponseDTO>> Search(
            [FromQuery] string? name, 
            [FromQuery] int? age, 
            [FromQuery] string? address, 
            [FromQuery] string? className)
        {
            var results = _studentService.SearchStudents(name, age, address, className);
            return Ok(new ApiResponse<List<StudentResponseDTO>>(results));
        }

        [HttpGet("search-pagination")]
        public ActionResult<PageResponse<StudentResponseDTO>> SearchWithPagination(
            [FromQuery] string? name, 
            [FromQuery] int? age, 
            [FromQuery] PaginationRequest paginationRequest)
        {
           
            if (paginationRequest.PageNumber <= 0) paginationRequest.PageNumber = 1;
            if (paginationRequest.PageSize <= 0) paginationRequest.PageSize = 10;

            var results = _studentService.SearchStudentsWithPagination(name, age, paginationRequest);
            return Ok(new ApiResponse<PageResponse<StudentResponseDTO>>(results));
        }
    }
}