// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2007-Jan-10	HV		- created
//
//  2007-July-01 HV     - added modifications
// - Added DSHOWNET conditional for using the older DShowNET library
//   instead of the DirectShowLib library
// - Added IAMTVAudio interface for DSHOWNET
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
	/// <summary>
	/// Summary description for FineTune.
	/// 
	/// This file adds fine tuning functionality to the Tuner class.
	/// Upon creation of the class the TV tuner will be initialized.
	/// </summary>
	public class TVFineTune: Tuner, IDisposable
	{
		internal TVFineTune(IAMTVTuner tuner)
		{
			//
			// TODO: Add constructor logic here
			//
			tvTuner = tuner;

			// Initialize the TV tuner
#if DSHOWNET
			this.AudioMode = DShowNET.AMTunerModeType.TV;
			this.InputType = DShowNET.TunerInputType.Cable;
#else
			this.AudioMode = AMTunerModeType.TV;
			this.InputType = TunerInputType.Cable;
#endif

			this.TuningSpace = 31; // Netherlands

			// Default choice used for selecting my favorit channel (at 567MHz,
			// this frequency maps to Windows channel number 212. The Windows
			// default choice is usually channel 4 (48Mhz)
			this.Channel = 212;

			// Minimum and maximum frequencies of TV tuner. These values are country
			// and TV tuner dependent! Real frequencies can be found via checking the
			// frequencies corresponding with the minimum and maximum channel numbers.
			// But becareful, some TV tuners return incorrect values.
			// For US the maximum frequency is usually 801MHz, for European countries
			// the maximum frequency is usually 863MHz.
			minFrequency = 45250000;
			maxFrequency = 863250000;
		}

		private static readonly Guid PROPSETID_TUNER = new Guid(0x6a2e0605, 0x28e4, 0x11d0, 0xa1, 0x8c, 0x00, 0xa0, 0xc9, 0x11, 0x89, 0x56);

		/// <summary>
		/// KSPROPERTY with Guid and additional data
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
			public struct KSPROPERTY
		{
			// size Guid is long + 2 short + 8 byte = 4 longs
			Guid   Set;
			[MarshalAs(UnmanagedType.U4)]
			int  Id;
			[MarshalAs(UnmanagedType.U4)]
			int  Flags;
		}
		// KSIDENTIFIER, *PKSIDENTIFIER;

		/// <summary>
		/// Tuner property values specifying the property that can be read
		/// (or written)
		/// </summary>
		public enum KSPROPERTY_TUNER
		{
			/// <summary> R  -overall device capabilities </summary>
			TUNER_CAPS,			
			/// <summary> R  -capabilities in this mode </summary>
			TUNER_MODE_CAPS,    
			/// <summary> RW -set a mode (TV, FM, AM, DSS) </summary>
			TUNER_MODE,         
			/// <summary> R  -get TV standard (only if TV mode) </summary>
			TUNER_STANDARD,     
			/// <summary> RW -set/get frequency </summary>
			TUNER_FREQUENCY,    
			/// <summary> RW -select an input </summary>
			TUNER_INPUT,        
			/// <summary> R  -tuning status </summary>
			TUNER_STATUS,       
			/// <summary> R O-Medium for IF or Transport Pin </summary>
			TUNER_IF_MEDIUM     
		}

		// Describes how the device tunes.  Only one of these flags may be set
		// in KSPROPERTY_TUNER_MODE_CAPS_S.Strategy

		/// <summary>
		/// Describe how the driver should attempt to tune:
		/// EXACT:   just go to the frequency specified (no fine tuning)
		/// FINE:    (slow) do an exhaustive search for the best signal
		/// COARSE:  (fast) use larger frequency jumps to just determine if any signal
		/// </summary>
		public enum KS_TUNER_TUNING_FLAGS
		{
			/// <summary> No fine tuning </summary>
			TUNING_EXACT = 1,
			/// <summary> Fine grained search </summary>
			TUNING_FINE,
			/// <summary> Coarse search </summary>
			TUNING_COARSE,
		}

		/// <summary>
		/// KSPROPERTY tuner frequency data structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
			public struct KSPROPERTY_TUNERFREQUENCY
		{
			/// <summary> Hz </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  Frequency;				
			/// <summary> Hz (last known good) </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  LastFrequency;          
			/// <summary> KS_TUNER_TUNING_FLAGS </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  TuningFlags;            
			/// <summary> DSS </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  VideoSubChannel;        
			/// <summary> DSS </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  AudioSubChannel;
			/// <summary> Channel number </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  Channel;                
			/// <summary> Country number </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  Country;                
			/// <summary> Undocumented or error ... </summary>
			[MarshalAs(UnmanagedType.U4)]
			public int  Dummy;                  
			// Dummy added to get a succesful return of the Get, Set function
		}

		/// <summary>
		/// KSPROPERTY tuner frequency structure including the tuner frequency
		/// data structure.
		/// Size is 6 + 7 (+ 1 dummy) ints
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
			public struct KSPROPERTY_TUNER_FREQUENCY_S
		{
			/// <summary> Property Guid </summary>
			public KSPROPERTY Property;			
			/// <summary> Tuner frequency data structure </summary>
			public KSPROPERTY_TUNERFREQUENCY Instance;	
		}
		// KSPROPERTY_TUNER_FREQUENCY_S, *PKSPROPERTY_TUNER_FREQUENCY_S;

#if DSHOWNET
		/// <summary>
		/// From KSPROPERTY_SUPPORT_* defines
		/// </summary>
		[Flags]
			private enum KSPropertySupport
		{
			Get = 1,
			Set = 2
		}

		[Guid("31EFAC30-515C-11d0-A9AA-00AA0061BE93"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			private interface IKsPropertySet
		{
			[PreserveSig]
			int Set(
				[In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
				[In] int dwPropID,
				[In] IntPtr pInstanceData,
				[In] int cbInstanceData,
				[In] IntPtr pPropData,
				[In] int cbPropData
				);

			[PreserveSig]
			int Get(
				[In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
				[In] int dwPropID,
				[In] IntPtr pInstanceData,
				[In] int cbInstanceData,
				[In, Out] IntPtr pPropData,
				[In] int cbPropData,
				[Out] out int pcbReturned
				);

			[PreserveSig]
			int QuerySupported(
				[In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
				[In] int dwPropID,
				[Out] out KSPropertySupport pTypeSupport
				);
		}
#endif // DSHOWNET

		/// <summary>
		/// DShowErr enumerations
		/// </summary>
		public enum DshowError : long
		{
			// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/directshow/htm/errorandsuccesscodes.asp
			// HRESULT Values Specific to DirectShow
			/// <summary> </summary>
			VFW_NO_ERROR = 0L,
			/// <summary> </summary>
			VFW_E_NO_INTERFACE = 0x80040215,
		}

		/// <summary>
		/// Set broadcast frequency using the IKsProperySet interface.
		/// </summary>
		/// <param name="Freq"></param>
		/// <returns></returns>
		public int SetFrequency(int Freq)
		{ 
			int hr;
			IKsPropertySet pKs = tvTuner as IKsPropertySet;
#if DSHOWNET
			KSPropertySupport dwSupported = new KSPropertySupport();
#else
			DirectShowLib.KSPropertySupport dwSupported = new KSPropertySupport();
#endif
			DshowError errorCode = DshowError.VFW_NO_ERROR;
		
			// Use IKsProperySet interface (interface for Vfw like property
			// window) for getting/setting tuner specific information.
			// Check first if the Property is supported.
			if(pKs == null)
			{
				errorCode = DshowError.VFW_E_NO_INTERFACE;
				return (int)errorCode;
			}

			// Use IKsProperySet interface (interface for Vfw like propery
			// window) for getting and setting tuner specific information
			// like the real broadcast frequency.
			hr = pKs.QuerySupported(
				PROPSETID_TUNER, 
				(int)KSPROPERTY_TUNER.TUNER_FREQUENCY,
				out dwSupported);
			if(hr == 0)
			{
#if DSHOWNET
				if( ((dwSupported & KSPropertySupport.Get)== KSPropertySupport.Get)&&
					((dwSupported & KSPropertySupport.Set)== KSPropertySupport.Set)&
#else
				if( ((dwSupported & DirectShowLib.KSPropertySupport.Get)== DirectShowLib.KSPropertySupport.Get)&&
					((dwSupported & DirectShowLib.KSPropertySupport.Set)== DirectShowLib.KSPropertySupport.Set)&
#endif
					(Freq >= this.minFrequency && Freq <= this.maxFrequency) )
				{
					// Create and prepare data structures
					KSPROPERTY_TUNER_FREQUENCY_S Frequency = new KSPROPERTY_TUNER_FREQUENCY_S();
					IntPtr freqData = Marshal.AllocCoTaskMem(Marshal.SizeOf(Frequency));
					IntPtr instData = Marshal.AllocCoTaskMem(Marshal.SizeOf(Frequency.Instance));
					int cbBytes = 0;

					// Convert the data
					Marshal.StructureToPtr(Frequency, freqData, true);
					Marshal.StructureToPtr(Frequency.Instance, instData, true);

					hr = pKs.Get(
						PROPSETID_TUNER,
						(int)KSPROPERTY_TUNER.TUNER_FREQUENCY,
						instData,
						Marshal.SizeOf(Frequency.Instance),
						freqData,
						Marshal.SizeOf(Frequency),
						out cbBytes);
					if(hr == 0)
					{
						// Specify the TV broadcast frequency and tuning flag
						Frequency.Instance.Frequency = Freq;
						Frequency.Instance.TuningFlags = (int)KS_TUNER_TUNING_FLAGS.TUNING_EXACT;

						// Convert the data
						Marshal.StructureToPtr(Frequency, freqData, true);
						Marshal.StructureToPtr(Frequency.Instance, instData, true);

						// Now change the broadcast frequency
						hr = pKs.Set(
							PROPSETID_TUNER,
							(int)KSPROPERTY_TUNER.TUNER_FREQUENCY,
							instData,
							Marshal.SizeOf(Frequency.Instance),
							freqData,
							Marshal.SizeOf(Frequency));
						if(hr < 0)
						{
							errorCode = (DshowError)hr;
						}
					} 
					else
					{
						errorCode = (DshowError)hr;
					}

					if(freqData != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(freqData);
					}
					if(instData != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(instData);
					}
				}
			} 
			else
			{	// QuerySupported
				errorCode = (DshowError)hr;
			}

			return (int)errorCode;
		}

		private int minFrequency;
		private int maxFrequency;

		/// <summary>
		/// Maximum TV tuning frequency
		/// </summary>
		public int MaxFrequency
		{
			get
			{
				return maxFrequency;
			}
		}

		/// <summary>
		/// Minimum TV tuning frequency
		/// </summary>
		public int MinFrequency
		{
			get
			{
				return minFrequency;
			}
		}

//#if NEWCODE
		/// <summary>
		/// IAMTVAudio property
		/// </summary>
		protected IAMTVAudio tvAudio = null;

    public IAMTVAudio TvAudio
    {
      set { this.tvAudio = value; }
    }
#if DSHOWNET
		/// <summary>
		/// From AMTVAudioEventType
		/// </summary>
		[Flags]
			public enum AMTVAudioEventType
		{
			Changed = 0x0001
		}

		/// <summary>
		/// IAMTVAudio interface
		/// </summary>
		[Guid("83EC1C30-23D1-11d1-99E6-00A0C9560266"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			public interface IAMTVAudio
		{
			[PreserveSig]
			int GetHardwareSupportedTVAudioModes([Out] out TVAudioMode plModes);

			[PreserveSig]
			int GetAvailableTVAudioModes([Out] out TVAudioMode plModes);

			[PreserveSig]
			int get_TVAudioMode([Out] out TVAudioMode plMode);

			[PreserveSig]
			int put_TVAudioMode([In] TVAudioMode lMode);

			[PreserveSig]
			int RegisterNotificationCallBack(
				[In] IAMTunerNotification pNotify,
				[In] AMTVAudioEventType lEvents
				);

			[PreserveSig]
			int UnRegisterNotificationCallBack([In] IAMTunerNotification pNotify);
		}

		/// <summary>
		/// From TVAudioMode
		/// </summary>
		[Flags]
			public enum TVAudioMode
		{
			Mono = 0x0001,
			Stereo = 0x0002,
			LangA = 0x0010,
			LangB = 0x0020,
			LangC = 0x0040,
		}
		
		/// <summary>
		/// Access to TV audio property
		/// </summary>
		public IAMTVAudio TvAudio
		{
			get { return tvAudio; }
			set
			{
				tvAudio = value;
			}
		}
//#endif

		// ---------------- Public Methods ---------------

		/// <summary>
		/// Dispose Tuner property
		/// </summary>
		new public void Dispose()
		{
			if ( tvTuner != null )
			{
				Marshal.ReleaseComObject( tvTuner );
				tvTuner = null;
			}

			if(tvAudio != null)
			{
				Marshal.ReleaseComObject(tvAudio);
				tvAudio = null;
			}
		}
#endif		
	}
}
