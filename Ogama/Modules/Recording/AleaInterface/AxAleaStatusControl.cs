// <copyright file="AxAleaStatusControl.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>

namespace Ogama.Modules.Recording.AleaInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// ActiveX Wrapper. Allows to create the ActiveX dynamically without adding any references to the solution.
  /// Internally the ActiveX-CLSID is passed to the underlying AxHost constructor.
  /// </summary>
  public class AxAleaStatusControl : AxHost
  {
    /// <summary>
    /// Initializes a new instance of the AxAleaStatusControl class.
    /// Passes the CLSID of the Alea Status Control to the underlying AxHost constructor.
    /// </summary>
    /// <exception cref="System.NullReferenceException">Thrown when ActiveX-Control is not registered by IntelliGaze.</exception>
    public AxAleaStatusControl()
      : base(Type.GetTypeFromProgID("ALEA.HeadDisplayCtrl", false).GUID.ToString())
    {
    }
  }
}