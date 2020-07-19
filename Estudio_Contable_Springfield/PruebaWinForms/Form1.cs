using System;
using Negocio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace PruebaWinForms
{
    public partial class Form1 : Form
    {
        private EmpresaServicio _emprs;
        public Form1()
        {
            this._emprs = new EmpresaServicio();
            InitializeComponent();
        }
        #region métodos
        
        private void CargarListaEmpresas(List<Empresa> le)
        {
            listBox1.DataSource = null;
            listBox1.Sorted = true;
            listBox1.DataSource = le;
            listBox1.DisplayMember = "NombreCompletoEmpresa";
            listBox1.SelectedItem = null;
        }
        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            listBox1.SelectedItem = null;
        }
        private Boolean ValidarCampos()
        {
            bool valido = true;
            string msg = string.Empty;
            if (ValidacionHelper.ValidarString(textBox1.Text) == "" || ValidacionHelper.ValidarString(textBox2.Text) == "" || ValidacionHelper.ValidarCuit(textBox3.Text) == false) 
            {
                msg = "Debe ingresar valores validos en los campos Razon social, CUIT, Domicilio.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
        }
        private Boolean ValidarUnicidadCuit(Int64 cuit)
        {
            bool valido = true;
            string msg = string.Empty;
            List<long> listcuit = new List<long>();
            List<Empresa> listempresas = _emprs.TraerListado();
            foreach (Empresa e in listempresas)
            {
                listcuit.Add(e.Cuit);
            }
            if (listcuit.Any(x => x == cuit))
            {
                msg = "El Cuit ya se encuentra ingresado.";
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
        #endregion

        #region eventos
        private void Form1_Load(object sender, EventArgs e)
        {
            CargarListaEmpresas(_emprs.TraerListado());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos() && ValidarUnicidadCuit(Convert.ToInt64(textBox3.Text)))
                {
                    string razonsocial = FormatoString(textBox1.Text);
                    string domicilio = FormatoString(textBox2.Text);
                    Int64 cuit = Convert.ToInt64(textBox3.Text);                    
                    this._emprs.AltaEmpresa(razonsocial, cuit, domicilio);
                    MessageBox.Show("La empresa se dió de alta exitosamente");
                    CargarListaEmpresas(this._emprs.TraerListado());
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            listBox1.SelectedItem = null;
        }
        #endregion
    }
}
