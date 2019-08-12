namespace TestComponent_cs
{
    partial class MainForm
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
            this.pane = new System.Windows.Forms.TableLayoutPanel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelReplay = new System.Windows.Forms.Label();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.playerBox = new System.Windows.Forms.TextBox();
            this.pause = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.Button();
            this.browserBox = new System.Windows.Forms.TextBox();
            this.labelPlay = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.browserPane = new System.Windows.Forms.Panel();
            this.subtitle = new System.Windows.Forms.Label();
            this.fillform = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.pane.SuspendLayout();
            this.SuspendLayout();
            // 
            // pane
            // 
            this.pane.ColumnCount = 3;
            this.pane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.pane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.pane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pane.Controls.Add(this.labelStatus, 1, 2);
            this.pane.Controls.Add(this.labelReplay, 1, 1);
            this.pane.Controls.Add(this.errorBox, 2, 2);
            this.pane.Controls.Add(this.playerBox, 2, 1);
            this.pane.Controls.Add(this.pause, 0, 2);
            this.pane.Controls.Add(this.stop, 0, 1);
            this.pane.Controls.Add(this.play, 0, 0);
            this.pane.Controls.Add(this.browserBox, 2, 0);
            this.pane.Controls.Add(this.labelPlay, 1, 0);
            this.pane.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pane.Location = new System.Drawing.Point(0, 519);
            this.pane.Name = "pane";
            this.pane.RowCount = 3;
            this.pane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pane.Size = new System.Drawing.Size(935, 110);
            this.pane.TabIndex = 0;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(103, 72);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(40, 38);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "Status:";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelReplay
            // 
            this.labelReplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReplay.AutoSize = true;
            this.labelReplay.Location = new System.Drawing.Point(103, 36);
            this.labelReplay.Name = "labelReplay";
            this.labelReplay.Size = new System.Drawing.Size(57, 36);
            this.labelReplay.TabIndex = 8;
            this.labelReplay.Text = "Replaying:";
            this.labelReplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorBox
            // 
            this.errorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorBox.Location = new System.Drawing.Point(253, 75);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(679, 32);
            this.errorBox.TabIndex = 6;
            // 
            // playerBox
            // 
            this.playerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerBox.Location = new System.Drawing.Point(253, 39);
            this.playerBox.Multiline = true;
            this.playerBox.Name = "playerBox";
            this.playerBox.ReadOnly = true;
            this.playerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.playerBox.Size = new System.Drawing.Size(679, 30);
            this.playerBox.TabIndex = 5;
            // 
            // pause
            // 
            this.pause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pause.Location = new System.Drawing.Point(3, 75);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(94, 32);
            this.pause.TabIndex = 3;
            this.pause.Text = "Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // stop
            // 
            this.stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stop.Location = new System.Drawing.Point(3, 39);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(94, 30);
            this.stop.TabIndex = 2;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // play
            // 
            this.play.Dock = System.Windows.Forms.DockStyle.Fill;
            this.play.Location = new System.Drawing.Point(3, 3);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(94, 30);
            this.play.TabIndex = 1;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // browserBox
            // 
            this.browserBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserBox.Location = new System.Drawing.Point(253, 3);
            this.browserBox.Multiline = true;
            this.browserBox.Name = "browserBox";
            this.browserBox.ReadOnly = true;
            this.browserBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.browserBox.Size = new System.Drawing.Size(679, 30);
            this.browserBox.TabIndex = 4;
            // 
            // labelPlay
            // 
            this.labelPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPlay.AutoSize = true;
            this.labelPlay.Location = new System.Drawing.Point(103, 0);
            this.labelPlay.Name = "labelPlay";
            this.labelPlay.Size = new System.Drawing.Size(143, 36);
            this.labelPlay.TabIndex = 7;
            this.labelPlay.Text = "Click Play to load macro from file";
            this.labelPlay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "iim";
            // 
            // browserPane
            // 
            this.browserPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browserPane.Location = new System.Drawing.Point(3, 25);
            this.browserPane.Name = "browserPane";
            this.browserPane.Size = new System.Drawing.Size(932, 441);
            this.browserPane.TabIndex = 1;
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Location = new System.Drawing.Point(0, 6);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(379, 13);
            this.subtitle.TabIndex = 2;
            this.subtitle.Text = "This .NET application uses the iMacros Component to automate web browsing.";
            // 
            // fillform
            // 
            this.fillform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fillform.Location = new System.Drawing.Point(3, 478);
            this.fillform.Name = "fillform";
            this.fillform.Size = new System.Drawing.Size(192, 30);
            this.fillform.TabIndex = 3;
            this.fillform.Text = "Run embedded FillForm macro";
            this.fillform.UseVisualStyleBackColor = true;
            this.fillform.Click += new System.EventHandler(this.fillform_Click);
            // 
            // search
            // 
            this.search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.search.Location = new System.Drawing.Point(201, 478);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(192, 30);
            this.search.TabIndex = 4;
            this.search.Text = "Search iMacros wiki page";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 629);
            this.Controls.Add(this.search);
            this.Controls.Add(this.fillform);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.browserPane);
            this.Controls.Add(this.pane);
            this.Name = "MainForm";
            this.Text = "iMacros Component Test Application";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.pane.ResumeLayout(false);
            this.pane.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pane;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.TextBox playerBox;
        private System.Windows.Forms.TextBox browserBox;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Panel browserPane;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelReplay;
        private System.Windows.Forms.Label labelPlay;
        private System.Windows.Forms.Button fillform;
        private System.Windows.Forms.Button search;
    }
}

