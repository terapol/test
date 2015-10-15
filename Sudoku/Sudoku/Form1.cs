using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string[,] sudoku1 = new string[,]
			{
				{ "" , "", "" ,"2","6","" ,"7","" ,"1"},
				{ "6","8", "" ,"" ,"7","" ,"" ,"9","" },
				{ "1","9", "" ,"" ,"" ,"" ,"" ,"" ,"" },
				{ "8","2", "" ,"1","" ,"" ,"" ,"4","" },
				{ "" ,"" , "4","6","" ,"2","9","" ,"" },
				{ "" ,"5", "" ,"" ,"" ,"3","" ,"2","8"},
				{ "" ,"" , "9","3","" ,"" ,"" ,"7","4"},
				{ "" ,"4", "" ,"" ,"5","" ,"" ,"3","6"},
				{ "7","" , "3","" ,"1","8","" ,"" ,"" },
			};
			BoxLoad(sudoku1);
		}
		private void BoxLoad(string[,] stringMatrix)
		{
			int counter = 1;
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					TextBox textbox = (TextBox)Controls.Find(string.Format("textBox{0}", counter), false).FirstOrDefault();
					textbox.Text = stringMatrix[i, j];
					if (stringMatrix[i,j] != "")
					{
						textbox.ReadOnly = true;
					}
					counter++;
                }
			}
		}

		private void checkHorizontal()
		{

		}
	}
}
