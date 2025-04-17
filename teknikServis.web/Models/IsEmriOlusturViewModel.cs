using TeknikServis.Entities.Servis;

namespace TeknikServis.Web.Models
{
    public class IsEmriOlusturViewModel
    {
        public Musteri Musteri { get; set; } = null!;
        public IEnumerable<IsEmriTeslim> AcikIsEmirleri { get; set; } = Enumerable.Empty<IsEmriTeslim>();
        public IEnumerable<IsEmriTeslim> KapaliIsEmirleri { get; set; } = Enumerable.Empty<IsEmriTeslim>();
        public IsEmriTeslim NewIsEmri { get; set; } = new IsEmriTeslim();
    }
}
