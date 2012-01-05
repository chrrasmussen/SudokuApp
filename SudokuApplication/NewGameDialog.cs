using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuApplication
{
    public partial class NewGameDialog : Form
    {
        /* Fields */

        private SudokuForm _sudokuForm;

        private PictureBox _lastPicture;
        private Label _lastLabel;

        private int _boardSize;
        private int _difficulty;


        /* Constructors */

        public NewGameDialog(SudokuForm sudokuForm)
        {
            InitializeComponent();

            _sudokuForm = sudokuForm;

            _lastPicture = picture9x9;
            _lastLabel = label9x9;
            _boardSize = 9;

            _difficulty = 1;
        }


        /* Events */

        private void NewGameDialog_Load(object sender, EventArgs e)
        {
            picture4x4.Image = Thumbnail.CreateThumbnail(4, 2, 2);
            picture6x6.Image = Thumbnail.CreateThumbnail(6, 3, 2);
            picture9x9.Image = Thumbnail.CreateThumbnail(9, 3, 3);
            picture12x12.Image = Thumbnail.CreateThumbnail(12, 4, 3);
            picture16x16.Image = Thumbnail.CreateThumbnail(16, 4, 4);
        }

        private void boardPicture_Click(object sender, EventArgs e)
        {
            Font normalText = new Font("Microsoft Sans Serif", 8.25f);
            Font boldText = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);

            _lastPicture.BorderStyle = BorderStyle.None;
            _lastLabel.Font = normalText;

            if (sender == picture4x4)
            {
                picture4x4.BorderStyle = BorderStyle.FixedSingle;
                label4x4.Font = boldText;
                _boardSize = 4;

                _lastPicture = picture4x4;
                _lastLabel = label4x4;
            }
            else if (sender == picture6x6)
            {
                picture6x6.BorderStyle = BorderStyle.FixedSingle;
                label6x6.Font = boldText;
                _boardSize = 6;

                _lastPicture = picture6x6;
                _lastLabel = label6x6;
            }
            else if (sender == picture9x9)
            {
                picture9x9.BorderStyle = BorderStyle.FixedSingle;
                label9x9.Font = boldText;
                _boardSize = 9;

                _lastPicture = picture9x9;
                _lastLabel = label9x9;
            }
            else if (sender == picture12x12)
            {
                picture12x12.BorderStyle = BorderStyle.FixedSingle;
                label12x12.Font = boldText;
                _boardSize = 12;

                _lastPicture = picture12x12;
                _lastLabel = label12x12;
            }
            else if (sender == picture16x16)
            {
                picture16x16.BorderStyle = BorderStyle.FixedSingle;
                label16x16.Font = boldText;
                _boardSize = 16;

                _lastPicture = picture16x16;
                _lastLabel = label16x16;
            }
        }

        private void boardDifficulty_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == radioEasy)
            {
                _difficulty = 0;
            }
            else if (sender == radioMedium)
            {
                _difficulty = 1;
            }
            else if (sender == radioHard)
            {
                _difficulty = 2;
            }
            else if (sender == radioExpert)
            {
                _difficulty = 3;
            }
        }

        private void generateBoard_Click(object sender, EventArgs e)
        {
            _sudokuForm.GenerateBoard(_boardSize, _difficulty);

            this.Close();
        }

        private void createBlankBoard_Click(object sender, EventArgs e)
        {
            _sudokuForm.CreateBlankBoard(_boardSize);

            this.Close();
        }
    }
}
