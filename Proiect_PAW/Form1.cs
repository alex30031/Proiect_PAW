using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_PAW
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();





            dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            dataTable.Columns.Add("Job title", typeof(string));
            dataTable.Columns.Add("Salary", typeof(int));
            dataTable.Columns.Add("School", typeof(string));
            dataTable.Columns.Add("Degree", typeof(string));

            LoadDataFromCSV();

            dataGridView1.DataSource = dataTable;

            dataGridView1.MouseDown += dataGridView1_MouseDown;
            listBox1.DragEnter += listBox1_DragEnter;
            listBox1.DragDrop += listBox1_DragDrop;



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadDataFromCSV();

        }

        



        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();

            listBox1.DoDragDrop(selectedItem, DragDropEffects.Copy);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string data = (string)e.Data.GetData(DataFormats.Text);

            textBox1.Text = data;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dataGridView1.DoDragDrop(dataGridView1.Rows[hit.RowIndex], DragDropEffects.Copy);
                }
            }
        }



        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string rowData = e.Data.GetData(DataFormats.StringFormat) as string;

               
                if (!listBox1.Items.Contains(rowData))
                {
                    listBox1.Items.Add(rowData);
                }
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string rowData = $"{row.Cells["Name"].Value}, {row.Cells["Age"].Value}, {row.Cells["Gender"].Value}, {row.Cells["Job title"].Value}, {row.Cells["Salary"].Value}, {row.Cells["School"].Value}, {row.Cells["Degree"].Value}";

                dataGridView1.DoDragDrop(rowData, DragDropEffects.Copy);
            }
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void adaugaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.DataTableReference = dataTable; 
            form2.Show();
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                
                dataGridView1.Rows.Remove(selectedRow);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
           
            Graphics graphics = e.Graphics;
            Font font = new Font("Arial", 10);
            Brush brush = Brushes.Black;

            
            string[] columnHeaders = { "Name", "Gender", "Age", "Job Title", "Salary", "School", "Degree" };

            
            float x = 50;
            float y = 50;

           
            for (int col = 0; col < columnHeaders.Length; col++)
            {
                graphics.DrawString(columnHeaders[col], font, brush, x, y);
                x += 100; 
            }

            
            x = 50;
            y += 20; 

           
            DataGridViewRow firstRow = dataGridView1.Rows[0];
            object[] cellValues = new object[firstRow.Cells.Count];
            for (int col = 0; col < firstRow.Cells.Count; col++)
            {
                cellValues[col] = firstRow.Cells[col].Value;
            }

           
            for (int col = 0; col < cellValues.Length; col++)
            {
                string cellValueString = Convert.ToString(cellValues[col]);
                graphics.DrawString(cellValueString, font, brush, x, y);
                x += 100; 
            }
        }

        private void LoadDataFromCSV()
        {
            // Change filepath accordingly
            string filePath = "C:\\Users\\USER\\Downloads\\Proiect_PAW\\Proiect_PAW\\data.csv";

            if (File.Exists(filePath))
            {
                
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                  
                    string[] headers = lines[0].Split(',');

                   
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header.Trim());
                    }

                    
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(',');
                        dataTable.Rows.Add(fields);
                    }
                }
            }





        }
    }
}

