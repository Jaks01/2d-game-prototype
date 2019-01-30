using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learn_opentk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            new MainWindow().Run(60.0, 0.0);

        }
    }

    public struct Vertex
    {
        public const int size = (4 + 4) * 4;    //size of struct in bytes

        private readonly Vector4 position; //readonly only assignable in constructor
        private readonly Color4 color;

        public Vertex(Vector4 _pos, Color4 _col)
        {
            position = _pos;
            color = _col;
        }
    }
}
