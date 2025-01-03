using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Model
{
    [Serializable]
    public class Teacher : Person
    {
        public string TeacherId { get; set; }
        public Teacher(string teacherId, string fullName, DateTime dateOfBirth, bool gender) : base(fullName,dateOfBirth, gender)
        {
            TeacherId = teacherId;
        }
    }
}
