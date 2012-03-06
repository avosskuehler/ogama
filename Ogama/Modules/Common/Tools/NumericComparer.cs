// (c) Vasian Cepa 2005
// Version 2

using System.Collections;
using System.IO;

namespace Ogama.Modules.Common.Tools
{

  public class NumericComparer : IComparer
  {

    public int Compare(object x, object y)
    {
      if ((x is string) && (y is string))
      {
        return StringLogicalComparer.Compare((string)x, (string)y);
      }

      if ((x is FileInfo) && (y is FileInfo))
      {
        return StringLogicalComparer.Compare(((FileInfo)x).Name, ((FileInfo)y).Name);
      }

      return -1;
    }
  }

}