using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectShowLib.DMO;
using System.Runtime.InteropServices;

namespace Ogama.Modules.Rta
{
    public class DMOHelper
    {
        /// <summary>
        /// Finds the <see cref="Guid"/> for the given 
        /// category and object
        /// </summary>
        /// <param name="gn">A string containing the name of the DMO.</param>
        /// <param name="cat">A <see cref="Guid"/> with the dmo category.</param>
        /// <returns>The <see cref="Guid"/> of the found DMO.</returns>
        public Guid FindGuid(string gn, Guid cat)
        {
            int hr;

            IEnumDMO dmoEnum;
            Guid[] g2 = new Guid[1];
            string[] sn = new string[1];

            hr = DMOUtils.DMOEnum(cat, 0, 0, null, 0, null, out dmoEnum);
            DMOError.ThrowExceptionForHR(hr);

            try
            {
                do
                {
                    hr = dmoEnum.Next(1, g2, sn, IntPtr.Zero);
                }
                while (hr == 0 && sn[0] != gn);

                // Handle any serious errors
                DMOError.ThrowExceptionForHR(hr);

                if (hr != 0)
                {
                    throw new Exception("Cannot find " + gn);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(dmoEnum);
            }

            return g2[0];
        }
    }
}
