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
    public partial class OptionsDialog : Form
    {
        /* Constructor */

        public OptionsDialog()
        {
            InitializeComponent();

            // Set default values
            wrongNumbers.Checked = Properties.Settings.Default.IndicateWrongNumbers;
            validatedSections.Checked = Properties.Settings.Default.IndicateValidatedSections;
        }


        /* Events */

        private void OK_Click(object sender, EventArgs e)
        {
            // Save new settings
            Properties.Settings.Default.IndicateWrongNumbers = wrongNumbers.Checked;
            Properties.Settings.Default.IndicateValidatedSections = validatedSections.Checked;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
