using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        List<PointWithAttributes> Stroke = new List<PointWithAttributes>();
        List<PointWithAttributes> Redo = new List<PointWithAttributes>();
        Color currentColor = Color.Black;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color color = Color.Azure;
            Graphics g = e.Graphics;
            
            

            foreach(PointWithAttributes pt in Stroke)
            {
                Brush br = new SolidBrush(pt.color);
                g.FillRectangle(br, pt.point.X, pt.point.Y, 8, 8);
            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.None)
            {
                PointWithAttributes pt = new PointWithAttributes();
                pt.color = currentColor;
                pt.point = e.Location;
                Stroke.Add(pt);
                Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
                currentColor = Color.Red;
            if (e.KeyCode == Keys.B)
                currentColor = Color.Black;
            if (e.KeyCode == Keys.Z && e.Control && Stroke.Count > 0)
            {
                //undo
                Redo.Add(Stroke[Stroke.Count - 1]);
                Stroke.RemoveAt(Stroke.Count - 1);
                Invalidate();
            }
            if (e.KeyCode == Keys.Y && e.Control && Redo.Count > 0)
            {
                //redo
                Stroke.Add(Redo[Redo.Count - 1]);
                Redo.RemoveAt(Redo.Count - 1);
                Invalidate();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Form1_MouseMove(sender, e);
        }
    }
}
