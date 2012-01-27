// <copyright file="DescriptiveResult.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics
{
  using System;

  /// <summary>
  /// The result class the holds the analysis results
  /// </summary>
  public class DescriptiveResult
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
    /// Count of items
    /// </summary>
    private uint count;

    /// <summary>
    /// Sum of items
    /// </summary>
    private double sum;

    /// <summary>
    /// Arithmetic mean
    /// </summary>
    private double mean;

    /// <summary>
    /// Geometric mean
    /// </summary>
    private double geometricMean;

    /// <summary>
    /// Harmonic mean
    /// </summary>
    private double harmonicMean;

    /// <summary>
    /// Minimum value
    /// </summary>
    private double min;

    /// <summary>
    /// Maximum value
    /// </summary>
    private double max;

    /// <summary>
    /// The range of the values
    /// </summary>
    private double range;

    /// <summary>
    /// Sample variance
    /// </summary>
    private double variance;

    /// <summary>
    /// Sample standard deviation
    /// </summary>
    private double stdDev;

    /// <summary>
    /// Skewness of the data distribution
    /// </summary>
    private double skewness;

    /// <summary>
    /// Kurtosis of the data distribution
    /// </summary>
    private double kurtosis;

    /// <summary>
    /// Interquartile range
    /// </summary>
    private double iqr;

    /// <summary>
    /// Median, or second quartile, or at 50 percentile
    /// </summary>
    private double median;

    /// <summary>
    /// First quartile, at 25 percentile
    /// </summary>
    private double firstQuartile;

    /// <summary>
    /// Third quartile, at 75 percentile
    /// </summary>
    private double thirdQuartile;

    /// <summary>
    /// sortedData is used to calculate percentiles 
    /// </summary>
    private double[] sortedData;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DescriptiveResult class.
    /// </summary>
    public DescriptiveResult()
    {
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
    /// Gets or sets the Count of items
    /// </summary>
    public uint Count
    {
      get { return this.count; }
      set { this.count = value; }
    }

    /// <summary>
    /// Gets or sets the Sum of items
    /// </summary>
    public double Sum
    {
      get { return this.sum; }
      set { this.sum = value; }
    }

    /// <summary>
    /// Gets or sets the Arithmetic mean
    /// </summary>
    public double Mean
    {
      get { return this.mean; }
      set { this.mean = value; }
    }

    /// <summary>
    /// Gets or sets the Geometric mean
    /// </summary>
    public double GeometricMean
    {
      get { return this.geometricMean; }
      set { this.geometricMean = value; }
    }

    /// <summary>
    /// Gets or sets the Harmonic mean
    /// </summary>
    public double HarmonicMean
    {
      get { return this.harmonicMean; }
      set { this.harmonicMean = value; }
    }

    /// <summary>
    /// Gets or sets the Minimum value
    /// </summary>
    public double Min
    {
      get { return this.min; }
      set { this.min = value; }
    }

    /// <summary>
    /// Gets or sets the Maximum value
    /// </summary>
    public double Max
    {
      get { return this.max; }
      set { this.max = value; }
    }

    /// <summary>
    /// Gets or sets the The range of the values
    /// </summary>
    public double Range
    {
      get { return this.range; }
      set { this.range = value; }
    }

    /// <summary>
    /// Gets or sets the Sample variance
    /// </summary>
    public double Variance
    {
      get { return this.variance; }
      set { this.variance = value; }
    }

    /// <summary>
    /// Gets or sets the Sample standard deviation
    /// </summary>
    public double StdDev
    {
      get { return this.stdDev; }
      set { this.stdDev = value; }
    }

    /// <summary>
    /// Gets or sets the Skewness of the data distribution
    /// </summary>
    public double Skewness
    {
      get { return this.skewness; }
      set { this.skewness = value; }
    }

    /// <summary>
    /// Gets or sets the Kurtosis of the data distribution
    /// </summary>
    public double Kurtosis
    {
      get { return this.kurtosis; }
      set { this.kurtosis = value; }
    }

    /// <summary>
    /// Gets or sets the Interquartile range
    /// </summary>
    public double IQR
    {
      get { return this.iqr; }
      set { this.iqr = value; }
    }

    /// <summary>
    /// Gets or sets the Median, or second quartile, or at 50 percentile
    /// </summary>
    public double Median
    {
      get { return this.median; }
      set { this.median = value; }
    }

    /// <summary>
    /// Gets or sets the First quartile, at 25 percentile
    /// </summary>
    public double FirstQuartile
    {
      get { return this.firstQuartile; }
      set { this.firstQuartile = value; }
    }

    /// <summary>
    /// Gets or sets the Third quartile, at 75 percentile
    /// </summary>
    public double ThirdQuartile
    {
      get { return this.thirdQuartile; }
      set { this.thirdQuartile = value; }
    }

    /// <summary>
    /// Gets or sets the SortedData, that is used to calculate percentiles 
    /// </summary>
    public double[] SortedData
    {
      get { return this.sortedData; }
      set { this.sortedData = value; }
    }

    /// <summary>
    /// Gets the Percentile value.
    /// </summary>
    /// <param name="percent">Pecentile, between 0 to 100</param>
    /// <returns>Percentile value</returns>
    public double Percentile(double percent)
    {
      return Descriptive.Percentile(this.sortedData, percent);
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
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
    #endregion //HELPER
  }
}