﻿using Entidades;
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
            listBox1.SelectedItem = null;
            //comboBox1.SelectedItem = null;
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
            int idempresa = ObtenerIdEmpresa();
            int periodo = int.Parse(comboBox3.Text + comboBox2.Text);
            int idliq = int.Parse(periodo.ToString() + idempresa.ToString());
            return idliq;
        }
        private Boolean ValidarUnicidadLiquidacion(int idliquidacion)//el id de la liquidacion en este caso es idempresa + periodo
        {
            bool valido = true;
            string msg = string.Empty;
            List<int> listaidliquidaciones = new List<int>();
            List<Liquidaciones> listliquidaciones = _ls.TraerListadoPorEmpresa(ObtenerIdEmpresa());
            foreach (Liquidaciones l in listliquidaciones)
            {
                int idliq = int.Parse(l.Periodo.ToString() +l.idEmpresa.ToString());
                listaidliquidaciones.Add(idliq);
            }
            if (listaidliquidaciones.Any(x => x == idliquidacion))
            {
                msg = "El periodo ya se encuentra liquidado.";
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
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
                CargarListaLiquidaciones(_ls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.\n" + ex.Message);
            }
            listBox1.Enabled = true;
            textBox5.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            button2.Enabled = true;
            textBox1.Text = ObtenerBrutoTotalPorEmpresa().ToString("#,###,###.00");
            textBox2.Text = (ObtenerBrutoTotalPorEmpresa()*0.17).ToString("#,###,###.00");  
            textBox3.Text = DateTime.Today.ToString("dd/MM/yyyy");
            textBox4.Text = (ObtenerBrutoTotalPorEmpresa() * 0.83).ToString("#,###,###.00");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarUnicidadLiquidacion(ObtenerIdLiquidacion()))
                {
                    try
                    {
                        int idempresa = ObtenerIdEmpresa();
                        int periodo = int.Parse(comboBox3.Text + comboBox2.Text);
                        string codigotransferencia = textBox5.Text;
                        double bruto = ObtenerBrutoTotalPorEmpresa();
                        double descuentos = bruto * 0.17;
                        this._ls.AltaLiquidacion(idempresa, periodo, codigotransferencia, bruto, descuentos);
                        MessageBox.Show("La liquidación se dió de alta exitosamente");
                        LimpiarCampos2();
                        CargarListaEmpleados(_empls.TraerListadoPorEmpresa(ObtenerIdEmpresa()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, no se pudo grabar la liquidación.\n" + ex.Message);
                    }
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