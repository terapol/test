using System;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sudoku
{
	public partial class Form1 : Form
	{
		Stopwatch stopWatch = new Stopwatch();

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string[,] sudoku1 = new string[,]
			{
				{ "" ,"" ,"" ,"2","6","" ,"7","" ,"1"},
				{ "6","8","" ,"" ,"7","" ,"" ,"9","" },
				{ "1","9","" ,"" ,"" ,"" ,"" ,"" ,"" },
				{ "8","2","" ,"1","" ,"" ,"" ,"4","" },
				{ "" ,"" ,"4","6","" ,"2","9","" ,"" },
				{ "" ,"5","" ,"" ,"" ,"3","" ,"2","8"},
				{ "" ,"" ,"9","3","" ,"" ,"" ,"7","4"},
				{ "" ,"4","" ,"" ,"5","" ,"" ,"3","6"},
				{ "7","" ,"3","" ,"1","8","" ,"" ,"" },
			};
			string[,] sudoku2 = new string[,]
			{
				{ "1","" ,"" ,"4","8","9","" ,"" ,"6"},
				{ "7","3","" ,"" ,"" ,"" ,"" ,"4","" },
				{ "1","9","" ,"" ,"" ,"1","2","9","5"},
				{ "" ,"" ,"7","1","2","" ,"6","" ,"" },
				{ "5","" ,"" ,"7","" ,"3","" ,"" ,"8"},
				{ "" ,"" ,"6","" ,"9","5","7","" ,"" },
				{ "9","1","4","6","" ,"" ,"" ,"" ,"" },
				{ "" ,"2","" ,"" ,"" ,"" ,"" ,"3","7"},
				{ "8","","" ,"5","1","2","" ,"" ,"4"},
			};
			string[,] sudoku3 = new string[,]
			{
				{ "" ,"2","" ,"6","" ,"8","" ,"" ,"" },
				{ "5","8","" ,"" ,"" ,"9","7","" ,"" },
				{ "" ,"" ,"" ,"" ,"4","" ,"" ,"" ,"" },
				{ "3","7","" ,"" ,"" ,"" ,"5","" ,"" },
				{ "6","" ,"" ,"" ,"" ,"" ,"" ,"" ,"4"},
				{ "" ,"" ,"8","" ,"" ,"" ,"" ,"1","3"},
				{ "" ,"" ,"" ,"" ,"2","" ,"" ,"" ,"" },
				{ "" ,"" ,"9","8","" ,"" ,"" ,"3","6"},
				{ "" ,"" ,"" ,"3","" ,"6","" ,"9","" },
			};
			BoxLoad(sudoku1,sudoku2,sudoku3);

			for (int i = 0; i < Controls.Count; i++)
			{
				if(Controls[i] is TextBox)
				{
					Controls[i].TextChanged += new EventHandler(OnTextChanged);
				}
			}

			stopWatch.Start();
		}
		private void BoxLoad(string[,] sudokuMatrix1, string[,] sudokuMatrix2, string[,] sudokuMatrix3)
		{
			int generatedRandomNumber;
			Random random = new Random();
			generatedRandomNumber = random.Next(1,4);
			string[,] randomSudoku = new string[,] { };

			if(generatedRandomNumber == 1)
			{
				randomSudoku = sudokuMatrix1;
			}
			else if (generatedRandomNumber == 2)
			{
				randomSudoku = sudokuMatrix2;
			}
			else if (generatedRandomNumber == 3)
			{
				randomSudoku = sudokuMatrix3;
			}

			int counter = 1;
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					TextBox textbox = (TextBox)Controls.Find(string.Format("textBox{0}", counter), false).FirstOrDefault();
					textbox.Text = randomSudoku[i, j];
					if (randomSudoku[i,j] != "")
					{
						textbox.ReadOnly = true;
					}
					counter++;
                }
			}
		}

		private void checkHorizontal(TextBox box, int row)
		{
			if (row > 1)
			{
				row--;
				row *= 9;
			}
			else
				row--;

			for (int j = 1; j < 10; j++)
			{
				TextBox currentTextBox = (TextBox)Controls.Find(string.Format("textBox{0}", j + row), false).FirstOrDefault();
				if (currentTextBox.Name != box.Name)
				{
					ColorErrorBox(box, currentTextBox);
				}
			}
		}

		private void checkVertical(TextBox box, int col)
		{
			col--;
			for (int j = 1; j < 82; j += 9)
			{
				TextBox currentTextBox = (TextBox)Controls.Find(string.Format("textBox{0}", j + col), false).FirstOrDefault();
				if (currentTextBox.Name != box.Name)
				{
					ColorErrorBox(box, currentTextBox);
				}
			}
		}

		private void checkBox(TextBox box, int row, int col)
		{
			int startBoxRow;
			int startBoxCol;
			row = (row - 1) / 3;
			col = (col - 1) / 3;
			startBoxRow = row * 27;
			startBoxCol = col * 3;
			for (int i = 0; i < 19 ; i += 9)
			{
				for (int j = 1; j < 4; j++)
				{
					TextBox currentTextBox = (TextBox)Controls.Find(string.Format("textBox{0}",startBoxRow + startBoxCol + j + i), false).FirstOrDefault();
					if (currentTextBox.Name != box.Name)
					{
						ColorErrorBox(box, currentTextBox);
					}
				}
			}
		}

		private void GetPosition(TextBox box)
		{
			string getTextBoxDigit;
			int row = 1;
			int col = 1;

			if (box.Name.Length == 8)
			{
				getTextBoxDigit = box.Name[box.Name.Length - 1].ToString();
				col = int.Parse(getTextBoxDigit);
			}
			else if (box.Name.Length == 9)
			{
				getTextBoxDigit = box.Name.Substring(box.Name.Length - 2);
				row = (int.Parse(getTextBoxDigit) - 1) / 9 + 1;
				col = int.Parse(getTextBoxDigit) % 9;
				if (col == 0)
					col = 9;

			}
			checkHorizontal(box, row);
			checkVertical(box, col);
			checkBox(box, row, col);
		}

		private void ColorErrorBox(TextBox writenBox, TextBox errorBox)
		{
			Color currentColor = new Color();
			if (errorBox.Text == writenBox.Text && writenBox.Text != "")
			{
				currentColor = errorBox.BackColor;
				errorBox.BackColor = Color.Red;
				writenBox.BackColor = Color.LightBlue;
				writenBox.MouseClick += new MouseEventHandler(OnClick);
			}
			else if (writenBox.Text == "")
			{
				errorBox.BackColor = currentColor;
				writenBox.BackColor = Color.White;
				writenBox.MouseClick -= new MouseEventHandler(OnClick);
			}
		}

		private void checkForSuccess()
		{
			int countErrors = 0;
			for (int i = 1; i < 82; i++)
			{
				TextBox textbox = (TextBox)Controls.Find(string.Format("textBox{0}", i), false).FirstOrDefault();
				if(textbox.BackColor == Color.Red || textbox.Text == "")
				{
					countErrors++;
				}
			}
			if (countErrors == 0)
			{
				string msg = String.Format("Success. Ellapsed time: {0} sec.", stopWatch.Elapsed.Seconds);
				stopWatch.Stop();
				MessageBox.Show(msg);
			}
		}

		private void OnTextChanged(object sender, EventArgs e)
		{
			GetPosition((TextBox)sender);
			checkForSuccess();
		}

		public void OnClick(object sender, MouseEventArgs e)
		{
			TextBox clicked = (TextBox)sender;
			clicked.Text = "";
		}
	}
}
