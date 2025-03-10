using System;
using System.Runtime.InteropServices;

namespace ASCOMRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ASCOMRegistration.exe [register|unregister]");
                return;
            }

            string action = args[0].ToLower();
            Type driverType = typeof(NINA_Driver_V1.Starwatcher_Driver);

            if (action == "register")
            {
                NINA_Driver_V1.Starwatcher_Driver.RegisterASCOM(driverType);
                Console.WriteLine("Driver registered successfully.");
            }
            else if (action == "unregister")
            {
                NINA_Driver_V1.Starwatcher_Driver.UnregisterASCOM(driverType);
                Console.WriteLine("Driver unregistered successfully.");
            }
            else
            {
                Console.WriteLine("Invalid action. Use 'register' or 'unregister'.");
            }
        }
    }
}