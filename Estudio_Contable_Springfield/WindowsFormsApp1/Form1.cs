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
using static WindowsFormsApp1.FormMenuPrincipal;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form, IFormulario
    {
        private EmpresaServicio _emprs;
        public Form1()
        {
            this._emprs = new EmpresaServicio();
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
        private Empresa ObtenerEmpresa()
        {
            Empresa empresa = new Empresa("", 30222222223, "");
            foreach (Empresa e in _emprs.TraerListado())
            {
                if (e.ToString() == listBox1.SelectedItem.ToString())
                {
                    empresa = e;
                }
            }
            return empresa;
        }
        private int ObtenerIdEmpresa()
        {
            int id = 0;
            foreach (Empresa e in _emprs.TraerListado())
            {
                if (e.ToString() == listBox1.SelectedItem.ToString())
                {
                    id = e.id;
                }
            }
            return id;
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
                if (listBox1.SelectedItem == null)
                {
                    if (ValidarCampos() && ValidarUnicidadCuit(Convert.ToInt64(textBox3.Text)))
                    {
                        string razonsocial = textBox1.Text.ToUpper();
                        string domicilio = FormatoString(textBox2.Text);
                        Int64 cuit = Convert.ToInt64(textBox3.Text);
                        this._emprs.AltaEmpresa(razonsocial, cuit, domicilio);
                        MessageBox.Show("La empresa se dió de alta exitosamente");
                        CargarListaEmpresas(this._emprs.TraerListado());
                        LimpiarCampos();
                    }
                }
                if (listBox1.SelectedItem != null)
                {
                    if (ValidarCampos())
                    {
                        string razonsocial = textBox1.Text.ToUpper();
                        string domicilio = FormatoString(textBox2.Text);
                        Int64 cuit = Convert.ToInt64(textBox3.Text);
                        int idempresa = ObtenerIdEmpresa();
                        this._emprs.ModificarEmpresa(razonsocial, cuit, domicilio, idempresa);
                        MessageBox.Show("La empresa se modificó exitosamente");
                        CargarListaEmpresas(this._emprs.TraerListado());
                        LimpiarCampos();
                    }
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
        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    Empresa empr = ObtenerEmpresa();
                    textBox1.Text = empr.RazonSocial;
                    textBox2.Text = empr.Domicilio;
                    textBox3.Text = empr.Cuit.ToString();                    
                    button3.Enabled = true;
                    button2.Enabled = true;
                    button1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar una empresa de la lista.\nAseguresé de que existan empresas dadas de alta.");
            }
        }
        #endregion
    }
}
