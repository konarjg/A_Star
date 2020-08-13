using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding
{
    public partial class Form1 : Form
    {
        public const int size = 4;
        public Node[,] nodes;
        public Node selected;

        public Node start;
        public Node end;
        public List<Node> path = new List<Node>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.nodes = new Node[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var node = new Node(false, false, false, false, 0, 0, 0, x, y);
                    node.Size = new Size(100, 100);
                    node.Location = new Point(10 + 105 * x, 10 + 105 * y);
                    node.Visible = true;
                    node.Invalidate();

                    this.nodes[x, y] = node;
                    this.Controls.Add(this.nodes[x, y]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (start == null || end == null)
                return;

            Pathfinding.FindPath(this, this.nodes, start, end);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pathfinding.Reset(this, this.nodes);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            if (start != null)
                return;

            start = selected;
            start.start = true;
            start.chosen = false;
            start.Invalidate();

            selected = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            if (end != null)
                return;

            end = selected;
            end.end = true;
            end.chosen = false;
            end.Invalidate();

            selected = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            if (start != null)
            {
                if (start.Equals(selected))
                    return;
            }

            if (end != null)
            {
                if (end.Equals(selected))
                    return;
            }

            selected.blocked = true;
            selected.chosen = false;
            selected.Invalidate();
        }
    }
}
