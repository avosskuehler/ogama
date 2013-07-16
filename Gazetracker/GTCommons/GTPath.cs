namespace GTCommons
{
    using System;
    using System.IO;

    public class GTPath
    {
        /// <summary>
        /// Returns the path to the gazetracker directory in the
        /// local application data folder of the user, which is writable
        /// even if the user has not admin rights.
        /// </summary>
        /// <returns>A <see cref="String"/> with the path to users
        /// local application data e.g. C:\Users\%Username%\AppData\Roaming\Gazetracker</returns>
        public static string GetLocalApplicationDataPath()
        {
            var localApplicationData =
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            localApplicationData += Path.DirectorySeparatorChar + "Gazetracker";
            if (!Directory.Exists(localApplicationData))
            {
                Directory.CreateDirectory(localApplicationData);
            }

            return localApplicationData;
        }
    }
}
