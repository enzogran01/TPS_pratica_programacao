using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.AddXY(0, 15);
            chart1.Series[0].Points.AddXY(1, 71);
            chart1.Series[0].Points.AddXY(2, 22);
            chart1.Series[0].Points.AddXY(3, 90);
            chart1.Series[0].Points.AddXY(4, 3);
            chart1.Series[0].Points.AddXY(5, 25);
            chart1.Series[0].Points.AddXY(6, 41);
            chart1.Series[0].Points.AddXY(7, 66);
        }
    }
}
