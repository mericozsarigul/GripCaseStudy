namespace GripCaseStudy.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageBase64 { get; set; }
        public string ImageThumbBase64 { get; set; }
    }
}
