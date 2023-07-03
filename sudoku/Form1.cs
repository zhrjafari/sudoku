using System;

namespace sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // if Text not in 1 ... 9 then clear it
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.CompareTo("1") < 0 || (sender as TextBox).Text.CompareTo("9") > 0)
                (sender as TextBox).Text = "";
        }

        // Solve button
        private void button1_Click(object sender, EventArgs e)
        {
            Read();
            if (Solve(array)) Write(); else MessageBox.Show("This puzzle is wrong!");
        }

        // Clear
        private void button2_Click(object sender, EventArgs e)
        {
            Array.Clear(array);
            Write();
        }

        // Array of numbers
        int[,] array = new int[9, 9];

        // Read all TextBoxs to array
        private void Read()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                TextBox textBox = panel1.Controls[i] as TextBox;
                if (textBox.Text == "")
                    array[Convert.ToInt32(textBox.Tag) / 10, Convert.ToInt32(textBox.Tag) % 10] = 0; // 0 is empty
                else
                    array[Convert.ToInt32(textBox.Tag) / 10, Convert.ToInt32(textBox.Tag) % 10] = Convert.ToInt32(textBox.Text);
            }
        }

        // Write array to TextBoxs
        private void Write()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                TextBox textBox = panel1.Controls[i] as TextBox;
                if (array[Convert.ToInt32(textBox.Tag) / 10, Convert.ToInt32(textBox.Tag) % 10] == 0)
                    textBox.Text = "";
                else
                    textBox.Text = Convert.ToString(array[Convert.ToInt32(textBox.Tag) / 10, Convert.ToInt32(textBox.Tag) % 10]);
            }
        }

        // Check for duplicate
        private bool Check(int[] digits)
        {
            //                             1      2      3      4      5      6      7      8      9
            bool[] exists = new bool[] { false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (digits[i] != 0)
                    if (exists[digits[i] - 1])
                        return false;
                    else
                        exists[digits[i] - 1] = true;
            }
            return true;
        }

        int[,] boxes = new int[,] {
                { 00, 01, 02,  10, 11, 12,  20, 21, 22 },
                { 03, 04, 05,  13, 14, 15,  23, 24, 25 },
                { 06, 07, 08,  16, 17, 18,  26, 27, 28 },
                { 30, 31, 32,  40, 41, 42,  50, 51, 52 },
                { 33, 34, 35,  43, 44, 45,  53, 54, 55 },
                { 36, 37, 38,  46, 47, 48,  56, 57, 58 },
                { 60, 61, 62,  70, 71, 72,  80, 81, 82 },
                { 63, 64, 65,  73, 74, 75,  83, 84, 85 },
                { 66, 67, 68,  76, 77, 78,  86, 87, 88 }
            };

        // Check the puzzle
        private bool CheckAll(int[,] array)
        {
            for (int i = 0; i < 9; i++)
            {
                // rows
                if (!Check(new int[] { array[i, 0], array[i, 1], array[i, 2], array[i, 3], array[i, 4], array[i, 5], array[i, 6], array[i, 7], array[i, 8] })) return false;
                // cols
                if (!Check(new int[] { array[0, i], array[1, i], array[2, i], array[3, i], array[4, i], array[5, i], array[6, i], array[7, i], array[8, i] })) return false;
                // boxes
                if (!Check(new int[] {
                    array[boxes[i, 0] / 10, boxes[i, 0] % 10], array[boxes[i, 1] / 10, boxes[i, 1] % 10], array[boxes[i, 2] / 10, boxes[i, 2] % 10],
                    array[boxes[i, 3] / 10, boxes[i, 3] % 10], array[boxes[i, 4] / 10, boxes[i, 4] % 10], array[boxes[i, 5] / 10, boxes[i, 5] % 10],
                    array[boxes[i, 6] / 10, boxes[i, 6] % 10], array[boxes[i, 7] / 10, boxes[i, 7] % 10], array[boxes[i, 8] / 10, boxes[i, 8] % 10],
                })) return false;
            }
            return true;
        }

        // Solve
        private bool Solve(int[,] array)
        {
            // Find an empty location
            int empty = -1;
            for (int i = 0; i < 81; i++)
                if (array[i / 9, i % 9] == 0)
                {
                    empty = i;
                    break;
                }

            // No empty? Solved!
            if (empty == -1) return true;

            // Do solve
            for (int num = 1; num <= 9; num++)
            {
                array[empty / 9, empty % 9] = num;
                if (CheckAll(array) && Solve(array)) return true;
                array[empty / 9, empty % 9] = 0;
            }

            // Cannot solve it
            return false;
        }
    }
}