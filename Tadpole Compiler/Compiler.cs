using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public void convertFile(string directory, string type)
        {
            switch (type)
            {
                case "ProjectFile":
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
    }
}
