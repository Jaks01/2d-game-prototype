using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
namespace learn_opentk
{
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
