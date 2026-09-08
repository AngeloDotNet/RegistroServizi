namespace RegistroServizi.Application.Validators.Applicazione;

//public class UpdateApplicazioneValidator : AbstractValidator<UpdateApplicazioneDto>
//{
//    //public UpdateApplicazioneValidator()
//    //{
//    //    RuleFor(x => x.NomeApplicazione)
//    //        .NotEmpty().WithMessage("NomeApplicazione non può essere vuoto.")
//    //        .MaximumLength(200).WithMessage("NomeApplicazione non può superare i 200 caratteri.");

//    //    RuleFor(x => x.Versione)
//    //        .NotEmpty().WithMessage("Versione non può essere vuoto.")
//    //        .MaximumLength(15).WithMessage("Versione non può superare i 15 caratteri.");
//    //}

//    private static readonly Regex versioneRegex = new(@"^\d+\.\d+\.\d+$", RegexOptions.Compiled);

//    public UpdateApplicazioneValidator()
//    {
//        RuleFor(x => x.NomeApplicazione)
//            .NotEmpty().WithMessage("NomeApplicazione non può essere vuoto.")
//            .MaximumLength(200).WithMessage("NomeApplicazione non può superare i 200 caratteri.");

//        RuleFor(x => x.Versione)
//            .NotEmpty().WithMessage("Versione non può essere vuoto.")
//            .MaximumLength(15).WithMessage("Versione non può superare i 15 caratteri.")
//            .Must(v => versioneRegex.IsMatch(v))
//            .WithMessage("Versione deve avere il formato X.Y.Z, ad esempio 1.0.0.");
//    }
//}