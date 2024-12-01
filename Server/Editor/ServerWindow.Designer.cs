namespace Server
{
    partial class ServerWindow
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
            Button_Run = new Button();
            Button_Stop = new Button();
            ListBox_RecivedLog = new ListBox();
            TextBox_Command = new TextBox();
            button3 = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            groupBox1 = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Button_Run
            // 
            Button_Run.Location = new Point(148, 21);
            Button_Run.Name = "Button_Run";
            Button_Run.Size = new Size(75, 23);
            Button_Run.TabIndex = 0;
            Button_Run.Text = "Run";
            Button_Run.UseVisualStyleBackColor = true;
            Button_Run.Click += button1_Click;
            // 
            // Button_Stop
            // 
            Button_Stop.Location = new Point(148, 54);
            Button_Stop.Name = "Button_Stop";
            Button_Stop.Size = new Size(75, 23);
            Button_Stop.TabIndex = 1;
            Button_Stop.Text = "Stop";
            Button_Stop.UseVisualStyleBackColor = true;
            // 
            // ListBox_RecivedLog
            // 
            ListBox_RecivedLog.FormattingEnabled = true;
            ListBox_RecivedLog.ItemHeight = 15;
            ListBox_RecivedLog.Location = new Point(247, 19);
            ListBox_RecivedLog.Name = "ListBox_RecivedLog";
            ListBox_RecivedLog.Size = new Size(446, 319);
            ListBox_RecivedLog.TabIndex = 2;
            ListBox_RecivedLog.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // TextBox_Command
            // 
            TextBox_Command.Location = new Point(247, 344);
            TextBox_Command.Name = "TextBox_Command";
            TextBox_Command.Size = new Size(365, 23);
            TextBox_Command.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(618, 344);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "Execute";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(45, 22);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(92, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(92, 23);
            textBox1.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(Button_Stop);
            groupBox1.Controls.Add(Button_Run);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(229, 83);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server Info";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 25);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 7;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 54);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 8;
            label2.Text = "Port";
            // 
            // ServerWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(705, 441);
            Controls.Add(groupBox1);
            Controls.Add(button3);
            Controls.Add(TextBox_Command);
            Controls.Add(ListBox_RecivedLog);
            Name = "ServerWindow";
            Text = "ServerWindow";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button_Run;
        private Button Button_Stop;
        private ListBox ListBox_RecivedLog;
        private TextBox TextBox_Command;
        private Button button3;
        private TextBox textBox2;
        private TextBox textBox1;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
    }
}