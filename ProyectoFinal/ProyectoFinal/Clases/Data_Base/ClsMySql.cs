using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;
using System.IO;


namespace ProyectoFinal.Clases.Data_Base
{
    class ClsMySql
    {
        //variables gobales
        public MySqlConnection conexion;
        private string Str_conexion { get; }

        //Constructor 
        public ClsMySql()
        {
            this.Str_conexion = "Database=productos_tecnologicos; Data Source=localhost; User Id=root; Password=12345678;";
        }

        //Apertura de la base de datos
        public void AbrirMySql()
        {
            try
            {
                this.conexion = new MySqlConnection(this.Str_conexion);
                conexion.Open();
            }
            catch (MySqlException er)
            {
                Console.WriteLine($"Error de conexion: {er}");
            }
        }

        //Cierre de base de datos 
        public void CerrarMysql()
        {
            this.conexion.Close();

        }
        //Insercion y Actuaizacion de datos.
        public void CrudMysql(string string_ejecutar)
        {
            AbrirMySql();
            try
            {
                MySqlCommand comand = new MySqlCommand(string_ejecutar, this.conexion);
                comand.ExecuteReader();
                MessageBox.Show("Ejecucion realizada");
            }
            catch (MySqlException er)
            {
                MessageBox.Show($"Error al hacer ejecucion de comando: {er}");
            }
            finally
            {
                CerrarMysql();
            }
        }
        //Consultas en MySql
        public string ConsultasMysql(string consulta = "SHOW DATABASES")
        {
            string datos_obtenidos = "";
            MySqlDataReader reader = null;
            AbrirMySql();
            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, this.conexion);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    datos_obtenidos += reader.GetString(0)+";" + reader.GetString(1)+";"+reader.GetString(2)+";"+reader.GetString(3)+"\n";
                }
            }
            catch (MySqlException er)
            {
                MessageBox.Show($"Error de consulta: {er}");
            }
            finally
            {
                CerrarMysql();
            }
            return datos_obtenidos;
        }
        public void gn_ar(string datos)
        {
            try
            {
                string ruta = @"C:\Users\USUARIOTC\source\repos\ProyectoFinal\ProyectoFinal\Archivos_Creados\consulta.csv";
                
                StreamWriter car = File.AppendText(ruta);
                car.WriteLine(datos);
                car.Close();
                MessageBox.Show("Archivo creado");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar archivo: {ex}");
            }

        }

        //Se arma el string para la insercion de datos
        public string Arm_String_Insert(string id, string nombre, string marca, string fecha, string precio, string existencias)
        {
            string cadena_insert = $"INSERT INTO productos (Id_Producto,Nombre,Marca,Fecha_Ingreso,Precio,Existencias) VALUE ({id},'{nombre}','{marca}','{fecha}',{precio},{existencias});";
            return cadena_insert;
        }

        //Se arma el string para actulaizacion
        public string Arm_String_Update(string id, string nombre, string marca, string fecha, string precio, string existencias)
        {
            string cadena_update = $"UPDATE productos SET Nombre='{nombre}',Marca='{marca}',Fecha_Ingreso='{fecha}',Precio={precio},Existencias={existencias} WHERE Id_Producto={id}; ";
            return cadena_update;
        }
        //Se arma el strin para la eliminacin a travez de Id
        public string Arm_String_Delete(string id)
        {
            string cadena_delete = $"DELETE FROM productos WHERE Id_Producto={id};";
            return cadena_delete;
        }

        //Eliminacion de datos con advertecia
        public void Eliminacion(string delete_string)
        {
            AbrirMySql();
            try
            {
                var advertencia = MessageBox.Show("¿Desea Eliminar?", "Eliminar", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (advertencia == MessageBoxResult.OK)
                {
                    MySqlCommand comand = new MySqlCommand(delete_string, this.conexion);
                    comand.ExecuteReader();
                    MessageBox.Show("Eliminacion Completada");
                }
                else
                {
                    MessageBox.Show("Accion cancelada");
                }
            }
            catch (MySqlException er)
            {
                MessageBox.Show($"Error al eliminar: {er}");
            }
            finally
            {
                CerrarMysql();
            }
        }
        public DataTable Mostrar_datos(string consulta = "SELECT *FROM productos;")
        {
           
            AbrirMySql();
            DataTable dt = new DataTable();
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand(consulta, this.conexion);
            try
            {
                reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (MySqlException er)
            {
                MessageBox.Show($"Error al mostrar datos: {er}");
            }
            finally
            {
                CerrarMysql();
            }

            return dt;
        }
    }
}
