namespace CHD806D2CPDemo
{
    partial class CHD806D2CP
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHD806D2CP));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("D7门状态监控     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("D6红外监控        √启用o禁用");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("D5第二感应头        √启用o禁用");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("D4第一感应头  √启用o禁用");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("D3开门状态时门磁开路     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("D2报警状态时红外开路  √启用o禁用");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("D1刷卡加密时段      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("D0紧急输入状态时门常关     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("控制字节1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("D7报警时报警继电器           √启用o禁用");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("D6手动按钮时报警继电器       √启用o禁用");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("D5第二头刷卡合法报警继电器        √启用o禁用");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("D4第一头刷卡合法报警继电器      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("D3无效卡刷卡报警继电器     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("D2手动按钮时开门继电器        √启用o禁用");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("D1第二头刷卡开门继电器    √启用o禁用");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("D0刷卡或按键报警继电器    √启用o禁用");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("控制字节2", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("D7网络正常由中心开门      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("D6N+1功能时段屏蔽          √启用o禁用");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("D5N+1功能[加特权卡确认]      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("D4双门不能同时开[互锁]     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("D3双卡确认开门      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("D2门锁关闭方式         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("D1多卡开门分组      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("D0DCU事件主动上报      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("控制字节3", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("D7多卡开门区分银行代码      √启用o禁用");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("D6保留         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("D5保留         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("D4保留         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("D3保留         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("D2首次N+1确认后单卡开门     √启用o禁用");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("D1首次N+1确认        √启用o禁用");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("D0三卡确认方式         √启用o禁用");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("控制字节4", new System.Windows.Forms.TreeNode[] {
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelInfo = new System.Windows.Forms.Button();
            this.txtNetId = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabpage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.button28 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button21 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button50 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dpLmtTime = new System.Windows.Forms.DateTimePicker();
            this.cmbDoor2Privilege = new System.Windows.Forms.ComboBox();
            this.cmbDoor1Privilege = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnAdduser = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.txtAddDoorPwd = new System.Windows.Forms.TextBox();
            this.txtAddUserID = new System.Windows.Forms.TextBox();
            this.txtAddCardNo = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.cmbDoorId_Ctr = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.txtRelayDelay = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtOpenDelay = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.txtIrSureDelay = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.txtIrOnDelay = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnSetOneByOne = new System.Windows.Forms.Button();
            this.btnReadAllCtrParam = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.txtRecovery = new System.Windows.Forms.TextBox();
            this.btnRecovery = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.button31 = new System.Windows.Forms.Button();
            this.btnQueryRecStatu = new System.Windows.Forms.Button();
            this.btnInitRec = new System.Windows.Forms.Button();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.btnReadNewRec = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.txtRecPos = new System.Windows.Forms.TextBox();
            this.btnReadRecByPos = new System.Windows.Forms.Button();
            this.label61 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.btnReadRec = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.btnReadRecByOrder = new System.Windows.Forms.Button();
            this.label39 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button51 = new System.Windows.Forms.Button();
            this.button52 = new System.Windows.Forms.Button();
            this.cmbDoorId_specia = new System.Windows.Forms.ComboBox();
            this.cmbDoorId_week = new System.Windows.Forms.ComboBox();
            this.label95 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.m_HolidayList = new System.Windows.Forms.DataGridView();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.m_WeekList = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button30 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.dgTimeSpan = new System.Windows.Forms.DataGridView();
            this.numbSpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start1spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end1spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start2spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end2spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start3spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end3spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start4spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end4spc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.button42 = new System.Windows.Forms.Button();
            this.button41 = new System.Windows.Forms.Button();
            this.button39 = new System.Windows.Forms.Button();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button40 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button49 = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.button37 = new System.Windows.Forms.Button();
            this.button36 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnReadState = new System.Windows.Forms.Button();
            this.btnReadVersion = new System.Windows.Forms.Button();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.btnReadTime = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button48 = new System.Windows.Forms.Button();
            this.btnAlwaysOpenDoorState = new System.Windows.Forms.Button();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.btnOpenAlwaysCloseDoor = new System.Windows.Forms.Button();
            this.btnCloseAlwaysCloseDoor = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.btnCloseAlwaysOpenDoor = new System.Windows.Forms.Button();
            this.label62 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.btnOpenAlwaysOpenDoor = new System.Windows.Forms.Button();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.button15 = new System.Windows.Forms.Button();
            this.label57 = new System.Windows.Forms.Label();
            this.button16 = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker7 = new System.Windows.Forms.DateTimePicker();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dateTimePicker8 = new System.Windows.Forms.DateTimePicker();
            this.button11 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabpage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox45.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_HolidayList)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_WeekList)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSpan)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelInfo);
            this.groupBox1.Controls.Add(this.txtNetId);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信";
            // 
            // btnDelInfo
            // 
            this.btnDelInfo.Location = new System.Drawing.Point(322, 20);
            this.btnDelInfo.Name = "btnDelInfo";
            this.btnDelInfo.Size = new System.Drawing.Size(75, 23);
            this.btnDelInfo.TabIndex = 0;
            this.btnDelInfo.Text = "删除信息";
            this.btnDelInfo.UseVisualStyleBackColor = true;
            this.btnDelInfo.Click += new System.EventHandler(this.btnDelInfo_Click);
            // 
            // txtNetId
            // 
            this.txtNetId.Location = new System.Drawing.Point(80, 21);
            this.txtNetId.Name = "txtNetId";
            this.txtNetId.Size = new System.Drawing.Size(144, 21);
            this.txtNetId.TabIndex = 1;
            this.txtNetId.Text = "1";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(19, 25);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(53, 12);
            this.label43.TabIndex = 0;
            this.label43.Text = "网络ID：";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "2.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 487);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 134);
            this.panel1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(873, 134);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "信息";
            this.Column1.MinimumWidth = 880;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 880;
            // 
            // tabpage
            // 
            this.tabpage.Controls.Add(this.tabPage1);
            this.tabpage.Controls.Add(this.tabPage2);
            this.tabpage.Controls.Add(this.tabPage3);
            this.tabpage.Controls.Add(this.tabPage6);
            this.tabpage.Controls.Add(this.tabPage4);
            this.tabpage.Controls.Add(this.tabPage5);
            this.tabpage.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabpage.Location = new System.Drawing.Point(0, 54);
            this.tabpage.Name = "tabpage";
            this.tabpage.SelectedIndex = 0;
            this.tabpage.Size = new System.Drawing.Size(873, 433);
            this.tabpage.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox3);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(865, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "卡/用户管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "月卡用户",
            "临时卡用户"});
            this.comboBox3.Location = new System.Drawing.Point(376, 180);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(98, 20);
            this.comboBox3.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox39);
            this.groupBox4.Controls.Add(this.textBox37);
            this.groupBox4.Controls.Add(this.textBox38);
            this.groupBox4.Controls.Add(this.button28);
            this.groupBox4.Controls.Add(this.button27);
            this.groupBox4.Controls.Add(this.button26);
            this.groupBox4.Controls.Add(this.button25);
            this.groupBox4.Controls.Add(this.button23);
            this.groupBox4.Controls.Add(this.textBox34);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.textBox36);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.textBox35);
            this.groupBox4.Controls.Add(this.textBox33);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.textBox32);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Location = new System.Drawing.Point(248, 184);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(465, 202);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "紧急开门密码/特权卡";
            // 
            // textBox39
            // 
            this.textBox39.Location = new System.Drawing.Point(292, 121);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(124, 21);
            this.textBox39.TabIndex = 6;
            this.textBox39.Text = "09002DAF9F";
            // 
            // textBox37
            // 
            this.textBox37.Location = new System.Drawing.Point(292, 90);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(124, 21);
            this.textBox37.TabIndex = 6;
            this.textBox37.Text = "09009F2DAE";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(82, 121);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(124, 21);
            this.textBox38.TabIndex = 6;
            this.textBox38.Text = "FFFFFFFF";
            // 
            // button28
            // 
            this.button28.Location = new System.Drawing.Point(339, 175);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(75, 23);
            this.button28.TabIndex = 9;
            this.button28.Text = "删除特权卡";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(339, 148);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(75, 23);
            this.button27.TabIndex = 9;
            this.button27.Text = "设置特权卡";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // button26
            // 
            this.button26.Location = new System.Drawing.Point(238, 148);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(75, 23);
            this.button26.TabIndex = 9;
            this.button26.Text = "读取特权卡";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(137, 148);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(75, 23);
            this.button25.TabIndex = 9;
            this.button25.Text = "设置密码";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(36, 148);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(75, 23);
            this.button23.TabIndex = 9;
            this.button23.Text = "读取密码";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // textBox34
            // 
            this.textBox34.Location = new System.Drawing.Point(82, 90);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(124, 21);
            this.textBox34.TabIndex = 6;
            this.textBox34.Text = "FFFFFFFF";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(258, 31);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(29, 12);
            this.label27.TabIndex = 5;
            this.label27.Text = "卡1:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(258, 127);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 12);
            this.label29.TabIndex = 3;
            this.label29.Text = "卡4:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(258, 95);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 3;
            this.label26.Text = "卡3:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(34, 31);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 5;
            this.label24.Text = "密码1:";
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(292, 59);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(124, 21);
            this.textBox36.TabIndex = 7;
            this.textBox36.Text = "09002DAF9F";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(34, 127);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 3;
            this.label28.Text = "密码4:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(34, 95);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 3;
            this.label22.Text = "密码3:";
            // 
            // textBox35
            // 
            this.textBox35.Location = new System.Drawing.Point(292, 28);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(124, 21);
            this.textBox35.TabIndex = 8;
            this.textBox35.Text = "09009F2DAE";
            // 
            // textBox33
            // 
            this.textBox33.Location = new System.Drawing.Point(82, 59);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(124, 21);
            this.textBox33.TabIndex = 7;
            this.textBox33.Text = "FFFFFFFF";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(258, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "卡2:";
            // 
            // textBox32
            // 
            this.textBox32.Location = new System.Drawing.Point(82, 28);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(124, 21);
            this.textBox32.TabIndex = 8;
            this.textBox32.Text = "FFFFFFFF";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(34, 63);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 4;
            this.label23.Text = "密码2:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button21);
            this.groupBox3.Controls.Add(this.button20);
            this.groupBox3.Controls.Add(this.button19);
            this.groupBox3.Controls.Add(this.button50);
            this.groupBox3.Controls.Add(this.button22);
            this.groupBox3.Controls.Add(this.button18);
            this.groupBox3.Controls.Add(this.textBox40);
            this.groupBox3.Controls.Add(this.textBox31);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.textBox30);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.textBox26);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Location = new System.Drawing.Point(248, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(465, 153);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(217, 103);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(75, 23);
            this.button21.TabIndex = 9;
            this.button21.Text = "读取计数";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(217, 74);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 9;
            this.button20.Text = "读取用户";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(217, 45);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 9;
            this.button19.Text = "读取用户";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button50
            // 
            this.button50.Location = new System.Drawing.Point(330, 103);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(75, 23);
            this.button50.TabIndex = 9;
            this.button50.Text = "清空用户";
            this.button50.UseVisualStyleBackColor = true;
            this.button50.Click += new System.EventHandler(this.button50_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(330, 16);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(75, 23);
            this.button22.TabIndex = 9;
            this.button22.Text = "删除用户";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(217, 16);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 9;
            this.button18.Text = "读取用户";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // textBox40
            // 
            this.textBox40.Enabled = false;
            this.textBox40.Location = new System.Drawing.Point(82, 105);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(124, 21);
            this.textBox40.TabIndex = 6;
            this.textBox40.Text = "0";
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(82, 76);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(124, 21);
            this.textBox31.TabIndex = 6;
            this.textBox31.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(42, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "卡号:";
            // 
            // textBox30
            // 
            this.textBox30.Location = new System.Drawing.Point(82, 47);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(124, 21);
            this.textBox30.TabIndex = 7;
            this.textBox30.Text = "00000001";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(30, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 12);
            this.label20.TabIndex = 4;
            this.label20.Text = "用户ID:";
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(82, 18);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(124, 21);
            this.textBox26.TabIndex = 8;
            this.textBox26.Text = "09009F2DAE";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(18, 111);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(59, 12);
            this.label32.TabIndex = 3;
            this.label32.Text = "用户计数:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 81);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 3;
            this.label21.Text = "储存位置:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dpLmtTime);
            this.groupBox2.Controls.Add(this.cmbDoor2Privilege);
            this.groupBox2.Controls.Add(this.cmbDoor1Privilege);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.btnAdduser);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.txtAddDoorPwd);
            this.groupBox2.Controls.Add(this.txtAddUserID);
            this.groupBox2.Controls.Add(this.txtAddCardNo);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(17, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 380);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // dpLmtTime
            // 
            this.dpLmtTime.Location = new System.Drawing.Point(79, 86);
            this.dpLmtTime.Name = "dpLmtTime";
            this.dpLmtTime.Size = new System.Drawing.Size(124, 21);
            this.dpLmtTime.TabIndex = 11;
            // 
            // cmbDoor2Privilege
            // 
            this.cmbDoor2Privilege.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoor2Privilege.FormattingEnabled = true;
            this.cmbDoor2Privilege.Items.AddRange(new object[] {
            "保持原有权限",
            "第二类卡",
            "第三类卡",
            "第四类卡",
            "第一类卡",
            "设置新权限",
            "特权卡",
            "无权限"});
            this.cmbDoor2Privilege.Location = new System.Drawing.Point(79, 178);
            this.cmbDoor2Privilege.Name = "cmbDoor2Privilege";
            this.cmbDoor2Privilege.Size = new System.Drawing.Size(124, 20);
            this.cmbDoor2Privilege.TabIndex = 10;
            // 
            // cmbDoor1Privilege
            // 
            this.cmbDoor1Privilege.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoor1Privilege.FormattingEnabled = true;
            this.cmbDoor1Privilege.Items.AddRange(new object[] {
            "第二类卡",
            "第三类卡",
            "第四类卡",
            "第一类卡",
            "特权卡"});
            this.cmbDoor1Privilege.Location = new System.Drawing.Point(79, 148);
            this.cmbDoor1Privilege.Name = "cmbDoor1Privilege";
            this.cmbDoor1Privilege.Size = new System.Drawing.Size(124, 20);
            this.cmbDoor1Privilege.TabIndex = 10;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(17, 183);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 9;
            this.label31.Text = "门2权限:";
            // 
            // btnAdduser
            // 
            this.btnAdduser.Location = new System.Drawing.Point(128, 209);
            this.btnAdduser.Name = "btnAdduser";
            this.btnAdduser.Size = new System.Drawing.Size(75, 23);
            this.btnAdduser.TabIndex = 9;
            this.btnAdduser.Text = "添加";
            this.btnAdduser.UseVisualStyleBackColor = true;
            this.btnAdduser.Click += new System.EventHandler(this.btnAdduser_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(17, 152);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 12);
            this.label30.TabIndex = 9;
            this.label30.Text = "门1权限:";
            // 
            // txtAddDoorPwd
            // 
            this.txtAddDoorPwd.Location = new System.Drawing.Point(79, 117);
            this.txtAddDoorPwd.Name = "txtAddDoorPwd";
            this.txtAddDoorPwd.Size = new System.Drawing.Size(124, 21);
            this.txtAddDoorPwd.TabIndex = 6;
            this.txtAddDoorPwd.Text = "0000";
            // 
            // txtAddUserID
            // 
            this.txtAddUserID.Location = new System.Drawing.Point(79, 55);
            this.txtAddUserID.Name = "txtAddUserID";
            this.txtAddUserID.Size = new System.Drawing.Size(124, 21);
            this.txtAddUserID.TabIndex = 7;
            this.txtAddUserID.Text = "00000001";
            // 
            // txtAddCardNo
            // 
            this.txtAddCardNo.Location = new System.Drawing.Point(79, 24);
            this.txtAddCardNo.Name = "txtAddCardNo";
            this.txtAddCardNo.Size = new System.Drawing.Size(124, 21);
            this.txtAddCardNo.TabIndex = 8;
            this.txtAddCardNo.Text = "09009F2DAE";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(23, 90);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(47, 12);
            this.label33.TabIndex = 3;
            this.label33.Text = "有效期:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 3;
            this.label19.Text = "开门密码:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "用户ID:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(35, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 5;
            this.label16.Text = "卡号:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox16);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(865, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "门禁参数设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.cmbDoorId_Ctr);
            this.groupBox16.Controls.Add(this.label65);
            this.groupBox16.Controls.Add(this.label64);
            this.groupBox16.Controls.Add(this.label63);
            this.groupBox16.Controls.Add(this.groupBox11);
            this.groupBox16.Controls.Add(this.treeView1);
            this.groupBox16.Controls.Add(this.btnSetOneByOne);
            this.groupBox16.Controls.Add(this.btnReadAllCtrParam);
            this.groupBox16.Location = new System.Drawing.Point(3, 5);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(665, 381);
            this.groupBox16.TabIndex = 1;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "控制器参数";
            // 
            // cmbDoorId_Ctr
            // 
            this.cmbDoorId_Ctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoorId_Ctr.FormattingEnabled = true;
            this.cmbDoorId_Ctr.Items.AddRange(new object[] {
            "1",
            "2",
            "0xFF"});
            this.cmbDoorId_Ctr.Location = new System.Drawing.Point(517, 271);
            this.cmbDoorId_Ctr.Name = "cmbDoorId_Ctr";
            this.cmbDoorId_Ctr.Size = new System.Drawing.Size(143, 20);
            this.cmbDoorId_Ctr.TabIndex = 12;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(44, 356);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(173, 12);
            this.label65.TabIndex = 11;
            this.label65.Text = "4个控制字,请仔细查看SDK文档!";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(12, 337);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(371, 12);
            this.label64.TabIndex = 11;
            this.label64.Text = "注意:不同设备控制字节的含义可能会不一致，并且有些设备可能没有";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(470, 275);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(41, 12);
            this.label63.TabIndex = 11;
            this.label63.Text = "门号：";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.txtRelayDelay);
            this.groupBox11.Controls.Add(this.label73);
            this.groupBox11.Controls.Add(this.txtOpenDelay);
            this.groupBox11.Controls.Add(this.label72);
            this.groupBox11.Controls.Add(this.label66);
            this.groupBox11.Controls.Add(this.txtIrSureDelay);
            this.groupBox11.Controls.Add(this.label67);
            this.groupBox11.Controls.Add(this.label71);
            this.groupBox11.Controls.Add(this.label68);
            this.groupBox11.Controls.Add(this.txtIrOnDelay);
            this.groupBox11.Controls.Add(this.label69);
            this.groupBox11.Controls.Add(this.label70);
            this.groupBox11.Location = new System.Drawing.Point(460, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(200, 251);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            // 
            // txtRelayDelay
            // 
            this.txtRelayDelay.Location = new System.Drawing.Point(24, 41);
            this.txtRelayDelay.Name = "txtRelayDelay";
            this.txtRelayDelay.Size = new System.Drawing.Size(40, 21);
            this.txtRelayDelay.TabIndex = 0;
            this.txtRelayDelay.Text = "3";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(23, 23);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(113, 12);
            this.label73.TabIndex = 1;
            this.label73.Text = "门锁继电器执行时间";
            // 
            // txtOpenDelay
            // 
            this.txtOpenDelay.Location = new System.Drawing.Point(24, 100);
            this.txtOpenDelay.Name = "txtOpenDelay";
            this.txtOpenDelay.Size = new System.Drawing.Size(40, 21);
            this.txtOpenDelay.TabIndex = 0;
            this.txtOpenDelay.Text = "3";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(23, 82);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(113, 12);
            this.label72.TabIndex = 1;
            this.label72.Text = "开门等待进入的时间";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(66, 212);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(71, 12);
            this.label66.TabIndex = 1;
            this.label66.Text = "秒(0-255秒)";
            // 
            // txtIrSureDelay
            // 
            this.txtIrSureDelay.Location = new System.Drawing.Point(24, 153);
            this.txtIrSureDelay.Name = "txtIrSureDelay";
            this.txtIrSureDelay.Size = new System.Drawing.Size(40, 21);
            this.txtIrSureDelay.TabIndex = 0;
            this.txtIrSureDelay.Text = "2";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(67, 157);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(71, 12);
            this.label67.TabIndex = 1;
            this.label67.Text = "秒(0-255秒)";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(23, 135);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(113, 12);
            this.label71.TabIndex = 1;
            this.label71.Text = "红外报警的确认时间";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(67, 104);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(71, 12);
            this.label68.TabIndex = 1;
            this.label68.Text = "秒(0-255秒)";
            // 
            // txtIrOnDelay
            // 
            this.txtIrOnDelay.Location = new System.Drawing.Point(23, 208);
            this.txtIrOnDelay.Name = "txtIrOnDelay";
            this.txtIrOnDelay.Size = new System.Drawing.Size(40, 21);
            this.txtIrOnDelay.TabIndex = 0;
            this.txtIrOnDelay.Text = "2";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(67, 45);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(95, 12);
            this.label69.TabIndex = 1;
            this.label69.Text = "0.1秒(0-25.5秒)";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(22, 190);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(113, 12);
            this.label70.TabIndex = 1;
            this.label70.Text = "安防报警的驱动时间";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(6, 20);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "d17";
            treeNode1.Text = "D7门状态监控     √启用o禁用";
            treeNode2.Name = "d16";
            treeNode2.Text = "D6红外监控        √启用o禁用";
            treeNode3.Name = "d15";
            treeNode3.Text = "D5第二感应头        √启用o禁用";
            treeNode4.Name = "d14";
            treeNode4.Text = "D4第一感应头  √启用o禁用";
            treeNode5.Name = "d13";
            treeNode5.Text = "D3开门状态时门磁开路     √启用o禁用";
            treeNode6.Name = "d12";
            treeNode6.Text = "D2报警状态时红外开路  √启用o禁用";
            treeNode7.Name = "d11";
            treeNode7.Text = "D1刷卡加密时段      √启用o禁用";
            treeNode8.Name = "d10";
            treeNode8.Text = "D0紧急输入状态时门常关     √启用o禁用";
            treeNode9.Name = "ctrParam1";
            treeNode9.Tag = "控制字节1";
            treeNode9.Text = "控制字节1";
            treeNode10.Name = "D27";
            treeNode10.Text = "D7报警时报警继电器           √启用o禁用";
            treeNode11.Name = "D26";
            treeNode11.Text = "D6手动按钮时报警继电器       √启用o禁用";
            treeNode12.Name = "D25";
            treeNode12.Text = "D5第二头刷卡合法报警继电器        √启用o禁用";
            treeNode13.Name = "D24";
            treeNode13.Text = "D4第一头刷卡合法报警继电器      √启用o禁用";
            treeNode14.Name = "D23";
            treeNode14.Text = "D3无效卡刷卡报警继电器     √启用o禁用";
            treeNode15.Name = "D22";
            treeNode15.Text = "D2手动按钮时开门继电器        √启用o禁用";
            treeNode16.Name = "D21";
            treeNode16.Text = "D1第二头刷卡开门继电器    √启用o禁用";
            treeNode17.Name = "D20";
            treeNode17.Text = "D0刷卡或按键报警继电器    √启用o禁用";
            treeNode18.Name = "ctrParam2";
            treeNode18.Tag = "控制字节2";
            treeNode18.Text = "控制字节2";
            treeNode19.Name = "节点2";
            treeNode19.Text = "D7网络正常由中心开门      √启用o禁用";
            treeNode20.Name = "节点3";
            treeNode20.Text = "D6N+1功能时段屏蔽          √启用o禁用";
            treeNode21.Name = "节点4";
            treeNode21.Text = "D5N+1功能[加特权卡确认]      √启用o禁用";
            treeNode22.Name = "节点5";
            treeNode22.Text = "D4双门不能同时开[互锁]     √启用o禁用";
            treeNode23.Name = "节点6";
            treeNode23.Text = "D3双卡确认开门      √启用o禁用";
            treeNode24.Name = "节点7";
            treeNode24.Text = "D2门锁关闭方式         √启用o禁用";
            treeNode25.Name = "节点8";
            treeNode25.Text = "D1多卡开门分组      √启用o禁用";
            treeNode26.Name = "节点9";
            treeNode26.Text = "D0DCU事件主动上报      √启用o禁用";
            treeNode27.Name = "节点0";
            treeNode27.Text = "控制字节3";
            treeNode28.Name = "节点10";
            treeNode28.Text = "D7多卡开门区分银行代码      √启用o禁用";
            treeNode29.Name = "节点11";
            treeNode29.Text = "D6保留         √启用o禁用";
            treeNode30.Name = "节点12";
            treeNode30.Text = "D5保留         √启用o禁用";
            treeNode31.Name = "节点13";
            treeNode31.Text = "D4保留         √启用o禁用";
            treeNode32.Name = "节点14";
            treeNode32.Text = "D3保留         √启用o禁用";
            treeNode33.Name = "节点15";
            treeNode33.Text = "D2首次N+1确认后单卡开门     √启用o禁用";
            treeNode34.Name = "节点16";
            treeNode34.Text = "D1首次N+1确认        √启用o禁用";
            treeNode35.Name = "节点17";
            treeNode35.Text = "D0三卡确认方式         √启用o禁用";
            treeNode36.Name = "节点1";
            treeNode36.Text = "控制字节4";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode18,
            treeNode27,
            treeNode36});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(446, 305);
            this.treeView1.TabIndex = 2;
            this.treeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCheck);
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // btnSetOneByOne
            // 
            this.btnSetOneByOne.Location = new System.Drawing.Point(584, 305);
            this.btnSetOneByOne.Name = "btnSetOneByOne";
            this.btnSetOneByOne.Size = new System.Drawing.Size(75, 23);
            this.btnSetOneByOne.TabIndex = 0;
            this.btnSetOneByOne.Text = "设置";
            this.btnSetOneByOne.UseVisualStyleBackColor = true;
            this.btnSetOneByOne.Click += new System.EventHandler(this.btnSetOneByOne_Click);
            // 
            // btnReadAllCtrParam
            // 
            this.btnReadAllCtrParam.Location = new System.Drawing.Point(462, 305);
            this.btnReadAllCtrParam.Name = "btnReadAllCtrParam";
            this.btnReadAllCtrParam.Size = new System.Drawing.Size(75, 23);
            this.btnReadAllCtrParam.TabIndex = 0;
            this.btnReadAllCtrParam.Text = "读取";
            this.btnReadAllCtrParam.UseVisualStyleBackColor = true;
            this.btnReadAllCtrParam.Click += new System.EventHandler(this.btnReadAllCtrParam_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox39);
            this.tabPage3.Controls.Add(this.groupBox38);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(865, 407);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "记录管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.groupBox41);
            this.groupBox39.Controls.Add(this.groupBox40);
            this.groupBox39.Location = new System.Drawing.Point(7, 280);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(620, 86);
            this.groupBox39.TabIndex = 1;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "记录操作";
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.txtRecovery);
            this.groupBox41.Controls.Add(this.btnRecovery);
            this.groupBox41.Controls.Add(this.label60);
            this.groupBox41.Location = new System.Drawing.Point(359, 18);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Size = new System.Drawing.Size(236, 60);
            this.groupBox41.TabIndex = 0;
            this.groupBox41.TabStop = false;
            this.groupBox41.Text = "记录恢复";
            // 
            // txtRecovery
            // 
            this.txtRecovery.Enabled = false;
            this.txtRecovery.Location = new System.Drawing.Point(71, 27);
            this.txtRecovery.Name = "txtRecovery";
            this.txtRecovery.Size = new System.Drawing.Size(63, 21);
            this.txtRecovery.TabIndex = 2;
            this.txtRecovery.Text = "0";
            // 
            // btnRecovery
            // 
            this.btnRecovery.Enabled = false;
            this.btnRecovery.Location = new System.Drawing.Point(140, 25);
            this.btnRecovery.Name = "btnRecovery";
            this.btnRecovery.Size = new System.Drawing.Size(75, 23);
            this.btnRecovery.TabIndex = 1;
            this.btnRecovery.Text = "恢复";
            this.btnRecovery.UseVisualStyleBackColor = true;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(7, 32);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(65, 12);
            this.label60.TabIndex = 0;
            this.label60.Text = "恢复记录数";
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.button31);
            this.groupBox40.Controls.Add(this.btnQueryRecStatu);
            this.groupBox40.Controls.Add(this.btnInitRec);
            this.groupBox40.Location = new System.Drawing.Point(9, 18);
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.Size = new System.Drawing.Size(333, 60);
            this.groupBox40.TabIndex = 0;
            this.groupBox40.TabStop = false;
            // 
            // button31
            // 
            this.button31.Location = new System.Drawing.Point(217, 20);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(114, 23);
            this.button31.TabIndex = 1;
            this.button31.Text = "当前设备记录总数";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // btnQueryRecStatu
            // 
            this.btnQueryRecStatu.Location = new System.Drawing.Point(97, 20);
            this.btnQueryRecStatu.Name = "btnQueryRecStatu";
            this.btnQueryRecStatu.Size = new System.Drawing.Size(103, 23);
            this.btnQueryRecStatu.TabIndex = 1;
            this.btnQueryRecStatu.Text = "设备记录区状态";
            this.btnQueryRecStatu.UseVisualStyleBackColor = true;
            this.btnQueryRecStatu.Click += new System.EventHandler(this.btnQueryRecStatu_Click);
            // 
            // btnInitRec
            // 
            this.btnInitRec.Location = new System.Drawing.Point(4, 20);
            this.btnInitRec.Name = "btnInitRec";
            this.btnInitRec.Size = new System.Drawing.Size(76, 23);
            this.btnInitRec.TabIndex = 1;
            this.btnInitRec.Text = "初始化记录";
            this.btnInitRec.UseVisualStyleBackColor = true;
            this.btnInitRec.Click += new System.EventHandler(this.btnInitRec_Click);
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.groupBox45);
            this.groupBox38.Controls.Add(this.groupBox44);
            this.groupBox38.Controls.Add(this.groupBox43);
            this.groupBox38.Controls.Add(this.groupBox42);
            this.groupBox38.Location = new System.Drawing.Point(7, 13);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(620, 252);
            this.groupBox38.TabIndex = 2;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "记录读取";
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.btnReadNewRec);
            this.groupBox45.Controls.Add(this.label59);
            this.groupBox45.Location = new System.Drawing.Point(9, 191);
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.Size = new System.Drawing.Size(586, 46);
            this.groupBox45.TabIndex = 0;
            this.groupBox45.TabStop = false;
            // 
            // btnReadNewRec
            // 
            this.btnReadNewRec.Location = new System.Drawing.Point(490, 13);
            this.btnReadNewRec.Name = "btnReadNewRec";
            this.btnReadNewRec.Size = new System.Drawing.Size(75, 23);
            this.btnReadNewRec.TabIndex = 1;
            this.btnReadNewRec.Text = "读取";
            this.btnReadNewRec.UseVisualStyleBackColor = true;
            this.btnReadNewRec.Click += new System.EventHandler(this.btnReadNewRec_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(16, 17);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(257, 12);
            this.label59.TabIndex = 0;
            this.label59.Text = "读取设备最新的一条记录，不移动设备读取指针";
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.txtRecPos);
            this.groupBox44.Controls.Add(this.btnReadRecByPos);
            this.groupBox44.Controls.Add(this.label61);
            this.groupBox44.Controls.Add(this.label42);
            this.groupBox44.Location = new System.Drawing.Point(9, 130);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Size = new System.Drawing.Size(586, 55);
            this.groupBox44.TabIndex = 0;
            this.groupBox44.TabStop = false;
            // 
            // txtRecPos
            // 
            this.txtRecPos.Location = new System.Drawing.Point(368, 22);
            this.txtRecPos.Name = "txtRecPos";
            this.txtRecPos.Size = new System.Drawing.Size(100, 21);
            this.txtRecPos.TabIndex = 2;
            this.txtRecPos.Text = "0";
            // 
            // btnReadRecByPos
            // 
            this.btnReadRecByPos.Location = new System.Drawing.Point(490, 20);
            this.btnReadRecByPos.Name = "btnReadRecByPos";
            this.btnReadRecByPos.Size = new System.Drawing.Size(75, 23);
            this.btnReadRecByPos.TabIndex = 1;
            this.btnReadRecByPos.Text = "读取";
            this.btnReadRecByPos.UseVisualStyleBackColor = true;
            this.btnReadRecByPos.Click += new System.EventHandler(this.btnReadRecByPos_Click);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(308, 26);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(53, 12);
            this.label61.TabIndex = 0;
            this.label61.Text = "记录序号";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 26);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(269, 12);
            this.label42.TabIndex = 0;
            this.label42.Text = "指定记录序号读取一条记录，不移动设备读取指针";
            // 
            // groupBox43
            // 
            this.groupBox43.Controls.Add(this.btnReadRec);
            this.groupBox43.Controls.Add(this.label41);
            this.groupBox43.Controls.Add(this.label40);
            this.groupBox43.Location = new System.Drawing.Point(9, 71);
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.Size = new System.Drawing.Size(586, 54);
            this.groupBox43.TabIndex = 0;
            this.groupBox43.TabStop = false;
            // 
            // btnReadRec
            // 
            this.btnReadRec.Location = new System.Drawing.Point(490, 20);
            this.btnReadRec.Name = "btnReadRec";
            this.btnReadRec.Size = new System.Drawing.Size(75, 23);
            this.btnReadRec.TabIndex = 1;
            this.btnReadRec.Text = "读取";
            this.btnReadRec.UseVisualStyleBackColor = true;
            this.btnReadRec.Click += new System.EventHandler(this.btnReadRec_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(16, 33);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(317, 12);
            this.label41.TabIndex = 0;
            this.label41.Text = "提示：正常情况下序号是连续的！由此可判断记录是否丢失";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(16, 14);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(389, 12);
            this.label40.TabIndex = 0;
            this.label40.Text = "顺序读取记录同时返回记录序号，并将设备读取指针移动到下一个记录。";
            // 
            // groupBox42
            // 
            this.groupBox42.Controls.Add(this.btnReadRecByOrder);
            this.groupBox42.Controls.Add(this.label39);
            this.groupBox42.Location = new System.Drawing.Point(9, 20);
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.Size = new System.Drawing.Size(586, 45);
            this.groupBox42.TabIndex = 0;
            this.groupBox42.TabStop = false;
            // 
            // btnReadRecByOrder
            // 
            this.btnReadRecByOrder.Location = new System.Drawing.Point(490, 13);
            this.btnReadRecByOrder.Name = "btnReadRecByOrder";
            this.btnReadRecByOrder.Size = new System.Drawing.Size(75, 23);
            this.btnReadRecByOrder.TabIndex = 1;
            this.btnReadRecByOrder.Text = "读取";
            this.btnReadRecByOrder.UseVisualStyleBackColor = true;
            this.btnReadRecByOrder.Click += new System.EventHandler(this.btnReadRecByOrder_Click);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(16, 19);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(281, 12);
            this.label39.TabIndex = 0;
            this.label39.Text = "顺序读取记录，并将设备读取指针移动到下一个记录";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button51);
            this.tabPage6.Controls.Add(this.button52);
            this.tabPage6.Controls.Add(this.cmbDoorId_specia);
            this.tabPage6.Controls.Add(this.cmbDoorId_week);
            this.tabPage6.Controls.Add(this.label95);
            this.tabPage6.Controls.Add(this.label94);
            this.tabPage6.Controls.Add(this.groupBox13);
            this.tabPage6.Controls.Add(this.groupBox12);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(865, 407);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "门禁时段设置";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button51
            // 
            this.button51.Location = new System.Drawing.Point(664, 374);
            this.button51.Name = "button51";
            this.button51.Size = new System.Drawing.Size(80, 23);
            this.button51.TabIndex = 8;
            this.button51.Text = "设置时段";
            this.button51.UseVisualStyleBackColor = true;
            this.button51.Click += new System.EventHandler(this.button51_Click);
            // 
            // button52
            // 
            this.button52.Location = new System.Drawing.Point(557, 374);
            this.button52.Name = "button52";
            this.button52.Size = new System.Drawing.Size(80, 23);
            this.button52.TabIndex = 9;
            this.button52.Text = "读取时段";
            this.button52.UseVisualStyleBackColor = true;
            this.button52.Click += new System.EventHandler(this.button52_Click);
            // 
            // cmbDoorId_specia
            // 
            this.cmbDoorId_specia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoorId_specia.FormattingEnabled = true;
            this.cmbDoorId_specia.Items.AddRange(new object[] {
            "1门",
            "2门",
            "全部设置"});
            this.cmbDoorId_specia.Location = new System.Drawing.Point(415, 375);
            this.cmbDoorId_specia.Name = "cmbDoorId_specia";
            this.cmbDoorId_specia.Size = new System.Drawing.Size(121, 20);
            this.cmbDoorId_specia.TabIndex = 6;
            // 
            // cmbDoorId_week
            // 
            this.cmbDoorId_week.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoorId_week.FormattingEnabled = true;
            this.cmbDoorId_week.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbDoorId_week.Location = new System.Drawing.Point(255, 375);
            this.cmbDoorId_week.Name = "cmbDoorId_week";
            this.cmbDoorId_week.Size = new System.Drawing.Size(121, 20);
            this.cmbDoorId_week.TabIndex = 7;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(386, 379);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(29, 12);
            this.label95.TabIndex = 4;
            this.label95.Text = "门号";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(221, 379);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(29, 12);
            this.label94.TabIndex = 5;
            this.label94.Text = "月份";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.m_HolidayList);
            this.groupBox13.Location = new System.Drawing.Point(8, 193);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(742, 165);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "工作日/非工作日时段";
            // 
            // m_HolidayList
            // 
            this.m_HolidayList.AllowUserToAddRows = false;
            this.m_HolidayList.AllowUserToDeleteRows = false;
            this.m_HolidayList.AllowUserToResizeColumns = false;
            this.m_HolidayList.AllowUserToResizeRows = false;
            this.m_HolidayList.BackgroundColor = System.Drawing.Color.White;
            this.m_HolidayList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_HolidayList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20});
            this.m_HolidayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_HolidayList.Location = new System.Drawing.Point(3, 17);
            this.m_HolidayList.Name = "m_HolidayList";
            this.m_HolidayList.RowHeadersVisible = false;
            this.m_HolidayList.RowTemplate.Height = 23;
            this.m_HolidayList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_HolidayList.Size = new System.Drawing.Size(736, 145);
            this.m_HolidayList.TabIndex = 0;
            this.m_HolidayList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_HolidayList_MouseClick);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.m_WeekList);
            this.groupBox12.Location = new System.Drawing.Point(8, 12);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(742, 165);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "星期时段";
            // 
            // m_WeekList
            // 
            this.m_WeekList.AllowDrop = true;
            this.m_WeekList.AllowUserToAddRows = false;
            this.m_WeekList.AllowUserToDeleteRows = false;
            this.m_WeekList.AllowUserToOrderColumns = true;
            this.m_WeekList.AllowUserToResizeColumns = false;
            this.m_WeekList.AllowUserToResizeRows = false;
            this.m_WeekList.BackgroundColor = System.Drawing.Color.White;
            this.m_WeekList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_WeekList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.m_WeekList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_WeekList.Location = new System.Drawing.Point(3, 17);
            this.m_WeekList.Name = "m_WeekList";
            this.m_WeekList.RowHeadersVisible = false;
            this.m_WeekList.RowTemplate.Height = 23;
            this.m_WeekList.Size = new System.Drawing.Size(736, 145);
            this.m_WeekList.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button30);
            this.tabPage4.Controls.Add(this.button29);
            this.tabPage4.Controls.Add(this.dgTimeSpan);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(865, 407);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "时间段设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button30
            // 
            this.button30.Location = new System.Drawing.Point(764, 269);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(75, 23);
            this.button30.TabIndex = 10;
            this.button30.Text = "设置";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // button29
            // 
            this.button29.Location = new System.Drawing.Point(764, 59);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(75, 23);
            this.button29.TabIndex = 10;
            this.button29.Text = "读取";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // dgTimeSpan
            // 
            this.dgTimeSpan.AllowUserToAddRows = false;
            this.dgTimeSpan.AllowUserToOrderColumns = true;
            this.dgTimeSpan.AllowUserToResizeColumns = false;
            this.dgTimeSpan.AllowUserToResizeRows = false;
            this.dgTimeSpan.BackgroundColor = System.Drawing.Color.White;
            this.dgTimeSpan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeSpan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numbSpc,
            this.start1spc,
            this.end1spc,
            this.start2spc,
            this.end2spc,
            this.start3spc,
            this.end3spc,
            this.start4spc,
            this.end4spc});
            this.dgTimeSpan.Location = new System.Drawing.Point(3, 4);
            this.dgTimeSpan.MultiSelect = false;
            this.dgTimeSpan.Name = "dgTimeSpan";
            this.dgTimeSpan.RowHeadersVisible = false;
            this.dgTimeSpan.RowTemplate.Height = 23;
            this.dgTimeSpan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTimeSpan.Size = new System.Drawing.Size(755, 400);
            this.dgTimeSpan.TabIndex = 1;
            // 
            // numbSpc
            // 
            this.numbSpc.HeaderText = "序号";
            this.numbSpc.Name = "numbSpc";
            this.numbSpc.ReadOnly = true;
            this.numbSpc.Width = 55;
            // 
            // start1spc
            // 
            this.start1spc.HeaderText = "起始1";
            this.start1spc.Name = "start1spc";
            this.start1spc.Width = 84;
            // 
            // end1spc
            // 
            this.end1spc.HeaderText = "结束1";
            this.end1spc.Name = "end1spc";
            this.end1spc.Width = 84;
            // 
            // start2spc
            // 
            this.start2spc.HeaderText = "起始2";
            this.start2spc.Name = "start2spc";
            this.start2spc.Width = 84;
            // 
            // end2spc
            // 
            this.end2spc.HeaderText = "结束2";
            this.end2spc.Name = "end2spc";
            this.end2spc.Width = 84;
            // 
            // start3spc
            // 
            this.start3spc.HeaderText = "起始3";
            this.start3spc.Name = "start3spc";
            this.start3spc.Width = 84;
            // 
            // end3spc
            // 
            this.end3spc.HeaderText = "结束3";
            this.end3spc.Name = "end3spc";
            this.end3spc.Width = 84;
            // 
            // start4spc
            // 
            this.start4spc.HeaderText = "起始4";
            this.start4spc.Name = "start4spc";
            this.start4spc.Width = 84;
            // 
            // end4spc
            // 
            this.end4spc.HeaderText = "结束4";
            this.end4spc.Name = "end4spc";
            this.end4spc.Width = 84;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox10);
            this.tabPage5.Controls.Add(this.groupBox9);
            this.tabPage5.Controls.Add(this.groupBox8);
            this.tabPage5.Controls.Add(this.groupBox7);
            this.tabPage5.Controls.Add(this.groupBox6);
            this.tabPage5.Controls.Add(this.groupBox5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(865, 407);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "其他";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.button42);
            this.groupBox10.Controls.Add(this.button41);
            this.groupBox10.Controls.Add(this.button39);
            this.groupBox10.Controls.Add(this.textBox41);
            this.groupBox10.Controls.Add(this.textBox42);
            this.groupBox10.Controls.Add(this.label34);
            this.groupBox10.Controls.Add(this.label35);
            this.groupBox10.Location = new System.Drawing.Point(489, 111);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(237, 290);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "密码设置";
            // 
            // button42
            // 
            this.button42.Location = new System.Drawing.Point(68, 181);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(85, 23);
            this.button42.TabIndex = 0;
            this.button42.Text = "取消权限";
            this.button42.UseVisualStyleBackColor = true;
            this.button42.Click += new System.EventHandler(this.button42_Click);
            // 
            // button41
            // 
            this.button41.Location = new System.Drawing.Point(68, 131);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(85, 23);
            this.button41.TabIndex = 0;
            this.button41.Text = "更改访问密码";
            this.button41.UseVisualStyleBackColor = true;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // button39
            // 
            this.button39.Location = new System.Drawing.Point(68, 81);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(85, 23);
            this.button39.TabIndex = 0;
            this.button39.Text = "确认权限";
            this.button39.UseVisualStyleBackColor = true;
            this.button39.Click += new System.EventHandler(this.button39_Click);
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(68, 54);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(124, 21);
            this.textBox41.TabIndex = 11;
            this.textBox41.Text = "000000";
            // 
            // textBox42
            // 
            this.textBox42.Location = new System.Drawing.Point(68, 23);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(124, 21);
            this.textBox42.TabIndex = 12;
            this.textBox42.Text = "0000";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 59);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(53, 12);
            this.label34.TabIndex = 9;
            this.label34.Text = "键盘密码";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(10, 28);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 12);
            this.label35.TabIndex = 10;
            this.label35.Text = "系统密码";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button40);
            this.groupBox9.Location = new System.Drawing.Point(7, 339);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(476, 62);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "胁迫报警";
            // 
            // button40
            // 
            this.button40.Location = new System.Drawing.Point(233, 20);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(75, 23);
            this.button40.TabIndex = 0;
            this.button40.Text = "解除报警";
            this.button40.UseVisualStyleBackColor = true;
            this.button40.Click += new System.EventHandler(this.button40_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button49);
            this.groupBox8.Controls.Add(this.button38);
            this.groupBox8.Location = new System.Drawing.Point(7, 263);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(476, 70);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "报警继电器";
            // 
            // button49
            // 
            this.button49.Location = new System.Drawing.Point(357, 29);
            this.button49.Name = "button49";
            this.button49.Size = new System.Drawing.Size(75, 23);
            this.button49.TabIndex = 0;
            this.button49.Text = "关闭";
            this.button49.UseVisualStyleBackColor = true;
            this.button49.Click += new System.EventHandler(this.button49_Click);
            // 
            // button38
            // 
            this.button38.Location = new System.Drawing.Point(233, 29);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(75, 23);
            this.button38.TabIndex = 0;
            this.button38.Text = "开启";
            this.button38.UseVisualStyleBackColor = true;
            this.button38.Click += new System.EventHandler(this.button38_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label37);
            this.groupBox7.Controls.Add(this.textBox44);
            this.groupBox7.Controls.Add(this.button37);
            this.groupBox7.Controls.Add(this.button36);
            this.groupBox7.Location = new System.Drawing.Point(7, 187);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(476, 70);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "主动上传通道";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(14, 34);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(47, 12);
            this.label37.TabIndex = 9;
            this.label37.Text = "用户ID:";
            // 
            // textBox44
            // 
            this.textBox44.Location = new System.Drawing.Point(70, 31);
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new System.Drawing.Size(124, 21);
            this.textBox44.TabIndex = 11;
            // 
            // button37
            // 
            this.button37.Location = new System.Drawing.Point(357, 29);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(75, 23);
            this.button37.TabIndex = 0;
            this.button37.Text = "设置";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // button36
            // 
            this.button36.Location = new System.Drawing.Point(233, 29);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(75, 23);
            this.button36.TabIndex = 0;
            this.button36.Text = "读取";
            this.button36.UseVisualStyleBackColor = true;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnReadState);
            this.groupBox6.Controls.Add(this.btnReadVersion);
            this.groupBox6.Controls.Add(this.btnSetTime);
            this.groupBox6.Controls.Add(this.btnReadTime);
            this.groupBox6.Location = new System.Drawing.Point(7, 111);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(476, 70);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            // 
            // btnReadState
            // 
            this.btnReadState.Location = new System.Drawing.Point(357, 27);
            this.btnReadState.Name = "btnReadState";
            this.btnReadState.Size = new System.Drawing.Size(75, 23);
            this.btnReadState.TabIndex = 0;
            this.btnReadState.Text = "监控状态";
            this.btnReadState.UseVisualStyleBackColor = true;
            this.btnReadState.Click += new System.EventHandler(this.btnReadState_Click);
            // 
            // btnReadVersion
            // 
            this.btnReadVersion.Location = new System.Drawing.Point(233, 27);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(75, 23);
            this.btnReadVersion.TabIndex = 0;
            this.btnReadVersion.Text = "设备版本";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.btnReadVersion_Click);
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(125, 28);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(75, 23);
            this.btnSetTime.TabIndex = 0;
            this.btnSetTime.Text = "同步时间";
            this.btnSetTime.UseVisualStyleBackColor = true;
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnReadTime
            // 
            this.btnReadTime.Location = new System.Drawing.Point(10, 27);
            this.btnReadTime.Name = "btnReadTime";
            this.btnReadTime.Size = new System.Drawing.Size(75, 23);
            this.btnReadTime.TabIndex = 0;
            this.btnReadTime.Text = "读取时间";
            this.btnReadTime.UseVisualStyleBackColor = true;
            this.btnReadTime.Click += new System.EventHandler(this.btnReadTime_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.button48);
            this.groupBox5.Controls.Add(this.btnAlwaysOpenDoorState);
            this.groupBox5.Controls.Add(this.comboBox5);
            this.groupBox5.Controls.Add(this.btnOpenAlwaysCloseDoor);
            this.groupBox5.Controls.Add(this.btnCloseAlwaysCloseDoor);
            this.groupBox5.Controls.Add(this.label38);
            this.groupBox5.Controls.Add(this.btnCloseAlwaysOpenDoor);
            this.groupBox5.Controls.Add(this.label62);
            this.groupBox5.Controls.Add(this.label36);
            this.groupBox5.Controls.Add(this.btnOpenAlwaysOpenDoor);
            this.groupBox5.Controls.Add(this.textBox45);
            this.groupBox5.Controls.Add(this.textBox43);
            this.groupBox5.Location = new System.Drawing.Point(7, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(719, 97);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "门操作";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(238, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "带操作员";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button48
            // 
            this.button48.Location = new System.Drawing.Point(604, 25);
            this.button48.Name = "button48";
            this.button48.Size = new System.Drawing.Size(75, 23);
            this.button48.TabIndex = 0;
            this.button48.Text = "远程开门";
            this.button48.UseVisualStyleBackColor = true;
            this.button48.Click += new System.EventHandler(this.button48_Click);
            // 
            // btnAlwaysOpenDoorState
            // 
            this.btnAlwaysOpenDoorState.Enabled = false;
            this.btnAlwaysOpenDoorState.Location = new System.Drawing.Point(604, 64);
            this.btnAlwaysOpenDoorState.Name = "btnAlwaysOpenDoorState";
            this.btnAlwaysOpenDoorState.Size = new System.Drawing.Size(75, 23);
            this.btnAlwaysOpenDoorState.TabIndex = 0;
            this.btnAlwaysOpenDoorState.Text = "常开门状态";
            this.btnAlwaysOpenDoorState.UseVisualStyleBackColor = true;
            this.btnAlwaysOpenDoorState.Click += new System.EventHandler(this.btnAlwaysOpenDoorState_Click);
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "1",
            "2",
            "FF"});
            this.comboBox5.Location = new System.Drawing.Point(76, 28);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(124, 20);
            this.comboBox5.TabIndex = 13;
            // 
            // btnOpenAlwaysCloseDoor
            // 
            this.btnOpenAlwaysCloseDoor.Enabled = false;
            this.btnOpenAlwaysCloseDoor.Location = new System.Drawing.Point(418, 64);
            this.btnOpenAlwaysCloseDoor.Name = "btnOpenAlwaysCloseDoor";
            this.btnOpenAlwaysCloseDoor.Size = new System.Drawing.Size(75, 23);
            this.btnOpenAlwaysCloseDoor.TabIndex = 0;
            this.btnOpenAlwaysCloseDoor.Text = "开启常闭门";
            this.btnOpenAlwaysCloseDoor.UseVisualStyleBackColor = true;
            this.btnOpenAlwaysCloseDoor.Click += new System.EventHandler(this.btnOpenAlwaysCloseDoor_Click);
            // 
            // btnCloseAlwaysCloseDoor
            // 
            this.btnCloseAlwaysCloseDoor.Enabled = false;
            this.btnCloseAlwaysCloseDoor.Location = new System.Drawing.Point(511, 64);
            this.btnCloseAlwaysCloseDoor.Name = "btnCloseAlwaysCloseDoor";
            this.btnCloseAlwaysCloseDoor.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAlwaysCloseDoor.TabIndex = 0;
            this.btnCloseAlwaysCloseDoor.Text = "取消常闭门";
            this.btnCloseAlwaysCloseDoor.UseVisualStyleBackColor = true;
            this.btnCloseAlwaysCloseDoor.Click += new System.EventHandler(this.btnCloseAlwaysCloseDoor_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(24, 32);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(47, 12);
            this.label38.TabIndex = 12;
            this.label38.Text = "选择门:";
            // 
            // btnCloseAlwaysOpenDoor
            // 
            this.btnCloseAlwaysOpenDoor.Enabled = false;
            this.btnCloseAlwaysOpenDoor.Location = new System.Drawing.Point(325, 64);
            this.btnCloseAlwaysOpenDoor.Name = "btnCloseAlwaysOpenDoor";
            this.btnCloseAlwaysOpenDoor.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAlwaysOpenDoor.TabIndex = 0;
            this.btnCloseAlwaysOpenDoor.Text = "取消常开门";
            this.btnCloseAlwaysOpenDoor.UseVisualStyleBackColor = true;
            this.btnCloseAlwaysOpenDoor.Click += new System.EventHandler(this.btnCloseAlwaysOpenDoor_Click);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(174, 69);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(29, 12);
            this.label62.TabIndex = 9;
            this.label62.Text = "分钟";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(36, 71);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(35, 12);
            this.label36.TabIndex = 9;
            this.label36.Text = "延时:";
            // 
            // btnOpenAlwaysOpenDoor
            // 
            this.btnOpenAlwaysOpenDoor.Enabled = false;
            this.btnOpenAlwaysOpenDoor.Location = new System.Drawing.Point(232, 64);
            this.btnOpenAlwaysOpenDoor.Name = "btnOpenAlwaysOpenDoor";
            this.btnOpenAlwaysOpenDoor.Size = new System.Drawing.Size(75, 23);
            this.btnOpenAlwaysOpenDoor.TabIndex = 0;
            this.btnOpenAlwaysOpenDoor.Text = "开启常开门";
            this.btnOpenAlwaysOpenDoor.UseVisualStyleBackColor = true;
            this.btnOpenAlwaysOpenDoor.Click += new System.EventHandler(this.btnOpenAlwaysOpenDoor_Click);
            // 
            // textBox45
            // 
            this.textBox45.Enabled = false;
            this.textBox45.Location = new System.Drawing.Point(312, 25);
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(124, 21);
            this.textBox45.TabIndex = 11;
            this.textBox45.Text = "002B83421B";
            // 
            // textBox43
            // 
            this.textBox43.Location = new System.Drawing.Point(76, 66);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(91, 21);
            this.textBox43.TabIndex = 11;
            this.textBox43.Text = "5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "设置时间";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "读取时间";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(14, 119);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "读取版本";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(17, 246);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 0;
            this.button17.Text = "增加";
            this.button17.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 1;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(15, 76);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(53, 12);
            this.label56.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 146);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 182);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 1;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(113, 146);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(47, 12);
            this.label44.TabIndex = 1;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(209, 146);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(47, 12);
            this.label47.TabIndex = 1;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(299, 146);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(47, 12);
            this.label50.TabIndex = 1;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(398, 146);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(47, 12);
            this.label53.TabIndex = 1;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(108, 182);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(53, 12);
            this.label45.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(206, 182);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(53, 12);
            this.label48.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(299, 182);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(53, 12);
            this.label51.TabIndex = 1;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(398, 182);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(53, 12);
            this.label54.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 220);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 1;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(109, 220);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(53, 12);
            this.label46.TabIndex = 1;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(206, 220);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(53, 12);
            this.label49.TabIndex = 1;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(299, 220);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 12);
            this.label52.TabIndex = 1;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(398, 220);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(53, 12);
            this.label55.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(179, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(339, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 1;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(66, 143);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(41, 21);
            this.textBox9.TabIndex = 1;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(66, 176);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(41, 21);
            this.textBox10.TabIndex = 1;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(162, 142);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(43, 21);
            this.textBox12.TabIndex = 1;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(162, 175);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(43, 21);
            this.textBox13.TabIndex = 1;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(258, 142);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(40, 21);
            this.textBox15.TabIndex = 1;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(258, 175);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(40, 21);
            this.textBox16.TabIndex = 1;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(353, 142);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(40, 21);
            this.textBox18.TabIndex = 1;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(353, 175);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(40, 21);
            this.textBox19.TabIndex = 1;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(453, 140);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(36, 21);
            this.textBox21.TabIndex = 1;
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(453, 173);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(36, 21);
            this.textBox22.TabIndex = 1;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(66, 214);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(41, 21);
            this.textBox11.TabIndex = 1;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(162, 213);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(43, 21);
            this.textBox14.TabIndex = 1;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(258, 213);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(40, 21);
            this.textBox17.TabIndex = 1;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(353, 213);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(40, 21);
            this.textBox20.TabIndex = 1;
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(453, 211);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(36, 21);
            this.textBox23.TabIndex = 1;
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(74, 30);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(92, 21);
            this.textBox24.TabIndex = 1;
            // 
            // textBox25
            // 
            this.textBox25.Location = new System.Drawing.Point(238, 30);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(92, 21);
            this.textBox25.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(399, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(88, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(74, 70);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(74, 101);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker3.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(186, 70);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker4.Location = new System.Drawing.Point(186, 101);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.ShowUpDown = true;
            this.dateTimePicker4.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker4.TabIndex = 3;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(161, 21);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 0;
            this.button15.Text = "注销";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(11, 28);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(53, 12);
            this.label57.TabIndex = 1;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(162, 63);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 0;
            this.button16.Text = "全部删除";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(64, 23);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(92, 21);
            this.textBox7.TabIndex = 1;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(64, 64);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(92, 21);
            this.textBox8.TabIndex = 1;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(11, 69);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(53, 12);
            this.label58.TabIndex = 1;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(166, 96);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(52, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "续费";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            // 
            // dateTimePicker5
            // 
            this.dateTimePicker5.Location = new System.Drawing.Point(65, 23);
            this.dateTimePicker5.Name = "dateTimePicker5";
            this.dateTimePicker5.Size = new System.Drawing.Size(72, 21);
            this.dateTimePicker5.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            // 
            // dateTimePicker6
            // 
            this.dateTimePicker6.Location = new System.Drawing.Point(66, 60);
            this.dateTimePicker6.Name = "dateTimePicker6";
            this.dateTimePicker6.Size = new System.Drawing.Size(71, 21);
            this.dateTimePicker6.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            // 
            // dateTimePicker7
            // 
            this.dateTimePicker7.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker7.Location = new System.Drawing.Point(143, 23);
            this.dateTimePicker7.Name = "dateTimePicker7";
            this.dateTimePicker7.ShowUpDown = true;
            this.dateTimePicker7.Size = new System.Drawing.Size(75, 21);
            this.dateTimePicker7.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(65, 98);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(92, 21);
            this.textBox3.TabIndex = 1;
            // 
            // dateTimePicker8
            // 
            this.dateTimePicker8.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker8.Location = new System.Drawing.Point(143, 59);
            this.dateTimePicker8.Name = "dateTimePicker8";
            this.dateTimePicker8.ShowUpDown = true;
            this.dateTimePicker8.Size = new System.Drawing.Size(75, 21);
            this.dateTimePicker8.TabIndex = 3;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(154, 24);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(42, 23);
            this.button11.TabIndex = 0;
            this.button11.Text = "增加";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(154, 64);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(42, 23);
            this.button13.TabIndex = 0;
            this.button13.Text = "挂失";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(199, 24);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(41, 23);
            this.button12.TabIndex = 0;
            this.button12.Text = "注销";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(199, 63);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(41, 23);
            this.button14.TabIndex = 0;
            this.button14.Text = "解挂";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 1;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(59, 25);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(92, 21);
            this.textBox5.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(59, 64);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(92, 21);
            this.textBox6.TabIndex = 1;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(143, 145);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "读取电梯状态";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(9, 145);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 0;
            this.button8.Text = "远程驱动";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(65, 33);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(92, 21);
            this.textBox4.TabIndex = 1;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(167, 24);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(52, 23);
            this.button10.TabIndex = 0;
            this.button10.Text = "续期";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(66, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(92, 21);
            this.textBox2.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(9, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "访问权限确认";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(9, 132);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(92, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "访问权限取消";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(9, 186);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 23);
            this.button6.TabIndex = 0;
            this.button6.Text = "修改密码";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(92, 21);
            this.textBox1.TabIndex = 1;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "星期";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 75;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "一类卡";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 75;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "二类卡";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 75;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "三类卡";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 75;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "四类卡";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "常开时段";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 75;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "卡+密码";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 75;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "自动布防";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 75;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "N+1屏蔽";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 75;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "月";
            this.Column11.Name = "Column11";
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.Width = 38;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "日";
            this.Column12.Name = "Column12";
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 38;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "一类卡";
            this.Column13.Name = "Column13";
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 75;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "二类卡";
            this.Column14.Name = "Column14";
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column14.Width = 75;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "三类卡";
            this.Column15.Name = "Column15";
            this.Column15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column15.Width = 75;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "四类卡";
            this.Column16.Name = "Column16";
            this.Column16.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column16.Width = 75;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "常开时段";
            this.Column17.Name = "Column17";
            this.Column17.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column17.Width = 75;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "卡+密码";
            this.Column18.Name = "Column18";
            this.Column18.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column18.Width = 75;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "自动布防";
            this.Column19.Name = "Column19";
            this.Column19.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column19.Width = 75;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "N+1屏蔽";
            this.Column20.Name = "Column20";
            this.Column20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column20.Width = 75;
            // 
            // CHD806D2CP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(873, 621);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabpage);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "CHD806D2CP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHD806D2CP_Demo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabpage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox39.ResumeLayout(false);
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox40.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.groupBox45.ResumeLayout(false);
            this.groupBox45.PerformLayout();
            this.groupBox44.ResumeLayout(false);
            this.groupBox44.PerformLayout();
            this.groupBox43.ResumeLayout(false);
            this.groupBox43.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_HolidayList)).EndInit();
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_WeekList)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSpan)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelInfo;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtNetId;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TabControl tabpage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtAddDoorPwd;
        private System.Windows.Forms.TextBox txtAddUserID;
        private System.Windows.Forms.TextBox txtAddCardNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DateTimePicker dpLmtTime;
        private System.Windows.Forms.ComboBox cmbDoor2Privilege;
        private System.Windows.Forms.ComboBox cmbDoor1Privilege;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnAdduser;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.DataGridView dgTimeSpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn numbSpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn start1spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn end1spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn start2spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn end2spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn start3spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn end3spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn start4spc;
        private System.Windows.Forms.DataGridViewTextBoxColumn end4spc;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.TextBox txtRecovery;
        private System.Windows.Forms.Button btnRecovery;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button btnQueryRecStatu;
        private System.Windows.Forms.Button btnInitRec;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.GroupBox groupBox45;
        private System.Windows.Forms.Button btnReadNewRec;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.GroupBox groupBox44;
        private System.Windows.Forms.TextBox txtRecPos;
        private System.Windows.Forms.Button btnReadRecByPos;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.GroupBox groupBox43;
        private System.Windows.Forms.Button btnReadRec;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.Button btnReadRecByOrder;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button button42;
        private System.Windows.Forms.Button button41;
        private System.Windows.Forms.Button button39;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button40;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button49;
        private System.Windows.Forms.Button button38;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBox44;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnReadState;
        private System.Windows.Forms.Button btnReadVersion;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.Button btnReadTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button48;
        private System.Windows.Forms.Button btnAlwaysOpenDoorState;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Button btnOpenAlwaysCloseDoor;
        private System.Windows.Forms.Button btnCloseAlwaysCloseDoor;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btnCloseAlwaysOpenDoor;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button btnOpenAlwaysOpenDoor;
        private System.Windows.Forms.TextBox textBox45;
        private System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox txtRelayDelay;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txtOpenDelay;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txtIrSureDelay;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox txtIrOnDelay;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnSetOneByOne;
        private System.Windows.Forms.Button btnReadAllCtrParam;
        private System.Windows.Forms.ComboBox cmbDoorId_Ctr;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.DataGridView m_HolidayList;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.DataGridView m_WeekList;
        private System.Windows.Forms.Button button50;
        private System.Windows.Forms.Button button51;
        private System.Windows.Forms.Button button52;
        private System.Windows.Forms.ComboBox cmbDoorId_specia;
        private System.Windows.Forms.ComboBox cmbDoorId_week;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
    }
}

