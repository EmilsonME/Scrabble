using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iskrabol
{
    class Program
    {
    	[STAThread]
   
        public static void Main()
        {
        	Application.Run(new Board());
        }
    }
}
