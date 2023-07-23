namespace Network
{
    [System.Serializable]
    public class VisitorInZone
    {
        public int visistor_id;
        public int tag_id;
        public int zone_id;
        public int age;
        public string gender;
        public int thematic_path_id;
        public string path_audio;
        public string language;
    }
    [System.Serializable]
    public class VisitorsInZone
    {
        public VisitorInZone[] visitor;
    }
}