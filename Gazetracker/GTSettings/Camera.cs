// <copyright file="CameraSettings.cs" company="ITU">
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

using System;
using System.Xml;
using DirectShowLib;

namespace GTSettings
{
    public class Camera
    {
        #region CONSTANTS

        public const string Name = "CameraSettings";

        #endregion //CONSTANTS

        #region FIELDS

        private int brightness;
        private int brightnessDef = 135;
        private int brightnessMax = 255;
        private int brightnessStep = 1;
        private int contrast;

        private int contrastDef = 135;
        private int contrastMax = 255;
        private int contrastStep = 1;
        private string deviceName = "";
        private int exposure;
        private int exposureDef = 135;
        private int exposureMax = 255;
        private int exposureStep = 1;
        private bool flipImage = true;
        private int focus = 1;
        private int focusDef = 5;
        private int focusMax = 10;
        private int focusStep = 1;
        private int saturation;

        private int saturationDef = 135;
        private int saturationMax = 255;
        private int saturationStep = 1;
        private int sharpness;

        private int sharpnessDef = 135;
        private int sharpnessMax = 255;
        private int sharpnessStep = 1;
        private int zoom;
        private int zoomDef = 135;
        private int zoomMax = 255;
        private int zoomStep = 1;

        #endregion //FIELDS

        #region EVENTS

        #region Delegates

        public delegate void CameraControlPropertyChangeHandler(CameraControlProperty property, int value);

        public delegate void VideoControlFlagsChangeHandler(VideoControlFlags property, int value);

        public delegate void VideoProcAmpPropertyChangeHandler(VideoProcAmpProperty property, int value);

        #endregion

        public event CameraControlPropertyChangeHandler OnCameraControlPropertyChanged;

        public event VideoProcAmpPropertyChangeHandler OnVideoProcAmpPropertyChanged;

        public event VideoControlFlagsChangeHandler OnVideoControlFlagsChanged;

        #endregion EVENTS

        #region PROPERTIES

        #region Device

        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }


        public int DeviceNumber { get; set; }


        public int DeviceMode { get; set; }

        #endregion

        #region Brightness

        public int Brightness
        {
            get { return brightness; }
            set
            {
                brightness = value;
                PropertyChanged(VideoProcAmpProperty.Brightness, value);
            }
        }

        public bool BrightnessAuto { get; set; }

        public int BrightnessMinimum { get; set; }

        public int BrightnessMaximum
        {
            get { return brightnessMax; }
            set { brightnessMax = value; }
        }

        public int BrightnessDefault
        {
            get { return brightnessDef; }
            set { brightnessDef = value; }
        }

        public int BrightnessStepping
        {
            get { return brightnessStep; }
            set { brightnessStep = value; }
        }

        #endregion

        #region Contrast

        public int Contrast
        {
            get { return contrast; }
            set
            {
                contrast = value;
                PropertyChanged(VideoProcAmpProperty.Contrast, value);
            }
        }

        public bool ContrastAuto { get; set; }

        public int ContrastMinimum { get; set; }

        public int ContrastMaximum
        {
            get { return contrastMax; }
            set { contrastMax = value; }
        }

        public int ContrastDefault
        {
            get { return contrastDef; }
            set { contrastDef = value; }
        }

        public int ContrastStepping
        {
            get { return contrastStep; }
            set { contrastStep = value; }
        }

        #endregion

        #region Saturation

        public int Saturation
        {
            get { return saturation; }
            set
            {
                saturation = value;
                PropertyChanged(VideoProcAmpProperty.Saturation, value);
            }
        }

        public bool SaturationAuto { get; set; }

        public int SaturationMinimum { get; set; }

        public int SaturationMaximum
        {
            get { return saturationMax; }
            set { saturationMax = value; }
        }

        public int SaturationDefault
        {
            get { return saturationDef; }
            set { saturationDef = value; }
        }

        public int SaturationStepping
        {
            get { return saturationStep; }
            set { saturationStep = value; }
        }

        #endregion

        #region Sharpness

        public int Sharpness
        {
            get { return sharpness; }
            set
            {
                sharpness = value;
                PropertyChanged(VideoProcAmpProperty.Sharpness, value);
            }
        }

        public bool SharpnessAuto { get; set; }

        public int SharpnessMinimum { get; set; }

        public int SharpnessMaximum
        {
            get { return sharpnessMax; }
            set { sharpnessMax = value; }
        }

        public int SharpnessDefault
        {
            get { return sharpnessDef; }
            set { sharpnessDef = value; }
        }

        public int SharpnessStepping
        {
            get { return sharpnessStep; }
            set { sharpnessStep = value; }
        }

        #endregion

        #region Zoom

        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                PropertyChanged(CameraControlProperty.Zoom, value);
            }
        }

        public bool ZoomAuto { get; set; }

        public int ZoomMinimum { get; set; }

        public int ZoomMaximum
        {
            get { return zoomMax; }
            set { zoomMax = value; }
        }

        public int ZoomDefault
        {
            get { return zoomDef; }
            set { zoomDef = value; }
        }

        public int ZoomStepping
        {
            get { return zoomStep; }
            set { zoomStep = value; }
        }

        #endregion

        #region Focus

        public int Focus
        {
            get { return focus; }
            set
            {
                focus = value;
                PropertyChanged(CameraControlProperty.Focus, value);
            }
        }

        public bool FocusAuto { get; set; }

        public int FocusMinimum { get; set; }

        public int FocusMaximum
        {
            get { return focusMax; }
            set { focusMax = value; }
        }

        public int FocusDefault
        {
            get { return focusDef; }
            set { focusDef = value; }
        }

        public int FocusStepping
        {
            get { return focusStep; }
            set { focusStep = value; }
        }

        #endregion

        #region Exposure

        public int Exposure
        {
            get { return exposure; }
            set
            {
                exposure = value;
                PropertyChanged(CameraControlProperty.Exposure, value);
            }
        }

        public bool ExposureAuto { get; set; }

        public int ExposureMinimum { get; set; }

        public int ExposureMaximum
        {
            get { return exposureMax; }
            set { exposureMax = value; }
        }

        public int ExposureDefault
        {
            get { return exposureDef; }
            set { exposureDef = value; }
        }

        public int ExposureStepping
        {
            get { return exposureStep; }
            set { exposureStep = value; }
        }

        #endregion

        #region FlipImage

        public bool FlipImage
        {
            get { return flipImage; }
            set
            {
                flipImage = value;

                if (value)
                    PropertyChanged(VideoControlFlags.FlipVertical, 1);
                else
                    PropertyChanged(VideoControlFlags.FlipVertical, 0);
            }
        }

        #endregion

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            if (DeviceName.Length < 1)
            {
                DeviceName = "Settings " + DateTime.Now;
            }

            xmlWriter.WriteStartElement("CameraSettings");

            Settings.WriteElement(xmlWriter, "DeviceName", DeviceName);
            Settings.WriteElement(xmlWriter, "DeviceNumber", DeviceNumber.ToString());
            Settings.WriteElement(xmlWriter, "DeviceMode", DeviceMode.ToString());

            Settings.WriteElement(xmlWriter, "Brightness", Brightness.ToString());
            Settings.WriteElement(xmlWriter, "BrightnessAuto", BrightnessAuto.ToString());

            Settings.WriteElement(xmlWriter, "Contrast", Contrast.ToString());
            Settings.WriteElement(xmlWriter, "ContrastAuto", ContrastAuto.ToString());

            Settings.WriteElement(xmlWriter, "Saturation", Saturation.ToString());
            Settings.WriteElement(xmlWriter, "SaturationAuto", SaturationAuto.ToString());

            Settings.WriteElement(xmlWriter, "Sharpness", Sharpness.ToString());
            Settings.WriteElement(xmlWriter, "SharpnessAuto", SharpnessAuto.ToString());

            Settings.WriteElement(xmlWriter, "Zoom", Zoom.ToString());
            Settings.WriteElement(xmlWriter, "ZoomAuto", ZoomAuto.ToString());

            Settings.WriteElement(xmlWriter, "Focus", Focus.ToString());
            Settings.WriteElement(xmlWriter, "FocusAuto", FocusAuto.ToString());

            Settings.WriteElement(xmlWriter, "Exposure", Exposure.ToString());
            Settings.WriteElement(xmlWriter, "ExposureAuto", ExposureAuto.ToString());

            Settings.WriteElement(xmlWriter, "FlipImage", FlipImage.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = "";

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        sName = xmlReader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (sName)
                        {
                            case "DeviceName":
                                DeviceName = xmlReader.Value;
                                break;
                            case "DeviceNumber":
                                DeviceNumber = int.Parse(xmlReader.Value);
                                break;
                            case "DeviceMode":
                                DeviceMode = int.Parse(xmlReader.Value);
                                break;

                            case "Brightness":
                                Brightness = int.Parse(xmlReader.Value);
                                break;
                            case "BrightnessAuto":
                                BrightnessAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Contrast":
                                Contrast = int.Parse(xmlReader.Value);
                                break;
                            case "ContrastAuto":
                                BrightnessAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Saturation":
                                Saturation = int.Parse(xmlReader.Value);
                                break;
                            case "SaturationAuto":
                                SaturationAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Sharpness":
                                Sharpness = int.Parse(xmlReader.Value);
                                break;
                            case "SharpnessAuto":
                                SharpnessAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Zoom":
                                Zoom = int.Parse(xmlReader.Value);
                                break;
                            case "ZoomAuto":
                                ZoomAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Focus":
                                Focus = int.Parse(xmlReader.Value);
                                break;
                            case "FocusAuto":
                                FocusAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "Exposure":
                                Exposure = int.Parse(xmlReader.Value);
                                break;
                            case "ExposureAuto":
                                ExposureAuto = Convert.ToBoolean(xmlReader.Value);
                                break;

                            case "FlipImage":
                                FlipImage = Convert.ToBoolean(xmlReader.Value);
                                break;
                        }
                        break;
                }
            }
        }

        public string SettingsEncodeString()
        {
            string sep = ",";
            string str = "";

            str += "Brightness=" + Brightness + sep;
            str += "Contrast=" + Contrast + sep;
            str += "Saturation=" + Saturation + sep;
            str += "Sharpness=" + Sharpness + sep;
            str += "Zoom=" + Zoom + sep;
            str += "Focus=" + Focus + sep;
            str += "Exposure=" + Exposure + sep;
            str += "FlipImage=" + FlipImage;

            return str;
        }

        public void ExtractParametersFromString(string parameterStr)
        {
            try
            {
                // Seperating commands
                char[] sepParam = {','};
                string[] camParams = parameterStr.Split(sepParam, 20);

                // Seperating values/parameters
                char[] sepValues = {'='};

                foreach (string t in camParams)
                {
                    string[] cmdString = t.Split(sepValues, 5);
                    string subCmdStr = cmdString[0];
                    string value = cmdString[1];

                    switch (subCmdStr)
                    {
                        case "Brightness":
                            Brightness = int.Parse(value);
                            break;
                        case "Contrast":
                            Contrast = int.Parse(value);
                            break;
                        case "Saturation":
                            Saturation = int.Parse(value);
                            break;
                        case "Sharpness":
                            Sharpness = int.Parse(value);
                            break;
                        case "Zoom":
                            Zoom = int.Parse(value);
                            break;
                        case "Focus":
                            Focus = int.Parse(value);
                            break;
                        case "Exposure":
                            Exposure = int.Parse(value);
                            break;
                        case "FlipImage":
                            FlipImage = bool.Parse(value);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                //ErrorLogger.ProcessException(ex, true);
            }
        }

        #endregion //PUBLICMETHODS

        #region EVENTHANDLER

        private void PropertyChanged(VideoProcAmpProperty videoProcAmpProperty, int value)
        {
            if (OnVideoProcAmpPropertyChanged != null)
            {
                OnVideoProcAmpPropertyChanged(videoProcAmpProperty, value);
            }
        }

        private void PropertyChanged(CameraControlProperty cameraControlProperty, int value)
        {
            if (OnCameraControlPropertyChanged != null)
            {
                OnCameraControlPropertyChanged(cameraControlProperty, value);
            }
        }

        private void PropertyChanged(VideoControlFlags videoControlFlagsProperty, int value)
        {
            if (OnVideoControlFlagsChanged != null)
            {
                OnVideoControlFlagsChanged(videoControlFlagsProperty, value);
            }
        }

        #endregion //EVENTHANDLER

    }
}