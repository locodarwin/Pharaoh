namespace Pharaoh
{
    partial class Form1
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
            this.butLogin = new System.Windows.Forms.Button();
            this.butLogout = new System.Windows.Forms.Button();
            this.butConfig = new System.Windows.Forms.Button();
            this.lisStatus = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolLoggedIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBotname = new System.Windows.Forms.TextBox();
            this.textCitnum = new System.Windows.Forms.TextBox();
            this.textPrivPass = new System.Windows.Forms.TextBox();
            this.textWorld = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textCoords = new System.Windows.Forms.TextBox();
            this.textAvatar = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butLogin
            // 
            this.butLogin.Location = new System.Drawing.Point(289, 14);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(75, 23);
            this.butLogin.TabIndex = 0;
            this.butLogin.Text = "Log In";
            this.butLogin.UseVisualStyleBackColor = true;
            this.butLogin.Click += new System.EventHandler(this.butLogin_Click);
            // 
            // butLogout
            // 
            this.butLogout.Location = new System.Drawing.Point(289, 43);
            this.butLogout.Name = "butLogout";
            this.butLogout.Size = new System.Drawing.Size(75, 23);
            this.butLogout.TabIndex = 1;
            this.butLogout.Text = "Log Out";
            this.butLogout.UseVisualStyleBackColor = true;
            this.butLogout.Click += new System.EventHandler(this.butLogout_Click);
            // 
            // butConfig
            // 
            this.butConfig.Location = new System.Drawing.Point(289, 72);
            this.butConfig.Name = "butConfig";
            this.butConfig.Size = new System.Drawing.Size(75, 23);
            this.butConfig.TabIndex = 2;
            this.butConfig.Text = "Config";
            this.butConfig.UseVisualStyleBackColor = true;
            this.butConfig.Click += new System.EventHandler(this.butConfig_Click);
            // 
            // lisStatus
            // 
            this.lisStatus.FormattingEnabled = true;
            this.lisStatus.Location = new System.Drawing.Point(6, 111);
            this.lisStatus.Name = "lisStatus";
            this.lisStatus.Size = new System.Drawing.Size(358, 160);
            this.lisStatus.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLoggedIn});
            this.statusStrip1.Location = new System.Drawing.Point(0, 276);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(369, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolLoggedIn
            // 
            this.toolLoggedIn.Name = "toolLoggedIn";
            this.toolLoggedIn.Size = new System.Drawing.Size(70, 17);
            this.toolLoggedIn.Text = "Logged Out";
            // 
            // textBotname
            // 
            this.textBotname.Location = new System.Drawing.Point(53, 16);
            this.textBotname.Name = "textBotname";
            this.textBotname.Size = new System.Drawing.Size(74, 20);
            this.textBotname.TabIndex = 5;
            // 
            // textCitnum
            // 
            this.textCitnum.Location = new System.Drawing.Point(53, 45);
            this.textCitnum.Name = "textCitnum";
            this.textCitnum.Size = new System.Drawing.Size(74, 20);
            this.textCitnum.TabIndex = 6;
            // 
            // textPrivPass
            // 
            this.textPrivPass.Location = new System.Drawing.Point(53, 74);
            this.textPrivPass.Name = "textPrivPass";
            this.textPrivPass.PasswordChar = '*';
            this.textPrivPass.Size = new System.Drawing.Size(74, 20);
            this.textPrivPass.TabIndex = 7;
            // 
            // textWorld
            // 
            this.textWorld.Location = new System.Drawing.Point(181, 16);
            this.textWorld.Name = "textWorld";
            this.textWorld.Size = new System.Drawing.Size(90, 20);
            this.textWorld.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Botname:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Citnum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "PrivPass:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "World:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Coords:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Avatar:";
            // 
            // textCoords
            // 
            this.textCoords.Location = new System.Drawing.Point(181, 45);
            this.textCoords.Name = "textCoords";
            this.textCoords.Size = new System.Drawing.Size(90, 20);
            this.textCoords.TabIndex = 15;
            // 
            // textAvatar
            // 
            this.textAvatar.Location = new System.Drawing.Point(181, 74);
            this.textAvatar.Name = "textAvatar";
            this.textAvatar.Size = new System.Drawing.Size(52, 20);
            this.textAvatar.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 298);
            this.Controls.Add(this.textAvatar);
            this.Controls.Add(this.textCoords);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textWorld);
            this.Controls.Add(this.textPrivPass);
            this.Controls.Add(this.textCitnum);
            this.Controls.Add(this.textBotname);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lisStatus);
            this.Controls.Add(this.butConfig);
            this.Controls.Add(this.butLogout);
            this.Controls.Add(this.butLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pharaoh";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butLogin;
        private System.Windows.Forms.Button butLogout;
        private System.Windows.Forms.Button butConfig;
        private System.Windows.Forms.ListBox lisStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolLoggedIn;
        private System.Windows.Forms.TextBox textBotname;
        private System.Windows.Forms.TextBox textCitnum;
        private System.Windows.Forms.TextBox textPrivPass;
        private System.Windows.Forms.TextBox textWorld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textCoords;
        private System.Windows.Forms.TextBox textAvatar;
    }
}

