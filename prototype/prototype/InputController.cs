using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
namespace prototype
{
    static class InputController
    {
        public static bool ExitInput()
        {
            var keys = Keyboard.GetState();
            if (keys.IsKeyDown(Key.Escape))
                return true;
            return false;
        }
    }
}
