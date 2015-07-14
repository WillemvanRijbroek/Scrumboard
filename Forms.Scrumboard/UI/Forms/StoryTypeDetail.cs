using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.ScrumboardService;
using ScrumBoard.Common;

namespace ScrumBoard.UI.Forms
{
    public partial class StoryTypeDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        private TextBox txtName;
        private Label label4;
        private Button btnCancel;
        private Button btnOk;
        private CheckBox chkBurndownEnabled;
        StoryType s;

        public StoryTypeDetail()
        {
            InitializeComponent();

        }

        public StoryType StoryType
        {
            set
            {
                s = value;
                if (s != null)
                {
                    txtName.Text = value.Name;
                    chkBurndownEnabled.Checked = value.BurnDownEnabled;
                }
            }
            get
            {
                return s;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (s.Id > -1)
            {
                client.StoryTypeUpdate(s.Id, s.Name, -256, chkBurndownEnabled.Checked);
            }
            else
            {
                client.StoryTypeInsert(s.Name, -256, chkBurndownEnabled.Checked);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkBurndownEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(64, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(371, 20);
            this.txtName.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(360, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(279, 62);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkBurndownEnabled
            // 
            this.chkBurndownEnabled.AutoSize = true;
            this.chkBurndownEnabled.Location = new System.Drawing.Point(64, 39);
            this.chkBurndownEnabled.Name = "chkBurndownEnabled";
            this.chkBurndownEnabled.Size = new System.Drawing.Size(212, 17);
            this.chkBurndownEnabled.TabIndex = 15;
            this.chkBurndownEnabled.Text = "Show these stories in a burndown chart";
            this.chkBurndownEnabled.UseVisualStyleBackColor = true;
            // 
            // StoryTypeDetail
            // 
            this.ClientSize = new System.Drawing.Size(442, 98);
            this.Controls.Add(this.chkBurndownEnabled);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "StoryTypeDetail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {

        }

    }
}
