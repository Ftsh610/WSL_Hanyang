// Decompiled with JetBrains decompiler
// Type: ADI_PLL_Int_N.Main_Form
// Assembly: ADAR1000, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 01CA4812-C580-46A2-996B-A79D49742374
// Assembly location: C:\Program Files (x86)\Analog Devices\EV-ADAR1000\ADAR1000.exe

using sdpApi1;
using sdpApi1.Enum;
using sdpApi1.Exceptions;
using sdpApi1.Peripherals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace ADI_PLL_Int_N
{
    public class Main_Form : Form
    {
        private bool messageShownToUser = false;
        private uint[] TXGain = new uint[4];
        private uint[] RXGain = new uint[4];
        private uint[] RXPhase = new uint[8];
        private uint[] TXPhase = new uint[8];
        private uint[] BIAS_ON = new uint[5];
        private uint[] BIAS_OFF = new uint[5];
        private uint[] RX_Enable = new uint[1];
        private uint[] RX_Enable_2 = new uint[1];
        private uint[] TX_Enable = new uint[1];
        private uint[] TX_Enable_2 = new uint[1];
        private uint[] MISC_Enable = new uint[1];
        private uint[] SW_CTRL = new uint[1];
        private uint[] SW_CTRL_2 = new uint[1];
        private uint[] ADC_CTRL = new uint[1];
        private uint[] BIAS_CURRENT = new uint[4];
        private uint[] OVERRIDE = new uint[2];
        private uint[] MEMCTRL = new uint[1];
        private uint[] MEM_INDEX_RX = new uint[5];
        private uint[] MEM_INDEX_TX = new uint[5];
        private uint[] LDO_TRIMR = new uint[1];
        private uint[] LDO_TRIMS = new uint[1];
        private uint[] NVM_CTRL = new uint[4];
        private uint[] NVM_BYPASS = new uint[3];
        private uint[] Delay_Control = new uint[2];
        private uint[] BEAM_STEP = new uint[4];
        private uint[] BIAS_RAM_CNTRL = new uint[2];
        private uint[] GPIO = new uint[6];
        private uint[] toRead = new uint[1];
        private uint[] toWrite = new uint[1];
        private uint[] DEMO_RUN = new uint[80];
        private uint[] TX_init_seq = new uint[11];
        private uint[] RX_init_seq = new uint[11];
        private uint[] DemoPhase = new uint[4];
        private uint ADDR0 = 0;
        private uint ADDR1 = 0;
        private uint[] Read = new uint[90];
        private int Finish = 0;
        private Dictionary<string, Phase> tables = new Dictionary<string, Phase>();
        private Dictionary<string, Phase> tables2 = new Dictionary<string, Phase>();
        private Dictionary<string, uint> Gtables = new Dictionary<string, uint>();
        private Dictionary<string, uint> Gtables2 = new Dictionary<string, uint>();
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker ADIworker = new BackgroundWorker();
        private uint[] RX1Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      12159U,
      13846U,
      14086U,
      12610U,
      7423U,
      8246U,
      8502U,
      10242U,
      10240U
        };
        private uint[] RX2Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      12159U,
      13846U,
      14086U,
      12610U,
      7679U,
      8758U,
      9014U,
      10242U,
      10240U
        };
        private uint[] RX3Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      12159U,
      13846U,
      14086U,
      12610U,
      7935U,
      9270U,
      9526U,
      10242U,
      10240U
        };
        private uint[] RX4Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      12159U,
      13846U,
      14086U,
      12610U,
      8191U,
      9782U,
      10038U,
      10242U,
      10240U
        };
        private uint[] TX1Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      11903U,
      13320U,
      13590U,
      12576U,
      4351U,
      5174U,
      5414U,
      10241U,
      10240U
        };
        private uint[] TX2Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      11903U,
      13320U,
      13590U,
      12576U,
      4607U,
      5686U,
      5926U,
      10241U,
      10240U
        };
        private uint[] TX3Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      11903U,
      13320U,
      13590U,
      12576U,
      4863U,
      6198U,
      6438U,
      10241U,
      10240U
        };
        private uint[] TX4Seq = new uint[13]
        {
      24U,
      129U,
      262229U,
      14432U,
      11903U,
      13320U,
      13590U,
      12576U,
      5119U,
      6710U,
      6950U,
      10241U,
      10240U
        };
        private string version = "0.1.1";
        private string version_date = "March 2020";
        private IContainer components = (IContainer)null;
        private static SdpBase sdp;
        private static SdpBase sdp2;
        private ISpi session;
        private ISpi session2;
        private IGpio g_session;
        private IGpio g_session2;
        private int Demo_Phase_Angle_diff;
        private uint Demo_Loop_Delay;
        private int ADIDemo_Phase_Angle_diff;
        private uint ADIDemo_Loop_Delay;

        private bool RX1;
        private bool RX2;
        private bool RX3;
        private bool RX4;
        private bool TX1;
        private bool TX2;
        private bool TX3;
        private bool TX4;

        private StatusStrip MainFormStatusBar;
        private MenuStrip MainFormMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripStatusLabel DeviceConnectionStatus;
        private ToolStripStatusLabel DeviceConnectionStatus2;

        private TextBox EventLog;
        private PictureBox ADILogo;
        private TextBox R0Box;
        private Button R0Button;
        private Label label8;
        private ToolStripStatusLabel StatusBarLabel;
        private ToolStripStatusLabel StatusBarLabel2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox groupBox2;
        private Label label3;
        private TabControl tabControl1;
        private TabPage ManualRegWrite;

        private TabPage AutoRegWrite;
        private TabPage BeamAlignmentTx;
        private TabPage BeamAlignmentRx;

        private TabPage Tracking_with_PF;



        private Button ChooseInputButton;
        private OpenFileDialog LoadFileDialog;

        private TextBox ValuesToWriteBox;
        private TextBox ValuesToWriteBox00;
        private TextBox ValuesToWriteBox01;
        private TextBox ValuesToWriteBox02;
        private TextBox ValuesToWriteBoxT0;
        private TextBox ValuesToWriteBoxT1;
        private TextBox ValuesToWriteBoxT2;
        private TextBox ValuesToWriteBoxR0;
        private TextBox ValuesToWriteBoxR1;
        private TextBox ValuesToWriteBoxR2;
        private TextBox ValuesToWirteBoxPF01;

        private Button WriteAllButton;
        private Button StartBeamSweeping;
        private Button StartRandomBeamForming;
        private Button StartBeamAlignmentTx;
        private Button StartBeamAlignmentTx_using_Loc_info;
        private Button StartBeamAlignmentRx;
        private Button StartBeamAlignmentRx_using_Loc_info;

        private Button StartBeamTracking_SIMO_PF;

        //BeamSweeping Graphic By Hyunsoo
        private Panel BeamSweeping_GUI_Panel_0;
        private Panel BeamSweeping_GUI_Panel_T;
        private Panel BeamSweeping_GUI_Panel_R;
        private Panel BeamTracking_GUI_Panel_SIMO;
        

        private GroupBox groupBox4;
        private GroupBox groupBox666;
        private GroupBox groupBox667;
        private GroupBox groupBox668;
        private GroupBox groupBox669;

        private PictureBox pictureBox1;
        private Button button1;
        private RadioButton radioButton1;
        private Label label10;
        private TabPage ConnectionTab;
        private TabPage ConnectionTab2;
        private PictureBox pictureBox2;
        private Button ConnectSDPButton;
        private Button ConnectSDPButton2;
        private Label label11;
        private Label label1;
        private Label label180;
        private Label label181;
        private Label label182;
        private Label label183;
        private Label label184;
        private Label label185;
        private Label mylabel001;

        private TextBox textBox1;
        private TextBox textBox10;
        private TabPage MISC;
        private GroupBox groupBox7;
        private Label label54;
        private Button button9;
        private CheckBox CH4_DET_EN;
        private CheckBox CH3_DET_EN;
        private CheckBox CH2_DET_EN;
        private CheckBox CH1_DET_EN;
        private CheckBox LNA_BIAS_OUT_EN;
        private CheckBox BIAS_CTRL;
        private GroupBox groupBox8;
        private Label label51;
        private NumericUpDown LNA_BIAS_ON;
        private Label label47;
        private Label label50;
        private Label label48;
        private NumericUpDown CH1PA_BIAS_ON;
        private Label label49;
        private Button button6;
        private NumericUpDown CH4PA_BIAS_ON;
        private NumericUpDown CH2PA_BIAS_ON;
        private NumericUpDown CH3PA_BIAS_ON;
        private GroupBox groupBox11;
        private Label label56;
        private CheckBox ST_CONV;
        private CheckBox CLK_EN;
        private CheckBox ADC_EN;
        private CheckBox ADC_CLKFREQ_SEL;
        private Button button12;
        private Label label57;
        private GroupBox groupBox13;
        private GroupBox groupBox17;
        private Label label100;
        private NumericUpDown NVM_ADDR_BYP;
        private Label label99;
        private Label label94;
        private CheckBox NVM_MARGIN;
        private Label label95;
        private Label label96;
        private CheckBox NVM_TEST;
        private Label label98;
        private CheckBox NVM_PROG_PULSE;
        private Label label92;
        private CheckBox NVM_CTL_BYP_EN;
        private Label label87;
        private CheckBox NVM_RD_BYP;
        private Label label88;
        private Label label90;
        private CheckBox NVM_ON_BYP;
        private Label label91;
        private CheckBox NVM_START_BYP;
        private Label label86;
        private CheckBox FUSE_BYPASS;
        private Label label89;
        private Label label93;
        private NumericUpDown NVM_BIT_SEL;
        private CheckBox NVM_REREAD;
        private Button button20;
        private Label label97;
        private CheckBox FUSE_CLOCK_CTL;
        private Label label103;
        private NumericUpDown NVM_DIN;
        private Label label101;
        private GroupBox groupBox18;
        private Label label104;
        private NumericUpDown LDO_TRIM_BYP_C;
        private Label label102;
        private NumericUpDown LDO_TRIM_BYP_B;
        private Label label109;
        private Label label115;
        private NumericUpDown LDO_TRIM_BYP_A;
        private Button button21;
        private TabPage RXRegisters;
        private TabPage TXRegisters;
        private GroupBox groupBox20;
        private GroupBox groupBox19;
        private GroupBox groupBox21;
        private GroupBox groupBox22;
        private Button button11;
        private Label label61;
        private Label label58;
        private NumericUpDown RX_VM_BIAS2;
        private NumericUpDown RX_VGA_BIAS2;
        private Label label68;
        private Button button22;
        private Label label62;
        private Label label63;
        private NumericUpDown TX_VM_BIAS3;
        private NumericUpDown TX_VGA_BIAS3;
        private Label label67;
        private GroupBox groupBox3;
        private Button button3;
        private NumericUpDown RXGain4;
        private CheckBox RXGain4_Attenuation;
        private NumericUpDown RXGain3;
        private CheckBox RXGain3_Attenuation;
        private NumericUpDown RXGain2;
        private CheckBox RXGain2_Attenuation;
        private NumericUpDown RXGain1;
        private Label label19;
        private CheckBox RXGain1_Attenuation;
        private GroupBox groupBox5;
        private Label label34;
        private Label label33;
        private Label label32;
        private Label label31;
        private Label label30;
        private Label label29;
        private Label label28;
        private Label label27;
        private NumericUpDown CH4_RX_Phase_Q;
        private CheckBox RX_VM_CH4_POL_Q;
        private NumericUpDown CH3_RX_Phase_Q;
        private CheckBox RX_VM_CH3_POL_Q;
        private NumericUpDown CH2_RX_Phase_Q;
        private CheckBox RX_VM_CH2_POL_Q;
        private NumericUpDown CH1_RX_Phase_Q;
        private CheckBox RX_VM_CH1_POL_Q;
        private Button button4;
        private NumericUpDown CH4_RX_Phase_I;
        private Label label23;
        private CheckBox RX_VM_CH4_POL_I;
        private NumericUpDown CH3_RX_Phase_I;
        private Label label24;
        private CheckBox RX_VM_CH3_POL_I;
        private NumericUpDown CH2_RX_Phase_I;
        private Label label25;
        private CheckBox RX_VM_CH2_POL_I;
        private NumericUpDown CH1_RX_Phase_I;
        private Label label26;
        private CheckBox RX_VM_CH1_POL_I;
        private NumericUpDown Gain4_Value;
        private NumericUpDown Gain3_Value;
        private NumericUpDown Gain2_Value;
        private Button button2;
        private NumericUpDown Gain1_Value;
        private GroupBox groupBox1;
        private CheckBox CH4_ATTN_TX;
        private CheckBox CH3_ATTN_TX;
        private CheckBox CH2_ATTN_TX;
        private CheckBox CH1_ATTN_TX;
        private GroupBox groupBox6;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label40;
        private Label label41;
        private Label label42;
        private NumericUpDown CH4_TX_Phase_Q;
        private CheckBox TX_VM_CH4_POL_Q;
        private NumericUpDown CH3_TX_Phase_Q;
        private CheckBox TX_VM_CH3_POL_Q;
        private NumericUpDown CH2_TX_Phase_Q;
        private CheckBox TX_VM_CH2_POL_Q;
        private NumericUpDown CH1_TX_Phase_Q;
        private CheckBox TX_VM_CH1_POL_Q;
        private Button button5;
        private NumericUpDown CH4_TX_Phase_I;
        private Label label43;
        private CheckBox TX_VM_CH4_POL_I;
        private NumericUpDown CH3_TX_Phase_I;
        private Label label44;
        private CheckBox TX_VM_CH3_POL_I;
        private NumericUpDown CH2_TX_Phase_I;
        private Label label45;
        private CheckBox TX_VM_CH2_POL_I;
        private NumericUpDown CH1_TX_Phase_I;
        private Label label46;
        private CheckBox TX_VM_CH1_POL_I;
        private Label label79;
        private CheckBox RX_CH4_RAM_FETCH;
        private Label label78;
        private CheckBox RX_CH3_RAM_FETCH;
        private Label label77;
        private CheckBox RX_CH2_RAM_FETCH;
        private Label label76;
        private Button button16;
        private NumericUpDown RX_CH4_RAM_INDEX;
        private Label label64;
        private NumericUpDown RX_CH3_RAM_INDEX;
        private Label label66;
        private NumericUpDown RX_CH2_RAM_INDEX;
        private Label label70;
        private NumericUpDown RX_CH1_RAM_INDEX;
        private Label label71;
        private CheckBox RX_CH1_RAM_FETCH;
        private Label label83;
        private CheckBox TX_CH4_RAM_FETCH;
        private Label label82;
        private CheckBox TX_CH3_RAM_FETCH;
        private Label label81;
        private CheckBox TX_CH2_RAM_FETCH;
        private Label label80;
        private CheckBox TX_CH1_RAM_FETCH;
        private Button button17;
        private NumericUpDown TX_CH4_RAM_INDEX;
        private Label label72;
        private NumericUpDown TX_CH3_RAM_INDEX;
        private Label label73;
        private NumericUpDown TX_CH2_RAM_INDEX;
        private Label label74;
        private NumericUpDown TX_CH1_RAM_INDEX;
        private Label label75;
        private GroupBox groupBox12;
        private Label label59;
        private Button button13;
        private Label label69;
        private CheckBox DRV_GAIN;
        private GroupBox groupBox14;
        private Button button15;
        private CheckBox BEAM_RAM_BYPASS;
        private CheckBox SCAN_MODE_EN;
        private GroupBox groupBox16;
        private Label label85;
        private Label label84;
        private NumericUpDown LDO_TRIM_REG;
        private NumericUpDown LDO_TRIM_SEL;
        private Button button18;
        private Button button19;
        private TabPage ReadBack;
        private GroupBox groupBox15;
        private Label label105;
        private NumericUpDown LNA_BIAS_OFF;
        private Label label106;
        private Label label107;
        private Label label108;
        private NumericUpDown CH1PA_BIAS_OFF;
        private Label label110;
        private Button button23;
        private NumericUpDown CH4PA_BIAS_OFF;
        private NumericUpDown CH2PA_BIAS_OFF;
        private NumericUpDown CH3PA_BIAS_OFF;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Registers;
        private DataGridViewTextBoxColumn Written;
        private DataGridViewTextBoxColumn Read_col;
        private Label label60;
        private NumericUpDown LNA_BIAS;
        private Label label65;
        private NumericUpDown TX_DRV_BIAS;
        private Label label20;
        private Label label17;
        private Label label16;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label112;
        private Label label111;
        private Label label18;
        private Label label15;
        private Label label22;
        private Label label12;
        private Label label21;
        private Label label2;
        private Label label117;
        private Label label116;
        private Label label114;
        private Label label113;
        private Label label121;
        private Label label120;
        private Label label119;
        private Label label118;
        private Button button25;
        private TabPage GPIOpins;
        private PictureBox pictureBox3;
        private TabPage TRControl;
        private GroupBox groupBox26;
        private CheckBox SW_DRV_TR_STATE_2;
        private Label label124;
        private CheckBox POL_2;
        private CheckBox TR_SPI_2;
        private CheckBox TR_SOURCE_2;
        private CheckBox SW_DRV_EN_POL_2;
        private CheckBox SW_DRV_EN_TR_2;
        private Button button10_2;
        private GroupBox groupBox25;
        private CheckBox RX_EN_2;
        private Label label123;
        private CheckBox RX_VGA_EN_2;
        private CheckBox RX_VM_EN_2;
        private CheckBox RX_LNA_EN_2;
        private CheckBox CH4_RX_EN_2;
        private CheckBox CH3_RX_EN_2;
        private CheckBox CH2_RX_EN_2;
        private CheckBox CH1_RX_EN_2;
        private Button button7_2;
        private GroupBox groupBox24;
        private CheckBox TX_EN_2;
        private CheckBox CH1_TX_EN_2;
        private Label label122;
        private Button button8_2;
        private CheckBox TX_VGA_EN_2;
        private CheckBox TX_VM_EN_2;
        private CheckBox TX_DRV_EN_2;
        private CheckBox CH4_TX_EN_2;
        private CheckBox CH3_TX_EN_2;
        private CheckBox CH2_TX_EN_2;
        private Label label125;
        private Label label126;
        private Label label127;
        private TextBox ADC_EOC;
        private Button button7;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button8;
        private Label label53;
        private Label label52;
        private Label label133;
        private TextBox ROBox;
        private Button button26;
        private GroupBox groupBox27;
        private CheckBox TR_;
        private CheckBox ADDR_1;
        private CheckBox ADDR_0;
        private CheckBox TX_LOAD;
        private CheckBox RX_LOAD;
        private ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private TextBox textBox4;
        private ComboBox MUX_SEL;
        private Button button14;
        private Button button10;
        private Panel TestmodesPanel;
        private Panel PasswordPanel;
        private Button OKButton;
        private MaskedTextBox PasswordBox;
        private Label label4;
        private Panel Test_Modes_Panel2;
        private Panel panel1;
        private CheckBox GPIO_5;
        private CheckBox GPIO_52;
        private TabPage PhaseLoop;
        private GroupBox groupBox9;
        private NumericUpDown EndPoint;
        private NumericUpDown StartPoint;
        private Label label55;
        private Label label14;
        private Label label13;
        private NumericUpDown TimeDelay;
        private Button button24;
        private Button button27;
        private TabPage Tx_Control;
        private PictureBox pictureBox5;
        private ComboBox TX_CH1_Phase_Angle;
        private Button phase_block_button;
        private Button button28;
        private ComboBox TX_CH2_Phase_Angle;
        private Button button29;
        private ComboBox TX_CH4_Phase_Angle;
        private Button button30;
        private ComboBox TX_CH3_Phase_Angle;
        private Button button31;
        private ComboBox RX_CH4_Phase_Angle;
        private Button button32;
        private ComboBox RX_CH3_Phase_Angle;
        private Button button33;
        private ComboBox RX_CH2_Phase_Angle;
        private Button button34;
        private ComboBox RX_CH1_Phase_Angle;
        private TabPage Rx_Control;
        private PictureBox pictureBox6;
        private Label label128;
        private Panel panel3;
        private Panel panel5;
        private Panel panel6;
        private Panel panel4;
        private CheckBox TX3_Attn_CheckBox;
        private CheckBox TX4_Attn_CheckBox;
        private CheckBox TX2_Attn_CheckBox;
        private CheckBox TX1_Attn_CheckBox;
        private Label label129;
        private Button button35;
        private Button button36;
        private CheckBox RX4_Attn_CheckBox;
        private CheckBox RX3_Attn_CheckBox;
        private CheckBox RX2_Attn_CheckBox;
        private CheckBox RX1_Attn_CheckBox;
        private Label label130;
        private ComboBox TX_CH3_Gain;
        private ComboBox TX_CH4_Gain;
        private ComboBox TX_CH2_Gain;
        private ComboBox TX_CH1_Gain;
        private Label label131;
        private Label label132;
        private Label label134;
        private Panel panel7;
        private ComboBox RX_CH3_Gain;
        private Panel panel8;
        private ComboBox RX_CH4_Gain;
        private Panel panel9;
        private ComboBox RX_CH2_Gain;
        private Panel panel10;
        private ComboBox RX_CH1_Gain;
        private PictureBox TX2_Attn_Pic;
        private PictureBox TX3_Attn_Pic;
        private PictureBox TX4_Attn_Pic;
        private PictureBox TX1_Attn_Pic;
        private PictureBox RX1_Attn_Pic;
        private PictureBox RX4_Attn_Pic;
        private PictureBox RX3_Attn_Pic;
        private PictureBox RX2_Attn_Pic;
        private Panel panel11;
        private Label TX_Init_Label;
        private Panel TX_Init_Indicator;
        private Button TX_Init_Button;
        private Label RX_Init_Label;
        private Panel RX_Init_Indicator;
        private Button RX_Init_Button;
        private RadioButton RX2_radioButton;
        private RadioButton RX1_radioButton;
        private RadioButton TX4_radioButton;
        private RadioButton TX3_radioButton;
        private RadioButton TX2_radioButton;
        private RadioButton TX1_radioButton;
        private RadioButton RX4_radioButton;
        private RadioButton RX3_radioButton;
        private ComboBox Demo_angle_list;
        private NumericUpDown Demo_loop_time;
        private Button Stop_demo_button;
        private Button Start_demo_button;
        private Button ADI_logo_demo_button;
        private Label label137;
        private Label label138;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox1;
        private GroupBox groupBox10;
        private Label label136;
        private Label label135;
        private GroupBox groupBox28;
        private GroupBox groupBox23;
        private CheckBox BIAS_RAM_BYPASS;
        private CheckBox RX_CHX_RAM_BYPASS;
        private CheckBox TX_CHX_RAM_BYPASS;
        private CheckBox RX_BEAM_STEP_EN;
        private CheckBox TX_BEAM_STEP_EN;
        private Label label139;
        private NumericUpDown RX_CHX_RAM_INDEX;
        private Label label140;
        private CheckBox RX_CHX_RAM_FETCH;
        private Label label141;
        private CheckBox TX_CHX_RAM_FETCH;
        private NumericUpDown TX_CHX_RAM_INDEX;
        private Label label142;
        private GroupBox groupBox31;
        private Label label153;
        private CheckBox TX_BIAS_RAM_FETCH;
        private Label label152;
        private CheckBox RX_BIAS_RAM_FETCH;
        private Button button40;
        private Label label151;
        private NumericUpDown RX_BIAS_RAM_INDEX;
        private Label label154;
        private NumericUpDown TX_BIAS_RAM_INDEX;
        private Button button39;
        private GroupBox groupBox30;
        private Label label147;
        private NumericUpDown TX_BEAM_STEP_START;
        private Label label148;
        private NumericUpDown RX_BEAM_STEP_STOP;
        private Label label149;
        private NumericUpDown RX_BEAM_STEP_START;
        private Label label150;
        private NumericUpDown TX_BEAM_STEP_STOP;
        private Button button38;
        private GroupBox groupBox29;
        private Label label146;
        private NumericUpDown TX_TO_RX_DELAY_1;
        private Label label143;
        private NumericUpDown RX_TO_TX_DELAY_2;
        private Label label144;
        private NumericUpDown RX_TO_TX_DELAY_1;
        private Label label145;
        private NumericUpDown TX_TO_RX_DELAY_2;
        private Button button37;
        private TabPage BeamSequencer;
        private Label label161;
        private Label label162;
        private ComboBox DemoPhase4;
        private ComboBox DemoGain4;
        private Label label159;
        private Label label160;
        private ComboBox DemoPhase3;
        private ComboBox DemoGain3;
        private Label label157;
        private Label label158;
        private ComboBox DemoPhase2;
        private ComboBox DemoGain2;
        private Label label156;
        private Label label155;
        private ComboBox DemoPhase1;
        private ComboBox DemoGain1;
        private Button button41;
        private PictureBox PolarPlot;
        private TextBox DemoFileName;
        private Button SelectDemoFile;
        private Button LoadDemoFile;
        private Panel panel2;
        private CheckBox ADDR1_checkBox;
        private CheckBox ADDR0_checkBox;

        private int StartingAngle;
        private int EndingAngle;
        private int Resolution;
        private int TD;
        private string RegInfo;
        private int Iteration;
        private int Init_AOA;
        private float Var_alpha;
        private float Var_AOA;
        private int Num_particle;
        private int N_threshold;

        public System.Numerics.Complex constant_e = new System.Numerics.Complex(Math.E, 0);



        //private int PortNumber;

        private string ip = "127.0.0.1";
        private int port = 10801;
        private Thread listenThread; //Accept()가 블럭
        private Thread receiveThread; //Receive() 작업
        private Socket clientSocket; //연결된 클라이언트 소켓

        private System.Windows.Forms.RichTextBox richTextBox9001;
        private System.Windows.Forms.TextBox textBox9001;
        private System.Windows.Forms.ListBox listBox9001;
        private System.Windows.Forms.Button button9001;
        private System.Windows.Forms.GroupBox groupBox9001;
        private System.Windows.Forms.Label label9001;

        public int theta_Op;


        public Main_Form()
        {
            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.exitEventHandler);
            this.worker.DoWork += new DoWorkEventHandler(this.DemoLoop);
            this.worker.WorkerSupportsCancellation = true;
            this.ADIworker.DoWork += new DoWorkEventHandler(this.ADIDemoLoop);
            this.ADIworker.WorkerSupportsCancellation = true;
            this.Demo_angle_list.TextChanged += new EventHandler(this.Demo_angle_list_TextChanged);
        }

        private void Demo_angle_list_TextChanged(object sender, EventArgs e)
        {
            this.Demo_Phase_Angle_diff = this.Demo_angle_list.SelectedIndex;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Demo_Loop_Delay = Convert.ToUInt32(this.Demo_loop_time.Value);
        }

        private void ADI_Phase_Offset_Angle_TextChanged(object sender, EventArgs e)
        {
            this.ADIDemo_Phase_Angle_diff = this.comboBox1.SelectedIndex;
        }

        private void ADI_Time_Delay_ValueChanged(object sender, EventArgs e)
        {
            this.ADIDemo_Loop_Delay = Convert.ToUInt32(this.numericUpDown1.Value);
        }

        private void RX_TX_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.RX1 = this.RX1_radioButton.Checked;
            this.RX2 = this.RX2_radioButton.Checked;
            this.RX3 = this.RX3_radioButton.Checked;
            this.RX4 = this.RX4_radioButton.Checked;
            this.TX1 = this.TX1_radioButton.Checked;
            this.TX2 = this.TX2_radioButton.Checked;
            this.TX3 = this.TX3_radioButton.Checked;
            if (this.TX4_radioButton.Checked)
                this.TX4 = true;
            else
                this.TX4 = false;
        }

        private void exitEventHandler(object sender, EventArgs e)
        {
            if (Main_Form.sdp == null)
                return;
            Main_Form.sdp.Unlock();
        }

        public void Connect_SDP()
        {
            this.log("Attempting SDP connection...");
            try
            {
                Main_Form.sdp = new SdpBase();
                SdpManager.connectVisualStudioDialog("6065711100000001", "", false, out Main_Form.sdp);
                this.log("Flashing LED.");
                Main_Form.sdp.flashLed1();
                this.messageShownToUser = false;
                try
                {
                    Main_Form.sdp.programSdram(Application.StartupPath + "\\SDP_Blackfin_Firmware.ldr", true, true);
                    string bootStatus;
                    Main_Form.sdp.reportBootStatus(out bootStatus);
                    try
                    {
                        this.configNormal(Main_Form.sdp.ID1Connector, 0U);
                    }
                    catch (Exception ex)
                    {
                        if (!(ex is SdpApiErrEx) || (ex as SdpApiErrEx).number != SdpApiErr.FunctionNotSupported)
                            throw ex;
                        if (ex.Message.Substring(17) == "Use Connector A")
                        {
                            int num = (int)MessageBox.Show(ex.Message.Substring(17));
                            this.messageShownToUser = true;
                            Main_Form.sdp.Unlock();
                            throw new Exception();
                        }
                        if (!(ex.Message.Substring(17) == "For optimal performance ensure CLKOUT is disabled"))
                            throw ex;
                        int num1 = (int)MessageBox.Show("Remove R57 from the SDP board to ensure optimum performance", "Warning!");
                        this.messageShownToUser = true;
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is SdpApiWarnEx) || (ex as SdpApiWarnEx).number != SdpApiWarn.NonFatalFunctionNotSupported)
                        throw ex;
                }
                this.ConnectSDPButton.Enabled = false;
                this.DeviceConnectionStatus.Text = "SDP board connected. Using " + Main_Form.sdp.ID1Connector.ToString();
                this.DeviceConnectionStatus.ForeColor = Color.Green;
                this.log("SDP connected.");
                int sclkFrequency = 500 * 1000;
                Main_Form.sdp.newSpi(Main_Form.sdp.ID1Connector, SpiSel.selA, 24, false, false, false, sclkFrequency, 0, out this.session);
                Main_Form.sdp.newGpio(Main_Form.sdp.ID1Connector, out this.g_session);
                this.g_session.configOutput((byte)63);
                this.g_session.bitSet((byte)13);
                this.g_session.bitClear((byte)16);
                this.g_session.configOutput((byte)2);
                this.g_session.bitClear((byte)2);
            }
            catch
            {
                Main_Form.sdp.Unlock();
                int num = (int)MessageBox.Show("SDP connection failed");
                this.log("SDP connection failed.");
            }
            this.GPIO_Click((object)null, new EventArgs());
        }


        public void Connect_SDP2()
        {
            this.log("Attempting SDP2 connection...");
            try
            {
                Main_Form.sdp2 = new SdpBase();
                SdpManager.connectVisualStudioDialog("6065711100000001", "", false, out Main_Form.sdp2);
                this.log("Flashing LED.");
                Main_Form.sdp2.flashLed1();
                this.messageShownToUser = false;
                try
                {/////////ok
                    Main_Form.sdp2.programSdram(Application.StartupPath + "\\SDP_Blackfin_Firmware.ldr", true, true);
                    string bootStatus;
                    Main_Form.sdp2.reportBootStatus(out bootStatus);
                    try
                    {
                        this.configNormal2(Main_Form.sdp2.ID1Connector, 0U);
                    }
                    catch (Exception ex)
                    {
                        if (!(ex is SdpApiErrEx) || (ex as SdpApiErrEx).number != SdpApiErr.FunctionNotSupported)
                            throw ex;
                        if (ex.Message.Substring(17) == "Use Connector A")
                        {
                            int num = (int)MessageBox.Show(ex.Message.Substring(17));
                            this.messageShownToUser = true;
                            Main_Form.sdp2.Unlock();
                            throw new Exception();
                        }
                        if (!(ex.Message.Substring(17) == "For optimal performance ensure CLKOUT is disabled"))
                            throw ex;
                        int num1 = (int)MessageBox.Show("Remove R57 from the SDP board to ensure optimum performance", "Warning!");
                        this.messageShownToUser = true;
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is SdpApiWarnEx) || (ex as SdpApiWarnEx).number != SdpApiWarn.NonFatalFunctionNotSupported)
                        throw ex;
                }
                this.ConnectSDPButton2.Enabled = false;
                this.DeviceConnectionStatus2.Text = "SDP board connected. Using " + Main_Form.sdp2.ID1Connector.ToString();
                this.DeviceConnectionStatus2.ForeColor = Color.Green;
                this.log("SDP connected.");
                int sclkFrequency = 500 * 1000;
                Main_Form.sdp2.newSpi(Main_Form.sdp2.ID1Connector, SpiSel.selA, 24, false, false, false, sclkFrequency, 0, out this.session2);
                Main_Form.sdp2.newGpio(Main_Form.sdp2.ID1Connector, out this.g_session2);
                this.g_session2.configOutput((byte)63);
                this.g_session2.bitSet((byte)13);
                this.g_session2.bitClear((byte)16);
                this.g_session2.configOutput((byte)2);
                this.g_session2.bitClear((byte)2);
            }
            catch
            {
                Main_Form.sdp2.Unlock();
                int num = (int)MessageBox.Show("SDP connection failed");
                this.log("SDP connection failed.");
            }
            this.GPIO_Click2((object)null, new EventArgs());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.log("Exiting.");
            this.Close();
        }

        private void WriteToDevice(uint data)
        {
            uint[] writeData = new uint[1];
            if (this.session == null)
                return;
            int num = 500;
            writeData[0] = data;
            this.session.sclkFrequency = num * 1000;
            this.session.writeU24(writeData);
            //if (this.session.writeU24(writeData) == 0)
                //this.log("0x" + string.Format("{0:X}", (object)data) + " written to FW beamformer.");
                
        }

        private void WriteToDevice2(uint data2)
        {
            uint[] writeData2 = new uint[1];
            if (this.session2 == null)
                return;
            int num = 500;
            writeData2[0] = data2;
            this.session2.sclkFrequency = num * 1000;
            this.session2.writeU24(writeData2);
            //if (this.session2.writeU24(writeData2) == 0)
            //this.log("0x" + string.Format("{0:X}", (object)data2) + " written to BW beamformer.");

        }

        private void log(string message)
        {
            DateTime now = DateTime.Now;
            string str1 = now.Hour.ToString();
            string str2 = now.Minute.ToString();
            string str3 = now.Second.ToString();
            if (str1.Length == 1)
                str1 = "0" + str1;
            if (str2.Length == 1)
                str2 = "0" + str2;
            if (str3.Length == 1)
                str3 = "0" + str3;
            TextBox eventLog = this.EventLog;
            eventLog.Text = eventLog.Text + "\r\n" + str1 + ":" + str2 + ":" + str3 + ": " + message;
            this.EventLog.Update();
            this.EventLog.SelectionStart = this.EventLog.Text.Length;
            this.EventLog.ScrollToCaret();
        }

        private void WriteSpeedBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void ChooseInputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            int num = (int)openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                string end = streamReader.ReadToEnd();
                streamReader.Close();
                this.ValuesToWriteBox.Text = end;
            }
            openFileDialog.Dispose();
        }

        private void WriteAllButton_Click(object sender, EventArgs e)
        {
            string[] strArray = this.ValuesToWriteBox.Text.Split('\n');
            for (int index = 0; index < strArray.Length; ++index)
                strArray[index] = strArray[index].Trim();
            foreach (string str in strArray)
                this.WriteToDevice(Convert.ToUInt32(str, 16));
        }










        /// 
        /// 0-0. Beamforming Handler
        /// 
        private void Beamforming_handler(int theta)
        {
            //this.log("reading files for FW.");
            ///theta 방향으로 빔 포밍. Choose Input Button _ Click 가져와봄.
            StreamReader streamReader1 = new StreamReader(RegInfo + "FW_Reg_Beam_" + theta.ToString("G") + ".txt"); //인식할 수 없는 이스케이프 시퀀스??
            string end = streamReader1.ReadToEnd();
            streamReader1.Close();
            this.ValuesToWriteBox01.Text = end;

            /// 쓰기. Write All button _ Click 도 가져와봄.
            string[] strArray1 = this.ValuesToWriteBox01.Text.Split('\n');
            for (int index = 0; index < strArray1.Length; ++index)
                strArray1[index] = strArray1[index].Trim();
            foreach (string str1 in strArray1)
                this.WriteToDevice(Convert.ToUInt32(str1, 16));

            // 다썼습니당!!
            //this.log("Reg.Info. for beam-steering angle of " + string.Format("{0}", (object)theta) + " degree is written to forward beamformer.");

            //this.log("reading files for BW.");
            ///theta 방향으로 빔 포밍. Choose Input Button _ Click 가져와봄.2222
            StreamReader streamReader2 = new StreamReader(RegInfo + "BW_Reg_Beam_" + theta.ToString("G") + ".txt"); //인식할 수 없는 이스케이프 시퀀스??
            string end2 = streamReader2.ReadToEnd();
            streamReader2.Close();
            this.ValuesToWriteBox02.Text = end2;

            /// 쓰기. Write All button _ Click 도 가져와봄.
            string[] strArray2 = this.ValuesToWriteBox02.Text.Split('\n');
            for (int index = 0; index < strArray2.Length; ++index)
                strArray2[index] = strArray2[index].Trim();
            foreach (string str2 in strArray2)
                this.WriteToDevice2(Convert.ToUInt32(str2, 16));
            /////////////////////////////

            // 다썼습니당!!
            //this.log("Reg.Info. for beam-steering angle of " + string.Format("{0}", (object)theta) + "degree is written to backward beamformer.");

            //this.log("Angle of " + string.Format("{0}", (object)theta) + " degree is written.");

        }


        /// 
        /// 0-1. Exhaustive Sweeping Handler for Tx
        /// 
        private int Exhaustive_Sweeping_Tx_handler(int Angle_Start, int Angle_End, int Angle_interval, Socket Client)
        {
            int New_Beam_Required = 0;
            int num_TxBeam = (Angle_End - Angle_Start) / Angle_interval + 1;
            int Optimal_Tx_Beam_Idx = (num_TxBeam+1)/2; // 디폴트 전방

            for(int theta_TxSweep = Angle_Start; (theta_TxSweep - Angle_Start) * (Angle_End - theta_TxSweep) >= 0; theta_TxSweep = theta_TxSweep + Angle_interval)
            {
                Beamforming_handler(theta_TxSweep);
                do
                {
                    //연결된 클라이언트가 보낸 데이터 수신
                    
                    byte[] receiveBuffer = new byte[512]; //
                    this.log("Waiting for receiving a data from Rx...");
                    int length = Client.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                    this.log("Data received from Rx...");
                    string msg = Encoding.UTF8.GetString(receiveBuffer);

                    string[] raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    string proc = string.Join(" ", raw);

                    string[] origin_msg = proc.Split('[', ',', ']');
                    this.log("proc msg is : " + proc);

                    New_Beam_Required = Convert.ToInt16(origin_msg[1]); // 첫 번째 자리를 New Beam으로 사용하겠다..
                    Optimal_Tx_Beam_Idx = Convert.ToInt16(origin_msg[2]); // 두 번째 자리를 Opt Beam으로

                    this.log("NBR and OTBI is : " + Convert.ToString(New_Beam_Required) +","+ Convert.ToString(Optimal_Tx_Beam_Idx));
                    

                    //ShowMsg("Raw Msg from Rx is : " + Convert.ToString(raw));

                    //ShowMsg("origin_msg[1] from Rx is : " + origin_msg[1]);
                    //ShowMsg("origin_msg[2] from Rx is : " + origin_msg[2]);


                } while (New_Beam_Required != 1);
                // Rx가 새로운 빔을 요청했다. 다음으로 넘어간다.

            }
            this.log("One Exhaustive Beam Sweeping loop is done.");
            return Optimal_Tx_Beam_Idx;
        }



        /// 
        /// 0-2. Exhaustive Sweeping Handler for Rx
        /// 
        private int[] Exhaustive_Sweeping_Rx_handler(int Angle_Start, int Angle_End, int Angle_interval, int Time_Beam_Duration,
            Socket Server, MemoryMappedViewAccessor Accessor_NewBeam, MemoryMappedViewAccessor Accessor_PowerOfBeam)

        {
            //this.log("Exhaustive Sweeping is started...");
            float NB = 0;
            float isPOB = 0;
            bool BPOB;
            int num_TxBeam = (Angle_End - Angle_Start) / Angle_interval + 1;
            int num_RxBeam = (Angle_End - Angle_Start) / Angle_interval + 1;
            int Tx_Beam_Idx = 0;
            int Rx_Beam_Idx = 0; // 시작 0은 Angle_Start 기준
            string Report_Msg;

            double[,] POB = new double[num_TxBeam, num_RxBeam];
            for (int theta_TxSweep = Angle_Start; (theta_TxSweep - Angle_Start) * (Angle_End - theta_TxSweep) >= 0; theta_TxSweep = theta_TxSweep + Angle_interval)
            {

                //this.log("here2");
                Rx_Beam_Idx = 0;
                for (int theta_RxSweep = Angle_Start; (theta_RxSweep - Angle_Start) * (Angle_End - theta_RxSweep) >= 0; theta_RxSweep = theta_RxSweep + Angle_interval)
                {

                    //this.log("here3");
                    Beamforming_handler(theta_RxSweep);
                    // 옛다 새빔이당
                    NB = 1;
                    Accessor_NewBeam.Write(0, ref NB);
                    // 잠깐 기다릴까?
                    //this.log("here4");
                    System.Threading.Thread.Sleep(Time_Beam_Duration);

                    // 이제 전력 값을 제대로 들어있을 때 까지 가져옴.
                    Accessor_PowerOfBeam.Read(0, out isPOB);
                    //this.log("here4.5");
                    float tempPOB = isPOB;
                    BPOB = Convert.ToBoolean(isPOB);
                    //this.log("here5");
                    while (true)
                    {
                        if (BPOB)
                        {
                            POB[Tx_Beam_Idx, Rx_Beam_Idx] = isPOB;
                            isPOB = 0;
                            Accessor_PowerOfBeam.Write(0, ref isPOB);
                            //새 빔 끝났당
                            NB = 0;
                            Accessor_NewBeam.Write(0, ref NB);
                            //this.log("here6");
                            break;
                        }
                        // 제대로 안들어있으면 쉬고 다시 가져옴.
                        else
                        {
                            NB = 1;
                            Accessor_NewBeam.Write(0, ref NB);
                            System.Threading.Thread.Sleep(Time_Beam_Duration);
                            Accessor_PowerOfBeam.Read(0, out isPOB);
                            //this.log("here66");
                            tempPOB = isPOB;
                            BPOB = Convert.ToBoolean(isPOB);
                            //this.log("here666");
                        }
                    }
                    Rx_Beam_Idx++;
                    //this.log("here7");
                }
                Tx_Beam_Idx++;
                if (Tx_Beam_Idx < num_TxBeam)
                {
                    //this.log("one Rx sweeping loop is done, with Rx index of " + Convert.ToString(Rx_Beam_Idx));

                    Report_Msg = "[" + Convert.ToString(1) + "," + "0" + "]"; // 여기서 0은 그냥 더미
                    //this.log("I'm gonna send that : " + "[" + Convert.ToString(1) + "," + "0" + "]");
                    Server.Send(Encoding.UTF8.GetBytes(Report_Msg)); //  "Tx 빔 바꿔줘 !!" 라는 뜻.
                    //System.Threading.Thread.Sleep(Time_Beam_Duration);
                }
            }
            //this.log("Beam sweeping loop for Tx is also done, with Tx index of " + Convert.ToString(Tx_Beam_Idx));
            //이제 Exhaustive Beam Sweeping 전부 끝남.

            int txidx_opt = 0;
            int rxidx_opt = 0;
            for (int txidx = 0; txidx < num_TxBeam; txidx++)
            {
                for (int rxidx = 0; rxidx < num_RxBeam; rxidx++)
                {
                    if (POB[txidx, rxidx] > POB[txidx_opt, rxidx_opt])
                    {
                        txidx_opt = txidx;
                        rxidx_opt = rxidx;
                    }
                }
            }
            //this.log("My Msg is : " + "[" + Convert.ToString(1) + "," + Convert.ToString(txidx_opt) + "]");
            this.log("Optimal T/Rx beam index is : " + Convert.ToString(txidx_opt) + "," + Convert.ToString(rxidx_opt));
            Report_Msg = "[" + Convert.ToString(1) + "," + Convert.ToString(txidx_opt) + "]"; // 여기서 txidx_opt 보냄
            Server.Send(Encoding.UTF8.GetBytes(Report_Msg));

            int[] returning = new int[] { txidx_opt, rxidx_opt };
            return returning;
        }

        

        ///
        /// 1-1. Beam Sweeping
        ///
        private void StartBeamSweeping_Click(object sender, EventArgs e)
        {
            //this.rotateBeamGUI(-30f, BeamSweeping_GUI_Panel_0);

            string[] strArray0 = this.ValuesToWriteBox00.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]);
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }
            //이상으로 각 줄 읽어들임.
            // 드디어 S<E 조건을 후려쳤다. E<S에서 Res.를 음수로 주어도 됨!!
            for (int theta = StartingAngle; ((theta - StartingAngle) * (EndingAngle - theta)) >= 0; theta = theta + Resolution)
            {

                BeamSweeping_GUI_Panel_0.Refresh();
                this.rotateBeamGUI(-90 - theta, BeamSweeping_GUI_Panel_0);

                Beamforming_handler(theta);


                System.Threading.Thread.Sleep(TD);
            }

        }
        

        ///
        /// 1-2. Random Beam Forming
        ///
        private void Start_Random_Beam_Forming_Click(object sender, EventArgs e)
        {

            string[] strArray0 = this.ValuesToWriteBox00.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]);
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }

            Random r = new Random();
            int numBeam = 1 + ((EndingAngle - StartingAngle) / Resolution);

            int random_theta;
            while (true)
            {
                random_theta = StartingAngle + (r.Next(0, numBeam)) * Resolution;

                BeamSweeping_GUI_Panel_0.Refresh();
                this.rotateBeamGUI(-90 - random_theta, BeamSweeping_GUI_Panel_0);

                Beamforming_handler(random_theta);

                System.Threading.Thread.Sleep(TD);
            }


        }


        ///
        /// 2-1. Beam Alignment Tx
        ///

        //StartBeamAlignmentTx_Click
        private void StartBeamAlignmentTx_Click(object sender, EventArgs e)
        {
            this.log("StartBeamAlignmentTx_Click");
            // 각 줄 읽어들임.

            string[] strArray0 = this.ValuesToWriteBoxT0.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]); // 0325 수정. 자기 자신의 빔 유지시간 맞음.
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }

            // Tx 빔 갯수 계산
            //int numBeam = 1 + ((EndingAngle - StartingAngle) / Resolution);
            int Opt;
            int Opt_Idx;
            

            //종단점
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            this.log("Generating a Socket.");
            //소켓생성
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //바인드
            listenSocket.Bind(endPoint);
            this.log("Listening..");
            //준비
            listenSocket.Listen(10);
            this.log("Waiting for Client..");
            Log("클라이언트 요청 대기중..");
            clientSocket = listenSocket.Accept();
            Log("클라이언트 접속됨 - " + clientSocket.LocalEndPoint.ToString());
            this.log("Connected..");
            while (true)
            {
                Opt_Idx = Exhaustive_Sweeping_Tx_handler(StartingAngle, EndingAngle, Resolution, clientSocket);
                Opt = StartingAngle + Resolution * Opt_Idx;
                BeamSweeping_GUI_Panel_T.Refresh();
                this.rotateBeamGUI(-90 - Opt, BeamSweeping_GUI_Panel_T);
                Beamforming_handler(Opt);
            }
            

            
        }
        
        
        ///
        /// 2-2. Beam Alignment Tx Using Location-Info
        ///


        //StartBeamAlignmentTx_Click
        private void StartBeamAlignmentTx_using_Loc_info_Click(object sender, EventArgs e)
        {
            this.log("StartBeamAlignmentTx_Click");

            string[] strArray0 = this.ValuesToWriteBoxT0.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]); // 0325 수정. 자기 자신의 빔 유지시간 맞음.
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }

            this.rotateBeamGUI(-90 - 0, BeamSweeping_GUI_Panel_T);
            Beamforming_handler(0);


            theta_Op = 0;

            //일단 전방으로 Tx 이후 Socket Listen, Listen스레드 처리
            Log("Listen Thread is started !!!");
            //종단점
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            //소켓생성
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //바인드
            listenSocket.Bind(endPoint);
            //준비
            listenSocket.Listen(10);

            Log("클라이언트 요청 대기중..");
            clientSocket = listenSocket.Accept();
            Log("클라이언트 접속됨 - " + clientSocket.LocalEndPoint.ToString());
            while (true)
            {
                //연결된 클라이언트가 보낸 데이터 수신
                byte[] receiveBuffer = new byte[512]; // msg를 받을 자리를 마련함.
                int length = clientSocket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                //디코딩
                string msg = Encoding.UTF8.GetString(receiveBuffer);
                //엔터처리
                //richTextBox1.AppendText(msg);
                ShowMsg(msg);
                Log("메시지 수신함 : " + msg);

                string[] RxPos_raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string RxPos_rev = string.Join(" ", RxPos_raw);

                string[] RxPos = RxPos_rev.Split('[', ' ', ']');

                double RxPosX = Convert.ToDouble(RxPos[1]);
                double RxPosY = Convert.ToDouble(RxPos[2]);

                
                double TxPosX = 0;
                double TxPosY = 0;

                theta_Op = Convert.ToInt32(Math.Round((180 / Math.PI * (Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX)))) - 90) / Resolution)) * Resolution;
                ShowMsg("Opt_by_Loc is angle of : " + Convert.ToString(theta_Op));


                BeamSweeping_GUI_Panel_T.Refresh();
                this.rotateBeamGUI(-90 - theta_Op, BeamSweeping_GUI_Panel_T);
                Beamforming_handler(theta_Op);
                //this.log("theta_OP is : " + Convert.ToString(theta_Op));
                //if (theta_Op < StartingAngle)
                //{
                //    BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - StartingAngle, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(StartingAngle);
                //}
                //else if (theta_Op > EndingAngle)
                //{
                //    BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - EndingAngle, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(EndingAngle);
                //}
                //else
                //{
                //    BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - theta_Op, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(theta_Op);
                //}
                //System.Threading.Thread.Sleep(100);
            }

        }


        
        ///
        /// 3-1. Beam Alignment Rx
        ///
        //StartBeamAlignmentRx_Click
        /// 09/23

        private void StartBeamAlignmentRx_Click(object sender, EventArgs e)
        {

            string[] strArray0 = this.ValuesToWriteBoxR0.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]);
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    case 5:
                        Iteration = Convert.ToInt32(strArray0[index]);
                        break;
                    default: break;
                }

            }
            //double[] Time_Align = new double[Iteration];

            //이상으로 각 줄 읽어들임.
            // 빔 갯수 계산

            int endtime = 30;


            int numBeam = 1 + ((EndingAngle - StartingAngle) / Resolution);


            var mmf_NewBeam = MemoryMappedFile.OpenExisting("NB");
            var accessor_NewBeam = mmf_NewBeam.CreateViewAccessor();

            var mmf_PowerOfBeam = MemoryMappedFile.OpenExisting("POB");
            var accessor_PowerOfBeam = mmf_PowerOfBeam.CreateViewAccessor();

            string ip_server = "192.168.0.221";
            int port = 10801;
            IPAddress ip_server_address = IPAddress.Parse(ip_server);

            //소켓생성
            Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.Connect(new IPEndPoint(ip_server_address, port));

            //int Opt_Txidx=0;
            //int Opt_Rxidx=0;
            int[] Opt_Idx = new int[2];
            int Opt;
            this.log("initialized..");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int iter = 0; iter < Iteration; iter++)
            do
            {
                //stopwatch.Restart();

                Opt_Idx = Exhaustive_Sweeping_Rx_handler(StartingAngle, EndingAngle, Resolution, TD,
                    Server, accessor_NewBeam, accessor_PowerOfBeam);
                Opt = StartingAngle + Resolution * Opt_Idx[1];
                BeamSweeping_GUI_Panel_R.Refresh();
                this.rotateBeamGUI(-90 - Opt, BeamSweeping_GUI_Panel_R);
                Beamforming_handler(Opt);

                //stopwatch.Stop();
                //this.log("Beam alignment using exhaustive sweeping takes : " + Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms)");
                //Time_Align[iter] = stopwatch.ElapsedMilliseconds;
                System.Threading.Thread.Sleep(10);
                this.log(Convert.ToString(stopwatch.ElapsedMilliseconds) + " (ms) : Beam Index is " + Convert.ToString(Opt_Idx[1]));
            } while (stopwatch.ElapsedMilliseconds < endtime*1000);

            string Report_Msg = "[" + Convert.ToString(2) + "," + Convert.ToString(Opt_Idx[0]) + "]"; // 여기서 txidx_opt 보냄
            Server.Send(Encoding.UTF8.GetBytes(Report_Msg));
            this.log("Experiment is done.");
            //double Time_Align_Mean = Time_Align.Average();
            //double Time_Align_Var = 0;
            //for (int iter = 0; iter < Iteration; iter++)
            //{
            //    Time_Align_Var = Time_Align_Var + Math.Pow(Time_Align[iter] - Time_Align_Mean, 2);
            //}
            //Time_Align_Var = Time_Align_Var / (Iteration - 1);
            //this.log("After " + Convert.ToString(Iteration) + " Iteration, mean of time to beam align is : " + Convert.ToString(Time_Align_Mean));
            //this.log("After " + Convert.ToString(Iteration) + " Iteration, variance of time to beam align is : " + Convert.ToString(Time_Align_Var));
        }
        
        
        ///
        /// 3-2. Beam Alignment Rx Using Location-Info
        ///
        //StartBeamAlignmentRx_Click
        /// 09/23

        private void StartBeamAlignmentRx_using_Loc_info_Click(object sender, EventArgs e)
        {

            string[] strArray0 = this.ValuesToWriteBoxR0.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]);
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    default: break;
                }
            }


            int endtime = 300;

            var mmf_RxPos = MemoryMappedFile.OpenExisting("RxPos");
            var accessor_RxPos = mmf_RxPos.CreateViewAccessor();

            double TxPosX = 0;
            double TxPosY = 0;

            double RxPosX;
            double RxPosY;
            int[] Selected = new int[2];


            var mmf_NewBeam = MemoryMappedFile.OpenExisting("NB");
            var accessor_NewBeam = mmf_NewBeam.CreateViewAccessor();

            var mmf_PowerOfBeam = MemoryMappedFile.OpenExisting("POB");
            var accessor_PowerOfBeam = mmf_PowerOfBeam.CreateViewAccessor();
            
            float NB = 0;
            bool BPOB; float isPOB = 0;
            double[] POB = new double[1];

            //double[,] POB = new double[num_TxBeam, num_RxBeam];

            ////string ip_server = "192.168.10.251";
            ////int port = 10802; // location 말고 빔을 주고받을 포트.
            ////IPAddress ip_server_address = IPAddress.Parse(ip_server);

            ////소켓생성
            //Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Server.Connect(new IPEndPoint(ip_server_address, port));// Tx C#과 연결

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            stopwatch.Restart();

            //while (true)
            this.log("Initialized");
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"test012.csv");
            file.WriteLine("{0},{1},{2},{3},{4}", "Timestamp", "Pos_X", "Pos_Y", "AoA/AoD","RSS");
            do
                {
                var RxPosRaw = new byte[24];
                accessor_RxPos.ReadArray<byte>(0, RxPosRaw, 0, RxPosRaw.Length);
                string RxPosDec = System.Text.Encoding.UTF8.GetString(RxPosRaw);

                string[] RxPos_raw = RxPosDec.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string RxPos_rev = string.Join(" ", RxPos_raw);

                string[] RxPos = RxPos_rev.Split('[', ' ', ']');

                RxPosX = Convert.ToDouble(RxPos[1]);
                RxPosY = Convert.ToDouble(RxPos[2]);
                this.log("Position here is : " + Convert.ToString(RxPosX) + "," + Convert.ToString(RxPosY));
                theta_Op = Convert.ToInt32(Math.Round(-(180 / Math.PI * (Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX)))) - 90) / Resolution)) * Resolution;

                if (theta_Op <= StartingAngle)
                {
                    theta_Op = StartingAngle;
                }
                else if (theta_Op >= EndingAngle)
                {
                    theta_Op = EndingAngle;
                }
                ////int theta_idx = (theta_Op - StartingAngle) / Resolution;
                ////Server.Send(Encoding.UTF8.GetBytes("[" + Convert.ToString(1) + "," + Convert.ToString(theta_idx) + "]"));

                Beamforming_handler(theta_Op);
                BeamSweeping_GUI_Panel_R.Refresh();
                this.rotateBeamGUI(-90 - theta_Op, BeamSweeping_GUI_Panel_R);

                #region
                NB = 1;
                accessor_NewBeam.Write(0, ref NB);

                int NB_out;
                accessor_NewBeam.Read(0, out NB_out);
                System.Threading.Thread.Sleep(TD);
                // 이제 전력 값을 제대로 들어있을 때 까지 가져옴.
                accessor_PowerOfBeam.Read(0, out isPOB);
                float tempPOB = isPOB;
                BPOB = Convert.ToBoolean(isPOB);
                //this.log("here5");
                while (true)
                {
                    if (BPOB)
                    {
                        POB[0] = isPOB;
                        isPOB = 0;
                        accessor_PowerOfBeam.Write(0, ref isPOB);
                        NB = 0;
                        accessor_NewBeam.Write(0, ref NB);
                        break;
                    }
                    // 제대로 안들어있으면 쉬고 다시 가져옴.
                    else
                    {
                        NB = 1;
                        accessor_NewBeam.Write(0, ref NB);
                        //Accessor_NewBeam.Read(0, out NB_out);
                        //this.log("NB_out is : " + NB_out);
                        System.Threading.Thread.Sleep(TD);
                        accessor_PowerOfBeam.Read(0, out isPOB);
                        tempPOB = isPOB;
                        BPOB = Convert.ToBoolean(isPOB);
                    }
                }
                this.log("POB is : " + POB[0]);


                //file.WriteLine("{0},{1},{2},{3},{4}", "Timestamp", "Pos_X", "Pos_Y", "AoA/AoD", "RSS");
                #endregion

                //위는 Power
                this.log("log has been written..");
                file.WriteLine("{0},{1},{2},{3},{4}",
                    Convert.ToString(stopwatch.ElapsedMilliseconds),
                    Convert.ToString(RxPosX), Convert.ToString(RxPosY),
                    Convert.ToString(theta_Op), Convert.ToString(POB[0]));


            } while (stopwatch.ElapsedMilliseconds < endtime * 1000);
            stopwatch.Stop();

            //string Report_Msg = "[" + Convert.ToString(2) + "," + Convert.ToString(Selected[0]) + "]"; // 여기서 txidx_opt 보냄
            //Server.Send(Encoding.UTF8.GetBytes(Report_Msg));
            this.log("Experiment is done.");
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        ///
        /// Start SIMO BeamTracking with PF
        ///
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////


        private void Start_SIMO_BeamTracking_with_PF(object sender, EventArgs e)
        {
            #region
            string[] strArray0 = this.ValuesToWirteBoxPF01.Text.Split('\n');
            for (int index = 0; index < strArray0.Length; ++index)
            {
                strArray0[index] = strArray0[index].Trim();
                switch (index)
                {
                    case 0:
                        StartingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 1:
                        EndingAngle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 2:
                        Resolution = Convert.ToInt32(strArray0[index]);
                        break;
                    case 3:
                        TD = Convert.ToInt32(strArray0[index]);
                        break;
                    case 4:
                        RegInfo = strArray0[index];
                        break;
                    case 5:
                        Init_AOA = Convert.ToInt32(strArray0[index]);
                        break;
                    case 6:
                        Var_alpha = Convert.ToSingle(strArray0[index]);
                        break;
                    case 7:
                        Var_AOA = Convert.ToSingle(strArray0[index]);
                        break;
                    case 8:
                        Num_particle = Convert.ToInt32(strArray0[index]);
                        break;
                    case 9:
                        N_threshold = Convert.ToInt32(strArray0[index]);
                        break;
                    default: break;
                }
            }
            #endregion 
            //읽어오기!!

            // Exhaustive Search 가정???
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            // 방향 알았다고 치고, \alpha 추정해야지. 기다리기!!
            Init_AOA = Convert.ToInt32(System.Math.Round(  Convert.ToSingle(Init_AOA / Resolution)  ) * Resolution);
            Beamforming_handler(Init_AOA);
            BeamTracking_GUI_Panel_SIMO.Refresh();
            this.rotateBeamGUI(-90 - Init_AOA, BeamTracking_GUI_Panel_SIMO);
            System.Threading.Thread.Sleep(1000); // 1초면 알파 나오겠지?
            
            var mmf_ReceivedPilot_h = MemoryMappedFile.OpenExisting("ReceivedPilot");
            var accessor_ReceivedPilot_h = mmf_ReceivedPilot_h.CreateViewAccessor();
            
            float[] h = new float[2] ;
            //float h_real, h_imag;
            System.Numerics.Complex h_complex;
            accessor_ReceivedPilot_h.ReadArray(0, h, 0, 2);
            //System.Numerics.Complex Init_h = new System.Numerics.Complex(h[0], h[1]);
            // 실제 마주본 상태로, 90도에서 시작하는 가정을 그대로 구현. 안테나 갯수는 현재 8로 고정
            System.Numerics.Complex Init_alpha = new System.Numerics.Complex(h[0] / 8, h[1] / 8) ; // 현재 M_R은 8
            
            // Particle Filter 실행
            // *** Initializing ***
            float[][] x_k_1 = new float[3][];
            x_k_1[0] = JYNormalRandomVariables_Init(Convert.ToSingle(Init_alpha.Real), 0, Num_particle); 
            x_k_1[1] = JYNormalRandomVariables_Init(Convert.ToSingle(Init_alpha.Imaginary), 0, Num_particle); 
            x_k_1[2] = JYNormalRandomVariables_Init(Init_AOA, 0, Num_particle);
            float[][] x_k = new float[3][]; // 가변배열. 속도 손해볼 듯 한데.. 다른 방법 없나?

            float[,] x_k_resamp = new float[3,Num_particle];

            float[] w_k_1 = Enumerable.Repeat<float>(Convert.ToSingle(1.0/Num_particle), Num_particle).ToArray<float>();
            
            float[] w_k = new float[Num_particle];
            //float[] w_k_2 = new float[Num_particle];

            int thetabar = Init_AOA; double w_sum; float N_eff = 0; float[] CSW = new float[Num_particle]; float CSW_dummy;
            double tracking_theta=0; float tracking_alpha_real; float tracking_alpha_imag; float MCMC_alpha;
            //float theta_radian;
            System.Random myrand = new System.Random();
            
            #region

            //while(true)
            int Num_loop = 100; float[,] h_accum = new float[2, Num_loop];
            float[,] tracking_alpha_accum = new float[2, Num_loop];

            for (int loop_idx = 0; loop_idx < Num_loop; loop_idx++)
            {
                // h값 읽어오고
                accessor_ReceivedPilot_h.ReadArray(0, h, 0, 2);
                h_complex = new System.Numerics.Complex(h[0], h[1]);
                h_accum[0, loop_idx] = h[0];
                h_accum[1, loop_idx] = h[1];

                //this.log(loop_idx + "-th h_complex is : " + h_complex.Real + "  +  j " + h_complex.Imaginary);
                this.log(loop_idx + "-th h_complex_Mag is : " + h_complex.Magnitude);
                this.log(loop_idx + "-th h_complex_Phase is : " + h_complex.Phase * 180 / Math.PI);

                do
                {
                    x_k[0] = JYNormalRandomVariables(x_k_1[0], Var_alpha, Num_particle);
                    x_k[1] = JYNormalRandomVariables(x_k_1[1], Var_alpha, Num_particle);
                    x_k[2] = JYNormalRandomVariables(x_k_1[2], Var_AOA, Num_particle);
                    // 각 x_k[n][s]는 x_k_1[n][s]를 평균으로하는 normal random variable

                    // *** Update Weights ***
                    for (int s = 0; s < Num_particle; s++)
                    {
                        w_k[s] = Likelihood_Particle_Complex(h_complex, Measurement_Function(x_k[0][s], x_k[1][s], x_k[2][s], thetabar, 8)) * w_k_1[s];
                    }

                    // *** Normalization ***
                    w_sum = w_k.Sum();
                    w_k = JYMultiplyConstToArray(Convert.ToSingle(1.0 / w_sum), w_k);
                    if (double.IsNaN(w_sum))
                    {
                        this.log("Right after normalization. w_sum is : " + w_sum);
                    }
                } while (double.IsNaN(w_sum));

                // *** Calculate N_eff ***
                N_eff = 0; CSW_dummy = 0;
                // Accumulating w square-sum, for N_eff
                for (int s = 0; s < Num_particle; s++)
                {
                    N_eff += Convert.ToSingle(Math.Pow(w_k[s], 2));
                    CSW_dummy += w_k[s];
                    CSW[s] = CSW_dummy;
                }

                float idx_particle_normalized; int idx_peak;
                // *** Decision whether resample, or not ***
                //this.log(loop_idx + "-th loop N_eff is " + (1.0 / N_eff));
                if ((1.0/N_eff) < N_threshold)
                {
                    this.log(loop_idx + "-th loop performs the resampling process");
                    for (int s = 0; s < Num_particle; s++)
                    {
                        idx_particle_normalized = Convert.ToSingle(s + myrand.NextDouble()) / Num_particle;
                        idx_peak = s;
                        if (CSW[s] < idx_particle_normalized)
                        {
                            while (CSW[idx_peak] < idx_particle_normalized)
                            {
                                if(idx_peak < Num_particle-1)
                                {
                                    idx_peak++;
                                    idx_particle_normalized = Convert.ToSingle(idx_peak + myrand.NextDouble()) / Num_particle;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            while (CSW[idx_peak] > idx_particle_normalized)
                            {
                                if (idx_peak > 0)
                                {
                                    idx_peak--;
                                    idx_particle_normalized = Convert.ToSingle(idx_peak + myrand.NextDouble()) / Num_particle;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            idx_peak++;
                        }

                        x_k_resamp[0,s] = JYNormalRandomVariables_one(x_k[0][idx_peak], Var_alpha);
                        x_k_resamp[1,s] = JYNormalRandomVariables_one(x_k[1][idx_peak], Var_alpha);
                        x_k_resamp[2,s] = JYNormalRandomVariables_one(x_k[2][idx_peak], Var_AOA);

                        MCMC_alpha = System.Math.Min(1,
                            (Likelihood_Particle_Complex(h_complex, Measurement_Function(x_k_resamp[0,s], x_k_resamp[1,s], x_k_resamp[2,s], thetabar, 8))
                            * Likelihood_Particle(x_k_resamp[0,s], x_k_resamp[1,s], x_k_resamp[2,s], x_k_1[0][s], x_k_1[1][s], x_k_1[2][s], Var_alpha, Var_alpha, Var_AOA))
                            /
                            //여기 아래 고침. 03.18.
                            (Likelihood_Particle_Complex(h_complex, Measurement_Function(x_k[0][idx_peak], x_k[1][idx_peak], x_k[2][idx_peak], thetabar, 8))
                            * Likelihood_Particle(x_k[0][idx_peak], x_k[1][idx_peak], x_k[2][idx_peak], x_k_1[0][s], x_k_1[1][s], x_k_1[2][s], Var_alpha, Var_alpha, Var_AOA)));
                        if (myrand.NextDouble() < MCMC_alpha)
                        {
                            x_k_1[0][s] = x_k_resamp[0,s]; x_k_1[1][s] = x_k_resamp[1,s]; x_k_1[2][s] = x_k_resamp[2,s];
                            // 실제로 좌변의 의미는 x_k_prediction이지만, 메모리 최적화를 위해 이렇게 수행한다.
                        }
                        else
                        {
                            x_k_1[0][s] = x_k[0][idx_peak]; x_k_1[1][s] = x_k[1][idx_peak]; x_k_1[2][s] = x_k[2][idx_peak];
                        }
                        w_k[s] = Convert.ToSingle(1.0 / Num_particle);
                    }
                }
                else // *** Don't resampling ***
                {
                    x_k_1[0] = x_k[0]; x_k_1[1] = x_k[1]; x_k_1[2] = x_k[2];
                }
                w_k_1 = w_k;
                for (int s = 0; s < Num_particle; s++)
                {
                    if (float.IsNaN(w_k_1[s]))
                    {
                        this.log("Right after final weight update." + loop_idx + "-th loop " + s + "-th weight is NaN. : " + w_k[s]);
                        this.log("w_k_1[s] is : " + w_k_1[s]);
                        this.log("Measurement_Function is : " + Measurement_Function(x_k[0][s], x_k[1][s], x_k[2][s], thetabar, 8));
                        this.log("Likelihood_Particle_Complex is : " + Likelihood_Particle_Complex(h_complex, Measurement_Function(x_k[0][s], x_k[1][s], x_k[2][s], thetabar, 8)));
                        return;
                    }
                }
                // *** state update ***

                tracking_alpha_real = (JYElementwiseMultiplyTwoFloatArrasy(x_k_1[0], w_k_1)).Sum();
                tracking_alpha_imag = (JYElementwiseMultiplyTwoFloatArrasy(x_k_1[1], w_k_1)).Sum();
                tracking_alpha_accum[0, loop_idx] = tracking_alpha_real;
                tracking_alpha_accum[1, loop_idx] = tracking_alpha_imag;
                tracking_theta = - (JYElementwiseMultiplyTwoFloatArrasy(x_k_1[2], w_k_1)).Sum();

                if (double.IsNaN(tracking_theta))
                {
                    this.log(loop_idx + "-th loop tracking_theta is NaN. : " + tracking_theta);
                    for (int s = 0; s < Num_particle/100; s++)
                        this.log(s + "-th states and weight are : " + x_k_1[0][s] + "," + x_k_1[1][s] + "," + x_k_1[2][s] + "," + w_k_1[s]);
                    return;
                }
                this.log(loop_idx + "-th tracking_theta is : " + tracking_theta);

                thetabar = Convert.ToInt32(System.Math.Round(tracking_theta / Resolution) * Resolution); // 이게 theta
                if (thetabar > 60)
                {
                    thetabar = 60;
                }
                else if (thetabar < -60)
                {
                    thetabar = -60;
                }
                this.log(loop_idx + "-th steering_angle is : " + thetabar);

                //Beamforming_handler(thetabar);
                BeamTracking_GUI_Panel_SIMO.Refresh();
                this.rotateBeamGUI(-90 - thetabar, BeamTracking_GUI_Panel_SIMO);
                thetabar = 0;

                //thetabar = Convert.ToInt32( System.Math.Round(tracking_theta / Resolution) * Resolution ); // 이게 theta
                //if(thetabar > 60)
                //{
                //    thetabar = 60;
                //}
                //else if(thetabar< -60)
                //{
                //    thetabar = -60;
                //}
                //this.log(loop_idx + "-th steering_angle is : " + thetabar);
                //Beamforming_handler(thetabar);
                //BeamTracking_GUI_Panel_SIMO.Refresh();
                //this.rotateBeamGUI(-90 - thetabar, BeamTracking_GUI_Panel_SIMO);
                

                //System.Threading.Thread.Sleep(100);
            }
            #endregion // Main Loop

            double alpha_accum_sum_0 = 0;
            for (int s = 0; s < Num_loop; s++)
            {
                alpha_accum_sum_0 += tracking_alpha_accum[0, s];
            }
            double alpha_accum_mean_0 = alpha_accum_sum_0 / Num_particle;
            double alpha_accum_var_0 = 0;
            for (int s = 0; s < Num_loop; s++)
            {
                alpha_accum_var_0 += Convert.ToSingle(Math.Pow((alpha_accum_mean_0 - tracking_alpha_accum[0, s]), 2));
            }
            alpha_accum_var_0 = alpha_accum_var_0 / Num_particle;
            this.log("alpha_accum_mean_0 is : " + alpha_accum_mean_0 + ", and alpha_accum_var_0 is : " + alpha_accum_var_0);
            //double h_accum_sum_0 = 0;
            //for (int s = 0; s < Num_loop; s++)
            //{
            //    h_accum_sum_0 += h_accum[0, s];
            //}
            //double h_accum_mean_0 = h_accum_sum_0 / Num_particle;
            //double h_accum_var_0 = 0;
            //for (int s = 0; s < Num_loop; s++)
            //{
            //    h_accum_var_0 += Convert.ToSingle(Math.Pow((h_accum_mean_0 - h_accum[0, s]),2))  ;
            //}
            //h_accum_var_0 = h_accum_var_0 / Num_particle;
            //this.log("h_accum_mean_0 is : " + h_accum_mean_0 + ", and h_accum_var_0 is : " + h_accum_var_0);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////

        public float JYNormalRandomVariables_one(float mean, float sigma)
        {
            Random rand = new Random(); //reuse this if you are generating many
            
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                            Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return Convert.ToSingle(mean + sigma * randStdNormal); //random normal(mean,stdDev^2)
        }
        public float[] JYNormalRandomVariables_Init(float mean, float sigma, int num_gen)
        {
            Random rand = new Random(); //reuse this if you are generating many
            float[] randNormal = new float[num_gen];

            for (int idx = 0; idx < num_gen; idx++)
            {
                double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                randNormal[idx] = Convert.ToSingle(mean + sigma * randStdNormal); //random normal(mean,stdDev^2)
            }
            return randNormal;
        }
        public float[] JYNormalRandomVariables(float[] mean, float sigma, int num_gen)
        {
            Random rand = new Random(); //reuse this if you are generating many
            float[] randNormal = new float[num_gen];

            for (int idx=0; idx<num_gen; idx++)
            {
                double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                randNormal[idx] = Convert.ToSingle(mean[idx] + sigma * randStdNormal); //random normal(mean,stdDev^2)
            }
            return randNormal;
        }

        public static float[] JYAddTwoFloatArrays(float[] a, float[] b)
        {
            return a.Zip(b, (x, y) => x + y).ToArray();
        }
        public static float[] JYAddConstToArray(float a, float[] b)
        {
            for (int idx = 0; idx < b.Length; idx++)
            {
                b[idx] = Convert.ToSingle(b[idx] + a);
            }
            return b;
        }

        public static float[] JYSubstractTwoFloatArrays(float[] a, float[] b)
        {
            return a.Zip(b, (x, y) => x - y).ToArray();
        }
        public static float[] JYSubstractConstFromArrays(float[] a, float b)
        {
            for (int idx = 0; idx < a.Length; idx++)
            {
                a[idx] = Convert.ToSingle(a[idx] - b);
            }
            return a;
        }

        public static float[] JYElementwiseMultiplyTwoFloatArrasy(float[] a, float[] b)
        {
            return a.Zip(b, (x, y) => x * y).ToArray();
        }
        public static float[] JYMultiplyConstToArray(float a, float[] b)
        {
            for(int idx=0; idx< b.Length; idx++)
            {
                b[idx] = Convert.ToSingle(b[idx] * a);
            }
            return b;
        }

        public float Likelihood_Particle_Complex(System.Numerics.Complex input, System.Numerics.Complex mean) 
            // 원래는 input이 벡터. 근데 snapthot 1개라서 value
        {
            System.Numerics.Complex Temp = System.Numerics.Complex.Subtract(input, mean);
            return Convert.ToSingle(Math.Exp(
                -1.0 / 16 * System.Math.Pow(System.Numerics.Complex.Abs(Temp),2) 
                ));
        } // -0.5 * 1/8
        public float Likelihood_Particle(float input1, float input2, float input3, float mean1, float mean2, float mean3,
            float var1, float var2, float var3) // 원래는 input이 벡터. 근데 snapthot 1개라서 value
        {

            return Convert.ToSingle(  
                System.Math.Exp((-0.5) * ( Math.Pow((input1 - mean1),2)/var1 + Math.Pow((input2 - mean2), 2)/var2 + Math.Pow((input3 - mean3), 2)/var3 ) ) 
                /
                System.Math.Sqrt( Math.Pow(2*Math.PI,3) *var1*var2*var3  ) 
                );
        } // -0.5 * 1/8

        public System.Numerics.Complex Measurement_Function(float alpha_s_real, float alpha_s_imag, float theta, float thetabar, int M_R)
        {
            // thetabar는 내가 받아오는 내값, theta는 추정하고자 하는 3번째 state x[2]
            if(theta==thetabar)
            {
                return new System.Numerics.Complex(alpha_s_real,alpha_s_imag) *M_R;
            }
            else
            {
                theta = theta * Convert.ToSingle(Math.PI) / 180;
                thetabar = thetabar * Convert.ToSingle(Math.PI) / 180;
                //Math.Cos();
                //Math.Sin();
                //M_R* Math.PI * (Math.Sin(theta) - Math.Sin(thetabar))
                //Math.PI * (Math.Sin(theta) - Math.Sin(thetabar))
                //System.Numerics.Complex denominator = new System.Numerics.Complex(
                //    1.0 - Math.Cos( M_R * Math.PI * ( Math.Sin(theta) - Math.Sin(thetabar) ) ), 
                //    Math.Sin( M_R * Math.PI * ( Math.Sin(theta) - Math.Sin(thetabar) ) )   );

                //System.Numerics.Complex numerator = new System.Numerics.Complex(
                //    1.0 - Math.Cos( Math.PI * ( Math.Sin(theta) - Math.Sin(thetabar) ) ),
                //    Math.Sin( Math.PI * ( Math.Sin(theta) - Math.Sin(thetabar) ) )   );
                System.Numerics.Complex denominator = new System.Numerics.Complex(
                    1.0 - Math.Cos(M_R * Math.PI * (Math.Sin(thetabar) - Math.Sin(theta))),
                    Math.Sin(M_R * Math.PI * (Math.Sin(thetabar) - Math.Sin(theta))));

                System.Numerics.Complex numerator = new System.Numerics.Complex(
                    1.0 - Math.Cos(Math.PI * (Math.Sin(thetabar) - Math.Sin(theta))),
                    Math.Sin(Math.PI * (Math.Sin(thetabar) - Math.Sin(theta))));

                return System.Numerics.Complex.Multiply(new System.Numerics.Complex(alpha_s_real, alpha_s_imag), System.Numerics.Complex.Divide(denominator, numerator));
            }
        }


        
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////




        //BeamSweeping Graphic By Hyunsoo
        private void rotateBeamGUI(float angle, Panel panel)
        {
            /*
             * @ -------- 0도
             *
             * @ 시계방향으로 +각도
             *
             */
            int Height = 50;
            int Width = 200;
            //double angle_rad = Math.PI * angle / 180;

            //Panel 에서의 위치 + Panel의 기준점 위치
            Point p_Center = new Point(200, 280);//+ new Size(730, 8);
            Graphics g = panel.CreateGraphics();
            //g.ScaleTransform(scale, scale);
            g.TranslateTransform(p_Center.X, p_Center.Y);
            g.RotateTransform(angle);
            g.FillEllipse(Brushes.Black, new Rectangle(0, -Height / 2, Width, Height));

        }


        private void ConnectSDPButton_Click(object sender, EventArgs e)
        {
            this.Connect_SDP();
            this.populate_block(true);
        }

        private void ConnectSDPButton_Click2(object sender, EventArgs e)
        {
            this.Connect_SDP2();
            this.populate_block2(true);
        }

        private void ConnectDeviceButton_Click(object sender, EventArgs e)
        {
            this.Connect_SDP();
        }

        private void ConnectDeviceButton_Click2(object sender, EventArgs e)
        {
            this.Connect_SDP2();
        }

        private void R0Button_Click(object sender, EventArgs e)
        {
            uint data = 0;
            try
            {
                data = Convert.ToUInt32(this.R0Box.Text, 16);
            }
            catch
            {
            }
            this.WriteToDevice(data);
        }

        private void configQuiet(SdpConnector connector, uint quietParam)
        {
            try
            {
                Main_Form.sdp.configQuiet(Main_Form.sdp.ID1Connector, quietParam);
            }
            catch (Exception ex)
            {
                if (!(ex is SdpApiErrEx) || (ex as SdpApiErrEx).number != SdpApiErr.FunctionNotSupported)
                    throw ex;
                if (!this.messageShownToUser && !(ex.Message.Substring(17) == "SDPS: Quiet mode not supported"))
                    throw ex;
            }
        }

        private void configNormal(SdpConnector connector, uint quietParam)
        {
            try
            {
                Main_Form.sdp.configNormal(Main_Form.sdp.ID1Connector, quietParam);
            }
            catch (Exception ex)
            {
                if (!(ex is SdpApiErrEx) || (ex as SdpApiErrEx).number != SdpApiErr.FunctionNotSupported)
                    throw ex;
                if (!this.messageShownToUser && !(ex.Message.Substring(17) == "SDPS: Quiet mode not supported"))
                    throw ex;
            }
        }
        private void configNormal2(SdpConnector connector, uint quietParam)
        {
            try
            {
                Main_Form.sdp2.configNormal(Main_Form.sdp2.ID1Connector, quietParam);
            }
            catch (Exception ex)
            {
                if (!(ex is SdpApiErrEx) || (ex as SdpApiErrEx).number != SdpApiErr.FunctionNotSupported)
                    throw ex;
                if (!this.messageShownToUser && !(ex.Message.Substring(17) == "SDPS: Quiet mode not supported"))
                    throw ex;
            }
        }
        private void SaveConfigurationStripMenuItem_Click(object sender, EventArgs e)
        {
            this.log("Saving configuration...");
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            savefile.Title = "Save a configuration file";
            savefile.FileName = "ADAR1000_settings.txt";
            string str = "ADAR1000";
            savefile.FileName = str + "_software_settings_v" + this.version + ".txt";
            DialogResult dialogResult = savefile.ShowDialog();
            File.WriteAllText(savefile.FileName, "");
            this.SaveControls_Checkboxes((Control)this.tabControl1, ref savefile);
            this.SaveControls_EverythingElse((Control)this.tabControl1, ref savefile);
            if (dialogResult == DialogResult.OK)
                this.log("Configuration saved.");
            else
                this.log("Configuration not saved.");
            savefile.Dispose();
        }

        private void LoadConfigurationStripMenuItem_Click(object sender, EventArgs e)
        {
            this.log("Loading configuration...");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
                this.LoadControls((Control)this.tabControl1, File.ReadAllLines(openFileDialog.FileName));
            openFileDialog.Dispose();
            if (dialogResult == DialogResult.OK)
                this.log("Configuration loaded.");
            else
                this.log("Configuration not loaded.");
        }

        private void SaveControls_Checkboxes(Control ctrl, ref SaveFileDialog savefile)
        {
            foreach (Control control in (ArrangedElementCollection)ctrl.Controls)
            {
                if (control is CheckBox)
                    File.AppendAllText(savefile.FileName, "CheckBox: " + control.Name + ".Checked = " + ((CheckBox)control).Checked.ToString() + "\r\n");
                else if (control.Controls.Count > 0)
                    this.SaveControls_Checkboxes(control, ref savefile);
            }
        }

        private void SaveControls_EverythingElse(Control ctrl, ref SaveFileDialog savefile)
        {
            foreach (Control control in (ArrangedElementCollection)ctrl.Controls)
            {
                if (control is TextBox || control is RadioButton || control is NumericUpDown || control is ComboBox)
                {
                    if (control is TextBox)
                        File.AppendAllText(savefile.FileName, "TextBox: " + control.Name + ".Text = " + control.Text + "\r\n");
                    if (control is RadioButton)
                        File.AppendAllText(savefile.FileName, "RadioButton: " + control.Name + ".Checked = " + ((RadioButton)control).Checked.ToString() + "\r\n");
                    if (control is NumericUpDown)
                        File.AppendAllText(savefile.FileName, "NumericUpDown: " + control.Name + ".Value = " + (object)((NumericUpDown)control).Value + "\r\n");
                    if (control is ComboBox)
                        File.AppendAllText(savefile.FileName, "ComboBox: " + control.Name + ".SelectedIndex = " + (object)((ListControl)control).SelectedIndex + "\r\n");
                }
                else if (control.Controls.Count > 0)
                    this.SaveControls_EverythingElse(control, ref savefile);
            }
        }

        private void LoadControls(Control ctrl, string[] contents)
        {
            for (int index = 0; index < contents.Length; ++index)
            {
                if (contents[index] != null)
                {
                    string[] strArray = contents[index].Split('.', ' ', ':');
                    Control control = new Control();
                    Control controlByName = this.GetControlByName(strArray[2], ctrl);
                    if (strArray.Length == 7)
                        strArray[5] = strArray[5] + "." + strArray[6];
                    try
                    {
                        if (strArray[0] == "TextBox")
                            controlByName.Text = strArray[5];
                        else if (strArray[0] == "CheckBox")
                            ((CheckBox)controlByName).Checked = Convert.ToBoolean(strArray[5]);
                        else if (strArray[0] == "NumericUpDown")
                            ((NumericUpDown)controlByName).Value = Convert.ToDecimal(strArray[5]);
                        else if (strArray[0] == "RadioButton")
                            ((RadioButton)controlByName).Checked = Convert.ToBoolean(strArray[5]);
                        else if (strArray[0] == "ComboBox")
                            ((ListControl)controlByName).SelectedIndex = (int)Convert.ToInt16(strArray[5]);
                    }
                    catch
                    {
                        this.log("Control " + strArray[2] + " not found. Probably removed. Continuing...");
                    }
                    control = (Control)null;
                }
            }
        }

        private Control GetControlByName(string Name, Control ctrl)
        {
            foreach (Control control1 in (ArrangedElementCollection)ctrl.Controls)
            {
                if (control1 is TextBox || control1 is CheckBox || (control1 is NumericUpDown || control1 is RadioButton) || control1 is ComboBox)
                {
                    if (control1.Name == Name)
                        return control1;
                }
                else if (control1.Controls.Count > 0)
                {
                    Control control2 = new Control();
                    Control controlByName = this.GetControlByName(Name, control1);
                    if (controlByName != null)
                        return controlByName;
                }
            }
            return (Control)null;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)MessageBox.Show("Analog Devices ADAR1000 software - " + this.version + " - " + this.version_date);
        }

        private void Register_Write(uint sender)
        {
            this.session.sclkFrequency = 500 * 1000;
            uint[] writeData = new uint[1]
            {
        (uint) (((int) this.ADDR0 << 21) + ((int) this.ADDR1 << 22)) + sender
            };
            this.session.writeU24(writeData);
            this.log(writeData[0].ToString("X5") + " written to part");
        }

        private void ADDR_Check_Changed(object sender, EventArgs e)
        {
            this.ADDR0 = Convert.ToUInt32(this.ADDR0_checkBox.Checked ? 1 : 0);
            this.ADDR1 = Convert.ToUInt32(this.ADDR1_checkBox.Checked ? 1 : 0);
        }

        private void TXGain_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.Gain1_Value.Value);
            uint uint32_2 = Convert.ToUInt32(this.CH1_ATTN_TX.Checked ? 0 : 1);
            uint uint32_3 = Convert.ToUInt32(this.Gain2_Value.Value);
            uint uint32_4 = Convert.ToUInt32(this.CH2_ATTN_TX.Checked ? 0 : 1);
            uint uint32_5 = Convert.ToUInt32(this.Gain3_Value.Value);
            uint uint32_6 = Convert.ToUInt32(this.CH3_ATTN_TX.Checked ? 0 : 1);
            uint uint32_7 = Convert.ToUInt32(this.Gain4_Value.Value);
            uint uint32_8 = Convert.ToUInt32(this.CH4_ATTN_TX.Checked ? 0 : 1);
            this.TXGain[0] = (uint)((int)uint32_1 + ((int)uint32_2 << 7) + 7168);
            this.TXGain[1] = (uint)((int)uint32_3 + ((int)uint32_4 << 7) + 7424);
            this.TXGain[2] = (uint)((int)uint32_5 + ((int)uint32_6 << 7) + 7680);
            this.TXGain[3] = (uint)((int)uint32_7 + ((int)uint32_8 << 7) + 7936);
        }

        private void WriteTXGain_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.TXGain.Length; ++index)
                    this.Register_Write(this.TXGain[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RXGain_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.RXGain1.Value);
            uint uint32_2 = Convert.ToUInt32(this.RXGain1_Attenuation.Checked ? 0 : 1);
            uint uint32_3 = Convert.ToUInt32(this.RXGain2.Value);
            uint uint32_4 = Convert.ToUInt32(this.RXGain2_Attenuation.Checked ? 0 : 1);
            uint uint32_5 = Convert.ToUInt32(this.RXGain3.Value);
            uint uint32_6 = Convert.ToUInt32(this.RXGain3_Attenuation.Checked ? 0 : 1);
            uint uint32_7 = Convert.ToUInt32(this.RXGain4.Value);
            uint uint32_8 = Convert.ToUInt32(this.RXGain4_Attenuation.Checked ? 0 : 1);
            this.RXGain[0] = (uint)((int)uint32_1 + ((int)uint32_2 << 7) + 4096);
            this.RXGain[1] = (uint)((int)uint32_3 + ((int)uint32_4 << 7) + 4352);
            this.RXGain[2] = (uint)((int)uint32_5 + ((int)uint32_6 << 7) + 4608);
            this.RXGain[3] = (uint)((int)uint32_7 + ((int)uint32_8 << 7) + 4864);
        }

        private void WriteRXGain_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RXGain.Length; ++index)
                    this.Register_Write(this.RXGain[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_Phase_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.CH1_RX_Phase_I.Value);
            uint uint32_2 = Convert.ToUInt32(this.RX_VM_CH1_POL_I.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.CH1_RX_Phase_Q.Value);
            uint uint32_4 = Convert.ToUInt32(this.RX_VM_CH1_POL_Q.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.CH2_RX_Phase_I.Value);
            uint uint32_6 = Convert.ToUInt32(this.RX_VM_CH2_POL_I.Checked ? 1 : 0);
            uint uint32_7 = Convert.ToUInt32(this.CH2_RX_Phase_Q.Value);
            uint uint32_8 = Convert.ToUInt32(this.RX_VM_CH2_POL_Q.Checked ? 1 : 0);
            uint uint32_9 = Convert.ToUInt32(this.CH3_RX_Phase_I.Value);
            uint uint32_10 = Convert.ToUInt32(this.RX_VM_CH3_POL_I.Checked ? 1 : 0);
            uint uint32_11 = Convert.ToUInt32(this.CH3_RX_Phase_Q.Value);
            uint uint32_12 = Convert.ToUInt32(this.RX_VM_CH3_POL_Q.Checked ? 1 : 0);
            uint uint32_13 = Convert.ToUInt32(this.CH4_RX_Phase_I.Value);
            uint uint32_14 = Convert.ToUInt32(this.RX_VM_CH4_POL_I.Checked ? 1 : 0);
            uint uint32_15 = Convert.ToUInt32(this.CH4_RX_Phase_Q.Value);
            uint uint32_16 = Convert.ToUInt32(this.RX_VM_CH1_POL_Q.Checked ? 1 : 0);
            this.RXPhase[0] = (uint)((int)uint32_1 + ((int)uint32_2 << 5) + 5120);
            this.RXPhase[1] = (uint)((int)uint32_3 + ((int)uint32_4 << 5) + 5376);
            this.RXPhase[2] = (uint)((int)uint32_5 + ((int)uint32_6 << 5) + 5632);
            this.RXPhase[3] = (uint)((int)uint32_7 + ((int)uint32_8 << 5) + 5888);
            this.RXPhase[4] = (uint)((int)uint32_9 + ((int)uint32_10 << 5) + 6144);
            this.RXPhase[5] = (uint)((int)uint32_11 + ((int)uint32_12 << 5) + 6400);
            this.RXPhase[6] = (uint)((int)uint32_13 + ((int)uint32_14 << 5) + 6656);
            this.RXPhase[7] = (uint)((int)uint32_15 + ((int)uint32_16 << 5) + 6912);
        }

        private void WriteRXPhase_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RXPhase.Length; ++index)
                    this.Register_Write(this.RXPhase[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_Phase_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.CH1_TX_Phase_I.Value);
            uint uint32_2 = Convert.ToUInt32(this.TX_VM_CH1_POL_I.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.CH1_TX_Phase_Q.Value);
            uint uint32_4 = Convert.ToUInt32(this.TX_VM_CH1_POL_Q.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.CH2_TX_Phase_I.Value);
            uint uint32_6 = Convert.ToUInt32(this.TX_VM_CH2_POL_I.Checked ? 1 : 0);
            uint uint32_7 = Convert.ToUInt32(this.CH2_TX_Phase_Q.Value);
            uint uint32_8 = Convert.ToUInt32(this.TX_VM_CH2_POL_Q.Checked ? 1 : 0);
            uint uint32_9 = Convert.ToUInt32(this.CH3_TX_Phase_I.Value);
            uint uint32_10 = Convert.ToUInt32(this.TX_VM_CH3_POL_I.Checked ? 1 : 0);
            uint uint32_11 = Convert.ToUInt32(this.CH3_TX_Phase_Q.Value);
            uint uint32_12 = Convert.ToUInt32(this.TX_VM_CH3_POL_Q.Checked ? 1 : 0);
            uint uint32_13 = Convert.ToUInt32(this.CH4_TX_Phase_I.Value);
            uint uint32_14 = Convert.ToUInt32(this.TX_VM_CH4_POL_I.Checked ? 1 : 0);
            uint uint32_15 = Convert.ToUInt32(this.CH4_TX_Phase_Q.Value);
            uint uint32_16 = Convert.ToUInt32(this.TX_VM_CH1_POL_Q.Checked ? 1 : 0);
            this.TXPhase[0] = (uint)((int)uint32_1 + ((int)uint32_2 << 5) + 8192);
            this.TXPhase[1] = (uint)((int)uint32_3 + ((int)uint32_4 << 5) + 8448);
            this.TXPhase[2] = (uint)((int)uint32_5 + ((int)uint32_6 << 5) + 8704);
            this.TXPhase[3] = (uint)((int)uint32_7 + ((int)uint32_8 << 5) + 8960);
            this.TXPhase[4] = (uint)((int)uint32_9 + ((int)uint32_10 << 5) + 9216);
            this.TXPhase[5] = (uint)((int)uint32_11 + ((int)uint32_12 << 5) + 9472);
            this.TXPhase[6] = (uint)((int)uint32_13 + ((int)uint32_14 << 5) + 9728);
            this.TXPhase[7] = (uint)((int)uint32_15 + ((int)uint32_16 << 5) + 9984);
        }

        private void WriteTXPhase_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.TXPhase.Length; ++index)
                    this.Register_Write(this.TXPhase[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void OverrideRX_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1] { 10241U };
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void OverrideTX_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1] { 10242U };
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void Bias_ON_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.CH1PA_BIAS_ON.Value);
            uint uint32_2 = Convert.ToUInt32(this.CH2PA_BIAS_ON.Value);
            uint uint32_3 = Convert.ToUInt32(this.CH3PA_BIAS_ON.Value);
            uint uint32_4 = Convert.ToUInt32(this.CH4PA_BIAS_ON.Value);
            uint uint32_5 = Convert.ToUInt32(this.LNA_BIAS_ON.Value);
            this.BIAS_ON[0] = uint32_1 + 10496U;
            this.BIAS_ON[1] = uint32_2 + 10752U;
            this.BIAS_ON[2] = uint32_3 + 11008U;
            this.BIAS_ON[3] = uint32_4 + 11264U;
            this.BIAS_ON[4] = uint32_5 + 11520U;
        }

        private void Bias_ON_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.BIAS_ON.Length; ++index)
                    this.Register_Write(this.BIAS_ON[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void Bias_OFF_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.CH1PA_BIAS_OFF.Value);
            uint uint32_2 = Convert.ToUInt32(this.CH2PA_BIAS_OFF.Value);
            uint uint32_3 = Convert.ToUInt32(this.CH3PA_BIAS_OFF.Value);
            uint uint32_4 = Convert.ToUInt32(this.CH4PA_BIAS_OFF.Value);
            uint uint32_5 = Convert.ToUInt32(this.LNA_BIAS_OFF.Value);
            this.BIAS_OFF[0] = uint32_1 + 17920U;
            this.BIAS_OFF[1] = uint32_2 + 18176U;
            this.BIAS_OFF[2] = uint32_3 + 18432U;
            this.BIAS_OFF[3] = uint32_4 + 18688U;
            this.BIAS_OFF[4] = uint32_5 + 18944U;
        }

        private void Bias_OFF_Click_1(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.BIAS_OFF.Length; ++index)
                    this.Register_Write(this.BIAS_OFF[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void EN_BIAS_2_CheckChanged(object sender, EventArgs e)
        {
            this.RX_Enable_2[0] = (uint)(11776 + ((int)Convert.ToUInt32(this.CH1_RX_EN_2.Checked ? 1 : 0) << 6) + ((int)Convert.ToUInt32(this.CH2_RX_EN_2.Checked ? 1 : 0) << 5) + ((int)Convert.ToUInt32(this.CH3_RX_EN_2.Checked ? 1 : 0) << 4) + ((int)Convert.ToUInt32(this.CH4_RX_EN_2.Checked ? 1 : 0) << 3) + ((int)Convert.ToUInt32(this.RX_LNA_EN_2.Checked ? 1 : 0) << 2) + ((int)Convert.ToUInt32(this.RX_VM_EN_2.Checked ? 1 : 0) << 1)) + Convert.ToUInt32(this.RX_VGA_EN_2.Checked ? 1 : 0);
        }

        private void RX_enables_2_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RX_Enable_2.Length; ++index)
                    this.Register_Write(this.RX_Enable_2[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_EN_2_CheckedChanged(object sender, EventArgs e)
        {
            this.TX_Enable_2[0] = (uint)(12032 + ((int)Convert.ToUInt32(this.CH1_TX_EN_2.Checked ? 1 : 0) << 6) + ((int)Convert.ToUInt32(this.CH2_TX_EN_2.Checked ? 1 : 0) << 5) + ((int)Convert.ToUInt32(this.CH3_TX_EN_2.Checked ? 1 : 0) << 4) + ((int)Convert.ToUInt32(this.CH4_TX_EN_2.Checked ? 1 : 0) << 3) + ((int)Convert.ToUInt32(this.TX_DRV_EN_2.Checked ? 1 : 0) << 2) + ((int)Convert.ToUInt32(this.TX_VM_EN_2.Checked ? 1 : 0) << 1)) + Convert.ToUInt32(this.TX_VGA_EN_2.Checked ? 1 : 0);
        }

        private void TX_enables_2_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.TX_Enable_2.Length; ++index)
                    this.Register_Write(this.TX_Enable_2[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void MISC_EN_CheckedChanged(object sender, EventArgs e)
        {
            this.MISC_Enable[0] = (uint)(12288 + ((int)Convert.ToUInt32(this.BIAS_CTRL.Checked ? 1 : 0) << 6) + ((int)Convert.ToUInt32(this.LNA_BIAS_OUT_EN.Checked ? 1 : 0) << 4) + ((int)Convert.ToUInt32(this.CH1_DET_EN.Checked ? 1 : 0) << 3) + ((int)Convert.ToUInt32(this.CH2_DET_EN.Checked ? 1 : 0) << 2) + ((int)Convert.ToUInt32(this.CH3_DET_EN.Checked ? 1 : 0) << 1)) + Convert.ToUInt32(this.CH4_DET_EN.Checked ? 1 : 0);
        }

        private void MISC_EN_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
                this.Register_Write(this.MISC_Enable[0]);
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_Enable_toggle(object sender, EventArgs e)
        {
            if (this.TX_EN_2.Checked)
            {
                this.RX_EN_2.Checked = false;
                this.RX_EN_2.ThreeState = false;
                this.groupBox25.Enabled = false;
                this.groupBox24.Enabled = true;
            }
            else
                this.groupBox25.Enabled = true;
            this.SW_2_CheckedChanged((object)null, (EventArgs)null);
        }

        private void RX_Enable_toggle(object sender, EventArgs e)
        {
            if (this.RX_EN_2.Checked)
            {
                this.TX_EN_2.Checked = false;
                this.TX_EN_2.ThreeState = false;
                this.groupBox25.Enabled = true;
                this.groupBox24.Enabled = false;
            }
            else
                this.groupBox24.Enabled = true;
            this.SW_2_CheckedChanged((object)null, (EventArgs)null);
        }

        private void SW_2_CheckedChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.SW_DRV_TR_STATE_2.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.TX_EN_2.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.RX_EN_2.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.SW_DRV_EN_TR_2.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.SW_DRV_EN_POL_2.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.TR_SOURCE_2.Checked ? 1 : 0);
            uint uint32_7 = Convert.ToUInt32(this.TR_SPI_2.Checked ? 1 : 0);
            this.SW_CTRL_2[0] = (uint)((int)Convert.ToUInt32(this.POL_2.Checked ? 1 : 0) + ((int)uint32_7 << 1) + ((int)uint32_6 << 2) + ((int)uint32_5 << 3) + ((int)uint32_4 << 4) + ((int)uint32_3 << 5) + ((int)uint32_2 << 6) + ((int)uint32_1 << 7) + 12544);
        }

        private void SW_CTRL_2_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.SW_CTRL_2.Length; ++index)
                    this.Register_Write(this.SW_CTRL_2[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void ADC_CheckedChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.ADC_CLKFREQ_SEL.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.ADC_EN.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.CLK_EN.Checked ? 1 : 0);
            this.ADC_CTRL[0] = (uint)(((int)Convert.ToUInt32(this.MUX_SEL.SelectedIndex) << 1) + ((int)Convert.ToUInt32(this.ST_CONV.Checked ? 1 : 0) << 4) + ((int)uint32_3 << 5) + ((int)uint32_2 << 6) + ((int)uint32_1 << 7) + 12800);
        }

        private void ADC_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.ADC_CTRL.Length; ++index)
                    this.Register_Write(this.ADC_CTRL[index]);
                if ((this.ReadFromDevice(Convert.ToUInt32(50)) & 15U) != 1U)
                    return;
                this.ADC_EOC.BackColor = Color.Green;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void ADC_convertion_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint uint32 = Convert.ToUInt32(51);
                this.textBox4.Text = (this.ReadFromDevice(uint32) & (uint)byte.MaxValue).ToString("X");
                this.ADC_EOC.BackColor = Color.Red;
                this.log(uint32.ToString("X5") + " written to part");
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void BIAS_CURRENT_ValueChanged(object sender, EventArgs e)
        {
            Convert.ToUInt32(this.DRV_GAIN.Checked ? 1 : 0);
            uint uint32_1 = Convert.ToUInt32(this.TX_DRV_BIAS.Value);
            uint uint32_2 = Convert.ToUInt32(this.LNA_BIAS.Value);
            uint uint32_3 = Convert.ToUInt32(this.RX_VGA_BIAS2.Value);
            uint uint32_4 = Convert.ToUInt32(this.RX_VM_BIAS2.Value);
            uint uint32_5 = Convert.ToUInt32(this.TX_VGA_BIAS3.Value);
            uint uint32_6 = Convert.ToUInt32(this.TX_VM_BIAS3.Value);
            this.BIAS_CURRENT[0] = uint32_2 + 13312U;
            this.BIAS_CURRENT[1] = (uint)((int)uint32_4 + ((int)uint32_3 << 3) + 13568);
            this.BIAS_CURRENT[2] = (uint)((int)uint32_6 + ((int)uint32_5 << 3) + 13824);
            this.BIAS_CURRENT[3] = uint32_1 + 14080U;
        }

        private void Bias_Current_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.BIAS_CURRENT.Length; ++index)
                    this.Register_Write(this.BIAS_CURRENT[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void MEM_CTRL_CheckedChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.SCAN_MODE_EN.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.BEAM_RAM_BYPASS.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.BIAS_RAM_BYPASS.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.TX_BEAM_STEP_EN.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.RX_BEAM_STEP_EN.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.TX_CHX_RAM_BYPASS.Checked ? 1 : 0);
            this.MEMCTRL[0] = (uint)((int)Convert.ToUInt32(this.RX_CHX_RAM_BYPASS.Checked ? 1 : 0) + ((int)uint32_6 << 1) + ((int)uint32_5 << 2) + ((int)uint32_4 << 3) + ((int)uint32_3 << 5) + ((int)uint32_2 << 6) + ((int)uint32_1 << 7) + 14336);
        }

        private void Mem_Ctrl_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.MEMCTRL.Length; ++index)
                    this.Register_Write(this.MEMCTRL[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void MEMORY_INDEX_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.RX_CH1_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.RX_CH1_RAM_INDEX.Value);
            uint uint32_3 = Convert.ToUInt32(this.RX_CH2_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.RX_CH2_RAM_INDEX.Value);
            uint uint32_5 = Convert.ToUInt32(this.RX_CH3_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.RX_CH3_RAM_INDEX.Value);
            uint uint32_7 = Convert.ToUInt32(this.RX_CH4_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_8 = Convert.ToUInt32(this.RX_CH4_RAM_INDEX.Value);
            uint uint32_9 = Convert.ToUInt32(this.RX_CHX_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_10 = Convert.ToUInt32(this.RX_CHX_RAM_INDEX.Value);
            uint uint32_11 = Convert.ToUInt32(this.TX_CH1_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_12 = Convert.ToUInt32(this.TX_CH1_RAM_INDEX.Value);
            uint uint32_13 = Convert.ToUInt32(this.TX_CH2_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_14 = Convert.ToUInt32(this.TX_CH2_RAM_INDEX.Value);
            uint uint32_15 = Convert.ToUInt32(this.TX_CH3_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_16 = Convert.ToUInt32(this.TX_CH3_RAM_INDEX.Value);
            uint uint32_17 = Convert.ToUInt32(this.TX_CH4_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_18 = Convert.ToUInt32(this.TX_CH4_RAM_INDEX.Value);
            uint uint32_19 = Convert.ToUInt32(this.TX_CHX_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_20 = Convert.ToUInt32(this.TX_CHX_RAM_INDEX.Value);
            this.MEM_INDEX_RX[0] = (uint)((int)uint32_2 + ((int)uint32_1 << 7) + 15616);
            this.MEM_INDEX_RX[1] = (uint)((int)uint32_4 + ((int)uint32_3 << 7) + 15872);
            this.MEM_INDEX_RX[2] = (uint)((int)uint32_6 + ((int)uint32_5 << 7) + 16128);
            this.MEM_INDEX_RX[3] = (uint)((int)uint32_8 + ((int)uint32_7 << 7) + 16384);
            this.MEM_INDEX_RX[4] = (uint)((int)uint32_10 + ((int)uint32_9 << 7) + 14592);
            this.MEM_INDEX_TX[0] = (uint)((int)uint32_12 + ((int)uint32_11 << 7) + 16640);
            this.MEM_INDEX_TX[1] = (uint)((int)uint32_14 + ((int)uint32_13 << 7) + 16896);
            this.MEM_INDEX_TX[2] = (uint)((int)uint32_16 + ((int)uint32_15 << 7) + 17152);
            this.MEM_INDEX_TX[3] = (uint)((int)uint32_18 + ((int)uint32_17 << 7) + 17408);
            this.MEM_INDEX_TX[4] = (uint)((int)uint32_20 + ((int)uint32_19 << 7) + 14848);
        }

        private void RX_Mem_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.MEM_INDEX_RX.Length; ++index)
                    this.Register_Write(this.MEM_INDEX_RX[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_Mem_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.MEM_INDEX_TX.Length; ++index)
                    this.Register_Write(this.MEM_INDEX_TX[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void LDO_TRIM_REG_ValueChanged(object sender, EventArgs e)
        {
            this.LDO_TRIMR[0] = (uint)(((int)Convert.ToUInt32(this.LDO_TRIM_REG.Value) << 7) + 262144);
        }

        private void LDO_TRIM_REG_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.LDO_TRIMR.Length; ++index)
                    this.Register_Write(this.LDO_TRIMR[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void LDO_TRIM_SEL_ValueChanged(object sender, EventArgs e)
        {
            this.LDO_TRIMS[0] = (uint)(((int)Convert.ToUInt32(this.LDO_TRIM_SEL.Value) << 1) + 262400);
        }

        private void LDO_TRIM_SEL_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.LDO_TRIMS.Length; ++index)
                    this.Register_Write(this.LDO_TRIMS[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void NVM_CTRL_CheckedChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.FUSE_CLOCK_CTL.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.NVM_REREAD.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.FUSE_BYPASS.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.NVM_START_BYP.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.NVM_ON_BYP.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.NVM_RD_BYP.Checked ? 1 : 0);
            uint uint32_7 = Convert.ToUInt32(this.NVM_CTL_BYP_EN.Checked ? 1 : 0);
            uint uint32_8 = Convert.ToUInt32(this.NVM_TEST.Checked ? 1 : 0);
            uint uint32_9 = Convert.ToUInt32(this.NVM_MARGIN.Checked ? 1 : 0);
            uint uint32_10 = Convert.ToUInt32(this.NVM_PROG_PULSE.Checked ? 1 : 0);
            uint uint32_11 = Convert.ToUInt32(this.NVM_ADDR_BYP.Value);
            uint uint32_12 = Convert.ToUInt32(this.NVM_BIT_SEL.Value);
            uint uint32_13 = Convert.ToUInt32(this.NVM_DIN.Value);
            this.NVM_CTRL[0] = (uint)((int)uint32_3 + ((int)uint32_2 << 2) + ((int)uint32_1 << 3) + 264192);
            this.NVM_CTRL[1] = (uint)((int)uint32_7 + ((int)uint32_6 << 1) + ((int)uint32_5 << 2) + ((int)uint32_4 << 3) + ((int)uint32_11 << 7) + 264448);
            this.NVM_CTRL[2] = (uint)((int)uint32_12 + ((int)uint32_10 << 3) + ((int)uint32_9 << 4) + ((int)uint32_8 << 5) + 264704);
            this.NVM_CTRL[3] = uint32_13 + 264960U;
        }

        private void NVM_Ctrl_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.NVM_CTRL.Length; ++index)
                    this.Register_Write(this.NVM_CTRL[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void NVM_BYP_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.LDO_TRIM_BYP_A.Value);
            uint uint32_2 = Convert.ToUInt32(this.LDO_TRIM_BYP_B.Value);
            uint uint32_3 = Convert.ToUInt32(this.LDO_TRIM_BYP_C.Value);
            this.NVM_BYPASS[0] = uint32_1 + 327680U;
            this.NVM_BYPASS[1] = uint32_2 + 327936U;
            this.NVM_BYPASS[2] = uint32_3 + 328192U;
        }

        private void NVM_BYP_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.NVM_BYPASS.Length; ++index)
                    this.Register_Write(this.NVM_BYPASS[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void DelayControl(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.TX_TO_RX_DELAY_1.Value);
            uint uint32_2 = Convert.ToUInt32(this.TX_TO_RX_DELAY_2.Value);
            uint uint32_3 = Convert.ToUInt32(this.RX_TO_TX_DELAY_1.Value);
            uint uint32_4 = Convert.ToUInt32(this.RX_TO_TX_DELAY_2.Value);
            this.Delay_Control[0] = (uint)(19200 + ((int)uint32_1 << 4)) + uint32_2;
            this.Delay_Control[1] = (uint)(19456 + ((int)uint32_3 << 4)) + uint32_4;
        }

        private void Delay_Control_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.Delay_Control.Length; ++index)
                    this.Register_Write(this.Delay_Control[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void BEAM_STEP_CONTROL(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.TX_BEAM_STEP_START.Value);
            uint uint32_2 = Convert.ToUInt32(this.TX_BEAM_STEP_STOP.Value);
            uint uint32_3 = Convert.ToUInt32(this.RX_BEAM_STEP_START.Value);
            uint uint32_4 = Convert.ToUInt32(this.TX_BEAM_STEP_STOP.Value);
            this.BEAM_STEP[0] = 19712U + uint32_1;
            this.BEAM_STEP[1] = 19968U + uint32_2;
            this.BEAM_STEP[2] = 20224U + uint32_3;
            this.BEAM_STEP[3] = 20480U + uint32_4;
        }

        private void BEAM_STEP_CONTROL_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.BEAM_STEP.Length; ++index)
                    this.Register_Write(this.BEAM_STEP[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void BIAS_RAM_CONTROL(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.RX_BIAS_RAM_INDEX.Value);
            uint uint32_2 = Convert.ToUInt32(this.TX_BIAS_RAM_INDEX.Value);
            uint uint32_3 = Convert.ToUInt32(this.RX_BIAS_RAM_FETCH.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.TX_BIAS_RAM_FETCH.Checked ? 1 : 0);
            this.BIAS_RAM_CNTRL[0] = (uint)(20736 + ((int)uint32_3 << 3)) + uint32_1;
            this.BIAS_RAM_CNTRL[1] = (uint)(20992 + ((int)uint32_4 << 3)) + uint32_2;
        }

        private void BIAS_RAM_CONTROL_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.BIAS_RAM_CNTRL.Length; ++index)
                    this.Register_Write(this.BIAS_RAM_CNTRL[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void GPIO_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (this.g_session != null)
            {
                if (!this.RX_LOAD.Checked)
                {
                    num = this.g_session.bitClear((byte)1);
                    this.log("ADAR1000 RX LOAD set Low. GPIO0, Pin 43");
                }
                else
                {
                    num = this.g_session.bitSet((byte)1);
                    this.log("ADAR1000 RX LOAD set High. GPIO0, Pin 43");
                }
                if (!this.TX_LOAD.Checked)
                {
                    num = this.g_session.bitClear((byte)2);
                    this.log("ADAR1000 TX LOAD set Low. GPIO1, Pin 78");
                }
                else
                {
                    num = this.g_session.bitSet((byte)2);
                    this.log("ADAR1000 TX LOAD set High. GPIO1, Pin 78");
                }
                if (!this.ADDR_0.Checked)
                {
                    num = this.g_session.bitClear((byte)4);
                    this.log("ADAR1000 ADDR 0 set Low. GPIO2, Pin 44");
                }
                else
                {
                    num = this.g_session.bitSet((byte)4);
                    this.log("ADAR1000 ADDR 0 set High. GPIO2, Pin 44");
                }
                if (!this.ADDR_1.Checked)
                {
                    num = this.g_session.bitClear((byte)8);
                    this.log("ADAR1000 ADDR 1 set Low. GPIO3, Pin 77");
                }
                else
                {
                    num = this.g_session.bitSet((byte)8);
                    this.log("ADAR1000 ADDR 1 set High. GPIO3, Pin 77");
                }
                if (!this.TR_.Checked)
                {
                    num = this.g_session.bitClear((byte)16);
                    this.log("ADAR1000 TR set Low. GPIO4, Pin 45 ");
                }
                else
                {
                    num = this.g_session.bitSet((byte)16);
                    this.log("ADAR1000 TR set High. GPIO4, Pin 45");
                }
                if (!this.GPIO_5.Checked)
                {
                    num = this.g_session.bitClear((byte)32);
                    this.log("ADAR1000 GPIO5 set Low. GPIO5, Pin 76 ");
                }
                else
                {
                    num = this.g_session.bitSet((byte)32);
                    this.log("ADAR1000 GPIO5 set High. GPIO5, Pin 76");
                }
            }
            else
                this.log("Error with ADAR1000 GPIO write. Is SDP connected?");
        }


        private void GPIO_Click2(object sender, EventArgs e)
        {
            int num = 0;
            if (this.g_session2 != null)
            {
                if (!this.RX_LOAD.Checked)
                {
                    num = this.g_session2.bitClear((byte)1);
                    this.log("ADAR1000 RX LOAD set Low. GPIO0, Pin 43");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)1);
                    this.log("ADAR1000 RX LOAD set High. GPIO0, Pin 43");
                }
                if (!this.TX_LOAD.Checked)
                {
                    num = this.g_session2.bitClear((byte)2);
                    this.log("ADAR1000 TX LOAD set Low. GPIO1, Pin 78");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)2);
                    this.log("ADAR1000 TX LOAD set High. GPIO1, Pin 78");
                }
                if (!this.ADDR_0.Checked)
                {
                    num = this.g_session2.bitClear((byte)4);
                    this.log("ADAR1000 ADDR 0 set Low. GPIO2, Pin 44");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)4);
                    this.log("ADAR1000 ADDR 0 set High. GPIO2, Pin 44");
                }
                if (!this.ADDR_1.Checked)
                {
                    num = this.g_session2.bitClear((byte)8);
                    this.log("ADAR1000 ADDR 1 set Low. GPIO3, Pin 77");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)8);
                    this.log("ADAR1000 ADDR 1 set High. GPIO3, Pin 77");
                }
                if (!this.TR_.Checked)
                {
                    num = this.g_session2.bitClear((byte)16);
                    this.log("ADAR1000 TR set Low. GPIO4, Pin 45 ");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)16);
                    this.log("ADAR1000 TR set High. GPIO4, Pin 45");
                }
                if (!this.GPIO_52.Checked)
                {
                    num = this.g_session2.bitClear((byte)32);
                    this.log("ADAR1000 GPIO5 set Low. GPIO5, Pin 76 ");
                }
                else
                {
                    num = this.g_session2.bitSet((byte)32);
                    this.log("ADAR1000 GPIO5 set High. GPIO5, Pin 76");
                }
            }
            else
                this.log("Error with ADAR1000 GPIO write. Is SDP connected?");
        }

        private void GPIO_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.RX_LOAD.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.TX_LOAD.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.ADDR_0.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.ADDR_1.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.TR_.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.GPIO_5.Checked ? 1 : 0);
            this.GPIO[0] = uint32_1;
            this.GPIO[1] = uint32_2;
            this.GPIO[2] = uint32_3;
            this.GPIO[3] = uint32_4;
            this.GPIO[4] = uint32_5;
            this.GPIO[5] = uint32_6;
        }
        private void GPIO_ValueChanged2(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.RX_LOAD.Checked ? 1 : 0);
            uint uint32_2 = Convert.ToUInt32(this.TX_LOAD.Checked ? 1 : 0);
            uint uint32_3 = Convert.ToUInt32(this.ADDR_0.Checked ? 1 : 0);
            uint uint32_4 = Convert.ToUInt32(this.ADDR_1.Checked ? 1 : 0);
            uint uint32_5 = Convert.ToUInt32(this.TR_.Checked ? 1 : 0);
            uint uint32_6 = Convert.ToUInt32(this.GPIO_52.Checked ? 1 : 0);
            this.GPIO[0] = uint32_1;
            this.GPIO[1] = uint32_2;
            this.GPIO[2] = uint32_3;
            this.GPIO[3] = uint32_4;
            this.GPIO[4] = uint32_5;
            this.GPIO[5] = uint32_6;
        }
        private void Generate_read_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                RegCheck regCheck = new RegCheck();
                uint[][] numArray = new uint[19][]
                {
          this.RXGain,
          this.RXPhase,
          this.TXGain,
          this.TXPhase,
          this.BIAS_ON,
          this.RX_Enable,
          this.TX_Enable,
          this.MISC_Enable,
          this.SW_CTRL,
          this.ADC_CTRL,
          this.BIAS_CURRENT,
          this.MEMCTRL,
          this.MEM_INDEX_RX,
          this.MEM_INDEX_TX,
          this.BIAS_OFF,
          this.LDO_TRIMR,
          this.LDO_TRIMS,
          this.NVM_CTRL,
          this.NVM_BYPASS
                };
                DataTable dataTable = new DataTable();
                dataTable.NewRow();
                dataTable.Columns.Add(new DataColumn("Reg Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Write", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Read", typeof(int)));
                for (uint index1 = 0; (long)index1 < (long)numArray.Length; ++index1)
                {
                    for (int index2 = 0; index2 < numArray[(int)index1].Length; ++index2)
                    {
                        DataRow row = dataTable.NewRow();
                        uint num1;
                        switch (index1)
                        {
                            case 0:
                                row[0] = (object)("RXGain[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(0U);
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 1:
                                row[0] = (object)("RXPhase[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 2:
                                row[0] = (object)("TXGain[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 3:
                                row[0] = (object)("TXPhase[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 4:
                                row[0] = (object)("BIAS_ON[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 5:
                                row[0] = (object)("RX_Enable[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 6:
                                row[0] = (object)("TX_Enable[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 7:
                                row[0] = (object)("MISC_Enable[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 8:
                                row[0] = (object)("SW_CTRL[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 9:
                                row[0] = (object)("ADC_CTRL[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 10:
                                row[0] = (object)("BIAS_CURRENT[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 11:
                                row[0] = (object)("MEMCTRL[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 12:
                                row[0] = (object)("MEM_INDEX_RX[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 13:
                                row[0] = (object)("MEM_INDEX_TX[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 14:
                                row[0] = (object)("BIAS_OFF[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 15:
                                row[0] = (object)("LDO_TRIMR[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 16:
                                row[0] = (object)("LDO_TRIMS[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 17:
                                row[0] = (object)("NVM_CTRL[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                this.toWrite[0] = Convert.ToUInt32(8388608U + (numArray[(int)index1][index2] & 1048320U));
                                this.session.writeReadU24(this.toWrite, 1, out this.toRead);
                                num1 = this.toRead[0] & (uint)byte.MaxValue;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                            case 18:
                                row[0] = (object)("NVM_BYPASS[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                string[] strArray = new string[1]
                                {
                  this.ReadFromDevice(numArray[(int) index1][index2] & (uint) byte.MaxValue).ToString()
                                };
                                row[2] = (object)strArray[0];
                                break;
                            default:
                                row[0] = (object)("DEFAULT[" + (object)index2 + "]");
                                row[1] = (object)numArray[(int)index1][index2].ToString("X");
                                uint num2 = this.ReadFromDevice(numArray[(int)index1][index2] & 1048320U) & 1044480U;
                                row[2] = (object)this.toWrite[0].ToString();
                                break;
                        }
                        dataTable.Rows.Add(row);
                    }
                }
                this.dataGridView1.DataSource = (object)dataTable;
                this.dataGridView1.AutoGenerateColumns = true;
                this.dataGridView1.Columns[0].Width = 150;
            }
            else
                this.log("Error. Cannot read back from board. Check the SDP is connected.");
        }

        private uint ReadFromDevice(uint address)
        {
            uint[] writeData = new uint[1];
            uint[] readData = new uint[1];
            writeData[0] = (uint)(8388608 + ((int)address << 8));
            this.session.writeReadU24(writeData, 1, out readData);
            return readData[0];
        }

        private void ReadbackButton_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
                this.textBox3.Text = (this.ReadFromDevice(uint.Parse(this.textBox2.Text, NumberStyles.HexNumber)) & (uint)byte.MaxValue).ToString("X");
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void Write_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                uint data = 0;
                try
                {
                    data = Convert.ToUInt32(this.ROBox.Text, 16);
                }
                catch
                {
                }
                this.WriteToDevice(data);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void ADILogo_Click(object sender, EventArgs e)
        {
            this.PasswordPanel.Visible = true;
            this.ADILogo.Visible = false;
        }

        private void OKButton_Click_1(object sender, EventArgs e)
        {
            if (this.PasswordBox.Text.ToLower() == "september")
            {
                this.TestmodesPanel.Visible = true;
                this.Test_Modes_Panel2.Visible = true;
                this.PasswordPanel.Visible = false;
                this.ADILogo.Visible = true;
            }
            else
            {
                int num = (int)MessageBox.Show("Wrong password.");
            }
            this.PasswordBox.Text = "";
            this.PasswordPanel.Visible = false;
            this.ADILogo.Visible = true;
        }

        private void SelectDemoFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV|*.csv*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.DemoFileName.Text = openFileDialog.FileName;
        }

        private void LoadDemoFile_Click(object sender, EventArgs e)
        {
            using (StreamReader streamReader = new StreamReader(this.DemoFileName.Text))
            {
                streamReader.ReadLine();
                for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                {
                    uint num1 = 0;
                    string[] strArray = str.Split(',');
                    Convert.ToUInt32(strArray[0]);
                    Convert.ToUInt32(strArray[1]);
                    Convert.ToUInt32(strArray[2]);
                    Convert.ToUInt32(strArray[3]);
                    Convert.ToUInt32(strArray[4]);
                    Convert.ToUInt32(strArray[5]);
                    Convert.ToUInt32(strArray[6]);
                    Convert.ToUInt32(strArray[7]);
                    Convert.ToUInt32(strArray[8]);
                    Convert.ToUInt32(strArray[9]);
                    Convert.ToUInt32(strArray[10]);
                    Convert.ToUInt32(strArray[11]);
                    Convert.ToUInt32(strArray[12]);
                    Convert.ToUInt32(strArray[13]);
                    Convert.ToUInt32(strArray[14]);
                    Convert.ToUInt32(strArray[15]);
                    Convert.ToUInt32(strArray[16]);
                    Convert.ToUInt32(strArray[17]);
                    Convert.ToUInt32(strArray[18]);
                    Convert.ToUInt32(strArray[19]);
                    Convert.ToUInt32(strArray[20]);
                    Convert.ToUInt32(strArray[21]);
                    Convert.ToUInt32(strArray[22]);
                    Convert.ToUInt32(strArray[23]);
                    Convert.ToUInt32(strArray[24]);
                    Convert.ToUInt32(strArray[25]);
                    Convert.ToUInt32(strArray[26]);
                    Convert.ToUInt32(strArray[27]);
                    Convert.ToUInt32(strArray[28]);
                    Convert.ToUInt32(strArray[29]);
                    Convert.ToUInt32(strArray[30]);
                    Convert.ToUInt32(strArray[31]);
                    Convert.ToUInt32(strArray[32]);
                    Convert.ToUInt32(strArray[33]);
                    Convert.ToUInt32(strArray[34]);
                    Convert.ToUInt32(strArray[35]);
                    Convert.ToUInt32(strArray[36]);
                    uint[] numArray1 = new uint[36];
                    for (int index1 = 0; index1 == 4; ++index1)
                    {
                        uint num2 = 0;
                        for (int index2 = 0; index2 == 15; ++index2)
                        {
                            uint[] numArray2 = new uint[1];
                            num2 += num2;
                            num1 += num1;
                        }
                    }
                    str.Split(',');
                }
            }
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            uint[] numArray = new uint[24]
            {
        1048656U,
        1048878U,
        1049115U,
        1052797U,
        1052986U,
        1053196U,
        1056852U,
        1057082U,
        1057322U,
        1060984U,
        1061163U,
        1061433U,
        1065046U,
        1065228U,
        1065529U,
        1069177U,
        1069338U,
        1069614U,
        1073236U,
        1073434U,
        1073673U,
        1077368U,
        1077516U,
        1077784U
            };
            if (this.g_session != null)
            {
                for (int index = 0; index < numArray.Length; ++index)
                    this.Register_Write(numArray[index]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void DemoPoint_ValueChanged(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.StartPoint.Value);
            uint uint32_2 = Convert.ToUInt32(this.EndPoint.Value);
            if (uint32_2 > uint32_1)
                return;
            this.StartPoint.Value = (Decimal)(uint32_2 - 1U);
        }

        private void Run_Demo_button(object sender, EventArgs e)
        {
            uint uint32_1 = Convert.ToUInt32(this.StartPoint.Value);
            uint uint32_2 = Convert.ToUInt32(this.EndPoint.Value);
            uint uint32_3 = Convert.ToUInt32(this.TimeDelay.Value);
            uint num1 = 0;
            if (this.g_session != null)
            {
                uint[] numArray = new uint[1];
                this.Finish = 0;
                if (uint32_3 == 0U)
                {
                    numArray[0] = 14368U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 2111520U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 4208672U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 6305824U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 14976U + uint32_1 + num1;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 2112128U + uint32_1 + num1;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 4209280U + uint32_1 + num1;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 6306432U + uint32_1 + num1;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 2560U;
                    this.Register_Write(numArray[0]);
                    uint num2 = num1 + uint32_1 >= uint32_2 ? 0U : num1 + 1U;
                    this.g_session.bitSet((byte)2);
                    this.g_session.bitClear((byte)2);
                    this.log("GPIO1 Toggled");
                }
                else
                {
                label_5:
                    if (this.Finish != 0)
                        return;
                    uint num2 = 0;
                    numArray[0] = 14368U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 2111520U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 4208672U;
                    this.Register_Write(numArray[0]);
                    numArray[0] = 6305824U;
                    this.Register_Write(numArray[0]);
                    do
                    {
                        numArray[0] = 14976U + uint32_1 + num2;
                        this.Register_Write(numArray[0]);
                        numArray[0] = 2112128U + uint32_1 + num2;
                        this.Register_Write(numArray[0]);
                        numArray[0] = 4209280U + uint32_1 + num2;
                        this.Register_Write(numArray[0]);
                        numArray[0] = 6306432U + uint32_1 + num2;
                        this.Register_Write(numArray[0]);
                        numArray[0] = 2560U;
                        this.Register_Write(numArray[0]);
                        Application.DoEvents();
                        Thread.Sleep((int)Convert.ToUInt16(this.TimeDelay.Value));
                        Application.DoEvents();
                        this.g_session.bitSet((byte)2);
                        this.g_session.bitClear((byte)2);
                        this.log("GPIO1 Toggled");
                        ++num2;
                    }
                    while (num2 + uint32_1 <= uint32_2);
                    goto label_5;
                }
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void End_Demo_button(object sender, EventArgs e)
        {
            this.Finish = 1;
        }

        private void Start_demo_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                if (this.worker.IsBusy || this.ADIworker.IsBusy)
                    return;
                this.worker.RunWorkerAsync();
                this.worker.WorkerSupportsCancellation = true;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void Start_ADIdemo_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                if (this.ADIworker.IsBusy || this.worker.IsBusy)
                    return;
                this.ADIworker.RunWorkerAsync();
                this.ADIworker.WorkerSupportsCancellation = true;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void CancelDemo(object sender, EventArgs e)
        {
            this.CancelBgWorker();
        }

        private void CancelBgWorker()
        {
            if (this.worker.IsBusy)
            {
                this.worker.CancelAsync();
            }
            else
            {
                if (!this.ADIworker.IsBusy)
                    return;
                this.ADIworker.CancelAsync();
            }
        }

        private void DemoLoop(object sender, DoWorkEventArgs e)
        {
            if (this.RX1)
            {
                for (int index = 0; index < this.RX1Seq.Length + 1; ++index)
                    this.Register_Write(this.RX1Seq[index]);
            }
            if (this.RX2)
            {
                for (int index = 0; index < this.RX2Seq.Length + 1; ++index)
                    this.Register_Write(this.RX2Seq[index]);
            }
            if (this.RX3)
            {
                for (int index = 0; index < this.RX3Seq.Length + 1; ++index)
                    this.Register_Write(this.RX3Seq[index]);
            }
            if (this.RX4)
            {
                for (int index = 0; index < this.RX4Seq.Length + 1; ++index)
                    this.Register_Write(this.RX4Seq[index]);
            }
            if (this.TX1)
            {
                for (int index = 0; index < this.TX1Seq.Length + 1; ++index)
                    this.Register_Write(this.TX1Seq[index]);
            }
            if (this.TX2)
            {
                for (int index = 0; index < this.TX2Seq.Length + 1; ++index)
                    this.Register_Write(this.TX2Seq[index]);
            }
            if (this.TX3)
            {
                for (int index = 0; index < this.TX3Seq.Length + 1; ++index)
                    this.Register_Write(this.TX3Seq[index]);
            }
            if (this.TX4)
            {
                for (int index = 0; index < this.TX4Seq.Length + 1; ++index)
                    this.Register_Write(this.TX4Seq[index]);
            }
            string index1 = "127";
            int index2 = 0;
            int demoPhaseAngleDiff = this.Demo_Phase_Angle_diff;
            while (index2 <= (int)sbyte.MaxValue)
            {
                index2 += demoPhaseAngleDiff;
                if (index2 >= 128)
                    index2 -= 128;
                string key = this.tables.Keys.ElementAt<string>(index2);
                if (this.tables.ContainsKey(key))
                {
                    uint uint32_1 = Convert.ToUInt32(this.tables[key].Iangle);
                    uint uint32_2 = Convert.ToUInt32(this.tables[key].Qangle);
                    Convert.ToUInt32(this.Gtables[index1]);
                    this.DemoPhase[0] = uint32_1 + 5120U;
                    this.DemoPhase[1] = uint32_2 + 5376U;
                    if (this.worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    for (int index3 = 0; index3 < 2; ++index3)
                        this.Register_Write(this.DemoPhase[index3]);
                    uint[] numArray = new uint[1] { 10241U };
                    this.Register_Write(numArray[0]);
                    numArray[0] = 10240U;
                    this.Register_Write(numArray[0]);
                }
            }
        }

        private void ADIDemoLoop(object sender, DoWorkEventArgs e)
        {
            if (this.RX1)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RX1Seq.Length + 1; ++index)
                {
                    numArray[0] = this.RX1Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.RX2)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RX2Seq.Length + 1; ++index)
                {
                    numArray[0] = this.RX2Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.RX3)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RX3Seq.Length + 1; ++index)
                {
                    numArray[0] = this.RX3Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.RX4)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.RX4Seq.Length + 1; ++index)
                {
                    numArray[0] = this.RX4Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.TX1)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.TX1Seq.Length + 1; ++index)
                {
                    numArray[0] = this.TX1Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.TX2)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.TX2Seq.Length + 1; ++index)
                {
                    numArray[0] = this.TX2Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.TX3)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.TX3Seq.Length + 1; ++index)
                {
                    numArray[0] = this.TX3Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            if (this.TX4)
            {
                uint[] numArray = new uint[1];
                for (int index = 0; index < this.TX4Seq.Length + 1; ++index)
                {
                    numArray[0] = this.TX4Seq[index];
                    this.Register_Write(numArray[0]);
                }
            }
            string index1 = "127";
            int index2 = 0;
            int[] numArray1 = new int[3] { 0, 48, 80 };
            for (; index2 <= 3; ++index2)
            {
                if (index2 >= 3)
                    index2 = 0;
                int index3 = numArray1[index2] + this.ADIDemo_Phase_Angle_diff;
                if (index3 >= 128)
                    index3 -= 128;
                string key = this.tables.ElementAt<KeyValuePair<string, Phase>>(index3).Value.angle.ToString();
                Thread.Sleep(Convert.ToInt32(this.ADIDemo_Loop_Delay));
                if (this.tables.ContainsKey(key))
                {
                    uint uint32_1 = Convert.ToUInt32(this.tables[key].Iangle);
                    uint uint32_2 = Convert.ToUInt32(this.tables[key].Qangle);
                    Convert.ToUInt32(this.Gtables[index1]);
                    this.DemoPhase[0] = uint32_1 + 5120U;
                    this.DemoPhase[1] = uint32_2 + 5376U;
                    if (this.ADIworker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    uint[] numArray2 = new uint[1];
                    for (int index4 = 0; index4 < 2; ++index4)
                        this.Register_Write(this.DemoPhase[index4]);
                    numArray2[0] = 10241U;
                    this.Register_Write(numArray2[0]);
                    numArray2[0] = 10240U;
                    this.Register_Write(numArray2[0]);
                }
            }
        }

        private void populate_block(bool done)
        {
            if (this.g_session != null)
            {
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        Phase phase = new Phase();
                        phase.angle = Convert.ToString(strArray[0]);
                        phase.Iangle = Convert.ToUInt32(strArray[1]);
                        phase.Qangle = Convert.ToUInt32(strArray[2]);
                        char ch = str[index];
                        this.tables.Add(phase.angle, phase);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        string key = Convert.ToString(strArray[0]);
                        uint uint32 = Convert.ToUInt32(strArray[1]);
                        char ch = str[index];
                        this.Gtables.Add(key, uint32);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.DemoPhase1.Items.Add((object)strArray[0]);
                        this.DemoPhase2.Items.Add((object)strArray[0]);
                        this.DemoPhase3.Items.Add((object)strArray[0]);
                        this.DemoPhase4.Items.Add((object)strArray[0]);
                        this.Demo_angle_list.Items.Add((object)strArray[0]);
                        this.comboBox1.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.DemoGain1.Items.Add((object)strArray[0]);
                        this.DemoGain2.Items.Add((object)strArray[0]);
                        this.DemoGain3.Items.Add((object)strArray[0]);
                        this.DemoGain4.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                this.panel3.Enabled = false;
                this.panel4.Enabled = false;
                this.panel5.Enabled = false;
                this.panel6.Enabled = false;
                this.button35.Enabled = false;
                this.panel7.Enabled = false;
                this.panel8.Enabled = false;
                this.panel9.Enabled = false;
                this.panel10.Enabled = false;
                this.button36.Enabled = false;
                using (StreamReader streamReader = new StreamReader("TX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.TX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("RX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.RX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                this.TX_CH1_Phase_Angle.SelectedIndex = 0;
                this.TX_CH2_Phase_Angle.SelectedIndex = 0;
                this.TX_CH3_Phase_Angle.SelectedIndex = 0;
                this.TX_CH4_Phase_Angle.SelectedIndex = 0;
                this.TX_CH1_Gain.SelectedIndex = 0;
                this.TX_CH2_Gain.SelectedIndex = 0;
                this.TX_CH3_Gain.SelectedIndex = 0;
                this.TX_CH4_Gain.SelectedIndex = 0;
                this.RX_CH1_Phase_Angle.SelectedIndex = 0;
                this.RX_CH2_Phase_Angle.SelectedIndex = 0;
                this.RX_CH3_Phase_Angle.SelectedIndex = 0;
                this.RX_CH4_Phase_Angle.SelectedIndex = 0;
                this.RX_CH1_Gain.SelectedIndex = 0;
                this.RX_CH2_Gain.SelectedIndex = 0;
                this.RX_CH3_Gain.SelectedIndex = 0;
                this.RX_CH4_Gain.SelectedIndex = 0;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }


        private void populate_block2(bool done)
        {
            if (this.g_session2 != null)
            {
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        Phase phase = new Phase();
                        phase.angle = Convert.ToString(strArray[0]);
                        phase.Iangle = Convert.ToUInt32(strArray[1]);
                        phase.Qangle = Convert.ToUInt32(strArray[2]);
                        char ch = str[index];
                        this.tables2.Add(phase.angle, phase);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int index = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        string key = Convert.ToString(strArray[0]);
                        uint uint32 = Convert.ToUInt32(strArray[1]);
                        char ch = str[index];
                        this.Gtables2.Add(key, uint32);
                        index += index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("data.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.TX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH1_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH2_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH3_Phase_Angle.Items.Add((object)strArray[0]);
                        this.RX_CH4_Phase_Angle.Items.Add((object)strArray[0]);
                        this.DemoPhase1.Items.Add((object)strArray[0]);
                        this.DemoPhase2.Items.Add((object)strArray[0]);
                        this.DemoPhase3.Items.Add((object)strArray[0]);
                        this.DemoPhase4.Items.Add((object)strArray[0]);
                        this.Demo_angle_list.Items.Add((object)strArray[0]);
                        this.comboBox1.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                using (StreamReader streamReader = new StreamReader("GainData.csv"))
                {
                    int num = 0;
                    for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                    {
                        string[] strArray = str.Split(',');
                        this.TX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.TX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH1_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH2_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH3_Gain.Items.Add((object)strArray[0]);
                        this.RX_CH4_Gain.Items.Add((object)strArray[0]);
                        this.DemoGain1.Items.Add((object)strArray[0]);
                        this.DemoGain2.Items.Add((object)strArray[0]);
                        this.DemoGain3.Items.Add((object)strArray[0]);
                        this.DemoGain4.Items.Add((object)strArray[0]);
                        num += num;
                    }
                }
                this.panel3.Enabled = false;
                this.panel4.Enabled = false;
                this.panel5.Enabled = false;
                this.panel6.Enabled = false;
                this.button35.Enabled = false;
                this.panel7.Enabled = false;
                this.panel8.Enabled = false;
                this.panel9.Enabled = false;
                this.panel10.Enabled = false;
                this.button36.Enabled = false;
                using (StreamReader streamReader = new StreamReader("TX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.TX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                using (StreamReader streamReader = new StreamReader("RX_Init_Data.csv"))
                {
                    int index = 0;
                    string str = streamReader.ReadLine();
                    while (str != null)
                    {
                        string[] strArray = str.Split(',');
                        this.RX_init_seq[index] = Convert.ToUInt32(strArray[0]);
                        str = streamReader.ReadLine();
                        ++index;
                    }
                }
                this.TX_CH1_Phase_Angle.SelectedIndex = 0;
                this.TX_CH2_Phase_Angle.SelectedIndex = 0;
                this.TX_CH3_Phase_Angle.SelectedIndex = 0;
                this.TX_CH4_Phase_Angle.SelectedIndex = 0;
                this.TX_CH1_Gain.SelectedIndex = 0;
                this.TX_CH2_Gain.SelectedIndex = 0;
                this.TX_CH3_Gain.SelectedIndex = 0;
                this.TX_CH4_Gain.SelectedIndex = 0;
                this.RX_CH1_Phase_Angle.SelectedIndex = 0;
                this.RX_CH2_Phase_Angle.SelectedIndex = 0;
                this.RX_CH3_Phase_Angle.SelectedIndex = 0;
                this.RX_CH4_Phase_Angle.SelectedIndex = 0;
                this.RX_CH1_Gain.SelectedIndex = 0;
                this.RX_CH2_Gain.SelectedIndex = 0;
                this.RX_CH3_Gain.SelectedIndex = 0;
                this.RX_CH4_Gain.SelectedIndex = 0;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }


        private void TX_CH1_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.TX_CH1_Gain.SelectedItem.ToString();
                string key = this.TX_CH1_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.TX1_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.TXPhase[0] = uint32_2 + 8192U;
                this.TXPhase[1] = uint32_3 + 8448U;
                this.TXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 7168);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.TXPhase[index2]);
                numArray[0] = 10242U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_CH2_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.TX_CH2_Gain.SelectedItem.ToString();
                string key = this.TX_CH2_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.TX2_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.TXPhase[0] = uint32_2 + 8704U;
                this.TXPhase[1] = uint32_3 + 8960U;
                this.TXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 7424);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.TXPhase[index2]);
                numArray[0] = 10242U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_CH3_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.TX_CH3_Gain.SelectedItem.ToString();
                string key = this.TX_CH3_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.TX3_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.TXPhase[0] = uint32_2 + 9216U;
                this.TXPhase[1] = uint32_3 + 9472U;
                this.TXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 7680);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.TXPhase[index2]);
                numArray[0] = 10242U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_CH4_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.TX_CH4_Gain.SelectedItem.ToString();
                string key = this.TX_CH4_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.TX4_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.TXPhase[0] = uint32_2 + 9728U;
                this.TXPhase[1] = uint32_3 + 9984U;
                this.TXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 7936);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.TXPhase[index2]);
                numArray[0] = 10242U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_CH1_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.RX_CH1_Gain.SelectedItem.ToString();
                string key = this.RX_CH1_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.RX1_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.RXPhase[0] = uint32_2 + 5120U;
                this.RXPhase[1] = uint32_3 + 5376U;
                this.RXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 4096);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.RXPhase[index2]);
                numArray[0] = 10241U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_CH2_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.RX_CH2_Gain.SelectedItem.ToString();
                string key = this.RX_CH2_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.RX2_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.RXPhase[0] = uint32_2 + 5632U;
                this.RXPhase[1] = uint32_3 + 5888U;
                this.RXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 4352);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.RXPhase[index2]);
                numArray[0] = 10241U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_CH3_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.RX_CH3_Gain.SelectedItem.ToString();
                string key = this.RX_CH3_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.RX3_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.RXPhase[0] = uint32_2 + 6144U;
                this.RXPhase[1] = uint32_3 + 6400U;
                this.RXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 4608);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.RXPhase[index2]);
                numArray[0] = 10241U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_CH4_block_button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                string index1 = this.RX_CH4_Gain.SelectedItem.ToString();
                string key = this.RX_CH4_Phase_Angle.SelectedItem.ToString();
                uint uint32_1 = Convert.ToUInt32(this.RX4_Attn_CheckBox.Checked ? 0 : 1);
                if (!this.tables.ContainsKey(key))
                    return;
                uint uint32_2 = Convert.ToUInt32(this.tables[key].Iangle);
                uint uint32_3 = Convert.ToUInt32(this.tables[key].Qangle);
                uint uint32_4 = Convert.ToUInt32(this.Gtables[index1]);
                this.RXPhase[0] = uint32_2 + 6656U;
                this.RXPhase[1] = uint32_3 + 6912U;
                this.RXPhase[2] = (uint)((int)uint32_4 + ((int)uint32_1 << 7) + 4864);
                uint[] numArray = new uint[1];
                for (int index2 = 0; index2 < 3; ++index2)
                    this.Register_Write(this.RXPhase[index2]);
                numArray[0] = 10241U;
                this.Register_Write(numArray[0]);
                numArray[0] = 10240U;
                this.Register_Write(numArray[0]);
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_Write_All_Button(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                this.TX_CH1_block_button_Click((object)null, new EventArgs());
                this.TX_CH2_block_button_Click((object)null, new EventArgs());
                this.TX_CH3_block_button_Click((object)null, new EventArgs());
                this.TX_CH4_block_button_Click((object)null, new EventArgs());
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_Write_All_Button(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                this.RX_CH1_block_button_Click((object)null, new EventArgs());
                this.RX_CH2_block_button_Click((object)null, new EventArgs());
                this.RX_CH3_block_button_Click((object)null, new EventArgs());
                this.RX_CH4_block_button_Click((object)null, new EventArgs());
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX1_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.TX1_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.TX1_Attn_Pic.Visible = false;
            else
                this.TX1_Attn_Pic.Visible = true;
        }

        private void TX2_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.TX2_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.TX2_Attn_Pic.Visible = false;
            else
                this.TX2_Attn_Pic.Visible = true;
        }

        private void TX3_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.TX3_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.TX3_Attn_Pic.Visible = false;
            else
                this.TX3_Attn_Pic.Visible = true;
        }

        private void TX4_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.TX4_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.TX4_Attn_Pic.Visible = false;
            else
                this.TX4_Attn_Pic.Visible = true;
        }

        private void RX1_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.RX1_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.RX1_Attn_Pic.Visible = false;
            else
                this.RX1_Attn_Pic.Visible = true;
        }

        private void RX2_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.RX2_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.RX2_Attn_Pic.Visible = false;
            else
                this.RX2_Attn_Pic.Visible = true;
        }

        private void RX3_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.RX3_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.RX3_Attn_Pic.Visible = false;
            else
                this.RX3_Attn_Pic.Visible = true;
        }

        private void RX4_Attn_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.RX4_Attn_CheckBox.Checked ? 1 : 0) == 1)
                this.RX4_Attn_Pic.Visible = false;
            else
                this.RX4_Attn_Pic.Visible = true;
        }

        private void TX_Init_Button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.TX_init_seq.Length; ++index)
                    this.Register_Write(this.TX_init_seq[index]);
                this.TX_Init_Indicator.BackColor = Color.Green;
                this.RX_Init_Indicator.BackColor = Color.Red;
                this.TX_Init_Label.Text = "Initalised";
                this.RX_Init_Label.Text = "Not Initalised";
                this.panel3.Enabled = true;
                this.panel4.Enabled = true;
                this.panel5.Enabled = true;
                this.panel6.Enabled = true;
                this.button35.Enabled = true;
                this.panel7.Enabled = false;
                this.panel8.Enabled = false;
                this.panel9.Enabled = false;
                this.panel10.Enabled = false;
                this.button36.Enabled = false;
                this.TX_Init_Button.Enabled = false;
                this.RX_Init_Button.Enabled = true;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void RX_Init_Button_Click(object sender, EventArgs e)
        {
            if (this.g_session != null)
            {
                for (int index = 0; index < this.RX_init_seq.Length; ++index)
                    this.Register_Write(this.RX_init_seq[index]);
                this.TX_Init_Indicator.BackColor = Color.Red;
                this.RX_Init_Indicator.BackColor = Color.Green;
                this.TX_Init_Label.Text = "Not Initalised";
                this.RX_Init_Label.Text = "Initalised";
                this.panel3.Enabled = false;
                this.panel4.Enabled = false;
                this.panel5.Enabled = false;
                this.panel6.Enabled = false;
                this.button35.Enabled = false;
                this.panel7.Enabled = true;
                this.panel8.Enabled = true;
                this.panel9.Enabled = true;
                this.panel10.Enabled = true;
                this.button36.Enabled = true;
                this.TX_Init_Button.Enabled = true;
                this.RX_Init_Button.Enabled = false;
            }
            else
                this.log("Error with ADAR1000 write. Is SDP connected?");
        }

        private void TX_CH1_Phase_Angle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PolarPlotDemo(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.PolarPlot.Refresh();
            Graphics graphics = this.PolarPlot.CreateGraphics();
            double num1 = (Math.PI * Convert.ToDouble(this.DemoPhase1.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase2.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase3.SelectedItem) / 180.0 + Math.PI * Convert.ToDouble(this.DemoPhase4.SelectedItem) / 180.0) / 4.0;
            double num2 = Convert.ToDouble(this.DemoGain1.SelectedItem);
            double num3 = 210.0;
            double num4 = 210.0;
            double num5 = 210.0;
            double maxValue = (double)sbyte.MaxValue;
            double num6;
            if (num2 != 0.0)
            {
                double num7 = maxValue / num2;
                num6 = num5 / num7;
            }
            else
                num6 = 0.0;
            double num8 = Math.Sin(num1) * num6;
            double num9 = Math.Cos(num1) * num6;
            int x = Convert.ToInt32(num3) + Convert.ToInt32(num9);
            int y = Convert.ToInt32(num4) - Convert.ToInt32(num8);
            graphics.FillEllipse(Brushes.Magenta, new Rectangle(x, y, 15, 15));
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }





        // Methods about Socket Communications

        private void ListBox9001_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RichTextBox9001_TextChanged(object sender, EventArgs e)
        {

        }

        private void Log(string msg)
        {
            this.Invoke(new Action(delegate ()
            {
                listBox9001.Items.Add(string.Format("[{0}] {1}", DateTime.Now.ToString(), msg));
            }));

        }


        private void Listen()
        {
            Log("Listen Thread is started !!!");
            //종단점
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            //소켓생성
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //바인드
            listenSocket.Bind(endPoint);
            //준비
            listenSocket.Listen(10);
            //수신대기
            // - 여기서 블럭이 걸려야 하지만 스레드로 따로 뺐기때문에 다른 작업이 가능
            Log("클라이언트 요청 대기중..");
            clientSocket = listenSocket.Accept();
            Log("클라이언트 접속됨 - " + clientSocket.LocalEndPoint.ToString());
            //Receive 스레드 호출
            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.IsBackground = true;
            receiveThread.Start(); //Receive() 호출
        }
        //수신처리..

        private void Receive()
        {
            while (true) //*** clientSocket이 Close()되지 않았을 때만 반복하고 싶은데 방법을 모르겠다. ***
            {
                //연결된 클라이언트가 보낸 데이터 수신
                byte[] receiveBuffer = new byte[512]; // msg를 받을 자리를 마련함.
                int length = clientSocket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                //디코딩
                string msg = Encoding.UTF8.GetString(receiveBuffer);
                //엔터처리
                //richTextBox1.AppendText(msg);
                ShowMsg(msg);
                Log("메시지 수신함 : " + msg );



                string[] RxPos_raw = msg.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string RxPos_rev = string.Join(" ", RxPos_raw);

                string[] RxPos = RxPos_rev.Split('[', ' ', ']');

                double RxPosX = Convert.ToDouble(RxPos[1]);
                double RxPosY = Convert.ToDouble(RxPos[2]);


                

                double TxPosX = 3;
                double TxPosY = 0;

                int angleInterval = 5;

                theta_Op = Convert.ToInt32(Math.Round((180 / Math.PI *( Math.Abs(Math.Atan2((TxPosY - RxPosY), (TxPosX - RxPosX))))-90) / angleInterval)) * angleInterval;
                ShowMsg("Opt_by_Loc is angle of : " + Convert.ToString(theta_Op));

                //if(theta_Op < SA)
                //{
                //    //BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - SA, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(SA);
                //}
                //else if (theta_Op > EA)
                //{
                //    //BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - EA, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(EA);
                //}
                //else
                //{
                //    //BeamSweeping_GUI_Panel_T.Refresh();
                //    this.rotateBeamGUI(-90 - theta_Op, BeamSweeping_GUI_Panel_T);
                //    Beamforming_handler(theta_Op);
                //}

                

            }
        }
        //송수신 메시지를 대화창에 출력

        private void ShowMsg(string msg)
        {
            this.Invoke(new Action(delegate ()
            {
                //richTextBox에서 개행이 정상적으로 적용되지 않는다면
                //아래와 같이 따로
                richTextBox9001.AppendText(msg);
                richTextBox9001.AppendText("\r\n");
                //입력된 텍스트에 맞게 스크롤을 내려준다
                this.Activate();
                richTextBox9001.Focus();
                //캐럿(커서)를 텍스트박스의 끝으로 내려준다
                richTextBox9001.SelectionStart = richTextBox9001.Text.Length;
                richTextBox9001.ScrollToCaret(); //스크롤을 캐럿위치에 맞춰준다
            }));
        }

        private void Form9001_Load(object sender, EventArgs e)
        {
            textBox9001.Focus();
            Log("서버가 로드됨");
        }

        private void textBox9001_KeyDown(object sender, KeyEventArgs e) // 쌍방향통신이 아닐경우 필요없음
        {
            //메시지 전송하기
            if (textBox9001.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                byte[] sendBuffer =
                Encoding.UTF8.GetBytes(textBox9001.Text.Trim());
                clientSocket.Send(sendBuffer);
                Log("메시지 전송됨");
                ShowMsg("나] " + textBox9001.Text);
                textBox9001.Text = "";//초기화
            }
        }

        










        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main_Form));
            DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
            this.MainFormStatusBar = new StatusStrip();
            this.DeviceConnectionStatus = new ToolStripStatusLabel();
            this.DeviceConnectionStatus2 = new ToolStripStatusLabel();

            this.StatusBarLabel = new ToolStripStatusLabel();
            this.StatusBarLabel2 = new ToolStripStatusLabel();
            this.EventLog = new TextBox();
            this.MainFormMenu = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.ADILogo = new PictureBox();
            this.label8 = new Label();
            this.R0Button = new Button();
            this.R0Box = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.tabControl1 = new TabControl();
            this.ConnectionTab = new TabPage();
            this.ConnectionTab2 = new TabPage();
            this.ADDR1_checkBox = new CheckBox();
            this.ADDR0_checkBox = new CheckBox();
            this.pictureBox3 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.ConnectSDPButton = new Button();
            this.ConnectSDPButton2 = new Button();
            this.label11 = new Label();
            this.Tx_Control = new TabPage();
            this.TX_Init_Label = new Label();
            this.TX_Init_Indicator = new Panel();
            this.TX_Init_Button = new Button();
            this.TX1_Attn_Pic = new PictureBox();
            this.TX4_Attn_Pic = new PictureBox();
            this.TX3_Attn_Pic = new PictureBox();
            this.TX2_Attn_Pic = new PictureBox();
            this.label130 = new Label();
            this.button35 = new Button();
            this.label129 = new Label();
            this.panel5 = new Panel();
            this.TX_CH3_Gain = new ComboBox();
            this.TX3_Attn_CheckBox = new CheckBox();
            this.button30 = new Button();
            this.TX_CH3_Phase_Angle = new ComboBox();
            this.panel6 = new Panel();
            this.TX_CH4_Gain = new ComboBox();
            this.TX4_Attn_CheckBox = new CheckBox();
            this.button29 = new Button();
            this.TX_CH4_Phase_Angle = new ComboBox();
            this.panel4 = new Panel();
            this.TX_CH2_Gain = new ComboBox();
            this.TX2_Attn_CheckBox = new CheckBox();
            this.button28 = new Button();
            this.TX_CH2_Phase_Angle = new ComboBox();
            this.label128 = new Label();
            this.panel3 = new Panel();
            this.TX_CH1_Gain = new ComboBox();
            this.TX1_Attn_CheckBox = new CheckBox();
            this.TX_CH1_Phase_Angle = new ComboBox();
            this.phase_block_button = new Button();
            this.pictureBox5 = new PictureBox();
            this.TXRegisters = new TabPage();
            this.groupBox6 = new GroupBox();
            this.label117 = new Label();
            this.label116 = new Label();
            this.label114 = new Label();
            this.label113 = new Label();
            this.label35 = new Label();
            this.label36 = new Label();
            this.label37 = new Label();
            this.label38 = new Label();
            this.label39 = new Label();
            this.label40 = new Label();
            this.label41 = new Label();
            this.label42 = new Label();
            this.CH4_TX_Phase_Q = new NumericUpDown();
            this.TX_VM_CH4_POL_Q = new CheckBox();
            this.CH3_TX_Phase_Q = new NumericUpDown();
            this.TX_VM_CH3_POL_Q = new CheckBox();
            this.CH2_TX_Phase_Q = new NumericUpDown();
            this.TX_VM_CH2_POL_Q = new CheckBox();
            this.CH1_TX_Phase_Q = new NumericUpDown();
            this.TX_VM_CH1_POL_Q = new CheckBox();
            this.button5 = new Button();
            this.CH4_TX_Phase_I = new NumericUpDown();
            this.label43 = new Label();
            this.TX_VM_CH4_POL_I = new CheckBox();
            this.CH3_TX_Phase_I = new NumericUpDown();
            this.label44 = new Label();
            this.TX_VM_CH3_POL_I = new CheckBox();
            this.CH2_TX_Phase_I = new NumericUpDown();
            this.label45 = new Label();
            this.TX_VM_CH2_POL_I = new CheckBox();
            this.CH1_TX_Phase_I = new NumericUpDown();
            this.label46 = new Label();
            this.TX_VM_CH1_POL_I = new CheckBox();
            this.groupBox1 = new GroupBox();
            this.label112 = new Label();
            this.button2 = new Button();
            this.label111 = new Label();
            this.label18 = new Label();
            this.label15 = new Label();
            this.label22 = new Label();
            this.label12 = new Label();
            this.label21 = new Label();
            this.label2 = new Label();
            this.CH4_ATTN_TX = new CheckBox();
            this.CH3_ATTN_TX = new CheckBox();
            this.CH2_ATTN_TX = new CheckBox();
            this.CH1_ATTN_TX = new CheckBox();
            this.Gain4_Value = new NumericUpDown();
            this.Gain1_Value = new NumericUpDown();
            this.Gain3_Value = new NumericUpDown();
            this.Gain2_Value = new NumericUpDown();
            this.groupBox21 = new GroupBox();
            this.label141 = new Label();
            this.TX_CHX_RAM_FETCH = new CheckBox();
            this.TX_CHX_RAM_INDEX = new NumericUpDown();
            this.label142 = new Label();
            this.label83 = new Label();
            this.TX_CH4_RAM_FETCH = new CheckBox();
            this.label82 = new Label();
            this.TX_CH3_RAM_FETCH = new CheckBox();
            this.label81 = new Label();
            this.TX_CH2_RAM_FETCH = new CheckBox();
            this.label80 = new Label();
            this.TX_CH1_RAM_FETCH = new CheckBox();
            this.button17 = new Button();
            this.TX_CH4_RAM_INDEX = new NumericUpDown();
            this.label72 = new Label();
            this.TX_CH3_RAM_INDEX = new NumericUpDown();
            this.label73 = new Label();
            this.TX_CH2_RAM_INDEX = new NumericUpDown();
            this.label74 = new Label();
            this.TX_CH1_RAM_INDEX = new NumericUpDown();
            this.label75 = new Label();
            this.groupBox22 = new GroupBox();
            this.label65 = new Label();
            this.TX_DRV_BIAS = new NumericUpDown();
            this.button22 = new Button();
            this.label62 = new Label();
            this.label63 = new Label();
            this.TX_VM_BIAS3 = new NumericUpDown();
            this.TX_VGA_BIAS3 = new NumericUpDown();
            this.label67 = new Label();
            this.Rx_Control = new TabPage();
            this.RX_Init_Label = new Label();
            this.RX_Init_Indicator = new Panel();
            this.RX_Init_Button = new Button();
            this.RX1_Attn_Pic = new PictureBox();
            this.RX4_Attn_Pic = new PictureBox();
            this.RX3_Attn_Pic = new PictureBox();
            this.RX2_Attn_Pic = new PictureBox();
            this.label131 = new Label();
            this.label132 = new Label();
            this.label134 = new Label();
            this.panel7 = new Panel();
            this.RX_CH3_Gain = new ComboBox();
            this.RX_CH3_Phase_Angle = new ComboBox();
            this.button32 = new Button();
            this.RX3_Attn_CheckBox = new CheckBox();
            this.panel8 = new Panel();
            this.RX_CH4_Gain = new ComboBox();
            this.RX_CH4_Phase_Angle = new ComboBox();
            this.button31 = new Button();
            this.RX4_Attn_CheckBox = new CheckBox();
            this.panel9 = new Panel();
            this.RX_CH2_Gain = new ComboBox();
            this.RX_CH2_Phase_Angle = new ComboBox();
            this.button33 = new Button();
            this.RX2_Attn_CheckBox = new CheckBox();
            this.panel10 = new Panel();
            this.RX_CH1_Gain = new ComboBox();
            this.RX1_Attn_CheckBox = new CheckBox();
            this.RX_CH1_Phase_Angle = new ComboBox();
            this.button34 = new Button();
            this.button36 = new Button();
            this.pictureBox6 = new PictureBox();
            this.RXRegisters = new TabPage();
            this.groupBox5 = new GroupBox();
            this.label121 = new Label();
            this.label120 = new Label();
            this.label119 = new Label();
            this.label118 = new Label();
            this.label34 = new Label();
            this.label33 = new Label();
            this.label32 = new Label();
            this.label31 = new Label();
            this.label30 = new Label();
            this.label29 = new Label();
            this.label28 = new Label();
            this.label27 = new Label();
            this.CH4_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH4_POL_Q = new CheckBox();
            this.CH3_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH3_POL_Q = new CheckBox();
            this.CH2_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH2_POL_Q = new CheckBox();
            this.CH1_RX_Phase_Q = new NumericUpDown();
            this.RX_VM_CH1_POL_Q = new CheckBox();
            this.button4 = new Button();
            this.CH4_RX_Phase_I = new NumericUpDown();
            this.label23 = new Label();
            this.RX_VM_CH4_POL_I = new CheckBox();
            this.CH3_RX_Phase_I = new NumericUpDown();
            this.label24 = new Label();
            this.RX_VM_CH3_POL_I = new CheckBox();
            this.CH2_RX_Phase_I = new NumericUpDown();
            this.label25 = new Label();
            this.RX_VM_CH2_POL_I = new CheckBox();
            this.CH1_RX_Phase_I = new NumericUpDown();
            this.label26 = new Label();
            this.RX_VM_CH1_POL_I = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.label20 = new Label();
            this.label17 = new Label();
            this.label16 = new Label();
            this.label9 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.button3 = new Button();
            this.RXGain4 = new NumericUpDown();
            this.RXGain4_Attenuation = new CheckBox();
            this.RXGain3 = new NumericUpDown();
            this.RXGain3_Attenuation = new CheckBox();
            this.RXGain2 = new NumericUpDown();
            this.RXGain2_Attenuation = new CheckBox();
            this.RXGain1 = new NumericUpDown();
            this.label19 = new Label();
            this.RXGain1_Attenuation = new CheckBox();
            this.groupBox20 = new GroupBox();
            this.label139 = new Label();
            this.RX_CHX_RAM_INDEX = new NumericUpDown();
            this.label140 = new Label();
            this.RX_CHX_RAM_FETCH = new CheckBox();
            this.label126 = new Label();
            this.label79 = new Label();
            this.RX_CH4_RAM_FETCH = new CheckBox();
            this.label78 = new Label();
            this.RX_CH3_RAM_FETCH = new CheckBox();
            this.label77 = new Label();
            this.RX_CH2_RAM_FETCH = new CheckBox();
            this.label76 = new Label();
            this.button16 = new Button();
            this.RX_CH4_RAM_INDEX = new NumericUpDown();
            this.label64 = new Label();
            this.RX_CH3_RAM_INDEX = new NumericUpDown();
            this.label66 = new Label();
            this.RX_CH2_RAM_INDEX = new NumericUpDown();
            this.label70 = new Label();
            this.RX_CH1_RAM_INDEX = new NumericUpDown();
            this.label71 = new Label();
            this.RX_CH1_RAM_FETCH = new CheckBox();
            this.groupBox19 = new GroupBox();
            this.label60 = new Label();
            this.LNA_BIAS = new NumericUpDown();
            this.button11 = new Button();
            this.label61 = new Label();
            this.label58 = new Label();
            this.RX_VM_BIAS2 = new NumericUpDown();
            this.RX_VGA_BIAS2 = new NumericUpDown();
            this.label68 = new Label();
            this.TRControl = new TabPage();
            this.groupBox26 = new GroupBox();
            this.RX_EN_2 = new CheckBox();
            this.TX_EN_2 = new CheckBox();
            this.SW_DRV_TR_STATE_2 = new CheckBox();
            this.label124 = new Label();
            this.POL_2 = new CheckBox();
            this.TR_SPI_2 = new CheckBox();
            this.TR_SOURCE_2 = new CheckBox();
            this.SW_DRV_EN_POL_2 = new CheckBox();
            this.SW_DRV_EN_TR_2 = new CheckBox();
            this.button10_2 = new Button();
            this.groupBox25 = new GroupBox();
            this.label123 = new Label();
            this.RX_VGA_EN_2 = new CheckBox();
            this.RX_VM_EN_2 = new CheckBox();
            this.RX_LNA_EN_2 = new CheckBox();
            this.CH4_RX_EN_2 = new CheckBox();
            this.CH3_RX_EN_2 = new CheckBox();
            this.CH2_RX_EN_2 = new CheckBox();
            this.CH1_RX_EN_2 = new CheckBox();
            this.button7_2 = new Button();
            this.groupBox24 = new GroupBox();
            this.CH1_TX_EN_2 = new CheckBox();
            this.label122 = new Label();
            this.button8_2 = new Button();
            this.TX_VGA_EN_2 = new CheckBox();
            this.TX_VM_EN_2 = new CheckBox();
            this.TX_DRV_EN_2 = new CheckBox();
            this.CH4_TX_EN_2 = new CheckBox();
            this.CH3_TX_EN_2 = new CheckBox();
            this.CH2_TX_EN_2 = new CheckBox();
            this.GPIOpins = new TabPage();
            this.TestmodesPanel = new Panel();
            this.groupBox31 = new GroupBox();
            this.label153 = new Label();
            this.TX_BIAS_RAM_FETCH = new CheckBox();
            this.label152 = new Label();
            this.RX_BIAS_RAM_FETCH = new CheckBox();
            this.button40 = new Button();
            this.label151 = new Label();
            this.RX_BIAS_RAM_INDEX = new NumericUpDown();
            this.label154 = new Label();
            this.TX_BIAS_RAM_INDEX = new NumericUpDown();
            this.button39 = new Button();
            this.groupBox30 = new GroupBox();
            this.label147 = new Label();
            this.TX_BEAM_STEP_START = new NumericUpDown();
            this.label148 = new Label();
            this.RX_BEAM_STEP_STOP = new NumericUpDown();
            this.label149 = new Label();
            this.RX_BEAM_STEP_START = new NumericUpDown();
            this.label150 = new Label();
            this.TX_BEAM_STEP_STOP = new NumericUpDown();
            this.button38 = new Button();
            this.groupBox29 = new GroupBox();
            this.label146 = new Label();
            this.TX_TO_RX_DELAY_1 = new NumericUpDown();
            this.label143 = new Label();
            this.RX_TO_TX_DELAY_2 = new NumericUpDown();
            this.label144 = new Label();
            this.RX_TO_TX_DELAY_1 = new NumericUpDown();
            this.label145 = new Label();
            this.TX_TO_RX_DELAY_2 = new NumericUpDown();
            this.button37 = new Button();
            this.GPIO_5 = new CheckBox();
            this.GPIO_52 = new CheckBox();

            this.groupBox17 = new GroupBox();
            this.label103 = new Label();
            this.NVM_DIN = new NumericUpDown();
            this.label101 = new Label();
            this.label100 = new Label();
            this.NVM_ADDR_BYP = new NumericUpDown();
            this.label99 = new Label();
            this.label94 = new Label();
            this.NVM_MARGIN = new CheckBox();
            this.label95 = new Label();
            this.label96 = new Label();
            this.NVM_TEST = new CheckBox();
            this.label98 = new Label();
            this.NVM_PROG_PULSE = new CheckBox();
            this.label92 = new Label();
            this.NVM_CTL_BYP_EN = new CheckBox();
            this.label87 = new Label();
            this.NVM_RD_BYP = new CheckBox();
            this.label88 = new Label();
            this.label90 = new Label();
            this.NVM_ON_BYP = new CheckBox();
            this.label91 = new Label();
            this.NVM_START_BYP = new CheckBox();
            this.label86 = new Label();
            this.FUSE_BYPASS = new CheckBox();
            this.label89 = new Label();
            this.label93 = new Label();
            this.NVM_BIT_SEL = new NumericUpDown();
            this.NVM_REREAD = new CheckBox();
            this.button20 = new Button();
            this.label97 = new Label();
            this.FUSE_CLOCK_CTL = new CheckBox();
            this.groupBox18 = new GroupBox();
            this.label104 = new Label();
            this.LDO_TRIM_BYP_C = new NumericUpDown();
            this.label102 = new Label();
            this.LDO_TRIM_BYP_B = new NumericUpDown();
            this.label109 = new Label();
            this.label115 = new Label();
            this.LDO_TRIM_BYP_A = new NumericUpDown();
            this.button21 = new Button();
            this.groupBox16 = new GroupBox();
            this.label85 = new Label();
            this.label84 = new Label();
            this.LDO_TRIM_REG = new NumericUpDown();
            this.LDO_TRIM_SEL = new NumericUpDown();
            this.button18 = new Button();
            this.button19 = new Button();
            this.groupBox27 = new GroupBox();
            this.TR_ = new CheckBox();
            this.ADDR_1 = new CheckBox();
            this.ADDR_0 = new CheckBox();
            this.TX_LOAD = new CheckBox();
            this.RX_LOAD = new CheckBox();
            this.MISC = new TabPage();
            this.groupBox15 = new GroupBox();
            this.label105 = new Label();
            this.LNA_BIAS_OFF = new NumericUpDown();
            this.label106 = new Label();
            this.label107 = new Label();
            this.label108 = new Label();
            this.CH1PA_BIAS_OFF = new NumericUpDown();
            this.label110 = new Label();
            this.button23 = new Button();
            this.CH4PA_BIAS_OFF = new NumericUpDown();
            this.CH2PA_BIAS_OFF = new NumericUpDown();
            this.CH3PA_BIAS_OFF = new NumericUpDown();
            this.groupBox14 = new GroupBox();
            this.BIAS_RAM_BYPASS = new CheckBox();
            this.RX_CHX_RAM_BYPASS = new CheckBox();
            this.TX_CHX_RAM_BYPASS = new CheckBox();
            this.RX_BEAM_STEP_EN = new CheckBox();
            this.TX_BEAM_STEP_EN = new CheckBox();
            this.Test_Modes_Panel2 = new Panel();
            this.button15 = new Button();
            this.BEAM_RAM_BYPASS = new CheckBox();
            this.SCAN_MODE_EN = new CheckBox();
            this.groupBox12 = new GroupBox();
            this.label59 = new Label();
            this.button13 = new Button();
            this.label69 = new Label();
            this.DRV_GAIN = new CheckBox();
            this.groupBox13 = new GroupBox();
            this.button14 = new Button();
            this.button10 = new Button();
            this.groupBox11 = new GroupBox();
            this.MUX_SEL = new ComboBox();
            this.textBox4 = new TextBox();
            this.button7 = new Button();
            this.label127 = new Label();
            this.ADC_EOC = new TextBox();
            this.label125 = new Label();
            this.label57 = new Label();
            this.label56 = new Label();
            this.ST_CONV = new CheckBox();
            this.CLK_EN = new CheckBox();
            this.ADC_EN = new CheckBox();
            this.ADC_CLKFREQ_SEL = new CheckBox();
            this.button12 = new Button();
            this.groupBox7 = new GroupBox();
            this.label54 = new Label();
            this.button9 = new Button();
            this.CH4_DET_EN = new CheckBox();
            this.CH3_DET_EN = new CheckBox();
            this.CH2_DET_EN = new CheckBox();
            this.CH1_DET_EN = new CheckBox();
            this.LNA_BIAS_OUT_EN = new CheckBox();
            this.BIAS_CTRL = new CheckBox();
            this.groupBox8 = new GroupBox();
            this.label51 = new Label();
            this.LNA_BIAS_ON = new NumericUpDown();
            this.label47 = new Label();
            this.label50 = new Label();
            this.label48 = new Label();
            this.CH1PA_BIAS_ON = new NumericUpDown();
            this.label49 = new Label();
            this.button6 = new Button();
            this.CH4PA_BIAS_ON = new NumericUpDown();
            this.CH2PA_BIAS_ON = new NumericUpDown();
            this.CH3PA_BIAS_ON = new NumericUpDown();
            this.BeamSequencer = new TabPage();
            this.panel2 = new Panel();
            this.PolarPlot = new PictureBox();
            this.label161 = new Label();
            this.button41 = new Button();
            this.label162 = new Label();
            this.DemoGain1 = new ComboBox();
            this.DemoPhase4 = new ComboBox();
            this.DemoPhase1 = new ComboBox();
            this.DemoGain4 = new ComboBox();
            this.label155 = new Label();
            this.label156 = new Label();
            this.label159 = new Label();
            this.DemoGain2 = new ComboBox();
            this.label160 = new Label();
            this.DemoPhase2 = new ComboBox();
            this.DemoPhase3 = new ComboBox();
            this.label158 = new Label();
            this.DemoGain3 = new ComboBox();
            this.label157 = new Label();
            this.panel11 = new Panel();
            this.DemoFileName = new TextBox();
            this.LoadDemoFile = new Button();
            this.SelectDemoFile = new Button();
            this.groupBox9 = new GroupBox();
            this.button27 = new Button();
            this.button24 = new Button();
            this.TimeDelay = new NumericUpDown();
            this.EndPoint = new NumericUpDown();
            this.StartPoint = new NumericUpDown();
            this.label55 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.PhaseLoop = new TabPage();
            this.groupBox28 = new GroupBox();
            this.ADI_logo_demo_button = new Button();
            this.comboBox1 = new ComboBox();
            this.label137 = new Label();
            this.numericUpDown1 = new NumericUpDown();
            this.label138 = new Label();
            this.groupBox23 = new GroupBox();
            this.Start_demo_button = new Button();
            this.Demo_angle_list = new ComboBox();
            this.Demo_loop_time = new NumericUpDown();
            this.label135 = new Label();
            this.label136 = new Label();
            this.groupBox10 = new GroupBox();
            this.RX1_radioButton = new RadioButton();
            this.RX2_radioButton = new RadioButton();
            this.RX3_radioButton = new RadioButton();
            this.RX4_radioButton = new RadioButton();
            this.TX1_radioButton = new RadioButton();
            this.TX2_radioButton = new RadioButton();
            this.TX3_radioButton = new RadioButton();
            this.TX4_radioButton = new RadioButton();
            this.Stop_demo_button = new Button();

            this.ManualRegWrite = new TabPage();
            this.AutoRegWrite = new TabPage();
            this.BeamAlignmentTx = new TabPage();
            this.BeamAlignmentRx = new TabPage();

            this.Tracking_with_PF = new TabPage();

            this.groupBox4 = new GroupBox();

            this.groupBox666 = new GroupBox();
            this.groupBox667 = new GroupBox();
            this.groupBox668 = new GroupBox();
            this.groupBox669 = new GroupBox();

            this.label1 = new Label();
            this.label180 = new Label();
            this.label181 = new Label();
            this.label182 = new Label();
            this.label183 = new Label();
            this.label184 = new Label();
            this.label185 = new Label();
            this.mylabel001 = new Label();
            this.textBox1 = new TextBox();
            this.textBox10 = new TextBox();
            this.ChooseInputButton = new Button();

            this.ValuesToWriteBox = new TextBox();
            this.ValuesToWriteBox00 = new TextBox();
            this.ValuesToWriteBox01 = new TextBox();
            this.ValuesToWriteBox02 = new TextBox();
            this.ValuesToWriteBoxT0 = new TextBox();
            this.ValuesToWriteBoxT1 = new TextBox();
            this.ValuesToWriteBoxT2 = new TextBox();
            this.ValuesToWriteBoxR0 = new TextBox();
            this.ValuesToWriteBoxR1 = new TextBox();
            this.ValuesToWriteBoxR2 = new TextBox();
            this.ValuesToWirteBoxPF01 = new TextBox();

            this.WriteAllButton = new Button();
            this.StartBeamSweeping = new Button();
            this.StartRandomBeamForming = new Button();
            
            this.StartBeamAlignmentTx = new Button();
            this.StartBeamAlignmentTx_using_Loc_info = new Button();
            this.StartBeamAlignmentRx = new Button();
            this.StartBeamAlignmentRx_using_Loc_info = new Button();
            this.StartBeamTracking_SIMO_PF = new Button();


            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0 = new Panel();
            this.BeamSweeping_GUI_Panel_T = new Panel();
            this.BeamSweeping_GUI_Panel_R = new Panel();
            this.BeamTracking_GUI_Panel_SIMO = new Panel();

            this.ReadBack = new TabPage();
            this.panel1 = new Panel();
            this.label133 = new Label();
            this.ROBox = new TextBox();
            this.button26 = new Button();
            this.button8 = new Button();
            this.label53 = new Label();
            this.label52 = new Label();
            this.textBox3 = new TextBox();
            this.textBox2 = new TextBox();
            this.button25 = new Button();
            this.dataGridView1 = new DataGridView();
            this.PasswordPanel = new Panel();
            this.OKButton = new Button();
            this.PasswordBox = new MaskedTextBox();
            this.label4 = new Label();
            this.LoadFileDialog = new OpenFileDialog();
            this.pictureBox1 = new PictureBox();
            this.button1 = new Button();
            this.radioButton1 = new RadioButton();
            this.label10 = new Label();
            this.Registers = new DataGridViewTextBoxColumn();
            this.Written = new DataGridViewTextBoxColumn();
            this.Read_col = new DataGridViewTextBoxColumn();
            //BeamSweeping Graphic By Hyunsoo
            //this.BeamSweeping_GUI_Panel_0.SuspendLayout();
            this.BeamSweeping_GUI_Panel_T.SuspendLayout();
            this.BeamSweeping_GUI_Panel_R.SuspendLayout();
            this.BeamTracking_GUI_Panel_SIMO.SuspendLayout();

            this.MainFormStatusBar.SuspendLayout();
            this.MainFormMenu.SuspendLayout();
            ((ISupportInitialize)this.ADILogo).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ConnectionTab.SuspendLayout();
            this.ConnectionTab2.SuspendLayout();
            ((ISupportInitialize)this.pictureBox3).BeginInit();
            ((ISupportInitialize)this.pictureBox2).BeginInit();
            this.Tx_Control.SuspendLayout();
            ((ISupportInitialize)this.TX1_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.TX4_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.TX3_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.TX2_Attn_Pic).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize)this.pictureBox5).BeginInit();
            this.TXRegisters.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.CH4_TX_Phase_Q.BeginInit();
            this.CH3_TX_Phase_Q.BeginInit();
            this.CH2_TX_Phase_Q.BeginInit();
            this.CH1_TX_Phase_Q.BeginInit();
            this.CH4_TX_Phase_I.BeginInit();
            this.CH3_TX_Phase_I.BeginInit();
            this.CH2_TX_Phase_I.BeginInit();
            this.CH1_TX_Phase_I.BeginInit();
            this.groupBox1.SuspendLayout();
            this.Gain4_Value.BeginInit();
            this.Gain1_Value.BeginInit();
            this.Gain3_Value.BeginInit();
            this.Gain2_Value.BeginInit();
            this.groupBox21.SuspendLayout();
            this.TX_CHX_RAM_INDEX.BeginInit();
            this.TX_CH4_RAM_INDEX.BeginInit();
            this.TX_CH3_RAM_INDEX.BeginInit();
            this.TX_CH2_RAM_INDEX.BeginInit();
            this.TX_CH1_RAM_INDEX.BeginInit();
            this.groupBox22.SuspendLayout();
            this.TX_DRV_BIAS.BeginInit();
            this.TX_VM_BIAS3.BeginInit();
            this.TX_VGA_BIAS3.BeginInit();
            this.Rx_Control.SuspendLayout();
            ((ISupportInitialize)this.RX1_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.RX4_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.RX3_Attn_Pic).BeginInit();
            ((ISupportInitialize)this.RX2_Attn_Pic).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            ((ISupportInitialize)this.pictureBox6).BeginInit();
            this.RXRegisters.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.CH4_RX_Phase_Q.BeginInit();
            this.CH3_RX_Phase_Q.BeginInit();
            this.CH2_RX_Phase_Q.BeginInit();
            this.CH1_RX_Phase_Q.BeginInit();
            this.CH4_RX_Phase_I.BeginInit();
            this.CH3_RX_Phase_I.BeginInit();
            this.CH2_RX_Phase_I.BeginInit();
            this.CH1_RX_Phase_I.BeginInit();
            this.groupBox3.SuspendLayout();
            this.RXGain4.BeginInit();
            this.RXGain3.BeginInit();
            this.RXGain2.BeginInit();
            this.RXGain1.BeginInit();
            this.groupBox20.SuspendLayout();
            this.RX_CHX_RAM_INDEX.BeginInit();
            this.RX_CH4_RAM_INDEX.BeginInit();
            this.RX_CH3_RAM_INDEX.BeginInit();
            this.RX_CH2_RAM_INDEX.BeginInit();
            this.RX_CH1_RAM_INDEX.BeginInit();
            this.groupBox19.SuspendLayout();
            this.LNA_BIAS.BeginInit();
            this.RX_VM_BIAS2.BeginInit();
            this.RX_VGA_BIAS2.BeginInit();
            this.TRControl.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.GPIOpins.SuspendLayout();
            //this.GPIOpins2.SuspendLayout();귀찮아아ㅏㅏㅏ
            this.TestmodesPanel.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.RX_BIAS_RAM_INDEX.BeginInit();
            this.TX_BIAS_RAM_INDEX.BeginInit();
            this.groupBox30.SuspendLayout();
            this.TX_BEAM_STEP_START.BeginInit();
            this.RX_BEAM_STEP_STOP.BeginInit();
            this.RX_BEAM_STEP_START.BeginInit();
            this.TX_BEAM_STEP_STOP.BeginInit();
            this.groupBox29.SuspendLayout();
            this.TX_TO_RX_DELAY_1.BeginInit();
            this.RX_TO_TX_DELAY_2.BeginInit();
            this.RX_TO_TX_DELAY_1.BeginInit();
            this.TX_TO_RX_DELAY_2.BeginInit();
            this.groupBox17.SuspendLayout();
            this.NVM_DIN.BeginInit();
            this.NVM_ADDR_BYP.BeginInit();
            this.NVM_BIT_SEL.BeginInit();
            this.groupBox18.SuspendLayout();
            this.LDO_TRIM_BYP_C.BeginInit();
            this.LDO_TRIM_BYP_B.BeginInit();
            this.LDO_TRIM_BYP_A.BeginInit();
            this.groupBox16.SuspendLayout();
            this.LDO_TRIM_REG.BeginInit();
            this.LDO_TRIM_SEL.BeginInit();
            this.groupBox27.SuspendLayout();
            this.MISC.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.LNA_BIAS_OFF.BeginInit();
            this.CH1PA_BIAS_OFF.BeginInit();
            this.CH4PA_BIAS_OFF.BeginInit();
            this.CH2PA_BIAS_OFF.BeginInit();
            this.CH3PA_BIAS_OFF.BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.LNA_BIAS_ON.BeginInit();
            this.CH1PA_BIAS_ON.BeginInit();
            this.CH4PA_BIAS_ON.BeginInit();
            this.CH2PA_BIAS_ON.BeginInit();
            this.CH3PA_BIAS_ON.BeginInit();
            this.BeamSequencer.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize)this.PolarPlot).BeginInit();
            this.panel11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.TimeDelay.BeginInit();
            this.EndPoint.BeginInit();
            this.StartPoint.BeginInit();
            this.PhaseLoop.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.numericUpDown1.BeginInit();
            this.groupBox23.SuspendLayout();
            this.Demo_loop_time.BeginInit();
            this.groupBox10.SuspendLayout();
            this.ManualRegWrite.SuspendLayout();

            this.AutoRegWrite.SuspendLayout();
            this.BeamAlignmentTx.SuspendLayout();
            this.BeamAlignmentRx.SuspendLayout();
            this.Tracking_with_PF.SuspendLayout();


            this.groupBox4.SuspendLayout();

            this.groupBox666.SuspendLayout();
            this.groupBox667.SuspendLayout();
            this.groupBox668.SuspendLayout();
            this.groupBox669.SuspendLayout();



            this.ReadBack.SuspendLayout();
            ((ISupportInitialize)this.dataGridView1).BeginInit();
            this.PasswordPanel.SuspendLayout();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.SuspendLayout();
            this.MainFormStatusBar.ImageScalingSize = new Size(20, 20);
            this.MainFormStatusBar.Items.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.DeviceConnectionStatus,
        (ToolStripItem) this.StatusBarLabel,
        (ToolStripItem) this.StatusBarLabel2
            });
            this.MainFormStatusBar.Location = new Point(0, 609);
            this.MainFormStatusBar.Name = "MainFormStatusBar";
            this.MainFormStatusBar.Padding = new Padding(1, 0, 19, 0);
            this.MainFormStatusBar.Size = new Size(1195, 22);
            this.MainFormStatusBar.TabIndex = 0;
            this.MainFormStatusBar.Text = "statusStrip1";

            this.DeviceConnectionStatus.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.DeviceConnectionStatus.ForeColor = Color.Tomato;
            this.DeviceConnectionStatus.Name = "DeviceConnectionStatus";
            this.DeviceConnectionStatus.Size = new Size(151, 17);
            this.DeviceConnectionStatus.Text = "No device connected";

            this.DeviceConnectionStatus2.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.DeviceConnectionStatus2.ForeColor = Color.Tomato;
            this.DeviceConnectionStatus2.Name = "DeviceConnectionStatus2";
            this.DeviceConnectionStatus2.Size = new Size(151, 17);
            this.DeviceConnectionStatus2.Text = "No device connected2";

            this.StatusBarLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            this.StatusBarLabel.Name = "StatusBarLabel";
            this.StatusBarLabel.Size = new Size(4, 17);
            this.StatusBarLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left;
            this.StatusBarLabel2.Name = "StatusBarLabel2";
            this.StatusBarLabel2.Size = new Size(4, 17);
            this.EventLog.Location = new Point(5, 516);
            this.EventLog.Margin = new Padding(4);
            this.EventLog.Multiline = true;
            this.EventLog.Name = "EventLog";
            this.EventLog.ReadOnly = true;
            this.EventLog.ScrollBars = ScrollBars.Vertical;
            //
            this.EventLog.Size = new Size(650, 500);//650
            //이게 아래 이벤트로그 전체 길이
            this.EventLog.TabIndex = 11;
            this.EventLog.Text = "Application started.";
            this.MainFormMenu.ImageScalingSize = new Size(20, 20);
            this.MainFormMenu.Items.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
            });
            this.MainFormMenu.Location = new Point(0, 0);
            this.MainFormMenu.Name = "MainFormMenu";
            this.MainFormMenu.Padding = new Padding(8, 2, 0, 2);
            this.MainFormMenu.Size = new Size(1195, 28);
            this.MainFormMenu.TabIndex = 2;
            this.MainFormMenu.Text = "menuStrip1";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
        (ToolStripItem) this.saveConfigurationToolStripMenuItem,
        (ToolStripItem) this.loadConfigurationToolStripMenuItem,
        (ToolStripItem) this.exitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.Size = new Size(212, 26);
            this.saveConfigurationToolStripMenuItem.Text = "Save Configuration";
            this.saveConfigurationToolStripMenuItem.Click += new EventHandler(this.SaveConfigurationStripMenuItem_Click);
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new Size(212, 26);
            this.loadConfigurationToolStripMenuItem.Text = "Load Configuration";
            this.loadConfigurationToolStripMenuItem.Click += new EventHandler(this.LoadConfigurationStripMenuItem_Click);
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(212, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
            {
        (ToolStripItem) this.aboutToolStripMenuItem
            });
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new Size(125, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);

            //this.ADILogo.Image = (Image)componentResourceManager.GetObject("5G-resized.image");
            //this.ADILogo.InitialImage = (Image)null;
            //this.ADILogo.Location = new Point(660, 516);
            //this.ADILogo.Margin = new Padding(4);
            //this.ADILogo.Name = "ADILogo";
            ////
            //this.ADILogo.Size = new Size(600, 250);
            //// WSL 로고 사이즈
            //this.ADILogo.TabIndex = 12;
            //this.ADILogo.TabStop = false;
            //this.ADILogo.DoubleClick += new EventHandler(this.ADILogo_Click);

            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.label8.Location = new Point(15, 70);
            this.label8.Margin = new Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(26, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "0x";
            this.R0Button.Location = new Point(139, 68);
            this.R0Button.Margin = new Padding(4);
            this.R0Button.Name = "R0Button";
            this.R0Button.Size = new Size(119, 25);
            this.R0Button.TabIndex = 1;
            this.R0Button.Text = "Write";
            this.R0Button.UseVisualStyleBackColor = true;
            this.R0Button.Click += new EventHandler(this.R0Button_Click);
            this.R0Box.Location = new Point(12, 68);
            this.R0Box.Margin = new Padding(4);
            this.R0Box.Name = "R0Box";
            this.R0Box.RightToLeft = RightToLeft.Yes;
            this.R0Box.Size = new Size(117, 22);
            this.R0Box.TabIndex = 0;
            this.R0Box.Text = "00099";
            this.groupBox2.Controls.Add((Control)this.label3);
            this.groupBox2.Controls.Add((Control)this.label8);
            this.groupBox2.Controls.Add((Control)this.R0Box);
            this.groupBox2.Controls.Add((Control)this.R0Button);
            this.groupBox2.Location = new Point(439, 8);
            this.groupBox2.Margin = new Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(4);
            this.groupBox2.Size = new Size(267, 119);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Values to write";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(80, 34);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "(hex values)";
            this.tabControl1.Controls.Add((Control)this.ConnectionTab);
            //this.tabControl1.Controls.Add((Control)this.Tx_Control);
            //this.tabControl1.Controls.Add((Control)this.TXRegisters);
            //this.tabControl1.Controls.Add((Control)this.Rx_Control);
            //this.tabControl1.Controls.Add((Control)this.RXRegisters);
            //this.tabControl1.Controls.Add((Control)this.TRControl);
            //this.tabControl1.Controls.Add((Control)this.GPIOpins);
            //this.tabControl1.Controls.Add((Control)this.MISC);
            //this.tabControl1.Controls.Add((Control)this.BeamSequencer);
            //this.tabControl1.Controls.Add((Control)this.PhaseLoop);
            this.tabControl1.Controls.Add((Control)this.ManualRegWrite);
            this.tabControl1.Controls.Add((Control)this.AutoRegWrite);
            this.tabControl1.Controls.Add((Control)this.BeamAlignmentTx);
            this.tabControl1.Controls.Add((Control)this.BeamAlignmentRx);
            this.tabControl1.Controls.Add((Control)this.Tracking_with_PF);


            this.tabControl1.Controls.Add((Control)this.ReadBack);
            this.tabControl1.Location = new Point(2, 33);
            this.tabControl1.Margin = new Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1193, 475);
            //컨트롤 패널 전체사이즈
            this.tabControl1.TabIndex = 14;
            //this.ConnectionTab.Controls.Add((Control)this.ADDR1_checkBox);
            //this.ConnectionTab.Controls.Add((Control)this.ADDR0_checkBox);
            this.ConnectionTab.Controls.Add((Control)this.pictureBox3);
            this.ConnectionTab.Controls.Add((Control)this.pictureBox2);
            this.ConnectionTab.Controls.Add((Control)this.ConnectSDPButton);
            this.ConnectionTab.Controls.Add((Control)this.label11);

            this.ConnectionTab.Controls.Add((Control)this.ConnectSDPButton2);



            this.ConnectionTab.Location = new Point(4, 25);
            this.ConnectionTab.Margin = new Padding(4);
            this.ConnectionTab.Name = "ConnectionTab";
            this.ConnectionTab.Padding = new Padding(4);
            this.ConnectionTab.Size = new Size(1185, 446);
            this.ConnectionTab.TabIndex = 3;
            this.ConnectionTab.Text = "Connection";
            this.ConnectionTab.ToolTipText = "Verify connectivity to SDP Interface Board and to ADAR1000 Evaluation Board";
            this.ConnectionTab.UseVisualStyleBackColor = true;
            this.ADDR1_checkBox.AutoSize = true;
            this.ADDR1_checkBox.Location = new Point(589, 219);
            this.ADDR1_checkBox.Name = "ADDR1_checkBox";
            this.ADDR1_checkBox.Size = new Size(77, 21);
            this.ADDR1_checkBox.TabIndex = 15;
            this.ADDR1_checkBox.Text = "ADDR1";
            this.ADDR1_checkBox.UseVisualStyleBackColor = true;
            this.ADDR1_checkBox.CheckedChanged += new EventHandler(this.ADDR_Check_Changed);
            this.ADDR0_checkBox.AutoSize = true;
            this.ADDR0_checkBox.Location = new Point(589, 192);
            this.ADDR0_checkBox.Name = "ADDR0_checkBox";
            this.ADDR0_checkBox.Size = new Size(77, 21);
            this.ADDR0_checkBox.TabIndex = 14;
            this.ADDR0_checkBox.Text = "ADDR0";
            this.ADDR0_checkBox.UseVisualStyleBackColor = true;
            this.ADDR0_checkBox.CheckedChanged += new EventHandler(this.ADDR_Check_Changed);
            this.pictureBox3.Image = (Image)componentResourceManager.GetObject("pictureBox3.Image");
            this.pictureBox3.InitialImage = (Image)componentResourceManager.GetObject("pictureBox3.InitialImage");
            this.pictureBox3.Location = new Point(56, 89);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Size(126, 240);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox2.Image = (Image)componentResourceManager.GetObject("pictureBox2.Image");
            this.pictureBox2.Location = new Point(237, 89);
            this.pictureBox2.Margin = new Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(235, 239);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;

            this.ConnectSDPButton.Location = new Point(792, 182);
            this.ConnectSDPButton.Margin = new Padding(4);
            this.ConnectSDPButton.Name = "ConnectSDPButton";
            this.ConnectSDPButton.Size = new Size(200, 58);
            this.ConnectSDPButton.TabIndex = 8;
            this.ConnectSDPButton.Text = "Connect1 : Forward";
            this.ConnectSDPButton.UseVisualStyleBackColor = true;
            this.ConnectSDPButton.Click += new EventHandler(this.ConnectSDPButton_Click);


            this.ConnectSDPButton2.Location = new Point(792, 262);
            this.ConnectSDPButton2.Margin = new Padding(4);
            this.ConnectSDPButton2.Name = "ConnectSDPButton2";
            this.ConnectSDPButton2.Size = new Size(200, 58);
            this.ConnectSDPButton2.TabIndex = 8;
            this.ConnectSDPButton2.Text = "Connect2 : Backward";
            this.ConnectSDPButton2.UseVisualStyleBackColor = true;
            this.ConnectSDPButton2.Click += new EventHandler(this.ConnectSDPButton_Click2);

            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label11.Location = new Point(479, 372);
            this.label11.Margin = new Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new Size(140, 25);
            this.label11.TabIndex = 9;
            this.label11.Text = "Connecting...";
            this.label11.Visible = false;
            this.Tx_Control.Controls.Add((Control)this.TX_Init_Label);
            this.Tx_Control.Controls.Add((Control)this.TX_Init_Indicator);
            this.Tx_Control.Controls.Add((Control)this.TX_Init_Button);
            this.Tx_Control.Controls.Add((Control)this.TX1_Attn_Pic);
            this.Tx_Control.Controls.Add((Control)this.TX4_Attn_Pic);
            this.Tx_Control.Controls.Add((Control)this.TX3_Attn_Pic);
            this.Tx_Control.Controls.Add((Control)this.TX2_Attn_Pic);
            this.Tx_Control.Controls.Add((Control)this.label130);
            this.Tx_Control.Controls.Add((Control)this.button35);
            this.Tx_Control.Controls.Add((Control)this.label129);
            this.Tx_Control.Controls.Add((Control)this.panel5);
            this.Tx_Control.Controls.Add((Control)this.panel6);
            this.Tx_Control.Controls.Add((Control)this.panel4);
            this.Tx_Control.Controls.Add((Control)this.label128);
            this.Tx_Control.Controls.Add((Control)this.panel3);
            this.Tx_Control.Controls.Add((Control)this.pictureBox5);
            this.Tx_Control.Location = new Point(4, 25);
            this.Tx_Control.Name = "Tx_Control";
            this.Tx_Control.Size = new Size(1185, 446);
            this.Tx_Control.TabIndex = 14;
            this.Tx_Control.Text = "Tx Control";
            this.Tx_Control.ToolTipText = "Set Tx gain a phase directly by inputting a gain index and an angle";
            this.Tx_Control.UseVisualStyleBackColor = true;
            this.TX_Init_Label.AutoSize = true;
            this.TX_Init_Label.Location = new Point(772, 379);
            this.TX_Init_Label.Name = "TX_Init_Label";
            this.TX_Init_Label.Size = new Size(93, 17);
            this.TX_Init_Label.TabIndex = 38;
            this.TX_Init_Label.Text = "Not Initalised ";
            this.TX_Init_Indicator.BackColor = Color.Red;
            this.TX_Init_Indicator.Location = new Point(738, 373);
            this.TX_Init_Indicator.Name = "TX_Init_Indicator";
            this.TX_Init_Indicator.Size = new Size(28, 28);
            this.TX_Init_Indicator.TabIndex = 37;
            this.TX_Init_Button.Location = new Point(632, 373);
            this.TX_Init_Button.Name = "TX_Init_Button";
            this.TX_Init_Button.Size = new Size(100, 28);
            this.TX_Init_Button.TabIndex = 35;
            this.TX_Init_Button.Text = "Tx Initalise";
            this.TX_Init_Button.UseVisualStyleBackColor = true;
            this.TX_Init_Button.Click += new EventHandler(this.TX_Init_Button_Click);
            this.TX1_Attn_Pic.ErrorImage = (Image)null;
            this.TX1_Attn_Pic.Image = (Image)componentResourceManager.GetObject("TX1_Attn_Pic.Image");
            this.TX1_Attn_Pic.InitialImage = (Image)null;
            this.TX1_Attn_Pic.Location = new Point(251, 215);
            this.TX1_Attn_Pic.Name = "TX1_Attn_Pic";
            this.TX1_Attn_Pic.Size = new Size(43, 47);
            this.TX1_Attn_Pic.TabIndex = 19;
            this.TX1_Attn_Pic.TabStop = false;
            this.TX4_Attn_Pic.ErrorImage = (Image)null;
            this.TX4_Attn_Pic.Image = (Image)componentResourceManager.GetObject("TX4_Attn_Pic.Image");
            this.TX4_Attn_Pic.InitialImage = (Image)null;
            this.TX4_Attn_Pic.Location = new Point(310, 215);
            this.TX4_Attn_Pic.Name = "TX4_Attn_Pic";
            this.TX4_Attn_Pic.Size = new Size(33, 48);
            this.TX4_Attn_Pic.TabIndex = 18;
            this.TX4_Attn_Pic.TabStop = false;
            this.TX3_Attn_Pic.ErrorImage = (Image)null;
            this.TX3_Attn_Pic.Image = (Image)componentResourceManager.GetObject("TX3_Attn_Pic.Image");
            this.TX3_Attn_Pic.InitialImage = (Image)null;
            this.TX3_Attn_Pic.Location = new Point(311, 151);
            this.TX3_Attn_Pic.Name = "TX3_Attn_Pic";
            this.TX3_Attn_Pic.Size = new Size(33, 46);
            this.TX3_Attn_Pic.TabIndex = 17;
            this.TX3_Attn_Pic.TabStop = false;
            this.TX2_Attn_Pic.ErrorImage = (Image)null;
            this.TX2_Attn_Pic.Image = (Image)componentResourceManager.GetObject("TX2_Attn_Pic.Image");
            this.TX2_Attn_Pic.InitialImage = (Image)null;
            this.TX2_Attn_Pic.Location = new Point(249, 152);
            this.TX2_Attn_Pic.Name = "TX2_Attn_Pic";
            this.TX2_Attn_Pic.Size = new Size(43, 47);
            this.TX2_Attn_Pic.TabIndex = 16;
            this.TX2_Attn_Pic.TabStop = false;
            this.label130.AutoSize = true;
            this.label130.Location = new Point(902, 21);
            this.label130.Name = "label130";
            this.label130.Size = new Size(75, 17);
            this.label130.TabIndex = 15;
            this.label130.Text = "Gain Index";
            this.button35.Location = new Point(1031, 373);
            this.button35.Name = "button35";
            this.button35.Size = new Size(100, 28);
            this.button35.TabIndex = 14;
            this.button35.Text = "Load All";
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new EventHandler(this.TX_Write_All_Button);
            this.label129.AutoSize = true;
            this.label129.Location = new Point(639, 21);
            this.label129.Name = "label129";
            this.label129.Size = new Size(80, 17);
            this.label129.TabIndex = 13;
            this.label129.Text = "Attenuation";
            this.panel5.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.panel5.Controls.Add((Control)this.TX_CH3_Gain);
            this.panel5.Controls.Add((Control)this.TX3_Attn_CheckBox);
            this.panel5.Controls.Add((Control)this.button30);
            this.panel5.Controls.Add((Control)this.TX_CH3_Phase_Angle);
            this.panel5.Location = new Point(618, 209);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(530, 76);
            this.panel5.TabIndex = 12;
            this.TX_CH3_Gain.FormattingEnabled = true;
            this.TX_CH3_Gain.Location = new Point(264, 26);
            this.TX_CH3_Gain.Name = "TX_CH3_Gain";
            this.TX_CH3_Gain.Size = new Size(103, 24);
            this.TX_CH3_Gain.TabIndex = 8;
            this.TX_CH3_Gain.Text = "0";
            this.TX3_Attn_CheckBox.AutoSize = true;
            this.TX3_Attn_CheckBox.Location = new Point(55, 30);
            this.TX3_Attn_CheckBox.Name = "TX3_Attn_CheckBox";
            this.TX3_Attn_CheckBox.Size = new Size(18, 17);
            this.TX3_Attn_CheckBox.TabIndex = 7;
            this.TX3_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.TX3_Attn_CheckBox.CheckedChanged += new EventHandler(this.TX3_Attn_CheckBox_CheckedChanged);
            this.button30.Location = new Point(413, 25);
            this.button30.Name = "button30";
            this.button30.Size = new Size(100, 25);
            this.button30.TabIndex = 6;
            this.button30.Text = "TX3 Load";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new EventHandler(this.TX_CH3_block_button_Click);
            this.TX_CH3_Phase_Angle.FormattingEnabled = true;
            this.TX_CH3_Phase_Angle.Location = new Point(128, 26);
            this.TX_CH3_Phase_Angle.Name = "TX_CH3_Phase_Angle";
            this.TX_CH3_Phase_Angle.Size = new Size(103, 24);
            this.TX_CH3_Phase_Angle.TabIndex = 5;
            this.TX_CH3_Phase_Angle.Text = "0";
            this.panel6.BackColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192);
            this.panel6.Controls.Add((Control)this.TX_CH4_Gain);
            this.panel6.Controls.Add((Control)this.TX4_Attn_CheckBox);
            this.panel6.Controls.Add((Control)this.button29);
            this.panel6.Controls.Add((Control)this.TX_CH4_Phase_Angle);
            this.panel6.Location = new Point(618, 291);
            this.panel6.Name = "panel6";
            this.panel6.Size = new Size(530, 76);
            this.panel6.TabIndex = 12;
            this.TX_CH4_Gain.FormattingEnabled = true;
            this.TX_CH4_Gain.Location = new Point(264, 26);
            this.TX_CH4_Gain.Name = "TX_CH4_Gain";
            this.TX_CH4_Gain.Size = new Size(103, 24);
            this.TX_CH4_Gain.TabIndex = 10;
            this.TX_CH4_Gain.Text = "0";
            this.TX4_Attn_CheckBox.AutoSize = true;
            this.TX4_Attn_CheckBox.Location = new Point(55, 30);
            this.TX4_Attn_CheckBox.Name = "TX4_Attn_CheckBox";
            this.TX4_Attn_CheckBox.Size = new Size(18, 17);
            this.TX4_Attn_CheckBox.TabIndex = 9;
            this.TX4_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.TX4_Attn_CheckBox.CheckedChanged += new EventHandler(this.TX4_Attn_CheckBox_CheckedChanged);
            this.button29.Location = new Point(413, 25);
            this.button29.Name = "button29";
            this.button29.Size = new Size(100, 25);
            this.button29.TabIndex = 8;
            this.button29.Text = "TX4 Load";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new EventHandler(this.TX_CH4_block_button_Click);
            this.TX_CH4_Phase_Angle.DisplayMember = "0";
            this.TX_CH4_Phase_Angle.FormattingEnabled = true;
            this.TX_CH4_Phase_Angle.Location = new Point(128, 26);
            this.TX_CH4_Phase_Angle.Name = "TX_CH4_Phase_Angle";
            this.TX_CH4_Phase_Angle.Size = new Size(103, 24);
            this.TX_CH4_Phase_Angle.TabIndex = 7;
            this.TX_CH4_Phase_Angle.Text = "0";
            this.panel4.BackColor = Color.FromArgb(192, (int)byte.MaxValue, 192);
            this.panel4.Controls.Add((Control)this.TX_CH2_Gain);
            this.panel4.Controls.Add((Control)this.TX2_Attn_CheckBox);
            this.panel4.Controls.Add((Control)this.button28);
            this.panel4.Controls.Add((Control)this.TX_CH2_Phase_Angle);
            this.panel4.Location = new Point(618, (int)sbyte.MaxValue);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(530, 76);
            this.panel4.TabIndex = 11;
            this.TX_CH2_Gain.FormattingEnabled = true;
            this.TX_CH2_Gain.Location = new Point(264, 26);
            this.TX_CH2_Gain.Name = "TX_CH2_Gain";
            this.TX_CH2_Gain.Size = new Size(103, 24);
            this.TX_CH2_Gain.TabIndex = 6;
            this.TX_CH2_Gain.Text = "0";
            this.TX2_Attn_CheckBox.AutoSize = true;
            this.TX2_Attn_CheckBox.Location = new Point(55, 30);
            this.TX2_Attn_CheckBox.Name = "TX2_Attn_CheckBox";
            this.TX2_Attn_CheckBox.Size = new Size(18, 17);
            this.TX2_Attn_CheckBox.TabIndex = 5;
            this.TX2_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.TX2_Attn_CheckBox.CheckedChanged += new EventHandler(this.TX2_Attn_CheckBox_CheckedChanged);
            this.button28.Location = new Point(413, 25);
            this.button28.Name = "button28";
            this.button28.Size = new Size(100, 25);
            this.button28.TabIndex = 4;
            this.button28.Text = "TX2 Load";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new EventHandler(this.TX_CH2_block_button_Click);
            this.TX_CH2_Phase_Angle.FormattingEnabled = true;
            this.TX_CH2_Phase_Angle.Location = new Point(128, 26);
            this.TX_CH2_Phase_Angle.Name = "TX_CH2_Phase_Angle";
            this.TX_CH2_Phase_Angle.Size = new Size(103, 24);
            this.TX_CH2_Phase_Angle.TabIndex = 3;
            this.TX_CH2_Phase_Angle.Text = "0";
            this.label128.AutoSize = true;
            this.label128.Location = new Point(743, 21);
            this.label128.Name = "label128";
            this.label128.Size = new Size(112, 17);
            this.label128.TabIndex = 10;
            this.label128.Text = "Phase Angle ( °)";
            this.panel3.BackColor = Color.MistyRose;
            this.panel3.Controls.Add((Control)this.TX_CH1_Gain);
            this.panel3.Controls.Add((Control)this.TX1_Attn_CheckBox);
            this.panel3.Controls.Add((Control)this.TX_CH1_Phase_Angle);
            this.panel3.Controls.Add((Control)this.phase_block_button);
            this.panel3.Location = new Point(618, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(530, 76);
            this.panel3.TabIndex = 9;
            this.TX_CH1_Gain.FormattingEnabled = true;
            this.TX_CH1_Gain.Location = new Point(264, 26);
            this.TX_CH1_Gain.Name = "TX_CH1_Gain";
            this.TX_CH1_Gain.Size = new Size(103, 24);
            this.TX_CH1_Gain.TabIndex = 4;
            this.TX_CH1_Gain.Text = "0";
            this.TX1_Attn_CheckBox.AutoSize = true;
            this.TX1_Attn_CheckBox.Location = new Point(55, 30);
            this.TX1_Attn_CheckBox.Name = "TX1_Attn_CheckBox";
            this.TX1_Attn_CheckBox.Size = new Size(18, 17);
            this.TX1_Attn_CheckBox.TabIndex = 3;
            this.TX1_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.TX1_Attn_CheckBox.CheckedChanged += new EventHandler(this.TX1_Attn_CheckBox_CheckedChanged);
            this.TX_CH1_Phase_Angle.FormattingEnabled = true;
            this.TX_CH1_Phase_Angle.Location = new Point(128, 26);
            this.TX_CH1_Phase_Angle.Name = "TX_CH1_Phase_Angle";
            this.TX_CH1_Phase_Angle.Size = new Size(103, 24);
            this.TX_CH1_Phase_Angle.TabIndex = 1;
            this.TX_CH1_Phase_Angle.Text = "0";
            this.TX_CH1_Phase_Angle.SelectedIndexChanged += new EventHandler(this.TX_CH1_Phase_Angle_SelectedIndexChanged);
            this.phase_block_button.Location = new Point(413, 25);
            this.phase_block_button.Name = "phase_block_button";
            this.phase_block_button.Size = new Size(100, 25);
            this.phase_block_button.TabIndex = 2;
            this.phase_block_button.Text = "TX1 Load";
            this.phase_block_button.UseVisualStyleBackColor = true;
            this.phase_block_button.Click += new EventHandler(this.TX_CH1_block_button_Click);
            this.pictureBox5.Image = (Image)componentResourceManager.GetObject("pictureBox5.Image");
            this.pictureBox5.Location = new Point(20, 21);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new Size(560, 398);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            this.TXRegisters.Controls.Add((Control)this.groupBox6);
            this.TXRegisters.Controls.Add((Control)this.groupBox1);
            this.TXRegisters.Controls.Add((Control)this.groupBox21);
            this.TXRegisters.Controls.Add((Control)this.groupBox22);
            this.TXRegisters.Location = new Point(4, 25);
            this.TXRegisters.Name = "TXRegisters";
            this.TXRegisters.Padding = new Padding(3);
            this.TXRegisters.Size = new Size(1185, 446);
            this.TXRegisters.TabIndex = 9;
            this.TXRegisters.Text = "TX Registers";
            this.TXRegisters.ToolTipText = "Adjust Tx setting at a register level";
            this.TXRegisters.UseVisualStyleBackColor = true;
            this.groupBox6.Controls.Add((Control)this.label117);
            this.groupBox6.Controls.Add((Control)this.label116);
            this.groupBox6.Controls.Add((Control)this.label114);
            this.groupBox6.Controls.Add((Control)this.label113);
            this.groupBox6.Controls.Add((Control)this.label35);
            this.groupBox6.Controls.Add((Control)this.label36);
            this.groupBox6.Controls.Add((Control)this.label37);
            this.groupBox6.Controls.Add((Control)this.label38);
            this.groupBox6.Controls.Add((Control)this.label39);
            this.groupBox6.Controls.Add((Control)this.label40);
            this.groupBox6.Controls.Add((Control)this.label41);
            this.groupBox6.Controls.Add((Control)this.label42);
            this.groupBox6.Controls.Add((Control)this.CH4_TX_Phase_Q);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH4_POL_Q);
            this.groupBox6.Controls.Add((Control)this.CH3_TX_Phase_Q);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH3_POL_Q);
            this.groupBox6.Controls.Add((Control)this.CH2_TX_Phase_Q);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH2_POL_Q);
            this.groupBox6.Controls.Add((Control)this.CH1_TX_Phase_Q);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH1_POL_Q);
            this.groupBox6.Controls.Add((Control)this.button5);
            this.groupBox6.Controls.Add((Control)this.CH4_TX_Phase_I);
            this.groupBox6.Controls.Add((Control)this.label43);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH4_POL_I);
            this.groupBox6.Controls.Add((Control)this.CH3_TX_Phase_I);
            this.groupBox6.Controls.Add((Control)this.label44);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH3_POL_I);
            this.groupBox6.Controls.Add((Control)this.CH2_TX_Phase_I);
            this.groupBox6.Controls.Add((Control)this.label45);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH2_POL_I);
            this.groupBox6.Controls.Add((Control)this.CH1_TX_Phase_I);
            this.groupBox6.Controls.Add((Control)this.label46);
            this.groupBox6.Controls.Add((Control)this.TX_VM_CH1_POL_I);
            this.groupBox6.Location = new Point(272, 7);
            this.groupBox6.Margin = new Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new Padding(4);
            this.groupBox6.Size = new Size(260, 412);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tx Phase Control";
            this.label117.AutoSize = true;
            this.label117.Location = new Point(130, 273);
            this.label117.Name = "label117";
            this.label117.Size = new Size(118, 17);
            this.label117.TabIndex = 32;
            this.label117.Text = "TX VM CH4 GAIN";
            this.label116.AutoSize = true;
            this.label116.Location = new Point(130, 194);
            this.label116.Name = "label116";
            this.label116.Size = new Size(118, 17);
            this.label116.TabIndex = 31;
            this.label116.Text = "TX VM CH3 GAIN";
            this.label114.AutoSize = true;
            this.label114.Location = new Point(126, 115);
            this.label114.Name = "label114";
            this.label114.Size = new Size(118, 17);
            this.label114.TabIndex = 30;
            this.label114.Text = "TX VM CH2 GAIN";
            this.label113.AutoSize = true;
            this.label113.Location = new Point(130, 36);
            this.label113.Name = "label113";
            this.label113.Size = new Size(118, 17);
            this.label113.TabIndex = 29;
            this.label113.Text = "TX VM CH1 GAIN";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(26, 322);
            this.label35.Name = "label35";
            this.label35.Size = new Size(19, 17);
            this.label35.TabIndex = 28;
            this.label35.Text = "Q";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(26, 240);
            this.label36.Name = "label36";
            this.label36.Size = new Size(19, 17);
            this.label36.TabIndex = 27;
            this.label36.Text = "Q";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(26, 163);
            this.label37.Name = "label37";
            this.label37.Size = new Size(19, 17);
            this.label37.TabIndex = 26;
            this.label37.Text = "Q";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(26, 85);
            this.label38.Name = "label38";
            this.label38.Size = new Size(19, 17);
            this.label38.TabIndex = 25;
            this.label38.Text = "Q";
            this.label39.AutoSize = true;
            this.label39.Location = new Point(26, 296);
            this.label39.Name = "label39";
            this.label39.Size = new Size(11, 17);
            this.label39.TabIndex = 24;
            this.label39.Text = "I";
            this.label40.AutoSize = true;
            this.label40.Location = new Point(26, 215);
            this.label40.Name = "label40";
            this.label40.Size = new Size(11, 17);
            this.label40.TabIndex = 23;
            this.label40.Text = "I";
            this.label41.AutoSize = true;
            this.label41.Location = new Point(26, 138);
            this.label41.Name = "label41";
            this.label41.Size = new Size(11, 17);
            this.label41.TabIndex = 22;
            this.label41.Text = "I";
            this.label42.AutoSize = true;
            this.label42.Location = new Point(26, 60);
            this.label42.Name = "label42";
            this.label42.Size = new Size(11, 17);
            this.label42.TabIndex = 21;
            this.label42.Text = "I";
            this.CH4_TX_Phase_Q.Location = new Point(133, 320);
            this.CH4_TX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH4_TX_Phase_Q.Name = "CH4_TX_Phase_Q";
            this.CH4_TX_Phase_Q.Size = new Size(120, 22);
            this.CH4_TX_Phase_Q.TabIndex = 20;
            this.CH4_TX_Phase_Q.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.TX_VM_CH4_POL_Q.AutoSize = true;
            this.TX_VM_CH4_POL_Q.Location = new Point(57, 323);
            this.TX_VM_CH4_POL_Q.Name = "TX_VM_CH4_POL_Q";
            this.TX_VM_CH4_POL_Q.Size = new Size(18, 17);
            this.TX_VM_CH4_POL_Q.TabIndex = 19;
            this.TX_VM_CH4_POL_Q.UseVisualStyleBackColor = true;
            this.TX_VM_CH4_POL_Q.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH3_TX_Phase_Q.Location = new Point(133, 241);
            this.CH3_TX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH3_TX_Phase_Q.Name = "CH3_TX_Phase_Q";
            this.CH3_TX_Phase_Q.Size = new Size(120, 22);
            this.CH3_TX_Phase_Q.TabIndex = 18;
            this.CH3_TX_Phase_Q.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.TX_VM_CH3_POL_Q.AutoSize = true;
            this.TX_VM_CH3_POL_Q.Location = new Point(57, 243);
            this.TX_VM_CH3_POL_Q.Name = "TX_VM_CH3_POL_Q";
            this.TX_VM_CH3_POL_Q.Size = new Size(18, 17);
            this.TX_VM_CH3_POL_Q.TabIndex = 17;
            this.TX_VM_CH3_POL_Q.UseVisualStyleBackColor = true;
            this.TX_VM_CH3_POL_Q.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH2_TX_Phase_Q.Location = new Point(133, 162);
            this.CH2_TX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH2_TX_Phase_Q.Name = "CH2_TX_Phase_Q";
            this.CH2_TX_Phase_Q.Size = new Size(120, 22);
            this.CH2_TX_Phase_Q.TabIndex = 16;
            this.CH2_TX_Phase_Q.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.TX_VM_CH2_POL_Q.AutoSize = true;
            this.TX_VM_CH2_POL_Q.Location = new Point(57, 164);
            this.TX_VM_CH2_POL_Q.Name = "TX_VM_CH2_POL_Q";
            this.TX_VM_CH2_POL_Q.Size = new Size(18, 17);
            this.TX_VM_CH2_POL_Q.TabIndex = 15;
            this.TX_VM_CH2_POL_Q.UseVisualStyleBackColor = true;
            this.TX_VM_CH2_POL_Q.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH1_TX_Phase_Q.Location = new Point(133, 83);
            this.CH1_TX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH1_TX_Phase_Q.Name = "CH1_TX_Phase_Q";
            this.CH1_TX_Phase_Q.Size = new Size(120, 22);
            this.CH1_TX_Phase_Q.TabIndex = 14;
            this.CH1_TX_Phase_Q.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.TX_VM_CH1_POL_Q.AutoSize = true;
            this.TX_VM_CH1_POL_Q.Location = new Point(57, 85);
            this.TX_VM_CH1_POL_Q.Name = "TX_VM_CH1_POL_Q";
            this.TX_VM_CH1_POL_Q.Size = new Size(18, 17);
            this.TX_VM_CH1_POL_Q.TabIndex = 13;
            this.TX_VM_CH1_POL_Q.UseVisualStyleBackColor = true;
            this.TX_VM_CH1_POL_Q.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.button5.Location = new Point(57, 376);
            this.button5.Margin = new Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new Size(160, 28);
            this.button5.TabIndex = 12;
            this.button5.Text = "Write Phase Values";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new EventHandler(this.WriteTXPhase_Click);
            this.CH4_TX_Phase_I.Location = new Point(133, 292);
            this.CH4_TX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH4_TX_Phase_I.Name = "CH4_TX_Phase_I";
            this.CH4_TX_Phase_I.Size = new Size(120, 22);
            this.CH4_TX_Phase_I.TabIndex = 11;
            this.CH4_TX_Phase_I.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.label43.AutoSize = true;
            this.label43.Location = new Point(7, 273);
            this.label43.Name = "label43";
            this.label43.Size = new Size(113, 17);
            this.label43.TabIndex = 10;
            this.label43.Text = "TX VM CH4 POL";
            this.TX_VM_CH4_POL_I.AutoSize = true;
            this.TX_VM_CH4_POL_I.Location = new Point(57, 297);
            this.TX_VM_CH4_POL_I.Name = "TX_VM_CH4_POL_I";
            this.TX_VM_CH4_POL_I.Size = new Size(18, 17);
            this.TX_VM_CH4_POL_I.TabIndex = 9;
            this.TX_VM_CH4_POL_I.UseVisualStyleBackColor = true;
            this.TX_VM_CH4_POL_I.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH3_TX_Phase_I.Location = new Point(133, 213);
            this.CH3_TX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH3_TX_Phase_I.Name = "CH3_TX_Phase_I";
            this.CH3_TX_Phase_I.Size = new Size(120, 22);
            this.CH3_TX_Phase_I.TabIndex = 8;
            this.CH3_TX_Phase_I.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.label44.AutoSize = true;
            this.label44.Location = new Point(7, 194);
            this.label44.Name = "label44";
            this.label44.Size = new Size(113, 17);
            this.label44.TabIndex = 7;
            this.label44.Text = "TX VM CH3 POL";
            this.TX_VM_CH3_POL_I.AutoSize = true;
            this.TX_VM_CH3_POL_I.Location = new Point(57, 218);
            this.TX_VM_CH3_POL_I.Name = "TX_VM_CH3_POL_I";
            this.TX_VM_CH3_POL_I.Size = new Size(18, 17);
            this.TX_VM_CH3_POL_I.TabIndex = 6;
            this.TX_VM_CH3_POL_I.UseVisualStyleBackColor = true;
            this.TX_VM_CH3_POL_I.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH2_TX_Phase_I.Location = new Point(133, 134);
            this.CH2_TX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH2_TX_Phase_I.Name = "CH2_TX_Phase_I";
            this.CH2_TX_Phase_I.Size = new Size(120, 22);
            this.CH2_TX_Phase_I.TabIndex = 5;
            this.CH2_TX_Phase_I.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.label45.AutoSize = true;
            this.label45.Location = new Point(7, 115);
            this.label45.Name = "label45";
            this.label45.Size = new Size(113, 17);
            this.label45.TabIndex = 4;
            this.label45.Text = "TX VM CH2 POL";
            this.TX_VM_CH2_POL_I.AutoSize = true;
            this.TX_VM_CH2_POL_I.Location = new Point(57, 139);
            this.TX_VM_CH2_POL_I.Name = "TX_VM_CH2_POL_I";
            this.TX_VM_CH2_POL_I.Size = new Size(18, 17);
            this.TX_VM_CH2_POL_I.TabIndex = 3;
            this.TX_VM_CH2_POL_I.UseVisualStyleBackColor = true;
            this.TX_VM_CH2_POL_I.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.CH1_TX_Phase_I.Location = new Point(133, 55);
            this.CH1_TX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH1_TX_Phase_I.Name = "CH1_TX_Phase_I";
            this.CH1_TX_Phase_I.Size = new Size(120, 22);
            this.CH1_TX_Phase_I.TabIndex = 2;
            this.CH1_TX_Phase_I.ValueChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.label46.AutoSize = true;
            this.label46.Location = new Point(7, 36);
            this.label46.Name = "label46";
            this.label46.Size = new Size(113, 17);
            this.label46.TabIndex = 1;
            this.label46.Text = "TX VM CH1 POL";
            this.TX_VM_CH1_POL_I.AutoSize = true;
            this.TX_VM_CH1_POL_I.Location = new Point(57, 60);
            this.TX_VM_CH1_POL_I.Name = "TX_VM_CH1_POL_I";
            this.TX_VM_CH1_POL_I.Size = new Size(18, 17);
            this.TX_VM_CH1_POL_I.TabIndex = 0;
            this.TX_VM_CH1_POL_I.UseVisualStyleBackColor = true;
            this.TX_VM_CH1_POL_I.CheckStateChanged += new EventHandler(this.TX_Phase_ValueChanged);
            this.groupBox1.Controls.Add((Control)this.label112);
            this.groupBox1.Controls.Add((Control)this.button2);
            this.groupBox1.Controls.Add((Control)this.label111);
            this.groupBox1.Controls.Add((Control)this.label18);
            this.groupBox1.Controls.Add((Control)this.label15);
            this.groupBox1.Controls.Add((Control)this.label22);
            this.groupBox1.Controls.Add((Control)this.label12);
            this.groupBox1.Controls.Add((Control)this.label21);
            this.groupBox1.Controls.Add((Control)this.label2);
            this.groupBox1.Controls.Add((Control)this.CH4_ATTN_TX);
            this.groupBox1.Controls.Add((Control)this.CH3_ATTN_TX);
            this.groupBox1.Controls.Add((Control)this.CH2_ATTN_TX);
            this.groupBox1.Controls.Add((Control)this.CH1_ATTN_TX);
            this.groupBox1.Controls.Add((Control)this.Gain4_Value);
            this.groupBox1.Controls.Add((Control)this.Gain1_Value);
            this.groupBox1.Controls.Add((Control)this.Gain3_Value);
            this.groupBox1.Controls.Add((Control)this.Gain2_Value);
            this.groupBox1.Location = new Point(4, 7);
            this.groupBox1.Margin = new Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4);
            this.groupBox1.Size = new Size(260, 412);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tx Gain Control";
            this.label112.AutoSize = true;
            this.label112.Location = new Point(130, 272);
            this.label112.Name = "label112";
            this.label112.Size = new Size(90, 17);
            this.label112.TabIndex = 23;
            this.label112.Text = "TX VGA CH4";
            this.button2.Location = new Point(50, 376);
            this.button2.Margin = new Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new Size(160, 28);
            this.button2.TabIndex = 12;
            this.button2.Text = "Write Gain Values";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.WriteTXGain_Click);
            this.label111.AutoSize = true;
            this.label111.Location = new Point(130, 194);
            this.label111.Name = "label111";
            this.label111.Size = new Size(90, 17);
            this.label111.TabIndex = 22;
            this.label111.Text = "TX VGA CH3";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(130, 114);
            this.label18.Name = "label18";
            this.label18.Size = new Size(90, 17);
            this.label18.TabIndex = 21;
            this.label18.Text = "TX VGA CH2";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(130, 36);
            this.label15.Name = "label15";
            this.label15.Size = new Size(90, 17);
            this.label15.TabIndex = 20;
            this.label15.Text = "TX VGA CH1";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(6, 195);
            this.label22.Name = "label22";
            this.label22.Size = new Size(98, 17);
            this.label22.TabIndex = 19;
            this.label22.Text = "CH3 TX ATTN";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(6, 273);
            this.label12.Name = "label12";
            this.label12.Size = new Size(98, 17);
            this.label12.TabIndex = 18;
            this.label12.Text = "CH4 TX ATTN";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(6, 114);
            this.label21.Name = "label21";
            this.label21.Size = new Size(98, 17);
            this.label21.TabIndex = 17;
            this.label21.Text = "CH2 TX ATTN";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new Size(98, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "CH1 TX ATTN";
            this.CH4_ATTN_TX.AutoSize = true;
            this.CH4_ATTN_TX.Location = new Point(40, 298);
            this.CH4_ATTN_TX.Name = "CH4_ATTN_TX";
            this.CH4_ATTN_TX.Size = new Size(18, 17);
            this.CH4_ATTN_TX.TabIndex = 4;
            this.CH4_ATTN_TX.UseVisualStyleBackColor = true;
            this.CH4_ATTN_TX.CheckStateChanged += new EventHandler(this.TXGain_ValueChanged);
            this.CH3_ATTN_TX.AutoSize = true;
            this.CH3_ATTN_TX.Location = new Point(40, 220);
            this.CH3_ATTN_TX.Name = "CH3_ATTN_TX";
            this.CH3_ATTN_TX.Size = new Size(18, 17);
            this.CH3_ATTN_TX.TabIndex = 3;
            this.CH3_ATTN_TX.UseVisualStyleBackColor = true;
            this.CH3_ATTN_TX.CheckStateChanged += new EventHandler(this.TXGain_ValueChanged);
            this.CH2_ATTN_TX.AutoSize = true;
            this.CH2_ATTN_TX.Location = new Point(40, 141);
            this.CH2_ATTN_TX.Name = "CH2_ATTN_TX";
            this.CH2_ATTN_TX.Size = new Size(18, 17);
            this.CH2_ATTN_TX.TabIndex = 2;
            this.CH2_ATTN_TX.UseVisualStyleBackColor = true;
            this.CH2_ATTN_TX.CheckStateChanged += new EventHandler(this.TXGain_ValueChanged);
            this.CH1_ATTN_TX.AutoSize = true;
            this.CH1_ATTN_TX.Location = new Point(40, 61);
            this.CH1_ATTN_TX.Name = "CH1_ATTN_TX";
            this.CH1_ATTN_TX.Size = new Size(18, 17);
            this.CH1_ATTN_TX.TabIndex = 1;
            this.CH1_ATTN_TX.UseVisualStyleBackColor = true;
            this.CH1_ATTN_TX.CheckStateChanged += new EventHandler(this.TXGain_ValueChanged);
            this.Gain4_Value.Location = new Point(132, 293);
            this.Gain4_Value.Margin = new Padding(4);
            this.Gain4_Value.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.Gain4_Value.Name = "Gain4_Value";
            this.Gain4_Value.Size = new Size(120, 22);
            this.Gain4_Value.TabIndex = 15;
            this.Gain4_Value.ValueChanged += new EventHandler(this.TXGain_ValueChanged);
            this.Gain1_Value.Location = new Point(132, 54);
            this.Gain1_Value.Margin = new Padding(4);
            this.Gain1_Value.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.Gain1_Value.Name = "Gain1_Value";
            this.Gain1_Value.Size = new Size(120, 22);
            this.Gain1_Value.TabIndex = 10;
            this.Gain1_Value.ValueChanged += new EventHandler(this.TXGain_ValueChanged);
            this.Gain3_Value.Location = new Point(132, 215);
            this.Gain3_Value.Margin = new Padding(4);
            this.Gain3_Value.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.Gain3_Value.Name = "Gain3_Value";
            this.Gain3_Value.Size = new Size(120, 22);
            this.Gain3_Value.TabIndex = 14;
            this.Gain3_Value.ValueChanged += new EventHandler(this.TXGain_ValueChanged);
            this.Gain2_Value.Location = new Point(132, 135);
            this.Gain2_Value.Margin = new Padding(4);
            this.Gain2_Value.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.Gain2_Value.Name = "Gain2_Value";
            this.Gain2_Value.Size = new Size(120, 22);
            this.Gain2_Value.TabIndex = 13;
            this.Gain2_Value.ValueChanged += new EventHandler(this.TXGain_ValueChanged);
            this.groupBox21.Controls.Add((Control)this.label141);
            this.groupBox21.Controls.Add((Control)this.TX_CHX_RAM_FETCH);
            this.groupBox21.Controls.Add((Control)this.TX_CHX_RAM_INDEX);
            this.groupBox21.Controls.Add((Control)this.label142);
            this.groupBox21.Controls.Add((Control)this.label83);
            this.groupBox21.Controls.Add((Control)this.TX_CH4_RAM_FETCH);
            this.groupBox21.Controls.Add((Control)this.label82);
            this.groupBox21.Controls.Add((Control)this.TX_CH3_RAM_FETCH);
            this.groupBox21.Controls.Add((Control)this.label81);
            this.groupBox21.Controls.Add((Control)this.TX_CH2_RAM_FETCH);
            this.groupBox21.Controls.Add((Control)this.label80);
            this.groupBox21.Controls.Add((Control)this.TX_CH1_RAM_FETCH);
            this.groupBox21.Controls.Add((Control)this.button17);
            this.groupBox21.Controls.Add((Control)this.TX_CH4_RAM_INDEX);
            this.groupBox21.Controls.Add((Control)this.label72);
            this.groupBox21.Controls.Add((Control)this.TX_CH3_RAM_INDEX);
            this.groupBox21.Controls.Add((Control)this.label73);
            this.groupBox21.Controls.Add((Control)this.TX_CH2_RAM_INDEX);
            this.groupBox21.Controls.Add((Control)this.label74);
            this.groupBox21.Controls.Add((Control)this.TX_CH1_RAM_INDEX);
            this.groupBox21.Controls.Add((Control)this.label75);
            this.groupBox21.Location = new Point(539, 6);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new Size(341, 412);
            this.groupBox21.TabIndex = 5;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Memory Index";
            this.label141.AutoSize = true;
            this.label141.Location = new Point(244, 84);
            this.label141.Name = "label141";
            this.label141.Size = new Size(54, 17);
            this.label141.TabIndex = 61;
            this.label141.Text = "SPI Ctrl";
            this.TX_CHX_RAM_FETCH.AutoSize = true;
            this.TX_CHX_RAM_FETCH.Location = new Point(304, 84);
            this.TX_CHX_RAM_FETCH.Name = "TX_CHX_RAM_FETCH";
            this.TX_CHX_RAM_FETCH.Size = new Size(18, 17);
            this.TX_CHX_RAM_FETCH.TabIndex = 60;
            this.TX_CHX_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_CHX_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.TX_CHX_RAM_INDEX.Location = new Point(202, 56);
            this.TX_CHX_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.TX_CHX_RAM_INDEX.Name = "TX_CHX_RAM_INDEX";
            this.TX_CHX_RAM_INDEX.Size = new Size(120, 22);
            this.TX_CHX_RAM_INDEX.TabIndex = 59;
            this.TX_CHX_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label142.Location = new Point(199, 37);
            this.label142.Name = "label142";
            this.label142.Size = new Size(136, 17);
            this.label142.TabIndex = 58;
            this.label142.Text = "TX CHX RAM INDEX";
            this.label83.AutoSize = true;
            this.label83.Location = new Point(61, 323);
            this.label83.Name = "label83";
            this.label83.Size = new Size(54, 17);
            this.label83.TabIndex = 57;
            this.label83.Text = "SPI Ctrl";
            this.TX_CH4_RAM_FETCH.AutoSize = true;
            this.TX_CH4_RAM_FETCH.Location = new Point(121, 323);
            this.TX_CH4_RAM_FETCH.Name = "TX_CH4_RAM_FETCH";
            this.TX_CH4_RAM_FETCH.Size = new Size(18, 17);
            this.TX_CH4_RAM_FETCH.TabIndex = 56;
            this.TX_CH4_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_CH4_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label82.AutoSize = true;
            this.label82.Location = new Point(61, 243);
            this.label82.Name = "label82";
            this.label82.Size = new Size(54, 17);
            this.label82.TabIndex = 55;
            this.label82.Text = "SPI Ctrl";
            this.TX_CH3_RAM_FETCH.AutoSize = true;
            this.TX_CH3_RAM_FETCH.Location = new Point(121, 243);
            this.TX_CH3_RAM_FETCH.Name = "TX_CH3_RAM_FETCH";
            this.TX_CH3_RAM_FETCH.Size = new Size(18, 17);
            this.TX_CH3_RAM_FETCH.TabIndex = 54;
            this.TX_CH3_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_CH3_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label81.AutoSize = true;
            this.label81.Location = new Point(61, 163);
            this.label81.Name = "label81";
            this.label81.Size = new Size(54, 17);
            this.label81.TabIndex = 53;
            this.label81.Text = "SPI Ctrl";
            this.TX_CH2_RAM_FETCH.AutoSize = true;
            this.TX_CH2_RAM_FETCH.Location = new Point(121, 163);
            this.TX_CH2_RAM_FETCH.Name = "TX_CH2_RAM_FETCH";
            this.TX_CH2_RAM_FETCH.Size = new Size(18, 17);
            this.TX_CH2_RAM_FETCH.TabIndex = 52;
            this.TX_CH2_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_CH2_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label80.AutoSize = true;
            this.label80.Location = new Point(61, 83);
            this.label80.Name = "label80";
            this.label80.Size = new Size(54, 17);
            this.label80.TabIndex = 51;
            this.label80.Text = "SPI Ctrl";
            this.TX_CH1_RAM_FETCH.AutoSize = true;
            this.TX_CH1_RAM_FETCH.Location = new Point(121, 83);
            this.TX_CH1_RAM_FETCH.Name = "TX_CH1_RAM_FETCH";
            this.TX_CH1_RAM_FETCH.Size = new Size(18, 17);
            this.TX_CH1_RAM_FETCH.TabIndex = 50;
            this.TX_CH1_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_CH1_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.button17.Location = new Point(7, 376);
            this.button17.Margin = new Padding(4);
            this.button17.Name = "button17";
            this.button17.Size = new Size(157, 28);
            this.button17.TabIndex = 49;
            this.button17.Text = "Write Memory Values";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new EventHandler(this.TX_Mem_Click);
            this.TX_CH4_RAM_INDEX.Location = new Point(19, 295);
            this.TX_CH4_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.TX_CH4_RAM_INDEX.Name = "TX_CH4_RAM_INDEX";
            this.TX_CH4_RAM_INDEX.Size = new Size(120, 22);
            this.TX_CH4_RAM_INDEX.TabIndex = 48;
            this.TX_CH4_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label72.AutoSize = true;
            this.label72.Location = new Point(16, 273);
            this.label72.Name = "label72";
            this.label72.Size = new Size(136, 17);
            this.label72.TabIndex = 47;
            this.label72.Text = "TX CH4 RAM INDEX";
            this.TX_CH3_RAM_INDEX.Location = new Point(19, 215);
            this.TX_CH3_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.TX_CH3_RAM_INDEX.Name = "TX_CH3_RAM_INDEX";
            this.TX_CH3_RAM_INDEX.Size = new Size(120, 22);
            this.TX_CH3_RAM_INDEX.TabIndex = 46;
            this.TX_CH3_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label73.AutoSize = true;
            this.label73.Location = new Point(16, 195);
            this.label73.Name = "label73";
            this.label73.Size = new Size(136, 17);
            this.label73.TabIndex = 45;
            this.label73.Text = "TX CH3 RAM INDEX";
            this.TX_CH2_RAM_INDEX.Location = new Point(19, 135);
            this.TX_CH2_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.TX_CH2_RAM_INDEX.Name = "TX_CH2_RAM_INDEX";
            this.TX_CH2_RAM_INDEX.RightToLeft = RightToLeft.No;
            this.TX_CH2_RAM_INDEX.Size = new Size(120, 22);
            this.TX_CH2_RAM_INDEX.TabIndex = 44;
            this.TX_CH2_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label74.AutoSize = true;
            this.label74.Location = new Point(16, 115);
            this.label74.Name = "label74";
            this.label74.Size = new Size(136, 17);
            this.label74.TabIndex = 43;
            this.label74.Text = "TX CH2 RAM INDEX";
            this.TX_CH1_RAM_INDEX.Location = new Point(19, 55);
            this.TX_CH1_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.TX_CH1_RAM_INDEX.Name = "TX_CH1_RAM_INDEX";
            this.TX_CH1_RAM_INDEX.Size = new Size(120, 22);
            this.TX_CH1_RAM_INDEX.TabIndex = 42;
            this.TX_CH1_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label75.AutoSize = true;
            this.label75.Location = new Point(16, 36);
            this.label75.Name = "label75";
            this.label75.Size = new Size(136, 17);
            this.label75.TabIndex = 41;
            this.label75.Text = "TX CH1 RAM INDEX";
            this.groupBox22.Controls.Add((Control)this.label65);
            this.groupBox22.Controls.Add((Control)this.TX_DRV_BIAS);
            this.groupBox22.Controls.Add((Control)this.button22);
            this.groupBox22.Controls.Add((Control)this.label62);
            this.groupBox22.Controls.Add((Control)this.label63);
            this.groupBox22.Controls.Add((Control)this.TX_VM_BIAS3);
            this.groupBox22.Controls.Add((Control)this.TX_VGA_BIAS3);
            this.groupBox22.Controls.Add((Control)this.label67);
            this.groupBox22.Location = new Point(886, 6);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new Size(241, 196);
            this.groupBox22.TabIndex = 4;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Bias Current";
            this.label65.AutoSize = true;
            this.label65.Location = new Point(1, 115);
            this.label65.Name = "label65";
            this.label65.Size = new Size(93, 17);
            this.label65.TabIndex = 37;
            this.label65.Text = "TX DRV BIAS";
            this.TX_DRV_BIAS.Location = new Point(100, 113);
            this.TX_DRV_BIAS.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.TX_DRV_BIAS.Name = "TX_DRV_BIAS";
            this.TX_DRV_BIAS.Size = new Size(120, 22);
            this.TX_DRV_BIAS.TabIndex = 36;
            this.TX_DRV_BIAS.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.button22.Location = new Point(48, 161);
            this.button22.Margin = new Padding(4);
            this.button22.Name = "button22";
            this.button22.Size = new Size(146, 28);
            this.button22.TabIndex = 35;
            this.button22.Text = "Write Bias Values";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new EventHandler(this.Bias_Current_Click);
            this.label62.AutoSize = true;
            this.label62.Location = new Point(10, 87);
            this.label62.Name = "label62";
            this.label62.Size = new Size(84, 17);
            this.label62.TabIndex = 34;
            this.label62.Text = "TX VM BIAS";
            this.label63.AutoSize = true;
            this.label63.Location = new Point(1, 59);
            this.label63.Name = "label63";
            this.label63.Size = new Size(93, 17);
            this.label63.TabIndex = 33;
            this.label63.Text = "TX VGA BIAS";
            this.TX_VM_BIAS3.Location = new Point(100, 85);
            this.TX_VM_BIAS3.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.TX_VM_BIAS3.Name = "TX_VM_BIAS3";
            this.TX_VM_BIAS3.Size = new Size(120, 22);
            this.TX_VM_BIAS3.TabIndex = 31;
            this.TX_VM_BIAS3.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.TX_VGA_BIAS3.Location = new Point(100, 57);
            this.TX_VGA_BIAS3.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.TX_VGA_BIAS3.Name = "TX_VGA_BIAS3";
            this.TX_VGA_BIAS3.Size = new Size(120, 22);
            this.TX_VGA_BIAS3.TabIndex = 32;
            this.TX_VGA_BIAS3.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.label67.AutoSize = true;
            this.label67.Location = new Point(10, 36);
            this.label67.Name = "label67";
            this.label67.Size = new Size(98, 17);
            this.label67.TabIndex = 30;
            this.label67.Text = "Bias Current 3";
            this.Rx_Control.Controls.Add((Control)this.RX_Init_Label);
            this.Rx_Control.Controls.Add((Control)this.RX_Init_Indicator);
            this.Rx_Control.Controls.Add((Control)this.RX_Init_Button);
            this.Rx_Control.Controls.Add((Control)this.RX1_Attn_Pic);
            this.Rx_Control.Controls.Add((Control)this.RX4_Attn_Pic);
            this.Rx_Control.Controls.Add((Control)this.RX3_Attn_Pic);
            this.Rx_Control.Controls.Add((Control)this.RX2_Attn_Pic);
            this.Rx_Control.Controls.Add((Control)this.label131);
            this.Rx_Control.Controls.Add((Control)this.label132);
            this.Rx_Control.Controls.Add((Control)this.label134);
            this.Rx_Control.Controls.Add((Control)this.panel7);
            this.Rx_Control.Controls.Add((Control)this.panel8);
            this.Rx_Control.Controls.Add((Control)this.panel9);
            this.Rx_Control.Controls.Add((Control)this.panel10);
            this.Rx_Control.Controls.Add((Control)this.button36);
            this.Rx_Control.Controls.Add((Control)this.pictureBox6);
            this.Rx_Control.Location = new Point(4, 25);
            this.Rx_Control.Name = "Rx_Control";
            this.Rx_Control.Size = new Size(1185, 446);
            this.Rx_Control.TabIndex = 15;
            this.Rx_Control.Text = "Rx Control";
            this.Rx_Control.ToolTipText = "Set Rx gain a phase directly by inputting a gain index and an angle";
            this.Rx_Control.UseVisualStyleBackColor = true;
            this.RX_Init_Label.AutoSize = true;
            this.RX_Init_Label.Location = new Point(772, 379);
            this.RX_Init_Label.Name = "RX_Init_Label";
            this.RX_Init_Label.Size = new Size(93, 17);
            this.RX_Init_Label.TabIndex = 36;
            this.RX_Init_Label.Text = "Not Initalised ";
            this.RX_Init_Indicator.BackColor = Color.Red;
            this.RX_Init_Indicator.Location = new Point(738, 373);
            this.RX_Init_Indicator.Name = "RX_Init_Indicator";
            this.RX_Init_Indicator.Size = new Size(28, 28);
            this.RX_Init_Indicator.TabIndex = 35;
            this.RX_Init_Button.Location = new Point(632, 373);
            this.RX_Init_Button.Name = "RX_Init_Button";
            this.RX_Init_Button.Size = new Size(100, 28);
            this.RX_Init_Button.TabIndex = 34;
            this.RX_Init_Button.Text = "Rx Initalise";
            this.RX_Init_Button.UseVisualStyleBackColor = true;
            this.RX_Init_Button.Click += new EventHandler(this.RX_Init_Button_Click);
            this.RX1_Attn_Pic.ErrorImage = (Image)null;
            this.RX1_Attn_Pic.Image = (Image)componentResourceManager.GetObject("RX1_Attn_Pic.Image");
            this.RX1_Attn_Pic.InitialImage = (Image)null;
            this.RX1_Attn_Pic.Location = new Point(251, 215);
            this.RX1_Attn_Pic.Name = "RX1_Attn_Pic";
            this.RX1_Attn_Pic.Size = new Size(43, 47);
            this.RX1_Attn_Pic.TabIndex = 33;
            this.RX1_Attn_Pic.TabStop = false;
            this.RX4_Attn_Pic.ErrorImage = (Image)null;
            this.RX4_Attn_Pic.Image = (Image)componentResourceManager.GetObject("RX4_Attn_Pic.Image");
            this.RX4_Attn_Pic.InitialImage = (Image)null;
            this.RX4_Attn_Pic.Location = new Point(310, 215);
            this.RX4_Attn_Pic.Name = "RX4_Attn_Pic";
            this.RX4_Attn_Pic.Size = new Size(33, 48);
            this.RX4_Attn_Pic.TabIndex = 32;
            this.RX4_Attn_Pic.TabStop = false;
            this.RX3_Attn_Pic.ErrorImage = (Image)null;
            this.RX3_Attn_Pic.Image = (Image)componentResourceManager.GetObject("RX3_Attn_Pic.Image");
            this.RX3_Attn_Pic.InitialImage = (Image)null;
            this.RX3_Attn_Pic.Location = new Point(311, 151);
            this.RX3_Attn_Pic.Name = "RX3_Attn_Pic";
            this.RX3_Attn_Pic.Size = new Size(33, 46);
            this.RX3_Attn_Pic.TabIndex = 31;
            this.RX3_Attn_Pic.TabStop = false;
            this.RX2_Attn_Pic.ErrorImage = (Image)null;
            this.RX2_Attn_Pic.Image = (Image)componentResourceManager.GetObject("RX2_Attn_Pic.Image");
            this.RX2_Attn_Pic.InitialImage = (Image)null;
            this.RX2_Attn_Pic.Location = new Point(249, 152);
            this.RX2_Attn_Pic.Name = "RX2_Attn_Pic";
            this.RX2_Attn_Pic.Size = new Size(43, 47);
            this.RX2_Attn_Pic.TabIndex = 30;
            this.RX2_Attn_Pic.TabStop = false;
            this.label131.AutoSize = true;
            this.label131.Location = new Point(902, 21);
            this.label131.Name = "label131";
            this.label131.Size = new Size(75, 17);
            this.label131.TabIndex = 29;
            this.label131.Text = "Gain Index";
            this.label132.AutoSize = true;
            this.label132.Location = new Point(639, 21);
            this.label132.Name = "label132";
            this.label132.Size = new Size(80, 17);
            this.label132.TabIndex = 28;
            this.label132.Text = "Attenuation";
            this.label134.AutoSize = true;
            this.label134.Location = new Point(743, 21);
            this.label134.Name = "label134";
            this.label134.Size = new Size(112, 17);
            this.label134.TabIndex = 27;
            this.label134.Text = "Phase Angle ( °)";
            this.panel7.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.panel7.Controls.Add((Control)this.RX_CH3_Gain);
            this.panel7.Controls.Add((Control)this.RX_CH3_Phase_Angle);
            this.panel7.Controls.Add((Control)this.button32);
            this.panel7.Controls.Add((Control)this.RX3_Attn_CheckBox);
            this.panel7.Location = new Point(618, 209);
            this.panel7.Name = "panel7";
            this.panel7.Size = new Size(530, 76);
            this.panel7.TabIndex = 25;
            this.RX_CH3_Gain.FormattingEnabled = true;
            this.RX_CH3_Gain.Location = new Point(264, 26);
            this.RX_CH3_Gain.Name = "RX_CH3_Gain";
            this.RX_CH3_Gain.Size = new Size(103, 24);
            this.RX_CH3_Gain.TabIndex = 22;
            this.RX_CH3_Gain.Text = "0";
            this.RX_CH3_Phase_Angle.FormattingEnabled = true;
            this.RX_CH3_Phase_Angle.Location = new Point(128, 26);
            this.RX_CH3_Phase_Angle.Name = "RX_CH3_Phase_Angle";
            this.RX_CH3_Phase_Angle.Size = new Size(103, 24);
            this.RX_CH3_Phase_Angle.TabIndex = 13;
            this.RX_CH3_Phase_Angle.Text = "0";
            this.button32.Location = new Point(413, 25);
            this.button32.Name = "button32";
            this.button32.Size = new Size(100, 25);
            this.button32.TabIndex = 14;
            this.button32.Text = "RX3 Load";
            this.button32.UseVisualStyleBackColor = true;
            this.button32.Click += new EventHandler(this.RX_CH3_block_button_Click);
            this.RX3_Attn_CheckBox.AutoSize = true;
            this.RX3_Attn_CheckBox.Location = new Point(55, 30);
            this.RX3_Attn_CheckBox.Name = "RX3_Attn_CheckBox";
            this.RX3_Attn_CheckBox.Size = new Size(18, 17);
            this.RX3_Attn_CheckBox.TabIndex = 21;
            this.RX3_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.RX3_Attn_CheckBox.CheckedChanged += new EventHandler(this.RX3_Attn_CheckBox_CheckedChanged);
            this.panel8.BackColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192);
            this.panel8.Controls.Add((Control)this.RX_CH4_Gain);
            this.panel8.Controls.Add((Control)this.RX_CH4_Phase_Angle);
            this.panel8.Controls.Add((Control)this.button31);
            this.panel8.Controls.Add((Control)this.RX4_Attn_CheckBox);
            this.panel8.Location = new Point(618, 291);
            this.panel8.Name = "panel8";
            this.panel8.Size = new Size(530, 76);
            this.panel8.TabIndex = 26;
            this.RX_CH4_Gain.FormattingEnabled = true;
            this.RX_CH4_Gain.Location = new Point(264, 26);
            this.RX_CH4_Gain.Name = "RX_CH4_Gain";
            this.RX_CH4_Gain.Size = new Size(103, 24);
            this.RX_CH4_Gain.TabIndex = 23;
            this.RX_CH4_Gain.Text = "0";
            this.RX_CH4_Phase_Angle.FormattingEnabled = true;
            this.RX_CH4_Phase_Angle.Location = new Point(128, 26);
            this.RX_CH4_Phase_Angle.Name = "RX_CH4_Phase_Angle";
            this.RX_CH4_Phase_Angle.Size = new Size(103, 24);
            this.RX_CH4_Phase_Angle.TabIndex = 15;
            this.RX_CH4_Phase_Angle.Text = "0";
            this.button31.Location = new Point(413, 25);
            this.button31.Name = "button31";
            this.button31.Size = new Size(100, 25);
            this.button31.TabIndex = 16;
            this.button31.Text = "RX4 Load";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new EventHandler(this.RX_CH4_block_button_Click);
            this.RX4_Attn_CheckBox.AutoSize = true;
            this.RX4_Attn_CheckBox.Location = new Point(55, 30);
            this.RX4_Attn_CheckBox.Name = "RX4_Attn_CheckBox";
            this.RX4_Attn_CheckBox.Size = new Size(18, 17);
            this.RX4_Attn_CheckBox.TabIndex = 22;
            this.RX4_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.RX4_Attn_CheckBox.CheckedChanged += new EventHandler(this.RX4_Attn_CheckBox_CheckedChanged);
            this.panel9.BackColor = Color.FromArgb(192, (int)byte.MaxValue, 192);
            this.panel9.Controls.Add((Control)this.RX_CH2_Gain);
            this.panel9.Controls.Add((Control)this.RX_CH2_Phase_Angle);
            this.panel9.Controls.Add((Control)this.button33);
            this.panel9.Controls.Add((Control)this.RX2_Attn_CheckBox);
            this.panel9.Location = new Point(618, (int)sbyte.MaxValue);
            this.panel9.Name = "panel9";
            this.panel9.Size = new Size(530, 76);
            this.panel9.TabIndex = 24;
            this.RX_CH2_Gain.FormattingEnabled = true;
            this.RX_CH2_Gain.Location = new Point(264, 26);
            this.RX_CH2_Gain.Name = "RX_CH2_Gain";
            this.RX_CH2_Gain.Size = new Size(103, 24);
            this.RX_CH2_Gain.TabIndex = 21;
            this.RX_CH2_Gain.Text = "0";
            this.RX_CH2_Phase_Angle.FormattingEnabled = true;
            this.RX_CH2_Phase_Angle.Location = new Point(128, 26);
            this.RX_CH2_Phase_Angle.Name = "RX_CH2_Phase_Angle";
            this.RX_CH2_Phase_Angle.Size = new Size(103, 24);
            this.RX_CH2_Phase_Angle.TabIndex = 11;
            this.RX_CH2_Phase_Angle.Text = "0";
            this.button33.Location = new Point(413, 25);
            this.button33.Name = "button33";
            this.button33.Size = new Size(100, 25);
            this.button33.TabIndex = 12;
            this.button33.Text = "RX2 Load";
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new EventHandler(this.RX_CH2_block_button_Click);
            this.RX2_Attn_CheckBox.AutoSize = true;
            this.RX2_Attn_CheckBox.Location = new Point(55, 30);
            this.RX2_Attn_CheckBox.Name = "RX2_Attn_CheckBox";
            this.RX2_Attn_CheckBox.Size = new Size(18, 17);
            this.RX2_Attn_CheckBox.TabIndex = 20;
            this.RX2_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.RX2_Attn_CheckBox.CheckedChanged += new EventHandler(this.RX2_Attn_CheckBox_CheckedChanged);
            this.panel10.BackColor = Color.MistyRose;
            this.panel10.Controls.Add((Control)this.RX_CH1_Gain);
            this.panel10.Controls.Add((Control)this.RX1_Attn_CheckBox);
            this.panel10.Controls.Add((Control)this.RX_CH1_Phase_Angle);
            this.panel10.Controls.Add((Control)this.button34);
            this.panel10.Location = new Point(618, 45);
            this.panel10.Name = "panel10";
            this.panel10.Size = new Size(530, 76);
            this.panel10.TabIndex = 23;
            this.RX_CH1_Gain.FormattingEnabled = true;
            this.RX_CH1_Gain.Location = new Point(264, 26);
            this.RX_CH1_Gain.Name = "RX_CH1_Gain";
            this.RX_CH1_Gain.Size = new Size(103, 24);
            this.RX_CH1_Gain.TabIndex = 20;
            this.RX_CH1_Gain.Text = "0";
            this.RX1_Attn_CheckBox.AutoSize = true;
            this.RX1_Attn_CheckBox.Location = new Point(55, 30);
            this.RX1_Attn_CheckBox.Name = "RX1_Attn_CheckBox";
            this.RX1_Attn_CheckBox.Size = new Size(18, 17);
            this.RX1_Attn_CheckBox.TabIndex = 19;
            this.RX1_Attn_CheckBox.UseVisualStyleBackColor = true;
            this.RX1_Attn_CheckBox.CheckedChanged += new EventHandler(this.RX1_Attn_CheckBox_CheckedChanged);
            this.RX_CH1_Phase_Angle.FormattingEnabled = true;
            this.RX_CH1_Phase_Angle.Location = new Point(128, 26);
            this.RX_CH1_Phase_Angle.Name = "RX_CH1_Phase_Angle";
            this.RX_CH1_Phase_Angle.Size = new Size(103, 24);
            this.RX_CH1_Phase_Angle.TabIndex = 9;
            this.RX_CH1_Phase_Angle.Text = "0";
            this.button34.Location = new Point(413, 25);
            this.button34.Name = "button34";
            this.button34.Size = new Size(100, 25);
            this.button34.TabIndex = 10;
            this.button34.Text = "RX1 Load";
            this.button34.UseVisualStyleBackColor = true;
            this.button34.Click += new EventHandler(this.RX_CH1_block_button_Click);
            this.button36.Location = new Point(1031, 373);
            this.button36.Name = "button36";
            this.button36.Size = new Size(100, 28);
            this.button36.TabIndex = 18;
            this.button36.Text = "Load All";
            this.button36.UseVisualStyleBackColor = true;
            this.button36.Click += new EventHandler(this.RX_Write_All_Button);
            this.pictureBox6.Image = (Image)componentResourceManager.GetObject("pictureBox6.Image");
            this.pictureBox6.Location = new Point(20, 21);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new Size(560, 398);
            this.pictureBox6.TabIndex = 17;
            this.pictureBox6.TabStop = false;
            this.RXRegisters.Controls.Add((Control)this.groupBox5);
            this.RXRegisters.Controls.Add((Control)this.groupBox3);
            this.RXRegisters.Controls.Add((Control)this.groupBox20);
            this.RXRegisters.Controls.Add((Control)this.groupBox19);
            this.RXRegisters.Location = new Point(4, 25);
            this.RXRegisters.Name = "RXRegisters";
            this.RXRegisters.Padding = new Padding(3);
            this.RXRegisters.Size = new Size(1185, 446);
            this.RXRegisters.TabIndex = 8;
            this.RXRegisters.Text = "RX Registers";
            this.RXRegisters.ToolTipText = "Adjust Rx setting at a register level";
            this.RXRegisters.UseVisualStyleBackColor = true;
            this.groupBox5.Controls.Add((Control)this.label121);
            this.groupBox5.Controls.Add((Control)this.label120);
            this.groupBox5.Controls.Add((Control)this.label119);
            this.groupBox5.Controls.Add((Control)this.label118);
            this.groupBox5.Controls.Add((Control)this.label34);
            this.groupBox5.Controls.Add((Control)this.label33);
            this.groupBox5.Controls.Add((Control)this.label32);
            this.groupBox5.Controls.Add((Control)this.label31);
            this.groupBox5.Controls.Add((Control)this.label30);
            this.groupBox5.Controls.Add((Control)this.label29);
            this.groupBox5.Controls.Add((Control)this.label28);
            this.groupBox5.Controls.Add((Control)this.label27);
            this.groupBox5.Controls.Add((Control)this.CH4_RX_Phase_Q);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH4_POL_Q);
            this.groupBox5.Controls.Add((Control)this.CH3_RX_Phase_Q);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH3_POL_Q);
            this.groupBox5.Controls.Add((Control)this.CH2_RX_Phase_Q);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH2_POL_Q);
            this.groupBox5.Controls.Add((Control)this.CH1_RX_Phase_Q);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH1_POL_Q);
            this.groupBox5.Controls.Add((Control)this.button4);
            this.groupBox5.Controls.Add((Control)this.CH4_RX_Phase_I);
            this.groupBox5.Controls.Add((Control)this.label23);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH4_POL_I);
            this.groupBox5.Controls.Add((Control)this.CH3_RX_Phase_I);
            this.groupBox5.Controls.Add((Control)this.label24);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH3_POL_I);
            this.groupBox5.Controls.Add((Control)this.CH2_RX_Phase_I);
            this.groupBox5.Controls.Add((Control)this.label25);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH2_POL_I);
            this.groupBox5.Controls.Add((Control)this.CH1_RX_Phase_I);
            this.groupBox5.Controls.Add((Control)this.label26);
            this.groupBox5.Controls.Add((Control)this.RX_VM_CH1_POL_I);
            this.groupBox5.Location = new Point(272, 7);
            this.groupBox5.Margin = new Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new Padding(4);
            this.groupBox5.Size = new Size(260, 412);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rx Phase Control";
            this.label121.AutoSize = true;
            this.label121.Location = new Point(130, 273);
            this.label121.Name = "label121";
            this.label121.Size = new Size(119, 17);
            this.label121.TabIndex = 32;
            this.label121.Text = "RX VM CH4 GAIN";
            this.label120.AutoSize = true;
            this.label120.Location = new Point(130, 194);
            this.label120.Name = "label120";
            this.label120.Size = new Size(119, 17);
            this.label120.TabIndex = 31;
            this.label120.Text = "RX VM CH3 GAIN";
            this.label119.AutoSize = true;
            this.label119.Location = new Point(130, 115);
            this.label119.Name = "label119";
            this.label119.Size = new Size(119, 17);
            this.label119.TabIndex = 30;
            this.label119.Text = "RX VM CH2 GAIN";
            this.label118.AutoSize = true;
            this.label118.Location = new Point(130, 36);
            this.label118.Name = "label118";
            this.label118.Size = new Size(119, 17);
            this.label118.TabIndex = 29;
            this.label118.Text = "RX VM CH1 GAIN";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(26, 322);
            this.label34.Name = "label34";
            this.label34.Size = new Size(19, 17);
            this.label34.TabIndex = 28;
            this.label34.Text = "Q";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(26, 240);
            this.label33.Name = "label33";
            this.label33.Size = new Size(19, 17);
            this.label33.TabIndex = 27;
            this.label33.Text = "Q";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(26, 163);
            this.label32.Name = "label32";
            this.label32.Size = new Size(19, 17);
            this.label32.TabIndex = 26;
            this.label32.Text = "Q";
            this.label31.AutoSize = true;
            this.label31.Location = new Point(26, 85);
            this.label31.Name = "label31";
            this.label31.Size = new Size(19, 17);
            this.label31.TabIndex = 25;
            this.label31.Text = "Q";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(26, 296);
            this.label30.Name = "label30";
            this.label30.Size = new Size(11, 17);
            this.label30.TabIndex = 24;
            this.label30.Text = "I";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(26, 215);
            this.label29.Name = "label29";
            this.label29.Size = new Size(11, 17);
            this.label29.TabIndex = 23;
            this.label29.Text = "I";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(26, 138);
            this.label28.Name = "label28";
            this.label28.Size = new Size(11, 17);
            this.label28.TabIndex = 22;
            this.label28.Text = "I";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(26, 60);
            this.label27.Name = "label27";
            this.label27.Size = new Size(11, 17);
            this.label27.TabIndex = 21;
            this.label27.Text = "I";
            this.CH4_RX_Phase_Q.Location = new Point(133, 320);
            this.CH4_RX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH4_RX_Phase_Q.Name = "CH4_RX_Phase_Q";
            this.CH4_RX_Phase_Q.Size = new Size(120, 22);
            this.CH4_RX_Phase_Q.TabIndex = 20;
            this.CH4_RX_Phase_Q.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.RX_VM_CH4_POL_Q.AutoSize = true;
            this.RX_VM_CH4_POL_Q.Location = new Point(57, 323);
            this.RX_VM_CH4_POL_Q.Name = "RX_VM_CH4_POL_Q";
            this.RX_VM_CH4_POL_Q.Size = new Size(18, 17);
            this.RX_VM_CH4_POL_Q.TabIndex = 19;
            this.RX_VM_CH4_POL_Q.UseVisualStyleBackColor = true;
            this.RX_VM_CH4_POL_Q.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH3_RX_Phase_Q.Location = new Point(133, 241);
            this.CH3_RX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH3_RX_Phase_Q.Name = "CH3_RX_Phase_Q";
            this.CH3_RX_Phase_Q.Size = new Size(120, 22);
            this.CH3_RX_Phase_Q.TabIndex = 18;
            this.CH3_RX_Phase_Q.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.RX_VM_CH3_POL_Q.AutoSize = true;
            this.RX_VM_CH3_POL_Q.Location = new Point(57, 243);
            this.RX_VM_CH3_POL_Q.Name = "RX_VM_CH3_POL_Q";
            this.RX_VM_CH3_POL_Q.Size = new Size(18, 17);
            this.RX_VM_CH3_POL_Q.TabIndex = 17;
            this.RX_VM_CH3_POL_Q.UseVisualStyleBackColor = true;
            this.RX_VM_CH3_POL_Q.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH2_RX_Phase_Q.Location = new Point(133, 162);
            this.CH2_RX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH2_RX_Phase_Q.Name = "CH2_RX_Phase_Q";
            this.CH2_RX_Phase_Q.Size = new Size(120, 22);
            this.CH2_RX_Phase_Q.TabIndex = 16;
            this.CH2_RX_Phase_Q.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.RX_VM_CH2_POL_Q.AutoSize = true;
            this.RX_VM_CH2_POL_Q.Location = new Point(57, 164);
            this.RX_VM_CH2_POL_Q.Name = "RX_VM_CH2_POL_Q";
            this.RX_VM_CH2_POL_Q.Size = new Size(18, 17);
            this.RX_VM_CH2_POL_Q.TabIndex = 15;
            this.RX_VM_CH2_POL_Q.UseVisualStyleBackColor = true;
            this.RX_VM_CH2_POL_Q.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH1_RX_Phase_Q.Location = new Point(133, 83);
            this.CH1_RX_Phase_Q.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH1_RX_Phase_Q.Name = "CH1_RX_Phase_Q";
            this.CH1_RX_Phase_Q.Size = new Size(120, 22);
            this.CH1_RX_Phase_Q.TabIndex = 14;
            this.CH1_RX_Phase_Q.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.RX_VM_CH1_POL_Q.AutoSize = true;
            this.RX_VM_CH1_POL_Q.Location = new Point(57, 85);
            this.RX_VM_CH1_POL_Q.Name = "RX_VM_CH1_POL_Q";
            this.RX_VM_CH1_POL_Q.Size = new Size(18, 17);
            this.RX_VM_CH1_POL_Q.TabIndex = 13;
            this.RX_VM_CH1_POL_Q.UseVisualStyleBackColor = true;
            this.RX_VM_CH1_POL_Q.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.button4.Location = new Point(57, 376);
            this.button4.Margin = new Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new Size(160, 28);
            this.button4.TabIndex = 12;
            this.button4.Text = "Write Phase Values";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.WriteRXPhase_Click);
            this.CH4_RX_Phase_I.Location = new Point(133, 292);
            this.CH4_RX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH4_RX_Phase_I.Name = "CH4_RX_Phase_I";
            this.CH4_RX_Phase_I.Size = new Size(120, 22);
            this.CH4_RX_Phase_I.TabIndex = 11;
            this.CH4_RX_Phase_I.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.label23.AutoSize = true;
            this.label23.Location = new Point(7, 273);
            this.label23.Name = "label23";
            this.label23.Size = new Size(114, 17);
            this.label23.TabIndex = 10;
            this.label23.Text = "RX VM CH4 POL";
            this.RX_VM_CH4_POL_I.AutoSize = true;
            this.RX_VM_CH4_POL_I.Location = new Point(57, 297);
            this.RX_VM_CH4_POL_I.Name = "RX_VM_CH4_POL_I";
            this.RX_VM_CH4_POL_I.Size = new Size(18, 17);
            this.RX_VM_CH4_POL_I.TabIndex = 9;
            this.RX_VM_CH4_POL_I.UseVisualStyleBackColor = true;
            this.RX_VM_CH4_POL_I.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH3_RX_Phase_I.Location = new Point(133, 213);
            this.CH3_RX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH3_RX_Phase_I.Name = "CH3_RX_Phase_I";
            this.CH3_RX_Phase_I.Size = new Size(120, 22);
            this.CH3_RX_Phase_I.TabIndex = 8;
            this.CH3_RX_Phase_I.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.label24.AutoSize = true;
            this.label24.Location = new Point(7, 194);
            this.label24.Name = "label24";
            this.label24.Size = new Size(114, 17);
            this.label24.TabIndex = 7;
            this.label24.Text = "RX VM CH3 POL";
            this.RX_VM_CH3_POL_I.AutoSize = true;
            this.RX_VM_CH3_POL_I.Location = new Point(57, 218);
            this.RX_VM_CH3_POL_I.Name = "RX_VM_CH3_POL_I";
            this.RX_VM_CH3_POL_I.Size = new Size(18, 17);
            this.RX_VM_CH3_POL_I.TabIndex = 6;
            this.RX_VM_CH3_POL_I.UseVisualStyleBackColor = true;
            this.RX_VM_CH3_POL_I.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH2_RX_Phase_I.Location = new Point(133, 134);
            this.CH2_RX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH2_RX_Phase_I.Name = "CH2_RX_Phase_I";
            this.CH2_RX_Phase_I.Size = new Size(120, 22);
            this.CH2_RX_Phase_I.TabIndex = 5;
            this.CH2_RX_Phase_I.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.label25.AutoSize = true;
            this.label25.Location = new Point(7, 115);
            this.label25.Name = "label25";
            this.label25.Size = new Size(114, 17);
            this.label25.TabIndex = 4;
            this.label25.Text = "RX VM CH2 POL";
            this.RX_VM_CH2_POL_I.AutoSize = true;
            this.RX_VM_CH2_POL_I.Location = new Point(57, 139);
            this.RX_VM_CH2_POL_I.Name = "RX_VM_CH2_POL_I";
            this.RX_VM_CH2_POL_I.Size = new Size(18, 17);
            this.RX_VM_CH2_POL_I.TabIndex = 3;
            this.RX_VM_CH2_POL_I.UseVisualStyleBackColor = true;
            this.RX_VM_CH2_POL_I.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.CH1_RX_Phase_I.Location = new Point(133, 55);
            this.CH1_RX_Phase_I.Maximum = new Decimal(new int[4]
            {
        31,
        0,
        0,
        0
            });
            this.CH1_RX_Phase_I.Name = "CH1_RX_Phase_I";
            this.CH1_RX_Phase_I.Size = new Size(120, 22);
            this.CH1_RX_Phase_I.TabIndex = 2;
            this.CH1_RX_Phase_I.ValueChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.label26.AutoSize = true;
            this.label26.Location = new Point(7, 36);
            this.label26.Name = "label26";
            this.label26.Size = new Size(114, 17);
            this.label26.TabIndex = 1;
            this.label26.Text = "RX VM CH1 POL";
            this.RX_VM_CH1_POL_I.AutoSize = true;
            this.RX_VM_CH1_POL_I.Location = new Point(57, 60);
            this.RX_VM_CH1_POL_I.Name = "RX_VM_CH1_POL_I";
            this.RX_VM_CH1_POL_I.Size = new Size(18, 17);
            this.RX_VM_CH1_POL_I.TabIndex = 0;
            this.RX_VM_CH1_POL_I.UseVisualStyleBackColor = true;
            this.RX_VM_CH1_POL_I.CheckStateChanged += new EventHandler(this.RX_Phase_ValueChanged);
            this.groupBox3.Controls.Add((Control)this.label20);
            this.groupBox3.Controls.Add((Control)this.label17);
            this.groupBox3.Controls.Add((Control)this.label16);
            this.groupBox3.Controls.Add((Control)this.label9);
            this.groupBox3.Controls.Add((Control)this.label7);
            this.groupBox3.Controls.Add((Control)this.label6);
            this.groupBox3.Controls.Add((Control)this.label5);
            this.groupBox3.Controls.Add((Control)this.button3);
            this.groupBox3.Controls.Add((Control)this.RXGain4);
            this.groupBox3.Controls.Add((Control)this.RXGain4_Attenuation);
            this.groupBox3.Controls.Add((Control)this.RXGain3);
            this.groupBox3.Controls.Add((Control)this.RXGain3_Attenuation);
            this.groupBox3.Controls.Add((Control)this.RXGain2);
            this.groupBox3.Controls.Add((Control)this.RXGain2_Attenuation);
            this.groupBox3.Controls.Add((Control)this.RXGain1);
            this.groupBox3.Controls.Add((Control)this.label19);
            this.groupBox3.Controls.Add((Control)this.RXGain1_Attenuation);
            this.groupBox3.Location = new Point(4, 7);
            this.groupBox3.Margin = new Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new Padding(4);
            this.groupBox3.Size = new Size(260, 412);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rx Gain Control";
            this.label20.AutoSize = true;
            this.label20.Location = new Point(130, 272);
            this.label20.Name = "label20";
            this.label20.Size = new Size(91, 17);
            this.label20.TabIndex = 19;
            this.label20.Text = "RX VGA CH4";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(130, 194);
            this.label17.Name = "label17";
            this.label17.Size = new Size(91, 17);
            this.label17.TabIndex = 18;
            this.label17.Text = "RX VGA CH3";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(130, 114);
            this.label16.Name = "label16";
            this.label16.Size = new Size(91, 17);
            this.label16.TabIndex = 17;
            this.label16.Text = "RX VGA CH2";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(6, 273);
            this.label9.Name = "label9";
            this.label9.Size = new Size(99, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "CH4 RX ATTN";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(6, 194);
            this.label7.Name = "label7";
            this.label7.Size = new Size(99, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "CH3 RX ATTN";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(6, 114);
            this.label6.Name = "label6";
            this.label6.Size = new Size(99, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "CH2 RX ATTN";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(130, 36);
            this.label5.Name = "label5";
            this.label5.Size = new Size(91, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "RX VGA CH1";
            this.button3.Location = new Point(50, 376);
            this.button3.Margin = new Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new Size(160, 28);
            this.button3.TabIndex = 12;
            this.button3.Text = "Write Gain Values";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.WriteRXGain_Click);
            this.RXGain4.Location = new Point(132, 293);
            this.RXGain4.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RXGain4.Name = "RXGain4";
            this.RXGain4.Size = new Size(120, 22);
            this.RXGain4.TabIndex = 11;
            this.RXGain4.ValueChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain4_Attenuation.AutoSize = true;
            this.RXGain4_Attenuation.Location = new Point(40, 298);
            this.RXGain4_Attenuation.Name = "RXGain4_Attenuation";
            this.RXGain4_Attenuation.Size = new Size(18, 17);
            this.RXGain4_Attenuation.TabIndex = 9;
            this.RXGain4_Attenuation.UseVisualStyleBackColor = true;
            this.RXGain4_Attenuation.CheckStateChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain3.Location = new Point(132, 215);
            this.RXGain3.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RXGain3.Name = "RXGain3";
            this.RXGain3.Size = new Size(120, 22);
            this.RXGain3.TabIndex = 8;
            this.RXGain3.ValueChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain3_Attenuation.AutoSize = true;
            this.RXGain3_Attenuation.Location = new Point(40, 220);
            this.RXGain3_Attenuation.Name = "RXGain3_Attenuation";
            this.RXGain3_Attenuation.Size = new Size(18, 17);
            this.RXGain3_Attenuation.TabIndex = 6;
            this.RXGain3_Attenuation.UseVisualStyleBackColor = true;
            this.RXGain3_Attenuation.CheckStateChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain2.Location = new Point(132, 135);
            this.RXGain2.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RXGain2.Name = "RXGain2";
            this.RXGain2.RightToLeft = RightToLeft.No;
            this.RXGain2.Size = new Size(120, 22);
            this.RXGain2.TabIndex = 5;
            this.RXGain2.ValueChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain2_Attenuation.AutoSize = true;
            this.RXGain2_Attenuation.Location = new Point(40, 140);
            this.RXGain2_Attenuation.Name = "RXGain2_Attenuation";
            this.RXGain2_Attenuation.Size = new Size(18, 17);
            this.RXGain2_Attenuation.TabIndex = 3;
            this.RXGain2_Attenuation.UseVisualStyleBackColor = true;
            this.RXGain2_Attenuation.CheckStateChanged += new EventHandler(this.RXGain_ValueChanged);
            this.RXGain1.Location = new Point(132, 54);
            this.RXGain1.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RXGain1.Name = "RXGain1";
            this.RXGain1.Size = new Size(120, 22);
            this.RXGain1.TabIndex = 2;
            this.RXGain1.ValueChanged += new EventHandler(this.RXGain_ValueChanged);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(7, 36);
            this.label19.Name = "label19";
            this.label19.Size = new Size(99, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "CH1 RX ATTN";
            this.RXGain1_Attenuation.AutoSize = true;
            this.RXGain1_Attenuation.Location = new Point(40, 61);
            this.RXGain1_Attenuation.Name = "RXGain1_Attenuation";
            this.RXGain1_Attenuation.Size = new Size(18, 17);
            this.RXGain1_Attenuation.TabIndex = 0;
            this.RXGain1_Attenuation.UseVisualStyleBackColor = true;
            this.RXGain1_Attenuation.CheckStateChanged += new EventHandler(this.RXGain_ValueChanged);
            this.groupBox20.Controls.Add((Control)this.label139);
            this.groupBox20.Controls.Add((Control)this.RX_CHX_RAM_INDEX);
            this.groupBox20.Controls.Add((Control)this.label140);
            this.groupBox20.Controls.Add((Control)this.RX_CHX_RAM_FETCH);
            this.groupBox20.Controls.Add((Control)this.label126);
            this.groupBox20.Controls.Add((Control)this.label79);
            this.groupBox20.Controls.Add((Control)this.RX_CH4_RAM_FETCH);
            this.groupBox20.Controls.Add((Control)this.label78);
            this.groupBox20.Controls.Add((Control)this.RX_CH3_RAM_FETCH);
            this.groupBox20.Controls.Add((Control)this.label77);
            this.groupBox20.Controls.Add((Control)this.RX_CH2_RAM_FETCH);
            this.groupBox20.Controls.Add((Control)this.label76);
            this.groupBox20.Controls.Add((Control)this.button16);
            this.groupBox20.Controls.Add((Control)this.RX_CH4_RAM_INDEX);
            this.groupBox20.Controls.Add((Control)this.label64);
            this.groupBox20.Controls.Add((Control)this.RX_CH3_RAM_INDEX);
            this.groupBox20.Controls.Add((Control)this.label66);
            this.groupBox20.Controls.Add((Control)this.RX_CH2_RAM_INDEX);
            this.groupBox20.Controls.Add((Control)this.label70);
            this.groupBox20.Controls.Add((Control)this.RX_CH1_RAM_INDEX);
            this.groupBox20.Controls.Add((Control)this.label71);
            this.groupBox20.Controls.Add((Control)this.RX_CH1_RAM_FETCH);
            this.groupBox20.Location = new Point(539, 6);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new Size(341, 412);
            this.groupBox20.TabIndex = 2;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Memory Index";
            this.label139.AutoSize = true;
            this.label139.Location = new Point(243, 84);
            this.label139.Name = "label139";
            this.label139.Size = new Size(54, 17);
            this.label139.TabIndex = 53;
            this.label139.Text = "SPI Ctrl";
            this.RX_CHX_RAM_INDEX.Location = new Point(201, 56);
            this.RX_CHX_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RX_CHX_RAM_INDEX.Name = "RX_CHX_RAM_INDEX";
            this.RX_CHX_RAM_INDEX.Size = new Size(120, 22);
            this.RX_CHX_RAM_INDEX.TabIndex = 52;
            this.RX_CHX_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label140.AutoSize = true;
            this.label140.Location = new Point(198, 37);
            this.label140.Name = "label140";
            this.label140.Size = new Size(138, 17);
            this.label140.TabIndex = 51;
            this.label140.Text = "RX CHX RAM INDEX";
            this.RX_CHX_RAM_FETCH.AutoSize = true;
            this.RX_CHX_RAM_FETCH.Location = new Point(303, 84);
            this.RX_CHX_RAM_FETCH.Name = "RX_CHX_RAM_FETCH";
            this.RX_CHX_RAM_FETCH.Size = new Size(18, 17);
            this.RX_CHX_RAM_FETCH.TabIndex = 50;
            this.RX_CHX_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_CHX_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label126.AutoSize = true;
            this.label126.Location = new Point(61, 323);
            this.label126.Name = "label126";
            this.label126.Size = new Size(54, 17);
            this.label126.TabIndex = 49;
            this.label126.Text = "SPI Ctrl";
            this.label79.Location = new Point(0, 0);
            this.label79.Name = "label79";
            this.label79.Size = new Size(100, 23);
            this.label79.TabIndex = 0;
            this.RX_CH4_RAM_FETCH.AutoSize = true;
            this.RX_CH4_RAM_FETCH.Location = new Point(121, 323);
            this.RX_CH4_RAM_FETCH.Name = "RX_CH4_RAM_FETCH";
            this.RX_CH4_RAM_FETCH.Size = new Size(18, 17);
            this.RX_CH4_RAM_FETCH.TabIndex = 48;
            this.RX_CH4_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_CH4_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label78.AutoSize = true;
            this.label78.Location = new Point(61, 243);
            this.label78.Name = "label78";
            this.label78.Size = new Size(54, 17);
            this.label78.TabIndex = 47;
            this.label78.Text = "SPI Ctrl";
            this.RX_CH3_RAM_FETCH.AutoSize = true;
            this.RX_CH3_RAM_FETCH.Location = new Point(121, 243);
            this.RX_CH3_RAM_FETCH.Name = "RX_CH3_RAM_FETCH";
            this.RX_CH3_RAM_FETCH.Size = new Size(18, 17);
            this.RX_CH3_RAM_FETCH.TabIndex = 46;
            this.RX_CH3_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_CH3_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label77.AutoSize = true;
            this.label77.Location = new Point(61, 163);
            this.label77.Name = "label77";
            this.label77.Size = new Size(54, 17);
            this.label77.TabIndex = 45;
            this.label77.Text = "SPI Ctrl";
            this.RX_CH2_RAM_FETCH.AutoSize = true;
            this.RX_CH2_RAM_FETCH.Location = new Point(121, 163);
            this.RX_CH2_RAM_FETCH.Name = "RX_CH2_RAM_FETCH";
            this.RX_CH2_RAM_FETCH.Size = new Size(18, 17);
            this.RX_CH2_RAM_FETCH.TabIndex = 44;
            this.RX_CH2_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_CH2_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label76.AutoSize = true;
            this.label76.Location = new Point(61, 83);
            this.label76.Name = "label76";
            this.label76.Size = new Size(54, 17);
            this.label76.TabIndex = 43;
            this.label76.Text = "SPI Ctrl";
            this.button16.Location = new Point(7, 376);
            this.button16.Margin = new Padding(4);
            this.button16.Name = "button16";
            this.button16.Size = new Size(157, 28);
            this.button16.TabIndex = 42;
            this.button16.Text = "Write Memory Values";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new EventHandler(this.TX_Mem_Click);
            this.RX_CH4_RAM_INDEX.Location = new Point(19, 295);
            this.RX_CH4_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RX_CH4_RAM_INDEX.Name = "RX_CH4_RAM_INDEX";
            this.RX_CH4_RAM_INDEX.Size = new Size(120, 22);
            this.RX_CH4_RAM_INDEX.TabIndex = 41;
            this.RX_CH4_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label64.AutoSize = true;
            this.label64.Location = new Point(15, 273);
            this.label64.Name = "label64";
            this.label64.Size = new Size(137, 17);
            this.label64.TabIndex = 40;
            this.label64.Text = "RX CH4 RAM INDEX";
            this.RX_CH3_RAM_INDEX.Location = new Point(19, 215);
            this.RX_CH3_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RX_CH3_RAM_INDEX.Name = "RX_CH3_RAM_INDEX";
            this.RX_CH3_RAM_INDEX.Size = new Size(120, 22);
            this.RX_CH3_RAM_INDEX.TabIndex = 39;
            this.RX_CH3_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label66.AutoSize = true;
            this.label66.Location = new Point(16, 195);
            this.label66.Name = "label66";
            this.label66.Size = new Size(137, 17);
            this.label66.TabIndex = 38;
            this.label66.Text = "RX CH3 RAM INDEX";
            this.RX_CH2_RAM_INDEX.Location = new Point(19, 135);
            this.RX_CH2_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RX_CH2_RAM_INDEX.Name = "RX_CH2_RAM_INDEX";
            this.RX_CH2_RAM_INDEX.RightToLeft = RightToLeft.No;
            this.RX_CH2_RAM_INDEX.Size = new Size(120, 22);
            this.RX_CH2_RAM_INDEX.TabIndex = 37;
            this.RX_CH2_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label70.AutoSize = true;
            this.label70.Location = new Point(16, 115);
            this.label70.Name = "label70";
            this.label70.Size = new Size(137, 17);
            this.label70.TabIndex = 36;
            this.label70.Text = "RX CH2 RAM INDEX";
            this.RX_CH1_RAM_INDEX.Location = new Point(19, 55);
            this.RX_CH1_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.RX_CH1_RAM_INDEX.Name = "RX_CH1_RAM_INDEX";
            this.RX_CH1_RAM_INDEX.Size = new Size(120, 22);
            this.RX_CH1_RAM_INDEX.TabIndex = 35;
            this.RX_CH1_RAM_INDEX.ValueChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.label71.AutoSize = true;
            this.label71.Location = new Point(16, 36);
            this.label71.Name = "label71";
            this.label71.Size = new Size(137, 17);
            this.label71.TabIndex = 34;
            this.label71.Text = "RX CH1 RAM INDEX";
            this.RX_CH1_RAM_FETCH.AutoSize = true;
            this.RX_CH1_RAM_FETCH.Location = new Point(121, 83);
            this.RX_CH1_RAM_FETCH.Name = "RX_CH1_RAM_FETCH";
            this.RX_CH1_RAM_FETCH.Size = new Size(18, 17);
            this.RX_CH1_RAM_FETCH.TabIndex = 33;
            this.RX_CH1_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_CH1_RAM_FETCH.CheckStateChanged += new EventHandler(this.MEMORY_INDEX_ValueChanged);
            this.groupBox19.Controls.Add((Control)this.label60);
            this.groupBox19.Controls.Add((Control)this.LNA_BIAS);
            this.groupBox19.Controls.Add((Control)this.button11);
            this.groupBox19.Controls.Add((Control)this.label61);
            this.groupBox19.Controls.Add((Control)this.label58);
            this.groupBox19.Controls.Add((Control)this.RX_VM_BIAS2);
            this.groupBox19.Controls.Add((Control)this.RX_VGA_BIAS2);
            this.groupBox19.Controls.Add((Control)this.label68);
            this.groupBox19.Location = new Point(886, 6);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new Size(241, 196);
            this.groupBox19.TabIndex = 1;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Bias Current";
            this.label60.AutoSize = true;
            this.label60.Location = new Point(25, 115);
            this.label60.Name = "label60";
            this.label60.Size = new Size(69, 17);
            this.label60.TabIndex = 35;
            this.label60.Text = "LNA BIAS";
            this.LNA_BIAS.Location = new Point(100, 113);
            this.LNA_BIAS.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.LNA_BIAS.Name = "LNA_BIAS";
            this.LNA_BIAS.Size = new Size(120, 22);
            this.LNA_BIAS.TabIndex = 34;
            this.LNA_BIAS.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.button11.Location = new Point(48, 161);
            this.button11.Margin = new Padding(4);
            this.button11.Name = "button11";
            this.button11.Size = new Size(146, 28);
            this.button11.TabIndex = 33;
            this.button11.Text = "Write Bias Values";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new EventHandler(this.Bias_Current_Click);
            this.label61.AutoSize = true;
            this.label61.Location = new Point(9, 87);
            this.label61.Name = "label61";
            this.label61.Size = new Size(85, 17);
            this.label61.TabIndex = 32;
            this.label61.Text = "RX VM BIAS";
            this.label58.AutoSize = true;
            this.label58.Location = new Point(0, 59);
            this.label58.Name = "label58";
            this.label58.Size = new Size(94, 17);
            this.label58.TabIndex = 31;
            this.label58.Text = "RX VGA BIAS";
            this.RX_VM_BIAS2.Location = new Point(100, 85);
            this.RX_VM_BIAS2.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.RX_VM_BIAS2.Name = "RX_VM_BIAS2";
            this.RX_VM_BIAS2.Size = new Size(120, 22);
            this.RX_VM_BIAS2.TabIndex = 30;
            this.RX_VM_BIAS2.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.RX_VGA_BIAS2.Location = new Point(100, 57);
            this.RX_VGA_BIAS2.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.RX_VGA_BIAS2.Name = "RX_VGA_BIAS2";
            this.RX_VGA_BIAS2.Size = new Size(120, 22);
            this.RX_VGA_BIAS2.TabIndex = 29;
            this.RX_VGA_BIAS2.ValueChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.label68.AutoSize = true;
            this.label68.Location = new Point(10, 36);
            this.label68.Name = "label68";
            this.label68.Size = new Size(98, 17);
            this.label68.TabIndex = 28;
            this.label68.Text = "Bias Current 2";
            this.TRControl.Controls.Add((Control)this.groupBox26);
            this.TRControl.Controls.Add((Control)this.groupBox25);
            this.TRControl.Controls.Add((Control)this.groupBox24);
            this.TRControl.Location = new Point(4, 25);
            this.TRControl.Name = "TRControl";
            this.TRControl.Size = new Size(1185, 446);
            this.TRControl.TabIndex = 12;
            this.TRControl.Text = "T/R Control";
            this.TRControl.ToolTipText = "Enable/disable Tx paths, LNAs, VGAs and control switches";
            this.TRControl.UseVisualStyleBackColor = true;
            this.groupBox26.Controls.Add((Control)this.RX_EN_2);
            this.groupBox26.Controls.Add((Control)this.TX_EN_2);
            this.groupBox26.Controls.Add((Control)this.SW_DRV_TR_STATE_2);
            this.groupBox26.Controls.Add((Control)this.label124);
            this.groupBox26.Controls.Add((Control)this.POL_2);
            this.groupBox26.Controls.Add((Control)this.TR_SPI_2);
            this.groupBox26.Controls.Add((Control)this.TR_SOURCE_2);
            this.groupBox26.Controls.Add((Control)this.SW_DRV_EN_POL_2);
            this.groupBox26.Controls.Add((Control)this.SW_DRV_EN_TR_2);
            this.groupBox26.Controls.Add((Control)this.button10_2);
            this.groupBox26.Location = new Point(381, 4);
            this.groupBox26.Margin = new Padding(4);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Padding = new Padding(4);
            this.groupBox26.Size = new Size(163, 406);
            this.groupBox26.TabIndex = 24;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Switch";
            this.RX_EN_2.AutoSize = true;
            this.RX_EN_2.Location = new Point(8, 81);
            this.RX_EN_2.Name = "RX_EN_2";
            this.RX_EN_2.Size = new Size(72, 21);
            this.RX_EN_2.TabIndex = 43;
            this.RX_EN_2.Text = "RX EN";
            this.RX_EN_2.UseVisualStyleBackColor = true;
            this.RX_EN_2.CheckStateChanged += new EventHandler(this.RX_Enable_toggle);
            this.TX_EN_2.AutoSize = true;
            this.TX_EN_2.Location = new Point(8, 54);
            this.TX_EN_2.Name = "TX_EN_2";
            this.TX_EN_2.Size = new Size(71, 21);
            this.TX_EN_2.TabIndex = 54;
            this.TX_EN_2.Text = "TX EN";
            this.TX_EN_2.UseVisualStyleBackColor = true;
            this.TX_EN_2.CheckStateChanged += new EventHandler(this.TX_Enable_toggle);
            this.SW_DRV_TR_STATE_2.AutoSize = true;
            this.SW_DRV_TR_STATE_2.Location = new Point(8, 108);
            this.SW_DRV_TR_STATE_2.Name = "SW_DRV_TR_STATE_2";
            this.SW_DRV_TR_STATE_2.Size = new Size(157, 21);
            this.SW_DRV_TR_STATE_2.TabIndex = 40;
            this.SW_DRV_TR_STATE_2.Text = "SW DRV TR STATE";
            this.SW_DRV_TR_STATE_2.UseVisualStyleBackColor = true;
            this.SW_DRV_TR_STATE_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.label124.AutoSize = true;
            this.label124.Location = new Point(5, 19);
            this.label124.Margin = new Padding(4, 0, 4, 0);
            this.label124.Name = "label124";
            this.label124.Size = new Size(97, 17);
            this.label124.TabIndex = 39;
            this.label124.Text = "Switch Control";
            this.POL_2.AutoSize = true;
            this.POL_2.Location = new Point(8, 243);
            this.POL_2.Name = "POL_2";
            this.POL_2.Size = new Size(58, 21);
            this.POL_2.TabIndex = 38;
            this.POL_2.Text = "POL";
            this.POL_2.UseVisualStyleBackColor = true;
            this.POL_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.TR_SPI_2.AutoSize = true;
            this.TR_SPI_2.Location = new Point(8, 216);
            this.TR_SPI_2.Name = "TR_SPI_2";
            this.TR_SPI_2.Size = new Size(74, 21);
            this.TR_SPI_2.TabIndex = 37;
            this.TR_SPI_2.Text = "TR SPI";
            this.TR_SPI_2.UseVisualStyleBackColor = true;
            this.TR_SPI_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.TR_SOURCE_2.AutoSize = true;
            this.TR_SOURCE_2.Location = new Point(8, 189);
            this.TR_SOURCE_2.Name = "TR_SOURCE_2";
            this.TR_SOURCE_2.Size = new Size(111, 21);
            this.TR_SOURCE_2.TabIndex = 36;
            this.TR_SOURCE_2.Text = "TR SOURCE";
            this.TR_SOURCE_2.UseVisualStyleBackColor = true;
            this.TR_SOURCE_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.SW_DRV_EN_POL_2.AutoSize = true;
            this.SW_DRV_EN_POL_2.Location = new Point(8, 162);
            this.SW_DRV_EN_POL_2.Name = "SW_DRV_EN_POL_2";
            this.SW_DRV_EN_POL_2.Size = new Size(140, 21);
            this.SW_DRV_EN_POL_2.TabIndex = 35;
            this.SW_DRV_EN_POL_2.Text = "SW DRV EN POL";
            this.SW_DRV_EN_POL_2.UseVisualStyleBackColor = true;
            this.SW_DRV_EN_POL_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.SW_DRV_EN_TR_2.AutoSize = true;
            this.SW_DRV_EN_TR_2.Location = new Point(8, 135);
            this.SW_DRV_EN_TR_2.Name = "SW_DRV_EN_TR_2";
            this.SW_DRV_EN_TR_2.Size = new Size(131, 21);
            this.SW_DRV_EN_TR_2.TabIndex = 34;
            this.SW_DRV_EN_TR_2.Text = "SW DRV EN TR";
            this.SW_DRV_EN_TR_2.UseVisualStyleBackColor = true;
            this.SW_DRV_EN_TR_2.CheckStateChanged += new EventHandler(this.SW_2_CheckedChanged);
            this.button10_2.Location = new Point(8, 371);
            this.button10_2.Margin = new Padding(4);
            this.button10_2.Name = "button10_2";
            this.button10_2.Size = new Size(143, 28);
            this.button10_2.TabIndex = 13;
            this.button10_2.Text = "Write Switch Values";
            this.button10_2.UseVisualStyleBackColor = true;
            this.button10_2.Click += new EventHandler(this.SW_CTRL_2_Click);
            this.groupBox25.Controls.Add((Control)this.label123);
            this.groupBox25.Controls.Add((Control)this.RX_VGA_EN_2);
            this.groupBox25.Controls.Add((Control)this.RX_VM_EN_2);
            this.groupBox25.Controls.Add((Control)this.RX_LNA_EN_2);
            this.groupBox25.Controls.Add((Control)this.CH4_RX_EN_2);
            this.groupBox25.Controls.Add((Control)this.CH3_RX_EN_2);
            this.groupBox25.Controls.Add((Control)this.CH2_RX_EN_2);
            this.groupBox25.Controls.Add((Control)this.CH1_RX_EN_2);
            this.groupBox25.Controls.Add((Control)this.button7_2);
            this.groupBox25.Location = new Point(204, 3);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new Size(170, 407);
            this.groupBox25.TabIndex = 5;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Enables";
            this.label123.AutoSize = true;
            this.label123.Location = new Point(7, 18);
            this.label123.Margin = new Padding(4, 0, 4, 0);
            this.label123.Name = "label123";
            this.label123.Size = new Size(75, 17);
            this.label123.TabIndex = 41;
            this.label123.Text = "RX Enable";
            this.RX_VGA_EN_2.AutoSize = true;
            this.RX_VGA_EN_2.Location = new Point(10, 217);
            this.RX_VGA_EN_2.Name = "RX_VGA_EN_2";
            this.RX_VGA_EN_2.Size = new Size(109, 21);
            this.RX_VGA_EN_2.TabIndex = 40;
            this.RX_VGA_EN_2.Text = "RX VGA EN ";
            this.RX_VGA_EN_2.UseVisualStyleBackColor = true;
            this.RX_VGA_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.RX_VM_EN_2.AutoSize = true;
            this.RX_VM_EN_2.Location = new Point(10, 190);
            this.RX_VM_EN_2.Name = "RX_VM_EN_2";
            this.RX_VM_EN_2.Size = new Size(96, 21);
            this.RX_VM_EN_2.TabIndex = 39;
            this.RX_VM_EN_2.Text = "RX VM EN";
            this.RX_VM_EN_2.UseVisualStyleBackColor = true;
            this.RX_VM_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.RX_LNA_EN_2.AutoSize = true;
            this.RX_LNA_EN_2.Location = new Point(10, 163);
            this.RX_LNA_EN_2.Name = "RX_LNA_EN_2";
            this.RX_LNA_EN_2.Size = new Size(103, 21);
            this.RX_LNA_EN_2.TabIndex = 38;
            this.RX_LNA_EN_2.Text = "RX LNA EN";
            this.RX_LNA_EN_2.UseVisualStyleBackColor = true;
            this.RX_LNA_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.CH4_RX_EN_2.AutoSize = true;
            this.CH4_RX_EN_2.Location = new Point(10, 136);
            this.CH4_RX_EN_2.Name = "CH4_RX_EN_2";
            this.CH4_RX_EN_2.Size = new Size(103, 21);
            this.CH4_RX_EN_2.TabIndex = 37;
            this.CH4_RX_EN_2.Text = "CH4 RX EN";
            this.CH4_RX_EN_2.UseVisualStyleBackColor = true;
            this.CH4_RX_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.CH3_RX_EN_2.AutoSize = true;
            this.CH3_RX_EN_2.Location = new Point(10, 109);
            this.CH3_RX_EN_2.Name = "CH3_RX_EN_2";
            this.CH3_RX_EN_2.Size = new Size(103, 21);
            this.CH3_RX_EN_2.TabIndex = 36;
            this.CH3_RX_EN_2.Text = "CH3 RX EN";
            this.CH3_RX_EN_2.UseVisualStyleBackColor = true;
            this.CH3_RX_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.CH2_RX_EN_2.AutoSize = true;
            this.CH2_RX_EN_2.Location = new Point(10, 82);
            this.CH2_RX_EN_2.Name = "CH2_RX_EN_2";
            this.CH2_RX_EN_2.Size = new Size(103, 21);
            this.CH2_RX_EN_2.TabIndex = 35;
            this.CH2_RX_EN_2.Text = "CH2 RX EN";
            this.CH2_RX_EN_2.UseVisualStyleBackColor = true;
            this.CH2_RX_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.CH1_RX_EN_2.AutoSize = true;
            this.CH1_RX_EN_2.Location = new Point(10, 55);
            this.CH1_RX_EN_2.Name = "CH1_RX_EN_2";
            this.CH1_RX_EN_2.Size = new Size(103, 21);
            this.CH1_RX_EN_2.TabIndex = 34;
            this.CH1_RX_EN_2.Text = "CH1 RX EN";
            this.CH1_RX_EN_2.UseVisualStyleBackColor = true;
            this.CH1_RX_EN_2.CheckStateChanged += new EventHandler(this.EN_BIAS_2_CheckChanged);
            this.button7_2.Location = new Point(7, 372);
            this.button7_2.Margin = new Padding(4);
            this.button7_2.Name = "button7_2";
            this.button7_2.Size = new Size(149, 28);
            this.button7_2.TabIndex = 33;
            this.button7_2.Text = "Write Enable Values";
            this.button7_2.UseVisualStyleBackColor = true;
            this.button7_2.Click += new EventHandler(this.RX_enables_2_Click);
            this.groupBox24.Controls.Add((Control)this.CH1_TX_EN_2);
            this.groupBox24.Controls.Add((Control)this.label122);
            this.groupBox24.Controls.Add((Control)this.button8_2);
            this.groupBox24.Controls.Add((Control)this.TX_VGA_EN_2);
            this.groupBox24.Controls.Add((Control)this.TX_VM_EN_2);
            this.groupBox24.Controls.Add((Control)this.TX_DRV_EN_2);
            this.groupBox24.Controls.Add((Control)this.CH4_TX_EN_2);
            this.groupBox24.Controls.Add((Control)this.CH3_TX_EN_2);
            this.groupBox24.Controls.Add((Control)this.CH2_TX_EN_2);
            this.groupBox24.Location = new Point(6, 3);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new Size(192, 407);
            this.groupBox24.TabIndex = 4;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Enables";
            this.CH1_TX_EN_2.AutoSize = true;
            this.CH1_TX_EN_2.Location = new Point(10, 55);
            this.CH1_TX_EN_2.Name = "CH1_TX_EN_2";
            this.CH1_TX_EN_2.Size = new Size(102, 21);
            this.CH1_TX_EN_2.TabIndex = 53;
            this.CH1_TX_EN_2.Text = "CH1 TX EN";
            this.CH1_TX_EN_2.UseVisualStyleBackColor = true;
            this.CH1_TX_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.label122.AutoSize = true;
            this.label122.Location = new Point(7, 18);
            this.label122.Margin = new Padding(4, 0, 4, 0);
            this.label122.Name = "label122";
            this.label122.Size = new Size(74, 17);
            this.label122.TabIndex = 52;
            this.label122.Text = "TX Enable";
            this.button8_2.Location = new Point(7, 371);
            this.button8_2.Margin = new Padding(4);
            this.button8_2.Name = "button8_2";
            this.button8_2.Size = new Size(178, 28);
            this.button8_2.TabIndex = 51;
            this.button8_2.Text = "Write Enable Values";
            this.button8_2.UseVisualStyleBackColor = true;
            this.button8_2.Click += new EventHandler(this.TX_enables_2_Click);
            this.TX_VGA_EN_2.AutoSize = true;
            this.TX_VGA_EN_2.Location = new Point(10, 217);
            this.TX_VGA_EN_2.Name = "TX_VGA_EN_2";
            this.TX_VGA_EN_2.Size = new Size(108, 21);
            this.TX_VGA_EN_2.TabIndex = 50;
            this.TX_VGA_EN_2.Text = "TX VGA EN ";
            this.TX_VGA_EN_2.UseVisualStyleBackColor = true;
            this.TX_VGA_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.TX_VM_EN_2.AutoSize = true;
            this.TX_VM_EN_2.Location = new Point(10, 190);
            this.TX_VM_EN_2.Name = "TX_VM_EN_2";
            this.TX_VM_EN_2.Size = new Size(95, 21);
            this.TX_VM_EN_2.TabIndex = 49;
            this.TX_VM_EN_2.Text = "TX VM EN";
            this.TX_VM_EN_2.UseVisualStyleBackColor = true;
            this.TX_VM_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.TX_DRV_EN_2.AutoSize = true;
            this.TX_DRV_EN_2.Location = new Point(10, 163);
            this.TX_DRV_EN_2.Name = "TX_DRV_EN_2";
            this.TX_DRV_EN_2.Size = new Size(104, 21);
            this.TX_DRV_EN_2.TabIndex = 48;
            this.TX_DRV_EN_2.Text = "TX DRV EN";
            this.TX_DRV_EN_2.UseVisualStyleBackColor = true;
            this.TX_DRV_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.CH4_TX_EN_2.AutoSize = true;
            this.CH4_TX_EN_2.Location = new Point(10, 136);
            this.CH4_TX_EN_2.Name = "CH4_TX_EN_2";
            this.CH4_TX_EN_2.Size = new Size(102, 21);
            this.CH4_TX_EN_2.TabIndex = 47;
            this.CH4_TX_EN_2.Text = "CH4 TX EN";
            this.CH4_TX_EN_2.UseVisualStyleBackColor = true;
            this.CH4_TX_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.CH3_TX_EN_2.AutoSize = true;
            this.CH3_TX_EN_2.Location = new Point(10, 109);
            this.CH3_TX_EN_2.Name = "CH3_TX_EN_2";
            this.CH3_TX_EN_2.Size = new Size(102, 21);
            this.CH3_TX_EN_2.TabIndex = 46;
            this.CH3_TX_EN_2.Text = "CH3 TX EN";
            this.CH3_TX_EN_2.UseVisualStyleBackColor = true;
            this.CH3_TX_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.CH2_TX_EN_2.AutoSize = true;
            this.CH2_TX_EN_2.Location = new Point(10, 82);
            this.CH2_TX_EN_2.Name = "CH2_TX_EN_2";
            this.CH2_TX_EN_2.Size = new Size(102, 21);
            this.CH2_TX_EN_2.TabIndex = 45;
            this.CH2_TX_EN_2.Text = "CH2 TX EN";
            this.CH2_TX_EN_2.UseVisualStyleBackColor = true;
            this.CH2_TX_EN_2.CheckStateChanged += new EventHandler(this.TX_EN_2_CheckedChanged);
            this.GPIOpins.Controls.Add((Control)this.TestmodesPanel);
            this.GPIOpins.Controls.Add((Control)this.groupBox27);
            this.GPIOpins.Location = new Point(4, 25);
            this.GPIOpins.Name = "GPIOpins";
            this.GPIOpins.Padding = new Padding(3);
            this.GPIOpins.Size = new Size(1185, 446);
            this.GPIOpins.TabIndex = 11;
            this.GPIOpins.Text = "GPIO ";
            this.GPIOpins.ToolTipText = "Set state of GPIO pins";
            this.GPIOpins.UseVisualStyleBackColor = true;
            this.TestmodesPanel.Controls.Add((Control)this.groupBox31);
            this.TestmodesPanel.Controls.Add((Control)this.groupBox30);
            this.TestmodesPanel.Controls.Add((Control)this.groupBox29);
            this.TestmodesPanel.Controls.Add((Control)this.GPIO_5);
            // 2 구현 귀찮아서 안함
            this.TestmodesPanel.Controls.Add((Control)this.groupBox17);
            this.TestmodesPanel.Controls.Add((Control)this.groupBox18);
            this.TestmodesPanel.Controls.Add((Control)this.groupBox16);
            this.TestmodesPanel.Location = new Point(253, 6);
            this.TestmodesPanel.Name = "TestmodesPanel";
            this.TestmodesPanel.Size = new Size(924, 434);
            this.TestmodesPanel.TabIndex = 54;
            this.TestmodesPanel.Visible = false;
            this.groupBox31.Controls.Add((Control)this.label153);
            this.groupBox31.Controls.Add((Control)this.TX_BIAS_RAM_FETCH);
            this.groupBox31.Controls.Add((Control)this.label152);
            this.groupBox31.Controls.Add((Control)this.RX_BIAS_RAM_FETCH);
            this.groupBox31.Controls.Add((Control)this.button40);
            this.groupBox31.Controls.Add((Control)this.label151);
            this.groupBox31.Controls.Add((Control)this.RX_BIAS_RAM_INDEX);
            this.groupBox31.Controls.Add((Control)this.label154);
            this.groupBox31.Controls.Add((Control)this.TX_BIAS_RAM_INDEX);
            this.groupBox31.Controls.Add((Control)this.button39);
            this.groupBox31.Location = new Point(367, 352);
            this.groupBox31.Margin = new Padding(4);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Padding = new Padding(4);
            this.groupBox31.Size = new Size(545, 74);
            this.groupBox31.TabIndex = 56;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Bias RAM";
            this.label153.AutoSize = true;
            this.label153.Location = new Point(264, 52);
            this.label153.Name = "label153";
            this.label153.Size = new Size(143, 17);
            this.label153.TabIndex = 53;
            this.label153.Text = "TX BIAS RAM FETCH";
            this.TX_BIAS_RAM_FETCH.AutoSize = true;
            this.TX_BIAS_RAM_FETCH.Location = new Point(414, 50);
            this.TX_BIAS_RAM_FETCH.Name = "TX_BIAS_RAM_FETCH";
            this.TX_BIAS_RAM_FETCH.Size = new Size(18, 17);
            this.TX_BIAS_RAM_FETCH.TabIndex = 52;
            this.TX_BIAS_RAM_FETCH.UseVisualStyleBackColor = true;
            this.TX_BIAS_RAM_FETCH.CheckedChanged += new EventHandler(this.BIAS_RAM_CONTROL);
            this.label152.AutoSize = true;
            this.label152.Location = new Point(264, 19);
            this.label152.Name = "label152";
            this.label152.Size = new Size(144, 17);
            this.label152.TabIndex = 51;
            this.label152.Text = "RX BIAS RAM FETCH";
            this.RX_BIAS_RAM_FETCH.AutoSize = true;
            this.RX_BIAS_RAM_FETCH.Location = new Point(414, 20);
            this.RX_BIAS_RAM_FETCH.Name = "RX_BIAS_RAM_FETCH";
            this.RX_BIAS_RAM_FETCH.Size = new Size(18, 17);
            this.RX_BIAS_RAM_FETCH.TabIndex = 50;
            this.RX_BIAS_RAM_FETCH.UseVisualStyleBackColor = true;
            this.RX_BIAS_RAM_FETCH.CheckedChanged += new EventHandler(this.BIAS_RAM_CONTROL);
            this.button40.Location = new Point(453, 9);
            this.button40.Margin = new Padding(4);
            this.button40.Name = "button40";
            this.button40.Size = new Size(88, 57);
            this.button40.TabIndex = 49;
            this.button40.Text = "Write BIAS Values";
            this.button40.UseVisualStyleBackColor = true;
            this.button40.Click += new EventHandler(this.BIAS_RAM_CONTROL_Click);
            this.label151.AutoSize = true;
            this.label151.Location = new Point(7, 20);
            this.label151.Name = "label151";
            this.label151.Size = new Size(140, 17);
            this.label151.TabIndex = 48;
            this.label151.Text = "RX BIAS RAM INDEX";
            this.RX_BIAS_RAM_INDEX.Location = new Point(153, 20);
            this.RX_BIAS_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.RX_BIAS_RAM_INDEX.Name = "RX_BIAS_RAM_INDEX";
            this.RX_BIAS_RAM_INDEX.Size = new Size(92, 22);
            this.RX_BIAS_RAM_INDEX.TabIndex = 47;
            this.RX_BIAS_RAM_INDEX.ValueChanged += new EventHandler(this.BIAS_RAM_CONTROL);
            this.label154.AutoSize = true;
            this.label154.Location = new Point(7, 48);
            this.label154.Name = "label154";
            this.label154.Size = new Size(139, 17);
            this.label154.TabIndex = 42;
            this.label154.Text = "TX BIAS RAM INDEX";
            this.TX_BIAS_RAM_INDEX.Location = new Point(153, 50);
            this.TX_BIAS_RAM_INDEX.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.TX_BIAS_RAM_INDEX.Name = "TX_BIAS_RAM_INDEX";
            this.TX_BIAS_RAM_INDEX.Size = new Size(92, 22);
            this.TX_BIAS_RAM_INDEX.TabIndex = 18;
            this.TX_BIAS_RAM_INDEX.ValueChanged += new EventHandler(this.BIAS_RAM_CONTROL);
            this.button39.Location = new Point(42, 138);
            this.button39.Margin = new Padding(4);
            this.button39.Name = "button39";
            this.button39.Size = new Size(160, 28);
            this.button39.TabIndex = 12;
            this.button39.Text = "Write NVM Values";
            this.button39.UseVisualStyleBackColor = true;
            this.groupBox30.Controls.Add((Control)this.label147);
            this.groupBox30.Controls.Add((Control)this.TX_BEAM_STEP_START);
            this.groupBox30.Controls.Add((Control)this.label148);
            this.groupBox30.Controls.Add((Control)this.RX_BEAM_STEP_STOP);
            this.groupBox30.Controls.Add((Control)this.label149);
            this.groupBox30.Controls.Add((Control)this.RX_BEAM_STEP_START);
            this.groupBox30.Controls.Add((Control)this.label150);
            this.groupBox30.Controls.Add((Control)this.TX_BEAM_STEP_STOP);
            this.groupBox30.Controls.Add((Control)this.button38);
            this.groupBox30.Location = new Point(649, 178);
            this.groupBox30.Margin = new Padding(4);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Padding = new Padding(4);
            this.groupBox30.Size = new Size(274, 175);
            this.groupBox30.TabIndex = 55;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Beam Step ";
            this.label147.AutoSize = true;
            this.label147.Location = new Point(7, 23);
            this.label147.Name = "label147";
            this.label147.Size = new Size(158, 17);
            this.label147.TabIndex = 48;
            this.label147.Text = "TX BEAM STEP START";
            this.TX_BEAM_STEP_START.Location = new Point(171, 26);
            this.TX_BEAM_STEP_START.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.TX_BEAM_STEP_START.Name = "TX_BEAM_STEP_START";
            this.TX_BEAM_STEP_START.Size = new Size(92, 22);
            this.TX_BEAM_STEP_START.TabIndex = 47;
            this.TX_BEAM_STEP_START.ValueChanged += new EventHandler(this.BEAM_STEP_CONTROL);
            this.label148.AutoSize = true;
            this.label148.Location = new Point(7, 107);
            this.label148.Name = "label148";
            this.label148.Size = new Size(151, 17);
            this.label148.TabIndex = 46;
            this.label148.Text = "RX BEAM STEP STOP";
            this.RX_BEAM_STEP_STOP.Location = new Point(171, 110);
            this.RX_BEAM_STEP_STOP.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.RX_BEAM_STEP_STOP.Name = "RX_BEAM_STEP_STOP";
            this.RX_BEAM_STEP_STOP.Size = new Size(92, 22);
            this.RX_BEAM_STEP_STOP.TabIndex = 45;
            this.RX_BEAM_STEP_STOP.ValueChanged += new EventHandler(this.BEAM_STEP_CONTROL);
            this.label149.AutoSize = true;
            this.label149.Location = new Point(7, 79);
            this.label149.Name = "label149";
            this.label149.Size = new Size(159, 17);
            this.label149.TabIndex = 44;
            this.label149.Text = "RX BEAM STEP START";
            this.RX_BEAM_STEP_START.Location = new Point(171, 82);
            this.RX_BEAM_STEP_START.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.RX_BEAM_STEP_START.Name = "RX_BEAM_STEP_START";
            this.RX_BEAM_STEP_START.Size = new Size(92, 22);
            this.RX_BEAM_STEP_START.TabIndex = 43;
            this.RX_BEAM_STEP_START.ValueChanged += new EventHandler(this.BEAM_STEP_CONTROL);
            this.label150.AutoSize = true;
            this.label150.Location = new Point(7, 51);
            this.label150.Name = "label150";
            this.label150.Size = new Size(150, 17);
            this.label150.TabIndex = 42;
            this.label150.Text = "TX BEAM STEP STOP";
            this.TX_BEAM_STEP_STOP.Location = new Point(171, 54);
            this.TX_BEAM_STEP_STOP.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.TX_BEAM_STEP_STOP.Name = "TX_BEAM_STEP_STOP";
            this.TX_BEAM_STEP_STOP.Size = new Size(92, 22);
            this.TX_BEAM_STEP_STOP.TabIndex = 18;
            this.TX_BEAM_STEP_STOP.ValueChanged += new EventHandler(this.BEAM_STEP_CONTROL);
            this.button38.Location = new Point(106, 139);
            this.button38.Margin = new Padding(4);
            this.button38.Name = "button38";
            this.button38.Size = new Size(160, 28);
            this.button38.TabIndex = 12;
            this.button38.Text = "Write BEAM Values";
            this.button38.UseVisualStyleBackColor = true;
            this.button38.Click += new EventHandler(this.BEAM_STEP_CONTROL_Click);
            this.groupBox29.Controls.Add((Control)this.label146);
            this.groupBox29.Controls.Add((Control)this.TX_TO_RX_DELAY_1);
            this.groupBox29.Controls.Add((Control)this.label143);
            this.groupBox29.Controls.Add((Control)this.RX_TO_TX_DELAY_2);
            this.groupBox29.Controls.Add((Control)this.label144);
            this.groupBox29.Controls.Add((Control)this.RX_TO_TX_DELAY_1);
            this.groupBox29.Controls.Add((Control)this.label145);
            this.groupBox29.Controls.Add((Control)this.TX_TO_RX_DELAY_2);
            this.groupBox29.Controls.Add((Control)this.button37);
            this.groupBox29.Location = new Point(691, 4);
            this.groupBox29.Margin = new Padding(4);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Padding = new Padding(4);
            this.groupBox29.Size = new Size(228, 175);
            this.groupBox29.TabIndex = 54;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Delay Control";
            this.label146.AutoSize = true;
            this.label146.Location = new Point(7, 23);
            this.label146.Name = "label146";
            this.label146.Size = new Size(111, 17);
            this.label146.TabIndex = 48;
            this.label146.Text = "TX-RX DELAY 1";
            this.TX_TO_RX_DELAY_1.Location = new Point(131, 25);
            this.TX_TO_RX_DELAY_1.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.TX_TO_RX_DELAY_1.Name = "TX_TO_RX_DELAY_1";
            this.TX_TO_RX_DELAY_1.Size = new Size(92, 22);
            this.TX_TO_RX_DELAY_1.TabIndex = 47;
            this.TX_TO_RX_DELAY_1.ValueChanged += new EventHandler(this.DelayControl);
            this.label143.AutoSize = true;
            this.label143.Location = new Point(7, 107);
            this.label143.Name = "label143";
            this.label143.Size = new Size(111, 17);
            this.label143.TabIndex = 46;
            this.label143.Text = "RX-TX DELAY 2";
            this.RX_TO_TX_DELAY_2.Location = new Point(131, 109);
            this.RX_TO_TX_DELAY_2.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.RX_TO_TX_DELAY_2.Name = "RX_TO_TX_DELAY_2";
            this.RX_TO_TX_DELAY_2.Size = new Size(92, 22);
            this.RX_TO_TX_DELAY_2.TabIndex = 45;
            this.RX_TO_TX_DELAY_2.ValueChanged += new EventHandler(this.DelayControl);
            this.label144.AutoSize = true;
            this.label144.Location = new Point(7, 79);
            this.label144.Name = "label144";
            this.label144.Size = new Size(111, 17);
            this.label144.TabIndex = 44;
            this.label144.Text = "RX-TX DELAY 1";
            this.RX_TO_TX_DELAY_1.Location = new Point(131, 81);
            this.RX_TO_TX_DELAY_1.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.RX_TO_TX_DELAY_1.Name = "RX_TO_TX_DELAY_1";
            this.RX_TO_TX_DELAY_1.Size = new Size(92, 22);
            this.RX_TO_TX_DELAY_1.TabIndex = 43;
            this.RX_TO_TX_DELAY_1.ValueChanged += new EventHandler(this.DelayControl);
            this.label145.AutoSize = true;
            this.label145.Location = new Point(7, 51);
            this.label145.Name = "label145";
            this.label145.Size = new Size(111, 17);
            this.label145.TabIndex = 42;
            this.label145.Text = "TX-RX DELAY 2";
            this.TX_TO_RX_DELAY_2.Location = new Point(131, 53);
            this.TX_TO_RX_DELAY_2.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.TX_TO_RX_DELAY_2.Name = "TX_TO_RX_DELAY_2";
            this.TX_TO_RX_DELAY_2.Size = new Size(92, 22);
            this.TX_TO_RX_DELAY_2.TabIndex = 18;
            this.TX_TO_RX_DELAY_2.ValueChanged += new EventHandler(this.DelayControl);
            this.button37.Location = new Point(42, 138);
            this.button37.Margin = new Padding(4);
            this.button37.Name = "button37";
            this.button37.Size = new Size(160, 28);
            this.button37.TabIndex = 12;
            this.button37.Text = "Write DELAY Values";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new EventHandler(this.Delay_Control_Click);

            this.GPIO_5.AutoSize = true;
            this.GPIO_5.Checked = true;
            this.GPIO_5.CheckState = CheckState.Checked;
            this.GPIO_5.Location = new Point(14, 393);
            this.GPIO_5.Name = "GPIO_5";
            this.GPIO_5.Size = new Size(72, 21);
            this.GPIO_5.TabIndex = 45;
            this.GPIO_5.Text = "GPIO5";
            this.GPIO_5.UseVisualStyleBackColor = true;
            this.GPIO_5.CheckedChanged += new EventHandler(this.GPIO_Click);

            this.GPIO_52.AutoSize = true;
            this.GPIO_52.Checked = true;
            this.GPIO_52.CheckState = CheckState.Checked;
            this.GPIO_52.Location = new Point(14, 393);
            this.GPIO_52.Name = "GPIO_52";
            this.GPIO_52.Size = new Size(72, 21);
            this.GPIO_52.TabIndex = 45;
            this.GPIO_52.Text = "GPIO5";
            this.GPIO_52.UseVisualStyleBackColor = true;
            this.GPIO_52.CheckedChanged += new EventHandler(this.GPIO_Click2);

            this.groupBox17.Controls.Add((Control)this.label103);
            this.groupBox17.Controls.Add((Control)this.NVM_DIN);
            this.groupBox17.Controls.Add((Control)this.label101);
            this.groupBox17.Controls.Add((Control)this.label100);
            this.groupBox17.Controls.Add((Control)this.NVM_ADDR_BYP);
            this.groupBox17.Controls.Add((Control)this.label99);
            this.groupBox17.Controls.Add((Control)this.label94);
            this.groupBox17.Controls.Add((Control)this.NVM_MARGIN);
            this.groupBox17.Controls.Add((Control)this.label95);
            this.groupBox17.Controls.Add((Control)this.label96);
            this.groupBox17.Controls.Add((Control)this.NVM_TEST);
            this.groupBox17.Controls.Add((Control)this.label98);
            this.groupBox17.Controls.Add((Control)this.NVM_PROG_PULSE);
            this.groupBox17.Controls.Add((Control)this.label92);
            this.groupBox17.Controls.Add((Control)this.NVM_CTL_BYP_EN);
            this.groupBox17.Controls.Add((Control)this.label87);
            this.groupBox17.Controls.Add((Control)this.NVM_RD_BYP);
            this.groupBox17.Controls.Add((Control)this.label88);
            this.groupBox17.Controls.Add((Control)this.label90);
            this.groupBox17.Controls.Add((Control)this.NVM_ON_BYP);
            this.groupBox17.Controls.Add((Control)this.label91);
            this.groupBox17.Controls.Add((Control)this.NVM_START_BYP);
            this.groupBox17.Controls.Add((Control)this.label86);
            this.groupBox17.Controls.Add((Control)this.FUSE_BYPASS);
            this.groupBox17.Controls.Add((Control)this.label89);
            this.groupBox17.Controls.Add((Control)this.label93);
            this.groupBox17.Controls.Add((Control)this.NVM_BIT_SEL);
            this.groupBox17.Controls.Add((Control)this.NVM_REREAD);
            this.groupBox17.Controls.Add((Control)this.button20);
            this.groupBox17.Controls.Add((Control)this.label97);
            this.groupBox17.Controls.Add((Control)this.FUSE_CLOCK_CTL);
            this.groupBox17.Location = new Point(4, 17);
            this.groupBox17.Margin = new Padding(4);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new Padding(4);
            this.groupBox17.Size = new Size(355, 354);
            this.groupBox17.TabIndex = 12;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "NVM Control";
            this.label103.AutoSize = true;
            this.label103.Location = new Point(167, 248);
            this.label103.Name = "label103";
            this.label103.Size = new Size(65, 17);
            this.label103.TabIndex = 52;
            this.label103.Text = "NVM DIN";
            this.NVM_DIN.Location = new Point(287, 248);
            this.NVM_DIN.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.NVM_DIN.Name = "NVM_DIN";
            this.NVM_DIN.Size = new Size(58, 22);
            this.NVM_DIN.TabIndex = 51;
            this.NVM_DIN.ValueChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label101.AutoSize = true;
            this.label101.Location = new Point(167, 224);
            this.label101.Name = "label101";
            this.label101.Size = new Size(115, 17);
            this.label101.TabIndex = 49;
            this.label101.Text = "NVM CTRL 040B";
            this.label100.AutoSize = true;
            this.label100.Location = new Point(7, 185);
            this.label100.Name = "label100";
            this.label100.Size = new Size(112, 17);
            this.label100.TabIndex = 48;
            this.label100.Text = "NVM ADDR BYP";
            this.NVM_ADDR_BYP.Location = new Point((int)sbyte.MaxValue, 185);
            this.NVM_ADDR_BYP.Maximum = new Decimal(new int[4]
            {
        15,
        0,
        0,
        0
            });
            this.NVM_ADDR_BYP.Name = "NVM_ADDR_BYP";
            this.NVM_ADDR_BYP.Size = new Size(70, 22);
            this.NVM_ADDR_BYP.TabIndex = 47;
            this.NVM_ADDR_BYP.ValueChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label99.AutoSize = true;
            this.label99.Location = new Point(167, 108);
            this.label99.Name = "label99";
            this.label99.Size = new Size(132, 17);
            this.label99.TabIndex = 46;
            this.label99.Text = "NVM PROG PUSLE";
            this.label94.AutoSize = true;
            this.label94.Location = new Point(167, 84);
            this.label94.Name = "label94";
            this.label94.Size = new Size(96, 17);
            this.label94.TabIndex = 45;
            this.label94.Text = "NVM MARGIN";
            this.NVM_MARGIN.AutoSize = true;
            this.NVM_MARGIN.Location = new Point(304, 85);
            this.NVM_MARGIN.Name = "NVM_MARGIN";
            this.NVM_MARGIN.Size = new Size(18, 17);
            this.NVM_MARGIN.TabIndex = 44;
            this.NVM_MARGIN.UseVisualStyleBackColor = true;
            this.NVM_MARGIN.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label95.AutoSize = true;
            this.label95.Location = new Point(167, 61);
            this.label95.Name = "label95";
            this.label95.Size = new Size(78, 17);
            this.label95.TabIndex = 43;
            this.label95.Text = "NVM TEST";
            this.label96.AutoSize = true;
            this.label96.Location = new Point(167, 130);
            this.label96.Name = "label96";
            this.label96.Size = new Size(94, 17);
            this.label96.TabIndex = 42;
            this.label96.Text = "NVM NIT SEL";
            this.NVM_TEST.AutoSize = true;
            this.NVM_TEST.Location = new Point(304, 62);
            this.NVM_TEST.Name = "NVM_TEST";
            this.NVM_TEST.Size = new Size(18, 17);
            this.NVM_TEST.TabIndex = 41;
            this.NVM_TEST.UseVisualStyleBackColor = true;
            this.NVM_TEST.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label98.AutoSize = true;
            this.label98.Location = new Point(167, 34);
            this.label98.Name = "label98";
            this.label98.Size = new Size(115, 17);
            this.label98.TabIndex = 40;
            this.label98.Text = "NVM CTRL 040A";
            this.NVM_PROG_PULSE.AutoSize = true;
            this.NVM_PROG_PULSE.Location = new Point(304, 108);
            this.NVM_PROG_PULSE.Name = "NVM_PROG_PULSE";
            this.NVM_PROG_PULSE.Size = new Size(18, 17);
            this.NVM_PROG_PULSE.TabIndex = 39;
            this.NVM_PROG_PULSE.UseVisualStyleBackColor = true;
            this.NVM_PROG_PULSE.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label92.AutoSize = true;
            this.label92.Location = new Point(7, 280);
            this.label92.Name = "label92";
            this.label92.Size = new Size(92, 17);
            this.label92.TabIndex = 38;
            this.label92.Text = "NVM BYP EN";
            this.NVM_CTL_BYP_EN.AutoSize = true;
            this.NVM_CTL_BYP_EN.Location = new Point((int)sbyte.MaxValue, 280);
            this.NVM_CTL_BYP_EN.Name = "NVM_CTL_BYP_EN";
            this.NVM_CTL_BYP_EN.Size = new Size(18, 17);
            this.NVM_CTL_BYP_EN.TabIndex = 37;
            this.NVM_CTL_BYP_EN.UseVisualStyleBackColor = true;
            this.NVM_CTL_BYP_EN.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label87.AutoSize = true;
            this.label87.Location = new Point(7, 257);
            this.label87.Name = "label87";
            this.label87.Size = new Size(93, 17);
            this.label87.TabIndex = 36;
            this.label87.Text = "NVM RD BYP";
            this.NVM_RD_BYP.AutoSize = true;
            this.NVM_RD_BYP.Location = new Point((int)sbyte.MaxValue, 257);
            this.NVM_RD_BYP.Name = "NVM_RD_BYP";
            this.NVM_RD_BYP.Size = new Size(18, 17);
            this.NVM_RD_BYP.TabIndex = 35;
            this.NVM_RD_BYP.UseVisualStyleBackColor = true;
            this.NVM_RD_BYP.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label88.AutoSize = true;
            this.label88.Location = new Point(7, 234);
            this.label88.Name = "label88";
            this.label88.Size = new Size(94, 17);
            this.label88.TabIndex = 34;
            this.label88.Text = "NVM ON BYP";
            this.label90.AutoSize = true;
            this.label90.Location = new Point(7, 211);
            this.label90.Name = "label90";
            this.label90.Size = new Size(119, 17);
            this.label90.TabIndex = 33;
            this.label90.Text = "NVM START BYP";
            this.NVM_ON_BYP.AutoSize = true;
            this.NVM_ON_BYP.Location = new Point((int)sbyte.MaxValue, 234);
            this.NVM_ON_BYP.Name = "NVM_ON_BYP";
            this.NVM_ON_BYP.Size = new Size(18, 17);
            this.NVM_ON_BYP.TabIndex = 32;
            this.NVM_ON_BYP.UseVisualStyleBackColor = true;
            this.NVM_ON_BYP.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label91.AutoSize = true;
            this.label91.Location = new Point(5, 158);
            this.label91.Name = "label91";
            this.label91.Size = new Size(114, 17);
            this.label91.TabIndex = 31;
            this.label91.Text = "NVM CTRL 0409";
            this.NVM_START_BYP.AutoSize = true;
            this.NVM_START_BYP.Location = new Point((int)sbyte.MaxValue, 211);
            this.NVM_START_BYP.Name = "NVM_START_BYP";
            this.NVM_START_BYP.Size = new Size(18, 17);
            this.NVM_START_BYP.TabIndex = 30;
            this.NVM_START_BYP.UseVisualStyleBackColor = true;
            this.NVM_START_BYP.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label86.AutoSize = true;
            this.label86.Location = new Point(7, 109);
            this.label86.Name = "label86";
            this.label86.Size = new Size(102, 17);
            this.label86.TabIndex = 29;
            this.label86.Text = "FUSE BYPASS";
            this.FUSE_BYPASS.AutoSize = true;
            this.FUSE_BYPASS.Location = new Point((int)sbyte.MaxValue, 109);
            this.FUSE_BYPASS.Name = "FUSE_BYPASS";
            this.FUSE_BYPASS.Size = new Size(18, 17);
            this.FUSE_BYPASS.TabIndex = 28;
            this.FUSE_BYPASS.UseVisualStyleBackColor = true;
            this.FUSE_BYPASS.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.label89.AutoSize = true;
            this.label89.Location = new Point(7, 86);
            this.label89.Name = "label89";
            this.label89.Size = new Size(90, 17);
            this.label89.TabIndex = 25;
            this.label89.Text = "NVM RERAD";
            this.label93.AutoSize = true;
            this.label93.Location = new Point(7, 63);
            this.label93.Name = "label93";
            this.label93.Size = new Size(114, 17);
            this.label93.TabIndex = 21;
            this.label93.Text = "FUSE CLK CTRL";
            this.NVM_BIT_SEL.Location = new Point(287, 130);
            this.NVM_BIT_SEL.Maximum = new Decimal(new int[4]
            {
        7,
        0,
        0,
        0
            });
            this.NVM_BIT_SEL.Name = "NVM_BIT_SEL";
            this.NVM_BIT_SEL.Size = new Size(58, 22);
            this.NVM_BIT_SEL.TabIndex = 18;
            this.NVM_BIT_SEL.ValueChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.NVM_REREAD.AutoSize = true;
            this.NVM_REREAD.Location = new Point((int)sbyte.MaxValue, 86);
            this.NVM_REREAD.Name = "NVM_REREAD";
            this.NVM_REREAD.Size = new Size(18, 17);
            this.NVM_REREAD.TabIndex = 13;
            this.NVM_REREAD.UseVisualStyleBackColor = true;
            this.NVM_REREAD.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.button20.Location = new Point(8, 318);
            this.button20.Margin = new Padding(4);
            this.button20.Name = "button20";
            this.button20.Size = new Size(160, 28);
            this.button20.TabIndex = 12;
            this.button20.Text = "Write NVM Values";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new EventHandler(this.NVM_Ctrl_Click);
            this.label97.AutoSize = true;
            this.label97.Location = new Point(7, 36);
            this.label97.Name = "label97";
            this.label97.Size = new Size(114, 17);
            this.label97.TabIndex = 1;
            this.label97.Text = "NVM CTRL 0408";
            this.FUSE_CLOCK_CTL.AutoSize = true;
            this.FUSE_CLOCK_CTL.Location = new Point((int)sbyte.MaxValue, 63);
            this.FUSE_CLOCK_CTL.Name = "FUSE_CLOCK_CTL";
            this.FUSE_CLOCK_CTL.Size = new Size(18, 17);
            this.FUSE_CLOCK_CTL.TabIndex = 0;
            this.FUSE_CLOCK_CTL.UseVisualStyleBackColor = true;
            this.FUSE_CLOCK_CTL.CheckStateChanged += new EventHandler(this.NVM_CTRL_CheckedChanged);
            this.groupBox18.Controls.Add((Control)this.label104);
            this.groupBox18.Controls.Add((Control)this.LDO_TRIM_BYP_C);
            this.groupBox18.Controls.Add((Control)this.label102);
            this.groupBox18.Controls.Add((Control)this.LDO_TRIM_BYP_B);
            this.groupBox18.Controls.Add((Control)this.label109);
            this.groupBox18.Controls.Add((Control)this.label115);
            this.groupBox18.Controls.Add((Control)this.LDO_TRIM_BYP_A);
            this.groupBox18.Controls.Add((Control)this.button21);
            this.groupBox18.Location = new Point(367, 17);
            this.groupBox18.Margin = new Padding(4);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Padding = new Padding(4);
            this.groupBox18.Size = new Size(228, 189);
            this.groupBox18.TabIndex = 53;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "NVM Bypass";
            this.label104.AutoSize = true;
            this.label104.Location = new Point(7, 117);
            this.label104.Name = "label104";
            this.label104.Size = new Size(118, 17);
            this.label104.TabIndex = 46;
            this.label104.Text = "LDO TRIM BYP C";
            this.LDO_TRIM_BYP_C.Location = new Point(131, 119);
            this.LDO_TRIM_BYP_C.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.LDO_TRIM_BYP_C.Name = "LDO_TRIM_BYP_C";
            this.LDO_TRIM_BYP_C.Size = new Size(92, 22);
            this.LDO_TRIM_BYP_C.TabIndex = 45;
            this.LDO_TRIM_BYP_C.ValueChanged += new EventHandler(this.NVM_BYP_ValueChanged);
            this.label102.AutoSize = true;
            this.label102.Location = new Point(7, 89);
            this.label102.Name = "label102";
            this.label102.Size = new Size(118, 17);
            this.label102.TabIndex = 44;
            this.label102.Text = "LDO TRIM BYP B";
            this.LDO_TRIM_BYP_B.Location = new Point(131, 91);
            this.LDO_TRIM_BYP_B.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.LDO_TRIM_BYP_B.Name = "LDO_TRIM_BYP_B";
            this.LDO_TRIM_BYP_B.Size = new Size(92, 22);
            this.LDO_TRIM_BYP_B.TabIndex = 43;
            this.LDO_TRIM_BYP_B.ValueChanged += new EventHandler(this.NVM_BYP_ValueChanged);
            this.label109.AutoSize = true;
            this.label109.Location = new Point(7, 61);
            this.label109.Name = "label109";
            this.label109.Size = new Size(118, 17);
            this.label109.TabIndex = 42;
            this.label109.Text = "LDO TRIM BYP A";
            this.label115.AutoSize = true;
            this.label115.Location = new Point(7, 34);
            this.label115.Name = "label115";
            this.label115.Size = new Size(88, 17);
            this.label115.TabIndex = 31;
            this.label115.Text = "NVM Bypass";
            this.LDO_TRIM_BYP_A.Location = new Point(131, 63);
            this.LDO_TRIM_BYP_A.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.LDO_TRIM_BYP_A.Name = "LDO_TRIM_BYP_A";
            this.LDO_TRIM_BYP_A.Size = new Size(92, 22);
            this.LDO_TRIM_BYP_A.TabIndex = 18;
            this.LDO_TRIM_BYP_A.ValueChanged += new EventHandler(this.NVM_BYP_ValueChanged);
            this.button21.Location = new Point(8, 153);
            this.button21.Margin = new Padding(4);
            this.button21.Name = "button21";
            this.button21.Size = new Size(160, 28);
            this.button21.TabIndex = 12;
            this.button21.Text = "Write NVM Values";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new EventHandler(this.NVM_BYP_Click);
            this.groupBox16.Controls.Add((Control)this.label85);
            this.groupBox16.Controls.Add((Control)this.label84);
            this.groupBox16.Controls.Add((Control)this.LDO_TRIM_REG);
            this.groupBox16.Controls.Add((Control)this.LDO_TRIM_SEL);
            this.groupBox16.Controls.Add((Control)this.button18);
            this.groupBox16.Controls.Add((Control)this.button19);
            this.groupBox16.Location = new Point(367, 214);
            this.groupBox16.Margin = new Padding(4);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Padding = new Padding(4);
            this.groupBox16.Size = new Size(228, 130);
            this.groupBox16.TabIndex = 43;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "LDO Trim Control";
            this.label85.AutoSize = true;
            this.label85.Location = new Point(5, 74);
            this.label85.Name = "label85";
            this.label85.Size = new Size(104, 17);
            this.label85.TabIndex = 44;
            this.label85.Text = "LDO TRIM SEL";
            this.label84.AutoSize = true;
            this.label84.Location = new Point(5, 27);
            this.label84.Name = "label84";
            this.label84.Size = new Size(108, 17);
            this.label84.TabIndex = 41;
            this.label84.Text = "LDO TRIM REG";
            this.LDO_TRIM_REG.Location = new Point(8, 48);
            this.LDO_TRIM_REG.Margin = new Padding(4);
            this.LDO_TRIM_REG.Maximum = new Decimal(new int[4]
            {
        (int) sbyte.MaxValue,
        0,
        0,
        0
            });
            this.LDO_TRIM_REG.Name = "LDO_TRIM_REG";
            this.LDO_TRIM_REG.Size = new Size(111, 22);
            this.LDO_TRIM_REG.TabIndex = 15;
            this.LDO_TRIM_REG.ValueChanged += new EventHandler(this.LDO_TRIM_REG_ValueChanged);
            this.LDO_TRIM_SEL.Location = new Point(8, 95);
            this.LDO_TRIM_SEL.Margin = new Padding(4);
            this.LDO_TRIM_SEL.Maximum = new Decimal(new int[4]
            {
        3,
        0,
        0,
        0
            });
            this.LDO_TRIM_SEL.Name = "LDO_TRIM_SEL";
            this.LDO_TRIM_SEL.Size = new Size(111, 22);
            this.LDO_TRIM_SEL.TabIndex = 43;
            this.LDO_TRIM_SEL.ValueChanged += new EventHandler(this.LDO_TRIM_SEL_ValueChanged);
            this.button18.Location = new Point(121, 45);
            this.button18.Margin = new Padding(4);
            this.button18.Name = "button18";
            this.button18.Size = new Size(102, 28);
            this.button18.TabIndex = 13;
            this.button18.Text = "Write Value";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new EventHandler(this.LDO_TRIM_REG_Click);
            this.button19.BackgroundImageLayout = ImageLayout.None;
            this.button19.Location = new Point(121, 90);
            this.button19.Margin = new Padding(4);
            this.button19.Name = "button19";
            this.button19.Size = new Size(102, 28);
            this.button19.TabIndex = 42;
            this.button19.Text = "Write Value";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new EventHandler(this.LDO_TRIM_SEL_Click);
            this.groupBox27.Controls.Add((Control)this.TR_);
            this.groupBox27.Controls.Add((Control)this.ADDR_1);
            this.groupBox27.Controls.Add((Control)this.ADDR_0);
            this.groupBox27.Controls.Add((Control)this.TX_LOAD);
            this.groupBox27.Controls.Add((Control)this.RX_LOAD);
            this.groupBox27.Location = new Point(6, 41);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new Size(241, 223);
            this.groupBox27.TabIndex = 2;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "GPIO Lines from SDP Board";
            this.TR_.AutoSize = true;
            this.TR_.Location = new Point(80, 149);
            this.TR_.Name = "TR_";
            this.TR_.Size = new Size(49, 21);
            this.TR_.TabIndex = 44;
            this.TR_.Text = "TR";
            this.TR_.UseVisualStyleBackColor = true;
            this.TR_.CheckedChanged += new EventHandler(this.GPIO_Click);
            this.ADDR_1.AutoSize = true;
            this.ADDR_1.Location = new Point(80, 122);
            this.ADDR_1.Name = "ADDR_1";
            this.ADDR_1.Size = new Size(81, 21);
            this.ADDR_1.TabIndex = 43;
            this.ADDR_1.Text = "ADDR 1";
            this.ADDR_1.UseVisualStyleBackColor = true;
            this.ADDR_1.CheckedChanged += new EventHandler(this.GPIO_Click);
            this.ADDR_0.AutoSize = true;
            this.ADDR_0.Location = new Point(80, 95);
            this.ADDR_0.Name = "ADDR_0";
            this.ADDR_0.Size = new Size(81, 21);
            this.ADDR_0.TabIndex = 42;
            this.ADDR_0.Text = "ADDR 0";
            this.ADDR_0.UseVisualStyleBackColor = true;
            this.ADDR_0.CheckedChanged += new EventHandler(this.GPIO_Click);
            this.TX_LOAD.AutoSize = true;
            this.TX_LOAD.Location = new Point(80, 68);
            this.TX_LOAD.Name = "TX_LOAD";
            this.TX_LOAD.Size = new Size(90, 21);
            this.TX_LOAD.TabIndex = 41;
            this.TX_LOAD.Text = "TX LOAD";
            this.TX_LOAD.UseVisualStyleBackColor = true;
            this.TX_LOAD.CheckedChanged += new EventHandler(this.GPIO_Click);
            this.RX_LOAD.AutoSize = true;
            this.RX_LOAD.Location = new Point(80, 39);
            this.RX_LOAD.Name = "RX_LOAD";
            this.RX_LOAD.Size = new Size(91, 21);
            this.RX_LOAD.TabIndex = 40;
            this.RX_LOAD.Text = "RX LOAD";
            this.RX_LOAD.UseVisualStyleBackColor = true;
            this.RX_LOAD.CheckedChanged += new EventHandler(this.GPIO_Click);
            this.MISC.Controls.Add((Control)this.groupBox15);
            this.MISC.Controls.Add((Control)this.groupBox14);
            this.MISC.Controls.Add((Control)this.groupBox12);
            this.MISC.Controls.Add((Control)this.groupBox13);
            this.MISC.Controls.Add((Control)this.groupBox11);
            this.MISC.Controls.Add((Control)this.groupBox7);
            this.MISC.Controls.Add((Control)this.groupBox8);
            this.MISC.Location = new Point(4, 25);
            this.MISC.Name = "MISC";
            this.MISC.Padding = new Padding(3);
            this.MISC.Size = new Size(1185, 446);
            this.MISC.TabIndex = 5;
            this.MISC.Text = "MISC";
            this.MISC.ToolTipText = "PA and LNA bias, Detector and ADC control, memory control";
            this.MISC.UseVisualStyleBackColor = true;
            this.groupBox15.Controls.Add((Control)this.label105);
            this.groupBox15.Controls.Add((Control)this.LNA_BIAS_OFF);
            this.groupBox15.Controls.Add((Control)this.label106);
            this.groupBox15.Controls.Add((Control)this.label107);
            this.groupBox15.Controls.Add((Control)this.label108);
            this.groupBox15.Controls.Add((Control)this.CH1PA_BIAS_OFF);
            this.groupBox15.Controls.Add((Control)this.label110);
            this.groupBox15.Controls.Add((Control)this.button23);
            this.groupBox15.Controls.Add((Control)this.CH4PA_BIAS_OFF);
            this.groupBox15.Controls.Add((Control)this.CH2PA_BIAS_OFF);
            this.groupBox15.Controls.Add((Control)this.CH3PA_BIAS_OFF);
            this.groupBox15.Location = new Point(171, 7);
            this.groupBox15.Margin = new Padding(4);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new Padding(4);
            this.groupBox15.Size = new Size(156, 432);
            this.groupBox15.TabIndex = 22;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Bias OFF";
            this.label105.AutoSize = true;
            this.label105.Location = new Point(8, 273);
            this.label105.Margin = new Padding(4, 0, 4, 0);
            this.label105.Name = "label105";
            this.label105.Size = new Size(66, 17);
            this.label105.TabIndex = 21;
            this.label105.Text = "LNA Bias";
            this.LNA_BIAS_OFF.Location = new Point(8, 294);
            this.LNA_BIAS_OFF.Margin = new Padding(4);
            this.LNA_BIAS_OFF.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.LNA_BIAS_OFF.Name = "LNA_BIAS_OFF";
            this.LNA_BIAS_OFF.Size = new Size(84, 22);
            this.LNA_BIAS_OFF.TabIndex = 20;
            this.LNA_BIAS_OFF.ValueChanged += new EventHandler(this.Bias_OFF_ValueChanged);
            this.label106.AutoSize = true;
            this.label106.Location = new Point(8, 212);
            this.label106.Margin = new Padding(4, 0, 4, 0);
            this.label106.Name = "label106";
            this.label106.Size = new Size(88, 17);
            this.label106.TabIndex = 19;
            this.label106.Text = "CH4 PA Bias";
            this.label107.AutoSize = true;
            this.label107.Location = new Point(8, 27);
            this.label107.Margin = new Padding(4, 0, 4, 0);
            this.label107.Name = "label107";
            this.label107.Size = new Size(88, 17);
            this.label107.TabIndex = 12;
            this.label107.Text = "CH1 PA Bias";
            this.label108.AutoSize = true;
            this.label108.Location = new Point(8, 150);
            this.label108.Margin = new Padding(4, 0, 4, 0);
            this.label108.Name = "label108";
            this.label108.Size = new Size(88, 17);
            this.label108.TabIndex = 18;
            this.label108.Text = "CH3 PA Bias";
            this.CH1PA_BIAS_OFF.Location = new Point(8, 50);
            this.CH1PA_BIAS_OFF.Margin = new Padding(4);
            this.CH1PA_BIAS_OFF.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH1PA_BIAS_OFF.Name = "CH1PA_BIAS_OFF";
            this.CH1PA_BIAS_OFF.Size = new Size(84, 22);
            this.CH1PA_BIAS_OFF.TabIndex = 11;
            this.CH1PA_BIAS_OFF.ValueChanged += new EventHandler(this.Bias_OFF_ValueChanged);
            this.label110.AutoSize = true;
            this.label110.Location = new Point(8, 89);
            this.label110.Margin = new Padding(4, 0, 4, 0);
            this.label110.Name = "label110";
            this.label110.Size = new Size(88, 17);
            this.label110.TabIndex = 17;
            this.label110.Text = "CH2 PA Bias";
            this.button23.Location = new Point(8, 396);
            this.button23.Margin = new Padding(4);
            this.button23.Name = "button23";
            this.button23.Size = new Size(137, 28);
            this.button23.TabIndex = 13;
            this.button23.Text = "Write Bias Values";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new EventHandler(this.Bias_OFF_Click_1);
            this.CH4PA_BIAS_OFF.Location = new Point(8, 232);
            this.CH4PA_BIAS_OFF.Margin = new Padding(4);
            this.CH4PA_BIAS_OFF.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH4PA_BIAS_OFF.Name = "CH4PA_BIAS_OFF";
            this.CH4PA_BIAS_OFF.Size = new Size(84, 22);
            this.CH4PA_BIAS_OFF.TabIndex = 16;
            this.CH4PA_BIAS_OFF.ValueChanged += new EventHandler(this.Bias_OFF_ValueChanged);
            this.CH2PA_BIAS_OFF.Location = new Point(8, 109);
            this.CH2PA_BIAS_OFF.Margin = new Padding(4);
            this.CH2PA_BIAS_OFF.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH2PA_BIAS_OFF.Name = "CH2PA_BIAS_OFF";
            this.CH2PA_BIAS_OFF.Size = new Size(84, 22);
            this.CH2PA_BIAS_OFF.TabIndex = 14;
            this.CH2PA_BIAS_OFF.ValueChanged += new EventHandler(this.Bias_OFF_ValueChanged);
            this.CH3PA_BIAS_OFF.Location = new Point(8, 170);
            this.CH3PA_BIAS_OFF.Margin = new Padding(4);
            this.CH3PA_BIAS_OFF.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH3PA_BIAS_OFF.Name = "CH3PA_BIAS_OFF";
            this.CH3PA_BIAS_OFF.Size = new Size(84, 22);
            this.CH3PA_BIAS_OFF.TabIndex = 15;
            this.CH3PA_BIAS_OFF.ValueChanged += new EventHandler(this.Bias_OFF_ValueChanged);
            this.groupBox14.Controls.Add((Control)this.BIAS_RAM_BYPASS);
            this.groupBox14.Controls.Add((Control)this.RX_CHX_RAM_BYPASS);
            this.groupBox14.Controls.Add((Control)this.TX_CHX_RAM_BYPASS);
            this.groupBox14.Controls.Add((Control)this.RX_BEAM_STEP_EN);
            this.groupBox14.Controls.Add((Control)this.TX_BEAM_STEP_EN);
            this.groupBox14.Controls.Add((Control)this.Test_Modes_Panel2);
            this.groupBox14.Controls.Add((Control)this.button15);
            this.groupBox14.Controls.Add((Control)this.BEAM_RAM_BYPASS);
            this.groupBox14.Controls.Add((Control)this.SCAN_MODE_EN);
            this.groupBox14.Location = new Point(919, 7);
            this.groupBox14.Margin = new Padding(4);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new Padding(4);
            this.groupBox14.Size = new Size(260, 250);
            this.groupBox14.TabIndex = 42;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Memory Control";
            this.BIAS_RAM_BYPASS.AutoSize = true;
            this.BIAS_RAM_BYPASS.Location = new Point(15, 78);
            this.BIAS_RAM_BYPASS.Name = "BIAS_RAM_BYPASS";
            this.BIAS_RAM_BYPASS.Size = new Size(152, 21);
            this.BIAS_RAM_BYPASS.TabIndex = 21;
            this.BIAS_RAM_BYPASS.Text = "BIAS RAM BYPASS";
            this.BIAS_RAM_BYPASS.UseVisualStyleBackColor = true;
            this.RX_CHX_RAM_BYPASS.AutoSize = true;
            this.RX_CHX_RAM_BYPASS.Location = new Point(15, 186);
            this.RX_CHX_RAM_BYPASS.Name = "RX_CHX_RAM_BYPASS";
            this.RX_CHX_RAM_BYPASS.Size = new Size(173, 21);
            this.RX_CHX_RAM_BYPASS.TabIndex = 20;
            this.RX_CHX_RAM_BYPASS.Text = "RX CHX RAM BYPASS";
            this.RX_CHX_RAM_BYPASS.UseVisualStyleBackColor = true;
            this.TX_CHX_RAM_BYPASS.AutoSize = true;
            this.TX_CHX_RAM_BYPASS.Location = new Point(15, 159);
            this.TX_CHX_RAM_BYPASS.Name = "TX_CHX_RAM_BYPASS";
            this.TX_CHX_RAM_BYPASS.Size = new Size(172, 21);
            this.TX_CHX_RAM_BYPASS.TabIndex = 19;
            this.TX_CHX_RAM_BYPASS.Text = "TX CHX RAM BYPASS";
            this.TX_CHX_RAM_BYPASS.UseVisualStyleBackColor = true;
            this.RX_BEAM_STEP_EN.AutoSize = true;
            this.RX_BEAM_STEP_EN.Location = new Point(15, 132);
            this.RX_BEAM_STEP_EN.Name = "RX_BEAM_STEP_EN";
            this.RX_BEAM_STEP_EN.Size = new Size(154, 21);
            this.RX_BEAM_STEP_EN.TabIndex = 18;
            this.RX_BEAM_STEP_EN.Text = "RX BEAM STEP EN";
            this.RX_BEAM_STEP_EN.UseVisualStyleBackColor = true;
            this.TX_BEAM_STEP_EN.AutoSize = true;
            this.TX_BEAM_STEP_EN.Location = new Point(15, 105);
            this.TX_BEAM_STEP_EN.Name = "TX_BEAM_STEP_EN";
            this.TX_BEAM_STEP_EN.Size = new Size(153, 21);
            this.TX_BEAM_STEP_EN.TabIndex = 17;
            this.TX_BEAM_STEP_EN.Text = "TX BEAM STEP EN";
            this.TX_BEAM_STEP_EN.UseVisualStyleBackColor = true;
            this.Test_Modes_Panel2.Location = new Point(15, 18);
            this.Test_Modes_Panel2.Name = "Test_Modes_Panel2";
            this.Test_Modes_Panel2.Size = new Size(188, 28);
            this.Test_Modes_Panel2.TabIndex = 16;
            this.button15.Location = new Point(74, 214);
            this.button15.Margin = new Padding(4);
            this.button15.Name = "button15";
            this.button15.Size = new Size(102, 28);
            this.button15.TabIndex = 13;
            this.button15.Text = "Write Value";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new EventHandler(this.Mem_Ctrl_Click);
            this.BEAM_RAM_BYPASS.AutoSize = true;
            this.BEAM_RAM_BYPASS.Location = new Point(15, 51);
            this.BEAM_RAM_BYPASS.Name = "BEAM_RAM_BYPASS";
            this.BEAM_RAM_BYPASS.Size = new Size(160, 21);
            this.BEAM_RAM_BYPASS.TabIndex = 15;
            this.BEAM_RAM_BYPASS.Text = "BEAM RAM BYPASS";
            this.BEAM_RAM_BYPASS.UseVisualStyleBackColor = true;
            this.BEAM_RAM_BYPASS.CheckStateChanged += new EventHandler(this.MEM_CTRL_CheckedChanged);
            this.SCAN_MODE_EN.AutoSize = true;
            this.SCAN_MODE_EN.Location = new Point(15, 23);
            this.SCAN_MODE_EN.Name = "SCAN_MODE_EN";
            this.SCAN_MODE_EN.Size = new Size(135, 21);
            this.SCAN_MODE_EN.TabIndex = 14;
            this.SCAN_MODE_EN.Text = "SCAN MODE EN";
            this.SCAN_MODE_EN.UseVisualStyleBackColor = true;
            this.SCAN_MODE_EN.CheckStateChanged += new EventHandler(this.MEM_CTRL_CheckedChanged);
            this.groupBox12.Controls.Add((Control)this.label59);
            this.groupBox12.Controls.Add((Control)this.button13);
            this.groupBox12.Controls.Add((Control)this.label69);
            this.groupBox12.Controls.Add((Control)this.DRV_GAIN);
            this.groupBox12.Location = new Point(685, 7);
            this.groupBox12.Margin = new Padding(4);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new Padding(4);
            this.groupBox12.Size = new Size(226, 106);
            this.groupBox12.TabIndex = 41;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Bias Current";
            this.label59.AutoSize = true;
            this.label59.Location = new Point(8, 44);
            this.label59.Name = "label59";
            this.label59.Size = new Size(74, 17);
            this.label59.TabIndex = 25;
            this.label59.Text = "DRV GAIN";
            this.button13.Location = new Point(8, 68);
            this.button13.Margin = new Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new Size(146, 28);
            this.button13.TabIndex = 12;
            this.button13.Text = "Write Bias Values";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new EventHandler(this.Bias_Current_Click);
            this.label69.AutoSize = true;
            this.label69.Location = new Point(7, 27);
            this.label69.Name = "label69";
            this.label69.Size = new Size(98, 17);
            this.label69.TabIndex = 1;
            this.label69.Text = "Bias Current 1";
            this.DRV_GAIN.AutoSize = true;
            this.DRV_GAIN.Location = new Point(88, 44);
            this.DRV_GAIN.Name = "DRV_GAIN";
            this.DRV_GAIN.Size = new Size(18, 17);
            this.DRV_GAIN.TabIndex = 0;
            this.DRV_GAIN.UseVisualStyleBackColor = true;
            this.DRV_GAIN.CheckStateChanged += new EventHandler(this.BIAS_CURRENT_ValueChanged);
            this.groupBox13.Controls.Add((Control)this.button14);
            this.groupBox13.Controls.Add((Control)this.button10);
            this.groupBox13.Location = new Point(919, 265);
            this.groupBox13.Margin = new Padding(4);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new Padding(4);
            this.groupBox13.Size = new Size(240, 79);
            this.groupBox13.TabIndex = 25;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Over Ride";
            this.button14.Location = new Point(129, 35);
            this.button14.Name = "button14";
            this.button14.Size = new Size(100, 27);
            this.button14.TabIndex = 17;
            this.button14.Text = "LDRX OVR";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new EventHandler(this.OverrideRX_Click);
            this.button10.Location = new Point(15, 35);
            this.button10.Name = "button10";
            this.button10.Size = new Size(100, 27);
            this.button10.TabIndex = 16;
            this.button10.Text = "LDTX OVR";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new EventHandler(this.OverrideTX_Click);
            this.groupBox11.Controls.Add((Control)this.MUX_SEL);
            this.groupBox11.Controls.Add((Control)this.textBox4);
            this.groupBox11.Controls.Add((Control)this.button7);
            this.groupBox11.Controls.Add((Control)this.label127);
            this.groupBox11.Controls.Add((Control)this.ADC_EOC);
            this.groupBox11.Controls.Add((Control)this.label125);
            this.groupBox11.Controls.Add((Control)this.label57);
            this.groupBox11.Controls.Add((Control)this.label56);
            this.groupBox11.Controls.Add((Control)this.ST_CONV);
            this.groupBox11.Controls.Add((Control)this.CLK_EN);
            this.groupBox11.Controls.Add((Control)this.ADC_EN);
            this.groupBox11.Controls.Add((Control)this.ADC_CLKFREQ_SEL);
            this.groupBox11.Controls.Add((Control)this.button12);
            this.groupBox11.Location = new Point(686, 121);
            this.groupBox11.Margin = new Padding(4);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new Padding(4);
            this.groupBox11.Size = new Size(225, 318);
            this.groupBox11.TabIndex = 40;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "ADC";
            this.MUX_SEL.DropDownStyle = ComboBoxStyle.DropDownList;
            this.MUX_SEL.FormattingEnabled = true;
            this.MUX_SEL.Items.AddRange(new object[5]
            {
        (object) "Temp",
        (object) "Det 1",
        (object) "Det 2",
        (object) "Det 3",
        (object) "Det 4"
            });
            this.MUX_SEL.Location = new Point(7, 144);
            this.MUX_SEL.Name = "MUX_SEL";
            this.MUX_SEL.Size = new Size(113, 24);
            this.MUX_SEL.TabIndex = 44;
            this.MUX_SEL.SelectedIndexChanged += new EventHandler(this.ADC_CheckedChanged);
            this.textBox4.Location = new Point(7, 253);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(136, 22);
            this.textBox4.TabIndex = 48;
            this.button7.Location = new Point(7, 282);
            this.button7.Margin = new Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new Size(130, 28);
            this.button7.TabIndex = 47;
            this.button7.Text = "Read ADC Word";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new EventHandler(this.ADC_convertion_Click);
            this.label127.AutoSize = true;
            this.label127.Location = new Point(98, 226);
            this.label127.Name = "label127";
            this.label127.Size = new Size(69, 17);
            this.label127.TabIndex = 46;
            this.label127.Text = "ADC EOC";
            this.ADC_EOC.BackColor = Color.Red;
            this.ADC_EOC.Location = new Point(173, 223);
            this.ADC_EOC.Name = "ADC_EOC";
            this.ADC_EOC.Size = new Size(24, 22);
            this.ADC_EOC.TabIndex = 45;
            this.label125.AutoSize = true;
            this.label125.Location = new Point(7, 228);
            this.label125.Name = "label125";
            this.label125.Size = new Size(74, 17);
            this.label125.TabIndex = 44;
            this.label125.Text = "ADC Word";
            this.label57.AutoSize = true;
            this.label57.Location = new Point(8, 124);
            this.label57.Margin = new Padding(4, 0, 4, 0);
            this.label57.Name = "label57";
            this.label57.Size = new Size(68, 17);
            this.label57.TabIndex = 41;
            this.label57.Text = "MUX SEL";
            this.label56.AutoSize = true;
            this.label56.Location = new Point(8, 19);
            this.label56.Margin = new Padding(4, 0, 4, 0);
            this.label56.Name = "label56";
            this.label56.Size = new Size(79, 17);
            this.label56.TabIndex = 39;
            this.label56.Text = "ADC Signal";
            this.ST_CONV.AutoSize = true;
            this.ST_CONV.Location = new Point(9, 202);
            this.ST_CONV.Name = "ST_CONV";
            this.ST_CONV.Size = new Size(91, 21);
            this.ST_CONV.TabIndex = 37;
            this.ST_CONV.Text = "ST CONV";
            this.ST_CONV.UseVisualStyleBackColor = true;
            this.ST_CONV.CheckStateChanged += new EventHandler(this.ADC_CheckedChanged);
            this.CLK_EN.AutoSize = true;
            this.CLK_EN.Location = new Point(7, 103);
            this.CLK_EN.Name = "CLK_EN";
            this.CLK_EN.Size = new Size(79, 21);
            this.CLK_EN.TabIndex = 36;
            this.CLK_EN.Text = "CLK EN";
            this.CLK_EN.UseVisualStyleBackColor = true;
            this.CLK_EN.CheckStateChanged += new EventHandler(this.ADC_CheckedChanged);
            this.ADC_EN.AutoSize = true;
            this.ADC_EN.Location = new Point(7, 76);
            this.ADC_EN.Name = "ADC_EN";
            this.ADC_EN.Size = new Size(81, 21);
            this.ADC_EN.TabIndex = 35;
            this.ADC_EN.Text = "ADC EN";
            this.ADC_EN.UseVisualStyleBackColor = true;
            this.ADC_EN.CheckStateChanged += new EventHandler(this.ADC_CheckedChanged);
            this.ADC_CLKFREQ_SEL.AutoSize = true;
            this.ADC_CLKFREQ_SEL.Location = new Point(7, 49);
            this.ADC_CLKFREQ_SEL.Name = "ADC_CLKFREQ_SEL";
            this.ADC_CLKFREQ_SEL.Size = new Size(160, 21);
            this.ADC_CLKFREQ_SEL.TabIndex = 34;
            this.ADC_CLKFREQ_SEL.Text = "ADC CLKFREQ SEL ";
            this.ADC_CLKFREQ_SEL.UseVisualStyleBackColor = true;
            this.ADC_CLKFREQ_SEL.CheckStateChanged += new EventHandler(this.ADC_CheckedChanged);
            this.button12.Location = new Point(7, 170);
            this.button12.Margin = new Padding(4);
            this.button12.Name = "button12";
            this.button12.Size = new Size(130, 28);
            this.button12.TabIndex = 13;
            this.button12.Text = "Write ADC Values";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new EventHandler(this.ADC_button_Click);
            this.groupBox7.Controls.Add((Control)this.label54);
            this.groupBox7.Controls.Add((Control)this.button9);
            this.groupBox7.Controls.Add((Control)this.CH4_DET_EN);
            this.groupBox7.Controls.Add((Control)this.CH3_DET_EN);
            this.groupBox7.Controls.Add((Control)this.CH2_DET_EN);
            this.groupBox7.Controls.Add((Control)this.CH1_DET_EN);
            this.groupBox7.Controls.Add((Control)this.LNA_BIAS_OUT_EN);
            this.groupBox7.Controls.Add((Control)this.BIAS_CTRL);
            this.groupBox7.Location = new Point(335, 7);
            this.groupBox7.Margin = new Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new Padding(4);
            this.groupBox7.Size = new Size(164, 432);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Enables";
            this.label54.AutoSize = true;
            this.label54.Location = new Point(8, 27);
            this.label54.Margin = new Padding(4, 0, 4, 0);
            this.label54.Name = "label54";
            this.label54.Size = new Size(84, 17);
            this.label54.TabIndex = 43;
            this.label54.Text = "Misc Enable";
            this.button9.Location = new Point(8, 396);
            this.button9.Margin = new Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new Size(148, 28);
            this.button9.TabIndex = 42;
            this.button9.Text = "Write Enable Values";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new EventHandler(this.MISC_EN_Click);
            this.CH4_DET_EN.AutoSize = true;
            this.CH4_DET_EN.Location = new Point(7, 182);
            this.CH4_DET_EN.Name = "CH4_DET_EN";
            this.CH4_DET_EN.Size = new Size(112, 21);
            this.CH4_DET_EN.TabIndex = 41;
            this.CH4_DET_EN.Text = "CH4 DET EN";
            this.CH4_DET_EN.UseVisualStyleBackColor = true;
            this.CH4_DET_EN.CheckStateChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.CH3_DET_EN.AutoSize = true;
            this.CH3_DET_EN.Location = new Point(7, 155);
            this.CH3_DET_EN.Name = "CH3_DET_EN";
            this.CH3_DET_EN.Size = new Size(112, 21);
            this.CH3_DET_EN.TabIndex = 40;
            this.CH3_DET_EN.Text = "CH3 DET EN";
            this.CH3_DET_EN.UseVisualStyleBackColor = true;
            this.CH3_DET_EN.CheckedChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.CH2_DET_EN.AutoSize = true;
            this.CH2_DET_EN.Location = new Point(7, 128);
            this.CH2_DET_EN.Name = "CH2_DET_EN";
            this.CH2_DET_EN.Size = new Size(112, 21);
            this.CH2_DET_EN.TabIndex = 39;
            this.CH2_DET_EN.Text = "CH2 DET EN";
            this.CH2_DET_EN.UseVisualStyleBackColor = true;
            this.CH2_DET_EN.CheckedChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.CH1_DET_EN.AutoSize = true;
            this.CH1_DET_EN.Location = new Point(7, 101);
            this.CH1_DET_EN.Name = "CH1_DET_EN";
            this.CH1_DET_EN.Size = new Size(112, 21);
            this.CH1_DET_EN.TabIndex = 38;
            this.CH1_DET_EN.Text = "CH1 DET EN";
            this.CH1_DET_EN.UseVisualStyleBackColor = true;
            this.CH1_DET_EN.CheckedChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.LNA_BIAS_OUT_EN.AutoSize = true;
            this.LNA_BIAS_OUT_EN.Location = new Point(7, 74);
            this.LNA_BIAS_OUT_EN.Name = "LNA_BIAS_OUT_EN";
            this.LNA_BIAS_OUT_EN.Size = new Size(148, 21);
            this.LNA_BIAS_OUT_EN.TabIndex = 37;
            this.LNA_BIAS_OUT_EN.Text = "LNA BIAS OUT EN";
            this.LNA_BIAS_OUT_EN.UseVisualStyleBackColor = true;
            this.LNA_BIAS_OUT_EN.CheckStateChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.BIAS_CTRL.AutoSize = true;
            this.BIAS_CTRL.Location = new Point(7, 47);
            this.BIAS_CTRL.Name = "BIAS_CTRL";
            this.BIAS_CTRL.Size = new Size(100, 21);
            this.BIAS_CTRL.TabIndex = 36;
            this.BIAS_CTRL.Text = "BIAS CTRL";
            this.BIAS_CTRL.UseVisualStyleBackColor = true;
            this.BIAS_CTRL.CheckStateChanged += new EventHandler(this.MISC_EN_CheckedChanged);
            this.groupBox8.Controls.Add((Control)this.label51);
            this.groupBox8.Controls.Add((Control)this.LNA_BIAS_ON);
            this.groupBox8.Controls.Add((Control)this.label47);
            this.groupBox8.Controls.Add((Control)this.label50);
            this.groupBox8.Controls.Add((Control)this.label48);
            this.groupBox8.Controls.Add((Control)this.CH1PA_BIAS_ON);
            this.groupBox8.Controls.Add((Control)this.label49);
            this.groupBox8.Controls.Add((Control)this.button6);
            this.groupBox8.Controls.Add((Control)this.CH4PA_BIAS_ON);
            this.groupBox8.Controls.Add((Control)this.CH2PA_BIAS_ON);
            this.groupBox8.Controls.Add((Control)this.CH3PA_BIAS_ON);
            this.groupBox8.Location = new Point(7, 7);
            this.groupBox8.Margin = new Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new Padding(4);
            this.groupBox8.Size = new Size(156, 432);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Bias ON";
            this.label51.AutoSize = true;
            this.label51.Location = new Point(8, 273);
            this.label51.Margin = new Padding(4, 0, 4, 0);
            this.label51.Name = "label51";
            this.label51.Size = new Size(66, 17);
            this.label51.TabIndex = 21;
            this.label51.Text = "LNA Bias";
            this.LNA_BIAS_ON.Location = new Point(8, 294);
            this.LNA_BIAS_ON.Margin = new Padding(4);
            this.LNA_BIAS_ON.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.LNA_BIAS_ON.Name = "LNA_BIAS_ON";
            this.LNA_BIAS_ON.Size = new Size(84, 22);
            this.LNA_BIAS_ON.TabIndex = 20;
            this.LNA_BIAS_ON.ValueChanged += new EventHandler(this.Bias_ON_ValueChanged);
            this.label47.AutoSize = true;
            this.label47.Location = new Point(8, 212);
            this.label47.Margin = new Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new Size(88, 17);
            this.label47.TabIndex = 19;
            this.label47.Text = "CH4 PA Bias";
            this.label50.AutoSize = true;
            this.label50.Location = new Point(8, 27);
            this.label50.Margin = new Padding(4, 0, 4, 0);
            this.label50.Name = "label50";
            this.label50.Size = new Size(88, 17);
            this.label50.TabIndex = 12;
            this.label50.Text = "CH1 PA Bias";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(8, 150);
            this.label48.Margin = new Padding(4, 0, 4, 0);
            this.label48.Name = "label48";
            this.label48.Size = new Size(88, 17);
            this.label48.TabIndex = 18;
            this.label48.Text = "CH3 PA Bias";
            this.CH1PA_BIAS_ON.Location = new Point(8, 50);
            this.CH1PA_BIAS_ON.Margin = new Padding(4);
            this.CH1PA_BIAS_ON.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH1PA_BIAS_ON.Name = "CH1PA_BIAS_ON";
            this.CH1PA_BIAS_ON.Size = new Size(84, 22);
            this.CH1PA_BIAS_ON.TabIndex = 11;
            this.CH1PA_BIAS_ON.ValueChanged += new EventHandler(this.Bias_ON_ValueChanged);
            this.label49.AutoSize = true;
            this.label49.Location = new Point(8, 89);
            this.label49.Margin = new Padding(4, 0, 4, 0);
            this.label49.Name = "label49";
            this.label49.Size = new Size(88, 17);
            this.label49.TabIndex = 17;
            this.label49.Text = "CH2 PA Bias";
            this.button6.Location = new Point(8, 396);
            this.button6.Margin = new Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new Size(137, 28);
            this.button6.TabIndex = 13;
            this.button6.Text = "Write Bias Values";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new EventHandler(this.Bias_ON_Click);
            this.CH4PA_BIAS_ON.Location = new Point(8, 232);
            this.CH4PA_BIAS_ON.Margin = new Padding(4);
            this.CH4PA_BIAS_ON.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH4PA_BIAS_ON.Name = "CH4PA_BIAS_ON";
            this.CH4PA_BIAS_ON.Size = new Size(84, 22);
            this.CH4PA_BIAS_ON.TabIndex = 16;
            this.CH4PA_BIAS_ON.ValueChanged += new EventHandler(this.Bias_ON_ValueChanged);
            this.CH2PA_BIAS_ON.Location = new Point(8, 109);
            this.CH2PA_BIAS_ON.Margin = new Padding(4);
            this.CH2PA_BIAS_ON.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH2PA_BIAS_ON.Name = "CH2PA_BIAS_ON";
            this.CH2PA_BIAS_ON.Size = new Size(84, 22);
            this.CH2PA_BIAS_ON.TabIndex = 14;
            this.CH2PA_BIAS_ON.ValueChanged += new EventHandler(this.Bias_ON_ValueChanged);
            this.CH3PA_BIAS_ON.Location = new Point(8, 170);
            this.CH3PA_BIAS_ON.Margin = new Padding(4);
            this.CH3PA_BIAS_ON.Maximum = new Decimal(new int[4]
            {
        (int) byte.MaxValue,
        0,
        0,
        0
            });
            this.CH3PA_BIAS_ON.Name = "CH3PA_BIAS_ON";
            this.CH3PA_BIAS_ON.Size = new Size(84, 22);
            this.CH3PA_BIAS_ON.TabIndex = 15;
            this.CH3PA_BIAS_ON.ValueChanged += new EventHandler(this.Bias_ON_ValueChanged);
            this.BeamSequencer.Controls.Add((Control)this.panel2);
            this.BeamSequencer.Controls.Add((Control)this.panel11);
            this.BeamSequencer.Location = new Point(4, 25);
            this.BeamSequencer.Name = "BeamSequencer";
            this.BeamSequencer.Size = new Size(1185, 446);
            this.BeamSequencer.TabIndex = 16;
            this.BeamSequencer.Text = "Beam Sequencer";
            this.BeamSequencer.ToolTipText = "Load and play vector files which drive up to four boards simultaneously";
            this.BeamSequencer.UseVisualStyleBackColor = true;
            this.panel2.Controls.Add((Control)this.PolarPlot);
            this.panel2.Controls.Add((Control)this.label161);
            this.panel2.Controls.Add((Control)this.button41);
            this.panel2.Controls.Add((Control)this.label162);
            this.panel2.Controls.Add((Control)this.DemoGain1);
            this.panel2.Controls.Add((Control)this.DemoPhase4);
            this.panel2.Controls.Add((Control)this.DemoPhase1);
            this.panel2.Controls.Add((Control)this.DemoGain4);
            this.panel2.Controls.Add((Control)this.label155);
            this.panel2.Controls.Add((Control)this.label156);
            this.panel2.Controls.Add((Control)this.label159);
            this.panel2.Controls.Add((Control)this.DemoGain2);
            this.panel2.Controls.Add((Control)this.label160);
            this.panel2.Controls.Add((Control)this.DemoPhase2);
            this.panel2.Controls.Add((Control)this.DemoPhase3);
            this.panel2.Controls.Add((Control)this.label158);
            this.panel2.Controls.Add((Control)this.DemoGain3);
            this.panel2.Controls.Add((Control)this.label157);
            this.panel2.Location = new Point(981, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(129, 73);
            this.panel2.TabIndex = 36;
            this.PolarPlot.Image = (Image)componentResourceManager.GetObject("PolarPlot.Image");
            this.PolarPlot.InitialImage = (Image)componentResourceManager.GetObject("PolarPlot.InitialImage");
            this.PolarPlot.Location = new Point(170, 0);
            this.PolarPlot.Name = "PolarPlot";
            this.PolarPlot.Size = new Size(557, 478);
            this.PolarPlot.SizeMode = PictureBoxSizeMode.CenterImage;
            this.PolarPlot.TabIndex = 18;
            this.PolarPlot.TabStop = false;
            this.label161.AutoSize = true;
            this.label161.Location = new Point(559, 283);
            this.label161.Name = "label161";
            this.label161.Size = new Size(48, 17);
            this.label161.TabIndex = 35;
            this.label161.Text = "Phase";
            this.button41.Location = new Point(572, 360);
            this.button41.Name = "button41";
            this.button41.Size = new Size(182, 48);
            this.button41.TabIndex = 19;
            this.button41.Text = "Click";
            this.button41.UseVisualStyleBackColor = true;
            this.button41.Click += new EventHandler(this.PolarPlotDemo);
            this.label162.AutoSize = true;
            this.label162.Location = new Point(569, 257);
            this.label162.Name = "label162";
            this.label162.Size = new Size(38, 17);
            this.label162.TabIndex = 34;
            this.label162.Text = "Gain";
            this.DemoGain1.FormattingEnabled = true;
            this.DemoGain1.Location = new Point(613, 31);
            this.DemoGain1.Name = "DemoGain1";
            this.DemoGain1.Size = new Size(141, 24);
            this.DemoGain1.TabIndex = 20;
            this.DemoGain1.Text = "0";
            this.DemoPhase4.FormattingEnabled = true;
            this.DemoPhase4.Location = new Point(613, 280);
            this.DemoPhase4.Name = "DemoPhase4";
            this.DemoPhase4.Size = new Size(141, 24);
            this.DemoPhase4.TabIndex = 33;
            this.DemoPhase4.Text = "0";
            this.DemoPhase1.FormattingEnabled = true;
            this.DemoPhase1.Location = new Point(613, 61);
            this.DemoPhase1.Name = "DemoPhase1";
            this.DemoPhase1.Size = new Size(141, 24);
            this.DemoPhase1.TabIndex = 21;
            this.DemoPhase1.Text = "0";
            this.DemoGain4.FormattingEnabled = true;
            this.DemoGain4.Location = new Point(613, 250);
            this.DemoGain4.Name = "DemoGain4";
            this.DemoGain4.Size = new Size(141, 24);
            this.DemoGain4.TabIndex = 32;
            this.DemoGain4.Text = "0";
            this.label155.AutoSize = true;
            this.label155.Location = new Point(569, 38);
            this.label155.Name = "label155";
            this.label155.Size = new Size(38, 17);
            this.label155.TabIndex = 22;
            this.label155.Text = "Gain";
            this.label156.AutoSize = true;
            this.label156.Location = new Point(559, 64);
            this.label156.Name = "label156";
            this.label156.Size = new Size(48, 17);
            this.label156.TabIndex = 23;
            this.label156.Text = "Phase";
            this.label159.AutoSize = true;
            this.label159.Location = new Point(559, 212);
            this.label159.Name = "label159";
            this.label159.Size = new Size(48, 17);
            this.label159.TabIndex = 31;
            this.label159.Text = "Phase";
            this.DemoGain2.FormattingEnabled = true;
            this.DemoGain2.Location = new Point(613, 105);
            this.DemoGain2.Name = "DemoGain2";
            this.DemoGain2.Size = new Size(141, 24);
            this.DemoGain2.TabIndex = 24;
            this.DemoGain2.Text = "0";
            this.label160.AutoSize = true;
            this.label160.Location = new Point(569, 186);
            this.label160.Name = "label160";
            this.label160.Size = new Size(38, 17);
            this.label160.TabIndex = 30;
            this.label160.Text = "Gain";
            this.DemoPhase2.FormattingEnabled = true;
            this.DemoPhase2.Location = new Point(613, 135);
            this.DemoPhase2.Name = "DemoPhase2";
            this.DemoPhase2.Size = new Size(141, 24);
            this.DemoPhase2.TabIndex = 25;
            this.DemoPhase2.Text = "0";
            this.DemoPhase3.FormattingEnabled = true;
            this.DemoPhase3.Location = new Point(613, 209);
            this.DemoPhase3.Name = "DemoPhase3";
            this.DemoPhase3.Size = new Size(141, 24);
            this.DemoPhase3.TabIndex = 29;
            this.DemoPhase3.Text = "0";
            this.label158.AutoSize = true;
            this.label158.Location = new Point(569, 112);
            this.label158.Name = "label158";
            this.label158.Size = new Size(38, 17);
            this.label158.TabIndex = 26;
            this.label158.Text = "Gain";
            this.DemoGain3.FormattingEnabled = true;
            this.DemoGain3.Location = new Point(613, 179);
            this.DemoGain3.Name = "DemoGain3";
            this.DemoGain3.Size = new Size(141, 24);
            this.DemoGain3.TabIndex = 28;
            this.DemoGain3.Text = "0";
            this.label157.AutoSize = true;
            this.label157.Location = new Point(559, 138);
            this.label157.Name = "label157";
            this.label157.Size = new Size(48, 17);
            this.label157.TabIndex = 27;
            this.label157.Text = "Phase";
            this.panel11.Controls.Add((Control)this.DemoFileName);
            this.panel11.Controls.Add((Control)this.LoadDemoFile);
            this.panel11.Controls.Add((Control)this.SelectDemoFile);
            this.panel11.Controls.Add((Control)this.groupBox9);
            this.panel11.Location = new Point(6, 8);
            this.panel11.Name = "panel11";
            this.panel11.Size = new Size(522, 435);
            this.panel11.TabIndex = 3;
            this.DemoFileName.Location = new Point(198, 33);
            this.DemoFileName.Name = "DemoFileName";
            this.DemoFileName.Size = new Size(272, 22);
            this.DemoFileName.TabIndex = 37;
            this.LoadDemoFile.Location = new Point(375, 61);
            this.LoadDemoFile.Name = "LoadDemoFile";
            this.LoadDemoFile.Size = new Size(95, 26);
            this.LoadDemoFile.TabIndex = 38;
            this.LoadDemoFile.Text = "Load File";
            this.LoadDemoFile.UseVisualStyleBackColor = true;
            this.LoadDemoFile.Click += new EventHandler(this.LoadDemoFile_Click);
            this.SelectDemoFile.Location = new Point(97, 31);
            this.SelectDemoFile.Name = "SelectDemoFile";
            this.SelectDemoFile.Size = new Size(95, 26);
            this.SelectDemoFile.TabIndex = 36;
            this.SelectDemoFile.Text = "Select File";
            this.SelectDemoFile.UseVisualStyleBackColor = true;
            this.SelectDemoFile.Click += new EventHandler(this.SelectDemoFile_Click);
            this.groupBox9.Controls.Add((Control)this.button27);
            this.groupBox9.Controls.Add((Control)this.button24);
            this.groupBox9.Controls.Add((Control)this.TimeDelay);
            this.groupBox9.Controls.Add((Control)this.EndPoint);
            this.groupBox9.Controls.Add((Control)this.StartPoint);
            this.groupBox9.Controls.Add((Control)this.label55);
            this.groupBox9.Controls.Add((Control)this.label14);
            this.groupBox9.Controls.Add((Control)this.label13);
            this.groupBox9.Location = new Point(97, 100);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new Size(222, 263);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Run Through";
            this.button27.Location = new Point(114, 225);
            this.button27.Name = "button27";
            this.button27.Size = new Size(102, 32);
            this.button27.TabIndex = 7;
            this.button27.Text = "Stop ";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new EventHandler(this.End_Demo_button);
            this.button24.Location = new Point(6, 225);
            this.button24.Name = "button24";
            this.button24.Size = new Size(102, 32);
            this.button24.TabIndex = 6;
            this.button24.Text = "Start ";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new EventHandler(this.Run_Demo_button);
            this.TimeDelay.Location = new Point(39, 180);
            this.TimeDelay.Maximum = new Decimal(new int[4]
            {
        1000,
        0,
        0,
        0
            });
            this.TimeDelay.Name = "TimeDelay";
            this.TimeDelay.Size = new Size(107, 22);
            this.TimeDelay.TabIndex = 5;
            this.EndPoint.Location = new Point(39, 122);
            this.EndPoint.Maximum = new Decimal(new int[4]
            {
        80,
        0,
        0,
        0
            });
            this.EndPoint.Minimum = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.EndPoint.Name = "EndPoint";
            this.EndPoint.Size = new Size(107, 22);
            this.EndPoint.TabIndex = 4;
            this.EndPoint.Value = new Decimal(new int[4]
            {
        2,
        0,
        0,
        0
            });
            this.EndPoint.ValueChanged += new EventHandler(this.DemoPoint_ValueChanged);
            this.StartPoint.Location = new Point(39, 66);
            this.StartPoint.Maximum = new Decimal(new int[4]
            {
        79,
        0,
        0,
        0
            });
            this.StartPoint.Name = "StartPoint";
            this.StartPoint.Size = new Size(107, 22);
            this.StartPoint.TabIndex = 3;
            this.StartPoint.ValueChanged += new EventHandler(this.DemoPoint_ValueChanged);
            this.label55.AutoSize = true;
            this.label55.Location = new Point(36, 160);
            this.label55.Name = "label55";
            this.label55.Size = new Size(111, 17);
            this.label55.TabIndex = 2;
            this.label55.Text = "Time Delay (ms)";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(36, 102);
            this.label14.Name = "label14";
            this.label14.Size = new Size(69, 17);
            this.label14.TabIndex = 1;
            this.label14.Text = "End Point";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(36, 46);
            this.label13.Name = "label13";
            this.label13.Size = new Size(74, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "Start Point";
            this.PhaseLoop.Controls.Add((Control)this.groupBox28);
            this.PhaseLoop.Controls.Add((Control)this.groupBox23);
            this.PhaseLoop.Controls.Add((Control)this.groupBox10);
            this.PhaseLoop.Controls.Add((Control)this.Stop_demo_button);
            this.PhaseLoop.Location = new Point(4, 25);
            this.PhaseLoop.Name = "PhaseLoop";
            this.PhaseLoop.Size = new Size(1185, 446);
            this.PhaseLoop.TabIndex = 13;
            this.PhaseLoop.Text = " Phase Loop";
            this.PhaseLoop.ToolTipText = "Demo mode where phase jumps through a series of fixed steps";
            this.PhaseLoop.UseVisualStyleBackColor = true;
            this.groupBox28.Controls.Add((Control)this.ADI_logo_demo_button);
            this.groupBox28.Controls.Add((Control)this.comboBox1);
            this.groupBox28.Controls.Add((Control)this.label137);
            this.groupBox28.Controls.Add((Control)this.numericUpDown1);
            this.groupBox28.Controls.Add((Control)this.label138);
            this.groupBox28.Location = new Point(204, 132);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new Size(453, 95);
            this.groupBox28.TabIndex = 25;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "ADI Logo Loop";
            this.ADI_logo_demo_button.Location = new Point(328, 46);
            this.ADI_logo_demo_button.Name = "ADI_logo_demo_button";
            this.ADI_logo_demo_button.Size = new Size(97, 34);
            this.ADI_logo_demo_button.TabIndex = 16;
            this.ADI_logo_demo_button.Text = "Start ";
            this.ADI_logo_demo_button.UseVisualStyleBackColor = true;
            this.ADI_logo_demo_button.Click += new EventHandler(this.Start_ADIdemo_button_Click);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(24, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(103, 24);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.Text = "0";
            this.label137.AutoSize = true;
            this.label137.Location = new Point(21, 33);
            this.label137.Name = "label137";
            this.label137.Size = new Size(118, 17);
            this.label137.TabIndex = 23;
            this.label137.Text = "Phase Offset ( ° )";
            this.numericUpDown1.Location = new Point(167, 53);
            this.numericUpDown1.Maximum = new Decimal(new int[4]
            {
        10000,
        0,
        0,
        0
            });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(120, 22);
            this.numericUpDown1.TabIndex = 21;
            this.label138.AutoSize = true;
            this.label138.Location = new Point(164, 33);
            this.label138.Name = "label138";
            this.label138.Size = new Size(108, 17);
            this.label138.TabIndex = 22;
            this.label138.Text = "Dwell Time (ms)";
            this.groupBox23.Controls.Add((Control)this.Start_demo_button);
            this.groupBox23.Controls.Add((Control)this.Demo_angle_list);
            this.groupBox23.Controls.Add((Control)this.Demo_loop_time);
            this.groupBox23.Controls.Add((Control)this.label135);
            this.groupBox23.Controls.Add((Control)this.label136);
            this.groupBox23.Location = new Point(204, 31);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new Size(453, 95);
            this.groupBox23.TabIndex = 24;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Phase Loop";
            this.Start_demo_button.Location = new Point(328, 51);
            this.Start_demo_button.Name = "Start_demo_button";
            this.Start_demo_button.Size = new Size(97, 34);
            this.Start_demo_button.TabIndex = 14;
            this.Start_demo_button.Text = "Start ";
            this.Start_demo_button.UseVisualStyleBackColor = true;
            this.Start_demo_button.Click += new EventHandler(this.Start_demo_button_Click);
            this.Demo_angle_list.FormattingEnabled = true;
            this.Demo_angle_list.Location = new Point(24, 56);
            this.Demo_angle_list.Name = "Demo_angle_list";
            this.Demo_angle_list.Size = new Size(103, 24);
            this.Demo_angle_list.TabIndex = 12;
            this.Demo_angle_list.Text = "0";
            this.Demo_loop_time.Location = new Point(167, 57);
            this.Demo_loop_time.Maximum = new Decimal(new int[4]
            {
        10000,
        0,
        0,
        0
            });
            this.Demo_loop_time.Name = "Demo_loop_time";
            this.Demo_loop_time.Size = new Size(120, 22);
            this.Demo_loop_time.TabIndex = 13;
            this.Demo_loop_time.ValueChanged += new EventHandler(this.numericUpDown1_ValueChanged);
            this.label135.AutoSize = true;
            this.label135.Location = new Point(164, 37);
            this.label135.Name = "label135";
            this.label135.Size = new Size(108, 17);
            this.label135.TabIndex = 17;
            this.label135.Text = "Dwell Time (ms)";
            this.label136.AutoSize = true;
            this.label136.Location = new Point(21, 37);
            this.label136.Name = "label136";
            this.label136.Size = new Size(109, 17);
            this.label136.TabIndex = 18;
            this.label136.Text = "Phase Step ( ° )";
            this.groupBox10.Controls.Add((Control)this.RX1_radioButton);
            this.groupBox10.Controls.Add((Control)this.RX2_radioButton);
            this.groupBox10.Controls.Add((Control)this.RX3_radioButton);
            this.groupBox10.Controls.Add((Control)this.RX4_radioButton);
            this.groupBox10.Controls.Add((Control)this.TX1_radioButton);
            this.groupBox10.Controls.Add((Control)this.TX2_radioButton);
            this.groupBox10.Controls.Add((Control)this.TX3_radioButton);
            this.groupBox10.Controls.Add((Control)this.TX4_radioButton);
            this.groupBox10.Location = new Point(73, 31);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(125, 284);
            this.groupBox10.TabIndex = 19;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Select Channel ";
            this.RX1_radioButton.AutoSize = true;
            this.RX1_radioButton.Checked = true;
            this.RX1_radioButton.Location = new Point(6, 21);
            this.RX1_radioButton.Name = "RX1_radioButton";
            this.RX1_radioButton.Size = new Size(56, 21);
            this.RX1_radioButton.TabIndex = 4;
            this.RX1_radioButton.TabStop = true;
            this.RX1_radioButton.Text = "RX1";
            this.RX1_radioButton.UseVisualStyleBackColor = true;
            this.RX1_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.RX2_radioButton.AutoSize = true;
            this.RX2_radioButton.Location = new Point(6, 48);
            this.RX2_radioButton.Name = "RX2_radioButton";
            this.RX2_radioButton.Size = new Size(56, 21);
            this.RX2_radioButton.TabIndex = 5;
            this.RX2_radioButton.Text = "RX2";
            this.RX2_radioButton.UseVisualStyleBackColor = true;
            this.RX2_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.RX3_radioButton.AutoSize = true;
            this.RX3_radioButton.Location = new Point(6, 75);
            this.RX3_radioButton.Name = "RX3_radioButton";
            this.RX3_radioButton.Size = new Size(56, 21);
            this.RX3_radioButton.TabIndex = 6;
            this.RX3_radioButton.Text = "RX3";
            this.RX3_radioButton.UseVisualStyleBackColor = true;
            this.RX3_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.RX4_radioButton.AutoSize = true;
            this.RX4_radioButton.Location = new Point(6, 102);
            this.RX4_radioButton.Name = "RX4_radioButton";
            this.RX4_radioButton.Size = new Size(56, 21);
            this.RX4_radioButton.TabIndex = 7;
            this.RX4_radioButton.Text = "RX4";
            this.RX4_radioButton.UseVisualStyleBackColor = true;
            this.RX4_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.TX1_radioButton.AutoSize = true;
            this.TX1_radioButton.Location = new Point(6, 172);
            this.TX1_radioButton.Name = "TX1_radioButton";
            this.TX1_radioButton.Size = new Size(55, 21);
            this.TX1_radioButton.TabIndex = 8;
            this.TX1_radioButton.Text = "TX1";
            this.TX1_radioButton.UseVisualStyleBackColor = true;
            this.TX1_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.TX2_radioButton.AutoSize = true;
            this.TX2_radioButton.Location = new Point(6, 199);
            this.TX2_radioButton.Name = "TX2_radioButton";
            this.TX2_radioButton.Size = new Size(55, 21);
            this.TX2_radioButton.TabIndex = 9;
            this.TX2_radioButton.Text = "TX2";
            this.TX2_radioButton.UseVisualStyleBackColor = true;
            this.TX2_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.TX3_radioButton.AutoSize = true;
            this.TX3_radioButton.Location = new Point(6, 226);
            this.TX3_radioButton.Name = "TX3_radioButton";
            this.TX3_radioButton.Size = new Size(55, 21);
            this.TX3_radioButton.TabIndex = 10;
            this.TX3_radioButton.Text = "TX3";
            this.TX3_radioButton.UseVisualStyleBackColor = true;
            this.TX3_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.TX4_radioButton.AutoSize = true;
            this.TX4_radioButton.Location = new Point(6, 253);
            this.TX4_radioButton.Name = "TX4_radioButton";
            this.TX4_radioButton.Size = new Size(55, 21);
            this.TX4_radioButton.TabIndex = 11;
            this.TX4_radioButton.Text = "TX4";
            this.TX4_radioButton.UseVisualStyleBackColor = true;
            this.TX4_radioButton.CheckedChanged += new EventHandler(this.RX_TX_radioButton_CheckedChanged);
            this.Stop_demo_button.Location = new Point(532, 257);
            this.Stop_demo_button.Name = "Stop_demo_button";
            this.Stop_demo_button.Size = new Size(97, 34);
            this.Stop_demo_button.TabIndex = 15;
            this.Stop_demo_button.Text = "Stop";
            this.Stop_demo_button.UseVisualStyleBackColor = true;
            this.Stop_demo_button.Click += new EventHandler(this.CancelDemo);
            // 경계 였던 것
            // 경계 였던 것
            this.ManualRegWrite.Controls.Add((Control)this.groupBox2);
            this.ManualRegWrite.Controls.Add((Control)this.groupBox4);
            this.ManualRegWrite.Location = new Point(4, 25);
            this.ManualRegWrite.Margin = new Padding(4);
            this.ManualRegWrite.Name = "ManualRegWrite";
            this.ManualRegWrite.Padding = new Padding(4);
            this.ManualRegWrite.Size = new Size(1185, 446);
            this.ManualRegWrite.TabIndex = 0;
            this.ManualRegWrite.Text = "Manual Register Write";
            this.ManualRegWrite.ToolTipText = "Write data to specific registers";
            this.ManualRegWrite.UseVisualStyleBackColor = true;

            this.groupBox4.Controls.Add((Control)this.label1);
            this.groupBox4.Controls.Add((Control)this.textBox1);
            this.groupBox4.Controls.Add((Control)this.ChooseInputButton);
            this.groupBox4.Controls.Add((Control)this.ValuesToWriteBox);
            this.groupBox4.Controls.Add((Control)this.WriteAllButton);
            this.groupBox4.Location = new Point(8, 8);
            this.groupBox4.Margin = new Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new Padding(4);
            this.groupBox4.Size = new Size(423, 370);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Load and Write";

            this.label1.AutoSize = true;
            this.label1.Location = new Point(232, 139);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(139, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sample file contents:";

            this.textBox1.Location = new Point(235, 160);
            this.textBox1.Margin = new Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(180, 61);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "123ABC ";

            this.ChooseInputButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.ChooseInputButton.Location = new Point(8, 81);
            this.ChooseInputButton.Margin = new Padding(4);
            this.ChooseInputButton.Name = "ChooseInputButton";
            this.ChooseInputButton.Size = new Size(181, 28);
            this.ChooseInputButton.TabIndex = 0;
            this.ChooseInputButton.Text = "Choose Input File";
            this.ChooseInputButton.UseVisualStyleBackColor = true;
            this.ChooseInputButton.Click += new EventHandler(this.ChooseInputButton_Click);

            this.ValuesToWriteBox.Location = new Point(8, 117);
            this.ValuesToWriteBox.Margin = new Padding(4);
            this.ValuesToWriteBox.Multiline = true;
            this.ValuesToWriteBox.Name = "ValuesToWriteBox";
            this.ValuesToWriteBox.Size = new Size(180, 143);
            this.ValuesToWriteBox.TabIndex = 1;

            this.WriteAllButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.WriteAllButton.Location = new Point(8, 268);
            this.WriteAllButton.Margin = new Padding(4);
            this.WriteAllButton.Name = "WriteAllButton";
            this.WriteAllButton.Size = new Size(181, 28);
            this.WriteAllButton.TabIndex = 2;
            this.WriteAllButton.Text = "Write All";
            this.WriteAllButton.UseVisualStyleBackColor = true;
            this.WriteAllButton.Click += new EventHandler(this.WriteAllButton_Click);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 경계 였던 것
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            this.AutoRegWrite.Controls.Add((Control)this.groupBox666);
            //BeamSweeping Graphic By Hyunsoo
            this.AutoRegWrite.Controls.Add((Control)this.BeamSweeping_GUI_Panel_0);
            this.AutoRegWrite.Location = new Point(4, 25);
            this.AutoRegWrite.Margin = new Padding(4);
            this.AutoRegWrite.Name = "BeamSweeper";
            this.AutoRegWrite.Padding = new Padding(4);
            this.AutoRegWrite.Size = new Size(1185, 446);
            this.AutoRegWrite.TabIndex = 666;
            this.AutoRegWrite.Text = "Beam Sweeper";
            this.AutoRegWrite.ToolTipText = "Write data to specific registers";
            this.AutoRegWrite.UseVisualStyleBackColor = true;

            this.groupBox666.Controls.Add((Control)this.label180);
            this.groupBox666.Controls.Add((Control)this.ValuesToWriteBox00);
            this.groupBox666.Controls.Add((Control)this.StartBeamSweeping);
            this.groupBox666.Controls.Add((Control)this.StartRandomBeamForming);

            

            this.groupBox666.Location = new Point(8, 8);
            this.groupBox666.Margin = new Padding(4);
            this.groupBox666.Name = "groupBox666";
            this.groupBox666.Padding = new Padding(4);
            this.groupBox666.Size = new Size(700, 400);
            this.groupBox666.TabIndex = 4;
            this.groupBox666.TabStop = false;
            this.groupBox666.Text = "Beam Sweeper";

            this.label180.AutoSize = true;
            this.label180.Location = new Point(20, 30);
            this.label180.Margin = new Padding(4, 0, 4, 0);
            this.label180.Name = "label180";
            this.label180.Size = new Size(300, 50);
            this.label180.TabIndex = 5;
            this.label180.Text = "Input Starting-Angle / Ending-Angle / Resolution of Angle / Time-Delay / Directory of Register Info :";

            this.ValuesToWriteBox00.Location = new Point(20, 50);
            this.ValuesToWriteBox00.Margin = new Padding(4);
            this.ValuesToWriteBox00.Multiline = true;
            this.ValuesToWriteBox00.Name = "Commend";
            this.ValuesToWriteBox00.Size = new Size(500, 250);
            this.ValuesToWriteBox00.TabIndex = 1;

            this.StartRandomBeamForming.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartRandomBeamForming.Location = new Point(330, 330);
            this.StartRandomBeamForming.Margin = new Padding(4);
            this.StartRandomBeamForming.Name = "StartRandomBeamForming_Click";
            this.StartRandomBeamForming.Size = new Size(300, 40);
            this.StartRandomBeamForming.TabIndex = 2;
            this.StartRandomBeamForming.Text = "Start Random Beam Forming !!";
            this.StartRandomBeamForming.UseVisualStyleBackColor = true;
            this.StartRandomBeamForming.Click += new EventHandler(this.Start_Random_Beam_Forming_Click);


            this.StartBeamSweeping.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamSweeping.Location = new Point(20, 330);
            this.StartBeamSweeping.Margin = new Padding(4);
            this.StartBeamSweeping.Name = "StartBeamSweeping_Click";
            this.StartBeamSweeping.Size = new Size(300, 40);
            this.StartBeamSweeping.TabIndex = 2;
            this.StartBeamSweeping.Text = "Start Beam Sweeping !!";
            this.StartBeamSweeping.UseVisualStyleBackColor = true;
            this.StartBeamSweeping.Click += new EventHandler(this.StartBeamSweeping_Click);

            
            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_0.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_0.Name = "BeamSweeping_GUI_Panel";
            this.BeamSweeping_GUI_Panel_0.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_0.TabIndex = 25;

            /*
            this.BeamSweeping_GUI_Panel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamSweeping.Location = new Point(20, 330);
            this.StartBeamSweeping.Margin = new Padding(4);
            this.StartBeamSweeping.Name = "StartBeamSweeping_Click";
            this.StartBeamSweeping.Size = new Size(300, 40);
            this.StartBeamSweeping.TabIndex = 2;
            this.StartBeamSweeping.Text = "Start Beam Sweeping !!";
            this.StartBeamSweeping.UseVisualStyleBackColor = true;
            this.StartBeamSweeping.Click += new EventHandler(this.StartBeamSweeping_Click);
            */


            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 Beam-Alignment for Tx
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            this.BeamAlignmentTx.Controls.Add((Control)this.groupBox667);
            //BeamSweeping Graphic By Hyunsoo
            this.BeamAlignmentTx.Controls.Add((Control)this.BeamSweeping_GUI_Panel_T);


            this.BeamAlignmentTx.Location = new Point(4, 25);
            this.BeamAlignmentTx.Margin = new Padding(4);
            this.BeamAlignmentTx.Name = "BeamSweeper";
            this.BeamAlignmentTx.Padding = new Padding(4);
            this.BeamAlignmentTx.Size = new Size(1185, 446);
            this.BeamAlignmentTx.TabIndex = 667;
            this.BeamAlignmentTx.Text = "Beam Alignment Tx";
            this.BeamAlignmentTx.ToolTipText = "Write data to specific registers";
            this.BeamAlignmentTx.UseVisualStyleBackColor = true;

            this.groupBox667.Controls.Add((Control)this.label181);
            this.groupBox667.Controls.Add((Control)this.ValuesToWriteBoxT0);
            this.groupBox667.Controls.Add((Control)this.StartBeamAlignmentTx);
            this.groupBox667.Controls.Add((Control)this.StartBeamAlignmentTx_using_Loc_info);

            this.groupBox667.Location = new Point(8, 8);
            this.groupBox667.Margin = new Padding(4);
            this.groupBox667.Name = "groupBox667";
            this.groupBox667.Padding = new Padding(4);
            this.groupBox667.Size = new Size(700, 400);
            this.groupBox667.TabIndex = 4;
            this.groupBox667.TabStop = false;
            this.groupBox667.Text = "Beam-Alignment for Tx";

            this.label181.AutoSize = true;
            this.label181.Location = new Point(20, 30);
            this.label181.Margin = new Padding(4, 0, 4, 0);
            this.label181.Name = "label181";
            this.label181.Size = new Size(300, 50);
            this.label181.TabIndex = 5;
            this.label181.Text = "Input Starting-Angle / Ending-Angle / Resolution of Angle / Time-Delay / Directory of Register Info :";

            this.ValuesToWriteBoxT0.Location = new Point(20, 50);
            this.ValuesToWriteBoxT0.Margin = new Padding(4);
            this.ValuesToWriteBoxT0.Multiline = true;
            this.ValuesToWriteBoxT0.Name = "Commend";
            this.ValuesToWriteBoxT0.Size = new Size(500, 250);
            this.ValuesToWriteBoxT0.TabIndex = 1;

            this.StartBeamAlignmentTx.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentTx.Location = new Point(20, 330);
            this.StartBeamAlignmentTx.Margin = new Padding(4);
            this.StartBeamAlignmentTx.Name = "StartBeamAlignmentTx_Click";
            this.StartBeamAlignmentTx.Size = new Size(300, 40);
            this.StartBeamAlignmentTx.TabIndex = 2;
            this.StartBeamAlignmentTx.Text = "Start Beam-Alignment in Tx Mode !!";
            this.StartBeamAlignmentTx.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentTx.Click += new EventHandler(this.StartBeamAlignmentTx_Click);

            this.StartBeamAlignmentTx_using_Loc_info.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentTx_using_Loc_info.Location = new Point(330, 330);
            this.StartBeamAlignmentTx_using_Loc_info.Margin = new Padding(4);
            this.StartBeamAlignmentTx_using_Loc_info.Name = "StartBeamAlignmentTx_Using_Location_Info_Click";
            this.StartBeamAlignmentTx_using_Loc_info.Size = new Size(350, 40);
            this.StartBeamAlignmentTx_using_Loc_info.TabIndex = 2;
            this.StartBeamAlignmentTx_using_Loc_info.Text = "Start Beam-Alignment in Tx Mode Using Location-Info !!";
            this.StartBeamAlignmentTx_using_Loc_info.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentTx_using_Loc_info.Click += new EventHandler(this.StartBeamAlignmentTx_using_Loc_info_Click);

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_T.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_T.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_T.Name = "BeamSweeping_GUI_Panel_Tx";
            this.BeamSweeping_GUI_Panel_T.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_T.TabIndex = 25;


            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 Beam-Alignment for Rx
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            this.BeamAlignmentRx.Controls.Add((Control)this.groupBox668);
            //BeamSweeping Graphic By Hyunsoo
            this.BeamAlignmentRx.Controls.Add((Control)this.BeamSweeping_GUI_Panel_R);

            this.BeamAlignmentRx.Location = new Point(4, 25);
            this.BeamAlignmentRx.Margin = new Padding(4);
            this.BeamAlignmentRx.Name = "BeamSweeper";
            this.BeamAlignmentRx.Padding = new Padding(4);
            this.BeamAlignmentRx.Size = new Size(1185, 446);
            this.BeamAlignmentRx.TabIndex = 667;
            this.BeamAlignmentRx.Text = "Beam Alignment Rx";
            this.BeamAlignmentRx.ToolTipText = "Write data to specific registers";
            this.BeamAlignmentRx.UseVisualStyleBackColor = true;

            this.groupBox668.Controls.Add((Control)this.label182);
            this.groupBox668.Controls.Add((Control)this.ValuesToWriteBoxR0);
            this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx);
            this.groupBox668.Controls.Add((Control)this.StartBeamAlignmentRx_using_Loc_info);

            this.groupBox668.Location = new Point(8, 8);
            this.groupBox668.Margin = new Padding(4);
            this.groupBox668.Name = "groupBox667";
            this.groupBox668.Padding = new Padding(4);
            this.groupBox668.Size = new Size(700, 400);
            this.groupBox668.TabIndex = 4;
            this.groupBox668.TabStop = false;
            this.groupBox668.Text = "Beam-Alignment for Rx";

            this.label182.AutoSize = true;
            this.label182.Location = new Point(20, 30);
            this.label182.Margin = new Padding(4, 0, 4, 0);
            this.label182.Name = "label181";
            this.label182.Size = new Size(300, 50);
            this.label182.TabIndex = 5;
            this.label182.Text = "Input Starting-Angle / Ending-Angle / Resolution of Angle / Time-Delay / Directory of Register Info :";

            this.ValuesToWriteBoxR0.Location = new Point(20, 50);
            this.ValuesToWriteBoxR0.Margin = new Padding(4);
            this.ValuesToWriteBoxR0.Multiline = true;
            this.ValuesToWriteBoxR0.Name = "Commend";
            this.ValuesToWriteBoxR0.Size = new Size(500, 250);
            this.ValuesToWriteBoxR0.TabIndex = 1;

            this.StartBeamAlignmentRx.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentRx.Location = new Point(20, 330);
            this.StartBeamAlignmentRx.Margin = new Padding(4);
            this.StartBeamAlignmentRx.Name = "StartBeamAlignmentRx_Click";
            this.StartBeamAlignmentRx.Size = new Size(300, 40);
            this.StartBeamAlignmentRx.TabIndex = 2;
            this.StartBeamAlignmentRx.Text = "Start Beam-Alignment in Rx Mode !!";
            this.StartBeamAlignmentRx.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentRx.Click += new EventHandler(this.StartBeamAlignmentRx_Click);

            this.StartBeamAlignmentRx_using_Loc_info.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamAlignmentRx_using_Loc_info.Location = new Point(330, 330);
            this.StartBeamAlignmentRx_using_Loc_info.Margin = new Padding(4);
            this.StartBeamAlignmentRx_using_Loc_info.Name = "StartBeamAlignmentRx_using_Loc_info_Click";
            this.StartBeamAlignmentRx_using_Loc_info.Size = new Size(350, 40);
            this.StartBeamAlignmentRx_using_Loc_info.TabIndex = 2;
            this.StartBeamAlignmentRx_using_Loc_info.Text = "Start Beam-Alignment in Rx Mode Using Location-info !!";
            this.StartBeamAlignmentRx_using_Loc_info.UseVisualStyleBackColor = true;
            this.StartBeamAlignmentRx_using_Loc_info.Click += new EventHandler(this.StartBeamAlignmentRx_using_Loc_info_Click);

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_R.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamSweeping_GUI_Panel_R.Location = new Point(730, 8);
            this.BeamSweeping_GUI_Panel_R.Name = "BeamSweeping_GUI_Panel_R";
            this.BeamSweeping_GUI_Panel_R.Size = new Size(400, 400);
            this.BeamSweeping_GUI_Panel_R.TabIndex = 25;



            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // 여기부턴 SIMO Beam Tracking using Particle Filter
            ////////////////////////////////////////////////////////////////////////////////////////////////////



            this.Tracking_with_PF.Controls.Add((Control)this.groupBox669);
            //BeamSweeping Graphic By Hyunsoo
            this.Tracking_with_PF.Controls.Add((Control)this.BeamTracking_GUI_Panel_SIMO);

            this.Tracking_with_PF.Location = new Point(4, 25);
            this.Tracking_with_PF.Margin = new Padding(4);
            this.Tracking_with_PF.Name = "BeamSweeper";
            this.Tracking_with_PF.Padding = new Padding(4);
            this.Tracking_with_PF.Size = new Size(1185, 446);
            this.Tracking_with_PF.TabIndex = 669;
            this.Tracking_with_PF.Text = "Beam Tracking using Particle Filter";
            this.Tracking_with_PF.ToolTipText = "Write data to specific registers";
            this.Tracking_with_PF.UseVisualStyleBackColor = true;

            this.groupBox669.Controls.Add((Control)this.mylabel001);
            this.groupBox669.Controls.Add((Control)this.ValuesToWirteBoxPF01);
            this.groupBox669.Controls.Add((Control)this.StartBeamTracking_SIMO_PF);

            this.groupBox669.Location = new Point(8, 8);
            this.groupBox669.Margin = new Padding(4);
            this.groupBox669.Name = "groupBox667";
            this.groupBox669.Padding = new Padding(4);
            this.groupBox669.Size = new Size(700, 400);
            this.groupBox669.TabIndex = 4;
            this.groupBox669.TabStop = false;
            this.groupBox669.Text = "Beam-Alignment for Rx";

            this.mylabel001.AutoSize = true;
            this.mylabel001.Location = new Point(20, 30);
            this.mylabel001.Margin = new Padding(4, 0, 4, 0);
            this.mylabel001.Name = "mylabel001";
            this.mylabel001.Size = new Size(300, 50);
            this.mylabel001.TabIndex = 5;
            this.mylabel001.Text = "Input Starting-Angle / Ending-Angle / Resolution of Angle / Time-Delay / Directory of Register Info \n / Initial AOA:";

            this.ValuesToWirteBoxPF01.Location = new Point(20, 70);
            this.ValuesToWirteBoxPF01.Margin = new Padding(4);
            this.ValuesToWirteBoxPF01.Multiline = true;
            this.ValuesToWirteBoxPF01.Name = "Command";
            this.ValuesToWirteBoxPF01.Size = new Size(500, 250);
            this.ValuesToWirteBoxPF01.TabIndex = 1;

            this.StartBeamTracking_SIMO_PF.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.StartBeamTracking_SIMO_PF.Location = new Point(20, 330);
            this.StartBeamTracking_SIMO_PF.Margin = new Padding(4);
            this.StartBeamTracking_SIMO_PF.Name = "StartBeamAlignmentRx_Click";
            this.StartBeamTracking_SIMO_PF.Size = new Size(300, 40);
            this.StartBeamTracking_SIMO_PF.TabIndex = 2;
            this.StartBeamTracking_SIMO_PF.Text = "Start Beam Tracking with Particle Filter !!";
            this.StartBeamTracking_SIMO_PF.UseVisualStyleBackColor = true;
            this.StartBeamTracking_SIMO_PF.Click += new EventHandler(this.Start_SIMO_BeamTracking_with_PF);

            //BeamSweeping Graphic By Hyunsoo
            this.BeamTracking_GUI_Panel_SIMO.BackColor = Color.FromArgb(192, (int)byte.MaxValue, (int)byte.MaxValue);
            this.BeamTracking_GUI_Panel_SIMO.Location = new Point(730, 8);
            this.BeamTracking_GUI_Panel_SIMO.Name = "BeamSweeping_GUI_Panel_R";
            this.BeamTracking_GUI_Panel_SIMO.Size = new Size(400, 400);
            this.BeamTracking_GUI_Panel_SIMO.TabIndex = 25;


            // 경계 였던 것
            this.ReadBack.Controls.Add((Control)this.panel1);
            this.ReadBack.Controls.Add((Control)this.label133);
            this.ReadBack.Controls.Add((Control)this.ROBox);
            this.ReadBack.Controls.Add((Control)this.button26);
            this.ReadBack.Controls.Add((Control)this.button8);
            this.ReadBack.Controls.Add((Control)this.label53);
            this.ReadBack.Controls.Add((Control)this.label52);
            this.ReadBack.Controls.Add((Control)this.textBox3);
            this.ReadBack.Controls.Add((Control)this.textBox2);
            this.ReadBack.Controls.Add((Control)this.button25);
            this.ReadBack.Controls.Add((Control)this.dataGridView1);
            this.ReadBack.Location = new Point(4, 25);
            this.ReadBack.Name = "ReadBack";
            this.ReadBack.Padding = new Padding(3);
            this.ReadBack.Size = new Size(1185, 446);
            this.ReadBack.TabIndex = 10;
            this.ReadBack.Text = " ReadBack";
            this.ReadBack.ToolTipText = "Readback data from specific retisters";
            this.ReadBack.UseVisualStyleBackColor = true;
            this.panel1.Location = new Point(6, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(457, 394);
            this.panel1.TabIndex = 16;
            this.label133.AutoSize = true;
            this.label133.Location = new Point(513, 100);
            this.label133.Name = "label133";
            this.label133.Size = new Size(41, 17);
            this.label133.TabIndex = 15;
            this.label133.Text = "Write";
            this.ROBox.Location = new Point(573, 95);
            this.ROBox.Name = "ROBox";
            this.ROBox.Size = new Size(136, 22);
            this.ROBox.TabIndex = 14;
            this.button26.Location = new Point(573, 123);
            this.button26.Name = "button26";
            this.button26.Size = new Size(136, 30);
            this.button26.TabIndex = 13;
            this.button26.Text = "Manual Write";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new EventHandler(this.Write_button_Click);
            this.button8.Location = new Point(573, 268);
            this.button8.Name = "button8";
            this.button8.Size = new Size(136, 30);
            this.button8.TabIndex = 12;
            this.button8.Text = "Manual Read";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new EventHandler(this.ReadbackButton_Click);
            this.label53.AutoSize = true;
            this.label53.Location = new Point(513, 245);
            this.label53.Name = "label53";
            this.label53.Size = new Size(38, 17);
            this.label53.TabIndex = 11;
            this.label53.Text = "Data";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(513, 217);
            this.label52.Name = "label52";
            this.label52.Size = new Size(61, 17);
            this.label52.TabIndex = 10;
            this.label52.Text = "Register";
            this.textBox3.Location = new Point(573, 240);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(136, 22);
            this.textBox3.TabIndex = 9;
            this.textBox2.Location = new Point(573, 212);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(136, 22);
            this.textBox2.TabIndex = 8;
            this.button25.Location = new Point(47, 373);
            this.button25.Name = "button25";
            this.button25.Size = new Size(129, 33);
            this.button25.TabIndex = 7;
            this.button25.Text = "Generate";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new EventHandler(this.Generate_read_Click);
            this.dataGridView1.BackgroundColor = SystemColors.GradientActiveCaption;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = gridViewCellStyle2;
            this.dataGridView1.Location = new Point(47, 57);
            this.dataGridView1.Name = "dataGridView1";
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = gridViewCellStyle3;
            this.dataGridView1.Size = new Size(373, 310);
            this.dataGridView1.TabIndex = 0;
            this.PasswordPanel.BorderStyle = BorderStyle.FixedSingle;
            this.PasswordPanel.Controls.Add((Control)this.OKButton);
            this.PasswordPanel.Controls.Add((Control)this.PasswordBox);
            this.PasswordPanel.Controls.Add((Control)this.label4);
            this.PasswordPanel.Location = new Point(978, 531);
            this.PasswordPanel.Margin = new Padding(4);
            this.PasswordPanel.Name = "PasswordPanel";
            this.PasswordPanel.Size = new Size(205, 61);
            this.PasswordPanel.TabIndex = 55;
            this.PasswordPanel.Visible = false;
            this.OKButton.Location = new Point(148, 7);
            this.OKButton.Margin = new Padding(4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new Size(51, 44);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new EventHandler(this.OKButton_Click_1);
            this.PasswordBox.Location = new Point(8, 27);
            this.PasswordBox.Margin = new Padding(4);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new Size(129, 22);
            this.PasswordBox.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 7);
            this.label4.Margin = new Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(73, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password:";
            this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(176, 194);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.button1.Location = new Point(213, 81);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(44, 219);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(112, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "SDP board (black)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label10.Location = new Point(109, 257);
            this.label10.Name = "label10";
            this.label10.Size = new Size(115, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Connecting...";
            this.label10.Visible = false;
            this.Registers.HeaderText = "Register";
            this.Registers.Name = "Registers";
            this.Written.HeaderText = "Written";
            this.Written.Name = "Written";
            this.Read_col.HeaderText = "Read";
            this.Read_col.Name = "Read_col";
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1195, 631);
            //this.Controls.Add((Control)this.ADILogo);
            this.Controls.Add((Control)this.PasswordPanel);
            this.Controls.Add((Control)this.tabControl1);
            this.Controls.Add((Control)this.EventLog);
            this.Controls.Add((Control)this.MainFormStatusBar);
            this.Controls.Add((Control)this.MainFormMenu);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MainMenuStrip = this.MainFormMenu;
            this.Margin = new Padding(4);
            this.Name = nameof(Main_Form);
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Analog Devices ADAR1000 Evalutaion Software";
            this.Leave += new EventHandler(this.exitToolStripMenuItem_Click);
            this.MainFormStatusBar.ResumeLayout(false);
            this.MainFormStatusBar.PerformLayout();
            this.MainFormMenu.ResumeLayout(false);
            this.MainFormMenu.PerformLayout();
            ((ISupportInitialize)this.ADILogo).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ConnectionTab.ResumeLayout(false);
            this.ConnectionTab.PerformLayout();
            ((ISupportInitialize)this.pictureBox3).EndInit();
            ((ISupportInitialize)this.pictureBox2).EndInit();
            this.Tx_Control.ResumeLayout(false);
            this.Tx_Control.PerformLayout();
            ((ISupportInitialize)this.TX1_Attn_Pic).EndInit();
            ((ISupportInitialize)this.TX4_Attn_Pic).EndInit();
            ((ISupportInitialize)this.TX3_Attn_Pic).EndInit();
            ((ISupportInitialize)this.TX2_Attn_Pic).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((ISupportInitialize)this.pictureBox5).EndInit();
            this.TXRegisters.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.CH4_TX_Phase_Q.EndInit();
            this.CH3_TX_Phase_Q.EndInit();
            this.CH2_TX_Phase_Q.EndInit();
            this.CH1_TX_Phase_Q.EndInit();
            this.CH4_TX_Phase_I.EndInit();
            this.CH3_TX_Phase_I.EndInit();
            this.CH2_TX_Phase_I.EndInit();
            this.CH1_TX_Phase_I.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Gain4_Value.EndInit();
            this.Gain1_Value.EndInit();
            this.Gain3_Value.EndInit();
            this.Gain2_Value.EndInit();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.TX_CHX_RAM_INDEX.EndInit();
            this.TX_CH4_RAM_INDEX.EndInit();
            this.TX_CH3_RAM_INDEX.EndInit();
            this.TX_CH2_RAM_INDEX.EndInit();
            this.TX_CH1_RAM_INDEX.EndInit();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.TX_DRV_BIAS.EndInit();
            this.TX_VM_BIAS3.EndInit();
            this.TX_VGA_BIAS3.EndInit();
            this.Rx_Control.ResumeLayout(false);
            this.Rx_Control.PerformLayout();
            ((ISupportInitialize)this.RX1_Attn_Pic).EndInit();
            ((ISupportInitialize)this.RX4_Attn_Pic).EndInit();
            ((ISupportInitialize)this.RX3_Attn_Pic).EndInit();
            ((ISupportInitialize)this.RX2_Attn_Pic).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((ISupportInitialize)this.pictureBox6).EndInit();
            this.RXRegisters.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.CH4_RX_Phase_Q.EndInit();
            this.CH3_RX_Phase_Q.EndInit();
            this.CH2_RX_Phase_Q.EndInit();
            this.CH1_RX_Phase_Q.EndInit();
            this.CH4_RX_Phase_I.EndInit();
            this.CH3_RX_Phase_I.EndInit();
            this.CH2_RX_Phase_I.EndInit();
            this.CH1_RX_Phase_I.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.RXGain4.EndInit();
            this.RXGain3.EndInit();
            this.RXGain2.EndInit();
            this.RXGain1.EndInit();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.RX_CHX_RAM_INDEX.EndInit();
            this.RX_CH4_RAM_INDEX.EndInit();
            this.RX_CH3_RAM_INDEX.EndInit();
            this.RX_CH2_RAM_INDEX.EndInit();
            this.RX_CH1_RAM_INDEX.EndInit();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.LNA_BIAS.EndInit();
            this.RX_VM_BIAS2.EndInit();
            this.RX_VGA_BIAS2.EndInit();
            this.TRControl.ResumeLayout(false);
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.GPIOpins.ResumeLayout(false);
            this.TestmodesPanel.ResumeLayout(false);
            this.TestmodesPanel.PerformLayout();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.RX_BIAS_RAM_INDEX.EndInit();
            this.TX_BIAS_RAM_INDEX.EndInit();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.TX_BEAM_STEP_START.EndInit();
            this.RX_BEAM_STEP_STOP.EndInit();
            this.RX_BEAM_STEP_START.EndInit();
            this.TX_BEAM_STEP_STOP.EndInit();
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.TX_TO_RX_DELAY_1.EndInit();
            this.RX_TO_TX_DELAY_2.EndInit();
            this.RX_TO_TX_DELAY_1.EndInit();
            this.TX_TO_RX_DELAY_2.EndInit();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.NVM_DIN.EndInit();
            this.NVM_ADDR_BYP.EndInit();
            this.NVM_BIT_SEL.EndInit();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.LDO_TRIM_BYP_C.EndInit();
            this.LDO_TRIM_BYP_B.EndInit();
            this.LDO_TRIM_BYP_A.EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.LDO_TRIM_REG.EndInit();
            this.LDO_TRIM_SEL.EndInit();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.MISC.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.LNA_BIAS_OFF.EndInit();
            this.CH1PA_BIAS_OFF.EndInit();
            this.CH4PA_BIAS_OFF.EndInit();
            this.CH2PA_BIAS_OFF.EndInit();
            this.CH3PA_BIAS_OFF.EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.LNA_BIAS_ON.EndInit();
            this.CH1PA_BIAS_ON.EndInit();
            this.CH4PA_BIAS_ON.EndInit();
            this.CH2PA_BIAS_ON.EndInit();
            this.CH3PA_BIAS_ON.EndInit();
            this.BeamSequencer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize)this.PolarPlot).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.TimeDelay.EndInit();
            this.EndPoint.EndInit();
            this.StartPoint.EndInit();
            this.PhaseLoop.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.numericUpDown1.EndInit();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.Demo_loop_time.EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ManualRegWrite.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ReadBack.ResumeLayout(false);
            this.ReadBack.PerformLayout();
            ((ISupportInitialize)this.dataGridView1).EndInit();
            this.PasswordPanel.ResumeLayout(false);
            this.PasswordPanel.PerformLayout();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            //BeamSweeping Graphic By Hyunsoo
            this.BeamSweeping_GUI_Panel_0.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_0.PerformLayout();
            this.BeamSweeping_GUI_Panel_T.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_T.PerformLayout();
            this.BeamSweeping_GUI_Panel_R.ResumeLayout(false);
            this.BeamSweeping_GUI_Panel_R.PerformLayout();


            //Socket Communication By Hyeong wook

            //this.EventLog.Location = new Point(5, 516);
            //this.EventLog.Margin = new Padding(4);
            //this.EventLog.Multiline = true;
            //this.EventLog.Name = "EventLog";
            //this.EventLog.ReadOnly = true;
            //this.EventLog.ScrollBars = ScrollBars.Vertical;
            ////
            //this.EventLog.Size = new Size(650, 500);//650
            ////이게 아래 이벤트로그 전체 길이
            //this.EventLog.TabIndex = 11;
            //this.EventLog.Text = "Application started.";

            this.richTextBox9001 = new System.Windows.Forms.RichTextBox();
            this.textBox9001 = new System.Windows.Forms.TextBox();
            this.listBox9001 = new System.Windows.Forms.ListBox();
            this.button9001 = new System.Windows.Forms.Button();
            this.groupBox9001 = new System.Windows.Forms.GroupBox();
            this.label9001 = new System.Windows.Forms.Label();
            this.groupBox9001.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox9001
            // 
            this.richTextBox9001.Location = new Point(580, 390);
            this.richTextBox9001.Name = "richTextBox9001";
            this.richTextBox9001.Size = new Size(465, 247);
            this.richTextBox9001.TabIndex = 0;
            this.richTextBox9001.Text = "";
            this.richTextBox9001.TextChanged += new System.EventHandler(this.RichTextBox9001_TextChanged);
            // 
            // textBox9001
            // 
            //this.textbox9001.location = new system.drawing.point(660, 837);
            //this.textbox9001.name = "textbox9001";
            //this.textbox9001.size = new system.drawing.size(776, 21);
            //this.textbox9001.tabindex = 9001;
            // 
            // listBox1
            // 
            this.listBox9001.FormattingEnabled = true;
            this.listBox9001.ItemHeight = 12;
            this.listBox9001.Location = new System.Drawing.Point(580, 647);
            this.listBox9001.Name = "listBox9001";
            this.listBox9001.Size = new System.Drawing.Size(465, 88);
            this.listBox9001.TabIndex = 9002;
            this.listBox9001.SelectedIndexChanged += new System.EventHandler(this.ListBox9001_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button9001.Location = new System.Drawing.Point(15, 19);
            this.button9001.Name = "button9001";
            this.button9001.Size = new System.Drawing.Size(75, 23);
            this.button9001.TabIndex = 9003;
            this.button9001.Text = "시작";
            this.button9001.UseVisualStyleBackColor = true;
            //this.button9001.Click += new System.EventHandler(this.Button9001_Click);
            // 
            // groupBox1
            // 
            this.groupBox9001.Controls.Add(this.label9001);
            this.groupBox9001.Controls.Add(this.button9001);
            this.groupBox9001.Location = new System.Drawing.Point(12, 12);
            this.groupBox9001.Name = "groupBox9001";
            this.groupBox9001.Size = new System.Drawing.Size(776, 48);
            this.groupBox9001.TabIndex = 9000;
            this.groupBox9001.TabStop = false;
            this.groupBox9001.Text = "서버 상태";
            // 
            // label1
            // 
            this.label9001.AutoSize = true;
            this.label9001.Location = new System.Drawing.Point(96, 24);
            this.label9001.Name = "label1";
            this.label9001.Size = new System.Drawing.Size(85, 12);
            this.label9001.TabIndex = 9003;
            this.label9001.Text = "서버 초기화 전";

            // 
            // server_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox9001);
            this.Controls.Add(this.listBox9001);
            this.Controls.Add(this.textBox9001);
            this.Controls.Add(this.richTextBox9001);
            this.Name = "server_form";
            this.Text = "서버";
            this.Load += new System.EventHandler(this.Form9001_Load);
            this.groupBox9001.ResumeLayout(false);
            this.groupBox9001.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
