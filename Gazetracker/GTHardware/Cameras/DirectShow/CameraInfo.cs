// <copyright file="GTSettings.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Martin Tall</author>
// <email>tall@stanford.edu</email>
// <modifiedby>Adrian Voßkühler</modifiedby>

using System.Collections.Generic;
using DirectShowLib;

namespace GTHardware.Cameras.DirectShow
{
    public class CameraInfo
    {
        public CameraInfo()
        {
            SupportedSizesAndFPS = new List<CamSizeFPS>();
        }

        public string Name { get; set; }
        public DsDevice DirectshowDevice { get; set; }
        public List<CamSizeFPS> SupportedSizesAndFPS { get; set; }

        public bool IsValidCamera
        {
            get
            {
                if (DirectshowDevice == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}