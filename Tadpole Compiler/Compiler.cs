using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tadpole_Compiler
{
    public partial class Compiler : Form
    {
        public Compiler()
        {
            InitializeComponent();
        }

        string[] fileContents;

        public void convertFile(string directory, string type)
        {
            fileContents = File.ReadAllLines(directory);
            switch (type)
            {
                case "ProjectFile":
                    for (int line = 0; line < fileContents.Length; line++)
                    {
                        fileContents[line] = fileContents[line].Replace(" ", "").Replace("project{", "");
                        shift();

                        if (fileContents[line].Contains(""))
                        {

                        }
                    }
                    break;
                case "Form":
                    break;
                case "Form Designer":
                    break;
                case "Script":
                    break;
            }
        }

        private void compileCodeClick(object sender, EventArgs e)
        {
            if (chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                convertFile(chooseFileDialog.FileName, "ProjectFile");
            }
        }

        private void shift()
        {
            for (int index = 0; index < fileContents.Length - 1; index++)
            {
                fileContents[index] = fileContents[index + 1];
            }
            fileContents[fileContents.Length - 1] = null;
        }
    }
}
