// <copyright file="StopConditionCollection.cs" company="FU Berlin">
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

namespace VectorGraphics.StopConditions
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Text;

  /// <summary>
  /// Derived from <see cref="CollectionBase"/>, implements 
  /// <see cref="ICustomTypeDescriptor"/> to provide custom type information.
  /// Inheriting from CollectionBase provides basic collection behavior. 
  /// Only, methods for adding, removing and querying items are be added. 
  /// </summary>
  /// <remarks>
  /// This is from http://www.codeproject.com/KB/tabs/customizingcollectiondata.aspx
  /// Customized display of collection data in a PropertyGrid By Gerd Klevesaat
  /// <para></para>
  /// Though the interface has a lot of methods, 
  /// most of the methods are trivial to implement because we 
  /// can delegate the call to a corresponding method of the static 
  /// TypeDescriptor object to provide standard behavior.
  /// The only methods we implement in a custom way are the 
  /// overloaded GetProperties()-methods. These are used to 
  /// return a collection of PropertyDescriptor objects used to 
  /// describe the properties, in a custom way. This property descriptor 
  /// objects will be used by the PropertyGrid later.</remarks>
  [TypeConverter(typeof(StopConditionCollectionConverter))]
  public class StopConditionCollection : CollectionBase, ICustomTypeDescriptor
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets <see cref="StopCondition"/> at the
    /// given index.
    /// </summary>
    /// <param name="index">An <see cref="int"/> with the elements index.</param>
    /// <returns>A <see cref="StopCondition"/> at the given Index.</returns>
    public StopCondition this[int index]
    {
      get { return (StopCondition)this.List[index]; }
      set { this.List[index] = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="StopConditionCollection"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this 
    /// <see cref="StopConditionCollection"/>.</returns>
    public override string ToString()
    {
      return this.GenerateStopConditionString();
    }

    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Adds an object to the end of the collection.
    /// </summary>
    /// <param name="stc">The <see cref="StopCondition"/> to be 
    /// added to the end of the collection.</param>
    public void Add(StopCondition stc)
    {
      this.List.Add(stc);
    }

    /// <summary>
    /// Determines whether the collection contains a specific element.
    /// </summary>
    /// <param name="stc">The Object to locate in the collection.</param>
    /// <returns><strong>True</strong> if the collection
    /// contains the specified value; otherwise, <strong>false</strong>.</returns>
    public bool Contains(StopCondition stc)
    {
      return this.List.Contains(stc);
    }

    /// <summary>
    /// Removes the first occurrence of a specific
    /// <see cref="StopCondition"/> from the collection. 
    /// </summary>
    /// <param name="stc">The <see cref="StopCondition"/>
    /// to remove from the collection.</param>
    public void Remove(StopCondition stc)
    {
      this.List.Remove(stc);
    }

    // Implementation of interface ICustomTypeDescriptor 
    #region ICustomTypeDescriptorImplementation

    /// <summary>
    /// Returns the class name of this instance of a component.
    /// </summary>
    /// <returns>The class name of the object, or a null 
    /// reference if the class does not have a name.</returns>
    public string GetClassName()
    {
      return TypeDescriptor.GetClassName(this, true);
    }

    /// <summary>
    /// Returns a collection of custom attributes 
    /// for this instance of a component. 
    /// </summary>
    /// <returns>An <see cref="AttributeCollection"/> containing 
    /// the attributes for this object. </returns>
    public AttributeCollection GetAttributes()
    {
      return TypeDescriptor.GetAttributes(this, true);
    }

    /// <summary>
    /// Returns the name of this instance of a component. 
    /// </summary>
    /// <returns>The name of the object, or a null reference 
    /// if the object does not have a name.</returns>
    public string GetComponentName()
    {
      return TypeDescriptor.GetComponentName(this, true);
    }

    /// <summary>
    /// Returns a type converter for this instance of a component. 
    /// </summary>
    /// <returns>A <see cref="TypeConverter"/> that is the converter 
    /// for this object, or a null reference 
    /// if there is no <see cref="TypeConverter"/> for this object.</returns>
    public TypeConverter GetConverter()
    {
      return TypeDescriptor.GetConverter(this, true);
    }

    /// <summary>
    /// Returns the default event for this instance of a component. 
    /// </summary>
    /// <returns>An EventDescriptor that represents the default 
    /// event for this object, or a null reference 
    /// if this object does not have events. </returns>
    public EventDescriptor GetDefaultEvent()
    {
      return TypeDescriptor.GetDefaultEvent(this, true);
    }

    /// <summary>
    /// Returns the default property for this instance of a component. 
    /// </summary>
    /// <returns>A PropertyDescriptor that represents the 
    /// default property for this object, or a null reference 
    /// if this object does not have properties.</returns>
    public PropertyDescriptor GetDefaultProperty()
    {
      return TypeDescriptor.GetDefaultProperty(this, true);
    }

    /// <summary>
    /// Returns an editor of the specified type for this instance of a component. 
    /// </summary>
    /// <param name="editorBaseType">A <see cref="Type"/> that represents the 
    /// editor for this object.</param>
    /// <returns>An Object of the specified type that is the editor for this 
    /// object, or a null reference
    /// if the editor cannot be found.</returns>
    public object GetEditor(Type editorBaseType)
    {
      return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    /// <summary>
    /// Returns the events for this instance of a component
    /// using the specified attribute array as a filter. 
    /// </summary>
    /// <param name="attributes">An array of type 
    /// <see cref="Attribute"/> that is used as a filter. </param>
    /// <returns>An <see cref="EventDescriptorCollection"/> that represents the 
    /// filtered events for this component instance. </returns>
    public EventDescriptorCollection GetEvents(Attribute[] attributes)
    {
      return TypeDescriptor.GetEvents(this, attributes, true);
    }

    /// <summary>
    /// Returns the events for this instance of a component. 
    /// </summary>
    /// <returns>An <see cref="EventDescriptorCollection"/> that represents the 
    /// events for this component instance. </returns>
    public EventDescriptorCollection GetEvents()
    {
      return TypeDescriptor.GetEvents(this, true);
    }

    /// <summary>
    /// Returns an object that contains the property 
    /// described by the specified property descriptor.
    /// </summary>
    /// <param name="pd">A <see cref="PropertyDescriptor"/> that represents 
    /// the property whose owner is to be found.</param>
    /// <returns>An Object that represents the owner of the specified property.</returns>
    public object GetPropertyOwner(PropertyDescriptor pd)
    {
      return this;
    }

    /// <summary>
    /// Called to get the properties of this type. Returns properties with certain
    /// attributes. This restriction is not implemented here.
    /// </summary>
    /// <param name="attributes">An array of type <see cref="Attribute"/> 
    /// that is used as a filter.</param>
    /// <returns>A <see cref="PropertyDescriptorCollection"/> that represents the 
    /// filtered properties for this component instance. </returns>
    public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    {
      return this.GetProperties();
    }

    /// <summary>
    /// Called to get the properties of this type.
    /// </summary>
    /// <returns>A <see cref="PropertyDescriptorCollection"/> that 
    /// represents the properties for this component instance.</returns>
    public PropertyDescriptorCollection GetProperties()
    {
      // Create a collection object to hold property descriptors
      PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

      // Iterate the list of employees
      for (int i = 0; i < this.List.Count; i++)
      {
        // Create a property descriptor for the StopCondition item 
        // and add to the property descriptor collection
        StopConditionCollectionPropertyDescriptor pd =
          new StopConditionCollectionPropertyDescriptor(this, i);
        pds.Add(pd);
      }

      // return the property descriptor collection
      return pds;
    }

    #endregion //ICustomTypeDescriptorImplementation

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Reads the StopConditionCollection and converts them into a stop condition string.
    /// </summary>
    /// <returns>A <see cref="string"/> with the stop condition.</returns>
    private string GenerateStopConditionString()
    {
      string stopConditionDescription = string.Empty;
      if (this.Count == 1)
      {
        if (this.List[0] is TimeStopCondition)
        {
          TimeStopCondition tsc = (TimeStopCondition)this.List[0];
          float duration = (float)(tsc.Duration / 1000f);
          string durationString = duration.ToString("N3");
          stopConditionDescription = "Show for " + durationString + " seconds";
        }
        else if (this.List[0] is MouseStopCondition)
        {
          MouseStopCondition msc = (MouseStopCondition)this.List[0];
          StringBuilder sb = new StringBuilder();
          stopConditionDescription = "Show until user pressed " +
            msc.ToString() + ".";
        }
        else if (this.List[0] is KeyStopCondition)
        {
          KeyStopCondition ksc = (KeyStopCondition)this.List[0];
          stopConditionDescription = "Show until user hits " +
            ksc.ToString() + ".";
        }
      }
      else if (this.Count == 0)
      {
        stopConditionDescription = "No stop condition !";
      }
      else
      {
        stopConditionDescription = "Multiple stop conditions";
      }

      return stopConditionDescription;
    }

    #endregion //HELPER
  }
}
