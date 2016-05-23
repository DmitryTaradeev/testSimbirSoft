using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TestProject
{
    public partial class Form1 : Form
    {
        OpenFileDialog dialogDictionaryFile = new OpenFileDialog();
        OpenFileDialog dialogTextFile = new OpenFileDialog();
        SaveFileDialog dialogHtml = new SaveFileDialog();
        Thread t;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            dialogDictionaryFile.InitialDirectory = Environment.CurrentDirectory; ;
            dialogDictionaryFile.Filter = "Текстовые файлы|*.txt";
            dialogDictionaryFile.RestoreDirectory = true;
            dialogDictionaryFile.ShowDialog();
            labelFileDictionary.Text = dialogDictionaryFile.FileName;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dialogTextFile.InitialDirectory = Environment.CurrentDirectory;
            dialogTextFile.Filter = "Текстовые файлы|*.txt";
            dialogTextFile.RestoreDirectory = true;
            dialogTextFile.ShowDialog();
            labelFileText.Text = dialogTextFile.FileName;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dialogHtml.InitialDirectory = Environment.CurrentDirectory; 
            dialogHtml.Filter = "Html документы|*.html";
            dialogHtml.ShowDialog();
            labelHtmlFiles.Text = dialogHtml.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (!ReadFile.CheckFile(dialogTextFile.FileName) || !ReadFile.CheckFile(dialogDictionaryFile.FileName) )
                MessageBox.Show("Ошибка чтения файла");            
            else
            {
                t = new Thread(delegate() { ReadFile.ReadAndWrite(dialogDictionaryFile.FileName, dialogTextFile.FileName, dialogHtml.FileName); });
                
            }
        }

       

       

    }
}
