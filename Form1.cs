using StudentManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagement.IO;
using System.Runtime.CompilerServices;
namespace StudentManagement
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private List<Faculty> faculties = new List<Faculty>();
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                faculties = FileFactory.ReadFile(Application.StartupPath + "\\data.txt");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DisPlayTreeView();

        }
        private void DisPlayTreeView()
        {
            tvOverView.Nodes.Clear();
            foreach (Faculty faculty in faculties)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = faculty;
                treeNode.Text = faculty.FacultyName;
                tvOverView.Nodes.Add(treeNode);
                foreach (Class cl in faculty.ClassList)
                {
                    TreeNode treeNode1 = new TreeNode();
                    treeNode1.Tag = cl;
                    treeNode1.Text = cl.ClassName;
                    treeNode.Nodes.Add(treeNode1);
                }
            }
        }

        private void tvOverView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvOverView.SelectedNode == null)
                return;
            if(tvOverView.SelectedNode.Level == 0)
            {
                Faculty faculty = tvOverView.SelectedNode.Tag as Faculty;
                txtFaculty.Text = faculty.FacultyName;
                txtFaculty.Tag = faculty;
            }
            if (tvOverView.SelectedNode.Level == 1)
            {
                Class @class = tvOverView.SelectedNode.Tag as Class;
                lvStudentList.Items.Clear();
                for (int i = 0; i < @class.Students.Count; i++)
                {
                    Student student = @class.Students[i];
                    ListViewItem lvStudent = new ListViewItem(i + 1 + "");
                    lvStudent.SubItems.Add(student.StudentId);
                    lvStudent.SubItems.Add(student.FullName);
                    lvStudent.SubItems.Add(student.Class.ClassName);
                    lvStudent.SubItems.Add(student.Gender == true ? "Male" : "Female");
                    lvStudent.SubItems.Add(student.DateOfBirth.ToString("dd/MM/yyyy"));
                    lvStudentList.Items.Add(lvStudent);
                    lvStudent.Tag = student;

                    txtName.Text = string.Empty;
                    txtStudentID.Text = string.Empty;
                    txtClass.Text = @class.ClassName;
                    txtClass.Tag = @class;
                }
                txtClassName.Text = @class.ClassName;
                txtClassName.Tag = @class;
               
                txtHeadTeacher.Text = @class.HeadTeacher.FullName;
                txtHeadTeacher.Tag = @class.HeadTeacher;
                
                txtFaculty.Text = @class.Faculty.FacultyName;
                txtFaculty.Tag = @class.Faculty;

                txtStudentNumber.Text = @class.Students.Count.ToString();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FileFactory.SaveFile(faculties, Application.StartupPath + "\\data.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvStudentList.SelectedIndices.Count > 0)
            {
                Student student = lvStudentList.SelectedItems[0].Tag as Student;
                txtStudentID.Text = student.StudentId;
                txtName.Text = student.FullName;
                txtClass.Text = student.Class.ClassName;
                txtClass.Tag = student.Class;
                rdbtnFemale.Checked = student.Gender == false;
                rdbtnMale.Checked = student.Gender == true;
                dtpDateOfBirth.Value = student.DateOfBirth;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (txtClass.Tag == null)
            {
                MessageBox.Show("Please Select A Class To Add Student");
                return;
            }
            Class cl = txtClass.Tag as Class;
            if(string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Information can't be blank");
                return;
            }
            Student student = new Student(txtName.Text, dtpDateOfBirth.Value, txtStudentID.Text, cl, rdbtnFemale.Checked);
            cl.Students.Add(student);
            RefreshListView();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
                if (lvStudentList.SelectedItems.Count == 0)
                {
                MessageBox.Show("Please Select A Student To Remove");
                return;
                }
                Student student = lvStudentList.SelectedItems[0].Tag as Student;
                Class cl = txtClass.Tag as Class;
                if (cl.Students.Contains(student))
                {
                    cl.Students.Remove(student);
                    RefreshListView();
                    return;
                }
                MessageBox.Show($"Not Found Student Named {student.FullName} In Class {cl.ClassName}");
                
            
        }
        private void RefreshListView()
        {
            TreeNode tn = tvOverView.SelectedNode;
            tvOverView.SelectedNode = null;
            tvOverView.SelectedNode = tn;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvStudentList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please Select A Student To Edit");
                return;
            }
            Student oldStudent = lvStudentList.SelectedItems [0].Tag as Student;
            Student newStudent = new Student(txtName.Text, dtpDateOfBirth.Value, txtStudentID.Text, txtClass.Tag as Class, rdbtnMale.Checked);
            Class cl = txtClass.Tag as Class;
            if(cl.Students.Contains(oldStudent))
            {
               int pos = cl.Students.IndexOf(oldStudent);
                cl.Students[pos] = newStudent;
                RefreshListView();
                return;
            }
            MessageBox.Show("Something went wrong!");
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is testing");
            if(txtFaculty.Tag ==  null) return;
            
           // Teacher teacher = new Teacher()
            Class clas = new Class();
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is testing");
            if (txtClassName.Tag == null) return;
            Class oldClass = txtClassName.Tag as Class;
            //Teacher teacher = new Teacher();
            //Class newClass = new Class(txtClassName.Text);
        }

        private void btnRemoveClass_Click(object sender, EventArgs e)
        {
            if (txtClassName.Tag == null)
            {
                MessageBox.Show("Please Select A Class To Remove");
                return;
            }
            var result = MessageBox.Show("Are you sure?"
                                         , "Notification"
                                         , MessageBoxButtons.YesNo
                                         , MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Faculty faculty = txtFaculty.Tag as Faculty;
                faculty.ClassList.Remove(txtClassName.Tag as Class);

                DisPlayTreeView();
            }
        }

        private void addFacultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvOverView.SelectedNode = null;
            frmInput frmInput = new frmInput();
            frmInput.ShowDialog();
            if (!string.IsNullOrWhiteSpace(frmInput.txtFacultyName.Text))
            {
                TreeNode treeNode = new TreeNode(frmInput.txtFacultyName.Text);
                Faculty faculty = new Faculty(frmInput.txtFacultyName.Text);
                treeNode.Tag = faculty;
                tvOverView.Nodes.Add(treeNode);
                faculties.Add(faculty);
            }
            else if(frmInput.DialogResult == DialogResult.OK) 
                MessageBox.Show("Can't be blank");
        }

        private void removeFacultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txtFaculty.Tag == null) return;
            var result = MessageBox.Show("Are you sure?"
                                         , "Notification"
                                         ,MessageBoxButtons.YesNo
                                         ,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                faculties.Remove(txtFaculty.Tag as Faculty);
                tvOverView.Nodes.Remove(tvOverView.SelectedNode);
            }
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvOverView.ExpandAll();
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvOverView.CollapseAll();
        }
    }
}
