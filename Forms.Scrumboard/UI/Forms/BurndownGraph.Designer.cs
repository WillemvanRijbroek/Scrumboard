using ScrumBoard.UI.Controls;
namespace ScrumBoard.UI.Forms
{
    partial class BurndownGraph
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
            this.burndown1 = new ScrumBoard.UI.Controls.Burndown();
            this.SuspendLayout();
            // 
            // burndown1
            // 
            this.burndown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.burndown1.Location = new System.Drawing.Point(0, 0);
            this.burndown1.Name = "burndown1";
            this.burndown1.Size = new System.Drawing.Size(777, 528);
            this.burndown1.TabIndex = 0;
            // 
            // BurndownGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 528);
            this.Controls.Add(this.burndown1);
            this.Name = "BurndownGraph";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Burndown";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Burndown burndown1;
    }
}