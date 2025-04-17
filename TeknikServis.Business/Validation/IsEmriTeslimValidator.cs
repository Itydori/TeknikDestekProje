//using FluentValidation;
//using TeknikServis.Entities.Servis;

//namespace TeknikServis.Business.Validation
//{
//    public class IsEmriTeslimValidator : AbstractValidator<IsEmriTeslim>
//    {
//        public IsEmriTeslimValidator()
//        {
//            RuleFor(x => x.Marka)
//                .NotEmpty().WithMessage("Marka zorunludur.")
//                .MaximumLength(100).WithMessage("Marka en fazla 100 karakter olabilir.");

//            RuleFor(x => x.Model)
//                .NotEmpty().WithMessage("Model giriniz.")
//                .MaximumLength(100);

//            RuleFor(x => x.ArizaDurumu)
//                .NotEmpty().WithMessage("Arıza durumu zorunludur.")
//                .MaximumLength(500);

//            RuleFor(x => x.Yil)
//                .InclusiveBetween(2000, DateTime.Now.Year)
//                .WithMessage($"Yıl 2000 ile {DateTime.Now.Year} arasında olmalıdır.");

//            RuleFor(x => x.GelisTarih)
//                .NotEmpty().WithMessage("Geliş tarihi zorunludur.")
//                .LessThanOrEqualTo(DateTime.Today)
//                .WithMessage("Geliş tarihi geleceğe olamaz.");

//            RuleFor(x => x.FisNo)
//                .NotEmpty().WithMessage("Fiş no giriniz.")
//                .MaximumLength(50);

//            RuleFor(x => x.GarantiDurumu)
//                .IsInEnum().WithMessage("Geçersiz garanti durumu.");

//            RuleFor(x => x.ServisTalebi)
//                .IsInEnum().WithMessage("Geçersiz servis talebi.");

//            // Opsiyonel alanlar için örnek:
//            RuleFor(x => x.AlinanOdeme)
//                .GreaterThanOrEqualTo(0).When(x => x.AlinanOdeme.HasValue)
//                .WithMessage("Alınan ödeme 0'dan küçük olamaz.");
//        }
//    }
//}
