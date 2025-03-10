using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASCOM.DeviceInterface; 
using ASCOM.Utilities;
using System.Runtime.InteropServices;

namespace NINA_Driver_V1
{
    [Guid("0e04bcfb-269d-46de-ae4b-8e8fb0fb3b22")] //benötigt einen Einzigartigen GUID (Globally Unique Identifier)
    [ClassInterface(ClassInterfaceType.None)]//keine automatische COm-Schnittstelle generieren
    public class Starwatcher_Driver : ITelescopeV3
    {
        //Connect & Disconnect
        private bool connected = false;
        public bool Connected
        {
            get { return connected; }
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
        //__________________________________________________________

        public void AbortSlew()
        {
            throw new NotImplementedException();
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            throw new NotImplementedException();
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
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

        public void FindHome()
        {
            throw new NotImplementedException();
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            throw new NotImplementedException();
        }

        public void Park()
        {
            throw new NotImplementedException();
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            throw new NotImplementedException();
        }

        public void SetPark()
        {
            throw new NotImplementedException();
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SlewToTarget()
        {
            throw new NotImplementedException();
        }

        public void SlewToTargetAsync()
        {
            throw new NotImplementedException();
        }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SyncToTarget()
        {
            throw new NotImplementedException();
        }

        public void Unpark()
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public bool connected { set => throw new NotImplementedException(); } 
        public bool AtHome => throw new NotImplementedException();
        public bool AtPark => throw new NotImplementedException();
        public double Azimuth => throw new NotImplementedException();
        public bool CanFindHome => throw new NotImplementedException();
        public bool CanPark => throw new NotImplementedException();
        public bool CanPulseGuide => throw new NotImplementedException();
        public bool CanSetDeclinationRate => throw new NotImplementedException();
        public bool CanSetGuideRates => throw new NotImplementedException();
        public bool CanSetPark => throw new NotImplementedException();
        public bool CanSetPierSide => throw new NotImplementedException();
        public bool CanSetRightAscensionRate => throw new NotImplementedException();
        public bool CanSetTracking => throw new NotImplementedException();
        public bool CanSlew => throw new NotImplementedException();
        public bool CanSlewAltAz => throw new NotImplementedException();
        public bool CanSlewAltAzAsync => throw new NotImplementedException();
        public bool CanSlewAsync => throw new NotImplementedException();
        public bool CanSync => throw new NotImplementedException();
        public bool CanSyncAltAz => throw new NotImplementedException();
        public bool CanUnpark => throw new NotImplementedException();
        public double Declination => throw new NotImplementedException();
        public double DeclinationRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesRefraction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public EquatorialCoordinateType EquatorialSystem => throw new NotImplementedException();
        public double FocalLength => throw new NotImplementedException();
        public double GuideRateDeclination { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double GuideRateRightAscension { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPulseGuiding => throw new NotImplementedException();
        public double RightAscension => throw new NotImplementedException();
        public double RightAscensionRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public PierSide SideOfPier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SiderealTime => throw new NotImplementedException();
        public double SiteElevation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SiteLatitude { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SiteLongitude { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public short SlewSettleTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double TargetDeclination { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double TargetRightAscension { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Tracking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DriveRates TrackingRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ITrackingRates TrackingRates => throw new NotImplementedException();
        public DateTime UTCDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Description => throw new NotImplementedException();
        public string DriverInfo => throw new NotImplementedException();
        public string DriverVersion => throw new NotImplementedException();
        public short InterfaceVersion => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public ArrayList SupportedActions => throw new NotImplementedException();
        public AlignmentModes AlignmentMode => throw new NotImplementedException();
        public double Altitude => throw new NotImplementedException();
        public double ApertureArea => throw new NotImplementedException();
        public double ApertureDiameter => throw new NotImplementedException();
        public bool Slewing => throw new NotImplementedException();
    }





