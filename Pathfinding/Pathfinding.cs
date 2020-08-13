using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding
{
    public static class Pathfinding
    {
        public static Node MinF(this List<Node> nodes)
        {
            Node min = nodes[nodes.Count - 1];

            for (int i = 1; i < nodes.Count; ++i)
            {
                if (nodes[i].f < min.f)
                    min = nodes[i];
            }

            return min;
        }

        public static int[] GetLocation(this Node node,  Node[,] nodes)
        {
            int[] location = new int[2];

            for (int y = 0; y < nodes.GetLength(1); ++y)
            {
                for (int x = 0; x < nodes.GetLength(0); ++x)
                {
                    if (nodes[x, y].Equals(node))
                    {
                        location[0] = x;
                        location[1] = y;

                        return location;
                    }
                }
            }

            return null;
        }

        public static float GetDistance(this Node A, Node B,  Node[,] nodes)
        {
            var posA = A.GetLocation(nodes);
            var posB = B.GetLocation(nodes);

            var x = Math.Pow(posB[0] - posA[0], 2);
            var y = Math.Pow(posB[1] - posA[1], 2);

            return (float)Math.Sqrt(x + y);
        }

        public static bool IsValid(this Node[,] nodes, int x, int y)
        {
            return x >= 0 && x < nodes.GetLength(0) && y >= 0 && y < nodes.GetLength(1);
        }

        public static void ShowPath(Form1 window, List<Node> path, Node[,] nodes, Node end)
        {
            window.ControlBox = false;

            for (int i = 0; i < path.Count; ++i)
            {
                path[i].selected = true;
                path[i].chosen = false;
                path[i].Invalidate();
            }

            end.selected = true;
            end.Invalidate();

            window.ControlBox = true;
            window.path = new List<Node>(path);
        }

        public static void Reset(Form1 window, Node[,] nodes)
        {
            for (int y = 0; y < nodes.GetLength(1); ++y)
            {
                for (int x = 0; x < nodes.GetLength(0); ++x)
                {
                    nodes[x, y].blocked = false;
                    nodes[x, y].selected = false;
                    nodes[x, y].start = false;
                    nodes[x, y].end = false;
                    nodes[x, y].chosen = false;
                    nodes[x, y].g = 0;
                    nodes[x, y].h = 0;
                    nodes[x, y].f = 0;
                    nodes[x, y].Invalidate();

                    window.selected = null;
                    window.start = null;
                    window.end = null;
                    window.path = new List<Node>();
                }
            }
        }

        public static void FindPath(Form1 window, Node[,] nodes, Node start, Node end)
        {
            List<Node> open = new List<Node>() { start };
            List<Node> closed = new List<Node>();

            do
            {
                Node q = open.MinF();

                open.Remove(q);
                closed.Add(q);

                int x = q.GetLocation(nodes)[0];
                int y = q.GetLocation(nodes)[1];

                if (nodes.IsValid(x - 1, y))
                {
                    var s = nodes[x - 1, y];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }

                if (nodes.IsValid(x + 1, y))
                {
                    var s = nodes[x + 1, y];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end, nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }

                if (nodes.IsValid(x, y + 1))
                {
                    var s = nodes[x, y + 1];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }

                if (nodes.IsValid(x, y - 1))
                {
                    var s = nodes[x, y - 1];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }
                if (nodes.IsValid(x - 1, y + 1))
                {
                    var s = nodes[x - 1, y + 1];

                    if (s.Equals(end))
                    {
                        continue;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }
                if (nodes.IsValid(x - 1, y - 1))
                {
                    var s = nodes[x - 1, y - 1];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }

                if (nodes.IsValid(x + 1, y + 1))
                {
                    var s = nodes[x + 1, y + 1];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }

                if (nodes.IsValid(x + 1, y - 1))
                {
                    var s = nodes[x + 1, y - 1];

                    if (s.Equals(end))
                    {
                        ShowPath(window, closed, nodes, end);
                        return;
                    }

                    if (!closed.Contains(s) && !s.blocked)
                    {
                        s.g = q.g + s.GetDistance(q, nodes);
                        s.h = s.GetDistance(end,  nodes);
                        s.f = s.g + s.h;

                        open.Add(s);
                    }
                }
            }
            while (open.Count != 0);
        }
    }
}
