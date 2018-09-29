public class tsProject
{
    public void tsProject(string directory)
    {
        fileContents = File.ReadAllLines(directory); //Contents of the .tsproject
        MessageBox.Show(fileContents.Length.ToString());
        for (int line = 0; line < fileContents.Length - 1; line++)
        {
            if (fileContents[line] != null)
            {
                fileContents[line] = fileContents[line].Replace(" ", "").Replace("project{", "");
                shift(); //Shifts the array 1 to the left Eg. entry 0 becomes 1, 1 becomes 2 etc...
                fileContents[fileContents.Length - 1] = null;

                #region If Statements
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
                    tpsFiles[tpsFilesPos] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                    tpsFilesPos++;
                }
                else if (fileContents[line].Contains("include") && fileContents[line].Contains("tpf"))
                {
                    tpfFiles[tpfFilesPos] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                    tpfFilesPos++;
                }
                else if (fileContents[line].Contains("include") && fileContents[line].Contains("tpfdesigner"))
                {
                    tpfdesignerFiles[tpfDesignerFilesPos] = fileContents[line].Replace("include\"", "").Replace("\";", "");
                    tpfDesignerFilesPos++;
                }
                else if (fileContents[line].Contains("Reference"))
                {
                    references[referencesPos] = fileContents[line].Replace("Reference\"", "").Replace("\";", "");
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
            for (int index = 0; index <= tpfFilesPos; index++)
            {
                projectFileP2[projectFileP2.Length] = "<Compile Include=\"" + tpfFiles[index].Replace(".tpf", ".cs") + "\"><SubType>Form</SubType></Compile>";
            }
        }

        if (tpfDesignerFilesPos != 0)
        {
            for (int index = 0; index <= tpfDesignerFilesPos; index++)
            {
                projectFileP2[projectFileP2.Length] = "<Compile Include=\"" + tpfdesignerFiles[index].Replace(".tpfdeigner", ".Designer.cs") + "\"><DependentUpon>" + tpfFiles[index].Replace(".tpf", ".cs") + "</DependetUpon></Compile>";
            }
        }

        if (tpsFilesPos != 0)
        {
            for (int index = 0; index <= tpsFilesPos; index++)
            {
                projectFileP2[projectFileP2.Length] = "<Compile Include=\"" + tpsFiles[index].Replace(".tps", ".cs") + "\" />";
            }
        }

        if (referencesPos != 0)
        {
            for (int index = 0; index <= referencesPos; index++)
            {
                projectFileP3[projectFileP3.Length] = "<Content Include=\"" + references[index] + "\" />";
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

        string csProject = Environment.CurrentDirectory + "\\" + assemblyName + ".csproject";
        File.WriteAllLines(csProject, projectFileP1);
        File.AppendAllLines(csProject, projectFileP2);
        File.AppendAllLines(csProject, projectFileP3);
        File.AppendAllLines(csProject, projectFileP4);
    }
}