// <copyright file="AsyncHelper.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools
{
  using System;
  using System.Threading;

  /// <summary>
  /// This class provides support for fire and forget events.
  /// </summary>
  public class AsyncHelper
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
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

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
    /// Fires the delegate without any need to call EndInvoke.
    /// </summary>
    /// <param name="d">Target Delegate - must contain only one Target method</param>
    /// <param name="args">Users arguments.</param>
    public static void FireAndForget(Delegate d, params object[] args)
    {
      Target target = new Target(d, args);
      ThreadPool.QueueUserWorkItem(new
      WaitCallback(target.ExecuteDelegate));
    }

    /// <summary>
    /// Fires each of the members in the delegate asynchronously. All the members
    /// will be fired even if one of them fires an exception
    /// </summary>
    /// <param name="del">The delegate we want to fire</param>
    /// <param name="args">Each of the args we want to fire.</param>
    public static void FireAsync(Delegate del, params object[] args)
    {
      // copy the delegate to ensure that we can test for null in a thread
      // safe manner.
      Delegate temp = del;
      if (temp != null)
      {
        Delegate[] delegates = temp.GetInvocationList();
        foreach (Delegate receiver in delegates)
        {
          FireAndForget(receiver, args);
        }
      }
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
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Private class holds data for a delegate to be run on the thread pool
    /// </summary>
    private class Target
    {
      /// <summary>
      /// This is the delegate for the target method.
      /// </summary>
      private readonly Delegate TargetDelegate;

      /// <summary>
      /// This is the array of objects for the method.
      /// </summary>
      private readonly object[] Args;

      /// <summary>
      /// Initializes a new instance of the Target class.
      /// Creates a new <see cref="Target"/> instance this holds arguments and contains
      /// the method ExecuteDelegate to be called on the threadpool.
      /// </summary>
      /// <param name="d">The users delegate to fire</param>
      /// <param name="args">The users arguments to the delegate</param>
      public Target(Delegate d, object[] args)
      {
        this.TargetDelegate = d;
        this.Args = args;
      }

      /// <summary>
      /// Executes the delegate by calling DynamicInvoke.
      /// </summary>
      /// <param name="o">This parameter is required by the threadpool but is unused.</param>
      public void ExecuteDelegate(object o)
      {
        this.TargetDelegate.DynamicInvoke(this.Args);
      }
    }

    #endregion //HELPER
  }
}
