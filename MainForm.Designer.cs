
namespace DazToUnrealOnlineInstaller
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InstructionLabel = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.TempDirectoryLabel = new System.Windows.Forms.Label();
            this.UnrealVersionComboBox = new System.Windows.Forms.ComboBox();
            this.DazStudioFolderLabel = new System.Windows.Forms.Label();
            this.UnrealEditorFolderLabel = new System.Windows.Forms.Label();
            this.BackupFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BackupFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.ChooseBackupFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InstructionLabel.Location = new System.Drawing.Point(12, 9);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(682, 19);
            this.InstructionLabel.TabIndex = 0;
            this.InstructionLabel.Text = "Click next to download DazToUnreal from https://github.com/David-Vodhanel/DazToUn" +
    "real/releases";
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(716, 189);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NextButton_MouseClick);
            // 
            // TempDirectoryLabel
            // 
            this.TempDirectoryLabel.AutoSize = true;
            this.TempDirectoryLabel.Location = new System.Drawing.Point(12, 34);
            this.TempDirectoryLabel.Name = "TempDirectoryLabel";
            this.TempDirectoryLabel.Size = new System.Drawing.Size(0, 15);
            this.TempDirectoryLabel.TabIndex = 2;
            // 
            // UnrealVersionComboBox
            // 
            this.UnrealVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnrealVersionComboBox.FormattingEnabled = true;
            this.UnrealVersionComboBox.Location = new System.Drawing.Point(12, 52);
            this.UnrealVersionComboBox.Name = "UnrealVersionComboBox";
            this.UnrealVersionComboBox.Size = new System.Drawing.Size(121, 23);
            this.UnrealVersionComboBox.TabIndex = 3;
            this.UnrealVersionComboBox.Visible = false;
            // 
            // DazStudioFolderLabel
            // 
            this.DazStudioFolderLabel.AutoSize = true;
            this.DazStudioFolderLabel.Location = new System.Drawing.Point(13, 82);
            this.DazStudioFolderLabel.Name = "DazStudioFolderLabel";
            this.DazStudioFolderLabel.Size = new System.Drawing.Size(0, 15);
            this.DazStudioFolderLabel.TabIndex = 4;
            // 
            // UnrealEditorFolderLabel
            // 
            this.UnrealEditorFolderLabel.AutoSize = true;
            this.UnrealEditorFolderLabel.Location = new System.Drawing.Point(13, 104);
            this.UnrealEditorFolderLabel.Name = "UnrealEditorFolderLabel";
            this.UnrealEditorFolderLabel.Size = new System.Drawing.Size(0, 15);
            this.UnrealEditorFolderLabel.TabIndex = 5;
            // 
            // BackupFolderPathTextBox
            // 
            this.BackupFolderPathTextBox.Location = new System.Drawing.Point(12, 123);
            this.BackupFolderPathTextBox.Name = "BackupFolderPathTextBox";
            this.BackupFolderPathTextBox.Size = new System.Drawing.Size(536, 23);
            this.BackupFolderPathTextBox.TabIndex = 6;
            this.BackupFolderPathTextBox.Visible = false;
            // 
            // ChooseBackupFolderButton
            // 
            this.ChooseBackupFolderButton.Location = new System.Drawing.Point(555, 123);
            this.ChooseBackupFolderButton.Name = "ChooseBackupFolderButton";
            this.ChooseBackupFolderButton.Size = new System.Drawing.Size(33, 23);
            this.ChooseBackupFolderButton.TabIndex = 7;
            this.ChooseBackupFolderButton.Text = "...";
            this.ChooseBackupFolderButton.UseVisualStyleBackColor = true;
            this.ChooseBackupFolderButton.Visible = false;
            this.ChooseBackupFolderButton.Click += new System.EventHandler(this.ChooseBackupFolderButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 219);
            this.Controls.Add(this.ChooseBackupFolderButton);
            this.Controls.Add(this.BackupFolderPathTextBox);
            this.Controls.Add(this.UnrealEditorFolderLabel);
            this.Controls.Add(this.DazStudioFolderLabel);
            this.Controls.Add(this.UnrealVersionComboBox);
            this.Controls.Add(this.TempDirectoryLabel);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.InstructionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Daz To Unreal Online Installer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InstructionLabel;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label TempDirectoryLabel;
        private System.Windows.Forms.ComboBox UnrealVersionComboBox;
        private System.Windows.Forms.Label DazStudioFolderLabel;
        private System.Windows.Forms.Label UnrealEditorFolderLabel;
        private System.Windows.Forms.FolderBrowserDialog BackupFolderBrowserDialog;
        private System.Windows.Forms.TextBox BackupFolderPathTextBox;
        private System.Windows.Forms.Button ChooseBackupFolderButton;
    }
}

