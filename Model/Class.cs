using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Model
{
    [Serializable]
    public class Class
    {
        public string ClassName { get; set; }
        public Teacher HeadTeacher { get; set; }
        public Faculty Faculty { get; set; }
        public List<Student> Students { get; set; }
        public Class(string className, Teacher headTeacher, Faculty faculty)
        {
            Students = new List<Student>();
            ClassName = className;
            HeadTeacher = headTeacher;
            Faculty = faculty;
        }
        public Class() { }

    }
}
