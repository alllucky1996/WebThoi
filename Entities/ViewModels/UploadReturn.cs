namespace Entities.ViewModels
{
    public class UploadReturn
    {
        public string name { get; set; }
        public long size { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
    }
}
