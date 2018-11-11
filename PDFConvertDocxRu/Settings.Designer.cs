﻿namespace PDFConvertDocxRu
{
    partial class Settings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInFilesFolder = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSelectFolderInFiles = new System.Windows.Forms.Button();
            this.cmdSelectFolderOutFiles = new System.Windows.Forms.Button();
            this.checkBoxIsOpenOutFile = new System.Windows.Forms.CheckBox();
            this.cmdAssocToPdf = new System.Windows.Forms.Button();
            this.textBoxOutFilesFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 43);
            this.panel1.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(297, 9);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 26);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Location = new System.Drawing.Point(200, 9);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(74, 26);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Папка исходных файлов по умолчанию";
            // 
            // textBoxInFilesFolder
            // 
            this.textBoxInFilesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInFilesFolder.Location = new System.Drawing.Point(25, 47);
            this.textBoxInFilesFolder.Name = "textBoxInFilesFolder";
            this.textBoxInFilesFolder.Size = new System.Drawing.Size(334, 20);
            this.textBoxInFilesFolder.TabIndex = 2;
            // 
            // cmdSelectFolderInFiles
            // 
            this.cmdSelectFolderInFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectFolderInFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdSelectFolderInFiles.Image = global::PDFConvertDocxRu.Properties.Resources.icons8_more_24;
            this.cmdSelectFolderInFiles.Location = new System.Drawing.Point(335, 26);
            this.cmdSelectFolderInFiles.Name = "cmdSelectFolderInFiles";
            this.cmdSelectFolderInFiles.Size = new System.Drawing.Size(24, 19);
            this.cmdSelectFolderInFiles.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cmdSelectFolderInFiles, "select folder");
            this.cmdSelectFolderInFiles.UseVisualStyleBackColor = true;
            this.cmdSelectFolderInFiles.Click += new System.EventHandler(this.cmdSelectFolderInFiles_Click);
            // 
            // cmdSelectFolderOutFiles
            // 
            this.cmdSelectFolderOutFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectFolderOutFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdSelectFolderOutFiles.Image = global::PDFConvertDocxRu.Properties.Resources.icons8_more_24;
            this.cmdSelectFolderOutFiles.Location = new System.Drawing.Point(335, 82);
            this.cmdSelectFolderOutFiles.Name = "cmdSelectFolderOutFiles";
            this.cmdSelectFolderOutFiles.Size = new System.Drawing.Size(24, 19);
            this.cmdSelectFolderOutFiles.TabIndex = 8;
            this.toolTip1.SetToolTip(this.cmdSelectFolderOutFiles, "select folder");
            this.cmdSelectFolderOutFiles.UseVisualStyleBackColor = true;
            this.cmdSelectFolderOutFiles.Click += new System.EventHandler(this.cmdSelectFolderOutFiles_Click);
            // 
            // checkBoxIsOpenOutFile
            // 
            this.checkBoxIsOpenOutFile.AutoSize = true;
            this.checkBoxIsOpenOutFile.Location = new System.Drawing.Point(25, 142);
            this.checkBoxIsOpenOutFile.Name = "checkBoxIsOpenOutFile";
            this.checkBoxIsOpenOutFile.Size = new System.Drawing.Size(270, 17);
            this.checkBoxIsOpenOutFile.TabIndex = 4;
            this.checkBoxIsOpenOutFile.Text = "открывать ли docx-файл после преобразования";
            this.checkBoxIsOpenOutFile.UseVisualStyleBackColor = true;
            // 
            // cmdAssocToPdf
            // 
            this.cmdAssocToPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAssocToPdf.Location = new System.Drawing.Point(13, 183);
            this.cmdAssocToPdf.Name = "cmdAssocToPdf";
            this.cmdAssocToPdf.Size = new System.Drawing.Size(214, 26);
            this.cmdAssocToPdf.TabIndex = 5;
            this.cmdAssocToPdf.Text = "Привязать PDF к данной проге";
            this.cmdAssocToPdf.UseVisualStyleBackColor = true;
            this.cmdAssocToPdf.Click += new System.EventHandler(this.cmdAssocToPdf_Click);
            // 
            // textBoxOutFilesFolder
            // 
            this.textBoxOutFilesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutFilesFolder.Location = new System.Drawing.Point(25, 103);
            this.textBoxOutFilesFolder.Name = "textBoxOutFilesFolder";
            this.textBoxOutFilesFolder.Size = new System.Drawing.Size(334, 20);
            this.textBoxOutFilesFolder.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Папка результирующих файлов по умолчанию";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 277);
            this.Controls.Add(this.cmdSelectFolderOutFiles);
            this.Controls.Add(this.textBoxOutFilesFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdAssocToPdf);
            this.Controls.Add(this.checkBoxIsOpenOutFile);
            this.Controls.Add(this.cmdSelectFolderInFiles);
            this.Controls.Add(this.textBoxInFilesFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 315);
            this.Name = "Settings";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInFilesFolder;
        private System.Windows.Forms.Button cmdSelectFolderInFiles;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxIsOpenOutFile;
        private System.Windows.Forms.Button cmdAssocToPdf;
        private System.Windows.Forms.Button cmdSelectFolderOutFiles;
        private System.Windows.Forms.TextBox textBoxOutFilesFolder;
        private System.Windows.Forms.Label label2;
    }
}