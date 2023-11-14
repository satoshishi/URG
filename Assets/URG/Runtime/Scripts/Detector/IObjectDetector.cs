namespace URG.Detecting
{
    using System.Collections.Generic;

    public interface IObjectDetector
    {
        List<DetectedObject> Currently();
    }
}
