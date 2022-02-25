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

namespace Core
{
    public partial class Form1 : Form
    {
        private string filePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void Open(object sender, EventArgs e)
        {
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            textBox1.Text = fileContent;
        }

        private void Save(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Сохранить изменения в файле?", "Созрел вопрос:", 
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                if (filePath.Equals(string.Empty))
                {
                    MessageBox.Show("Файл не выбран");
                }
                else
                {
                    File.WriteAllText(filePath, textBox1.Text);
                }
            }
        }

        private void SaveAs(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {

                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
                    filePath = saveFileDialog.FileName;
                }
            }
        }

        private void Copy(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void Cut(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
        }
    }
}
