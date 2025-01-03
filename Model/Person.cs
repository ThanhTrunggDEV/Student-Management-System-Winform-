using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Model
{
    [Serializable]
    public abstract class Person
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public Person(string fullName, DateTime dateOfBirth, bool gender)
        {
            FullName = fullName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }
        public Person(string fullName, DateTime dateOfBirth)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
        }
    }
}
