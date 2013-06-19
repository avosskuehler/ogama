using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaTestProject
{
    public class TestdataProvider
    {
        public static string GetTestFolder()
        {
            return "C:/temp";
        }

        public static string GetTestDatabaseFilename()
        {
            return System.IO.Path.Combine(GetTestFolder(), "test.db");
        }
    }
}
