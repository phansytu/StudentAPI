using StudentAPI.DTO;
using StudentAPI.Model;
namespace StudentAPI.Service{


public interface IStudentService
{
    PageResponse<StudentResponseDTO> GetStudents(PaginationRequest paginationRequest);  
    //tim kiem khog phan trang
     List<StudentResponseDTO> SearchStudents(string? name, int? age, string? address, string? className);
    //tim kiem co phan trang
     PageResponse<StudentResponseDTO> SearchStudentsWithPagination(string? name, int? age, PaginationRequest paginationRequest);
     StudentResponseDTO? GetStudentById(int id);
     StudentResponseDTO AddStudent(StudentDTO studentDTO);
     bool UpdateStudent(int id, StudentResponseDTO studentRDTO);
     bool Remove(int id);
    
}
}