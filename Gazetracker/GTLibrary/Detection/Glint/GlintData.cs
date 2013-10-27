using GTCommons.Enum;

namespace GTLibrary.Detection.Glint
{
    public class GlintData
    {
        public GlintData()
        {
            Glints = new GlintConfiguration(10);
        }

        public EyeEnum Eye { get; set; }

        public GlintConfiguration Glints { get; set; }
    }
}