using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChangeFormGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SetDateToControl(dateTimePicker1, Properties.Settings.Default.StartDate);
            SetDateToControl(dateTimePicker2, Properties.Settings.Default.EndDate);

           

            Properties.Settings.Default.Save();

           


        }

        private void SetDateToControl(DateTimePicker dtp, DateTime assignDate)
        {
            if (assignDate.Equals(DateTime.MinValue))
            {
                dtp.Value = DateTime.Now;
            }
            else
            {
                dateTimePicker1.Value = assignDate;
            }

        }
              

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GenerateEmailBody();
        }

        private void GenerateEmailBody()
        {
            var sb = new StringBuilder();


            sb.AppendLine("Change Control Form");
            sb.AppendLine($"Start Date/Time:\t\t {dateTimePicker1.Value:yyyy-MM-dd} {dateTimePicker3.Value:HH:mm} CST");
            sb.AppendLine($"Scheduled End Date/Time:\t {dateTimePicker2.Value:yyyy-MM-dd} {dateTimePicker4.Value:HH:mm} CST");
            sb.AppendLine();
            var titletype = IsMDORCommon() ? comboBoxType.Text : comboBoxType2.Text;
            sb.AppendLine($"DOR Title:      IA  -  {titletype}  -  {comboBoxEnvironment.Text}  migration");
            sb.AppendLine();
            sb.AppendLine("Library Control Form");
            sb.AppendLine($"Priority:      {comboBoxPriority.Text}");
            var userapptitle = IsUserApps() ? "" : "BatchApps";
            if (!IsUserApps()) sb.AppendLine(userapptitle);
            sb.AppendLine();
            var userappprocess = (IsUserApps() || IsStandard()) ? comboBoxType2.Text : "";
            sb.AppendLine($"{userappprocess} Build,  distribute,  deploy  {titletype} {comboBoxEnvironment.Text}   B{textBox3.Text} ");
            sb.AppendLine();
            sb.AppendLine($"SQR:  {txtBoxSQR.Text}");
            sb.AppendLine($"{textBox5.Text}");

            



            textBox2.Text = sb.ToString();

           
        }

        private void GenerateEmailSubject()
        {
            var sb = new StringBuilder();
            var subjectTitle = IsStandard() ? comboBoxType2.Text : comboBoxType.Text;  
            sb.Append($"IA  {subjectTitle} {comboBoxEnvironment.Text}  Change Form");

            textBox1.Text = sb.ToString();

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            GenerateEmailSubject();GenerateEmailSubject();
        }

        private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
        {
            GenerateEmailBody();
            GenerateEmailSubject();

        }
        private void comboBoxType_SelectedValueChanged2(object sender, EventArgs e)
        {
            GenerateEmailBody();
            GenerateEmailSubject();

        }

        private bool IsStandard()
        {
            return comboBoxType.Text.Equals("Standard");

        }
        private bool IsUserApps()
        {
            return comboBoxType.Text.Equals("User Apps");

        }
        private bool IsMDORCommon()
        {
            return comboBoxType.Text.Equals("MDOR Common");

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Properties.Settings.Default.StartDate = dateTimePicker1.Value ;
            Properties.Settings.Default.Save();
        }
    }
}
