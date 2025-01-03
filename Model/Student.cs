using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Model
{
    [Serializable]
    public class Student : Person
    {
        public string StudentId { get; set; }
        public Class Class { get; set; }
        public Student(string fullName, DateTime dateOfBirth, string studentId, Class clas, bool gender) : base(fullName, dateOfBirth, gender)
        {
            StudentId = studentId;
            Class = clas;
        }
        public Student(string fullName, DateTime dateOfBirth, string studentId, Class clas) : base(fullName, dateOfBirth)
        {
            StudentId = studentId;
            Class = clas;
        }

    }
}
