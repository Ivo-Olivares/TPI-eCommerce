using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Datos;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MarcaNegocio negocio = new MarcaNegocio();
                dgvMarcas.DataSource = negocio.ListarMarcas();
                dgvMarcas.DataBind();
            }
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                marca.Nombre = txtNombreMarca.Text;
                MarcaNegocio negocio = new MarcaNegocio();
                negocio.AgregarMArca(marca);

                dgvMarcas.DataSource = negocio.ListarMarcas();
                dgvMarcas.DataBind();

                txtNombreMarca.Text = "";
                lblError.Text = "";

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }


        }


        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow fila = dgvMarcas.SelectedRow;
            hfIdMarca.Value = fila.Cells[0].Text;
            txtNombreMarca.Text = fila.Cells[1].Text;
        }

        protected void btnModificarMarca_Click(object sender, EventArgs e)
        {
            try
            {
              Marca marca = new Marca();  
              marca.Id =int.Parse(hfIdMarca.Value);
              marca.Nombre = txtNombreMarca.Text;

              MarcaNegocio negocio = new MarcaNegocio();
              negocio.ModificarMarca(marca);

              dgvMarcas.DataSource = negocio.ListarMarcas();  
              dgvMarcas.DataBind();

              txtNombreMarca.Text = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            
        }

        protected void btnDesactivarMarca_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();
            marca.Id = int.Parse(hfIdMarca.Value);
            
            MarcaNegocio negocio = new MarcaNegocio();
            negocio.DesactivarMarca(marca);
            
            dgvMarcas.DataSource = negocio.ListarMarcas();
            dgvMarcas.DataBind();
            
            txtNombreMarca.Text = "";
            hfIdMarca.Value = "";   

        }

        protected void btnActicarMarca_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();  
            marca.Id = int.Parse(hfIdMarca.Value);

            MarcaNegocio negocio = new MarcaNegocio();
            negocio.ActivarMarca(marca);

            dgvMarcas.DataSource = negocio.ListarMarcas();
            dgvMarcas.DataBind();

            if (chkMostrarInactivas.Checked)
            {
                dgvMarcas.DataSource = negocio.ListarInactivas();
            }
            else
            {
                dgvMarcas.DataSource = negocio.ListarMarcas();
            }
               
            dgvMarcas.DataBind();

            txtNombreMarca.Text = "";
            hfIdMarca.Value = "";
        }

        protected void chkMostrarInactivas_CheckedChanged(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            if(chkMostrarInactivas.Checked)
            {
                dgvMarcas.DataSource = negocio.ListarInactivas();
                
            }
            else
            {
                dgvMarcas.DataSource = negocio.ListarMarcas();
                
            }

            dgvMarcas.DataBind();
        }
        }
}