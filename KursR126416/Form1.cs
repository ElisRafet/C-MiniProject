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

namespace KursR126416
{
    public partial class Form1 : Form
    {
        private Dictionary<string, double> stoki = new Dictionary<string, double>
        {
            {"Хляб", 1.00 },
            {"Ориз", 2.20 },
            {"Мляко", 2.50 },
            {"Сирене", 7.00 },
            {"Вафли", 3.00 },
            {"Шоколад", 5.00 },
            {"Кафе", 8.40 },
            {"Сапун", 1.90 },
            {"Омекотител", 14.99 },
            {"Препарат", 7.00 } 
        };
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(var item in stoki.Keys)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string izbranTip = "";
            if (radioButton1.Checked) 
            { 
                izbranTip = radioButton1.Text;
            }
            else if (radioButton2.Checked) 
            { 
                izbranTip = radioButton2.Text; 
            }
            else 
            { 
                MessageBox.Show("Моля изберете тип на стока");
                return;
            }

            string izbranaStoka = "";
            if (listBox1.SelectedItem  == null)
            {
                MessageBox.Show("Моля изберете стока от списъка");
                return;
            }
            else 
            {
                izbranaStoka = listBox1.SelectedItem.ToString();
            }

            label2.Text = izbranaStoka;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string izbranaStoka = "";
            if (string.IsNullOrEmpty(label2.Text) || textBox1.Text == "")
            {
                MessageBox.Show("Моля изберете стока и въведете брой");
                return;
            }
            else
            {
                izbranaStoka = label2.Text;
            }
            int kolichestvo;
            if (!int.TryParse(textBox1.Text, out kolichestvo) || kolichestvo <= 0) 
            {
                MessageBox.Show("Моля въведете валидни данни за брой");
                return;
            }
            double edCena = 0.0;
            if (!stoki.ContainsKey(izbranaStoka))
            {
                MessageBox.Show("Цената на избраната стока не е намерена");
                return;
            }
            else { edCena = stoki[izbranaStoka];}

            Poruchki poruchka = new Poruchki(izbranaStoka, radioButton1.Checked ? "Хранителен" : "Промишлен",
                kolichestvo, edCena);
            dataGridView1.Rows.Add(poruchka.NKasovBon, poruchka.Data.ToString(), poruchka.NaimenovanieStoka, 
                poruchka.TipStoka, poruchka.Kolichestvo, poruchka.EdCena, poruchka.Suma);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Няма налична поръчка за потвръждаване");
                return;
            }
            string filepath = "poruchki.txt";
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write)) 
            using (StreamWriter sr = new StreamWriter(fs))
            {
                for (int i = 0; i < dataGridView1.RowCount - 1;  i++)
                {
                    for(int j = 0; j < dataGridView1.ColumnCount; j ++)
                    {
                        string element = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        sr.Write(element + "\t");
                    }
                    sr.WriteLine();
                }
            }
            MessageBox.Show("Успешно направена и записана поръчка");
            dataGridView1.Rows.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
