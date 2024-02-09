using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace examenclient
{
    partial class Form1
    {
        NumericUpDown numId;
        Label lblId;
        TextBox txtNombre;
        Label lblNombre;
        TextBox txtDescripcion;
        Label lblDescripcion;
        Button btnSubmit;
        Button btnUpdate;
        Button btnDelete;
        Button btnRefresh;

        Panel pnlRadioGroup;
        RadioButton radioButtonStoredProcedure;
        RadioButton radioButtonWebService;

        DataGridView dgvConsulta;

        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.numId = new System.Windows.Forms.NumericUpDown();
            this.lblId = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgvConsulta = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.radioButtonStoredProcedure = new System.Windows.Forms.RadioButton();
            radioButtonStoredProcedure.Select();
            this.radioButtonWebService = new System.Windows.Forms.RadioButton();
            this.pnlRadioGroup = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsulta)).BeginInit();
            this.pnlRadioGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // numId
            // 
            this.numId.Location = new System.Drawing.Point(651, 9);
            this.numId.Name = "numId";
            this.numId.Size = new System.Drawing.Size(120, 22);
            this.numId.TabIndex = 0;
            this.numId.Text = "";
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(545, 9);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(100, 23);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "ID";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(651, 37);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 22);
            this.txtNombre.TabIndex = 2;
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(545, 37);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(100, 23);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(651, 65);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(100, 22);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Location = new System.Drawing.Point(545, 65);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(100, 23);
            this.lblDescripcion.TabIndex = 5;
            this.lblDescripcion.Text = "Description";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(680, 113);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Agregar";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dgvConsulta
            // 
            this.dgvConsulta.ColumnHeadersHeight = 29;
            this.dgvConsulta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nombre,
            this.Descripcion});
            this.dgvConsulta.Location = new System.Drawing.Point(2, 0);
            this.dgvConsulta.Name = "dgvConsulta";
            this.dgvConsulta.RowHeadersWidth = 51;
            this.dgvConsulta.Size = new System.Drawing.Size(430, 451);
            this.dgvConsulta.TabIndex = 7;
            this.dgvConsulta.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsulta_CellClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "IdEmpleado";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Width = 125;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 125;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 125;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(438, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.PerformClick();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(518, 113);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(599, 113);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // radioButtonStoredProcedure
            // 
            this.radioButtonStoredProcedure.Location = new System.Drawing.Point(3, 73);
            this.radioButtonStoredProcedure.Name = "radioButtonStoredProcedure";
            this.radioButtonStoredProcedure.Size = new System.Drawing.Size(180, 24);
            this.radioButtonStoredProcedure.TabIndex = 0;
            this.radioButtonStoredProcedure.Text = "Usar Stored Procedure";
            this.radioButtonStoredProcedure.CheckedChanged += new System.EventHandler(this.radioButtonStoredProcedure_CheckedChanged);
            this.radioButtonStoredProcedure.Checked= true;
            // 
            // radioButtonWebService
            // 
            this.radioButtonWebService.Location = new System.Drawing.Point(3, 3);
            this.radioButtonWebService.Name = "radioButtonWebService";
            this.radioButtonWebService.Size = new System.Drawing.Size(177, 24);
            this.radioButtonWebService.TabIndex = 1;
            this.radioButtonWebService.Text = "Usar web service";
            this.radioButtonWebService.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonWebService.CheckedChanged += new System.EventHandler(this.radioButtonWebService_CheckedChanged);
            // 
            // pnlRadioGroup
            // 
            this.pnlRadioGroup.Controls.Add(this.radioButtonStoredProcedure);
            this.pnlRadioGroup.Controls.Add(this.radioButtonWebService);
            this.pnlRadioGroup.Location = new System.Drawing.Point(536, 197);
            this.pnlRadioGroup.Name = "pnlRadioGroup";
            this.pnlRadioGroup.Size = new System.Drawing.Size(183, 100);
            this.pnlRadioGroup.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgvConsulta);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlRadioGroup);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsulta)).EndInit();
            this.pnlRadioGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Descripcion;
    }
}

