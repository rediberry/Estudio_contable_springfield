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
    public partial class Form4 : Form
    {
        private CategoriasServicio _cs;
        private EmpresaServicio _emprs;
        private EmpleadoServicio _empls;
        private LiquidacionesServicio _ls;
        public Form4()
        {
            this._cs = new CategoriasServicio();
            this._emprs = new EmpresaServicio();
            this._empls = new EmpleadoServicio();
            this._ls = new LiquidacionesServicio();
            InitializeComponent();
        }
        #region métodos
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
        private void CargarListaLiquidaciones(List<Liquidaciones> lliq)
        {
            listBox2.DataSource = null;
            listBox2.Sorted = true;
            listBox2.DataSource = lliq;
            listBox2.SelectedItem = null;
        }
        private void LimpiarCampos2()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox5.Enabled = false;
            listBox2.SelectedItem = null;            
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            button2.Enabled = false;            
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
        private double ObtenerSueldoBasicoCategoria(Empleado empl)
        {
            double sueldo = 0;
            foreach (Categorias c in _cs.TraerListado())
            {
                if (c.id == empl.IdCategoria)
                {
                    sueldo = c.SueldoBasico;
                    break;
                }
            }
            return sueldo;
        }
        private double ObtenerBrutoTotalPorEmpresa()
        {
            double monto = 0;
            foreach (Empleado empl in _empls.TraerListado())
            {
                if (empl.IdEmpresa == ObtenerIdEmpresa())
                {
                    
                    monto += ObtenerSueldoBasicoCategoria(empl);
                }
            }
            return monto;
        }
        private int ObtenerIdLiquidacion()
        {
            int idempleado = ObtenerIdEmpleado();
            int periodo = int.Parse(comboBox3.Text + comboBox2.Text);
            int idliq = int.Parse(periodo.ToString() + idempleado.ToString());
            return idliq;
        }
        private Boolean ValidarUnicidadLiquidacion(int idliquidacion)//el id de la liquidacion en este caso es idempresa + periodo
        {
            bool valido = true;
            string msg = string.Empty;
            List<int> listaidliquidaciones = new List<int>();
            List<Liquidaciones> listliquidaciones = _ls.TraerListadoPorEmpleado(ObtenerIdEmpleado());
            foreach (Liquidaciones l in listliquidaciones)
            {
                int idliq = int.Parse(l.Periodo.ToString() +l.idEmpleado.ToString());
                listaidliquidaciones.Add(idliq);
            }
            if (listaidliquidaciones.Any(x => x == idliquidacion))
            {
                msg = "El periodo ya se encuentra liquidado para este empleado.";
            }
            if (msg != string.Empty)
            {
                valido = false;
                MessageBox.Show(msg);
            }
            return valido;
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
        private Empleado ObtenerEmpleado()
        {
            Empleado e = new Empleado("", "", DateTime.Now, 2033333330, 1, 1);
            foreach (Empleado empl in _empls.TraerListado())
            {
                if (empl.ToString() == listBox1.SelectedItem.ToString())
                {
                    e = empl;
                }
            }
            return e;
        }
        private Liquidaciones ObtenerLiquidacion()
        {
            Liquidaciones liq = new Liquidaciones(1, 1, "", 1, 1);
            foreach (Liquidaciones l in _ls.TraerListadoPorEmpleado(ObtenerIdEmpleado()))
            {
                if (l.ToString() == listBox2.SelectedItem.ToString())
                {
                    liq = l;
                }
            }
            return liq;
        }
        private Boolean ValidarLiquidacionesPosteriores(int idliquidacionseleccionada)
        {
            Boolean validacion = false;
            List<Liquidaciones> listliquidaciones = _ls.TraerListadoPorEmpleado(ObtenerIdEmpleado());
            foreach (Liquidaciones l in listliquidaciones)
            {
                if (l.id > idliquidacionseleccionada)
                {
                    validacion = true;
                    break;
                }
            }            
            return validacion;
        }
        #endregion

        #region eventos
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
        private void Form4_Load(object sender, EventArgs e)
        {
            CargarComboEmpresas(_emprs.TraerListado());            
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;            
            listBox1.Enabled = false;            
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                listBox2.DataSource = null;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                textBox5.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
            listBox1.Enabled = true;            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem == null)
                {
                    if (ValidarUnicidadLiquidacion(ObtenerIdLiquidacion()))
                    {
                        try
                        {
                            int idempleado = ObtenerIdEmpleado();
                            int periodo = int.Parse(comboBox3.Text + comboBox2.Text);
                            string codigotransferencia = textBox5.Text;
                            double bruto = ObtenerSueldoBasicoCategoria(ObtenerEmpleado());
                            double descuentos = bruto * 0.17;
                            this._ls.AltaLiquidacion(idempleado, periodo, codigotransferencia, bruto, descuentos);
                            MessageBox.Show("La liquidación se dió de alta exitosamente");
                            CargarListaLiquidaciones(_ls.TraerListadoPorEmpleado(ObtenerIdEmpleado()));
                            textBox5.Clear();
                            comboBox2.SelectedItem = null;
                            comboBox3.SelectedItem = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, no se pudo grabar la liquidación.\n" + ex.Message);
                        }
                    }

                }
                if (listBox2.SelectedItem != null)
                {
                    try
                    {
                        Liquidaciones l = ObtenerLiquidacion();
                        int idempleado = l.idEmpleado;
                        int periodo = l.Periodo;
                        string codigotransferencia = textBox5.Text;
                        double bruto = l.Bruto;
                        double descuentos = l.Descuentos;
                        int idliquidacion = l.id;
                        this._ls.ModificarLiquidacion(idempleado, periodo, codigotransferencia, bruto, descuentos, idliquidacion);
                        MessageBox.Show("La liquidación se modificó exitosamente");
                        CargarListaLiquidaciones(_ls.TraerListadoPorEmpleado(ObtenerIdEmpleado()));
                        textBox5.Clear();
                        comboBox2.SelectedItem = null;
                        comboBox3.SelectedItem = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar.\n" + ex.Message);
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    textBox5.Enabled = true;
                    textBox5.Clear();
                    comboBox2.SelectedItem = null;
                    comboBox3.SelectedItem = null;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = false;
                    textBox1.Text = ObtenerSueldoBasicoCategoria(ObtenerEmpleado()).ToString("#,###,###.00");
                    textBox2.Text = ((ObtenerSueldoBasicoCategoria(ObtenerEmpleado()) * 0.17).ToString("#,###,###.00"));
                    textBox3.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    textBox4.Text = ((ObtenerSueldoBasicoCategoria(ObtenerEmpleado()) * 0.83).ToString("#,###,###.00"));
                    CargarListaLiquidaciones(_ls.TraerListadoPorEmpleado(ObtenerIdEmpleado()));
                }                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un empleado de la lista.\nAseguresé de que existan empleados dados de alta.");
            }            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
        }
        private void listBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem != null)
                {
                    Liquidaciones l = ObtenerLiquidacion();
                    textBox5.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;                    
                    textBox1.Text = l.Bruto.ToString("#,###,###.00");                    
                    textBox2.Text = l.Descuentos.ToString("#,###,###.00");                    
                    textBox3.Text = l.FechaAlta.ToString("dd/MM/yyyy");                    
                    textBox4.Text = l.SalarioNeto.ToString("#,###,###.00");                    
                    textBox5.Text = l.CodigoTransferencia;                    
                    comboBox3.SelectedIndex = comboBox3.FindString(l.Periodo.ToString().Substring(0,4));                    
                    comboBox2.SelectedIndex = comboBox2.FindString(l.Periodo.ToString().Substring(4));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la liquidación.\nAseguresé de que existan liquidaciones cargadas.");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = ObtenerLiquidacion().id;
                if (!ValidarLiquidacionesPosteriores(id))
                {
                    this._ls.EliminarLiquidacion(id);
                    MessageBox.Show("La liquidación se eliminó exitosamente");
                    LimpiarCampos2();
                    button3.Enabled = false;
                    button4.Enabled = false;
                    CargarListaLiquidaciones(_ls.TraerListadoPorEmpleado(ObtenerIdEmpleado()));
                }
                else
                {
                    MessageBox.Show("No se puede eliminar la liquidación.\nPosee liquidaciones posteriores.");
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
