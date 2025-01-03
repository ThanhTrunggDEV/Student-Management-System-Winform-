using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Model
{
    [Serializable]
    public class Faculty
    {
        public string FacultyName { get; set; }
        public List<Class> ClassList { get; set; }
        public Faculty(string facultyName)
        {
            FacultyName = facultyName;
            ClassList = new List<Class>();
        }
    }
}
