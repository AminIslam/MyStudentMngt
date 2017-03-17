using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentLayerApp.MODEL;
using StudentLayerApp.BLL;

namespace StudentLayerApp.UI
{
    public partial class StudentInfoUI : Form
    {
        StudentManager objStudentManager = new StudentManager();
        public StudentInfoUI()
        {
            InitializeComponent();
        }

        private void StudentInfoUI_Load(object sender, EventArgs e)
        {
           
            //LoadListView();
        }

        private void LoadListView()
        {
            studentListView.Items.Clear();
            List<MyStudent> students = objStudentManager.GetAllStudents();
            foreach (MyStudent stu in students)
            {
                ListViewItem item = new ListViewItem(stu.Id.ToString());
                item.SubItems.Add(stu.regNo);
                item.SubItems.Add(stu.name);
                item.SubItems.Add(stu.adress);
                studentListView.Items.Add(item);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MyStudent student = new MyStudent();
            student.regNo = txtRegNo.Text;
            if (objStudentManager.isRegNoExists(student.regNo))
            {
                MessageBox.Show("Reg No Already Exists !");
                return;
            }
            student.name = txtName.Text;
            student.adress = txtAddress.Text;



            int rowAffected = objStudentManager.InsertStudent(student);

            if (rowAffected > 0)
            {
                MessageBox.Show("Insert Successfull !");
                LoadListView();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Operation Failed !");
            }
        }

        private void ClearTextBoxes()
        {
            txtRegNo.Clear();
            txtName.Clear();
            txtAddress.Clear();
        }

        private void studentListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (!txtSearchName.Text.All(abc => char.IsNumber(abc)))
            {                
                studentListView.Items.Clear();
                List<MyStudent> students = objStudentManager.GetStudentsByName(txtSearchName.Text);
                foreach (MyStudent stu in students)
                {
                    ListViewItem item = new ListViewItem(stu.Id.ToString());
                    item.SubItems.Add(stu.regNo);
                    item.SubItems.Add(stu.name);
                    item.SubItems.Add(stu.adress);
                    studentListView.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Avoid integer number !");
            }
        }

        private void txtSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtSearchName.Text.All(abc => char.IsNumber(abc)))
                {
                    studentListView.Items.Clear();
                    List<MyStudent> students = objStudentManager.GetStudentsByName(txtSearchName.Text);
                    foreach (MyStudent stu in students)
                    {
                        ListViewItem item = new ListViewItem(stu.Id.ToString());
                        item.SubItems.Add(stu.regNo);
                        item.SubItems.Add(stu.name);
                        item.SubItems.Add(stu.adress);
                        studentListView.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Avoid integer number !");
                }
            }
        }
    }
}
