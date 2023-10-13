using LogicaNegocios;

namespace Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Class1 clase = new Class1();
            dgvClientes.DataSource = clase.ObtenerClientesPorNombre("Juan");
        }
    }
}