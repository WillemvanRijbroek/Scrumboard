namespace ScrumBoard.UI.Forms
{
    partial class ScrumBoardForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrumBoardForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importSprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSprintToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportStoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.addStoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoAlignStoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.burndownChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSprintToolStripMenuItem,
            this.newSprintToolStripMenuItem,
            this.editSprintToolStripMenuItem,
            this.importSprintToolStripMenuItem1,
            this.exportStoriesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.addStoryToolStripMenuItem,
            this.autoAlignStoriesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuOptions,
            this.refreshToolStripMenuItem,
            this.autoRefreshToolStripMenuItem,
            this.burndownChartToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 258);
            // 
            // importSprintToolStripMenuItem
            // 
            this.importSprintToolStripMenuItem.Name = "importSprintToolStripMenuItem";
            this.importSprintToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.importSprintToolStripMenuItem.Text = "Open Sprint";
            this.importSprintToolStripMenuItem.Click += new System.EventHandler(this.openSprintToolStripMenuItem_Click);
            // 
            // newSprintToolStripMenuItem
            // 
            this.newSprintToolStripMenuItem.Name = "newSprintToolStripMenuItem";
            this.newSprintToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.newSprintToolStripMenuItem.Text = "New Sprint";
            this.newSprintToolStripMenuItem.Click += new System.EventHandler(this.newSprintToolStripMenuItem_Click);
            // 
            // editSprintToolStripMenuItem
            // 
            this.editSprintToolStripMenuItem.Name = "editSprintToolStripMenuItem";
            this.editSprintToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editSprintToolStripMenuItem.Text = "Edit Sprint";
            this.editSprintToolStripMenuItem.Click += new System.EventHandler(this.editSprintToolStripMenuItem_Click);
            // 
            // importSprintToolStripMenuItem1
            // 
            this.importSprintToolStripMenuItem1.Name = "importSprintToolStripMenuItem1";
            this.importSprintToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.importSprintToolStripMenuItem1.Text = "Import Stories";
            this.importSprintToolStripMenuItem1.Click += new System.EventHandler(this.importSprintToolStripMenuItem1_Click);
            // 
            // exportStoriesToolStripMenuItem
            // 
            this.exportStoriesToolStripMenuItem.Name = "exportStoriesToolStripMenuItem";
            this.exportStoriesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exportStoriesToolStripMenuItem.Text = "Export Stories";
            this.exportStoriesToolStripMenuItem.Click += new System.EventHandler(this.exportStoriesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 6);
            // 
            // addStoryToolStripMenuItem
            // 
            this.addStoryToolStripMenuItem.Name = "addStoryToolStripMenuItem";
            this.addStoryToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addStoryToolStripMenuItem.Text = "Add Story";
            this.addStoryToolStripMenuItem.Click += new System.EventHandler(this.addStoryToolStripMenuItem_Click);
            // 
            // autoAlignStoriesToolStripMenuItem
            // 
            this.autoAlignStoriesToolStripMenuItem.Name = "autoAlignStoriesToolStripMenuItem";
            this.autoAlignStoriesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.autoAlignStoriesToolStripMenuItem.Text = "Auto Align Stories";
            this.autoAlignStoriesToolStripMenuItem.Click += new System.EventHandler(this.autoAlignStoriesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
            // 
            // mnuOptions
            // 
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(169, 22);
            this.mnuOptions.Text = "Options";
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // autoRefreshToolStripMenuItem
            // 
            this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
            this.autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.autoRefreshToolStripMenuItem.Text = "Auto Refresh On";
            this.autoRefreshToolStripMenuItem.Visible = false;
            this.autoRefreshToolStripMenuItem.Click += new System.EventHandler(this.autoRefreshToolStripMenuItem_Click);
            // 
            // burndownChartToolStripMenuItem
            // 
            this.burndownChartToolStripMenuItem.Name = "burndownChartToolStripMenuItem";
            this.burndownChartToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.burndownChartToolStripMenuItem.Text = "Burndown chart";
            this.burndownChartToolStripMenuItem.Click += new System.EventHandler(this.burndownChartToolStripMenuItem_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // ScrumBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(555, 335);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ScrumBoardForm";
            this.Text = "Scrumboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ScrumBoard_Load);
            this.Shown += new System.EventHandler(this.ScrumBoardForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.ScrumBoardForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.ScrumBoard_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addStoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSprintToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.ToolStripMenuItem autoAlignStoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem importSprintToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem burndownChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportStoriesToolStripMenuItem;
    }
}

