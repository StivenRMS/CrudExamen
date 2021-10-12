using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud2
{
    public partial class MENU : Form
    {
        public MENU()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new FORMULARIOpro();
            form.Show();
            this.Visible = false;
        }

        private void BTNclientes_Click(object sender, EventArgs e)
        {
            Form form = new mdiclientes();
            form.Show();
            this.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            
        }
    }
}
