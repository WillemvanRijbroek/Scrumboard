namespace ScrumBoard.UI.Forms
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numEstimate = new System.Windows.Forms.NumericUpDown();
            this.btnTodoBackColor = new System.Windows.Forms.Button();
            this.cmbStoryType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIssueTrackingURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEditDetails = new System.Windows.Forms.CheckBox();
            this.chkViewModus = new System.Windows.Forms.CheckBox();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelLayout = new System.Windows.Forms.Button();
            this.btnEditLayout = new System.Windows.Forms.Button();
            this.btnAddLayout = new System.Windows.Forms.Button();
            this.lvwLayouts = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemoveTeam = new System.Windows.Forms.Button();
            this.btnEditTeam = new System.Windows.Forms.Button();
            this.btnAddTeam = new System.Windows.Forms.Button();
            this.lvwTeams = new System.Windows.Forms.ListView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnEditStatus = new System.Windows.Forms.Button();
            this.btnNewStatus = new System.Windows.Forms.Button();
            this.lvwStates = new System.Windows.Forms.ListView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnEditStoryType = new System.Windows.Forms.Button();
            this.btnNewStoryType = new System.Windows.Forms.Button();
            this.lvwStoryTypes = new System.Windows.Forms.ListView();
            this.btnBoardBackColor = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEstimate)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numEstimate);
            this.groupBox1.Controls.Add(this.btnTodoBackColor);
            this.groupBox1.Controls.Add(this.cmbStoryType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtIssueTrackingURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 330);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 107);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Story default values";
            // 
            // numEstimate
            // 
            this.numEstimate.Location = new System.Drawing.Point(85, 21);
            this.numEstimate.Name = "numEstimate";
            this.numEstimate.Size = new System.Drawing.Size(151, 20);
            this.numEstimate.TabIndex = 21;
            // 
            // btnTodoBackColor
            // 
            this.btnTodoBackColor.Location = new System.Drawing.Point(242, 44);
            this.btnTodoBackColor.Name = "btnTodoBackColor";
            this.btnTodoBackColor.Size = new System.Drawing.Size(103, 23);
            this.btnTodoBackColor.TabIndex = 20;
            this.btnTodoBackColor.Text = "Todo back color";
            this.btnTodoBackColor.UseVisualStyleBackColor = true;
            this.btnTodoBackColor.Click += new System.EventHandler(this.btnTodoBackColor_Click);
            // 
            // cmbStoryType
            // 
            this.cmbStoryType.FormattingEnabled = true;
            this.cmbStoryType.Location = new System.Drawing.Point(85, 46);
            this.cmbStoryType.Name = "cmbStoryType";
            this.cmbStoryType.Size = new System.Drawing.Size(151, 21);
            this.cmbStoryType.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Default type:";
            // 
            // txtIssueTrackingURL
            // 
            this.txtIssueTrackingURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssueTrackingURL.Location = new System.Drawing.Point(85, 73);
            this.txtIssueTrackingURL.Name = "txtIssueTrackingURL";
            this.txtIssueTrackingURL.Size = new System.Drawing.Size(602, 20);
            this.txtIssueTrackingURL.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "External URL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Default estimate:";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(242, 18);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(103, 23);
            this.btnColor.TabIndex = 7;
            this.btnColor.Text = "Story back color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(630, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(549, 490);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnBoardBackColor);
            this.groupBox2.Controls.Add(this.chkEditDetails);
            this.groupBox2.Controls.Add(this.chkViewModus);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 54);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // chkEditDetails
            // 
            this.chkEditDetails.AutoSize = true;
            this.chkEditDetails.Location = new System.Drawing.Point(97, 19);
            this.chkEditDetails.Name = "chkEditDetails";
            this.chkEditDetails.Size = new System.Drawing.Size(162, 17);
            this.chkEditDetails.TabIndex = 1;
            this.chkEditDetails.Text = "Edit details on status change";
            this.chkEditDetails.UseVisualStyleBackColor = true;
            // 
            // chkViewModus
            // 
            this.chkViewModus.AutoSize = true;
            this.chkViewModus.Checked = true;
            this.chkViewModus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewModus.Location = new System.Drawing.Point(12, 19);
            this.chkViewModus.Name = "chkViewModus";
            this.chkViewModus.Size = new System.Drawing.Size(79, 17);
            this.chkViewModus.TabIndex = 0;
            this.chkViewModus.Text = "View Mode";
            this.chkViewModus.UseVisualStyleBackColor = true;
            // 
            // cmbTeam
            // 
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(85, 19);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(151, 21);
            this.cmbTeam.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Default team:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelLayout);
            this.groupBox3.Controls.Add(this.btnEditLayout);
            this.groupBox3.Controls.Add(this.btnAddLayout);
            this.groupBox3.Controls.Add(this.lvwLayouts);
            this.groupBox3.Location = new System.Drawing.Point(13, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(322, 114);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Layout";
            // 
            // btnDelLayout
            // 
            this.btnDelLayout.Location = new System.Drawing.Point(241, 78);
            this.btnDelLayout.Name = "btnDelLayout";
            this.btnDelLayout.Size = new System.Drawing.Size(75, 23);
            this.btnDelLayout.TabIndex = 5;
            this.btnDelLayout.Text = "Delete";
            this.btnDelLayout.UseVisualStyleBackColor = true;
            this.btnDelLayout.Visible = false;
            this.btnDelLayout.Click += new System.EventHandler(this.btnDelLayout_Click);
            // 
            // btnEditLayout
            // 
            this.btnEditLayout.Location = new System.Drawing.Point(241, 49);
            this.btnEditLayout.Name = "btnEditLayout";
            this.btnEditLayout.Size = new System.Drawing.Size(75, 23);
            this.btnEditLayout.TabIndex = 4;
            this.btnEditLayout.Text = "Edit";
            this.btnEditLayout.UseVisualStyleBackColor = true;
            this.btnEditLayout.Click += new System.EventHandler(this.btnEditLayout_Click);
            // 
            // btnAddLayout
            // 
            this.btnAddLayout.Location = new System.Drawing.Point(241, 20);
            this.btnAddLayout.Name = "btnAddLayout";
            this.btnAddLayout.Size = new System.Drawing.Size(75, 23);
            this.btnAddLayout.TabIndex = 3;
            this.btnAddLayout.Text = "New";
            this.btnAddLayout.UseVisualStyleBackColor = true;
            this.btnAddLayout.Click += new System.EventHandler(this.btnAddLayout_Click);
            // 
            // lvwLayouts
            // 
            this.lvwLayouts.Location = new System.Drawing.Point(7, 20);
            this.lvwLayouts.Name = "lvwLayouts";
            this.lvwLayouts.Size = new System.Drawing.Size(228, 88);
            this.lvwLayouts.TabIndex = 2;
            this.lvwLayouts.UseCompatibleStateImageBehavior = false;
            this.lvwLayouts.View = System.Windows.Forms.View.List;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbTeam);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnRemoveTeam);
            this.groupBox4.Controls.Add(this.btnEditTeam);
            this.groupBox4.Controls.Add(this.btnAddTeam);
            this.groupBox4.Controls.Add(this.lvwTeams);
            this.groupBox4.Location = new System.Drawing.Point(12, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(323, 135);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Team";
            // 
            // btnRemoveTeam
            // 
            this.btnRemoveTeam.Location = new System.Drawing.Point(242, 106);
            this.btnRemoveTeam.Name = "btnRemoveTeam";
            this.btnRemoveTeam.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveTeam.TabIndex = 5;
            this.btnRemoveTeam.Text = "Delete";
            this.btnRemoveTeam.UseVisualStyleBackColor = true;
            this.btnRemoveTeam.Click += new System.EventHandler(this.btnRemoveTeam_Click);
            // 
            // btnEditTeam
            // 
            this.btnEditTeam.Location = new System.Drawing.Point(242, 77);
            this.btnEditTeam.Name = "btnEditTeam";
            this.btnEditTeam.Size = new System.Drawing.Size(75, 23);
            this.btnEditTeam.TabIndex = 4;
            this.btnEditTeam.Text = "Edit";
            this.btnEditTeam.UseVisualStyleBackColor = true;
            this.btnEditTeam.Click += new System.EventHandler(this.btnEditTeam_Click);
            // 
            // btnAddTeam
            // 
            this.btnAddTeam.Location = new System.Drawing.Point(242, 48);
            this.btnAddTeam.Name = "btnAddTeam";
            this.btnAddTeam.Size = new System.Drawing.Size(75, 23);
            this.btnAddTeam.TabIndex = 3;
            this.btnAddTeam.Text = "New";
            this.btnAddTeam.UseVisualStyleBackColor = true;
            this.btnAddTeam.Click += new System.EventHandler(this.btnAddTeam_Click);
            // 
            // lvwTeams
            // 
            this.lvwTeams.Location = new System.Drawing.Point(6, 41);
            this.lvwTeams.Name = "lvwTeams";
            this.lvwTeams.Size = new System.Drawing.Size(230, 88);
            this.lvwTeams.TabIndex = 2;
            this.lvwTeams.UseCompatibleStateImageBehavior = false;
            this.lvwTeams.View = System.Windows.Forms.View.List;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnEditStatus);
            this.groupBox5.Controls.Add(this.btnNewStatus);
            this.groupBox5.Controls.Add(this.lvwStates);
            this.groupBox5.Location = new System.Drawing.Point(341, 73);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(322, 114);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status";
            // 
            // btnEditStatus
            // 
            this.btnEditStatus.Location = new System.Drawing.Point(241, 49);
            this.btnEditStatus.Name = "btnEditStatus";
            this.btnEditStatus.Size = new System.Drawing.Size(75, 23);
            this.btnEditStatus.TabIndex = 4;
            this.btnEditStatus.Text = "Edit";
            this.btnEditStatus.UseVisualStyleBackColor = true;
            this.btnEditStatus.Click += new System.EventHandler(this.btnEditStatus_Click);
            // 
            // btnNewStatus
            // 
            this.btnNewStatus.Location = new System.Drawing.Point(241, 20);
            this.btnNewStatus.Name = "btnNewStatus";
            this.btnNewStatus.Size = new System.Drawing.Size(75, 23);
            this.btnNewStatus.TabIndex = 3;
            this.btnNewStatus.Text = "New";
            this.btnNewStatus.UseVisualStyleBackColor = true;
            this.btnNewStatus.Click += new System.EventHandler(this.btnNewStatus_Click);
            // 
            // lvwStates
            // 
            this.lvwStates.Location = new System.Drawing.Point(7, 20);
            this.lvwStates.Name = "lvwStates";
            this.lvwStates.Size = new System.Drawing.Size(228, 88);
            this.lvwStates.TabIndex = 2;
            this.lvwStates.UseCompatibleStateImageBehavior = false;
            this.lvwStates.View = System.Windows.Forms.View.List;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnEditStoryType);
            this.groupBox6.Controls.Add(this.btnNewStoryType);
            this.groupBox6.Controls.Add(this.lvwStoryTypes);
            this.groupBox6.Location = new System.Drawing.Point(341, 193);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(322, 131);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Story type";
            // 
            // btnEditStoryType
            // 
            this.btnEditStoryType.Location = new System.Drawing.Point(241, 49);
            this.btnEditStoryType.Name = "btnEditStoryType";
            this.btnEditStoryType.Size = new System.Drawing.Size(75, 23);
            this.btnEditStoryType.TabIndex = 4;
            this.btnEditStoryType.Text = "Edit";
            this.btnEditStoryType.UseVisualStyleBackColor = true;
            this.btnEditStoryType.Click += new System.EventHandler(this.btnEditStoryType_Click);
            // 
            // btnNewStoryType
            // 
            this.btnNewStoryType.Location = new System.Drawing.Point(241, 20);
            this.btnNewStoryType.Name = "btnNewStoryType";
            this.btnNewStoryType.Size = new System.Drawing.Size(75, 23);
            this.btnNewStoryType.TabIndex = 3;
            this.btnNewStoryType.Text = "New";
            this.btnNewStoryType.UseVisualStyleBackColor = true;
            this.btnNewStoryType.Click += new System.EventHandler(this.btnNewStoryType_Click);
            // 
            // lvwStoryTypes
            // 
            this.lvwStoryTypes.Location = new System.Drawing.Point(7, 20);
            this.lvwStoryTypes.Name = "lvwStoryTypes";
            this.lvwStoryTypes.Size = new System.Drawing.Size(228, 105);
            this.lvwStoryTypes.TabIndex = 2;
            this.lvwStoryTypes.UseCompatibleStateImageBehavior = false;
            this.lvwStoryTypes.View = System.Windows.Forms.View.List;
            // 
            // btnBackColor
            // 
            this.btnBoardBackColor.Location = new System.Drawing.Point(295, 16);
            this.btnBoardBackColor.Name = "btnBackColor";
            this.btnBoardBackColor.Size = new System.Drawing.Size(103, 23);
            this.btnBoardBackColor.TabIndex = 8;
            this.btnBoardBackColor.Text = "Back color";
            this.btnBoardBackColor.UseVisualStyleBackColor = true;
            this.btnBoardBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // Options
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(717, 525);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEstimate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIssueTrackingURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkViewModus;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cmbStoryType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkEditDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDelLayout;
        private System.Windows.Forms.Button btnEditLayout;
        private System.Windows.Forms.Button btnAddLayout;
        private System.Windows.Forms.ListView lvwLayouts;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRemoveTeam;
        private System.Windows.Forms.Button btnEditTeam;
        private System.Windows.Forms.Button btnAddTeam;
        private System.Windows.Forms.ListView lvwTeams;
        private System.Windows.Forms.Button btnTodoBackColor;
        private System.Windows.Forms.ComboBox cmbTeam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numEstimate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnEditStatus;
        private System.Windows.Forms.Button btnNewStatus;
        private System.Windows.Forms.ListView lvwStates;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnEditStoryType;
        private System.Windows.Forms.Button btnNewStoryType;
        private System.Windows.Forms.ListView lvwStoryTypes;
        private System.Windows.Forms.Button btnBoardBackColor;

    }
}