namespace _2AP_Last_Dance
{
    partial class AutoPipe
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonSimulate = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxForwarding = new System.Windows.Forms.CheckBox();
            this.dataGridViewRegs = new System.Windows.Forms.DataGridView();
            this.panelStageAccess = new System.Windows.Forms.Panel();
            this.radioButtonMEM = new System.Windows.Forms.RadioButton();
            this.radioButtonID = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonF = new System.Windows.Forms.RadioButton();
            this.radioButtonT = new System.Windows.Forms.RadioButton();
            this.radioButtonN = new System.Windows.Forms.RadioButton();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegs)).BeginInit();
            this.panelStageAccess.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.Location = new System.Drawing.Point(13, 107);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 668);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit_1);
            // 
            // buttonSimulate
            // 
            this.buttonSimulate.BackColor = System.Drawing.Color.LawnGreen;
            this.buttonSimulate.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSimulate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSimulate.Location = new System.Drawing.Point(13, 13);
            this.buttonSimulate.Name = "buttonSimulate";
            this.buttonSimulate.Size = new System.Drawing.Size(168, 78);
            this.buttonSimulate.TabIndex = 2;
            this.buttonSimulate.Text = "START";
            this.buttonSimulate.UseVisualStyleBackColor = false;
            this.buttonSimulate.Click += new System.EventHandler(this.buttonSimulate_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.BackColor = System.Drawing.Color.Orange;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(187, 13);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(168, 79);
            this.buttonCheck.TabIndex = 3;
            this.buttonCheck.Text = "SERVICE";
            this.buttonCheck.UseVisualStyleBackColor = false;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(635, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 4;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBoxForwarding
            // 
            this.checkBoxForwarding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForwarding.AutoSize = true;
            this.checkBoxForwarding.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxForwarding.Location = new System.Drawing.Point(691, 12);
            this.checkBoxForwarding.Name = "checkBoxForwarding";
            this.checkBoxForwarding.Size = new System.Drawing.Size(114, 22);
            this.checkBoxForwarding.TabIndex = 5;
            this.checkBoxForwarding.Text = "Forwarding";
            this.checkBoxForwarding.UseVisualStyleBackColor = true;
            this.checkBoxForwarding.CheckedChanged += new System.EventHandler(this.checkBoxForwarding_CheckedChanged);
            // 
            // dataGridViewRegs
            // 
            this.dataGridViewRegs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRegs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegs.Location = new System.Drawing.Point(1343, 107);
            this.dataGridViewRegs.Name = "dataGridViewRegs";
            this.dataGridViewRegs.RowHeadersWidth = 51;
            this.dataGridViewRegs.RowTemplate.Height = 24;
            this.dataGridViewRegs.Size = new System.Drawing.Size(334, 668);
            this.dataGridViewRegs.TabIndex = 6;
            // 
            // panelStageAccess
            // 
            this.panelStageAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStageAccess.Controls.Add(this.radioButtonMEM);
            this.panelStageAccess.Controls.Add(this.radioButtonID);
            this.panelStageAccess.Location = new System.Drawing.Point(841, 4);
            this.panelStageAccess.Name = "panelStageAccess";
            this.panelStageAccess.Size = new System.Drawing.Size(196, 97);
            this.panelStageAccess.TabIndex = 7;
            // 
            // radioButtonMEM
            // 
            this.radioButtonMEM.AutoSize = true;
            this.radioButtonMEM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonMEM.Location = new System.Drawing.Point(13, 35);
            this.radioButtonMEM.Name = "radioButtonMEM";
            this.radioButtonMEM.Size = new System.Drawing.Size(68, 22);
            this.radioButtonMEM.TabIndex = 1;
            this.radioButtonMEM.TabStop = true;
            this.radioButtonMEM.Text = "MEM";
            this.radioButtonMEM.UseVisualStyleBackColor = true;
            // 
            // radioButtonID
            // 
            this.radioButtonID.AutoSize = true;
            this.radioButtonID.Checked = true;
            this.radioButtonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonID.Location = new System.Drawing.Point(13, 9);
            this.radioButtonID.Name = "radioButtonID";
            this.radioButtonID.Size = new System.Drawing.Size(45, 22);
            this.radioButtonID.TabIndex = 0;
            this.radioButtonID.TabStop = true;
            this.radioButtonID.Text = "ID";
            this.radioButtonID.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.radioButtonF);
            this.panel1.Controls.Add(this.radioButtonT);
            this.panel1.Controls.Add(this.radioButtonN);
            this.panel1.Location = new System.Drawing.Point(1043, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 97);
            this.panel1.TabIndex = 8;
            // 
            // radioButtonF
            // 
            this.radioButtonF.AutoSize = true;
            this.radioButtonF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonF.Location = new System.Drawing.Point(3, 61);
            this.radioButtonF.Name = "radioButtonF";
            this.radioButtonF.Size = new System.Drawing.Size(178, 22);
            this.radioButtonF.TabIndex = 2;
            this.radioButtonF.TabStop = true;
            this.radioButtonF.Text = "Перехода не будет";
            this.radioButtonF.UseVisualStyleBackColor = true;
            // 
            // radioButtonT
            // 
            this.radioButtonT.AutoSize = true;
            this.radioButtonT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonT.Location = new System.Drawing.Point(3, 34);
            this.radioButtonT.Name = "radioButtonT";
            this.radioButtonT.Size = new System.Drawing.Size(146, 22);
            this.radioButtonT.TabIndex = 1;
            this.radioButtonT.TabStop = true;
            this.radioButtonT.Text = "Переход будет";
            this.radioButtonT.UseVisualStyleBackColor = true;
            // 
            // radioButtonN
            // 
            this.radioButtonN.AutoSize = true;
            this.radioButtonN.Checked = true;
            this.radioButtonN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonN.Location = new System.Drawing.Point(3, 8);
            this.radioButtonN.Name = "radioButtonN";
            this.radioButtonN.Size = new System.Drawing.Size(250, 22);
            this.radioButtonN.TabIndex = 0;
            this.radioButtonN.TabStop = true;
            this.radioButtonN.Text = "Нет предсказаний перехода";
            this.radioButtonN.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Orange;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(361, 13);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(168, 79);
            this.buttonClear.TabIndex = 9;
            this.buttonClear.Text = "CLEAR";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // AutoPipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1689, 789);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelStageAccess);
            this.Controls.Add(this.checkBoxForwarding);
            this.Controls.Add(this.dataGridViewRegs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonSimulate);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AutoPipe";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegs)).EndInit();
            this.panelStageAccess.ResumeLayout(false);
            this.panelStageAccess.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSimulate;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxForwarding;
        private System.Windows.Forms.DataGridView dataGridViewRegs;
        private System.Windows.Forms.Panel panelStageAccess;
        private System.Windows.Forms.RadioButton radioButtonMEM;
        private System.Windows.Forms.RadioButton radioButtonID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonF;
        private System.Windows.Forms.RadioButton radioButtonT;
        private System.Windows.Forms.RadioButton radioButtonN;
        private System.Windows.Forms.Button buttonClear;
    }
}

