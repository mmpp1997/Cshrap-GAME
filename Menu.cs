using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Menu : Form
    {
        public bool start;
        public Menu()
        {
            InitializeComponent();
            
        }

        public void Izlaz_Click(object sender, EventArgs e)
        {
            start = false;
            this.Close();
            Application.Exit();
        }

        public void Menu_Load(object sender, EventArgs e)
        {
             
            Srednje.Checked = true;
            odabir.Text = "Ufo";
        }

        private void Pocetak_Click(object sender, EventArgs e)
        {
            if (odabir.Text != "Ufo" && odabir.Text != "Zrakoplov")
            {
                Problem novi = new Problem();
                MessageBox.Show(novi.Message);
            }
            else
            {
                start = true;
                this.Close();
            }
                
        }

        private void svojstva_Click(object sender, EventArgs e)
        {

        }

        private void odabir_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void odabir_TextChanged(object sender, EventArgs e)
        {
            if (odabir.Text=="Ufo")
            {
                svojstva.Text = "Lakša igrivost zato što nema krila, ali starta sa 2 života";
            }
            if (odabir.Text == "Zrakoplov")
            {
                svojstva.Text = "Teža igrivost zbog krila, ali starta sa 4 života";
            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (start == false)
            {
                this.Close();
                Application.Exit();
            }                          
        }
    }
}
