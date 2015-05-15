// <copyright file="Descriptive.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics
{
  using System;

  /// <summary>
  /// Class to calculate descriptive statistics on an array of double values,
  /// including mean, median, sum, quartiles etc.
  /// Should be used:
  ///    1. Instantiate a Descriptive object
  ///    2. Invoke its .Analyze() method
  ///    3. Retrieve results from its .Result object
  /// </summary>
  /// <remarks>This class is from http://www.codeproject.com/KB/recipes/DescriptiveStatisticClass.aspx
  /// by Jan Low. Thanks Jan!</remarks>
  public class Descriptive
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
    /// An array with the data to analyse
    /// </summary>
    private double[] data;

    /// <summary>
    /// An array with the data to analyse, but sorted by value
    /// </summary>
    private double[] sortedData;

    /// <summary>
    /// Descriptive results
    /// </summary>
    private DescriptiveResult result;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Descriptive class.
    /// </summary>
    public Descriptive() 
    {
      this.result = new DescriptiveResult();
    } 

    /// <summary>
    /// Initializes a new instance of the Descriptive class.
    /// </summary>
    /// <param name="dataVariable">Data array</param>
    public Descriptive(double[] dataVariable)
    {
      this.result = new DescriptiveResult();
      this.data = dataVariable;
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
    /// Gets the descriptive results
    /// </summary>
    public DescriptiveResult Result
    {
      get { return this.result; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Run the analysis to obtain descriptive information of the data
    /// </summary>
    public void Analyze()
    {
      // initializations
      this.result.Count = 0;
      this.result.Min = this.result.Max = this.result.Range = this.result.Mean =
      this.result.Sum = this.result.StdDev = this.result.Variance = 0.0d;

      ////double sumOfSquare = 0.0d;
      ////double sumOfESquare = 0.0d; // must initialize

      ////double[] squares = new double[data.Length];
      ////double cumProduct = 1.0d; // to calculate geometric mean
      ////double cumReciprocal = 0.0d; // to calculate harmonic mean

      // First iteration
      for (int i = 0; i < this.data.Length; i++)
      {
        if (i == 0) // first data point
        {
          this.result.Min = this.data[i];
          this.result.Max = this.data[i];
          this.result.Mean = this.data[i];
          this.result.Range = 0.0d;
        }
        else
        { // not the first data point
          if (this.data[i] < this.result.Min)
          {
            this.result.Min = this.data[i];
          }

          if (this.data[i] > this.result.Max)
          {
            this.result.Max = this.data[i];
          }
        }

        this.result.Sum += this.data[i];
        ////squares[i] = Math.Pow(data[i], 2); //TODO: may not be necessary
        ////sumOfSquare += squares[i];

        ////cumProduct *= data[i];
        ////cumReciprocal += 1.0d / data[i];
      }

      this.result.Count = (uint)this.data.Length;
      double n = (double)this.result.Count; // use a shorter variable in double type
      this.result.Mean = this.result.Sum / n;
      ////Result.GeometricMean = Math.Pow(cumProduct, 1.0 / n);
      ////Result.HarmonicMean = 1.0d / (cumReciprocal / n); // see http://mathworld.wolfram.com/HarmonicMean.html
      ////Result.Range = Result.Max - Result.Min;

      ////// second loop, calculate Stdev, sum of errors
      //////double[] eSquares = new double[data.Length];
      ////double m1 = 0.0d;
      ////double m2 = 0.0d;
      ////double m3 = 0.0d; // for skewness calculation
      ////double m4 = 0.0d; // for kurtosis calculation
      ////// for skewness
      ////for (int i = 0; i < data.Length; i++)
      ////{
      ////  double m = data[i] - Result.Mean;
      ////  double mPow2 = m * m;
      ////  double mPow3 = mPow2 * m;
      ////  double mPow4 = mPow3 * m;

      ////  m1 += Math.Abs(m);

      ////  m2 += mPow2;

      ////  // calculate skewness
      ////  m3 += mPow3;

      ////  // calculate skewness
      ////  m4 += mPow4;

      ////}

      ////Result.SumOfError = m1;
      ////Result.SumOfErrorSquare = m2; // Added for Excel function DEVSQ
      ////sumOfESquare = m2;

      ////// var and standard deviation
      ////Result.Variance = sumOfESquare / ((double)Result.Count - 1);
      ////Result.StdDev = Math.Sqrt(Result.Variance);

      ////// using Excel approach
      ////double skewCum = 0.0d; // the cum part of SKEW formula
      ////for (int i = 0; i < data.Length; i++)
      ////{
      ////  skewCum += Math.Pow((data[i] - Result.Mean) / Result.StdDev, 3);
      ////}
      ////Result.Skewness = n / (n - 1) / (n - 2) * skewCum;

      ////// kurtosis: see http://en.wikipedia.org/wiki/Kurtosis (heading: Sample Kurtosis)
      ////double m2_2 = Math.Pow(sumOfESquare, 2);
      ////Result.Kurtosis = ((n + 1) * n * (n - 1)) / ((n - 2) * (n - 3)) *
      ////    (m4 / m2_2) -
      ////    3 * Math.Pow(n - 1, 2) / ((n - 2) * (n - 3)); // second last formula for G2

      // calculate quartiles
      this.sortedData = new double[this.data.Length];
      this.data.CopyTo(this.sortedData, 0);
      Array.Sort(this.sortedData);

      // copy the sorted data to result object so that
      // user can calculate percentile easily
      this.result.SortedData = new double[this.data.Length];
      this.sortedData.CopyTo(this.result.SortedData, 0);

      ////Result.FirstQuartile = percentile(sortedData, 25);
      ////Result.ThirdQuartile = percentile(sortedData, 75);
      this.result.Median = Percentile(this.sortedData, 50);
      ////Result.IQR = percentile(sortedData, 75) -
      ////    percentile(sortedData, 25);
    } // end of method Analyze

    /// <summary>
    /// Calculate percentile of a sorted data set
    /// </summary>
    /// <param name="sortedData">The array with the sorted data</param>
    /// <param name="p">The percentile value</param>
    /// <returns>The percentile of a sorted data set at the given percentile.</returns>
    public static double Percentile(double[] sortedData, double p)
    {
      if (sortedData.Length == 0)
      {
        return -1;
      }
      else if (sortedData.Length == 1)
      {
        return sortedData[0];
      }

      // algo derived from Aczel pg 15 bottom
      if (p >= 100.0d)
      {
        return sortedData[sortedData.Length - 1];
      }

      double position = (double)(sortedData.Length + 1) * p / 100.0;
      double leftNumber = 0.0d, rightNumber = 0.0d;

      double n = p / 100.0d * (sortedData.Length - 1) + 1.0d;

      if (position >= 1)
      {
        leftNumber = sortedData[(int)System.Math.Floor(n) - 1];
        rightNumber = sortedData[(int)System.Math.Floor(n)];
      }
      else
      {
        leftNumber = sortedData[0]; // first data
        rightNumber = sortedData[1]; // first data
      }

      if (leftNumber == rightNumber)
      {
        return leftNumber;
      }
      else
      {
        double part = n - System.Math.Floor(n);
        return leftNumber + part * (rightNumber - leftNumber);
      }
    } // end of internal function percentile

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
  } // end of class Descriptive
}