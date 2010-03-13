// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
// 2006-March-1 HV - created
//
// Copyright (C) 2006 Hans Vosman
// ------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;
#if DSHOWNET
using DShowNET;
#else
using DirectShowLib;
#endif

namespace DirectX.Capture
{
	/// <summary>
	/// Summary description for AsfFormat.
	/// </summary>
	public class AsfFormat: CollectionBase
	{
		/// <summary> Initialize collection with no profiles. </summary>
		internal AsfFormat()
		{
			InnerList.Capacity = 1;
		}

		/// <summary>
		/// Initialize collection with the specified type of profiles.
		/// </summary>
		/// <param name="avformat"></param>
		internal AsfFormat(AsfFormatSelection avformat)
		{
			InnerList.Capacity = 1;
			GetProfileFormatInfo(avformat);
		}

		~AsfFormat()
		{
			Dispose();
		}

		/// <summary>
		/// Allowed Asf audio/video selection choices 
		/// </summary>
		public enum AsfFormatSelection
		{
			/// <summary> All Asf audio/video formats </summary>
			AllFormats,
			/// <summary> Video only asf formats </summary>
			VideoOnly,
			/// <summary> Audio only asf formats </summary>
			AudioOnly,
			/// <summary> All Asf formats with at least video </summary>
			Video,
		}

		private const WMVersion DefaultWMversion = WMVersion.V8_0;
		private const int MAXLENPROFNAME = 100;
		private const int MAXLENPROFDESC = 512;
		// Windows Media Video 8 for Broadband (PAL, 700 Kbps)
		private Guid DefaultVideoProfile = WMData.WMProfile_V80_700PALVideo;
		// Windows Media Audio 8 for Dial-up Modem (CD quality, 64 Kbps)
		private Guid DefaultAudioProfile = WMData.WMProfile_V80_64StereoAudio;

		// Default choice is use index
		private bool useindex = true;

		/// <summary>
		/// Index value 
		/// </summary>
		public bool UseIndex
		{
			get { return useindex; }
			set { useindex = value; }
		}

		/// <summary>
		/// Return index of current Asf item
		/// </summary>
		public int CurrentAsfItem
		{
			get
			{
				if(CurrentProfile != null)
				{
					// Get index of current profile
					return InnerList.IndexOf(CurrentProfile);
				}
				// There is no current profile
				return -1;
			}
			set
			{
				if((value >= 0)&&(value < this.InnerList.Count))
				{
					CurrentProfile = this[value];
				}
			}
		}

		private bool FindDefaultAsfItem(AsfFormatSelection avformat)
		{
			for(int i = 0; i < this.InnerList.Count; i++)
			{
				switch(avformat)
				{
						// Video selections:
					case AsfFormatSelection.AllFormats:
					case AsfFormatSelection.Video:
					case AsfFormatSelection.VideoOnly:
						if((this[i].Guid != Guid.Empty)&&(this[i].Guid == DefaultVideoProfile))
						{
							CurrentProfile = this[i];
							return true;
						}
						break;
					case AsfFormatSelection.AudioOnly:
						if((this[i].Guid != Guid.Empty)&&(this[i].Guid == DefaultAudioProfile))
						{
							CurrentProfile = this[i];
							return true;
						}
						break;
					default:
						break;
				}
			}
			return false;
		}

		/// <summary>
		/// Get current Asf item
		/// </summary>
		private Guid GetCurrentAsfItem()
		{
			if(CurrentProfile != null)
			{
				return CurrentProfile.Guid;
			}
			else
			{
				return Guid.Empty;
			}
		}

		/// <summary>
		/// Get information of current Asf item
		/// </summary>
		/// <param name="captureAudio"></param>
		/// <param name="captureVideo"></param>
		/// <returns></returns>
		public bool GetCurrentAsfAVInfo(out bool captureAudio, out bool captureVideo)
		{
			return GetAsfAVInfo(GetCurrentAsfItem(), out captureAudio, out captureVideo);
		}

		/// <summary>
		/// Get Asf audio/video capture information
		/// </summary>
		/// <param name="guid"></param>
		/// <param name="captureAudio"></param>
		/// <param name="captureVideo"></param>
		/// <returns></returns>
		private bool GetAsfAVInfo(Guid guid, out bool captureAudio, out bool captureVideo)
		{
			captureAudio = false;
			captureVideo = false;

			for(int i = 0; i < InnerList.Count; i++ )
			{
				if((this[i].Guid != Guid.Empty)&&(this[i].Guid == guid))
				{
					captureAudio = this[i].Audio;
					captureVideo = this[i].Video;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Return number of stored Asf items
		/// </summary>
		/// <returns></returns>
		public int NbrAsfItems()
		{
			return this.InnerList.Count;
		}

		/// <summary>
		/// Return Guid of stored item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public Guid GetAsfItemGuid(int item)
		{
			if((item >= 0)&&(item < this.InnerList.Count))
			{
				return this[item].Guid;
			}
			return Guid.Empty;
		}

		/// <summary>
		/// Get asf profile information
		/// </summary>
		/// <returns></returns>
		public bool GetProfileFormatInfo(AsfFormatSelection avformat)
		{
			int hr;
			IWMProfileManager profileManager = null;
			IWMProfile profile = null;

			if(this.InnerList.Count > 0)
			{
				// Profile information already loaded
				return true;
			}

#if DEBUG
			Debug.WriteLine("Look for Windows Media profile information: " + avformat.ToString());
#endif
			try
			{
				StringBuilder profileName = new StringBuilder(MAXLENPROFNAME);
				StringBuilder profileDescription = new StringBuilder(MAXLENPROFDESC);
				IWMProfileManager2 profileManager2 = null;
				IWMProfileManagerLanguage profileManagerLanguage = null;
				WMVersion wmversion = WMVersion.V4_0;
				int streamCount = 0;
				bool audio = false;
				bool video = false;
				int audioBitrate = 0;
				int videoBitrate = 0;
				int nbrProfiles = 0;
				short langID;

				// Open the profile manager
				hr = WMLib.WMCreateProfileManager(out profileManager);
				if(hr >= 0)
				{
					// Convert pWMProfileManager to a IWMProfileManager2
					profileManager2 = profileManager as IWMProfileManager2;
					profileManagerLanguage = profileManager as IWMProfileManagerLanguage;

					hr = profileManager2.GetSystemProfileVersion(out wmversion);
#if DEBUG
					Debug.WriteLine("WM version=" + wmversion.ToString());
#endif

					hr = profileManagerLanguage.GetUserLanguageID(out langID);
#if DEBUG
					Debug.WriteLine("WM language ID=" + langID.ToString());
#endif

					//IWMProfileManagerLanguage
					// Specify the version number of the profiles to use
					hr = profileManager2.SetSystemProfileVersion(DefaultWMversion);
					// Marshal.ThrowExceptionForHR(hr);

					if(hr >= 0)
					{
						hr = profileManager2.GetSystemProfileCount(out nbrProfiles);
						if(hr < 0)
						{
							// No profiles available, so nothing to do
							return false;
						}
#if DEBUG
						Debug.WriteLine("ProfileCount=" + nbrProfiles.ToString());
#endif
					} 
					else
					{
						// Error occured when setting profile version
						return false;
					}

				} 
				else
				{
					// Error occured when creating profile manager
					return false;
				}

				// There are profiles to look for!
				for(int p = 0;(p < nbrProfiles)&&(hr >= 0); p++)
				{
					Guid guid = Guid.Empty;
					int profileNameLen = MAXLENPROFNAME;
					int profileDescLen = MAXLENPROFDESC;

					// Load the profile specified by the caller
					//hr = profileManager2.LoadProfileByID(guid, out profile);
					// Marshal.ThrowExceptionForHR(hr);
					hr = profileManager2.LoadSystemProfile(p, out profile);
					if(hr >= 0)
					{
						IWMProfile2 profile2 = profile as IWMProfile2;
						hr = profile2.GetProfileID(out guid);
#if DEBUG
						if(hr < 0)
						{
							Debug.WriteLine("GetProfileID failed for item " + p.ToString());
						}
#endif
					}
#if DEBUG
					else
					{
						Debug.WriteLine("LoadSystemProfile failed for item " + p.ToString());
					}
#endif

					profileName.Length = 0;
					profileDescription.Length = 0;

					if(hr >= 0)
					{
						hr = profile.GetName(profileName, ref profileNameLen);
#if DEBUG
						if(hr < 0)
						{
							Debug.WriteLine("GetName failed");
						}
#endif
					}
				
					if(hr >= 0)
					{
						hr = profile.GetDescription(profileDescription, ref profileDescLen);
#if DEBUG
						if(hr < 0)
						{
							Debug.WriteLine("GetDescription failed");
						}
#endif
					}

					if(hr >= 0)
					{
						hr = profile.GetStreamCount(out streamCount);
#if DEBUG
						if(hr < 0)
						{
							Debug.WriteLine("GetStreamCount failed");
						}
#endif
					}

					if((streamCount > 0)&&(hr >= 0))
					{
						IWMStreamConfig streamConfig = null;
						Guid streamGuid = Guid.Empty;
						audio = false;
						video = false;
						audioBitrate = 0;
						videoBitrate = 0;

						for(short i = 1;(i <= streamCount)&&(hr >= 0); i++)
						{
							hr = profile.GetStreamByNumber(i, out streamConfig);
							if((hr >= 0)&&(streamConfig != null))
							{
								hr = streamConfig.GetStreamType(out streamGuid);
								if(hr >= 0)
								{
									if(streamGuid == MediaType.Video)
									{
										video = true;
										hr = streamConfig.GetBitrate(out videoBitrate);
										if(hr < 0)
										{
											videoBitrate = 0;
										}
									} 
									else
										if(streamGuid == MediaType.Audio)
									{
										audio = true;
										hr = streamConfig.GetBitrate(out audioBitrate);
										if(hr < 0)
										{
											audioBitrate = 0;
										}
									}
									hr = 0; // Allow possible unreadable bitrates
								}
#if DEBUG
								else
								{
									Debug.WriteLine("GetStreamByNumber failed");
								}
#endif
							} // for i
						}

						// Create profile format item
						if(hr >= 0)
						{
							WMProfileData profileInfo = new WMProfileData(
								guid, profileName.ToString(), profileDescription.ToString(), audioBitrate, videoBitrate, audio, video);

							bool StoreInfo = false;

							// Check if all profiles are allowed
							switch(avformat)
							{
								case AsfFormatSelection.AllFormats:
									StoreInfo = true;
									break;
								case AsfFormatSelection.AudioOnly:
									if((profileInfo.Audio)&&(!profileInfo.Video))
									{
										StoreInfo = true;
									}
									break;
								case AsfFormatSelection.Video:
									if(profileInfo.Video)
									{
										StoreInfo = true;
									}
									break;
								case AsfFormatSelection.VideoOnly:
									if((profileInfo.Video)&&(!profileInfo.Audio))
									{
										StoreInfo = true;
									}
									break;
								default:
									break;
							}

							if(StoreInfo)
							{
								this.InnerList.Add(profileInfo);
							}
						}
					}
				} // for p

				if(hr >= 0)
				{
					// No errors occured!
					return true;
				}
			}
			finally
			{
				FindDefaultAsfItem(avformat);

				// Release the locals
				if (profile != null)
				{
					Marshal.ReleaseComObject(profile);
					profile = null;
				}
				if (profileManager != null)
				{
					Marshal.ReleaseComObject(profileManager);
					profileManager = null;
				}
			}
			return false;
		}

		/// <summary>
		/// Update current Asf item information
		/// </summary>
		public void UpdateAsfAVFormat(IBaseFilter asfWriter)
		{
			if(asfWriter != null)
			{
				IConfigAsfWriter lConfig = asfWriter as IConfigAsfWriter;
				int hr = 0;

				// Set Windows Media Audio/Video format first
				if(this.CurrentAsfItem >= 0)
				{
					// Get Guid for this item
					Guid guid = GetAsfItemGuid(this.CurrentAsfItem);

					if(guid != Guid.Empty)
					{
						// Obsolete interface ???
						hr = lConfig.ConfigureFilterUsingProfileGuid(guid);
						if(hr < 0)
						{
							MessageBox.Show("Problems with updating Windows Media audio/video format information.");
						}
					}
				}

				// Set other (more general) settings, if needed

				// Set index
				hr = lConfig.SetIndexMode(this.UseIndex);
				if(hr < 0)
				{
					MessageBox.Show("Problems with updating Windows Media index information.");
				}
			}
		}

		/// <summary> Get the profile at the specified index. </summary>
		public WMProfileData this[int index]
		{
			get { return( (WMProfileData) InnerList[index] ); }
		}

		/// <summary>
		///  Gets or sets the profile currently in use.
		///  This is marked internal so it is possible to control
		///  how and when the profile is changed.
		/// </summary>
		internal WMProfileData CurrentProfile
		{
			get
			{
				// Loop through each source and find the first
				// enabled source.
				foreach(WMProfileData p in InnerList)
				{
					if(p.Enabled)
					{
						return p;
					}
				}
				return null;
			}
			set
			{
				// Disable all profiles
				foreach(WMProfileData p in InnerList)
				{
					p.Enabled = false;
				}

				// Enable selected profile
				if(value != null)
				{
					value.Enabled = true;
				}
			}
		}

		/// <summary> Empty the collection. </summary>
		public new void Clear()
		{
			for(int i = 0; i < InnerList.Count; i++ )
			{
				this[i].Dispose();
			}
			InnerList.Clear();
		}
		
		/// <summary> Release (unmanaged) resources. </summary>
		public void Dispose()
		{
			Clear();
			InnerList.Capacity = 1;
		}
	}
}
