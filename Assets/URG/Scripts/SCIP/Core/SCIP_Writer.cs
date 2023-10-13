namespace URG.SCIP.Core
{
    // http://sourceforge.net/p/urgnetwork/wiki/cs_sample_jp/
    public class SCIP_Writer
    {
        public static string END => "\n";

        /// <summary>
        /// write MD command
        /// </summary>
        /// <param name="start">measurement start step</param>
        /// <param name="end">measurement end step</param>
        /// <param name="grouping">grouping step number</param>
        /// <param name="skips">skip scan number</param>
        /// <param name="scans">get scan numbar</param>
        /// <returns>created command</returns>
        public static string MD(int start, int end, int grouping = 1, int skips = 0, int scans = 0)
        {
            return MODE.MD.ToCMD() + start.ToString("D4") + end.ToString("D4") + grouping.ToString("D2") + skips.ToString("D1") + scans.ToString("D2") + END;
        }

        public static string ME(int start, int end, int grouping = 1, int skips = 0, int scans = 0)
        {
            return MODE.ME.ToCMD() + start.ToString("D4") + end.ToString("D4") + grouping.ToString("D2") + skips.ToString("D1") + scans.ToString("D2") + END;
        }

        public static string BM()
        {
            return MODE.BM.ToCMD() + END;
        }

        public static string GD(int start, int end, int grouping = 1)
        {
            return MODE.GD.ToCMD() + start.ToString("D4") + end.ToString("D4") + grouping.ToString("D2") + END;
        }

        public static string VV()
        {
            return MODE.VV.ToCMD() + END;
        }

        public static string II()
        {
            return MODE.II.ToCMD() + END;
        }

        public static string PP()
        {
            return MODE.PP.ToCMD() + END;
        }

        public static string SCIP2()
        {
            return "SCIP2.0" + END;
        }

        public static string QT()
        {
            return MODE.QT.ToCMD() + END;
        }
    }
}
