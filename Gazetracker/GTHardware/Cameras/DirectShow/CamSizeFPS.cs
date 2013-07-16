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

namespace GTHardware.Cameras.DirectShow
{
    using System;

    public class CamSizeFPS : IComparable
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FPS { get; set; }

        public CamSizeFPS()
        {
        }

        public CamSizeFPS(int width, int height, int fps)
        {
            this.Width = width;
            this.Height = height;
            this.FPS = fps;
        }

        public int CompareTo(object obj)
        {
            if (obj is CamSizeFPS)
            {
                CamSizeFPS csf = (CamSizeFPS)obj;
                return (csf.Height + csf.Width + csf.FPS).CompareTo(this.Height + this.Width + this.FPS);
            }
            else
            {
                throw new ArgumentException("Object is not a CamSizeFPS");
            }
        }
    }
}