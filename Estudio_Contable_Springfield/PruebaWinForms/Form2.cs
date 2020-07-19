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
    public partial class Form2 : Form
    {
        private CategoriasServicio _cs;
        private EmpresaServicio _emprs;
        private EmpleadoServicio _empls;        
        public Form2()
        {
            this._cs = new CategoriasServicio();
            this._emprs = new EmpresaServicio();
            this._empls = new EmpleadoServicio();            
            InitializeComponent();
        }
        #region métodos
        private void CargarComboCategorias(List<Categorias> lc)
        {
            comboBox2.DataSource = null;
            comboBox2.DataSource = lc;
            comboBox2.SelectedItem = null;
        }
        private void CargarComboEmpresas(List<Empresa> lempr)
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = lempr;
            comboBox1.SelectedItem = null;
        }       
        
        private void CargarListaEmpleados(List<Empleado> lempl)
        {
            listBox1.DataSource = null;
            listBox1.Sorted = true;
            listBox1.DataSource = lempl;            
            listBox1.SelectedItem = null;
        }               
        private void LimpiarCampos2()
        {
            textBox1.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox10.Clear();
            comboBox2.SelectedItem = null;            
            listBox1.SelectedItem = null;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
        }
        private Boolean ValidarCampos()
        {
            bool valido = true;
            string msg = string.Empty;
            if (ValidacionHelper.ValidarStringNombre(textBox1.Text) == "" || ValidacionHelper.ValidarStringNombre(textBox5.Text) == "" || ValidacionHelper.ValidarCuit(textBox6.Text) == false || ValidacionHelper.ValidarDatetime(textBox10.Text) == false)
            {
                msg = "Debe ingresar valores validos en los campos Nombre, Apellido, CUIL y Fecha de nacimiento.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
        }
        private Boolean ValidarUnicidadCuil(Int64 cuit)
        {
            bool valido = true;
            string msg = string.Empty;
            List<long> listcuit = new List<long>();
            List<Empleado> listempleados = _empls.TraerListadoPorEmpresa(ObtenerIdEmpresa());
            foreach (Empleado e in listempleados)
            {
                listcuit.Add(e.Cuil);
            }
            if (listcuit.Any(x => x == cuit))
            {
                msg = "El Cuil ya se encuentra ingresado.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
        }
        private int ObtenerIdEmpresa()
        {  
            int id = 0;
            foreach (Empresa e in _emprs.TraerListado())
            {
                if (e.ToString() == comboBox1.SelectedItem.ToString())
                {
                    id = e.id;
                }
            }
            return id;           
        }
        private int ObtenerIdCategoria()
        {
            int id = 0;
            foreach (Categorias c in _cs.TraerListado())
            {
                if (c.ToString() == comboBox2.SelectedItem.ToString())
                {
                    id = c.id;
                }
            }
            return id;
        }
        private string ObtenerCategoria(Empleado empl)
        {
            string categoria = string.Empty;
            foreach (Categorias c in _cs.TraerListado())
            {
                if (c.id == empl.IdCategoria)
                {
                    categoria = c.ToString();
                    break;
                }                
            }
            return categoria;
        }
        private int ObtenerIdEmpleado()
        {
            int id = 0;
            foreach (Empleado empl in _empls.TraerListado())
            {
                if (empl.ToString() == listBox1.SelectedItem.ToString())
                {
                    id = empl.id;
                }
            }
            return id;
        }
        private string FormatoString(string s)
        {
            return s.First().ToString().ToUpper() + String.Join("", s.Skip(1)).ToLower();
        }
        #endregion
        #region eventos
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {            
            CargarComboEmpresas(_emprs.TraerListado());            
            comboBox2.Enabled = false;            
            textBox1.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox10.Enabled = false;
            listBox1.Enabled = false;           
            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos() 
                    && ValidarUnicidadCuil(Convert.ToInt64(textBox6.Text)))
                {
                    try
                    {
                        string nombre = FormatoString(textBox1.Text);
                        string apellido = FormatoString(textBox5.Text);
                        Int64 cuil = Convert.ToInt64(textBox6.Text);
                        DateTime fechanac = Convert.ToDateTime(textBox10.Text);
                        int idempresa = ObtenerIdEmpresa();
                        int idcategoria = ObtenerIdCategoria();
                        this._empls.AltaEmpleado(nombre, apellido, fechanac, cuil, idempresa, idcategoria);
                        MessageBox.Show("El empleado se dió de alta exitosamente");
                        LimpiarCampos2();
                        CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error al hacer click en grabar.\n" + ex.Message);
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
            try
            {
                int id = ObtenerIdEmpleado();
                this._empls.EliminarEmpleado(id);
                MessageBox.Show("El empleado se eliminó exitosamente");                
                LimpiarCampos2();
                CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
        }        
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
            listBox1.Enabled = true;
            comboBox2.Enabled = true;
            textBox1.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox10.Enabled = true;
            CargarComboCategorias(_cs.TraerListado());
            LimpiarCampos2();
        }        
        private void listBox1_Click(object sender, EventArgs e)
        {
            Empleado empl = _empls.ObtenerEmpleado(ObtenerIdEmpleado());
            textBox1.Text = empl.Nombre;
            textBox5.Text = empl.Apellido;
            textBox6.Text = empl.Cuil.ToString();
            textBox10.Text = empl.FechaNacimiento.ToString("dd/MM/yyyy");
            comboBox2.SelectedIndex = comboBox2.FindString(ObtenerCategoria(empl));            
            button2.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox10.Clear();
            comboBox2.SelectedItem = null;
            listBox1.SelectedItem = null;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {         
            button4.Enabled = true;
            button3.Enabled = true;
        }
        #endregion
    }
}
