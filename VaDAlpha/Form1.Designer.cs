namespace VaDAlpha
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelVid = new System.Windows.Forms.Panel();
            this.treeViewVid = new System.Windows.Forms.TreeView();
            this.panelTxt = new System.Windows.Forms.Panel();
            this.treeViewTxt = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxVid = new System.Windows.Forms.TextBox();
            this.textBoxTxt = new System.Windows.Forms.TextBox();
            this.buttonVidSelectPath = new System.Windows.Forms.Button();
            this.buttonVidLoadPath = new System.Windows.Forms.Button();
            this.buttonTxtLoadPath = new System.Windows.Forms.Button();
            this.buttonTxtSelectPath = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelVid.SuspendLayout();
            this.panelTxt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelVid
            // 
            this.panelVid.Controls.Add(this.treeViewVid);
            this.panelVid.Location = new System.Drawing.Point(12, 35);
            this.panelVid.Name = "panelVid";
            this.panelVid.Size = new System.Drawing.Size(572, 267);
            this.panelVid.TabIndex = 0;
            // 
            // treeViewVid
            // 
            this.treeViewVid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewVid.Location = new System.Drawing.Point(0, 0);
            this.treeViewVid.Name = "treeViewVid";
            this.treeViewVid.Size = new System.Drawing.Size(572, 267);
            this.treeViewVid.TabIndex = 0;
            this.treeViewVid.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewVid_NodeMouseClick);
            // 
            // panelTxt
            // 
            this.panelTxt.Controls.Add(this.treeViewTxt);
            this.panelTxt.Location = new System.Drawing.Point(595, 34);
            this.panelTxt.Name = "panelTxt";
            this.panelTxt.Size = new System.Drawing.Size(572, 268);
            this.panelTxt.TabIndex = 1;
            // 
            // treeViewTxt
            // 
            this.treeViewTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTxt.Location = new System.Drawing.Point(0, 0);
            this.treeViewTxt.Name = "treeViewTxt";
            this.treeViewTxt.Size = new System.Drawing.Size(572, 268);
            this.treeViewTxt.TabIndex = 0;
            this.treeViewTxt.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTxt_AfterSelect);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(596, 308);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(572, 321);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // textBoxVid
            // 
            this.textBoxVid.Location = new System.Drawing.Point(13, 13);
            this.textBoxVid.Name = "textBoxVid";
            this.textBoxVid.Size = new System.Drawing.Size(342, 20);
            this.textBoxVid.TabIndex = 4;
            // 
            // textBoxTxt
            // 
            this.textBoxTxt.Location = new System.Drawing.Point(596, 8);
            this.textBoxTxt.Name = "textBoxTxt";
            this.textBoxTxt.Size = new System.Drawing.Size(342, 20);
            this.textBoxTxt.TabIndex = 5;
            // 
            // buttonVidSelectPath
            // 
            this.buttonVidSelectPath.Location = new System.Drawing.Point(361, 10);
            this.buttonVidSelectPath.Name = "buttonVidSelectPath";
            this.buttonVidSelectPath.Size = new System.Drawing.Size(115, 23);
            this.buttonVidSelectPath.TabIndex = 6;
            this.buttonVidSelectPath.Text = "Выберите раздел";
            this.buttonVidSelectPath.UseVisualStyleBackColor = true;
            this.buttonVidSelectPath.Click += new System.EventHandler(this.buttonVidSelectPath_Click);
            // 
            // buttonVidLoadPath
            // 
            this.buttonVidLoadPath.Location = new System.Drawing.Point(482, 10);
            this.buttonVidLoadPath.Name = "buttonVidLoadPath";
            this.buttonVidLoadPath.Size = new System.Drawing.Size(102, 23);
            this.buttonVidLoadPath.TabIndex = 7;
            this.buttonVidLoadPath.Text = "Загрузить";
            this.buttonVidLoadPath.UseVisualStyleBackColor = true;
            this.buttonVidLoadPath.Click += new System.EventHandler(this.buttonVidLoadPath_Click);
            // 
            // buttonTxtLoadPath
            // 
            this.buttonTxtLoadPath.Location = new System.Drawing.Point(1065, 6);
            this.buttonTxtLoadPath.Name = "buttonTxtLoadPath";
            this.buttonTxtLoadPath.Size = new System.Drawing.Size(102, 23);
            this.buttonTxtLoadPath.TabIndex = 9;
            this.buttonTxtLoadPath.Text = "Загрузить";
            this.buttonTxtLoadPath.UseVisualStyleBackColor = true;
            this.buttonTxtLoadPath.Click += new System.EventHandler(this.buttonTxtLoadPath_Click);
            // 
            // buttonTxtSelectPath
            // 
            this.buttonTxtSelectPath.Location = new System.Drawing.Point(944, 6);
            this.buttonTxtSelectPath.Name = "buttonTxtSelectPath";
            this.buttonTxtSelectPath.Size = new System.Drawing.Size(115, 23);
            this.buttonTxtSelectPath.TabIndex = 8;
            this.buttonTxtSelectPath.Text = "Выберите раздел";
            this.buttonTxtSelectPath.UseVisualStyleBackColor = true;
            this.buttonTxtSelectPath.Click += new System.EventHandler(this.buttonTxtSelectPath_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 308);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(572, 321);
            this.axWindowsMediaPlayer1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 670);
            this.Controls.Add(this.buttonTxtLoadPath);
            this.Controls.Add(this.buttonTxtSelectPath);
            this.Controls.Add(this.buttonVidLoadPath);
            this.Controls.Add(this.buttonVidSelectPath);
            this.Controls.Add(this.textBoxTxt);
            this.Controls.Add(this.textBoxVid);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.panelTxt);
            this.Controls.Add(this.panelVid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelVid.ResumeLayout(false);
            this.panelTxt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelVid;
        private System.Windows.Forms.TreeView treeViewVid;
        private System.Windows.Forms.Panel panelTxt;
        private System.Windows.Forms.TreeView treeViewTxt;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxVid;
        private System.Windows.Forms.TextBox textBoxTxt;
        private System.Windows.Forms.Button buttonVidSelectPath;
        private System.Windows.Forms.Button buttonVidLoadPath;
        private System.Windows.Forms.Button buttonTxtLoadPath;
        private System.Windows.Forms.Button buttonTxtSelectPath;
    }
}

