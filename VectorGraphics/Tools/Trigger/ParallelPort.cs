// <copyright file="ParallelPort.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Tools.Trigger
{
  using System;
  using System.Runtime.InteropServices;
  using System.Threading;

  /// <summary>
  /// Class to access the Port API's from the inpout32.dll.
  /// </summary>
  /// <remarks>It uses the input32.dll from http://logix4u.net/.
  /// References:
  /// http://www.dotnettalk.net/Needed_help_on_printing_from_C_and_printer-6941579-1297-a.html
  /// http://www.dotnet247.com/247reference/msgs/16/84730.aspx
  /// http://www.groupsrv.com/dotnet/viewtopic.php?t=72572
  /// http://www.pinvoke.net/default.aspx/kernel32.CreateFile
  /// </remarks> 
  public static class ParallelPort
  {
#if WIN64
    /// <summary>
    /// This method will be used to send the data out to the parallel port.
    /// </summary>
    /// <param name="address">Address of the port to which the data needs to be sent.</param>
    /// <param name="value">Data that need to send out.</param>
    /// <remarks>Here the address 888 as int is actually 0x378 as Hex, which is the data 
    /// port of the parallel port. To reset the data that is sent on the data port, 
    /// you need to invoke the Output method with a value 0x00 i.e.0.
    /// <para></para>
    /// Register                            LPT1 LPT2
    /// Data Register (Base Address + 0)    0x378 0x278
    /// Status Register (Base Address + 1)  0x379 0x279
    /// Control Register (Base Address + 2) 0x37a 0x27a
    /// </remarks>
    [DllImport(@"inpoutx64.dll", EntryPoint = "Out32")]
    public static extern void Output(int address, int value);

    /// <summary>
    /// This method will be used to receive any data from the parallel port.
    /// </summary>
    /// <param name="address">Address of the port from which the data should be received.</param>
    /// <returns>Returns Integer read from the given port.</returns>
    [DllImport(@"inpoutx64.dll", EntryPoint = "Inp32")]
    public static extern int Input(int address);
#else
    /// <summary>
    /// This method will be used to send the data out to the parallel port.
    /// </summary>
    /// <param name="address">Address of the port to which the data needs to be sent.</param>
    /// <param name="value">Data that need to send out.</param>
    /// <remarks>Here the address 888 as int is actually 0x378 as Hex, which is the data 
    /// port of the parallel port. To reset the data that is sent on the data port, 
    /// you need to invoke the Output method with a value 0x00 i.e.0.
    /// <para></para>
    /// Register                            LPT1 LPT2
    /// Data Register (Base Address + 0)    0x378 0x278
    /// Status Register (Base Address + 1)  0x379 0x279
    /// Control Register (Base Address + 2) 0x37a 0x27a
    /// </remarks>
    [DllImport(@"inpout32.dll", EntryPoint = "Out32")]
    public static extern void Output(int address, int value);

    /// <summary>
    /// This method will be used to receive any data from the parallel port.
    /// </summary>
    /// <param name="address">Address of the port from which the data should be received.</param>
    /// <returns>Returns Integer read from the given port.</returns>
    [DllImport(@"inpout32.dll", EntryPoint = "Inp32")]
    public static extern int Input(int address);
#endif
    /// <summary>
    /// This method sends a trigger with the given value with the length
    /// given by signalTime to the port at the given address wich works
    /// only for LPT Ports.
    /// </summary>
    /// <param name="address">The <see cref="Int32"/> converted HEX address of
    /// the LPT port to send the signal to</param>
    /// <param name="signalTime">The time in milliseconds that indicates how
    /// long the signal will be set before it is reset to zero.</param>
    /// <param name="value">An <see cref="Int32"/> with the value to be sent.
    /// Can be from 0-255.</param>
    /// <remarks> LPT adress by default is 0x378=888, another is D800 =55296 </remarks>
    public static void SendTriggerToLPT(int address, int signalTime, int value)
    {
      Output(address, value);
      Thread.Sleep(signalTime);
      Output(address, 0);
    }
  }
}