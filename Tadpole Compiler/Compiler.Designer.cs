namespace Tadpole_Compiler
{
    partial class Compiler
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
            this.compileButton = new System.Windows.Forms.Button();
            this.chooseFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // compileButton
            // 
            this.compileButton.Font = new System.Drawing.Font("Open Sans SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compileButton.Location = new System.Drawing.Point(24, 16);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(136, 48);
            this.compileButton.TabIndex = 0;
            this.compileButton.Text = "Compile Code";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileCodeClick);
            // 
            // chooseFileDialog
            // 
            this.chooseFileDialog.FileName = "chooseFileDialog";
            this.chooseFileDialog.Title = "Choose Tadpole Project";
            // 
            // Compiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 89);
            this.Controls.Add(this.compileButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 128);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 128);
            this.Name = "Compiler";
            this.Text = "Compiler";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.OpenFileDialog chooseFileDialog;
    }
}

