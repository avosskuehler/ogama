// <copyright file="NameShapeDlg.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.AOI.Dialogs
{
  using System.Windows.Forms;

  /// <summary>
  /// A pop up <see cref="Form"/>. Asks for the name of a newly defined area of interest shape.
  /// </summary>
  public partial class NameShapeDlg : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Initializes a new instance of the NameShapeDlg class.
    /// </summary>
    public NameShapeDlg()
    {
      this.InitializeComponent();
    }
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets shape name written in textbox field.
    /// </summary>
    /// <value>A <see cref="string"/> with the new shape name.</value>
    public string ShapeName
    {
      get { return this.txbShapeName.Text; }
    }

    #endregion //PROPERTIES
  }
}