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
using static WindowsFormsApp1.FormMenuPrincipal;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form, IFormulario
    {
        private CategoriasServicio _cs;
        public Form3()
        {
            this._cs = new CategoriasServicio();
            InitializeComponent();
        }
        #region métodos
        public void InicializarParametros(params object[] parametros)
        {
            if (parametros.Length == 1)
            {
                Empresa parametro1 = (Empresa)parametros[0];
            }
            if (parametros.Length == 0)
            {

            }
            else
            {
                throw new Exception("El número de parámetros es incorrecto");
            }
        }
        private void CargarListaCategorias(List<Categorias> lc)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = lc;
            listBox1.DisplayMember = "NombreCompletoCategorias";
            listBox1.SelectedItem = null;
        }
        private void LimpiarCampos()
        {
            textBox2.Clear();            
            textBox1.Clear();
            listBox1.SelectedItem = null;
        }
        private Boolean ValidarCampos()
        {
            bool valido = true;
            string msg = string.Empty;
            if (ValidacionHelper.ValidarDouble(textBox2.Text) == -1 || ValidacionHelper.ValidarString(textBox1.Text) == "")
            {
                msg = "Debe ingresar valores validos en los campos Nombre y sueldo básico.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
        }
        private string FormatoString(string s)
        {
            return s.First().ToString().ToUpper() + String.Join("", s.Skip(1)).ToLower();
        }
        private Boolean ValidarUnicidadCategorias(string categoria)
        {
            bool valido = true;
            string msg = string.Empty;
            List<string> listnombres = new List<string>();
            List<Categorias> listcategorias = _cs.TraerListado();
            foreach (Categorias c in listcategorias)
            {
                listnombres.Add(c.Nombre);
            }
            if (listnombres.Any(x => x == categoria))
            {
                msg = "La Categoría ya se encuentra ingresada.";
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
            textBox3.Text = "CCT 130/75";
            textBox3.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos() && ValidarUnicidadCategorias(textBox1.Text))
                {
                    string nombre = FormatoString(textBox1.Text);
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
        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        #endregion
    }
}
