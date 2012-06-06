﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

/* // Sample struct usage
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace MultiWiiConfig
{

    #region Struct definitions: Version 1.9 (Stable)

    enum MultiRotorType
    {
        TRI = 1,
        QUADP = 2,
        QUADX = 3,
        BI = 4,
        GIMBAL = 5,
        Y6 = 6,
        HEX6 = 7,
        FLYING_WING = 8,
        Y4 = 9,
        HEX6X = 10,
        OCTOX8 = 11,
        OCTOFLATP = 11,
        OCTOFLATX = 11
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct Vector3
    {
        Int16 x;
        Int16 y;
        Int16 z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct PID
    {
        byte x; // VA.L (example: 30 is 3.0
        byte y; // 0.VAL  (example: 24 is 0.024)
        byte z;
    }

    // Struct recieved as response to sending 'M'
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MData
    {
        char startResponseCharacter; // Same as end 'M' if M package...
        byte version;
        Vector3 accSmooth;
        Vector3 gyroData; // Values divided by 8
        Vector3 magADC; // div 3
        UInt16 EstAlt;
        UInt16 heading; // Compass

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        UInt16[] servo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        UInt16[] motor;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        UInt16[] rcData;

        byte sensorBitMask; // nunchuk|ACC<<1|BARO<<2|MAG<<3|GPSPRESENT<<4
        byte sensorModeBitMask; // accMode|baroMode<<1|magMode<<2|(GPSModeHome|GPSModeHold)<<3
        UInt16 cycleTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] // values divided before send with 10.
        UInt16[] angle;

        byte MultiRotorType; // define & flashed.

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] // no division.
        PID[] PIDItems;

        byte Level_P;
        byte Level_I;
        byte PIDMAG;

        byte rcRate;
        byte rcExpo8;
        byte rollPitchRate;
        byte yawRate;
        byte dynThrPID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        byte[] activate;

        UInt16 GPS_distanceToHome;
        UInt16 GPS_directionToHome;
        byte GPS_numSat;
        byte GPS_fix;
        byte GPS_update;
        UInt16 intPowerMeterSum;
        UInt16 intPowerTrigger1;
        byte vbat;

        // 4 variables are here for general monitoring purpose
        UInt16 baroAlt; // div 10, 
        UInt16 debug2;             // 
        UInt16 debug3;       // debug3
        UInt16 debug4;       // debug4
        char endResponseCharacter; // 'M';
    }
    #endregion


    interface IConfigData
    {
        void WriteToDevice(SerialPort port);
        void ReadFromDevice(SerialPort port);
    }


    internal class ConfigData : IConfigData
    {


        public ConfigData()
        {
        }

        // Method that will convert a byte array to a struct:
        // use typeof(struct) to get 'Type' param.
        static public Object GetStruct(byte[] buffer, Type structType)
        {
            var structSize = Marshal.SizeOf(structType);
            if (buffer.Length != structSize)
            {
                throw new ArgumentException("Buffer length must be of equal size as struct");
            }

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Object obj = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), structType);
            handle.Free();
            return obj;
        }

        public void WriteToDevice(SerialPort port)
        {
        }

        public void ReadFromDevice(SerialPort port)
        {
        }
    }
}
 
 */ 

namespace Network.Packet
{

    public enum Type : byte
    {
        Chat,
        CreateServerSyncObject,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        public byte PacketType;
        public int PayloadSize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Chat // Dynamic size.
    {
        public Header header;
        public int messageSize;
        [MarshalAs(UnmanagedType.ByValArray)]
        public byte[] message;
    }
}