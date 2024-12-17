using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirstApp
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<int>> courseScores = new Dictionary<string, List<int>>();
        private string tasksFilePath = "tasks.txt";
        private string textboxFilePath = "text.txt";
        private string devoirFilePath = "devoir.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasks();
            LoadTasks2();
            LoadTask3();
            RemoveItemsNotNineChars();
            tabControl1.TabPages.Clear();
            foreach (var item in listBox2.Items)
            {

                TabPage tabPage = new TabPage();
                tabPage.Text = item.ToString();


                tabControl1.TabPages.Add(tabPage);
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                tabPage.Controls.Add(rtb);

                //https://eng.mkforlag.com/articles/04_c_sharp_013

                textBox4.Clear();
            }





        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Veuillez entrer un devoir et une date.");
            }
            else if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    string Merge = $"{textBox1.Text}, {textBox2.Text} ";
                    listBox1.Items.Add(Merge);
                    textBox1.Clear();
                    textBox2.Clear();
                    RemoveItemsNotNineChars(); 
                }
                else
                {
                    MessageBox.Show("Veuillez entrer une date.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un devoir.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un devoir");
            }
        }

        private void SaveTasks()
        {
            using (StreamWriter writer = new StreamWriter(devoirFilePath))
            {
                foreach (var item in listBox1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private void LoadTasks()
        {
            if (File.Exists(devoirFilePath))
            {
                using (StreamReader reader = new StreamReader(devoirFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                }
            }
        }

        private void SaveTasks2()
        {
            using (StreamWriter writer = new StreamWriter(textboxFilePath))
            {
                writer.WriteLine(textBox3.Text);
            }
        }

        private void LoadTasks2()
        {
            if (File.Exists(textboxFilePath))
            {
                using (StreamReader reader = new StreamReader(textboxFilePath))
                {
                    textBox3.Text = reader.ReadToEnd();
                }
            }
        }
        private void SaveTask3() 
        {
            using (StreamWriter writer = new StreamWriter(tasksFilePath))
            {
                foreach (var item in listBox2.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }
        private void LoadTask3()
        {
            if (File.Exists(tasksFilePath))
            {
                using (StreamReader reader = new StreamReader(tasksFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBox2.Items.Add(line);
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTasks();
            SaveTasks2();
            SaveTask3();
        }

        private void RemoveItemsNotNineChars()
        {
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                string itemText = listBox1.Items[i].ToString();
                string[] parts = itemText.Split(',');

                if (parts.Length < 2)
                {
                    listBox1.Items.RemoveAt(i);
                    continue;
                }

                string datePart = parts[1].Trim();
                string pattern = @"^\d{2}/\d{2}/\d{2}$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(datePart, pattern))
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9/]"))
            {
                MessageBox.Show("Veuillez entrer dans le format présenté");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }



        private void button4_Click_1(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
                tabControl1.TabPages.Clear();
                foreach (var item in listBox2.Items)
                {

                    TabPage tabPage = new TabPage();
                    tabPage.Text = item.ToString();


                    tabControl1.TabPages.Add(tabPage);

                }

                }
            else
            {
                MessageBox.Show("Veuillez sélectionner une classe");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                listBox2.Items.Add(textBox4.Text);
                textBox4.Clear();
                tabControl1.TabPages.Clear();
                foreach (var item in listBox2.Items)
                {
                    
                    TabPage tabPage = new TabPage();
                    tabPage.Text = item.ToString();
                   

                    tabControl1.TabPages.Add(tabPage);

                    textBox4.Clear();
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer une de vos classe");
            }


        }
    }
}
