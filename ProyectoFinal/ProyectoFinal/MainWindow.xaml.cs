using ProyectoFinal.Clases.Data_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;



namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mostrar_table();
        }

        ClsMySql CnMysql = new ClsMySql();

        //Limpiado de datos
        public void Limpiar()
        {
            txt_ID.Clear();
            txt_Nombre.Clear();
            txt_Marca.Clear();
            txt_Fecha.Clear();
            txt_Precio.Clear();
            txt_Existencias.Clear();
            txt_range1.Clear();
            txt_range2.Clear();
            lisbox_resultados.Items.Clear();
        }
        //Limpia la busqueda y la eliminacion 
        public void Clean_search()
        {
            txt_id_delete.Clear();
        }
        //metodo para mostrar la tabla 
        public void Mostrar_table()
        {
            DataTable dat_table = CnMysql.Mostrar_datos();
            datagrid_table.ItemsSource = dat_table.DefaultView;
        }
        private void btn_Insertar(object sender, RoutedEventArgs e)
        {
            string insert = CnMysql.Arm_String_Insert(txt_ID.Text, txt_Nombre.Text, txt_Marca.Text, txt_Fecha.Text, txt_Precio.Text, txt_Existencias.Text);
            CnMysql.CrudMysql(insert);
            Mostrar_table();
            Limpiar();
        }

        private void Btn_Actualizar(object sender, RoutedEventArgs e)
        {
            string update = CnMysql.Arm_String_Update(txt_ID.Text, txt_Nombre.Text, txt_Marca.Text, txt_Fecha.Text, txt_Precio.Text, txt_Existencias.Text);
            CnMysql.CrudMysql(update);
            Mostrar_table();
            Limpiar();
        }

        private void Btn_Borrar(object sender, RoutedEventArgs e)
        {
            string delete = CnMysql.Arm_String_Delete(txt_id_delete.Text);
            CnMysql.Eliminacion(delete);
            Mostrar_table();
            Limpiar();
        }

        private void Btn_Consultas(object sender, RoutedEventArgs e)
        {
            string consult = CnMysql.ConsultasMysql($"SELECT nombre, marca, existencias, fecha_ingreso from productos WHERE fecha_ingreso BETWEEN '{txt_range1.Text}' AND '{txt_range2.Text}' ORDER BY nombre DESC;");
            consult = consult.ToString();
            CnMysql.gn_ar(consult);
            lisbox_resultados.Items.Add(consult);

            

        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
