namespace Entities.ViewModels
{
    public class MucTinToDropDownGroup
    {
        public long Id { get; set; }
        public string Ten { get; set; }
        public long? IdMucTinCha { get; set; }
        public string MaUngDung { get; set; }
        public string TenUngDung { get; set; }
        public int? ThuTu { get; set; }
    }
}
