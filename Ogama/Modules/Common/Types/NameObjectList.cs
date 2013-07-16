// <copyright file="NameObjectList.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.Types
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  /// This class is a Generic <see cref="IList"/> that is accessible
  /// through the index AND a <see cref="string"/> Key like the
  /// <see cref="IDictionary"/>.
  /// </summary>
  /// <typeparam name="T">The type of the value objects.</typeparam>
  public class NameObjectList<T> : IList<T>, IXmlSerializable
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
    /// Saves the string key collection.
    /// </summary>
    private List<string> keys;

    /// <summary>
    /// Saves the values collection.
    /// </summary>
    private List<T> values;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the NameObjectList class.
    /// </summary>
    public NameObjectList()
    {
      this.Clear();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the list of keys of the List.
    /// </summary>
    public virtual List<string> Keys
    {
      get { return this.keys; }
    }

    /// <summary>
    /// Gets the list of values of the List.
    /// </summary>
    public virtual List<T> Values
    {
      get { return this.values; }
    }

    /// <summary>
    /// Gets a value indicating whether the IList is read-only. 
    /// This implementation returns always false,
    /// so instances are always writable.
    /// </summary>
    /// <remarks>A collection that is read-only does not allow 
    /// the addition, removal, or modification of elements 
    /// after the collection is created.</remarks>
    public bool IsReadOnly
    {
      get { return false; }
    }

    /// <summary>
    /// Gets the number of elements actually contained in the List.
    /// </summary>
    public virtual int Count
    {
      get { return this.values.Count; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Gets or sets the object at the given index
    /// </summary>
    /// <param name="index">The zero based index of the collection.</param>
    /// <returns>The object at the given index.</returns>
    public virtual T this[int index]
    {
      get { return this.values[index]; }
      set { this.values[index] = value; }
    }

    /// <summary>
    /// Gets or sets the object with the given key.
    /// </summary>
    /// <param name="name">A <see cref="string"/> with the key of the value to retreive.</param>
    /// <returns>The object with the given key.</returns>
    public virtual T this[string name]
    {
      get { return this.values[this.keys.IndexOf(name)]; }
      set { this.values[this.keys.IndexOf(name)] = value; }
    }

    /// <summary>
    /// Removes all element from this list.
    /// </summary>
    public virtual void Clear()
    {
      this.keys = new List<string>();
      this.values = new List<T>();
    }

    /// <summary>
    /// Copies the entire List to a compatible one-dimensional array, 
    /// starting at the beginning of the target array. 
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination 
    /// of the elements copied from List. The Array must have zero-based indexing.</param>
    public virtual void CopyTo(T[] array)
    {
      this.values.CopyTo(array);
    }

    /// <summary>
    /// Copies the entire List to a compatible one-dimensional array, 
    /// starting at the specified index of the target array. 
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination 
    /// of the elements copied from List. The Array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at 
    /// which copying begins.</param>
    public virtual void CopyTo(T[] array, int arrayIndex)
    {
      this.values.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Copies a range of elements from the List to a compatible 
    /// one-dimensional array, starting at the specified index of the target array. 
    /// </summary>
    /// <param name="index">The zero-based index in the source List at which copying begins.</param>
    /// <param name="array">The one-dimensional Array that is the destination 
    /// of the elements copied from List. The Array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at 
    /// which copying begins.</param>
    /// <param name="count">The number of elements to copy.</param>
    public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      this.values.CopyTo(index, array, arrayIndex, count);
    }

    /// <summary>
    /// Returns the zero-based index of the first occurrence of a value in the List or in a portion of it. 
    /// </summary>
    /// <param name="item">The object to locate in the List. 
    /// The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
    /// <returns>The zero-based index of the first occurrence of 
    /// item within the entire List, if found; otherwise, –1.</returns>
    public int IndexOf(T item)
    {
      return this.values.IndexOf(item);
    }

    /// <summary>
    /// Returns the zero-based index of the first occurrence of a value 
    /// with the given key in the List or in a portion of it. 
    /// </summary>
    /// <param name="name">The key of the object to locate in the List.</param>
    /// <returns>The zero-based index of the first occurrence of 
    /// item within the entire List, if found; otherwise, –1.</returns>
    public int IndexOf(string name)
    {
      return this.keys.IndexOf(name);
    }

    /// <summary>
    /// Inserts the elements of a collection into the List at the specified index. 
    /// </summary>
    /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
    /// <param name="names">The keys for the collection objects to be inserted.</param>
    /// <param name="items">The collection whose elements should be inserted 
    /// into the List. The collection itself cannot be a null reference 
    /// (Nothing in Visual Basic), but it can contain elements that are a 
    /// null reference (Nothing in Visual Basic), if type T is a reference type.</param>
    /// <remarks>names and items list should be of equal length,
    /// otherwise it will throw a <see cref="ArgumentOutOfRangeException"/></remarks>
    public void InsertRange(int index, List<string> names, IList<T> items)
    {
      if (names.Count != items.Count)
      {
        throw new ArgumentOutOfRangeException("Names and items collection lengths are not equal");
      }

      int count = 0;
      for (int i = 0; i < names.Count; i++)
      {
        string name = names[i];
        T item = items[i];
        if (this.keys.Contains(name))
        {
          this[name] = item;
        }
        else
        {
          this.keys.Insert(index + count, name);
          this.values.Insert(index + count, item);
          count++;
        }
      }
    }

    /// <summary>
    /// Inserts an element into the List at the specified index with the given key.
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="name">The key for the object to insert.</param>
    /// <param name="item">The object to insert. The value can be a null 
    /// reference (Nothing in Visual Basic) for reference types.</param>
    public void Insert(int index, string name, T item)
    {
      this.CheckIndex(index);
      if (this.keys.Contains(name))
      {
        this[name] = item;
      }
      else
      {
        this.keys.Insert(index, name);
        this.values.Insert(index, item);
      }
    }

    /// <summary>
    /// Inserts an element into the List at the specified index with a
    /// key that is retreived from object.ToString()
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The object to insert. The value can be a null 
    /// reference (Nothing in Visual Basic) for reference types.</param>
    public void Insert(int index, T item)
    {
      this.CheckIndex(index);
      string name = item.ToString();
      if (this.keys.Contains(name))
      {
        this[name] = item;
      }
      else
      {
        this.keys.Insert(index, name);
        this.values.Insert(index, item);
      }
    }

    /// <summary>
    /// Removes the element at the specified index of the List. 
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    public void RemoveAt(int index)
    {
      this.CheckIndex(index);
      if (this.values.Count > index)
      {
        this.keys.RemoveAt(index);
        this.values.RemoveAt(index);
      }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the List. 
    /// </summary>
    /// <param name="item">The object to remove from the List. 
    /// The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
    /// <returns><strong>true</strong> if item is successfully removed; otherwise, <strong>false</strong>. 
    /// This method also returns <strong>false</strong> if item was not found in the List. </returns>
    public virtual bool Remove(T item)
    {
      int itemIndex = this.values.IndexOf(item);
      if (itemIndex > -1)
      {
        this.keys.RemoveAt(itemIndex);
        this.values.Remove(item);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Removes the first occurrence of a specific object 
    /// with the given key from the List. 
    /// </summary>
    /// <param name="name">The key of the object to remove from the List.</param>
    /// <returns><strong>true</strong> if item is successfully removed; otherwise, <strong>false</strong>. 
    /// This method also returns <strong>false</strong> if item was not found in the List. </returns>
    public virtual bool Remove(string name)
    {
      int itemIndex = this.keys.IndexOf(name);
      if (itemIndex > -1)
      {
        this.keys.RemoveAt(itemIndex);
        this.values.RemoveAt(itemIndex);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Adds an object to the end of the List. 
    /// The key is retreived from object.ToString().
    /// </summary>
    /// <param name="item">The object to be added to the end of the List. 
    /// The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
    public void Add(T item)
    {
      string name = item.ToString();
      if (this.keys.Contains(name))
      {
        this[name] = item;
      }
      else
      {
        this.keys.Add(name);
        this.values.Add(item);
      }
    }

    /// <summary>
    /// Adds an object with the given key to the end of the List. 
    /// </summary>
    /// <param name="name">The key for the object.</param>
    /// <param name="item">The object to be added to the end of the List. 
    /// The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
    public virtual void Add(string name, T item)
    {
      if (this.keys.Contains(name))
      {
        this[name] = item;
      }
      else
      {
        this.keys.Add(name);
        this.values.Add(item);
      }
    }

    /// <summary>
    /// Adds the elements of the specified collection to the end of the List. 
    /// </summary>
    /// <param name="names">The keys for the elements to be added.</param>
    /// <param name="items">The collection whose elements should be added to 
    /// the end of the List. The collection itself cannot be a null reference 
    /// (Nothing in Visual Basic), but it can contain elements 
    /// that are a null reference (Nothing in Visual Basic), if type T is a reference type.</param>
    /// <remarks>names and items list should be of equal length,
    /// otherwise it will throw a <see cref="ArgumentOutOfRangeException"/></remarks>
    public void AddRange(List<string> names, IList<T> items)
    {
      if (names.Count != items.Count)
      {
        throw new ArgumentOutOfRangeException("Names and items collection lengths are not equal");
      }

      for (int i = 0; i < names.Count; i++)
      {
        string name = names[i];
        T item = items[i];
        if (this.keys.Contains(name))
        {
          this[name] = item;
        }
        else
        {
          this.keys.Add(name);
          this.values.Add(item);
        }
      }
    }

    /// <summary>
    /// Determines whether an element is in the List. 
    /// </summary>
    /// <param name="item">The object to locate in the List. 
    /// The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
    /// <returns><strong>true</strong> if item is found in the List; otherwise, <strong>false</strong>. 
    /// </returns>
    public bool Contains(T item)
    {
      if (this.values.Contains(item))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Determines whether an element of the given key is in the List. 
    /// </summary>
    /// <param name="name">The key of the object to locate in the List.</param>
    /// <returns><strong>true</strong> if item is found in the List; otherwise, <strong>false</strong>. 
    /// </returns>
    public bool Contains(string name)
    {
      if (this.keys.Contains(name))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

        /// <summary>
    /// Returns an enumerator that iterates through the List. 
    /// </summary>
    /// <returns>A List.Enumerator for the List. </returns>
    public IEnumerator<T> GetEnumerator()
    {
      return this.values.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the List. 
    /// </summary>
    /// <returns>A List.Enumerator for the List. </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.values.GetEnumerator();
    }

    /// <summary>
    /// Removes the all the elements that match the conditions defined by the specified predicate. 
    /// </summary>
    /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the 
    /// conditions of the elements to remove.</param>
    public void RemoveAll(Predicate<T> match)
    {
      foreach (T item in this.values)
      {
        if (match(item))
        {
          this.Remove(item);
        }
      }
    }

    /// <summary>
    /// Retrieves the all the elements that match the conditions defined by the specified predicate. 
    /// </summary>
    /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the 
    /// conditions of the elements to search for.</param>
    /// <returns>A List containing all the elements that match the 
    /// conditions defined by the specified predicate, 
    /// if found; otherwise, an empty List. </returns>
    public List<T> FindAll(Predicate<T> match)
    {
      List<T> results = new List<T>();
      foreach (T item in this.values)
      {
        if (match(item))
        {
          results.Add(item);
        }
      }

      return results;
    }

    #region IXmlSerializable Members

    /// <summary>
    /// This property is reserved, apply the 
    /// <see cref="XmlSchemaProviderAttribute"/> to the class instead. 
    /// </summary>
    /// <returns>In this implementation it returns null, because we do not use it.</returns>
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }

    /// <summary>
    /// Generates an <see cref="Dictionary{TKey,TValue}"/>  from its XML representation. 
    /// </summary>
    /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from 
    /// which the dictionary is deserialized. </param>
    public void ReadXml(System.Xml.XmlReader reader)
    {
      XmlSerializer keySerializer = new XmlSerializer(typeof(string));
      XmlSerializer valueSerializer = new XmlSerializer(typeof(T));

      bool wasEmpty = reader.IsEmptyElement;
      reader.Read();
      if (wasEmpty)
      {
        return;
      }

      while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
      {
        reader.ReadStartElement("item");
        reader.ReadStartElement("key");
        string key = (string)keySerializer.Deserialize(reader);
        reader.ReadEndElement();

        reader.ReadStartElement("value");
        T value = (T)valueSerializer.Deserialize(reader);
        reader.ReadEndElement();

        this.Add(key, value);

        reader.ReadEndElement();
        reader.MoveToContent();
      }

      reader.ReadEndElement();
    }

    /// <summary>
    /// Converts this <see cref="NameObjectList{T}"/>  into its XML representation. 
    /// </summary>
    /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream 
    /// to which the <see cref="NameObjectList{T}"/>  is serialized. </param>
    public void WriteXml(System.Xml.XmlWriter writer)
    {
      XmlSerializer keySerializer = new XmlSerializer(typeof(string));
      XmlSerializer valueSerializer = new XmlSerializer(typeof(T));
      foreach (string key in this.Keys)
      {
        writer.WriteStartElement("item");
        writer.WriteStartElement("key");
        keySerializer.Serialize(writer, key);
        writer.WriteEndElement();
        writer.WriteStartElement("value");
        T value = this[key];
        valueSerializer.Serialize(writer, value);
        writer.WriteEndElement();
        writer.WriteEndElement();
      }
    }

    #endregion

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
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
    #region PRIVATEMETHODS

    /// <summary>
    /// This method checks the given index 
    /// to be in the range of the values list.
    /// </summary>
    /// <param name="index">The zero-based index to check.</param>
    private void CheckIndex(int index)
    {
      if (index < 0 || index > this.values.Count)
      {
        throw new ArgumentOutOfRangeException("index");
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