using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;
using StudentAPI.DTO;
namespace StudentAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")] // api/student
    public class StudentController : ControllerBase
    {
        //khoi tao danh sach sinh vien
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Nguyen Van A", Age = 20, Address = "Ha Noi" },
            new Student { Id = 2, Name = "Tran Thi B", Age = 21, Address = "Hai Phong" },
            new Student { Id = 3, Name = "Le Van C", Age = 22, Address = "Da Nang" }
        };

        //1. Get: api/student (lay toan bo danh sach sinh vien)
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(students);
        }
        //2. Get: api/student/1 (lay sinh vien theo id)
        [HttpGet("{id}")] // api/student/1
        public IActionResult GetById(int id)
        {
            var student_ = students.FirstOrDefault(s => s.Id == id);
            if (student_ == null)
            {
                return NotFound(new { message = "Student not found" });
            }
            return Ok(student_);
        }
        //3 . Post: api/student (them moi sinh vien)
        [HttpPost]
        public IActionResult Create([FromBody] StudentDTO dto)
        {
            // chuyển đổi (Mapping) từ StudentDTO sang Student
            var newStudent = new Student
            {
                Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1, // Tự động tạo Id mới
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                ClassName = dto.ClassName
            };
            
            students.Add(newStudent);
            //tra ve ket qua them moi sinh vien thanh cong
            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
        }
        //4. Put: api/student/1 (cap nhat thong tin sinh vien theo id)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StudentDTO updatedStudentDTO)
        {
            var student_ = students.FirstOrDefault(s => s.Id == id);
            if (student_ == null)
            {
                return NotFound(new { message = "Student not found" });
            }
            //cap nhat thong tin sinh vien
            student_.Name = updatedStudentDTO.Name;
            student_.Age = updatedStudentDTO.Age;
            student_.Address = updatedStudentDTO.Address;
            student_.ClassName = updatedStudentDTO.ClassName;

            return NoContent(); //trả về mã trạng thái 204 No Content để cho biết rằng yêu cầu đã được thực hiện thành công nhưng không có nội dung trả về.
        }
        //5. Delete: api/student/1 (xoa sinh vien theo id)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student_ = students.FirstOrDefault(s => s.Id == id);
            if (student_ == null)
            {
                return NotFound(new { message = "Student not found" });
            }
            students.Remove(student_);
            return NoContent();
        }
    }
}