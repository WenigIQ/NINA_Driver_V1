﻿using System;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

//Dieser Treiber implementiert die von der ASCOM-Website heruntergeladene IDomeV2-Schnittstelle in einer Klasse namens Starwatcher_Driver.
//Die Klasse implementiert die Methoden und Eigenschaften der IDomeV2-Schnittstelle und verwendet einen TraceLogger für Debugging-Informationen.
//Dafür wird die ASCOM.Utilities und ASCOM.Deviceinterface verwendet, die in der ASCOM-Plattform enthalten ist.

namespace NINA_Driver_V1
{
    //Die folgenden Deklarationen und Attribute sind notwendig, um die Klasse als COM-Objekt zu registrieren und sie für
    //ASCOM sichtbar zu machen.
    //Ein COM-Objekt ist eine Technologie zur Interprozesskommunikation, die es ermöglicht Programmen und Komponenten, unabhängig
    //von der Programmiersprache, aufeinander zuzugreifen.
    [Guid("b535bec2-a119-4b89-b727-84add9028406")] //GUID: Eine eindeutige Kennung für die COM-Klasse
    [ClassInterface(ClassInterfaceType.None)]      //ClassInterface: Gibt an, dass keine automatische Schnittstelle von Windows erstellt wird 
    [ProgId("Driver_V1_Starwatcher_BenHfr")]       //Progid: Eindeutiger Name für die COM-Klasse
    [ComVisible(true)]                             //Macht die Klasse für COM sichtbar (WICHTIG). Muss in Assembly.cs auf false gesetzt werden
                                                   //damit nicht das komplette Programm für den COM-Compiler sichtbar ist.
    public class Starwatcher_Driver : IDomeV2
    {
        // Konsistente ID verwenden
        private const string driverID = "Driver_V1_Starwatcher_BenHfr";
        private const string driverDescription = "Starwatcher Maturaprojekt 24/25 von Ben Hofer";
        private const string driverName = "Starwatcher Driver BnHfr";
        private SerialPort serialPort;

        // TraceLogger für Debugging
        private TraceLogger tl;

        // Variable zur Speicherung des aktuellen Azimutwerts
        private double currentAzimuth;

        //Variable des slewing zustands
        private bool ISslewing;

        //Luke Status
        private ShutterState CurrentShutterState;

        // Konstruktor
        public Starwatcher_Driver()
        {
            // TraceLogger für Debugging
            //Schreibt alle Aktionen auf und speichert sie in einem Logfile in "C:\...\...\AppData\Local\ASCOM\Logs"
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ASCOM", "Logs", DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            string logFilePath = Path.Combine(logDirectory, "Starwatcher_log.txt");
            tl = new TraceLogger(logFilePath, "Starwatcher");
            tl.Enabled = true;
            tl.LogMessage("Starwatcher_Driver", "Initialisiert");

            //Initialisierung des seriellen Ports
            serialPort = new SerialPort("COM3", 115200);
        }

        //Registrierung-----------------------------------------------------------------
        //Diese Methoden werden benötigt, um den Treiber für ASCOM zu registrieren und zu de-registrieren.
        //Dies ermöglicht es ASCOM,, den Treiber zu erkennen und zu verwenden
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

        [ComVisible(false)]
        //Die Registration-Klasse enthält die eigentlichen Registrierungs- und De-Registrierungslogik; sie ist in den Developer-Tools
        //Von ASCOM enthalten und wird in den Registrierungsmethoden aufgerufen.
        public static class Registration
        {
            public static void Register(Type t)
            {
                using (var profile = new Profile())
                {
                    profile.DeviceType = "Dome";
                    profile.Register(driverID, "Starwatcher Ben Hofer");
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

        //Ersteinstellungen-----------------------------------------------------------------
        private string settingValue1;
        private string settingValue2;
        //Der SetupDialog ermglicht es dem Benutzer, Einstellungen für den Treiber zu konfigurieren.
        //Diese Einstellungen werden in der ASCOM-Profile-Registry gespeichert und können von der ASCOM-Plattform abgerufen werden.
        //SetupDialog ist notwendig um von der ASCOM-Plattform erkannt zu werden, auch wenn es für dieses Projekt nicht benötigt wird, da
        //mein Treiber nur für eine einzige Kuppel geschrieben wird, welche nicht umgestellt wird.
        public void SetupDialog()
        {
            using (var form = new SetupDialogForm())
            using (var profile = new Profile())
            {
                profile.DeviceType = "Dome";
                // Einstellungen laden
                form.SettingValue1 = profile.GetValue(driverID, "SettingName1", "", "DefaultValue1");
                form.SettingValue2 = profile.GetValue(driverID, "SettingName2", "", "DefaultValue2");

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Einstellungen speichern
                    profile.WriteValue(driverID, "SettingName1", form.SettingValue1);
                    profile.WriteValue(driverID, "SettingName2", form.SettingValue2);
                }
            }
        }

        //Commands senden über den Serial Port ermöglichen
        [ComVisible(false)]
        private void SendCommand(string command)
        {
            if (serialPort.IsOpen && serialPort != null)
            {
                serialPort.WriteLine(command);
                tl.LogMessage("SendCommand", "Command: " + command);
                return;
            }
            else
            {
                tl.LogMessage("SendCommand", "SerialPort not open");
            }
        }
        //----------------------------------------------------------------------------------------
        // Connect & Disconnect
        //Diese Methoden ermöglichen es, eine Verbindung zur Kuppel herzustellen und zu trennen. Die Connected-Eigenschaft von IDomeV2 steuert
        //den Verbindungsstatus.
        private bool connected;
        public bool Connected
        {
            get { return connected; }
            set
            {
                if (value) Connect();
                else Disconnect();
            }
        }

        public void Connect()
        {
            using (var profile = new Profile())
            {
                profile.DeviceType = "Dome";
                settingValue1 = profile.GetValue(driverID, "SettingName1", "", "DefaultValue1");
                settingValue2 = profile.GetValue(driverID, "SettingName2", "", "DefaultValue2");
            }
            serialPort.DataReceived += SerialPort_DataReceived; //Eventhandler für Datenempfang vom ESP32
            serialPort.Open();
            connected = true;
            tl.LogMessage("Connect", "Connected to Starwatcher");
        }

        public void Disconnect()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            connected = false;
            tl.LogMessage("Disconnect", "Disconnected from Starwatcher");
        }
        //----------------------------------------------------------------------------------------
        //Eventhandler für Datenempfang vom ESP32
        //Empfängt die Rückmeldungen vom ESP32 und gibt sie im TraceLogger aus
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Alle verfügbaren Daten lesen
                string data = serialPort.ReadLine();
                tl.LogMessage("ESP32", "Received: " + data);
                //Updated Azimuth
                if (data.StartsWith("AZ"))
                {
                    string[] parts = data.Split(' ');
                    currentAzimuth = Convert.ToDouble(parts[1]);
                }
                //Slew Befehl wurde empfangen
                else if (data.StartsWith("ACK SLEW")) 
                {
                    ISslewing = true;
                    tl.LogMessage("ESP32", "is Slewing");
                }
                //Slew Befehl wurde ausgeführt
                else if (data.StartsWith("DONE SLEW"))                 
                {
                    ISslewing = false;
                    tl.LogMessage("ESP32", "Slew Done");
                }
                //Luke Befehl erhalten (schließt sich)
                else if (data.StartsWith("ACK CLOSE")) 
                {
                    CurrentShutterState = ShutterState.shutterClosing;
                    tl.LogMessage("ESP32", "Shutter is closing");
                }
                //Luke Befehl erhalten (öffnet sich)
                else if (data.StartsWith("ACK OPEN")) 
                {
                    CurrentShutterState = ShutterState.shutterOpening;
                    tl.LogMessage("ESP32", "Shutter is opening");
                }
                //Luke Befehl ausgeführt (ist geschlossen)
                else if (data.StartsWith("DONE CLOSE")) 
                {
                    CurrentShutterState = ShutterState.shutterClosed;
                    tl.LogMessage("ESP32", "Shutter is closed");
                }
                //Luke Befehl ausgeführt (ist offen)
                else if (data.StartsWith("DONE OPEN")) 
                {
                    CurrentShutterState = ShutterState.shutterOpen;
                    tl.LogMessage("ESP32", "Shutter is open");
                }
                //ABORT Befehl wurde empfangen
                else if (data.StartsWith("ACK ABORT"))
                {
                    ISslewing = false;
                    CurrentShutterState = ShutterState.shutterError;
                    tl.LogMessage("ESP32", "Abort Slew");
                }

            }
            catch (Exception ex)
            {
                tl.LogMessage("ESP32", "Error: " + ex.Message);
            }
        }
        //----------------------------------------------------------------------------------------
        //Der Rest des Codes implementiert die Methoden und Eigenschaften der IDomeV2-Schnittstelle, die für die Steuerung einer Kuppel
        //gebraucht werden könnten.

        // Park
        private bool atPark = false;
        public bool AtPark
        {
            get { return atPark; }
        }
        public bool CanPark
        {
            get { return true; }
        }
        public void Park()
        {
            SendCommand("PARK");
            tl.LogMessage("Sent command: ", "PARK");
        }
        public bool AtHome
        {
            get { return false; }
        }
        //----------------------------------------------------------------------------------------
        public void SlewToAzimuth(double Azimuth)
        {
            SendCommand("SLEW " + Azimuth);
            tl.LogMessage("Sent command: ", "SLEW to Azimuth = " + Azimuth);
        }

        public void AbortSlew()
        {
            SendCommand("ABORT");
            tl.LogMessage("Sent command: ", "Abort Slew");
        }

        public bool Slewing
        {
            get { return ISslewing; }
        }
        public double Azimuth
        {
            get { return currentAzimuth; }
        }

        //----------------------------------------------------------------------------------------
        //Luke öffnen und schließen

        public void OpenShutter()
        {
            SendCommand("OPEN");
            tl.LogMessage("Sent command: ", "OPEN");
        }
        public void CloseShutter()
        {
            SendCommand("CLOSE");
            tl.LogMessage("Sent command: ", "CLOSE");
        }
        public ShutterState ShutterStatus
        {
            get { return CurrentShutterState;}
        }


        // Noch zu Implementieren
        public void SetPark()
        {
            throw new ASCOM.MethodNotImplementedException("SetPark is not implemented by this driver");
        }
        public double Altitude
        {
            get { return 0.0; }
        }
        public bool CanFindHome
        {
            get { return false; }
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
            get { return false; }
        }

        public bool CanSetShutter
        {
            get { return true; }
        }

        public bool CanSlave
        {
            get { return false; }
        }

        public bool CanSyncAzimuth
        {
            get { return true; }
        }

        public void FindHome()
        {
            tl.LogMessage("FindHome", "Find Home");
        }

        public bool Slaved
        {
            get { return false; }
            set { tl.LogMessage("Slaved", "Set Slaved: " + value); }
        }

        public void SlewToAltitude(double Altitude)
        {
            tl.LogMessage("SlewToAltitude", "Slew to Altitude: " + Altitude);
        }

        public void SyncToAzimuth(double Azimuth)
        {
            tl.LogMessage("SyncToAzimuth", "Sync to Azimuth: " + Azimuth);
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
            tl.Enabled = false;
            tl.Dispose();
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
            get { return "Starwatcher_Driver_BnHfr"; }
        }

        public ArrayList SupportedActions
        {
            get { return new ArrayList(); }
        }
    }
}