// <copyright file="MirametrixSetting.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>

namespace Ogama.Modules.Recording.MirametrixInterface
{
    using System;
    using System.Drawing;
    using System.Xml.Serialization;
    using VectorGraphics.CustomTypeConverter;

    /// <summary>
    /// Class to save settings for the Mirametrix eye tracking system.
    /// It is XML serializable and can be stored in a file via 
    /// the <see cref="XmlSerializer"/> class.
    /// <example>XmlSerializer serializer = new XmlSerializer(typeof(MirametrixSetting));</example>
    /// </summary>
    [Serializable]
    public class MirametrixSetting
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Defining Constants                                                        //
        ///////////////////////////////////////////////////////////////////////////////
        #region CONSTANTS
        #endregion //CONSTANTS

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////
        #region FIELDS

        /// <summary>
        /// Saves the server adress of the tobii tracking system.
        /// </summary>
        private string _serverAddress;

        /// <summary>
        /// Saves the port for the server adress of the tobii tracking system.
        /// </summary>
        private int _serverPort;

        /// <summary>
        /// Saves the number of calibration points.
        /// </summary>
        private int _numCalibPoints;

        /// <summary>
        /// Saves the calibration point size.
        /// </summary>
        private int _calibPointSize;

        /// <summary>
        /// Saves the calibration speed.
        /// </summary>
        private int _calibPointSpeed;

        /// <summary>
        /// Saves the color for the calibration points.
        /// </summary>
        private Color _pointColor;

        /// <summary>
        /// Saves the background color of the calibration procedure.
        /// </summary>
        private Color _backgroundColor;

        /// <summary>
        /// Saves the flag, whether the calibration point order should 
        /// be randomized.
        /// </summary>
        private bool _randomizeCalibPointOrder;

        #endregion //FIELDS

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region CONSTRUCTION

        /// <summary>
        /// Initializes a new instance of the TobiiSetting class.
        /// </summary>
        public MirametrixSetting()
        {
            //this._serverAddress = "127.0.0.1";
            //this._serverPort = 4242;
            //this._numCalibPoints = 9;
            //this._calibPointSize = 22;
            //this._calibPointSpeed = 2;
            //this._pointColor = Color.Red;
            //this._backgroundColor = SystemColors.ControlLight;
            //this._randomizeCalibPointOrder = true;
        }

        #endregion //CONSTRUCTION

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////
        #region PROPERTIES

        /// <summary>
        /// Gets or sets the TLTServer IP host address.
        /// </summary>
        /// <value>A <see cref="string"/> with the server address.</value>
        /// <remarks>This can be detected via a ping to the serial number in the command line.</remarks>
        public string ServerAddress
        {
            get { return this._serverAddress; }
            set { this._serverAddress = value; }
        }

        /// <summary>
        /// Gets or sets the TLTServer IP port number.
        /// </summary>
        /// <value>A <see cref="int"/> with the port number, default is 4455.</value>
        public int TltServerPort
        {
            get { return this._serverPort; }
            set { this._serverPort = value; }
        }

        /// <summary>
        /// Gets or sets the number of calibration points that are shown.
        /// </summary>
        /// <remarks>5 are standard, 9 gives higher precision, 2 are for uncooperative subjects.</remarks>
        /// <value>A <see cref="Int32"/> with the number of points, can be  (2,5,9).</value>
        public int TltNumCalibPoint
        {
            get { return this._numCalibPoints; }
            set { this._numCalibPoints = value; }
        }

        /// <summary>
        /// Gets or sets the size of the calibration point to change its place.
        /// </summary>
        /// <value>A <see cref="Int32"/> which is small, medium or large.</value>
        public int TltCalibPointSizes
        {
            get { return this._calibPointSize; }
            set { this._calibPointSize = value; }
        }

        /// <summary>
        /// Gets or sets the speed of the Calibration point.
        /// </summary>
        /// <value>A <see cref="Int32"/> which is between fast and slow.</value>
        public int TltCalibPointSpeeds
        {
            get { return this._calibPointSpeed; }
            set { this._calibPointSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the color of the circles marking the calibration points.
        /// The default value is red.
        /// </summary>
        /// <value>A <see cref="Color"/> for the calibration points,
        /// which is converted into a OLE_COLOR Structure.</value>
        [XmlIgnoreAttribute()]
        public Color TltCalibPointColor
        {
            get { return this._pointColor; }
            set { this._pointColor = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SerializedTltCalibPointColor"/>.
        /// Serializes and deserializes the <see cref="TltCalibPointColor"/> to XML,
        /// because XMLSerializer can not serialize <see cref="Color"/> values.
        /// </summary>
        public string SerializedTltCalibPointColor
        {
            get { return ObjectStringConverter.ColorToHtmlAlpha(this._pointColor); }
            set { this._pointColor = ObjectStringConverter.HtmlAlphaToColor(value); }
        }

        /// <summary>
        /// Gets or sets the color of the calibration window background.
        /// </summary>
        /// <value>A <see cref="Color"/> for the background,
        /// which is converted into a OLE_COLOR Structure.</value>
        [XmlIgnoreAttribute()]
        public Color TltCalibBackgroundColor
        {
            get { return this._backgroundColor; }
            set { this._backgroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SerializedTltCalibBackgroundColor"/>.
        /// Serializes and deserializes the <see cref="TltCalibBackgroundColor"/> to XML,
        /// because XMLSerializer can not serialize <see cref="Color"/> values.
        /// </summary>
        public string SerializedTltCalibBackgroundColor
        {
            get { return ObjectStringConverter.ColorToHtmlAlpha(this._backgroundColor); }
            set { this._backgroundColor = ObjectStringConverter.HtmlAlphaToColor(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether calibration point order should be randomized or not.
        /// </summary>
        /// <value>A <see cref="Boolean"/> indicating the randomizing of the calibration points.</value>
        public bool TltRandomizeCalibPointOrder
        {
            get { return this._randomizeCalibPointOrder; }
            set { this._randomizeCalibPointOrder = value; }
        }

        #endregion //PROPERTIES

        ///////////////////////////////////////////////////////////////////////////////
        // Eventhandler                                                              //
        ///////////////////////////////////////////////////////////////////////////////
        #region EVENTS

        ///////////////////////////////////////////////////////////////////////////////
        // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
        ///////////////////////////////////////////////////////////////////////////////
        #region WINDOWSEVENTHANDLER
        #endregion //WINDOWSEVENTHANDLER

        ///////////////////////////////////////////////////////////////////////////////
        // Eventhandler for Custom Defined Events                                    //
        ///////////////////////////////////////////////////////////////////////////////
        #region CUSTOMEVENTHANDLER
        #endregion //CUSTOMEVENTHANDLER

        #endregion //EVENTS

        ///////////////////////////////////////////////////////////////////////////////
        // Methods and Eventhandling for Background tasks                            //
        ///////////////////////////////////////////////////////////////////////////////
        #region BACKGROUNDWORKER
        #endregion //BACKGROUNDWORKER

        ///////////////////////////////////////////////////////////////////////////////
        // Inherited methods                                                         //
        ///////////////////////////////////////////////////////////////////////////////
        #region OVERRIDES
        #endregion //OVERRIDES

        ///////////////////////////////////////////////////////////////////////////////
        // Methods for doing main class job                                          //
        ///////////////////////////////////////////////////////////////////////////////
        #region METHODS
        #endregion //METHODS

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region HELPER
        #endregion //HELPER
    }
}