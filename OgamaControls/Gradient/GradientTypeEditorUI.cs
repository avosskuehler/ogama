#region - Terms of Usage -
/*
 * Copyright 2006 Sameera Perera
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */
#endregion   

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// User control that creates and edits custom gradients.
  /// </summary>
  [ToolboxBitmap(typeof(resfinder), "OgamaControls.Gradient.GradientTypeEditorUI.bmp")]
  public partial class GradientTypeEditorUI : UserControl
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
    /// current Marker
    /// </summary>
    private Marker marker;

    /// <summary>
    /// Event. Raised when gradients color blend has changed.
    /// </summary>
    public event EventHandler GradientChanged;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets or Sets current gradient.
    /// </summary>
    [Browsable(false), DefaultValue(null)]
    public Gradient Gradient
    {
      get { return this.gradientBuilder.Gradient; }
      set { this.gradientBuilder.Gradient = value; }
    }

    /// <summary>
    /// Gets or sets whether the GradientChanged event is fired when a marker
    /// is dragged.
    /// </summary>
    [DefaultValue(false)]
    public bool SilentMarkers
    {
      get { return this.gradientBuilder.SilentMarkers; }
      set { this.gradientBuilder.SilentMarkers = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Contructor. Initalizes Control
    /// </summary>
    public GradientTypeEditorUI()
    {
      InitializeComponent();

      colorEditor.EditedType = typeof(Color);
      colorEditor.Value = Color.White;

      Font editorFont = new Font(this.Font.FontFamily, 7.0f);
      colorEditor.Font = editorFont;
    }


    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    /// <summary>
    /// Color mixer field changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void colorMixerFieldValueChanged(object sender, EventArgs e)
    {
      if (marker != null)
        UpdateMarkerColor();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    /// <summary>
    /// Raised, when a marker is selected. Updates UI fields with marker values.
    /// </summary>
    /// <param name="sender">marker that is selected</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void gradientBuilder_MarkerSelected(object sender, EventArgs e)
    {
      Marker tempMarker = sender as Marker;
      marker = null; // Freeze all other event handlers
      if (tempMarker != null)
      {
        colorEditor.Value = tempMarker.Color;
        opacityBox.Value = Convert.ToDecimal(tempMarker.Color.A * (100.0f / 255));
        positionBox.Enabled = tempMarker.IsMovable;
        positionBox.Value = (int)(tempMarker.Position * 100.0f);
        grpEditor.Enabled = true;
      }
      else
        grpEditor.Enabled = false;
      marker = tempMarker;
    }

    /// <summary>
    /// Raises <c>GradientChanged</c> event.
    /// </summary>
    /// <param name="sender">GradientChanged</param>
    /// <param name="e">GradientChanged</param>
    private void gradientBuilder_GradientChanged(object sender, EventArgs e)
    {
      if (this.GradientChanged != null)
        GradientChanged(this, EventArgs.Empty);
    }

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
    /// <summary>
    /// Updates the markers color and position.
    /// </summary>
    private void UpdateMarkerColor()
    {
      Color color = (Color)colorEditor.Value;
      int a = Convert.ToInt32((float)opacityBox.Value * (255.0f / 100));
      marker.Color = Color.FromArgb(a, color);
      if (marker.IsMovable)
        marker.Position = (float)positionBox.Value / 100.0f;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
