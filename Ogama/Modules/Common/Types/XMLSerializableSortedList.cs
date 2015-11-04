// <copyright file="XMLSerializableSortedList.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  /// This class extends the generic <see cref="SortedList{TKey,TValue}"/> class with
  /// the possibility to XML Serialize with <see cref="XmlSerializer"/>.
  /// That is done implementing the <see cref="IXmlSerializable"/> interface.
  /// </summary>
  /// <remarks>We have to create all the constructors of the base class,
  /// because they are not inherited.</remarks>
  /// <typeparam name="TKey">The key of the underlying dictionary.</typeparam>
  /// <typeparam name="TValue">The value of the  underlying dictionary.</typeparam>
  [Serializable]
  public class XMLSerializableSortedList<TKey, TValue>
      : SortedList<TKey, TValue>, IXmlSerializable
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
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> 
    /// that is empty, has the default initial capacity and is sorted 
    /// according to the IComparable interface implemented by each key 
    /// added to the SortedList. 
    /// </summary>
    /// <seealso cref="SortedList{TKey,TValue}"/>
    public XMLSerializableSortedList()
      : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> that is empty, 
    /// has the default initial capacity and is sorted according to 
    /// the specified generic <see cref="IComparer{T}"/> interface.
    /// </summary>
    /// <param name="comparer">The generic <see cref="IComparer{T}"/> implementation to 
    /// use when comparing keys. -or- 
    /// a null reference to use the generic <see cref="IComparer{T}"/> implementation of each key.</param>
    public XMLSerializableSortedList(IComparer<TKey> comparer)
      : base(comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> that
    /// contains elements copied from the specified generic <see cref="IDictionary{TKey,TValue}"/>, 
    /// has the same initial capacity as the number of elements copied, 
    /// and is sorted according to the generic <see cref="IComparer{T}"/> interface implemented by each key. 
    /// </summary>
    /// <param name="dictionary">The generic <see cref="IDictionary{TKey,TValue}"/> to copy 
    /// to a new generic <see cref="SortedList{TKey,TValue}"/>.</param>
    public XMLSerializableSortedList(IDictionary<TKey, TValue> dictionary)
      : base(dictionary)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> hat 
    /// is empty, has the specified initial capacity and is sorted 
    /// according to the generic <see cref="IComparer{T}"/> interface implemented by each 
    /// key added to the generic <see cref="SortedList{TKey,TValue}"/>. 
    /// </summary>
    /// <param name="capacity">The initial number of elements 
    /// that the generic <see cref="SortedList{TKey,TValue}"/> can contain. </param>
    public XMLSerializableSortedList(int capacity)
      : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> 
    /// that is empty, has the specified initial capacity, 
    /// and is sorted according to the specified generic <see cref="IComparer{T}"/> interface. 
    /// </summary>
    /// <param name="capacity">The initial number of elements 
    /// that the generic <see cref="SortedList{TKey,TValue}"/> can contain. </param>
    /// <param name="comparer">The generic <see cref="IComparer{T}"/> implementation to 
    /// use when comparing keys. -or- 
    /// a null reference to use the generic <see cref="IComparer{T}"/> implementation of each key.</param>
    public XMLSerializableSortedList(int capacity, IComparer<TKey> comparer)
      : base(capacity, comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableSortedList class 
    /// generic <see cref="SortedList{TKey,TValue}"/> that 
    /// contains elements copied from the specified dictionary, 
    /// has the same initial capacity as the number of elements 
    /// copied, and is sorted according to the specified 
    /// generic <see cref="IComparer{T}"/> interface.
    /// </summary>
    /// <param name="dictionary">The generic <see cref="IDictionary{TKey,TValue}"/> to copy 
    /// to a new generic <see cref="SortedList{TKey,TValue}"/>.</param>
    /// <param name="comparer">The generic <see cref="IComparer{T}"/> implementation to 
    /// use when comparing keys. -or- 
    /// a null reference to use the generic <see cref="IComparer{T}"/> implementation of each key.</param>
    public XMLSerializableSortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
      : base(dictionary, comparer)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
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
    /// Generates an object from its XML representation. 
    /// </summary>
    /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from 
    /// which the object is deserialized. </param>
    public void ReadXml(System.Xml.XmlReader reader)
    {
      XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
      XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

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
        TKey key = (TKey)keySerializer.Deserialize(reader);
        reader.ReadEndElement();

        reader.ReadStartElement("value");
        TValue value = (TValue)valueSerializer.Deserialize(reader);
        reader.ReadEndElement();

        this.Add(key, value);

        reader.ReadEndElement();
        reader.MoveToContent();
      }

      reader.ReadEndElement();
    }

    /// <summary>
    /// Converts an object into its XML representation. 
    /// </summary>
    /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream 
    /// to which the object is serialized. </param>
    public void WriteXml(System.Xml.XmlWriter writer)
    {
      XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
      XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
      foreach (TKey key in this.Keys)
      {
        writer.WriteStartElement("item");
        writer.WriteStartElement("key");
        keySerializer.Serialize(writer, key);
        writer.WriteEndElement();
        writer.WriteStartElement("value");
        TValue value = this[key];
        valueSerializer.Serialize(writer, value);
        writer.WriteEndElement();
        writer.WriteEndElement();
      }
    }
    #endregion

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}