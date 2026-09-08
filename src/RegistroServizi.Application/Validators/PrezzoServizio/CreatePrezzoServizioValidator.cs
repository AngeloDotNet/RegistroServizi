namespace RegistroServizi.Application.Validators.PrezzoServizio;

//public class CreatePrezzoServizioValidator : AbstractValidator<CreatePrezzoServizioDto>
//{
//    public CreatePrezzoServizioValidator()
//    {
//        RuleFor(x => x.TipologiaServizio)
//            .IsInEnum()
//            .WithMessage("TipologiaServizio non valida.");

//        RuleFor(x => x.CostoFisso)
//            .GreaterThanOrEqualTo(0)
//            .WithMessage("CostoFisso deve essere maggiore o uguale a 0.");

//        RuleFor(x => x.CostoKm)
//            .GreaterThanOrEqualTo(0)
//            .WithMessage("CostoKm deve essere maggiore o uguale a 0.");

//        RuleFor(x => x.SecondoTrasportato)
//            .GreaterThanOrEqualTo(0)
//            .WithMessage("SecondoTrasportato deve essere maggiore o uguale a 0.");

//        RuleFor(x => x.FermoMacchina)
//            .GreaterThanOrEqualTo(0)
//            .WithMessage("FermoMacchina deve essere maggiore o uguale a 0.");

//        RuleFor(x => x.Accompagnatore)
//            .GreaterThanOrEqualTo(0)
//            .When(x => x.Accompagnatore.HasValue)
//            .WithMessage("Accompagnatore deve essere maggiore o uguale a 0.");

//        RuleFor(x => x.ScontoSocio)
//            .InclusiveBetween(0, 100)
//            .When(x => x.ScontoSocio.HasValue)
//            .WithMessage("ScontoSocio deve essere compreso tra 0 e 100.");
//    }
//}