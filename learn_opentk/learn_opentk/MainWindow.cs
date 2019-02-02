using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace learn_opentk
{
    public sealed class MainWindow : GameWindow
    {
        private String _title;
        private int _program;

        private List<RenderObject> _rendObjects = new List<RenderObject>();

        public MainWindow () : base(
            1280, 720,              //window x by y
            GraphicsMode.Default,   
            "Window Title",         //name
            GameWindowFlags.Default,    //window mode window, fullscreen, etc       
            DisplayDevice.GetDisplay(DisplayIndex.Primary),     //monitor to post to
            4, 0,
            GraphicsContextFlags.ForwardCompatible)
        {
            _title += Title + ": GL VER: " + GL.GetString(StringName.Version);
            Title = _title;
            VSync = VSyncMode.Adaptive;
            CursorVisible = true;
        }

        //reset viewport if game window changes
        protected override void OnResize(EventArgs e)
        {
            CreateProjection();
            GL.Viewport(0, 0, Width, Height);      
            //Debug.WriteLine("changed");
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Exit();
        }

        public override void Exit()
        {
            foreach (var obj in _rendObjects)
                obj.Dispose();
            GL.DeleteProgram(_program);
            base.Exit();
        }

        //gets called when the window is loaded, use to initialize
        protected override void OnLoad(EventArgs e)
        {

            CreateProjection();
            _rendObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.GreenYellow)));
            _rendObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.HotPink)));
            _rendObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.BlueViolet)));
            _rendObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.Red)));
            _rendObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.LimeGreen)));


            _program = CompileShader();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.Enable(EnableCap.DepthTest);

            Closed += OnClosed;
        }

        private int CompileShader()
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(@"../../vertexShader.vert"));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(@"../../fragmentShader.frag"));
            GL.CompileShader(fragmentShader);

            int shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return shaderProgram;
        }

        private Matrix4 _projectionMatrix;
        private void CreateProjection()
        {
            var aspectRatio = (float)Width / Height;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60 * ((float)Math.PI / 180f),
                aspectRatio,
                0.1f,
                4000f);
        }

        private double _time;
        private Matrix4 _modelView;
        private Vector3 _movePosition = new Vector3(0,0,-2);
        private float _rotationValue;
        //update none graphics
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _time += e.Time;
            var r1 = Matrix4.CreateRotationX(_rotationValue * 0.05f);
            var r2 = Matrix4.CreateRotationY(_rotationValue * 0.05f);
            var r3 = Matrix4.CreateRotationZ(_rotationValue * 0.05f);

            var t1 = Matrix4.CreateTranslation(
               _movePosition.X,
               _movePosition.Y,
               _movePosition.Z);
            _modelView = r1 * r2 * r3 * t1;

            KeyboardInput();
        }

       
        //update graphics
        protected override void OnRenderFrame(FrameEventArgs e)
        {            
            Title = $"{_title}: (Vsync: {VSync}) FPS: {1f / e.Time:0}";

            Color4 backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_program);
            GL.UniformMatrix4(20, false, ref _projectionMatrix);
            GL.UniformMatrix4(21, false, ref _modelView);

            float c = 0f;
            foreach (var renderObject in _rendObjects)
            {
                for (int i = 0; i < 5; i++)
                {
                    var k = i + (float)(_time * (0.05f + (0.1 * c)));
                    var t2 = Matrix4.CreateTranslation(
                        (float)(Math.Sin(k * 5f) * (c + 0.5f)),
                        (float)(Math.Cos(k * 5f) * (c + 0.5f)),
                        -2.7f);
                    var r1 = Matrix4.CreateRotationX(k * 13.0f + i);
                    var r2 = Matrix4.CreateRotationY(k * 13.0f + i);
                    var r3 = Matrix4.CreateRotationZ(k * 3.0f + i);
                    var modelView = r1 * r2 * r3 * t2;
                    GL.UniformMatrix4(21, false, ref modelView);
                    renderObject.Render();
                }
                c += 0.3f;
            }
            GL.PointSize(10);
            SwapBuffers();
        }

        private void KeyboardInput()
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (keyState.IsKeyDown(Key.C))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            if (keyState.IsKeyDown(Key.V))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            if(keyState.IsKeyDown(Key.W))
            {
                _movePosition.Y += 0.05f;
            }
            if (keyState.IsKeyDown(Key.S))
            {
                _movePosition.Y -= 0.05f;
            }
            if (keyState.IsKeyDown(Key.A))
            {
                _movePosition.X -= 0.05f;
            }
            if (keyState.IsKeyDown(Key.D))
            {
                _movePosition.X += 0.05f;
            }
            if (keyState.IsKeyDown(Key.Z))
            {
                _movePosition.Z += 0.05f;
            }
            if (keyState.IsKeyDown(Key.X))
            {
                _movePosition.Z -= 0.05f;
            }
            if (keyState.IsKeyDown(Key.Q))
            {
                _rotationValue += 1f;
            }
            if (keyState.IsKeyDown(Key.E))
            {
                _rotationValue -= 1f;
            }
        }
    }
}
