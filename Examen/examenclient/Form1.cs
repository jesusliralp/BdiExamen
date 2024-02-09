using apiexamen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace examenclient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            int Id = (int)numId.Value;
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;

            try
            {
                apiexamen.ClsExamen.AgregarExamen(Id, Nombre, Descripcion);
            }
            catch
            {
                Prompt.ShowDialog("Error al crear", "Error al crear");
            }

            if (apiexamen.ClsExamen.Errors.Count() > 0)
            {
                Prompt.ShowDialog(string.Join(". ", apiexamen.ClsExamen.Errors), "Error al crear");
                apiexamen.ClsExamen.Errors.Clear();
            }
            else
            {
                Prompt.ShowDialog("Creado exitosamente", "Creado exitosamente");
            }

            numId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            btnRefresh.PerformClick();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string Id = numId.Text;
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;

            IEnumerable<apiexamen.Examen> examenes = apiexamen.ClsExamen.ConsultarExamen(Id, Nombre, Descripcion);

            dgvConsulta.Rows.Clear();

            foreach (apiexamen.Examen examen in examenes)
            {
                dgvConsulta.Rows.Add(examen.IdExamen, examen.Nombre, examen.Descripcion);
            }

            if (apiexamen.ClsExamen.Errors.Count() > 0)
            {
                Prompt.ShowDialog(string.Join(". ", apiexamen.ClsExamen.Errors), "Error al actualizar");
                apiexamen.ClsExamen.Errors.Clear();
            }
        }

        public void dgvConsulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            if (row >= dgvConsulta.Rows.Count - 1) return;

            Examen examen = new Examen
            {
                IdExamen = int.Parse(dgvConsulta.Rows[row].Cells[0].Value.ToString()),
                Nombre = dgvConsulta.Rows[row].Cells[1].Value.ToString(),
                Descripcion = dgvConsulta.Rows[row].Cells[2].Value.ToString(),
            };

            numId.Value = int.Parse(dgvConsulta.Rows[row].Cells[0].Value.ToString());
            txtNombre.Text = dgvConsulta.Rows[row].Cells[1].Value.ToString();
            txtDescripcion.Text = dgvConsulta.Rows[row].Cells[2].Value.ToString();

            if (apiexamen.ClsExamen.Errors.Count() > 0)
            {
                Prompt.ShowDialog(string.Join(". ", apiexamen.ClsExamen.Errors), "Error al actualizar");
                apiexamen.ClsExamen.Errors.Clear();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = (int)numId.Value;
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;

            try
            {
                apiexamen.ClsExamen.ActualizarExamen(Id, Nombre, Descripcion);
            }
            catch
            {
                Prompt.ShowDialog("Error al actualizar", "Error al actualizar");
            }

            numId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            btnRefresh.PerformClick();

            if (apiexamen.ClsExamen.Errors.Count() > 0)
            {
                Prompt.ShowDialog(string.Join(". ", apiexamen.ClsExamen.Errors), "Error al actualizar");
                apiexamen.ClsExamen.Errors.Clear();
            }
            else
            {
                Prompt.ShowDialog("Actualizado exitosamente", "Actualizado exitosamente");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = (int)numId.Value;

            try
            {
                apiexamen.ClsExamen.EliminarExamen(Id);
            }
            catch
            {
                Prompt.ShowDialog("Error al eliminar", "Error al eliminar");
            }
            
            numId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            btnRefresh.PerformClick();

            if (apiexamen.ClsExamen.Errors.Count() > 0)
            {
                Prompt.ShowDialog(string.Join(". ", apiexamen.ClsExamen.Errors), "Error al actualizar");
                apiexamen.ClsExamen.Errors.Clear();
            }
            else
            {
                Prompt.ShowDialog("Eliminado exitosamente", "Eliminado exitosamente");
            }
        }

        private void radioButtonStoredProcedure_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonStoredProcedure.Checked == true)
            {
                apiexamen.ClsExamen.SetDalProvider(apiexamen.ClsExamen.DAL_PROVIDER_ENUM.STORED_PROCEDURES);
            }
        }

        private void radioButtonWebService_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonWebService.Checked == true)
            {
                apiexamen.ClsExamen.SetDalProvider(apiexamen.ClsExamen.DAL_PROVIDER_ENUM.WEB_SERVICE);
            }
        }
    }
}
