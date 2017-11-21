using System;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sorter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnRun;
        private ListBox NamesListBox;
        private Button button1;
        private List<Name> NamesList = new List<Name>();
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnRun = new System.Windows.Forms.Button();
            this.NamesListBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(27, 21);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(146, 48);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "&Display the list";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // NamesListBox
            // 
            this.NamesListBox.FormattingEnabled = true;
            this.NamesListBox.Location = new System.Drawing.Point(27, 86);
            this.NamesListBox.Name = "NamesListBox";
            this.NamesListBox.Size = new System.Drawing.Size(327, 186);
            this.NamesListBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Sort the List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(404, 298);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NamesListBox);
            this.Controls.Add(this.btnRun);
            this.Name = "Form1";
            this.Text = "Sorter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        
		private void btnRun_Click(object sender, System.EventArgs e)
		{    
            string line;
            StreamReader reader;

            //Emptying the list of names if the "Display Button"
            //is clicked for second time during the program; inorder 
            //to avoid duplicate entries into the list.

            if(NamesList.Count > 0)
            {
                NamesList.Clear();
            }
            
            try {
                //Reading the file from the given path
                reader = new StreamReader("input.txt");

                //Reading the file line by line
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != string.Empty)
                    {
                        //Removing extra empty spaces from line
                        line = Regex.Replace(line.Trim(), @"\s+", " ");

                        //Putting each word in the line into array
                        string[] words = line.Split(' ');

                        //If both firstname and lastname exist creating an object of Name
                        if (words.Length == 2)
                            NamesList.Add(new Name(words[0], words[1]));

                        //If lastname doesnot exist give it an empty string creating an object of Name
                        else if (words.Length == 1)
                        {
                            NamesList.Add(new Sorter.Name(words[0], ""));
                        }
                    }
                }
                //Displaying list into the listbox
                NamesListBox.DataSource = NamesList;
            }
            //throwing exception if the file was not found on the given path
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "!");
                
            }

            //Enabling the "Sort the list" button
            button1.Enabled = true;

		}

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                //Sorting the list 
                IEnumerable<Name> sorted_list = NamesList.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();


                //Creating a file name sortedList.txt and storing names in a sorted way
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("sortedList.txt"))
                {
                    foreach (Name name in sorted_list)
                    {
                        file.WriteLine(name);
                    }
                    file.Close();
                }

                //Displaying the sorted list of names in listbox
                NamesListBox.DataSource = sorted_list;

            }
             catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "!");
            }
           

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Disabling the "Sort the List" button since the list of Names is empty in the beginning
            button1.Enabled = false;
        }
    }
}
