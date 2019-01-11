namespace WatcomVS.Options
{
    partial class GeneralOptionsUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionsUI));
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.LinkLabel linkLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox2;
            this.textWatcomPath = new System.Windows.Forms.TextBox();
            this.buttonWatcomPathBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioOldPathsNo = new System.Windows.Forms.RadioButton();
            this.radioOldPathsLegacyOnly = new System.Windows.Forms.RadioButton();
            this.radioOldPathsAlways = new System.Windows.Forms.RadioButton();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            linkLabel = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(this.textWatcomPath, 0, 0);
            tableLayoutPanel1.Controls.Add(this.buttonWatcomPathBrowse, 1, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // textWatcomPath
            // 
            resources.ApplyResources(this.textWatcomPath, "textWatcomPath");
            this.textWatcomPath.Name = "textWatcomPath";
            this.textWatcomPath.ReadOnly = true;
            // 
            // buttonWatcomPathBrowse
            // 
            resources.ApplyResources(this.buttonWatcomPathBrowse, "buttonWatcomPathBrowse");
            this.buttonWatcomPathBrowse.Name = "buttonWatcomPathBrowse";
            this.buttonWatcomPathBrowse.UseVisualStyleBackColor = true;
            this.buttonWatcomPathBrowse.Click += new System.EventHandler(this.ButtonWatcomPathBrowse_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(linkLabel);
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Controls.Add(label1);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // linkLabel
            // 
            resources.ApplyResources(linkLabel, "linkLabel");
            linkLabel.Name = "linkLabel";
            linkLabel.TabStop = true;
            linkLabel.UseCompatibleTextRendering = true;
            linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(this.radioOldPathsAlways);
            groupBox2.Controls.Add(this.radioOldPathsLegacyOnly);
            groupBox2.Controls.Add(this.radioOldPathsNo);
            groupBox2.Controls.Add(this.label2);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // radioOldPathsNo
            // 
            resources.ApplyResources(this.radioOldPathsNo, "radioOldPathsNo");
            this.radioOldPathsNo.Name = "radioOldPathsNo";
            this.radioOldPathsNo.TabStop = true;
            this.radioOldPathsNo.UseVisualStyleBackColor = true;
            // 
            // radioOldPathsSometimes
            // 
            resources.ApplyResources(this.radioOldPathsLegacyOnly, "radioOldPathsSometimes");
            this.radioOldPathsLegacyOnly.Name = "radioOldPathsSometimes";
            this.radioOldPathsLegacyOnly.TabStop = true;
            this.radioOldPathsLegacyOnly.UseVisualStyleBackColor = true;
            // 
            // radioOldPathsAlways
            // 
            resources.ApplyResources(this.radioOldPathsAlways, "radioOldPathsAlways");
            this.radioOldPathsAlways.Name = "radioOldPathsAlways";
            this.radioOldPathsAlways.TabStop = true;
            this.radioOldPathsAlways.UseVisualStyleBackColor = true;
            // 
            // GeneralOptionsUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Name = "GeneralOptionsUI";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonWatcomPathBrowse;
        private System.Windows.Forms.TextBox textWatcomPath;
        private System.Windows.Forms.RadioButton radioOldPathsAlways;
        private System.Windows.Forms.RadioButton radioOldPathsLegacyOnly;
        private System.Windows.Forms.RadioButton radioOldPathsNo;
        private System.Windows.Forms.Label label2;
    }
}
