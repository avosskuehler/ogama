// <copyright file="AslEnum.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>University Toulouse 2 - CLLE-LTC UMR5263</author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  /// <summary>
  /// Type of an item that is returned from the serial port COM.
  /// </summary>
  public enum EAslSerialOutPortType
  {
    /// <summary>The return type is <see cref="byte"/></summary>
    ASL_TYPE_BYTE = 0,

    /// <summary>The return type is <see cref="short"/></summary>
    ASL_TYPE_SHORT = 1,

    /// <summary>The return type is <see cref="int"/></summary>
    ASL_TYPE_UNSIGNED_SHORT = 2,

    /// <summary>The return type is <see cref="long"/></summary>
    ASL_TYPE_LONG = 3,

    /// <summary>The return type is unsigned integer on 64 bits</summary>
    ASL_TYPE_UNSIGNED_LONG = 4,

    /// <summary>The return type is <see cref="float"/></summary>
    ASL_TYPE_FLOAT = 5,

    /// <summary>There is no return value</summary>
    ASL_TYPE_NO_VALUE = 6
  }

  /// <summary>
  /// The serial port COM number.
  /// </summary>
  public enum EInterfacePort
  {
    /// <summary>1 form serial port com1</summary>
    COM1 = 1,

    /// <summary>2 form serial port com2</summary>
    COM2 = 2,

    /// <summary>3 form serial port com3</summary>
    COM3 = 3,

    /// <summary>4 form serial port com4</summary>
    COM4 = 4,

    /// <summary>5 form serial port com5</summary>
    COM5 = 5,

    /// <summary>6 form serial port com6</summary>
    COM6 = 6,

    /// <summary>7 form serial port com7</summary>
    COM7 = 7,

    /// <summary>8 form serial port com8</summary>
    COM8 = 8,

    /// <summary>9 form serial port com9</summary>
    COM9 = 9
  }
}