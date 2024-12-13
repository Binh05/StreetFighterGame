namespace StreetFighterGame
{
    partial class StreetFighterGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreetFighterGame));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.labelWiner = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            // 
            // labelWiner
            // 
            this.labelWiner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelWiner.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelWiner.Font = new System.Drawing.Font("Wide Latin", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWiner.ForeColor = System.Drawing.Color.Orange;
            this.labelWiner.Location = new System.Drawing.Point(349, 147);
            this.labelWiner.Name = "labelWiner";
            this.labelWiner.Size = new System.Drawing.Size(473, 99);
            this.labelWiner.TabIndex = 0;
            this.labelWiner.Text = "Goku WIN";
            this.labelWiner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelWiner.Click += new System.EventHandler(this.labelWiner_Click);
            // 
            // StreetFighterGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 583);
            this.Controls.Add(this.labelWiner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "StreetFighterGame";
            this.Text = "Street Fighter 2.0";
            this.Load += new System.EventHandler(this.StreetFighterGame_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label labelWiner;
    }
}

