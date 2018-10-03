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

        string[] projectFileP1 = File.ReadAllLines(Environment.CurrentDirectory + "\\Files\\pAP1"); //Basic information (namepace etc.)
        string[] projectFileP2 = File.ReadAllLines(Environment.CurrentDirectory + "\\Files\\pAP2"); //Including files in build
        string[] projectFileP3 = File.ReadAllLines(Environment.CurrentDirectory + "\\Files\\pAP3"); //Referencing '.dll's and other files
        string[] projectFileP4 = File.ReadAllLines(Environment.CurrentDirectory + "\\Files\\pAP4"); //Closing everything
        string[] projectFileWP1 = new string[100];
        string[] projectFileWP2 = new string[100];
        string[] projectFileWP3 = new string[100];
        string[] projectFileWP4 = new string[100];
        string[] fileContents;
        string[] tpfFiles = new string[100];
        string[] tpsFiles = new string[100];
        string[] tpfdesignerFiles = new string[100];
        string[] references = new string[100];
        int tpfFilesPos = 0;
        int tpsFilesPos = 0;
        int tpfDesignerFilesPos = 0;
        int referencesPos = 0;
        int pAP1Length = 44;
        int pAP2Length = 2;
        int pAP3Length = 3;
        int pAP4Length = 3;
        string outputType;
        string assemblyName;
        string targetVersion;
        string Namespace;
        string directory;

        public void convertFile(string directory)
        {
            fileContents = File.ReadAllLines(directory); //Contents of the .tsproject
            shift(); //Shifts the array 1 to the left Eg. entry 0 becomes 1, 1 becomes 2 etc...
            fileContents[fileContents.Length - 1] = null;

            for (int index = 0; index < projectFileP1.Length; index++)
            {
                projectFileWP1[index] = projectFileP1[index];
            }

            for (int index = 0; index < projectFileP2.Length; index++)
            {
                projectFileWP2[index] = projectFileP2[index];
            }

            for (int index = 0; index < projectFileP3.Length; index++)
            {
                projectFileWP3[index] = projectFileP3[index];
            }

            for (int index = 0; index < projectFileP4.Length; index++)
            {
                projectFileWP4[index] = projectFileP4[index];
            }

            for (int line = 0; line < fileContents.Length - 1; line++)
            {
                if (fileContents[line] != null)
                {
                    string currentLine = fileContents[line]; //Setting 'currentLine' to the line of the file that the for loop is on
                    char tab = '\u0009'; //Setting the variable 'tab' to the tab character
                    currentLine = currentLine.Replace(tab.ToString(), "").Replace(" ", "").Replace("\";", "").Replace("=\"", ""); //Removing Tabs, Spaces, `";` and `="`
                    
                    #region Getting Information from .tsproject
                    if (currentLine.Contains("Namespace"))
                    {
                        Namespace = currentLine.Replace("Namespace", "");
                    }
                    else if (currentLine.Contains("OutputType"))
                    {
                        outputType = currentLine.Replace("OutputType", "");
                    }
                    else if (currentLine.Contains("AssemblyName"))
                    {
                        assemblyName = currentLine.Replace("AssemblyName", "");
                    }
                    else if (currentLine.Contains("TargetFramework"))
                    {
                        targetVersion = currentLine.Replace("TargetFramework", "");
                    }
                    else if (currentLine.Contains("include") && currentLine.Contains("tps"))
                    {
                        tpsFiles[tpsFilesPos] = currentLine.Replace("include\"", "");
                        tpsFilesPos++;
                    }
                    else if (currentLine.Contains("include") && currentLine.Contains("tpfdesigner"))
                    {
                        tpfdesignerFiles[tpfDesignerFilesPos] = currentLine.Replace("include\"", "");
                        tpfDesignerFilesPos++;
                    }
                    else if (currentLine.Contains("include") && currentLine.Contains("tpf"))
                    {
                        tpfFiles[tpfFilesPos] = currentLine.Replace("include\"", "");
                        tpfFilesPos++;
                    }
                    else if (currentLine.Contains("Reference"))
                    {
                        references[referencesPos] = currentLine.Replace("Reference\"", "");
                        referencesPos++;
                    }
                    #endregion

                }
                
            }

            if (tpfFiles.Length != tpfdesignerFiles.Length)
            {
                if (tpfFiles.Length < tpfdesignerFiles.Length)
                {
                    MessageBox.Show("You are missing a .tpf file! Please make the .tpf file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("You are missing a .tpfdesigner file! Please make the .tpfdesinger file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (tpfFilesPos != 0)
            {
                for (int index = 0; index < tpfFilesPos; index++) //For each tpf file there is
                {
                    projectFileWP2[pAP2Length + index] = "<Compile Include=\"" + tpfFiles[index].Replace(".tpf", ".cs") + "\"><SubType>Form</SubType></Compile>"; //Converting it to XML
                    pAP2Length++;
                }
            }
            
            if (tpfDesignerFilesPos != 0)
            {
                for (int index = 0; index < tpfDesignerFilesPos; index++) //For each tpfDesigner file there is
                {
                    projectFileWP2[pAP2Length + index] = "<Compile Include=\"" + tpfdesignerFiles[index].Replace(".tpfdesigner", ".Designer.cs") + "\"><DependentUpon>" + tpfFiles[index].Replace(".tpf", ".cs") + "</DependentUpon></Compile>"; //Converting it to XML
                    pAP2Length++;
                }
            }

            if (tpsFilesPos != 0)
            {
                for (int index = 0; index < tpsFilesPos; index++) //For each tps file there is
                {
                    projectFileWP2[pAP2Length + index] = "<Compile Include=\"" + tpsFiles[index].Replace(".tps", ".cs") + "\" />"; //Converting it to XML
                    pAP2Length++;
                }
            }

            if (referencesPos != 0)
            {
                for (int index = 0; index < referencesPos; index++) //For each reference there is
                {
                    projectFileWP3[pAP3Length + index] = "<Content Include=\"" + references[index] + "\" />"; //Converting it to XML
                    pAP3Length++;
                }
            }

            if (Namespace == null)
            {
                MessageBox.Show("No Namespace Specified! You need a namespace!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (targetVersion == null)
            {
                if (MessageBox.Show("No Target Version Specified! Do you want to continue and use .net framework v4.6.1?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    targetVersion = "v4.6.1";
                }
                else
                {
                    return;
                }
            }

            if (assemblyName == null)
            {
                if (MessageBox.Show("No Assembly Name Specified! Do you want to continue and set it the same as your Namespace?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    assemblyName = Namespace;
                }
                else
                {
                    return;
                }
            }

            if (outputType == null)
            {
                if (MessageBox.Show("No Output Type Specified! Do you want to continue and set it to WinExe?", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    outputType = "winExe";
                }
                else
                {
                    return;
                }
            }

            projectFileWP1[7] = "<OutputType>" + outputType + "</OutputType>"; //Converting the Output Type to XML
            projectFileWP1[8] = "<RootNamespace>" + Namespace + "</RootNamespace>"; //Converting the Namespace to XML
            projectFileWP1[9] = "<AssemblyName>" + assemblyName + "</AssemblyName>"; //Converting the Assembly Name to XML
            projectFileWP1[10] = "<TargetFrameworkVersion>" + targetVersion + "</TargetFrameworkVersion>"; //Converting the Targetted Framework Version to XML

            projectFileWP1 = projectFileWP1.Where(x => !string.IsNullOrEmpty(x)).ToArray(); //Removing blank entries of the array
            projectFileWP2 = projectFileWP2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            projectFileWP3 = projectFileWP3.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            projectFileWP4 = projectFileWP4.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            string csProject = directory + "\\" + assemblyName + ".csproj";
            File.WriteAllLines(csProject, projectFileWP1);
            File.AppendAllLines(csProject, projectFileWP2);
            File.AppendAllLines(csProject, projectFileWP3);
            File.AppendAllLines(csProject, projectFileWP4);
        }

        private void compileCodeClick(object sender, EventArgs e)
        {
            if (chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                convertFile(chooseFileDialog.FileName);
                directory = Path.GetDirectoryName(chooseFileDialog.FileName);
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
