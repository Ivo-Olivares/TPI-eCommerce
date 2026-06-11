using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategorias.DataSource = negocio.Listar();
                dgvCategorias.DataBind();
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdCategoria"] != null)
                {
                    Categoria categoria = new Categoria();

                    categoria.Id = (int)ViewState["IdCategoria"];
                    categoria.Nombre = txtNombreCategoria.Text.Trim();

                    CategoriaNegocio negocio = new CategoriaNegocio();

                    negocio.ModificarCategoria(categoria);
                    dgvCategorias.DataSource = negocio.Listar();
                    dgvCategorias.DataBind();

                    ViewState["IdCategoria"] = null;
                    txtNombreCategoria.Text = "";
                    lblError.Text = "";

                    btnAgregarCategoria.Text = "Agregar Categoría";
                }
                else
                {
                    Categoria categoria = new Categoria();
                    categoria.Nombre = txtNombreCategoria.Text.Trim();

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.AgregarCategoria(categoria);
                    dgvCategorias.DataSource = negocio.Listar();
                    dgvCategorias.DataBind();

                    txtNombreCategoria.Text = "";
                    lblError.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = dgvCategorias.SelectedRow;
            txtNombreCategoria.Text = fila.Cells[1].Text;
            ViewState["IdCategoria"] = dgvCategorias.SelectedDataKey.Value;
            btnAgregarCategoria.Text = "Modificar Categoría";
            btnCancelar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ViewState["IdCategoria"] = null;
            txtNombreCategoria.Text = "";
            lblError.Text = "";
            btnAgregarCategoria.Text = "Agregar Categoría";
            btnCancelar.Visible = false;
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            CategoriaNegocio negocio = new CategoriaNegocio();

            if (e.CommandName == "Editar")
            {
                Categoria categoria = negocio.Listar().Find(x => x.Id == id);

                txtNombreCategoria.Text = categoria.Nombre;

                ViewState["IdCategoria"] = categoria.Id;

                btnAgregarCategoria.Text = "Modificar Categoria";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                Categoria categoria = new Categoria();
                categoria.Id = id;

                negocio.DesactivarCategoria(categoria);

                dgvCategorias.DataSource = negocio.Listar();
                dgvCategorias.DataBind();
            }
        }
    }
}