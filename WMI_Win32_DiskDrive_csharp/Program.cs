using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI_Win32_DiskDrive_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Test #1");
            Console.WriteLine("-----------------");

            WMI_Win32_DiskDrive win32diskDrive = WMI_Win32_DiskDrive.Instance;
            Console.WriteLine(win32diskDrive.ToString());

            Console.WriteLine("-----------------");
            Console.WriteLine("Test #2");
            Console.WriteLine("-----------------");

            win32diskDrive.Refresh();

            for (int i = 0; i < win32diskDrive.Count; i++)
            {
                Console.WriteLine(win32diskDrive[i].ToShortString());
            }
        }
    }
}
