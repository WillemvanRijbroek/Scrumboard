namespace ScrumBoard.UI.Controls
{
    partial class StickyNote
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
            this.SuspendLayout();
            // 
            // StickyNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "StickyNote";
            this.Size = new System.Drawing.Size(119, 108);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StickyNote_MouseDown);
            this.MouseHover += new System.EventHandler(this.StickyNote_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StickyNote_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StickyNote_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion


    }
}
