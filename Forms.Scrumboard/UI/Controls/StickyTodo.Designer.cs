namespace ScrumBoard.UI.Controls
{
    partial class StickyTodo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemoveStory = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtEstimate = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuRemoveStory});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 76);
            this.contextMenuStrip1.Text = "&Open";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuRemoveStory
            // 
            this.mnuRemoveStory.Name = "mnuRemoveStory";
            this.mnuRemoveStory.Size = new System.Drawing.Size(152, 22);
            this.mnuRemoveStory.Text = "Remove";
            this.mnuRemoveStory.Click += new System.EventHandler(this.mnuRemoveStory_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.CausesValidation = false;
            this.txtDescription.ContextMenuStrip = this.contextMenuStrip1;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(4, 22);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(109, 83);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TabStop = false;
            this.txtDescription.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtDescription_MouseDoubleClick);
            this.txtDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtDescription_MouseDown);
            this.txtDescription.MouseHover += new System.EventHandler(this.txtDescription_MouseHover);
            this.txtDescription.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtDescription_MouseMove);
            this.txtDescription.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtDescription_MouseUp);
            // 
            // txtEstimate
            // 
            this.txtEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstimate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtEstimate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEstimate.ContextMenuStrip = this.contextMenuStrip1;
            this.txtEstimate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstimate.Location = new System.Drawing.Point(79, 6);
            this.txtEstimate.Name = "txtEstimate";
            this.txtEstimate.Size = new System.Drawing.Size(34, 16);
            this.txtEstimate.TabIndex = 3;
            this.txtEstimate.TabStop = false;
            this.txtEstimate.Text = "8";
            this.txtEstimate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEstimate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtEstimate_MouseDown);
            this.txtEstimate.MouseHover += new System.EventHandler(this.txtEstimate_MouseHover);
            this.txtEstimate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtEstimate_MouseMove);
            this.txtEstimate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtEstimate_MouseUp);
            // 
            // StickyTodo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtEstimate);
            this.Name = "StickyTodo";
            this.Size = new System.Drawing.Size(119, 108);
            this.MouseLeave += new System.EventHandler(this.StickyTodo_MouseLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtEstimate;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveStory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

    }
}
