using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
namespace learn_opentk
{
    class RenderObject : IDisposable
    {
        private bool _initialized;
        private readonly int _vertexArray;
        private readonly int _buffer;    
        private readonly int _verticesCount;
        public RenderObject(Vertex[] vertices)
        {
            _verticesCount = vertices.Length;
            _vertexArray = GL.GenVertexArray();
            _buffer = GL.GenBuffer();

            GL.BindVertexArray(_vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
            
            //create first buffer
            GL.NamedBufferStorage(
                _buffer,        
                Vertex.size * vertices.Length,      //buffer size needed
                vertices,                           //data to initialize with
                BufferStorageFlags.MapWriteBit);    //only write to buffer for now

            //for positions
            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(
                _vertexArray,       
                0,                          //attribute index, from shader location = 0
                4,                          //vector4 with 4 points, size of the attribute
                VertexAttribType.Float,     //it has float
                false,                      //dont need to normalize it
                0);                         //relative position offset

            //for colors
            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(     
                _vertexArray,
                1,
                4,
                VertexAttribType.Float,
                false,
                16);                         //same params as above

            //link the buffer to the vertex array
            GL.VertexArrayVertexBuffer(_vertexArray, 0, _buffer, IntPtr.Zero, Vertex.size);
            _initialized = true;
            Debug.Print("Created ");
        }

        public void Bind()
        {
            GL.BindVertexArray(_vertexArray);
        }

        public void Render()
        {
            Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  //garbage collector, free rams
        }

        //virtual for override
        protected virtual void Dispose(bool _disposing)
        {
            if(_disposing)
            {
                if(_initialized)
                {
                    GL.DeleteVertexArray(_vertexArray);
                    GL.DeleteBuffer(_buffer);
                    _initialized = false;
                }
            }
        }
    }
}
