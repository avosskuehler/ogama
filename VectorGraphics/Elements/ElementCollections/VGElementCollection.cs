// <copyright file="VGElementCollection.cs" company="FU Berlin">
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
  using System.Collections;
  using System.ComponentModel;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Derived from <see cref="CollectionBase"/>, implements 
  /// <see cref="ICustomTypeDescriptor"/> to provide custom type information.
  /// Inheriting from CollectionBase provides basic collection behavior. 
  /// Only, methods for adding, removing and querying items are be added. 
  /// </summary>
  /// <remarks>
  /// This is from http://www.codeproject.com/KB/tabs/customizingcollectiondata.aspx
  /// Customized display of collection data in a PropertyGrid By Gerd Klevesaat
  /// Though the interface has a lot of methods, 
  /// most of the methods are trivial to implement because we 
  /// can delegate the call to a corresponding method of the static 
  /// TypeDescriptor object to provide standard behavior.
  /// The only methods we implement in a custom way are the 
  /// overloaded GetProperties()-methods. These are used to 
  /// return a collection of PropertyDescriptor objects used to 
  /// describe the properties, in a custom way. This property descriptor 
  /// objects will be used by the PropertyGrid later.</remarks>
  [TypeConverter(typeof(VGElementCollectionConverter))]
  public class VGElementCollection : CollectionBase, ICustomTypeDescriptor
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

    /// <summary>
    /// Initializes a new instance of the VGElementCollection class.
    /// </summary>
    public VGElementCollection()
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets <see cref="VGElement"/> at the
    /// given index.
    /// </summary>
    /// <param name="index">An <see cref="int"/> with the elements index.</param>
    /// <returns>A <see cref="VGElement"/> at the given Index.</returns>
    public VGElement this[int index]
    {
      get { return (VGElement)this.List[index]; }
      set { this.List[index] = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Moves gives object to the end of the collection.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be 
    /// moved to the end of the collection.</param>
    public void ToTail(VGElement element)
    {
      if (element != null)
      {
        if (this.List.Contains(element))
        {
          this.List.Remove(element);
        }

        this.List.Add(element);
      }
    }

    /// <summary>
    /// Moves gives object to the beginning of the collection.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be 
    /// moved to the beginning of the collection.</param>
    public void ToHead(VGElement element)
    {
      if (element != null)
      {
        if (this.List.Contains(element))
        {
          this.List.Remove(element);
        }

        this.List.Insert(0, element);
      }
    }

    /// <summary>
    /// Moves gives object one step in direction beginning of the collection.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be 
    /// moved in direction beginning of the collection.</param>
    public void MoveDown(VGElement element)
    {
      if (element != null)
      {
        if (this.List.Contains(element))
        {
          int index = this.List.IndexOf(element);
          if (index > 0)
          {
            this.List.Remove(element);
            this.List.Insert(index - 1, element);
          }
        }
      }
    }

    /// <summary>
    /// Moves gives object one step in direction end of the collection.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be 
    /// moved in direction end of the collection.</param>
    public void MoveUp(VGElement element)
    {
      if (element != null)
      {
        if (this.List.Contains(element))
        {
          int index = this.List.IndexOf(element);
          if (index < this.List.Count - 1)
          {
            this.List.Remove(element);
            this.List.Insert(index + 1, element);
          }
        }
      }
    }

    /// <summary>
    /// Adds an object to the end of the collection.
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/> to be 
    /// added to the end of the collection.</param>
    public void Add(VGElement element)
    {
      if (element != null)
      {
        this.List.Add(element);
      }
    }

    /// <summary>
    /// Adds a range of <see cref="VGElement"/>s to the collection.
    /// </summary>
    /// <param name="elementCollection">The <see cref="VGElementCollection"/> to be 
    /// added to the end of the collection.</param>
    public void AddRange(VGElementCollection elementCollection)
    {
      foreach (VGElement element in elementCollection)
      {
        this.List.Add(element);
      }
    }

    /// <summary>
    /// Determines whether the collection contains a specific element.
    /// </summary>
    /// <param name="element">The Object to locate in the collection.</param>
    /// <returns><strong>True</strong> if the collection
    /// contains the specified value; otherwise, <strong>false</strong>.</returns>
    public bool Contains(VGElement element)
    {
      return this.List.Contains(element);
    }

    /// <summary>
    /// Removes the first occurrence of a specific
    /// <see cref="VGElement"/> from the collection. 
    /// </summary>
    /// <param name="element">The <see cref="VGElement"/>
    /// to remove from the collection.</param>
    public void Remove(VGElement element)
    {
      if (element != null)
      {
        if (this.List.Contains(element))
        {
          this.List.Remove(element);
        }
      }
    }

    /// <summary>
    /// Removes all occurrences of <see cref="VGElement"/>s
    /// with the given name from the collection. 
    /// </summary>
    /// <param name="name">The <see cref="string"/> with the name of the elements
    /// to remove from the collection.</param>
    public void Remove(string name)
    {
      if (name != string.Empty)
      {
        VGElementCollection removeList = new VGElementCollection();
        foreach (VGElement element in this.List)
        {
          if (element.Name == name)
          {
            removeList.Add(element);
          }
        }

        foreach (VGElement element in removeList)
        {
          this.List.Remove(element);
        }
      }
    }

    /// <summary>
    /// Returns <strong>true</strong>, if an element with the given
    /// name is member of the collection, otherwise <strong>false</strong>.
    /// </summary>
    /// <param name="name">The <see cref="string"/> with the name of the element
    /// to search for.</param>
    /// <returns><strong>True</strong>, if an element with the given
    /// name is member of the collection, otherwise <strong>false</strong>.</returns>
    public bool Contains(string name)
    {
      if (name != string.Empty)
      {
        foreach (VGElement element in this.List)
        {
          if (element.Name == name)
          {
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Removes the first occurrence of a specific
    /// <see cref="VGElement"/> from the collection. 
    /// </summary>
    /// <param name="list">The <see cref="VGElement"/>
    /// to remove from the collection.</param>
    public void RemoveAll(VGElementCollection list)
    {
      if (list != null && list.Count > 0)
      {
        foreach (VGElement element in list)
        {
          if (this.List.Contains(element))
          {
            this.List.Remove(element);
          }
        }
      }
    }

    /// <summary>
    /// Removes a range of elements from the collection
    /// </summary>
    /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
    /// <param name="count">The number of elements to remove.</param>
    public void RemoveRange(int index, int count)
    {
      for (int i = 0; i < count; i++)
      {
        this.List.RemoveAt(index);
      }
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
        // Create a property descriptor for the VGElement item 
        // and add to the property descriptor collection
        VGElementCollectionPropertyDescriptor pd =
          new VGElementCollectionPropertyDescriptor(this, i);
        pds.Add(pd);
      }

      // return the property descriptor collection
      return pds;
    }

    #endregion //ICustomTypeDescriptorImplementation

    /// <summary>
    /// Searches the collection list for all members with
    /// the given group and returns this items in a new
    /// <see cref="VGElementCollection"/>
    /// </summary>
    /// <param name="searchGroup">The <see cref="VGStyleGroup"/>
    /// that the elements should match.</param>
    /// <returns>A <see cref="VGElementCollection"/> with the 
    /// members of the list that are in the given search group.</returns>
    public VGElementCollection FindAllGroupMembers(VGStyleGroup searchGroup)
    {
      VGElementCollection subList = new VGElementCollection();
      foreach (VGElement element in this.List)
      {
        if ((element.StyleGroup & searchGroup) == searchGroup)
        {
          subList.Add(element);
        }
      }

      return subList;
    }

    /// <summary>
    /// Gets the first element in the baselist with the given name
    /// or null, if no one is found.
    /// </summary>
    /// <param name="name">A <see cref="string"/> with the name of
    /// an element to search for.</param>
    /// <returns>The first <see cref="VGElement"/> with this name,
    /// or <strong>null</strong> if none is found.</returns>
    public VGElement GetElementByName(string name)
    {
      foreach (VGElement element in this.List)
      {
        if (element.Name == name)
        {
          return element;
        }
      }

      return null;
    }

    #endregion //PUBLICMETHODS

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
    /// Overridden. Performs additional custom processes when clearing 
    /// the contents of the CollectionBase instance.
    /// Disposes the <see cref="VGElement"/>s before clearing the list.
    /// </summary>
    protected override void OnClear()
    {
      this.Dispose();
      base.OnClear();
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Disposes the elements in this list and clears the list afterwards.
    /// </summary>
    private void Dispose()
    {
      foreach (VGElement element in this)
      {
        element.Dispose();
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
