namespace OTTER
{
    public partial class Menu
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
            this.Pocetak = new System.Windows.Forms.Button();
            this.Izlaz = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Srednje = new System.Windows.Forms.RadioButton();
            this.Tesko = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.odabir = new System.Windows.Forms.ComboBox();
            this.svojstva = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Pocetak
            // 
            this.Pocetak.Location = new System.Drawing.Point(16, 233);
            this.Pocetak.Name = "Pocetak";
            this.Pocetak.Size = new System.Drawing.Size(113, 57);
            this.Pocetak.TabIndex = 0;
            this.Pocetak.Text = "Početak";
            this.Pocetak.UseVisualStyleBackColor = true;
            this.Pocetak.Click += new System.EventHandler(this.Pocetak_Click);
            // 
            // Izlaz
            // 
            this.Izlaz.Location = new System.Drawing.Point(233, 233);
            this.Izlaz.Name = "Izlaz";
            this.Izlaz.Size = new System.Drawing.Size(113, 57);
            this.Izlaz.TabIndex = 1;
            this.Izlaz.Text = "Izlaz";
            this.Izlaz.UseVisualStyleBackColor = true;
            this.Izlaz.Click += new System.EventHandler(this.Izlaz_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mistral", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(134, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pass By ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(40, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Odabir Lika";
            // 
            // Srednje
            // 
            this.Srednje.AutoSize = true;
            this.Srednje.Location = new System.Drawing.Point(233, 89);
            this.Srednje.Name = "Srednje";
            this.Srednje.Size = new System.Drawing.Size(61, 17);
            this.Srednje.TabIndex = 6;
            this.Srednje.TabStop = true;
            this.Srednje.Text = "Srednje";
            this.Srednje.UseVisualStyleBackColor = true;
            // 
            // Tesko
            // 
            this.Tesko.AutoSize = true;
            this.Tesko.Location = new System.Drawing.Point(233, 141);
            this.Tesko.Name = "Tesko";
            this.Tesko.Size = new System.Drawing.Size(55, 17);
            this.Tesko.TabIndex = 7;
            this.Tesko.TabStop = true;
            this.Tesko.Text = "Teško";
            this.Tesko.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(229, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Odabir Težine";
            // 
            // odabir
            // 
            this.odabir.FormattingEnabled = true;
            this.odabir.Items.AddRange(new object[] {
            "Zrakoplov",
            "Ufo"});
            this.odabir.Location = new System.Drawing.Point(16, 89);
            this.odabir.Name = "odabir";
            this.odabir.Size = new System.Drawing.Size(121, 21);
            this.odabir.TabIndex = 9;
            this.odabir.SelectedIndexChanged += new System.EventHandler(this.odabir_SelectedIndexChanged);
            this.odabir.TextChanged += new System.EventHandler(this.odabir_TextChanged);
            // 
            // svojstva
            // 
            this.svojstva.AutoSize = true;
            this.svojstva.Location = new System.Drawing.Point(16, 117);
            this.svojstva.Name = "svojstva";
            this.svojstva.Size = new System.Drawing.Size(0, 13);
            this.svojstva.TabIndex = 10;
            this.svojstva.Click += new System.EventHandler(this.svojstva_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 302);
            this.Controls.Add(this.svojstva);
            this.Controls.Add(this.odabir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tesko);
            this.Controls.Add(this.Srednje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Izlaz);
            this.Controls.Add(this.Pocetak);
            this.Name = "Menu";
            this.Text = "Menu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Pocetak;
        public System.Windows.Forms.Button Izlaz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton Srednje;
        public System.Windows.Forms.RadioButton Tesko;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox odabir;
        private System.Windows.Forms.Label svojstva;
    }
}