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
        string[] projectFile;
        string[] tpfFiles;
        string[] tpsFiles;
        string[] tpfdesignerFiles;
        string[] references;
        string outputType;
        string assemblyName;
        string targetVersion;

        public void convertFile(string directory)
        {
            fileContents = File.ReadAllLines(directory);
            string Namespace;
            for (int line = 0; line < fileContents.Length; line++)
            {
                fileContents[line] = fileContents[line].Replace(" ", "").Replace("project{", "");
                shift(); //Shifts the array 1 to the left Eg. entry 0 becomes 1, 1 becomes 2 etc...

                if (fileContents[line].Contains("Namespace"))
                {
                    Namespace = fileContents[line].Replace("Namespace=\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("OutputType"))
                {
                    outputType = fileContents[line].Replace("OutputType=\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("AssemblyName"))
                {
                    assemblyName = fileContents[line].Replace("AssemblyName=\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("TargetFramework"))
                {
                    targetVersion = fileContents[line].Replace("TargetFramework=\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("include") && fileContents[line].Contains("tps"))
                {
                    tpsFiles[tpsFiles.Length] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("include") && fileContents[line].Contains("tpf"))
                {
                    tpfFiles[tpfFiles.Length] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("include") && fileContents[line].Contains("tpfdesigner"))
                {
                    tpfdesignerFiles[tpfdesignerFiles.Length] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                }
                else if (fileContents[line].Contains("Reference"))
                {
                    references[references.Length] = fileContents[line].Replace("Reference\"", "").Replace("\";", "");
                }
            }
        }

        private void compileCodeClick(object sender, EventArgs e)
        {
            if (chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                convertFile(chooseFileDialog.FileName);
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
