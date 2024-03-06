using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount++;
        }

        const decimal g = 9.81M;
        const decimal C = 0.15M;
        const decimal rho = 1.29M;
        decimal t, x, y, v0, cosa, sina, S, m, k, vx, vy, dt;
        int iter = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        decimal maxHeight;

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            decimal v = (decimal)Math.Sqrt((double)(vx * vx + vy * vy));
            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
            chart1.Series[0].Points.AddXY(x, y);
            if (y > maxHeight) 
            {
                maxHeight = y;
                dataGridView1.Rows[iter].Cells[2].Value = maxHeight;
            }
            dataGridView1.Rows[iter].Cells[1].Value = x;
            if (y <= 0) 
            {
                dataGridView1.Rows[iter].Cells[3].Value = v;
                timer1.Stop();
                iter++;
                dataGridView1.RowCount++;
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                maxHeight = 0;
                chart1.Series[0].Points.Clear();
                t = 0;
                x = 0;
                y = edHeight.Value;
                v0 = edSpeed.Value;
                dt = edDt.Value;
                double a = (double)edAngle.Value * Math.PI / 180;
                cosa = (decimal)Math.Cos(a);
                sina = (decimal)Math.Sin(a);
                S = edSize.Value;
                m = edWeight.Value;
                k = 0.5M * C * rho * S / m;
                vx = v0 * cosa;
                vy = v0 * sina;
                chart1.Series[0].Points.AddXY(x, y);

                dataGridView1.Rows[iter].Cells[0].Value = dt;
                timer1.Start();
            }
        }
    }
}
