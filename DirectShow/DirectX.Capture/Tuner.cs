// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2003-Jan-24		BL		- created
//
// Copyright (c) 2003 Brian Low
//
//  2003-Feb-25 fdaupias: functions using IAMTVTuner interface
//  2003-Mar-4	dauboro: functions added for using e.g. FM Radio
//  2006-Mar-1  H.Vosman: Wrapped everything together
//
//  2007-Jan-10    HV      - added modifications
// - Added DSHOWNET conditional for using the older DShowNET library
//   instead of the DirectShowLib library
// - Added TVFineTune.cs support
//
// Copyright (C) 2007 Hans Vosman
// ------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
#if DSHOWNET
using DShowNET;
#else
using DirectShowLib;
#endif

namespace DirectX.Capture
{

#if DSHOWNET
// Enumeration already in DShowNET, remove delaration
#else
  ///// <summary>
  /////  Specify the frequency of the TV tuner.
  ///// </summary>
  //public enum TunerInputType
  //{
  //  /// <summary> Cable frequency </summary>
  //  Cable,
  //  /// <summary> Antenna frequency </summary>
  //  Antenna
  //}
#endif


	/// <summary>
	///  Control and query a hardware TV Tuner.
	/// </summary>
	public class Tuner : IDisposable
	{
		// ---------------- Private Properties ---------------

		/// <summary>
		/// Access to IAMTVTuner interface functions
		/// </summary>
		protected IAMTVTuner tvTuner = null;		



		// ------------------- Constructors ------------------

		/// <summary> Initialize this object with a DirectShow tuner </summary>
		public Tuner(IAMTVTuner tuner)
		{
			tvTuner = tuner;
		}

		/// <summary>
		/// Added for TVFineTune.cs
		/// </summary>
		internal Tuner()
		{
		}

		// ---------------- Public Properties ---------------
		
		/// <summary>
		///  Get or set the TV Tuner channel.
		/// </summary>
		public int Channel
		{
			get
			{
				int channel;
#if DSHOWNET
				int v, a;
#else
                AMTunerSubChannel v, a;
#endif
                int hr = tvTuner.get_Channel(out channel, out v, out a);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( channel );
			}

			set
			{
				int hr = tvTuner.put_Channel( value, AMTunerSubChannel.Default, AMTunerSubChannel.Default );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Get or set the tuner frequency (cable or antenna).
		/// </summary>
		public TunerInputType InputType
		{
			get
			{
#if DSHOWNET
				DShowNET.TunerInputType t;
#else
                TunerInputType t;
#endif
				int hr = tvTuner.get_InputType( 0, out t );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( (TunerInputType) t );
			}
			set
			{
#if DSHOWNET
				DShowNET.TunerInputType t = (DShowNET.TunerInputType) value;
#else
                TunerInputType t = (TunerInputType)value;
#endif
				int hr = tvTuner.put_InputType( 0, t );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Indicates whether a signal is present on the current channel.
		///  If the signal strength cannot be determined, a NotSupportedException
		///  is thrown.
		/// </summary>
		public bool SignalPresent
		{
			get
			{
				AMTunerSignalStrength sig;
				int hr = tvTuner.SignalPresent( out sig );
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
#if DSHOWNET
				if ( sig == AMTunerSignalStrength.NA )
#else
                if (sig == AMTunerSignalStrength.HasNoSignalStrength)
#endif
                {
                    throw new NotSupportedException("Signal strength not available.");
                }
				return( sig == AMTunerSignalStrength.SignalPresent );
			}
		}

		// ---------------- Public Methods ---------------

		public void Dispose()
		{
			if ( tvTuner != null )
				Marshal.ReleaseComObject( tvTuner ); tvTuner = null;
		}

		// Start of code based on code written by fdaupias, february 25, 2003
		// http://www.codeproject.com/cs/media/DirectXCapture.asp?msg=427626#xx427626xx

		// get minimum and maximum channels
		public int[] ChanelMinMax
		{
			get
			{
				int min;
				int max;
				int hr = tvTuner.ChannelMinMax(out min, out max);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				int[] myArray = new int[] {min,max};
				return myArray;
			}
		}

		// useful for checking purposes
		public int GetVideoFrequency
		{
			get
			{
				int theFreq;
				int hr = tvTuner.get_VideoFrequency(out theFreq);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return theFreq;
			}
		}

		// not that useful, but...
		public int GetAudioFrequency
		{
			get
			{
				int theFreq;
				int hr = tvTuner.get_AudioFrequency (out theFreq);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return theFreq;
			}
		}

		//set this to your country code. Frequency Overrides should be set to this code
		public int TuningSpace
		{
			get
			{
				int tspace;
				int hr = tvTuner.get_TuningSpace(out tspace);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return tspace;
			}
			set
			{
				int tspace = value;
				int hr = tvTuner.put_TuningSpace(tspace);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		//Frequency Overrides are stored in the registry, in a key labeled TS[CountryCode]-[TunerInputType ], so for cable tv in Portugal it would be TS351-1
		public bool SetFrequencyOverride(int channel, int Frequency, int TuningSpace, TunerInputType InputType)
		{
			try
			{
				int IType;
				if (InputType == TunerInputType.Cable )
				{
					IType=1;
				}
				else
				{
					IType=0;
				}
				Microsoft.Win32.RegistryKey LocaleOverride;
				LocaleOverride = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\TV System Services\\TVAutoTune\\TS" + TuningSpace.ToString() + "-" + IType.ToString(), true);
				if (LocaleOverride == null)
				{
					LocaleOverride = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\TV System Services\\TVAutoTune\\TS" + TuningSpace.ToString() + "-" + IType.ToString());
				}
				LocaleOverride.SetValue(channel.ToString(), Frequency);
				return true;
			}
			catch
			{
				return false;
			}
		}
		// End of code based on code written by fdaupias

		// Start of code based on code written by dauboro, march 4, 2003
		// http://www.codeproject.com/cs/media/DirectXCapture.asp?msg=435837#xx435837xx

		/// <summary>
		/// Audio mode structure
		/// </summary>
		public struct AvAudioModes
		{
			/// <summary> Default audio mode available flag </summary>
			public bool Default;
			/// <summary> TV audio mode available flag </summary>
			public bool TV;
			/// <summary> FMRadio audio mode available flag </summary>
			public bool FMRadio;
			/// <summary> AMRadio audio mode available flag </summary>
			public bool AMRadio;
			/// <summary> Dss audio mode available flag </summary>
			public bool Dss; 

			/// <summary> Scan audio modes and set appropriate flags </summary>
			public AvAudioModes(bool Default, bool TV, bool FMRadio, bool AMRadio, bool Dss)
			{
				this.Default = Default;
				this.TV = TV;
				this.FMRadio = FMRadio;
				this.AMRadio = AMRadio;
				this.Dss = Dss;
			}
		}

		/// 
		/// Retrieves or sets the current mode on a multifunction tuner.
		/// 
		public AMTunerModeType AudioMode
		{
			get
			{
#if DSHOWNET
				DShowNET.AMTunerModeType AudioMode;
#else
                AMTunerModeType AudioMode;
#endif
                int hr = tvTuner.get_Mode(out AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( (AMTunerModeType) AudioMode );
			}
			set
			{
#if DSHOWNET
				DShowNET.AMTunerModeType AudioMode = (DShowNET.AMTunerModeType) value;
#else
                AMTunerModeType AudioMode = value;
#endif
                int hr = tvTuner.put_Mode(AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}


		/// 
		/// Retrieves the tuner's supported modes.
		/// 
		public AvAudioModes AvailableAudioModes
		{
			get
			{
#if DSHOWNET
				DShowNET.AMTunerModeType AudioMode;
#else
                AMTunerModeType AudioMode;
#endif
                int hr = tvTuner.GetAvailableModes(out AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				AvAudioModes AvModes;

				if ((int)AudioMode == (int)AMTunerModeType.TV)
				{
					AvModes = new AvAudioModes(true,true,false,false,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio)
				{
					AvModes = new AvAudioModes(true,true,false,true,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.FMRadio)
				{
					AvModes = new AvAudioModes(true,true,true,false,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,false,false,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.FMRadio)
				{
					AvModes = new AvAudioModes(true,true,true,true,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,false,true,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.FMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,true,false,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.FMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,true,true,true);
				}
				else
				{
					AvModes = new AvAudioModes(false,false,false,false,false);
				}

				return( AvModes );
			}
		}
		// End of code based on code written by dauboro
	}
}
