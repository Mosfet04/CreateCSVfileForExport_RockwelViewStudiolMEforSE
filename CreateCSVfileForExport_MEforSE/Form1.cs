using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;
using System.Xml;
using System.IO;


namespace CreateCSVfileForExport_MEforSE
{
    public partial class Form1 : Form
    {
        private static void create_tag(string arq_xml, string path, string nome_arq)
        {
            StringBuilder resultName = new StringBuilder();
            StringBuilder resultPLC = new StringBuilder();
            StringBuilder resultError = new StringBuilder();
            //Carregando xml
            XDocument xdoc = XDocument.Load(arq_xml+@"\"+nome_arq);

            //Run query
            var lv1s = from lv1 in xdoc.Descendants("triggers")
                       select new
                       {
                           PLC = lv1.Descendants("trigger")
                       };
            var Lv1s = from Lv1 in xdoc.Descendants("messages")
                       select new
                       {
                           ErrorText = Lv1.Descendants("message")
                       };
            //Loop buscando variaveis de interesse
            foreach (var lv1 in lv1s)
            {

                foreach (var lv2 in lv1.PLC)
                {
                    resultName.AppendLine(lv2.Attribute("exp").Value);
                    resultPLC.AppendLine(lv2.Attribute("exp").Value);
                }
            }
            foreach (var Lv1 in Lv1s)
            {

                foreach (var Lv2 in Lv1.ErrorText)
                    resultError.AppendLine(Lv2.Attribute("text").Value);
            }
            resultName = resultName.Replace("{", @"Alarmes_IHM\").Replace("[", "").Replace("]", "_").Replace("}", "_");
            resultPLC = resultPLC.Replace("{", "").Replace("}", "");

            //-------------------------------------------------------------TAG's CSV
            string[] delim = { Environment.NewLine, "\n" };
            string[] ResultName = resultName.ToString().Split(delim, StringSplitOptions.None);
            string[] ResultPLC = resultPLC.ToString().Split(delim, StringSplitOptions.None);

            int i = ResultName.Length - 1;
            string[] tag = new string[i + 1];
            for (int cont = 0; cont < i; cont++)
            {
                tag[cont + 1] = "D,\"" + ResultName[cont] + "\",,\"F\",\"D\",\"*\",\"F\",,,,,,,,,,\"Off\",\"On\",\"Off\",,,,\"" + ResultPLC[cont] + "\"";
            }
            string csv = ";Tag Type, Tag Name, Tag Description, Read Only, Data Source, Security Code, Alarmed, Native Type, Value Type, Min Analog, Max Analog, Initial Analog, Scale, Offset, DeadBand, Units, Off Label Digital, On Label Digital, Initial Digital, Length String, Initial String, Retentive, Address, System Source Name, System Source Index, RIO Address, Element Size Block, Number Elements Block, Initial Block\n" + ";###002 - THIS LINE CONTAINS VERSION INFORMATION. DO NOT REMOVE!!!\n" + "\n" + ";Folders Section (Must define folders before tags)\n" + "\"F\",\"Alarm_IHM\",,\"F\"\n\n" + ";Tag Section\n";
            tag[0] = csv;


            //string path = @"c:\Users\Public\Documents\TagsCsv.csv";
          
            File.WriteAllLines(path + @"\IMPORT.csv", tag, Encoding.UTF8);

        }
        public Form1(string arq_xml, string path, int ok, string nome_arq)
        {
            InitializeComponent();

            if (ok == 1)
            {
                try
                {
                    create_tag(arq_xml, path, nome_arq);
                }
                catch (Exception ex)
                {
                    // Display an error message

                    MessageBox.Show("Verifique os dados informados");
                }
                ok = 0;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(textBox1.Text, textBox2.Text,1,textBox3.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
