using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BinaryTree
{
    static class Program
    {
        // application entry point
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
