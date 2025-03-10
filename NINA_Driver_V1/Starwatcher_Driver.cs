using System;
using System.Collections;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

namespace NINA_Driver_V1
{
    [Guid("0e04bcfb-269d-46de-ae4b-8e8fb0fb3b22")] // benötigt einen einzigartigen GUID (Globally Unique Identifier)
    [ClassInterface(ClassInterfaceType.None)] // keine automatische COM-Schnittstelle generieren
    public class Starwatcher_Driver : IDomeV2
    {
        // Das Assembly muss als COM-Objekt registriert werden, damit es von anderen Programmen verwendet werden kann.
        // Diese Methoden werden aufgerufen, wenn das Assembly registriert oder deregistriert wird.
        public static class Registration
        {
            public static void Register(Type t)
            {
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    profile.Register(t.FullName, "Starwatcher_Dome_Driver");
                }
            }

            public static void Unregister(Type t)
            {
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    profile.Unregister(t.FullName);
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
            throw new NotImplementedException();
        }

        public double Altitude
        {
            get { throw new NotImplementedException(); }
        }

        public bool AtHome
        {
            get { throw new NotImplementedException(); }
        }

        public double Azimuth
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanFindHome
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanPark
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSetAltitude
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSetAzimuth
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSetPark
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSetShutter
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSlave
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSyncAzimuth
        {
            get { throw new NotImplementedException(); }
        }

        public void CloseShutter()
        {
            throw new NotImplementedException();
        }

        public void FindHome()
        {
            throw new NotImplementedException();
        }

        public void OpenShutter()
        {
            throw new NotImplementedException();
        }

        public void SetPark()
        {
            throw new NotImplementedException();
        }

        public ShutterState ShutterStatus
        {
            get { throw new NotImplementedException(); }
        }

        public bool Slaved
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void SlewToAltitude(double Altitude)
        {
            throw new NotImplementedException();
        }

        public bool Slewing
        {
            get { throw new NotImplementedException(); }
        }

        public void SyncToAzimuth(double Azimuth)
        {
            throw new NotImplementedException();
        }

        public void SetupDialog()
        {
            throw new NotImplementedException();
        }

        public string Action(string ActionName, string ActionParameters)
        {
            throw new NotImplementedException();
        }

        public void CommandBlind(string Command, bool Raw)
        {
            throw new NotImplementedException();
        }

        public bool CommandBool(string Command, bool Raw)
        {
            throw new NotImplementedException();
        }

        public string CommandString(string Command, bool Raw)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Description => throw new NotImplementedException();
        public string DriverInfo => throw new NotImplementedException();
        public string DriverVersion => throw new NotImplementedException();
        public short InterfaceVersion => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public ArrayList SupportedActions => throw new NotImplementedException();
    }
}