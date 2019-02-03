using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using System.Drawing;
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
    
    class TexturedRenderObject : IDisposable
    {
        private readonly int buffer;
        private readonly int vertexArray;
        private readonly int verticesCount;
        private readonly int texture;
        private readonly int program;
        private bool initialized;
        public TexturedRenderObject(TexturedVertex[] vertices, int prog, string fileName)
        {
            program = prog;

            verticesCount = vertices.Length;
            vertexArray = GL.GenVertexArray();
            buffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);

            //buffer the vertex
            GL.NamedBufferStorage(buffer, TexturedVertex.size, vertices, BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,    
                0,              //attribute index in shader
                4,              //vector 4 = 4 
                VertexAttribType.Float,     //uses float
                false,          //no normalization needed, float ignores 
                0);             //offset

            //repeat process to bind new attributes
            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,              //shader location attribute index
                2,              //size of it, vec2 = 2
                VertexAttribType.Float,      //contains float
                false,          //no normalization
                16);             //offset after vec4 i byte

            GL.VertexArrayVertexBuffer(vertexArray, 0, buffer, IntPtr.Zero, TexturedVertex.size);

            texture = InitTexture(fileName);
            initialized = true;
        }

        private int InitTexture(String fileName)
        {
            var data = LoadTexture(fileName, out int width, out int height);
            GL.CreateTextures(TextureTarget.Texture2D, 1, out int txt);
            GL.TextureStorage2D(
                txt,
                1,      //mipmap level
                SizedInternalFormat.Rgba32f,    //texture format
                width, height);

            GL.BindTexture(TextureTarget.Texture2D, txt);
            GL.TextureSubImage2D(txt,
                0,      //level 0/ first level
                0,      //x offset
                0,      //y offset
                width, height,
                PixelFormat.Rgba,
                PixelType.Float, 
                data);
            return txt;
            //now opengl has data 
        }

        //use system.drawing to load image from disk
        private float[] LoadTexture(String fileName, out int width, out int height)
        {
            float[] r;
            using (var bmp = (Bitmap)Image.FromFile(fileName))
            {
                width = bmp.Width;
                height = bmp.Height;
                r = new float[width * height * 4];
                int index = 0;
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        r[index++] = pixel.R / 255f;
                        r[index++] = pixel.G / 255f;
                        r[index++] = pixel.B / 255f;
                        r[index++] = pixel.A / 255f;
                    }
                }
            }
            return r;
        }

        public void Render()
        {
            Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  //garbage collector, free rams
        }

        //virtual for override
        protected virtual void Dispose(bool _disposing)
        {
            if (_disposing)
            {
                if (initialized)
                {
                    GL.DeleteVertexArray(vertexArray);
                    GL.DeleteTexture(texture);
                    GL.DeleteBuffer(buffer);
                    initialized = false;
                }
            }
        }

        public virtual void Bind()
        {
            GL.UseProgram(program);
            GL.BindVertexArray(vertexArray);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }
    }
}
