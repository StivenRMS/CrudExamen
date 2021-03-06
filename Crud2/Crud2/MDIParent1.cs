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
    public partial class FORMULARIOpro : Form
    {
        private int childFormNumber = 0;

        public FORMULARIOpro()
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
            String consulta = "SELECT * FROM PRODUCTO";
            SqlCommand cmd = new SqlCommand(consulta, ClsConexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        private void MDIParent1_Load(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            MessageBox.Show("CONEXION COMPLETA");
            dataGridView1.DataSource = llenar_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textCODIGO.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textNOMBRE.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textPRECIO.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textCANTIDAD.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClsConexion.Conectar();
            string insertar = "INSERT INTO PRODUCTO (CODIGO,NOMBRES,PRECIO,CANTIDAD)VALUES (@CODIGO,@NOMBRES,@PRECIO,@CANTIDAD)";
            SqlCommand cmd1 = new SqlCommand(insertar, ClsConexion.Conectar());
            cmd1.Parameters.AddWithValue("@CODIGO", textCODIGO.Text);
            cmd1.Parameters.AddWithValue("@NOMBRES", textNOMBRE.Text);
            cmd1.Parameters.AddWithValue("@PRECIO", textPRECIO.Text);
            cmd1.Parameters.AddWithValue("@CANTIDAD", textCANTIDAD.Text);

            cmd1.ExecuteNonQuery();
            MessageBox.Show("LOS DATOS FUERON AGRAGADOS EXITOSAMENTE");
            dataGridView1.DataSource = llenar_grid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            String actualizar = "UPDATE PRODUCTO SET CODIGO = @CODIGO,NOMBRES = @NOMBRES, PRECIO= @PRECIO, CANTIDAD = @CANTIDAD WHERE CODIGO=@CODIGO";
            SqlCommand cmd2 = new SqlCommand(actualizar, ClsConexion.Conectar());

            cmd2.Parameters.AddWithValue("@CODIGO", textCODIGO.Text);
            cmd2.Parameters.AddWithValue("@NOMBRES", textNOMBRE.Text);
            cmd2.Parameters.AddWithValue("@PRECIO", textPRECIO.Text);
            cmd2.Parameters.AddWithValue("@CANTIDAD", textCANTIDAD.Text);


            cmd2.ExecuteNonQuery();
            MessageBox.Show("LOS DATOS se han actualizado");
            dataGridView1.DataSource = llenar_grid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClsConexion.Conectar();
            String eliminar = "DELETE FROM PRODUCTO WHERE CODIGO = @CODIGO";
            SqlCommand cmd3 = new SqlCommand(eliminar, ClsConexion.Conectar());

            cmd3.Parameters.AddWithValue("@CODIGO", textCODIGO.Text);



            cmd3.ExecuteNonQuery();
            MessageBox.Show("EL DATO SE HA ELIMINADO");
            dataGridView1.DataSource = llenar_grid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textCODIGO.Clear();
            textNOMBRE.Clear();
            textCANTIDAD.Clear();
            textPRECIO.Clear();
            textCODIGO.Focus();

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void btnREGRESAR_Click(object sender, EventArgs e)
        {
            Form menu = new MENU();
            ClsConexion.Conectar().Close();
            this.Close();
            menu.Visible = true;
        }
    }
}
