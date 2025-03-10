using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

namespace NINA_Driver_V1
{
    [Guid("6ac65995-6304-4935-aa0c-aaeb0ae4c5cc")] // benötigt einen einzigartigen GUID (Globally Unique Identifier)
    [ClassInterface(ClassInterfaceType.None)] // keine automatische COM-Schnittstelle generieren
    public class Starwatcher_Driver : IDomeV2
    {
        private const string driverID = "ASCOM.NINA_Driver_V1.Starwatcher_Driver";
        private const string driverDescription = "Starwatcher Dome Driver";

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            Registration.Register(t);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            Registration.Unregister(t);
        }

        public static class Registration
        {
            public static void Register(Type t)
            {
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    profile.Register(driverID, driverDescription);
                }
            }

            public static void Unregister(Type t)
            {
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    profile.Unregister(driverID);
                }
            }
        }

        public void SetupDialog()
        {
            using (var form = new SetupDialogForm())
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    form.SettingValue = profile.GetValue(driverID, "SettingName", string.Empty, "DefaultSettingValue");
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Speichern Sie die Einstellungen aus dem Dialogfenster in der ASCOM-Profilklasse
                    using (var profile = new Profile())
                    {
                        profile.DeviceType = "Dome";
                        profile.WriteValue(driverID, "SettingName", form.SettingValue);
                    }
                }
            }

        // Connect & Disconnect
        private bool connected = false;
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public void Connect()
        {
            connected = true;
            Console.WriteLine("Connected to Starwatcher");
        }

        public void Disconnect()
        {
            connected = false;
            Console.WriteLine("Disconnected from Starwatcher");
        }

        // Park
        private bool atPark = false;
        public bool AtPark
        {
            get { return atPark; }
        }

        public void Park()
        {
            atPark = true;
            Console.WriteLine("Parked");
        }

        // Slew to Azimuth
        public void SlewToAzimuth(double Azimuth)
        {
            Console.WriteLine("Slewing to Azimuth: " + Azimuth);
        }

        // Noch zu Implementieren
        public void AbortSlew()
        {
            Console.WriteLine("Abort Slew");
        }

        public double Altitude
        {
            get { return 0.0; }
        }

        public bool AtHome
        {
            get { return false; }
        }

        public double Azimuth
        {
            get { return 0.0; }
        }

        public bool CanFindHome
        {
            get { return false; }
        }

        public bool CanPark
        {
            get { return true; }
        }

        public bool CanSetAltitude
        {
            get { return false; }
        }

        public bool CanSetAzimuth
        {
            get { return true; }
        }

        public bool CanSetPark
        {
            get { return true; }
        }

        public bool CanSetShutter
        {
            get { return false; }
        }

        public bool CanSlave
        {
            get { return false; }
        }

        public bool CanSyncAzimuth
        {
            get { return true; }
        }

        public void CloseShutter()
        {
            Console.WriteLine("Close Shutter");
        }

        public void FindHome()
        {
            Console.WriteLine("Find Home");
        }

        public void OpenShutter()
        {
            Console.WriteLine("Open Shutter");
        }

        public void SetPark()
        {
            Console.WriteLine("Set Park");
        }

        public ShutterState ShutterStatus
        {
            get { return ShutterState.shutterClosed; }
        }

        public bool Slaved
        {
            get { return false; }
            set { Console.WriteLine("Set Slaved: " + value); }
        }

        public void SlewToAltitude(double Altitude)
        {
            Console.WriteLine("Slew to Altitude: " + Altitude);
        }

        public bool Slewing
        {
            get { return false; }
        }

        public void SyncToAzimuth(double Azimuth)
        {
            Console.WriteLine("Sync to Azimuth: " + Azimuth);
        }

        public string Action(string ActionName, string ActionParameters)
        {
            throw new ASCOM.ActionNotImplementedException("Action " + ActionName + " is not implemented by this driver");
        }

        public void CommandBlind(string Command, bool Raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBlind is not implemented by this driver");
        }

        public bool CommandBool(string Command, bool Raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBool is not implemented by this driver");
        }

        public string CommandString(string Command, bool Raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandString is not implemented by this driver");
        }

        public void Dispose()
        {
            // Bereinigen Sie alle Ressourcen hier
        }

        public string Description
        {
            get { return driverDescription; }
        }

        public string DriverInfo
        {
            get { return "Starwatcher Dome Driver Version 1.0"; }
        }

        public string DriverVersion
        {
            get { return "1.0"; }
        }

        public short InterfaceVersion
        {
            get { return 2; }
        }

        public string Name
        {
            get { return "Starwatcher Dome Driver"; }
        }

        public ArrayList SupportedActions
        {
            get { return new ArrayList(); }
        }
    }
}