using System;
using OpenTK;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace prototype
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Debug.Print("ran");
            UpdateCenter.Instance.GameLaunchInitialize();
        }        
    }
}
