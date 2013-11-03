using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Recording.SMIInterface.RedM
{
	public interface SmiRedmWrapperListener
	{
		void onSampleData(EyeTrackingController.SampleStruct data);
	}

}
