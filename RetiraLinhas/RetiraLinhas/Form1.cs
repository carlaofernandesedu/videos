using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RetiraLinhas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sw = new StringBuilder();
            int valor;
            string row;
            using( StreamReader sr = File.OpenText(textBox1.Text))
            {
                while ((row = sr.ReadLine()) != null)
                {
                    if (row.Trim() != "" && row.Length > 1)
                    {
                        if (!Int32.TryParse(row.Substring(0, 2), out valor))
                        {
                            sw.AppendLine(row);
                        }
                    }
                }
            }
            File.WriteAllText("D:\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + ".txt", sw.ToString());
            MessageBox.Show(sw.ToString());
        }
    }
}
