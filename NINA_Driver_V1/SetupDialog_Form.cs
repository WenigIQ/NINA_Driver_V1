using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NINA_Driver_V1
{
    [ComVisible(false)] //Darf nicht für COM sichtbar sein, da sonst der COM-Compiler einen Fehler wirft.

    //Dieser Code ist ein Windowsform (von KI generiert) und wird für die Einstellungen des Treibers verwendet. Da ich es nicht benötige,
    //kann man nicht wirklich etwas einstellen.
    public partial class SetupDialogForm : Form
    {
        public string SettingValue1 { get; set; }
        public string SettingValue2 { get; set; }

        public SetupDialogForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBoxSetting1 = new System.Windows.Forms.TextBox();
            this.textBoxSetting2 = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSetting1
            // 
            this.textBoxSetting1.Location = new System.Drawing.Point(12, 12);
            this.textBoxSetting1.Name = "textBoxSetting1";
            this.textBoxSetting1.Size = new System.Drawing.Size(260, 20);
            this.textBoxSetting1.TabIndex = 0;
            // 
            // textBoxSetting2
            // 
            this.textBoxSetting2.Location = new System.Drawing.Point(12, 38);
            this.textBoxSetting2.Name = "textBoxSetting2";
            this.textBoxSetting2.Size = new System.Drawing.Size(260, 20);
            this.textBoxSetting2.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(116, 64);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(197, 64);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SetupDialogForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxSetting2);
            this.Controls.Add(this.textBoxSetting1);
            this.Name = "SetupDialogForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.SettingValue1 = this.textBoxSetting1.Text;
            this.SettingValue2 = this.textBoxSetting2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private System.Windows.Forms.TextBox textBoxSetting1;
        private System.Windows.Forms.TextBox textBoxSetting2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}