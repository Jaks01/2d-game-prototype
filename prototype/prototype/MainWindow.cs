using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
namespace prototype
{
    class MainWindow : GameWindow
    {
        public MainWindow() : base(
            1280,                                               //width
            720,                                                //height
            GraphicsMode.Default,                               //graphics mode
            "Prototype",                                        //window title
            GameWindowFlags.Default,                            //window appearance
            DisplayDevice.GetDisplay(DisplayIndex.Primary),     //monitor to display to
            4,                                                  //major version of OpenGL GraphicsContext
            0,                                                  //minor version of OpenGL GraphicsContext
            GraphicsContextFlags.Default                        //openGL GraphicsContext version
            )
        {
            Title += $": {Width} x {Height}";
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }
    }
}
