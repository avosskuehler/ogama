// <copyright file="XMLSerializableDictionary.cs" company="FU Berlin">
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
  using System.Collections.Generic;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  /// This class extends the <see cref="Dictionary{TKey,TValue}"/> class with
  /// the possibility to XML Serialize with <see cref="XmlSerializer"/>.
  /// That is done implementing the <see cref="IXmlSerializable"/> interface.
  /// </summary>
  /// <remarks>We have to create all the constructors of the base class,
  /// because they are not inherited.</remarks>
  /// <typeparam name="TKey">The key of the underlying dictionary.</typeparam>
  /// <typeparam name="TValue">The value of the underlying dictionary.</typeparam>
  [Serializable]
  public class XMLSerializableDictionary<TKey, TValue>
      : Dictionary<TKey, TValue>, IXmlSerializable, ICloneable
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
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> that is empty, 
    /// has the default initial capacity, and uses the 
    /// default equality comparer for the key type. 
    /// </summary>
    public XMLSerializableDictionary()
      : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class 
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> 
    /// that contains elements copied from the specified 
    /// <see cref="IDictionary{TKey,TValue}"/> and uses the default equality comparer for the key type. 
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are 
    /// copied to the new <see cref="XMLSerializableDictionary{TKey,TValue}"/>.</param>
    public XMLSerializableDictionary(IDictionary<TKey, TValue> dictionary)
      : base(dictionary)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> that 
    /// is empty, has the default initial capacity, and uses 
    /// the specified <see cref="IEqualityComparer{TKey}"/>. 
    /// </summary>
    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation 
    /// to use when comparing keys, or a null reference 
    /// to use the default EqualityComparer for the type of the key.</param>
    public XMLSerializableDictionary(IEqualityComparer<TKey> comparer)
      : base(comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> that is empty, 
    /// has the specified initial capacity, and uses 
    /// the default equality comparer for the key type.
    /// </summary>
    /// <param name="capacity">The initial number of elements 
    /// that the <see cref="XMLSerializableDictionary{TKey,TValue}"/> can contain.</param>
    public XMLSerializableDictionary(int capacity)
      : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/>
    /// that contains elements copied from the specified 
    /// <see cref="IDictionary{TKey,TValue}"/> and uses the specified IEqualityComparer.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are 
    /// copied to the new <see cref="XMLSerializableDictionary{TKey,TValue}"/>.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation 
    /// to use when comparing keys, or a null reference 
    /// to use the default EqualityComparer for the type of the key.</param>
    public XMLSerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
      : base(dictionary, comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> 
    /// that is empty, has the specified initial capacity, 
    /// and uses the specified IEqualityComparer. 
    /// </summary>
    /// <param name="capacity">The initial number of elements
    /// that the <see cref="XMLSerializableDictionary{TKey,TValue}"/> can contain.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation
    /// to use when comparing keys, or a null reference 
    /// to use the default EqualityComparer for the type of the key.</param>
    public XMLSerializableDictionary(int capacity, IEqualityComparer<TKey> comparer)
      : base(capacity, comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class 
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> 
    /// as an exact copy of the given object.
    /// </summary>
    /// <param name="dictionaryToClone">The <see cref="XMLSerializableDictionary{TKey,TValue}"/> whose elements are 
    /// copied to the new <see cref="XMLSerializableDictionary{TKey,TValue}"/>.</param>
    public XMLSerializableDictionary(XMLSerializableDictionary<TKey, TValue> dictionaryToClone)
      : base(dictionaryToClone)
    {
    }

    /// <summary>
    /// Initializes a new instance of the XMLSerializableDictionary class
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> with serialized data. 
    /// </summary>
    /// <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"/> 
    /// object containing the information required to serialize the 
    /// <see cref="XMLSerializableDictionary{TKey,TValue}"/> .</param>
    /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"/> 
    /// structure containing the source and destination of the serialized 
    /// stream associated with the <see cref="XMLSerializableDictionary{TKey,TValue}"/> .</param>
    protected XMLSerializableDictionary(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Implements ICloneable by copying all the contents.
    /// </summary>
    /// <returns>An <see cref="Object"/> with a copy of the current <see cref="XMLSerializableDictionary{TKey,TValue}"/></returns>
    public object Clone()
    {
      return new XMLSerializableDictionary<TKey, TValue>(this);
    }

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
    /// Generates an <see cref="Dictionary{TKey,TValue}"/>  from its XML representation. 
    /// </summary>
    /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from 
    /// which the dictionary is deserialized. </param>
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
    /// Converts this <see cref="Dictionary{TKey,TValue}"/>  into its XML representation. 
    /// </summary>
    /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream 
    /// to which the <see cref="Dictionary{TKey,TValue}"/>  is serialized. </param>
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
