using StudentManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
namespace StudentManagement.IO
{
    public class FileFactory
    {
        public static void SaveFile(List<Faculty> faculties, string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, faculties);
                fileStream.Close();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static List<Faculty> ReadFile(string path)
        {
            List<Faculty> faculties = new List<Faculty>();
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                faculties = binaryFormatter.Deserialize(fileStream) as List<Faculty>;
                fileStream.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return faculties;
        }
    }
}
