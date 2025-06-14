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
    public partial class Form2 : Form
    {
        private List<Poruchki> napraveniPoruchki = new List<Poruchki>();

        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            string filepath = "poruchki.txt";
            if (File.Exists(filepath))
            {
                var lines = File.ReadAllLines(filepath);
                foreach (var line in lines)
                {
                    var parts = line.Split('\t');
                    if (parts.Length >= 7)
                    {
                        Poruchki p = new Poruchki(int.Parse(parts[0]), DateTime.Parse(parts[1]),
                            parts[2], parts[3], int.Parse(parts[4]), double.Parse(parts[5]));
                        napraveniPoruchki.Add(p);
                    }
                }
                comboBox1.Items.AddRange(napraveniPoruchki.Select(p => p.NKasovBon.ToString()).Distinct().ToArray());
                comboBox2.Items.AddRange(napraveniPoruchki.Select(p => p.Data.ToShortDateString()).Distinct().ToArray());
                comboBox3.Items.AddRange(new string[] { "Хранителен", "Промишлен" });
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;
            int izbranKBon = int.Parse(comboBox1.SelectedItem.ToString());
            var danni = napraveniPoruchki.Where(p => p.NKasovBon == izbranKBon).ToList();
            listBox1.Items.Clear();
            foreach (var p in danni)
            {
                listBox1.Items.Add($"Бон #{p.NKasovBon} | {p.Data} | {p.NaimenovanieStoka} | " +
                    $"{p.TipStoka} | Бр. {p.Kolichestvo} | Ед.ц. {p.EdCena} | Об. {p.Suma}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;
            DateTime izbranaData = DateTime.Parse(comboBox2.SelectedItem.ToString());
            var danni = napraveniPoruchki.Where(p => p.Data.Date == izbranaData.Date).ToList();
            double oborot = danni.Sum(p => p.Suma);
            label3.Text = oborot.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null) return;
            string tip = comboBox3.SelectedItem.ToString();
            var danni = napraveniPoruchki.Where(p => p.TipStoka == tip).ToList();
            double suma = danni.Sum(p => p.Suma);
            label6.Text = suma.ToString();
        }
    }
}
