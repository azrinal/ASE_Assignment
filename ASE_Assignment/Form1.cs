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

namespace ASE_Assignment
{
    /// <summary>
    /// Our GraphicsProgram is inheriting Form class
    /// </summary>
    public partial class GraphicsProgram : Form
    {
        /// <summary>
        /// This is the Class defination of GrpahicsProgram Class
        /// </summary>
        public GraphicsProgram()
        {
            InitializeComponent();
        }

        Creator factory = new FactoryClass();
        //Defining Instance Variables
        int x = 0, y = 0, width, height, radius;
        /// <summary>
        /// It saves the data intested on the rich text box to the destination folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(txtInputCode.Text);
                write.Close();
                MessageBox.Show("File Saved Successfully");
            }
        }

        private void GraphicsProgram_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// The action performed is to clear the Output Panel and code on the rich text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInputCode.Clear();
            pnlOutput.Refresh();
        }

        private void txtInputCode_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// It enables to upload text files and exectues the codes in it through the rich text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse file from specified folder";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "DOCX files (*.docx)|*.docx|All files (*.*)|*.*";

            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Browse .txt file and docx from computer

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {                         
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                
                txtInputCode.Text = File.ReadAllText(openFileDialog1.FileName);

            }
        }

        public void moveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }
        public void drawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }
        /// <summary>
        /// This method executes the commands provided from the user from the rich text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            Graphics g = pnlOutput.CreateGraphics();
            
            string command = txtInputCode.Text.ToLower();
            string[] commandline = command.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int k = 0; k < commandline.Length; k++)
            {

                string[] cmd = commandline[k].Split(' ');
                if (cmd[0].Equals("moveto") == true)
                {
                    pnlOutput.Refresh();
                    string[] param = cmd[1].Split(',');

                    if (param.Length != 2)
                    {
                        MessageBox.Show("Incorrect Parameter");

                    }
                    else
                    {

                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        moveTo(x, y);
                    }


                }

                else if (cmd[0].Equals("drawto") == true)
                {
                    string[] param = cmd[1].Split(',');
                    int x = 0, y = 0;
                    if (param.Length != 2)
                    {
                        MessageBox.Show("Incorrect Parameter");

                    }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        drawTo(x, y);
                    }

                }
                else if (cmd[0].Equals("circle") == true)
                {
                    if (cmd.Length != 2)
                    {
                        MessageBox.Show("Incorrect Parameter");

                    }
                    else
                    {

                        if (cmd[1].Equals("radius") == true)
                        {
                            IShape circle = factory.getShape("circle");
                            circle.set(x, y, radius);
                            circle.draw(g);

                        }
                        else
                        {


                            Int32.TryParse(cmd[1], out radius);
                            IShape c = factory.getShape("circle");
                            c.set(x, y, radius);
                            c.draw(g);
                        }
                    }

                }
                else if (cmd[0].Equals("rectangle") == true)
                {
                    if (cmd.Length < 2)
                    {
                        MessageBox.Show("Invalid Parameter ");
                    }
                    else
                    {
                        string[] param = cmd[1].Split(',');
                        if (param.Length != 2)
                        {
                            MessageBox.Show("Invalid Parameter ");
                        }
                        else
                        {
                            Int32.TryParse(param[0], out width);
                            Int32.TryParse(param[1], out height);
                            IShape r = factory.getShape("rectangle");
                            r.set(x, y, width, height);
                            r.draw(g);
                        }
                    }
                }

                else if (cmd[0].Equals("triangle") == true)
                {
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    {
                        MessageBox.Show("Incorrect Parameter");

                    }
                    else
                    {

                        Int32.TryParse(param[0], out width);
                        Int32.TryParse(param[1], out height);
                        IShape r = factory.getShape("triangle");
                        r.set(x, y, width, height);
                        r.draw(g);
                    }

                }

                else if (!cmd[0].Equals(null))
                {
                    int errorLine = k + 1;
                    MessageBox.Show("Invalid command recognised on line " + errorLine, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}