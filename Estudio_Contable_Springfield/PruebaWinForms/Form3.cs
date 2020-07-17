using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaWinForms
{
    public partial class Form3 : Form
    {
        private CategoriasServicio _cs;
        public Form3()
        {
            this._cs = new CategoriasServicio();
            InitializeComponent();
        }
        #region métodos
        private void CargarListaCategorias(List<Categorias> lc)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = lc;            
        }
        private void LimpiarCampos()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Clear();           
        }
        private Boolean ValidarCampos()
        {
            bool valido = true;
            string msg = string.Empty;
            if (ValidacionHelper.ValidarDouble(textBox2.Text) == -1 || ValidacionHelper.ValidarString(textBox1.Text) == "" || ValidacionHelper.ValidarString(textBox3.Text) == "")
            {
                msg = "Debe ingresar valores validos en los campos Nombre, convenio y sueldo básico.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
        }
        #endregion

        #region eventos
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form3_Load(object sender, EventArgs e)
        {            
            CargarListaCategorias(_cs.TraerListado());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    string nombre = textBox1.Text;
                    double sueldobasico = Convert.ToDouble(textBox2.Text);
                    string convenio = textBox3.Text;                                       
                    this._cs.AltaCategorias(sueldobasico, convenio, nombre);
                    MessageBox.Show("La categoría se dió de alta exitosamente");
                    CargarListaCategorias(this._cs.TraerListado());
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
        }

        #endregion


    }
}
