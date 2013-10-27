using System;
using System.Text;
using System.IO;

namespace GTApplication
{
    public class Output
    {
        FileStream fs;
        StreamWriter sw;
        private string fullpath;
        private string folder = Directory.GetCurrentDirectory() + @"\TCP_Packages\";

        private static Output instance;
        public static Output Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Output();
                }
                return instance;
            }
        }


        private Output()
        {
            CreateFolder();
            string filename = 
                DateTime.Now.ToShortDateString() + "-" +
                DateTime.Now.ToShortTimeString() + "-" +
                DateTime.Now.Second.ToString() + " " + "Unknown user";
            filename = filename.Replace(":", "-");
            fullpath = folder + filename + ".txt";
            fs = new FileStream(fullpath, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs, Encoding.UTF8);
            //Add header here
            sw.WriteLine("TimeServer\tTimeClient\tIndex\tTargetX\tTargetY\tMouseX\tMouseY\tDistance\tDuration");
            sw.Close();
            fs.Close();
        }

        public void appendToFile(string data)
        {
            fs = new FileStream(fullpath, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine(data);
            sw.Close();
            fs.Close();
        }

        private void CreateFolder()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            if (!dirInfo.Exists)
                Directory.CreateDirectory(folder);
        }
    }
}
