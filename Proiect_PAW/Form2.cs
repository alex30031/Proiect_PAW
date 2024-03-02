using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proiect_PAW
{
    public partial class Form2 : Form
    {
        public Form2(Form Form1)
        {
            InitializeComponent();



         


        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public DataTable DataTableReference { get; set; }


        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(tbName.Text) || !char.IsUpper(tbName.Text[0]) || tbName.Text.Length > 20)
            {
                MessageBox.Show("Please enter a name.");
                return;
            }

            string gender = "";
            if (rbGender1.Checked)
            {
                gender = "Male";
            }
            else if (rbGender2.Checked)
            {
                gender = "Female";
            }
            else
            {
                MessageBox.Show("Please select a gender.");
                return;

            }


            if (!int.TryParse(tbAge.Text, out int age) || age < 0 || age > 80)
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Please enter a job title.");
                return;
            }

            if (!int.TryParse(tbSalary.Text, out int salary) || salary < 0)
            {
                MessageBox.Show("Please enter a valid salary.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbSchool.Text) || !char.IsUpper(tbName.Text[0]) || tbName.Text.Length > 50)
            {
                MessageBox.Show("Please enter a School.");
                return;
            }

            string degree = "";
            if (rbDegree1.Checked)
            {
                degree = "Bachelor";
            }
            else if (rbDegree2.Checked)
            {
                degree = "Master";
            }
            else if (rbDegree3.Checked)
            {
                degree = "Doctoral";
            }
            else
            {
                MessageBox.Show("Please select a degree.");
                return;

            

            }


            btnSave.Click += new EventHandler(btnSave_Click);
            this.Controls.Add(btnSave);
            DataRow row = DataTableReference.NewRow();
            row["Name"] = tbName.Text;
            row["Gender"] = gender;
            row["Age"] = int.Parse(tbAge.Text);
            row["Job Title"] = tbTitle.Text;
            row["Salary"] = int.Parse(tbSalary.Text);
            row["School"] = tbSchool.Text;
            row["Degree"] = degree;

            DataTableReference.Rows.Add(row);
            tbName.Text = "";
            tbAge.Text = "";
            tbTitle.Text = "";
            tbSalary.Text = "";
            tbSchool.Text = "";

            SaveDataToCSV();
        }


        private bool isFirstSave = true; 

        private void SaveDataToCSV()
        {
            // Change filepath accordingly
            string filePath = "C:\\Users\\USER\\Downloads\\Proiect_PAW\\Proiect_PAW\\data.csv";
            bool fileExists = File.Exists(filePath);

            using (StreamWriter writer = new StreamWriter(filePath, fileExists))
            {
                
                if (isFirstSave && !fileExists)
                {
                    string header = "Name,Gender,Age,Job title,Salary,School,Degree";
                    writer.WriteLine(header);
                    isFirstSave = false; 
                }

               
                foreach (DataRow row in DataTableReference.Rows)
                {
                    string rowData = string.Join(",", row.ItemArray.Select(field => field.ToString()));
                    writer.WriteLine(rowData);
                }
            }
        }




    }

}

