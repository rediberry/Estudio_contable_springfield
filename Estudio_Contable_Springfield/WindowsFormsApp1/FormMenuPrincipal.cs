using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormMenuPrincipal : Form
    {
        private EmpresaServicio _emprs;
        public FormMenuPrincipal()
        {
            InitializeComponent();
            //Estas lineas eliminan los parpadeos del formulario o controles en la interfaz grafica (Pero no en un 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            PersonalizarDiseño();
            this._emprs = new EmpresaServicio();
        }
        #region metodos
        private void CargarListaEmpresas(List<Empresa> le)
        {
            listBox1.DataSource = null;
            listBox1.Sorted = true;
            listBox1.DataSource = le;
            listBox1.DisplayMember = "NombreAbreviadoEmpresa";
            listBox1.SelectedItem = null;
        }
        private void PersonalizarDiseño()
        {
            panelSubmenu.Visible = false;
        }
        private void OcultarSubmenu()
        {
            if (panelSubmenu.Visible == true)
                panelSubmenu.Visible = false;
        }
        private void MostrarSubmenu(Panel submenu)
        {
            if (panelSubmenu.Visible == false)
            {
                OcultarSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }        
        //----------------METODO PARA ARRASTRAR EL FORMULARIO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public interface IFormulario
        {
            void InicializarParametros(params object[] parametros);
        }
        //----------------Método para Abrir formularios en el panel Contenedor
        private void AbrirFormulario<MiForm>(params object[] args) where MiForm : Form, IFormulario, new()
        {
            Form formulario;
            formulario = panelFormularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
                                                                                     //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelFormularios.Controls.Add(formulario);
                panelFormularios.Tag = formulario;
                formulario.Show();
                ((IFormulario)formulario).InicializarParametros(args);
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }
        //----------------METODO CAMBIAR COLOR DE BOTON AL CERRAR FORM
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
                button1.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form2"] == null)
                button3.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form3"] == null)
                button4.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form4"] == null)
                button5.BackColor = Color.FromArgb(4, 41, 68);
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
        private void CerrarFormulariosActivos()
        {
            Form[] formsList = Application.OpenForms.Cast<Form>().Where(x => x.Name == "Form1" || x.Name == "Form2" || x.Name == "Form3" || x.Name == "Form4").ToArray();
            foreach (Form openForm in formsList)
            {
                openForm.Close();
            }
        }
        #endregion

        #region eventos              
        private void button1_Click(object sender, EventArgs e)
        {
            MostrarSubmenu(panelSubmenu);
            button1.BackColor = Color.FromArgb(12, 61, 92);
            CargarListaEmpresas(_emprs.TraerListado());
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            CerrarFormulariosActivos();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.AbrirFormulario<Form1>();
            button2.BackColor = Color.FromArgb(12, 61, 92);
            OcultarSubmenu();
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.AbrirFormulario<Form2>(ObtenerEmpresa());
            button3.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.AbrirFormulario<Form3>();
            button4.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.AbrirFormulario<Form4>(ObtenerEmpresa());
            button5.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnRestaurar_Click_1(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }
        private void btnMaximizar_Click_1(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            OcultarSubmenu();
            button1.BackColor = Color.FromArgb(4, 41, 68);
            
        }
        private void FormMenuPrincipal_Load_1(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }
        private void panelBarraTitulo_MouseMove_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }        
        #endregion       
    }
}
