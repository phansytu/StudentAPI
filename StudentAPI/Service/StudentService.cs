using StudentAPI.DTO;
using StudentAPI.Model;
namespace StudentAPI.Service
{
public class StudentService : IStudentService
{
    private readonly List<Student> _students;

    public StudentService()
    {
        _students = new List<Student>
        {
            new Student { Id = 1, Name = "Nguyen Van A", Age = 20, Address = "Ha Noi" },
            new Student { Id = 2, Name = "Tran Thi B", Age = 21, Address = "Hai Phong" },
            new Student { Id = 3, Name = "Le Van C", Age = 22, Address = "Da Nang" },
            new Student { Id = 4, Name = "Pham Thi D", Age = 23, Address = "Ho Chi Minh" },
            new Student { Id = 5, Name = "Nguyen Van E", Age = 24, Address = "Can Tho" },
            new Student { Id = 6, Name = "Tran Van F", Age = 20, Address = "Ha Noi" }
        };
    }
    public PageResponse<StudentResponseDTO> GetStudents(PaginationRequest paginationRequest)
    {
        var totalItems = _students.Count();
        //chia trang
        var totalPages = (int)Math.Ceiling((double)totalItems / paginationRequest.PageSize);
        var studentsPage = _students.AsQueryable()
            .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(s => new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Address = s.Address,
                ClassName = s.ClassName
            })
            .ToList();

        return new PageResponse<StudentResponseDTO>
        {
            Data = studentsPage,
            TotalCount= totalItems,
            TotalPages = totalPages,
            PageNumber = paginationRequest.PageNumber
        };
    }
    public List<StudentResponseDTO> SearchStudents(string? name, int? age, string? address, string? className)
    {
        var query = _students.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
        if (age.HasValue)
        {
            query = query.Where(s => s.Age == age.Value);
        }
        if (!string.IsNullOrEmpty(address))
        {
            query = query.Where(s => s.Address.Contains(address, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(className))
        {
            query = query.Where(s => s.ClassName.Contains(className, StringComparison.OrdinalIgnoreCase));
        }

        return query.Select(s => new StudentResponseDTO
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Address = s.Address,
            ClassName = s.ClassName
        }).ToList();
    }
    public PageResponse<StudentResponseDTO> SearchStudentsWithPagination(string? name, int? age, PaginationRequest paginationRequest)
    {
        var query = _students.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
        if (age.HasValue)
        {
            query = query.Where(s => s.Age == age.Value);
        }

        var totalItems = query.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / paginationRequest.PageSize);

        var studentsPage = query
            .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(s => new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Address = s.Address,
                ClassName = s.ClassName
            })
            .ToList();

        return new PageResponse<StudentResponseDTO>
        {
            Data = studentsPage,
            TotalCount = totalItems,
            TotalPages = totalPages,
            PageNumber = paginationRequest.PageNumber
        };
    }
    public StudentResponseDTO? GetStudentById(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return null;
        }

        return new StudentResponseDTO
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Address = student.Address,
            ClassName = student.ClassName
        };
    }
    public StudentResponseDTO AddStudent(StudentDTO dto)
    {
        var newId = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
            var newStudent = new Student
            {
                Id = newId,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                ClassName = dto.ClassName
            };

            _students.Add(newStudent);

            return new StudentResponseDTO
            {
                Id = newStudent.Id,
                Name = newStudent.Name,
                Age = newStudent.Age,
                Address = newStudent.Address,
                ClassName = newStudent.ClassName
            };
    }
    public bool UpdateStudent(int id, StudentResponseDTO studentRDTO)
    {
        var existingStudent = _students.FirstOrDefault(s => s.Id == studentRDTO.Id);
        if (existingStudent == null) 
        {
            return false;
        }
        existingStudent.Name = studentRDTO.Name;
        existingStudent.Age = studentRDTO.Age;
        existingStudent.Address = studentRDTO.Address;
        existingStudent.ClassName = studentRDTO.ClassName;

        return true;
    }
    public bool Remove(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;

            _students.Remove(student);
            return true;
    }
    
}
}