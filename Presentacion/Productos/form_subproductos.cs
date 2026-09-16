using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nk_Colletion_New
{
    public partial class form_subproductos : Form
    {
        private class ComboBoxItem
        {
            public int Valor { get; set; }
            public string Texto { get; set; }

            public ComboBoxItem(int valor, string texto)
            {
                Valor = valor;
                Texto = texto;
            }

            public override string ToString()
            {
                return Texto;
            }
        }
        private void CargarCategorias()
        {
            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT id_categoria, nombre_categoria
                FROM categoria
                ORDER BY nombre_categoria;
            ";

                    using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                    using (NpgsqlDataReader lector = comando.ExecuteReader())
                    {
                        cmbcategoria.Items.Clear();

                        while (lector.Read())
                        {
                            cmbcategoria.Items.Add(
                                new ComboBoxItem(
                                    Convert.ToInt32(lector["id_categoria"]),
                                    lector["nombre_categoria"].ToString()
                                )
                            );
                        }
                    }
                }

                cmbcategoria.DisplayMember = "Texto";
                cmbcategoria.ValueMember = "Valor";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las categorías:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarMarcas()
        {
            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT id_marca, nombre
                FROM marca
                WHERE estado = TRUE
                ORDER BY nombre;
            ";

                    using (NpgsqlCommand comando = new NpgsqlCommand(consulta, conexion))
                    using (NpgsqlDataReader lector = comando.ExecuteReader())
                    {
                        cmbmarca.Items.Clear();

                        while (lector.Read())
                        {
                            cmbmarca.Items.Add(
                                new ComboBoxItem(
                                    Convert.ToInt32(lector["id_marca"]),
                                    lector["nombre"].ToString()
                                )
                            );
                        }
                    }
                }

                cmbmarca.DisplayMember = "Texto";
                cmbmarca.ValueMember = "Valor";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las marcas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarTallasSegunTipo()
        {
            cmb_talla.Items.Clear();

            string tipo = txtproducto.Text.Trim().ToLower();

            if (tipo == "")
                return;

            // PANTALONES
            if (tipo.Contains("pantalon") || tipo.Contains("pantalón"))
            {
                cmb_talla.Items.Add("26");
                cmb_talla.Items.Add("28");
                cmb_talla.Items.Add("30");
                cmb_talla.Items.Add("32");
                cmb_talla.Items.Add("34");
                cmb_talla.Items.Add("36");
                cmb_talla.Items.Add("38");
                cmb_talla.Items.Add("40");
                cmb_talla.Items.Add("42");
            }

            // CALZADO
            else if (tipo.Contains("zapato") ||
                     tipo.Contains("tenis") ||
                     tipo.Contains("sandalia") ||
                     tipo.Contains("bota") ||
                     tipo.Contains("tacon") ||
                     tipo.Contains("tacón"))
            {
                cmb_talla.Items.Add("35");
                cmb_talla.Items.Add("36");
                cmb_talla.Items.Add("37");
                cmb_talla.Items.Add("38");
                cmb_talla.Items.Add("39");
                cmb_talla.Items.Add("40");
                cmb_talla.Items.Add("41");
                cmb_talla.Items.Add("42");
                cmb_talla.Items.Add("43");
                cmb_talla.Items.Add("44");
            }

            // ACCESORIOS
            else if (tipo.Contains("bolso") ||
                     tipo.Contains("cartera") ||
                     tipo.Contains("cinturon") ||
                     tipo.Contains("cinturón") ||
                     tipo.Contains("gorra") ||
                     tipo.Contains("bufanda") ||
                     tipo.Contains("mochila"))
            {
                cmb_talla.Items.Add("Única");
            }

            // ROPA
            else
            {
                cmb_talla.Items.Add("XS");
                cmb_talla.Items.Add("S");
                cmb_talla.Items.Add("M");
                cmb_talla.Items.Add("L");
                cmb_talla.Items.Add("XL");
                cmb_talla.Items.Add("XXL");
            }
        }


        private void CargarColores()
        {
            cmb_color.Items.Clear();

            cmb_color.Items.Add("Negro");
            cmb_color.Items.Add("Blanco");
            cmb_color.Items.Add("Azul");
            cmb_color.Items.Add("Rojo");
            cmb_color.Items.Add("Verde");
            cmb_color.Items.Add("Amarillo");
            cmb_color.Items.Add("Gris");
            cmb_color.Items.Add("Beige");
            cmb_color.Items.Add("Rosado");
            cmb_color.Items.Add("Café");
        }

        private string ObtenerAbreviatura(string tipo)
        {
            tipo = tipo.Trim().ToLower();

            switch (tipo)
            {
                case "blusa":
                    return "BLU";

                case "camisa":
                    return "CAM";

                case "vestido":
                    return "VES";

                case "falda":
                    return "FAL";

                case "pantalón":
                case "pantalon":
                    return "PAN";

                case "short":
                    return "SHO";

                case "chaqueta":
                    return "CHA";

                case "camiseta":
                    return "CMA";

                case "polo":
                    return "POL";

                case "sudadera":
                    return "SUD";

                case "zapatos":
                case "zapato":
                    return "ZAP";

                case "tenis":
                    return "TEN";

                case "sandalias":
                case "sandalia":
                    return "SAN";

                case "botas":
                case "bota":
                    return "BOT";

                case "tacones":
                case "tacon":
                case "tacón":
                    return "TAC";

                case "bolso":
                    return "BOL";

                case "cartera":
                    return "CAR";

                case "cinturón":
                case "cinturon":
                    return "CIN";

                case "gorra":
                    return "GOR";

                case "bufanda":
                    return "BUF";

                case "mochila":
                    return "MOC";

                default:
                    return "PRO";
            }
        }

        private string GenerarCodigoProducto(string tipo)
        {
            string abreviatura = ObtenerAbreviatura(tipo);

            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT codigo
                FROM producto
                WHERE codigo LIKE @prefijo
                ORDER BY codigo DESC
                LIMIT 1;
            ";

                    using (NpgsqlCommand comando =
                           new NpgsqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@prefijo", abreviatura + "-%");

                        object resultado = comando.ExecuteScalar();

                        int siguienteNumero = 1;

                        if (resultado != null)
                        {
                            string ultimoCodigo = resultado.ToString();

                            // Ejemplo: CAM-005
                            string numero = ultimoCodigo.Substring(
                                ultimoCodigo.LastIndexOf("-") + 1
                            );

                            if (int.TryParse(numero, out int ultimoNumero))
                            {
                                siguienteNumero = ultimoNumero + 1;
                            }
                        }

                        return abreviatura + "-" +
                               siguienteNumero.ToString("D3");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo generar el código:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return null;
            }
        }

        private void CargarProductos()
        {
            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT
                    p.id_producto AS ""ID"",
                    p.codigo AS ""Código"",
                    tp.nombre_tipo AS ""Tipo de producto"",
                    c.nombre_categoria AS ""Categoría"",
                    m.nombre AS ""Marca"",
                    p.talla AS ""Talla"",
                    p.color AS ""Color"",
                    p.stock_minimo AS ""Stock mínimo"",
                    p.precio_venta AS ""Precio de venta"",
                    p.estado AS ""Estado""
                FROM producto p
                INNER JOIN tipo_producto tp
                    ON p.id_tipo_producto = tp.id_tipo_producto
                INNER JOIN categoria c
                    ON p.id_categoria = c.id_categoria
                INNER JOIN marca m
                    ON p.id_marca = m.id_marca
                ORDER BY p.id_producto;
            ";

                    using (NpgsqlCommand comando =
                           new NpgsqlCommand(consulta, conexion))
                    {
                        using (NpgsqlDataAdapter adaptador =
                               new NpgsqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();

                            adaptador.Fill(tabla);

                            dgvProductos.DataSource = tabla;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public form_subproductos()
        {
            InitializeComponent();
        }
        private void form_subproductos_Load(object sender, EventArgs e)
        {
            // Establecer la fecha actual en el DateTimePicker
            dtpFecha.Value = DateTime.Now;

            //Metodos
            CargarCategorias();
            CargarMarcas();
            CargarColores();

            //Mostrar la fecha actual en el label
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");

            // Configurar el ComboBox de búsqueda
            cmbbuscarpor.Items.Clear();

            cmbbuscarpor.Items.Add("Código");
            cmbbuscarpor.Items.Add("Tipo de producto");
            cmbbuscarpor.Items.Add("Categoría");
            cmbbuscarpor.Items.Add("Marca");
            cmbbuscarpor.Items.Add("Color");
            cmbbuscarpor.Items.Add("Talla");

            cmbbuscarpor.SelectedIndex = 0;

        }




        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Está segura/o de que desea cancelar? Se perderán los datos ingresados.",
            "Confirmar cancelación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {

                txtproducto.Clear();
                cmbcategoria.SelectedIndex = -1;
                cmbmarca.SelectedIndex = -1;
                cmb_color.SelectedIndex = -1;
                cmb_talla.SelectedIndex = -1;
                txtprecioventa.Clear();
                txtcodigo.Clear();
                txtstockmin.Clear();
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            // Validar tipo de producto
            if (string.IsNullOrWhiteSpace(txtproducto.Text))
            {
                MessageBox.Show("Ingrese el tipo de producto.");
                txtproducto.Focus();
                return;
            }

            // Validar código
            if (string.IsNullOrWhiteSpace(txtcodigo.Text))
            {
                MessageBox.Show("Ingrese el código del producto.");
                txtcodigo.Focus();
                return;
            }

            // Validar categoría
            if (cmbcategoria.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                cmbcategoria.Focus();
                return;
            }

            // Validar marca
            if (cmbmarca.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una marca.");
                cmbmarca.Focus();
                return;
            }

            // Validar talla
            if (cmb_talla.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una talla.");
                cmb_talla.Focus();
                return;
            }

            // Validar color
            if (string.IsNullOrWhiteSpace(cmb_color.Text))
            {
                MessageBox.Show("Ingrese o seleccione un color.");
                cmb_color.Focus();
                return;
            }

            // Validar stock mínimo
            if (!int.TryParse(txtstockmin.Text, out int stockMinimo))
            {
                MessageBox.Show("El stock mínimo debe ser un número entero.");
                txtstockmin.Focus();
                return;
            }

            if (stockMinimo < 0)
            {
                MessageBox.Show("El stock mínimo no puede ser negativo.");
                txtstockmin.Focus();
                return;
            }

            // Validar precio
            if (!decimal.TryParse(txtprecioventa.Text, out decimal precioVenta))
            {
                MessageBox.Show("El precio de venta no es válido.");
                txtprecioventa.Focus();
                return;
            }

            if (precioVenta <= 0)
            {
                MessageBox.Show("El precio de venta debe ser mayor que cero.");
                txtprecioventa.Focus();
                return;
            }

            ComboBoxItem categoria =
                (ComboBoxItem)cmbcategoria.SelectedItem;

            ComboBoxItem marca =
                (ComboBoxItem)cmbmarca.SelectedItem;

            string tipoProducto = txtproducto.Text.Trim();
            string talla = cmb_talla.SelectedItem.ToString();
            string color = cmb_color.Text.Trim();

            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    //=========================================
                    // 1. Buscar si el tipo de producto existe
                    // =========================================

                    string buscarTipo = @"
                SELECT id_tipo_producto
                FROM tipo_producto
                WHERE LOWER(nombre_tipo) = LOWER(@nombre)
                AND id_categoria = @categoria;
            ";

                    int idTipoProducto;

                    using (NpgsqlCommand comandoBuscar =
                           new NpgsqlCommand(buscarTipo, conexion))
                    {
                        comandoBuscar.Parameters.AddWithValue(
                            "@nombre", tipoProducto);

                        comandoBuscar.Parameters.AddWithValue(
                            "@categoria", categoria.Valor);

                        object resultado = comandoBuscar.ExecuteScalar();

                        if (resultado != null)
                        {
                            // Ya existe
                            idTipoProducto = Convert.ToInt32(resultado);
                        }
                        else
                        {
                            // =========================================
                            // 2. Crear el tipo de producto
                            // =========================================

                            string insertarTipo = @"
                        INSERT INTO tipo_producto
                        (
                            nombre_tipo,
                            id_categoria
                        )
                        VALUES
                        (
                            @nombre,
                            @categoria
                        )
                        RETURNING id_tipo_producto;
                    ";

                            using (NpgsqlCommand comandoTipo =
                                   new NpgsqlCommand(insertarTipo, conexion))
                            {
                                comandoTipo.Parameters.AddWithValue(
                                    "@nombre", tipoProducto);

                                comandoTipo.Parameters.AddWithValue(
                                    "@categoria", categoria.Valor);

                                idTipoProducto =
                                    Convert.ToInt32(
                                        comandoTipo.ExecuteScalar()
                                    );
                            }
                        }
                    }

                    // =========================================
                    // 3. Guardar el producto
                    // =========================================

                    string insertarProducto = @"
                INSERT INTO producto
                (
                    id_tipo_producto,
                    id_categoria,
                    id_marca,
                    talla,
                    color,
                    fecha,
                    codigo,
                    stock_minimo,
                    precio_venta,
                    estado
                )
                VALUES
                (
                    @tipo,
                    @categoria,
                    @marca,
                    @talla,
                    @color,
                    @fecha,
                    @codigo,
                    @stock,
                    @precio,
                    TRUE
                );
            ";

                    using (NpgsqlCommand comandoProducto =
                           new NpgsqlCommand(insertarProducto, conexion))
                    {
                        comandoProducto.Parameters.AddWithValue(
                            "@tipo", idTipoProducto);

                        comandoProducto.Parameters.AddWithValue(
                            "@categoria", categoria.Valor);

                        comandoProducto.Parameters.AddWithValue(
                            "@marca", marca.Valor);

                        comandoProducto.Parameters.AddWithValue(
                            "@talla", talla);

                        comandoProducto.Parameters.AddWithValue(
                            "@color", color);

                        comandoProducto.Parameters.AddWithValue(
                            "@fecha", dtpFecha.Value.Date);

                        comandoProducto.Parameters.AddWithValue(
                            "@codigo", txtcodigo.Text.Trim());

                        comandoProducto.Parameters.AddWithValue(
                            "@stock", stockMinimo);

                        comandoProducto.Parameters.AddWithValue(
                            "@precio", precioVenta);

                        comandoProducto.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Producto guardado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505")
                {
                    MessageBox.Show(
                        "El código del producto ya existe.",
                        "Código duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Error de PostgreSQL:\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el producto:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void txtproducto_TextChanged(object sender, EventArgs e)
        {
            CargarTallasSegunTipo();

            if (string.IsNullOrWhiteSpace(txtproducto.Text))
            {
                txtcodigo.Clear();
                return;
            }

            txtcodigo.Text = GenerarCodigoProducto(
                txtproducto.Text
            );
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (cmbbuscarpor.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una opción para buscar.");
                return;
            }

            string texto = txtbuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarProductos();
                return;
            }

            string opcion = cmbbuscarpor.SelectedItem.ToString();

            string campo;

            switch (opcion)
            {
                case "Código":
                    campo = "p.codigo";
                    break;

                case "Tipo de producto":
                    campo = "tp.nombre_tipo";
                    break;

                case "Categoría":
                    campo = "c.nombre_categoria";
                    break;

                case "Marca":
                    campo = "m.nombre";
                    break;

                case "Color":
                    campo = "p.color";
                    break;

                case "Talla":
                    campo = "p.talla";
                    break;

                default:
                    return;
            }

            BuscarProductos(campo, texto);
        }
    

    private void BuscarProductos(string campo, string texto)
        {
            try
            {
                Conexion_BD conexionBD = new Conexion_BD();

                using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = $@"
                SELECT
                    p.id_producto,
                    p.codigo,
                    tp.nombre_tipo AS tipo_producto,
                    c.nombre_categoria AS categoria,
                    m.nombre AS marca,
                    p.talla,
                    p.color,
                    p.stock_minimo,
                    p.precio_venta,
                    p.estado
                FROM producto p
                INNER JOIN tipo_producto tp
                    ON p.id_tipo_producto = tp.id_tipo_producto
                INNER JOIN categoria c
                    ON p.id_categoria = c.id_categoria
                INNER JOIN marca m
                    ON p.id_marca = m.id_marca
                WHERE {campo} ILIKE @texto
                ORDER BY p.id_producto;
            ";

                    using (NpgsqlCommand comando =
                           new NpgsqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@texto", "%" + texto + "%");

                        using (NpgsqlDataAdapter adaptador =
                               new NpgsqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();

                            adaptador.Fill(tabla);

                            dgvProductos.DataSource = tabla;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar productos:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    }


    }
    

