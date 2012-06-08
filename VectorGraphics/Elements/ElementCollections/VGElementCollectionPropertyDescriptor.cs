// <copyright file="VGElementCollectionPropertyDescriptor.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements.ElementCollections
{
  using System;
  using System.ComponentModel;

  /// <summary>
  /// Derived from <see cref="PropertyDescriptor"/>.
  /// This implementation ensures a correct expandable
  /// property list for the <see cref="System.Windows.Forms.PropertyGrid"/> of
  /// a <see cref="VGElementCollection"/> of <see cref="VGElement"/>s.
  /// </summary>
  /// <remarks>It is used in the <see cref="VGElementCollection"/>
  /// <see cref="ICustomTypeDescriptor"/> implementation.</remarks>
  public class VGElementCollectionPropertyDescriptor : PropertyDescriptor
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
    /// Saves the current <see cref="VGElementCollection"/>
    /// </summary>
    private VGElementCollection collection = null;

    /// <summary>
    /// Saves the current index of the collection.
    /// </summary>
    private int index = -1;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGElementCollectionPropertyDescriptor class.
    /// </summary>
    /// <param name="collection">The <see cref="VGElementCollection"/>
    /// for this <see cref="PropertyDescriptor"/></param>
    /// <param name="index">The index to use.</param>
    public VGElementCollectionPropertyDescriptor(VGElementCollection collection, int index)
      : base("#" + index.ToString(), null)
    {
      this.collection = collection;
      this.index = index;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the collection of attributes for this member.
    /// Overridden <see cref="MemberDescriptor.Attributes"/>.
    /// This implementation returns an empty <see cref="AttributeCollection"/>.
    /// </summary>
    /// <value>An <see cref="AttributeCollection"/> that provides the 
    /// attributes for  this member, or an empty collection if there 
    /// are no attributes in the <see cref="MemberDescriptor.AttributeArray"/>. </value>
    public override AttributeCollection Attributes
    {
      get { return new AttributeCollection(null); }
    }

    /// <summary>
    /// Gets the type of the component this property is bound to. 
    /// Overridden <see cref="PropertyDescriptor.ComponentType"/>.
    /// This implementation returns a <see cref="VGElementCollection"/> type.
    /// </summary>
    /// <value>A <see cref="Type"/> that represents the type of component this property 
    /// is bound to. When the GetValue or SetValue methods are invoked, 
    /// the object specified might be an instance of this type. </value>
    public override Type ComponentType
    {
      get { return this.collection.GetType(); }
    }

    /// <summary>
    /// Gets the name that can be displayed in a window, 
    /// such as a Properties window. 
    /// Overridden <see cref="MemberDescriptor.DisplayName"/>.
    /// This implementation returns the string representation of the current
    /// VGElement.
    /// </summary>
    /// <remarks>This property is the description at the left side of the row
    /// of the property grid.</remarks>
    /// <value>A <see cref="string"/> with the name to display for the member. </value>
    public override string DisplayName
    {
      get
      {
        VGElement element = this.collection[this.index];
        return element.GetType().Name;
      }
    }

    /// <summary>
    /// Gets the description of the member, as specified in the 
    /// <see cref="DescriptionAttribute"/>. 
    /// Overridden <see cref="MemberDescriptor.Description"/>.
    /// This implementation returns the string representation of the current
    /// <see cref="VGElement"/>.
    /// </summary>
    /// <remarks>This property is the description at the bottom
    /// of the property grid.</remarks>
    /// <value>The description of the member. This is the string
    /// representation of the current
    /// <see cref="VGElement"/></value>
    public override string Description
    {
      get
      {
        VGElement element = this.collection[this.index];
        return element.ToString();
      }
    }

    /// <summary>
    /// Gets a value indicating whether this property is read-only.
    /// Overridden <see cref="PropertyDescriptor.IsReadOnly"/>.
    /// This implementation returns always false, which means,
    /// this is not read only.
    /// </summary>
    /// <value><strong>True</strong> if the property is read-only; 
    /// otherwise, <strong>false</strong>.</value>
    public override bool IsReadOnly
    {
      get { return false; }
    }

    /// <summary>
    /// Gets the name of the member.
    /// Overridden <see cref="MemberDescriptor.Name"/>.
    /// This implementation returns the number of the current 
    /// collection member. (e.g "#1").
    /// </summary>
    /// <value>A <see cref="string"/> with the name of the member.</value>
    public override string Name
    {
      get { return "#" + this.index.ToString(); }
    }

    /// <summary>
    /// Gets the type of the property.
    /// Overridden <see cref="PropertyDescriptor.PropertyType"/>.
    /// This implementation returns the type of
    /// <see cref="VGElement"/>.
    /// </summary>
    /// <value>A <see cref="Type"/> that represents the type of the property. </value>
    public override Type PropertyType
    {
      get { return this.collection[this.index].GetType(); }
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

    /// <summary>
    /// Overridden <see cref="PropertyDescriptor.CanResetValue(object)"/>.
    /// Returns whether resetting an object changes its value. 
    /// This implementation returns always true.
    /// </summary>
    /// <param name="component">The component to test for reset capability.</param>
    /// <returns><strong>True</strong> if resetting the component changes 
    /// its value; otherwise, <strong>false</strong>.</returns>
    public override bool CanResetValue(object component)
    {
      return true;
    }

    /// <summary>
    /// Overridden <see cref="PropertyDescriptor.GetValue(object)"/>.
    /// Gets the current value of the property on a component. 
    /// This implementation returns the current <see cref="VGElement"/>.
    /// </summary>
    /// <param name="component">The component with the property for 
    /// which to retrieve the value.</param>
    /// <returns>A <see cref="object"/> with a <see cref="VGElement"/>
    /// value of a property for the given component.</returns>
    public override object GetValue(object component)
    {
      return this.collection[this.index];
    }

    /// <summary>
    /// Overridden <see cref="PropertyDescriptor.GetValue(object)"/>.
    /// Resets the value for this property of the component to the default value. 
    /// This implementation returns the current <see cref="VGElement"/>.
    /// </summary>
    /// <param name="component">The component with the property value that 
    /// is to be reset to the default value.</param>
    public override void ResetValue(object component)
    {
      this.collection[this.index].Reset();
    }

    /// <summary>
    /// Overridden <see cref="PropertyDescriptor.GetValue(object)"/>.
    /// Determines a value indicating whether the value 
    /// of this property needs to be persisted. 
    /// This implementation returns true.
    /// </summary>
    /// <param name="component">The component with the property to 
    /// be examined for persistence.</param>
    /// <returns><strong>True</strong> if resetting the property 
    /// should be persisted; otherwise, <strong>false</strong>.</returns>
    public override bool ShouldSerializeValue(object component)
    {
      return true;
    }

    /// <summary>
    /// Overridden <see cref="PropertyDescriptor.GetValue(object)"/>.
    /// Sets the value of the component to a different value. 
    /// </summary>
    /// <param name="component">The component with the property 
    /// value that is to be set.</param>
    /// <param name="value">The new value.</param>
    public override void SetValue(object component, object value)
    {
      this.collection[this.index] = (VGElement)value;
    }

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