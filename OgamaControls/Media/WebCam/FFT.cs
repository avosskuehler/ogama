///////////////////////////////////////////////////////////////////////////////
//  Copyright (c) 2008 Ernest Laurentin (http://www.ernzo.com/)
//
//  This software is provided 'as-is', without any express or implied
//  warranty. In no event will the authors be held liable for any damages
//  arising from the use of this software.
//
//  Permission is granted to anyone to use this software for any purpose,
//  including commercial applications, and to alter it and redistribute it
//  freely, subject to the following restrictions:
//
//  1. The origin of this software must not be misrepresented; you must not
//  claim that you wrote the original software. If you use this software
//  in a product, an acknowledgment in the product documentation would be
//  appreciated but is not required.
//
//  2. Altered source versions must be plainly marked as such, and must not be
//  misrepresented as being the original software.
//
//  3. This notice may not be removed or altered from any source
//  distribution.
//
//  File:       FFT.cs
//  Version:    1.0
///////////////////////////////////////////////////////////////////////////////
//  Based on 1998 original version by: Don Cross <dcross@intersrv.com>
///////////////////////////////////////////////////////////////////////////////
using System;

namespace OgamaControls
{
  /// <summary>
  /// This class provides the calculation of an Fast Fourier Transform.
  /// </summary>
  public class FFT
  {
    /// <summary>
    /// The constant Pi in detail.
    /// </summary>
    public const double DDC_PI = 3.14159265358979323846;

    /// <summary>
    /// Verifies a number is a power of two
    /// </summary>
    /// <param name="x">Number to check</param>
    /// <returns>true if number is a power two (i.e.:1,2,4,8,16,...)</returns>
    public static Boolean IsPowerOfTwo(UInt32 x)
    {
      return ((x != 0) && (x & (x - 1)) == 0);
    }

    /// <summary>
    /// Get Next power of number.
    /// </summary>
    /// <param name="x">Number to check</param>
    /// <returns>A power of two number</returns>
    public static UInt32 NextPowerOfTwo(UInt32 x)
    {
      x = x - 1;
      x = x | (x >> 1);
      x = x | (x >> 2);
      x = x | (x >> 4);
      x = x | (x >> 8);
      x = x | (x >> 16);
      return x + 1;
    }

    /// <summary>
    /// Get Number of bits needed for a power of two
    /// </summary>
    /// <param name="PowerOfTwo">Power of two number</param>
    /// <returns>Number of bits</returns>
    public static UInt32 NumberOfBitsNeeded(UInt32 PowerOfTwo)
    {
      if (PowerOfTwo > 0)
      {
        for (UInt32 i = 0, mask = 1; ; i++, mask <<= 1)
        {
          if ((PowerOfTwo & mask) != 0)
            return i;
        }
      }
      return 0; // error
    }

    /// <summary>
    /// Reverse bits
    /// </summary>
    /// <param name="index">Bits</param>
    /// <param name="NumBits">Number of bits to reverse</param>
    /// <returns>Reverse Bits</returns>
    public static UInt32 ReverseBits(UInt32 index, UInt32 NumBits)
    {
      UInt32 i, rev;

      for (i = rev = 0; i < NumBits; i++)
      {
        rev = (rev << 1) | (index & 1);
        index >>= 1;
      }

      return rev;
    }

    /// <summary>
    /// Return index to frequency based on number of samples
    /// </summary>
    /// <param name="Index">sample index</param>
    /// <param name="NumSamples">number of samples</param>
    /// <returns>Frequency index range</returns>
    public static double IndexToFrequency(UInt32 Index, UInt32 NumSamples)
    {
      if (Index >= NumSamples)
        return 0.0;
      else if (Index <= NumSamples / 2)
        return (double)Index / (double)NumSamples;

      return -(double)(NumSamples - Index) / (double)NumSamples;
    }

    /// <summary>
    /// Compute FFT
    /// </summary>
    /// <param name="NumSamples">NumSamples Number of samples (must be power two)</param>
    /// <param name="pRealIn">Real samples</param>
    /// <param name="pImagIn">Imaginary (optional, may be null)</param>
    /// <param name="pRealOut">Real coefficient output</param>
    /// <param name="pImagOut">Imaginary coefficient output</param>
    /// <param name="bInverseTransform">bInverseTransform when true, compute Inverse FFT</param>
    public static void Compute(UInt32 NumSamples, double[] pRealIn, double[] pImagIn,
                        double[] pRealOut, double[] pImagOut, Boolean bInverseTransform)
    {
      UInt32 NumBits;    /* Number of bits needed to store indices */
      UInt32 i, j, k, n;
      UInt32 BlockSize, BlockEnd;

      double angle_numerator = 2.0 * DDC_PI;
      double tr, ti;     /* temp real, temp imaginary */

      if (pRealIn == null || pRealOut == null || pImagOut == null)
      {
        // error
        throw new ArgumentNullException("Null argument");
      }
      if (!IsPowerOfTwo(NumSamples))
      {
        // error
        throw new ArgumentException("Number of samples must be power of 2");
      }
      if (pRealIn.Length < NumSamples || (pImagIn != null && pImagIn.Length < NumSamples) ||
           pRealOut.Length < NumSamples || pImagOut.Length < NumSamples)
      {
        // error
        throw new ArgumentException("Invalid Array argument detected");
      }

      if (bInverseTransform)
        angle_numerator = -angle_numerator;

      NumBits = NumberOfBitsNeeded(NumSamples);

      /*
      **   Do simultaneous data copy and bit-reversal ordering into outputs...
      */
      for (i = 0; i < NumSamples; i++)
      {
        j = ReverseBits(i, NumBits);
        pRealOut[j] = pRealIn[i];
        pImagOut[j] = (double)((pImagIn == null) ? 0.0 : pImagIn[i]);
      }

      /*
      **   Do the FFT itself...
      */
      BlockEnd = 1;
      for (BlockSize = 2; BlockSize <= NumSamples; BlockSize <<= 1)
      {
        double delta_angle = angle_numerator / (double)BlockSize;
        double sm2 = Math.Sin(-2 * delta_angle);
        double sm1 = Math.Sin(-delta_angle);
        double cm2 = Math.Cos(-2 * delta_angle);
        double cm1 = Math.Cos(-delta_angle);
        double w = 2 * cm1;
        double ar0, ar1, ar2;
        double ai0, ai1, ai2;

        for (i = 0; i < NumSamples; i += BlockSize)
        {
          ar2 = cm2;
          ar1 = cm1;

          ai2 = sm2;
          ai1 = sm1;

          for (j = i, n = 0; n < BlockEnd; j++, n++)
          {
            ar0 = w * ar1 - ar2;
            ar2 = ar1;
            ar1 = ar0;

            ai0 = w * ai1 - ai2;
            ai2 = ai1;
            ai1 = ai0;

            k = j + BlockEnd;
            tr = ar0 * pRealOut[k] - ai0 * pImagOut[k];
            ti = ar0 * pImagOut[k] + ai0 * pRealOut[k];

            pRealOut[k] = (pRealOut[j] - tr);
            pImagOut[k] = (pImagOut[j] - ti);

            pRealOut[j] += (tr);
            pImagOut[j] += (ti);
          }
        }

        BlockEnd = BlockSize;
      }

      /*
      **   Need to normalize if inverse transform...
      */
      if (bInverseTransform)
      {
        double denom = (double)(NumSamples);

        for (i = 0; i < NumSamples; i++)
        {
          pRealOut[i] /= denom;
          pImagOut[i] /= denom;
        }
      }
    }

    /// <summary>
    /// Calculate normal (power spectrum)
    /// </summary>
    /// <param name="NumSamples">Number of sample</param>
    /// <param name="pReal">Real coefficient buffer</param>
    /// <param name="pImag">Imaginary coefficient buffer</param>
    /// <param name="pAmpl">Working buffer to hold amplitude Xps(m) = | X(m)^2 | = Xreal(m)^2  + Ximag(m)^2</param>
    public static void Norm(UInt32 NumSamples, double[] pReal, double[] pImag, double[] pAmpl)
    {
      if (pReal == null || pImag == null || pAmpl == null)
      {
        // error
        throw new ArgumentNullException("pReal,pImag,pAmpl");
      }
      if (pReal.Length < NumSamples || pImag.Length < NumSamples || pAmpl.Length < NumSamples)
      {
        // error
        throw new ArgumentException("Invalid Array argument detected");
      }

      // Calculate amplitude values in the buffer provided
      for (UInt32 i = 0; i < NumSamples; i++)
      {
        pAmpl[i] = pReal[i] * pReal[i] + pImag[i] * pImag[i];
      }
    }

    /// <summary>
    /// Find Peak frequency in Hz
    /// </summary>
    /// <param name="NumSamples">Number of samples</param>
    /// <param name="pAmpl">Current amplitude</param>
    /// <param name="samplingRate">Sampling rate in samples/second (Hz)</param>
    /// <param name="index">Frequency index</param>
    /// <returns>Peak frequency in Hz</returns>
    public static double PeakFrequency(UInt32 NumSamples, double[] pAmpl, double samplingRate, ref UInt32 index)
    {
      UInt32 N = NumSamples >> 1;   // number of positive frequencies. (numSamples/2)

      if (pAmpl == null)
      {
        // error
        throw new ArgumentNullException("pAmpl");
      }
      if (pAmpl.Length < NumSamples)
      {
        // error
        throw new ArgumentException("Invalid Array argument detected");
      }

      double maxAmpl = -1.0;
      double peakFreq = -1.0;
      index = 0;

      for (UInt32 i = 0; i < N; i++)
      {
        if (pAmpl[i] > maxAmpl)
        {
          maxAmpl = (double)pAmpl[i];
          index = i;
          peakFreq = (double)(i);
        }
      }

      return samplingRate * peakFreq / (double)(NumSamples);
    }
  }
}
