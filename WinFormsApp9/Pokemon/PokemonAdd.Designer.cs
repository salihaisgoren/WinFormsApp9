namespace WinFormsApp9.Pokemon
{
    partial class PokemonAdd
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
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmbOwner = new ComboBox();
            cmbCategory = new ComboBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(99, 298);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 20;
            label4.Text = "Owner";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(99, 246);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 19;
            label3.Text = "Kategori";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(99, 200);
            label2.Name = "label2";
            label2.Size = new Size(98, 20);
            label2.TabIndex = 18;
            label2.Text = "Doğum Tarihi";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(99, 164);
            label1.Name = "label1";
            label1.Size = new Size(28, 20);
            label1.TabIndex = 17;
            label1.Text = "Ad";
            // 
            // cmbOwner
            // 
            cmbOwner.FormattingEnabled = true;
            cmbOwner.Location = new Point(229, 290);
            cmbOwner.Name = "cmbOwner";
            cmbOwner.Size = new Size(206, 28);
            cmbOwner.TabIndex = 16;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(229, 243);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(206, 28);
            cmbCategory.TabIndex = 15;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(229, 197);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(206, 27);
            textBox2.TabIndex = 14;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(229, 164);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(206, 27);
            textBox1.TabIndex = 13;
            // 
            // button2
            // 
            button2.Location = new Point(103, 74);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 12;
            button2.Text = "Back";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(341, 342);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 11;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PokemonAdd
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 445);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbOwner);
            Controls.Add(cmbCategory);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "PokemonAdd";
            Text = "PokemonAdd";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox cmbOwner;
        private ComboBox cmbCategory;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
    }
}