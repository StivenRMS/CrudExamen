using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Crud2
{
    public partial class mdiclientes : Form
    {
        private int childFormNumber = 0;

        public mdiclientes()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

      

        public DataTable llenar_grid()
        {
            ClsConexion.Conectar();
            DataTable dt = new DataTable();
            String consulta = "SELECT * FROM CLIENTES";
            SqlCommand cmd = new SqlCommand(consulta, ClsConexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private void MDIParent2_Load(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            MessageBox.Show("CONEXION COMPLETA");
            dataGridView1.DataSource = llenar_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            string insertar = "INSERT INTO CLIENTES (ID,NOMBRES,TOTAL,DIRECCION)VALUES (@ID,@NOMBRES,@TOTAL,@DIRECCION)";
            SqlCommand cmd1_1 = new SqlCommand(insertar, ClsConexion.Conectar());
            cmd1_1.Parameters.AddWithValue("@ID", textID.Text);
            cmd1_1.Parameters.AddWithValue("@NOMBRES", textNOMBRECLI.Text);
            cmd1_1.Parameters.AddWithValue("@TOTAL", textCOMPRA.Text);
            cmd1_1.Parameters.AddWithValue("@DIRECCION", textDIRECC.Text);

            cmd1_1.ExecuteNonQuery();
            MessageBox.Show("LOS DATOS FUERON AGRAGADOS EXITOSAMENTE");
            dataGridView1.DataSource = llenar_grid();
        }

        private void btnREGRESAR_Click(object sender, EventArgs e)
        {
            Form menu = new MENU();
            ClsConexion.Conectar().Close();
            this.Close();
            menu.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textID.Clear();
            textNOMBRECLI.Clear();
            textCOMPRA.Clear();
            textDIRECC.Clear();
            textID.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            String actualizar = "UPDATE CLIENTES SET ID = @ID,NOMBRES = @NOMBRES, TOTAL= @TOTAL, DIRECCION = @DIRECCION WHERE ID=@ID";
            SqlCommand cmd2 = new SqlCommand(actualizar, ClsConexion.Conectar());

            cmd2.Parameters.AddWithValue("@ID", textID.Text);
            cmd2.Parameters.AddWithValue("@NOMBRES", textNOMBRECLI.Text);
            cmd2.Parameters.AddWithValue("@TOTAL", textCOMPRA.Text);
            cmd2.Parameters.AddWithValue("@DIRECCION", textDIRECC.Text);


            cmd2.ExecuteNonQuery();
            MessageBox.Show("LOS DATOS se han actualizado");
            dataGridView1.DataSource = llenar_grid();
        }

        private void btnElimC_Click(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            String eliminar = "DELETE FROM CLIENTES WHERE ID = @ID";
            SqlCommand cmd3 = new SqlCommand(eliminar, ClsConexion.Conectar());

            cmd3.Parameters.AddWithValue("@ID", textID.Text);



            cmd3.ExecuteNonQuery();
            MessageBox.Show("EL DATO SE HA ELIMINADO");
            dataGridView1.DataSource = llenar_grid();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
                try
                {
                    textID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textNOMBRECLI.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textCOMPRA.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textDIRECC.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


                }
                catch { }
            
        }
    }
}
