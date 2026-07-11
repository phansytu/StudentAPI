using System;
using System.Text.Json.Serialization;
namespace StudentAPI.Model
{
    public class Student
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
}