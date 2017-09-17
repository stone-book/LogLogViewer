namespace LogLogViewer.Forms
{
    partial class SettingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDialog));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.maxLineLength = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxReadSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.radioButtonUTF16BE = new System.Windows.Forms.RadioButton();
            this.radioButtonUTF16LE = new System.Windows.Forms.RadioButton();
            this.radioButtonEucJP = new System.Windows.Forms.RadioButton();
            this.radioButtonJIS = new System.Windows.Forms.RadioButton();
            this.radioButtonUTF8 = new System.Windows.Forms.RadioButton();
            this.radioButtonShiftJIS = new System.Windows.Forms.RadioButton();
            this.postScriptModeCheck = new System.Windows.Forms.CheckBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFontInfo = new System.Windows.Forms.TextBox();
            this.buttonFont = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backColorPic = new System.Windows.Forms.PictureBox();
            this.fontColorPic = new System.Windows.Forms.PictureBox();
            this.checkBoxShowStatusBar = new System.Windows.Forms.CheckBox();
            this.checkBoxIsSaveHistory = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxIsUrlLink = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxPtn = new System.Windows.Forms.GroupBox();
            this.dataGridViewPtn = new System.Windows.Forms.DataGridView();
            this.buttonPtnDelete = new System.Windows.Forms.Button();
            this.buttonPtnUp = new System.Windows.Forms.Button();
            this.buttonPtnDown = new System.Windows.Forms.Button();
            this.buttonPtnAdd = new System.Windows.Forms.Button();
            this.checkBoxPtnEnable = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.dataGridViewFilter = new System.Windows.Forms.DataGridView();
            this.buttonFilterDelete = new System.Windows.Forms.Button();
            this.buttonFilterUp = new System.Windows.Forms.Button();
            this.buttonFilterDown = new System.Windows.Forms.Button();
            this.buttonFilterAdd = new System.Windows.Forms.Button();
            this.checkBoxFilterEnable = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBoxMail = new System.Windows.Forms.GroupBox();
            this.buttonMailAccount = new System.Windows.Forms.Button();
            this.dataGridViewMail = new System.Windows.Forms.DataGridView();
            this.buttonMailDelete = new System.Windows.Forms.Button();
            this.buttonMailUp = new System.Windows.Forms.Button();
            this.buttonMailDown = new System.Windows.Forms.Button();
            this.buttonMailAdd = new System.Windows.Forms.Button();
            this.checkBoxMailEnable = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonInport = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.maxLineLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxReadSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backColorPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPic)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxPtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPtn)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBoxMail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.toolTip1.SetToolTip(this.buttonOK, resources.GetString("buttonOK.ToolTip"));
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // maxLineLength
            // 
            resources.ApplyResources(this.maxLineLength, "maxLineLength");
            this.maxLineLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxLineLength.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxLineLength.Name = "maxLineLength";
            this.toolTip1.SetToolTip(this.maxLineLength, resources.GetString("maxLineLength.ToolTip"));
            this.maxLineLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // maxReadSize
            // 
            resources.ApplyResources(this.maxReadSize, "maxReadSize");
            this.maxReadSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxReadSize.Name = "maxReadSize";
            this.toolTip1.SetToolTip(this.maxReadSize, resources.GetString("maxReadSize.ToolTip"));
            this.maxReadSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.radioButtonAuto);
            this.groupBox2.Controls.Add(this.radioButtonUTF16BE);
            this.groupBox2.Controls.Add(this.radioButtonUTF16LE);
            this.groupBox2.Controls.Add(this.radioButtonEucJP);
            this.groupBox2.Controls.Add(this.radioButtonJIS);
            this.groupBox2.Controls.Add(this.radioButtonUTF8);
            this.groupBox2.Controls.Add(this.radioButtonShiftJIS);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // radioButtonAuto
            // 
            resources.ApplyResources(this.radioButtonAuto, "radioButtonAuto");
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonAuto, resources.GetString("radioButtonAuto.ToolTip"));
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            // 
            // radioButtonUTF16BE
            // 
            resources.ApplyResources(this.radioButtonUTF16BE, "radioButtonUTF16BE");
            this.radioButtonUTF16BE.Name = "radioButtonUTF16BE";
            this.radioButtonUTF16BE.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonUTF16BE, resources.GetString("radioButtonUTF16BE.ToolTip"));
            this.radioButtonUTF16BE.UseVisualStyleBackColor = true;
            // 
            // radioButtonUTF16LE
            // 
            resources.ApplyResources(this.radioButtonUTF16LE, "radioButtonUTF16LE");
            this.radioButtonUTF16LE.Name = "radioButtonUTF16LE";
            this.radioButtonUTF16LE.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonUTF16LE, resources.GetString("radioButtonUTF16LE.ToolTip"));
            this.radioButtonUTF16LE.UseVisualStyleBackColor = true;
            // 
            // radioButtonEucJP
            // 
            resources.ApplyResources(this.radioButtonEucJP, "radioButtonEucJP");
            this.radioButtonEucJP.Name = "radioButtonEucJP";
            this.radioButtonEucJP.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonEucJP, resources.GetString("radioButtonEucJP.ToolTip"));
            this.radioButtonEucJP.UseVisualStyleBackColor = true;
            // 
            // radioButtonJIS
            // 
            resources.ApplyResources(this.radioButtonJIS, "radioButtonJIS");
            this.radioButtonJIS.Name = "radioButtonJIS";
            this.radioButtonJIS.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonJIS, resources.GetString("radioButtonJIS.ToolTip"));
            this.radioButtonJIS.UseVisualStyleBackColor = true;
            // 
            // radioButtonUTF8
            // 
            resources.ApplyResources(this.radioButtonUTF8, "radioButtonUTF8");
            this.radioButtonUTF8.Name = "radioButtonUTF8";
            this.radioButtonUTF8.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonUTF8, resources.GetString("radioButtonUTF8.ToolTip"));
            this.radioButtonUTF8.UseVisualStyleBackColor = true;
            // 
            // radioButtonShiftJIS
            // 
            resources.ApplyResources(this.radioButtonShiftJIS, "radioButtonShiftJIS");
            this.radioButtonShiftJIS.Name = "radioButtonShiftJIS";
            this.radioButtonShiftJIS.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonShiftJIS, resources.GetString("radioButtonShiftJIS.ToolTip"));
            this.radioButtonShiftJIS.UseVisualStyleBackColor = true;
            // 
            // postScriptModeCheck
            // 
            resources.ApplyResources(this.postScriptModeCheck, "postScriptModeCheck");
            this.postScriptModeCheck.Name = "postScriptModeCheck";
            this.toolTip1.SetToolTip(this.postScriptModeCheck, resources.GetString("postScriptModeCheck.ToolTip"));
            this.postScriptModeCheck.UseVisualStyleBackColor = true;
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowEffects = false;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBoxFontInfo);
            this.groupBox1.Controls.Add(this.buttonFont);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.backColorPic);
            this.groupBox1.Controls.Add(this.fontColorPic);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // textBoxFontInfo
            // 
            resources.ApplyResources(this.textBoxFontInfo, "textBoxFontInfo");
            this.textBoxFontInfo.Name = "textBoxFontInfo";
            this.textBoxFontInfo.ReadOnly = true;
            this.textBoxFontInfo.TabStop = false;
            this.toolTip1.SetToolTip(this.textBoxFontInfo, resources.GetString("textBoxFontInfo.ToolTip"));
            // 
            // buttonFont
            // 
            resources.ApplyResources(this.buttonFont, "buttonFont");
            this.buttonFont.Image = global::LogLogViewer.Properties.Resources.edit_family;
            this.buttonFont.Name = "buttonFont";
            this.toolTip1.SetToolTip(this.buttonFont, resources.GetString("buttonFont.ToolTip"));
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // backColorPic
            // 
            resources.ApplyResources(this.backColorPic, "backColorPic");
            this.backColorPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.backColorPic.Name = "backColorPic";
            this.backColorPic.TabStop = false;
            this.toolTip1.SetToolTip(this.backColorPic, resources.GetString("backColorPic.ToolTip"));
            this.backColorPic.DoubleClick += new System.EventHandler(this.backColorPic_Click);
            // 
            // fontColorPic
            // 
            resources.ApplyResources(this.fontColorPic, "fontColorPic");
            this.fontColorPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fontColorPic.Name = "fontColorPic";
            this.fontColorPic.TabStop = false;
            this.toolTip1.SetToolTip(this.fontColorPic, resources.GetString("fontColorPic.ToolTip"));
            this.fontColorPic.DoubleClick += new System.EventHandler(this.fontColorPic_Click);
            // 
            // checkBoxShowStatusBar
            // 
            resources.ApplyResources(this.checkBoxShowStatusBar, "checkBoxShowStatusBar");
            this.checkBoxShowStatusBar.Name = "checkBoxShowStatusBar";
            this.toolTip1.SetToolTip(this.checkBoxShowStatusBar, resources.GetString("checkBoxShowStatusBar.ToolTip"));
            this.checkBoxShowStatusBar.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsSaveHistory
            // 
            resources.ApplyResources(this.checkBoxIsSaveHistory, "checkBoxIsSaveHistory");
            this.checkBoxIsSaveHistory.Name = "checkBoxIsSaveHistory";
            this.toolTip1.SetToolTip(this.checkBoxIsSaveHistory, resources.GetString("checkBoxIsSaveHistory.ToolTip"));
            this.checkBoxIsSaveHistory.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // buttonDefault
            // 
            resources.ApplyResources(this.buttonDefault, "buttonDefault");
            this.buttonDefault.Name = "buttonDefault";
            this.toolTip1.SetToolTip(this.buttonDefault, resources.GetString("buttonDefault.ToolTip"));
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.checkBoxIsUrlLink);
            this.tabPage1.Controls.Add(this.checkBoxShowStatusBar);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkBoxIsSaveHistory);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.maxReadSize);
            this.tabPage1.Controls.Add(this.postScriptModeCheck);
            this.tabPage1.Controls.Add(this.maxLineLength);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.buttonDefault);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Name = "tabPage1";
            this.toolTip1.SetToolTip(this.tabPage1, resources.GetString("tabPage1.ToolTip"));
            // 
            // checkBoxIsUrlLink
            // 
            resources.ApplyResources(this.checkBoxIsUrlLink, "checkBoxIsUrlLink");
            this.checkBoxIsUrlLink.Name = "checkBoxIsUrlLink";
            this.toolTip1.SetToolTip(this.checkBoxIsUrlLink, resources.GetString("checkBoxIsUrlLink.ToolTip"));
            this.checkBoxIsUrlLink.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage2.Controls.Add(this.groupBoxPtn);
            this.tabPage2.Controls.Add(this.checkBoxPtnEnable);
            this.tabPage2.Name = "tabPage2";
            this.toolTip1.SetToolTip(this.tabPage2, resources.GetString("tabPage2.ToolTip"));
            // 
            // groupBoxPtn
            // 
            resources.ApplyResources(this.groupBoxPtn, "groupBoxPtn");
            this.groupBoxPtn.Controls.Add(this.dataGridViewPtn);
            this.groupBoxPtn.Controls.Add(this.buttonPtnDelete);
            this.groupBoxPtn.Controls.Add(this.buttonPtnUp);
            this.groupBoxPtn.Controls.Add(this.buttonPtnDown);
            this.groupBoxPtn.Controls.Add(this.buttonPtnAdd);
            this.groupBoxPtn.Name = "groupBoxPtn";
            this.groupBoxPtn.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxPtn, resources.GetString("groupBoxPtn.ToolTip"));
            // 
            // dataGridViewPtn
            // 
            resources.ApplyResources(this.dataGridViewPtn, "dataGridViewPtn");
            this.dataGridViewPtn.AllowUserToAddRows = false;
            this.dataGridViewPtn.AllowUserToDeleteRows = false;
            this.dataGridViewPtn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewPtn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPtn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPtn.MultiSelect = false;
            this.dataGridViewPtn.Name = "dataGridViewPtn";
            this.dataGridViewPtn.RowHeadersVisible = false;
            this.dataGridViewPtn.RowTemplate.Height = 21;
            this.toolTip1.SetToolTip(this.dataGridViewPtn, resources.GetString("dataGridViewPtn.ToolTip"));
            this.dataGridViewPtn.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPtn_CellFormatting);
            this.dataGridViewPtn.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewPtn_CellMouseDoubleClick);
            // 
            // buttonPtnDelete
            // 
            resources.ApplyResources(this.buttonPtnDelete, "buttonPtnDelete");
            this.buttonPtnDelete.Image = global::LogLogViewer.Properties.Resources.cross;
            this.buttonPtnDelete.Name = "buttonPtnDelete";
            this.toolTip1.SetToolTip(this.buttonPtnDelete, resources.GetString("buttonPtnDelete.ToolTip"));
            this.buttonPtnDelete.UseVisualStyleBackColor = true;
            this.buttonPtnDelete.Click += new System.EventHandler(this.buttonPtnDelete_Click);
            // 
            // buttonPtnUp
            // 
            resources.ApplyResources(this.buttonPtnUp, "buttonPtnUp");
            this.buttonPtnUp.Image = global::LogLogViewer.Properties.Resources.arrow_090;
            this.buttonPtnUp.Name = "buttonPtnUp";
            this.toolTip1.SetToolTip(this.buttonPtnUp, resources.GetString("buttonPtnUp.ToolTip"));
            this.buttonPtnUp.UseVisualStyleBackColor = true;
            this.buttonPtnUp.Click += new System.EventHandler(this.buttonPtnUp_Click);
            // 
            // buttonPtnDown
            // 
            resources.ApplyResources(this.buttonPtnDown, "buttonPtnDown");
            this.buttonPtnDown.Image = global::LogLogViewer.Properties.Resources.arrow_270;
            this.buttonPtnDown.Name = "buttonPtnDown";
            this.toolTip1.SetToolTip(this.buttonPtnDown, resources.GetString("buttonPtnDown.ToolTip"));
            this.buttonPtnDown.UseVisualStyleBackColor = true;
            this.buttonPtnDown.Click += new System.EventHandler(this.buttonPtnDown_Click);
            // 
            // buttonPtnAdd
            // 
            resources.ApplyResources(this.buttonPtnAdd, "buttonPtnAdd");
            this.buttonPtnAdd.Image = global::LogLogViewer.Properties.Resources.plus_circle;
            this.buttonPtnAdd.Name = "buttonPtnAdd";
            this.toolTip1.SetToolTip(this.buttonPtnAdd, resources.GetString("buttonPtnAdd.ToolTip"));
            this.buttonPtnAdd.UseVisualStyleBackColor = true;
            this.buttonPtnAdd.Click += new System.EventHandler(this.buttonPtnAdd_Click);
            // 
            // checkBoxPtnEnable
            // 
            resources.ApplyResources(this.checkBoxPtnEnable, "checkBoxPtnEnable");
            this.checkBoxPtnEnable.Name = "checkBoxPtnEnable";
            this.toolTip1.SetToolTip(this.checkBoxPtnEnable, resources.GetString("checkBoxPtnEnable.ToolTip"));
            this.checkBoxPtnEnable.UseVisualStyleBackColor = true;
            this.checkBoxPtnEnable.CheckedChanged += new System.EventHandler(this.checkBoxPtnEnable_CheckedChanged);
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage3.Controls.Add(this.groupBoxFilter);
            this.tabPage3.Controls.Add(this.checkBoxFilterEnable);
            this.tabPage3.Name = "tabPage3";
            this.toolTip1.SetToolTip(this.tabPage3, resources.GetString("tabPage3.ToolTip"));
            // 
            // groupBoxFilter
            // 
            resources.ApplyResources(this.groupBoxFilter, "groupBoxFilter");
            this.groupBoxFilter.Controls.Add(this.dataGridViewFilter);
            this.groupBoxFilter.Controls.Add(this.buttonFilterDelete);
            this.groupBoxFilter.Controls.Add(this.buttonFilterUp);
            this.groupBoxFilter.Controls.Add(this.buttonFilterDown);
            this.groupBoxFilter.Controls.Add(this.buttonFilterAdd);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxFilter, resources.GetString("groupBoxFilter.ToolTip"));
            // 
            // dataGridViewFilter
            // 
            resources.ApplyResources(this.dataGridViewFilter, "dataGridViewFilter");
            this.dataGridViewFilter.AllowUserToAddRows = false;
            this.dataGridViewFilter.AllowUserToDeleteRows = false;
            this.dataGridViewFilter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewFilter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFilter.MultiSelect = false;
            this.dataGridViewFilter.Name = "dataGridViewFilter";
            this.dataGridViewFilter.RowHeadersVisible = false;
            this.dataGridViewFilter.RowTemplate.Height = 21;
            this.toolTip1.SetToolTip(this.dataGridViewFilter, resources.GetString("dataGridViewFilter.ToolTip"));
            // 
            // buttonFilterDelete
            // 
            resources.ApplyResources(this.buttonFilterDelete, "buttonFilterDelete");
            this.buttonFilterDelete.Image = global::LogLogViewer.Properties.Resources.cross;
            this.buttonFilterDelete.Name = "buttonFilterDelete";
            this.toolTip1.SetToolTip(this.buttonFilterDelete, resources.GetString("buttonFilterDelete.ToolTip"));
            this.buttonFilterDelete.UseVisualStyleBackColor = true;
            this.buttonFilterDelete.Click += new System.EventHandler(this.buttoFilterDelete_Click);
            // 
            // buttonFilterUp
            // 
            resources.ApplyResources(this.buttonFilterUp, "buttonFilterUp");
            this.buttonFilterUp.Image = global::LogLogViewer.Properties.Resources.arrow_090;
            this.buttonFilterUp.Name = "buttonFilterUp";
            this.toolTip1.SetToolTip(this.buttonFilterUp, resources.GetString("buttonFilterUp.ToolTip"));
            this.buttonFilterUp.UseVisualStyleBackColor = true;
            this.buttonFilterUp.Click += new System.EventHandler(this.buttoFilterUp_Click);
            // 
            // buttonFilterDown
            // 
            resources.ApplyResources(this.buttonFilterDown, "buttonFilterDown");
            this.buttonFilterDown.Image = global::LogLogViewer.Properties.Resources.arrow_270;
            this.buttonFilterDown.Name = "buttonFilterDown";
            this.toolTip1.SetToolTip(this.buttonFilterDown, resources.GetString("buttonFilterDown.ToolTip"));
            this.buttonFilterDown.UseVisualStyleBackColor = true;
            this.buttonFilterDown.Click += new System.EventHandler(this.buttoFilterDown_Click);
            // 
            // buttonFilterAdd
            // 
            resources.ApplyResources(this.buttonFilterAdd, "buttonFilterAdd");
            this.buttonFilterAdd.Image = global::LogLogViewer.Properties.Resources.plus_circle;
            this.buttonFilterAdd.Name = "buttonFilterAdd";
            this.toolTip1.SetToolTip(this.buttonFilterAdd, resources.GetString("buttonFilterAdd.ToolTip"));
            this.buttonFilterAdd.UseVisualStyleBackColor = true;
            this.buttonFilterAdd.Click += new System.EventHandler(this.buttoFilterAdd_Click);
            // 
            // checkBoxFilterEnable
            // 
            resources.ApplyResources(this.checkBoxFilterEnable, "checkBoxFilterEnable");
            this.checkBoxFilterEnable.Name = "checkBoxFilterEnable";
            this.toolTip1.SetToolTip(this.checkBoxFilterEnable, resources.GetString("checkBoxFilterEnable.ToolTip"));
            this.checkBoxFilterEnable.UseVisualStyleBackColor = true;
            this.checkBoxFilterEnable.CheckedChanged += new System.EventHandler(this.checkBoxFilterEnable_CheckedChanged);
            // 
            // tabPage5
            // 
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Controls.Add(this.groupBoxMail);
            this.tabPage5.Controls.Add(this.checkBoxMailEnable);
            this.tabPage5.Name = "tabPage5";
            this.toolTip1.SetToolTip(this.tabPage5, resources.GetString("tabPage5.ToolTip"));
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBoxMail
            // 
            resources.ApplyResources(this.groupBoxMail, "groupBoxMail");
            this.groupBoxMail.Controls.Add(this.buttonMailAccount);
            this.groupBoxMail.Controls.Add(this.dataGridViewMail);
            this.groupBoxMail.Controls.Add(this.buttonMailDelete);
            this.groupBoxMail.Controls.Add(this.buttonMailUp);
            this.groupBoxMail.Controls.Add(this.buttonMailDown);
            this.groupBoxMail.Controls.Add(this.buttonMailAdd);
            this.groupBoxMail.Name = "groupBoxMail";
            this.groupBoxMail.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxMail, resources.GetString("groupBoxMail.ToolTip"));
            // 
            // buttonMailAccount
            // 
            resources.ApplyResources(this.buttonMailAccount, "buttonMailAccount");
            this.buttonMailAccount.Name = "buttonMailAccount";
            this.toolTip1.SetToolTip(this.buttonMailAccount, resources.GetString("buttonMailAccount.ToolTip"));
            this.buttonMailAccount.UseVisualStyleBackColor = true;
            this.buttonMailAccount.Click += new System.EventHandler(this.buttonMailAccount_Click);
            // 
            // dataGridViewMail
            // 
            resources.ApplyResources(this.dataGridViewMail, "dataGridViewMail");
            this.dataGridViewMail.AllowUserToAddRows = false;
            this.dataGridViewMail.AllowUserToDeleteRows = false;
            this.dataGridViewMail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMail.MultiSelect = false;
            this.dataGridViewMail.Name = "dataGridViewMail";
            this.dataGridViewMail.RowHeadersVisible = false;
            this.dataGridViewMail.RowTemplate.Height = 21;
            this.toolTip1.SetToolTip(this.dataGridViewMail, resources.GetString("dataGridViewMail.ToolTip"));
            this.dataGridViewMail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMail_CellContentClick);
            // 
            // buttonMailDelete
            // 
            resources.ApplyResources(this.buttonMailDelete, "buttonMailDelete");
            this.buttonMailDelete.Image = global::LogLogViewer.Properties.Resources.cross;
            this.buttonMailDelete.Name = "buttonMailDelete";
            this.toolTip1.SetToolTip(this.buttonMailDelete, resources.GetString("buttonMailDelete.ToolTip"));
            this.buttonMailDelete.UseVisualStyleBackColor = true;
            this.buttonMailDelete.Click += new System.EventHandler(this.buttonMailDelete_Click);
            // 
            // buttonMailUp
            // 
            resources.ApplyResources(this.buttonMailUp, "buttonMailUp");
            this.buttonMailUp.Image = global::LogLogViewer.Properties.Resources.arrow_090;
            this.buttonMailUp.Name = "buttonMailUp";
            this.toolTip1.SetToolTip(this.buttonMailUp, resources.GetString("buttonMailUp.ToolTip"));
            this.buttonMailUp.UseVisualStyleBackColor = true;
            this.buttonMailUp.Click += new System.EventHandler(this.buttonMailUp_Click);
            // 
            // buttonMailDown
            // 
            resources.ApplyResources(this.buttonMailDown, "buttonMailDown");
            this.buttonMailDown.Image = global::LogLogViewer.Properties.Resources.arrow_270;
            this.buttonMailDown.Name = "buttonMailDown";
            this.toolTip1.SetToolTip(this.buttonMailDown, resources.GetString("buttonMailDown.ToolTip"));
            this.buttonMailDown.UseVisualStyleBackColor = true;
            this.buttonMailDown.Click += new System.EventHandler(this.buttonMailDown_Click);
            // 
            // buttonMailAdd
            // 
            resources.ApplyResources(this.buttonMailAdd, "buttonMailAdd");
            this.buttonMailAdd.Image = global::LogLogViewer.Properties.Resources.plus_circle;
            this.buttonMailAdd.Name = "buttonMailAdd";
            this.toolTip1.SetToolTip(this.buttonMailAdd, resources.GetString("buttonMailAdd.ToolTip"));
            this.buttonMailAdd.UseVisualStyleBackColor = true;
            this.buttonMailAdd.Click += new System.EventHandler(this.buttonMailAdd_Click);
            // 
            // checkBoxMailEnable
            // 
            resources.ApplyResources(this.checkBoxMailEnable, "checkBoxMailEnable");
            this.checkBoxMailEnable.Name = "checkBoxMailEnable";
            this.toolTip1.SetToolTip(this.checkBoxMailEnable, resources.GetString("checkBoxMailEnable.ToolTip"));
            this.checkBoxMailEnable.UseVisualStyleBackColor = true;
            this.checkBoxMailEnable.CheckedChanged += new System.EventHandler(this.checkBoxMailEnable_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cog.png");
            this.imageList1.Images.SetKeyName(1, "color_swatch.png");
            this.imageList1.Images.SetKeyName(2, "funnel.png");
            this.imageList1.Images.SetKeyName(3, "email.png");
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.toolTip1.SetToolTip(this.tabPage4, resources.GetString("tabPage4.ToolTip"));
            // 
            // buttonExport
            // 
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.buttonExport.Image = global::LogLogViewer.Properties.Resources.disk_return_black;
            this.buttonExport.Name = "buttonExport";
            this.toolTip1.SetToolTip(this.buttonExport, resources.GetString("buttonExport.ToolTip"));
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonInport
            // 
            resources.ApplyResources(this.buttonInport, "buttonInport");
            this.buttonInport.Image = global::LogLogViewer.Properties.Resources.folder_open_document_text;
            this.buttonInport.Name = "buttonInport";
            this.toolTip1.SetToolTip(this.buttonInport, resources.GetString("buttonInport.ToolTip"));
            this.buttonInport.UseVisualStyleBackColor = true;
            this.buttonInport.Click += new System.EventHandler(this.buttonInport_Click);
            // 
            // SettingDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonInport);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.maxLineLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxReadSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backColorPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPic)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBoxPtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPtn)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBoxMail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown maxReadSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxLineLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonUTF16BE;
        private System.Windows.Forms.RadioButton radioButtonUTF16LE;
        private System.Windows.Forms.RadioButton radioButtonEucJP;
        private System.Windows.Forms.RadioButton radioButtonJIS;
        private System.Windows.Forms.RadioButton radioButtonUTF8;
        private System.Windows.Forms.RadioButton radioButtonShiftJIS;
        private System.Windows.Forms.CheckBox postScriptModeCheck;
        private System.Windows.Forms.RadioButton radioButtonAuto;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFontInfo;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox backColorPic;
        private System.Windows.Forms.PictureBox fontColorPic;
        private System.Windows.Forms.CheckBox checkBoxIsSaveHistory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.CheckBox checkBoxShowStatusBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxPtn;
        private System.Windows.Forms.DataGridView dataGridViewPtn;
        private System.Windows.Forms.Button buttonPtnDelete;
        private System.Windows.Forms.Button buttonPtnUp;
        private System.Windows.Forms.Button buttonPtnDown;
        private System.Windows.Forms.Button buttonPtnAdd;
        private System.Windows.Forms.CheckBox checkBoxPtnEnable;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.DataGridView dataGridViewFilter;
        private System.Windows.Forms.Button buttonFilterDelete;
        private System.Windows.Forms.Button buttonFilterUp;
        private System.Windows.Forms.Button buttonFilterDown;
        private System.Windows.Forms.Button buttonFilterAdd;
        private System.Windows.Forms.CheckBox checkBoxFilterEnable;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBoxMail;
        private System.Windows.Forms.DataGridView dataGridViewMail;
        private System.Windows.Forms.Button buttonMailDelete;
        private System.Windows.Forms.Button buttonMailUp;
        private System.Windows.Forms.Button buttonMailDown;
        private System.Windows.Forms.Button buttonMailAdd;
        private System.Windows.Forms.CheckBox checkBoxMailEnable;
        private System.Windows.Forms.Button buttonMailAccount;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonInport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox checkBoxIsUrlLink;
    }
}