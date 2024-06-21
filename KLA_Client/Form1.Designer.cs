namespace NumberConverterDesktop

{
    partial class Form1
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
            inputTextBox = new TextBox();
            convertButton = new Button();
            errorLabel = new Label();
            resultTextBox = new TextBox();
            SuspendLayout();
            // 
            // inputTextBox
            // 
            inputTextBox.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            inputTextBox.Location = new Point(31, 69);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.Size = new Size(275, 39);
            inputTextBox.TabIndex = 0;
            // 
            // convertButton
            // 
            convertButton.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            convertButton.Location = new Point(340, 69);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(147, 39);
            convertButton.TabIndex = 2;
            convertButton.Text = "Convert";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += convertButton_Click;
            // 
            // errorLabel
            // 
            errorLabel.AutoSize = true;
            errorLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(31, 136);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(0, 25);
            errorLabel.TabIndex = 3;
            // 
            // resultTextBox
            // 
            resultTextBox.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            resultTextBox.Location = new Point(31, 191);
            resultTextBox.Multiline = true;
            resultTextBox.Name = "resultTextBox";
            resultTextBox.ReadOnly = true;
            resultTextBox.Size = new Size(742, 135);
            resultTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resultTextBox);
            Controls.Add(errorLabel);
            Controls.Add(convertButton);
            Controls.Add(inputTextBox);
            Name = "Form1";
            Text = "Numbers to Words";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox inputTextBox;
        private Button convertButton;
        private Label errorLabel;
        private TextBox resultTextBox;
    }
}
