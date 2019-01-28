using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace prototype
{
    class UpdateCenter
    {
        public static readonly UpdateCenter instance = new UpdateCenter();
        public static UpdateCenter Instance { get { return instance; } }

        private MainWindow window;

        public void GameLaunchInitialize()
        {
            window = new MainWindow();
            window.Run(60.0, 0.0);
            window.UpdateFrame += GameExitOnInput;
        }

        private void GameExitOnInput(object sender, FrameEventArgs e)
        {
            if (InputController.ExitInput())
                window.Exit();
        }
    }
}
