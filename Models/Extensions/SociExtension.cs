using System.Linq;
using RegistroServizi.Models.Entities;
using RegistroServizi.Models.InputModels.Soci;
using RegistroServizi.Models.ViewModels.Soci;

namespace RegistroServizi.Models.Extensions
{
    public static class SociExtension
    {
        public static SocioViewModel ToSocioViewModel(this Socio socio)
        {
            return new SocioViewModel
            {
                Id = socio.Id,
                Tessera = socio.Tessera,
                Nominativo = socio.Nominativo,
                Indirizzo = socio.Indirizzo,
                Cap = socio.Cap,
                Comune = socio.Comune,
                Provincia = socio.Provincia,
                LuogoNascita = socio.LuogoNascita,
                DataNascita = socio.DataNascita,
                CodiceFiscale = socio.CodiceFiscale,
                Telefono = socio.Telefono,
                Email = socio.Email,
                DataTesseramento = socio.DataTesseramento,
                TrattamentoDati = socio.TrattamentoDati,
                Professione = socio.Provincia,
                Zona = socio.Zona
            };
        }

        public static SocioDetailViewModel ToSocioDetailViewModel(this Socio socio)
        {
            return new SocioDetailViewModel
            {
                Id = socio.Id,
                Tessera = socio.Tessera,
                Nominativo = socio.Nominativo,
                Indirizzo = socio.Indirizzo,
                Cap = socio.Cap,
                Comune = socio.Comune,
                Provincia = socio.Provincia,
                LuogoNascita = socio.LuogoNascita,
                DataNascita = socio.DataNascita,
                CodiceFiscale = socio.CodiceFiscale,
                Telefono = socio.Telefono,
                Email = socio.Email,
                DataTesseramento = socio.DataTesseramento,
                TrattamentoDati = socio.TrattamentoDati,
                Professione = socio.Provincia,
                Zona = socio.Zona,
                SociFamiliari = socio.SociFamiliari
                                            .OrderBy(sociofamiliare => sociofamiliare.Id)
                                            .ThenBy(sociofamiliare => sociofamiliare.Id)
                                            .Select(sociofamiliare => sociofamiliare.ToSocioFamiliareViewModel())
                                            .ToList(),
                SociRinnovi = socio.SociRinnovi
                                            .OrderBy(sociorinnovo => sociorinnovo.Id)
                                            .ThenBy(sociorinnovo => sociorinnovo.Id)
                                            .Select(sociorinnovo => sociorinnovo.ToSocioRinnovoViewModel())
                                            .ToList()
            };
        }

        public static SocioDetailViewModel ToSocioDetailSingleViewModel(this Socio socio)
        {
            return new SocioDetailViewModel
            {
                Id = socio.Id,
                Tessera = socio.Tessera,
                Nominativo = socio.Nominativo,
                Indirizzo = socio.Indirizzo,
                Cap = socio.Cap,
                Comune = socio.Comune,
                Provincia = socio.Provincia,
                LuogoNascita = socio.LuogoNascita,
                DataNascita = socio.DataNascita,
                CodiceFiscale = socio.CodiceFiscale,
                Telefono = socio.Telefono,
                Email = socio.Email,
                DataTesseramento = socio.DataTesseramento,
                TrattamentoDati = socio.TrattamentoDati,
                Professione = socio.Provincia,
                Zona = socio.Zona
            };
        }

        public static SocioEditInputModel ToSocioEditInputModel(this Socio socio)
        {
            return new SocioEditInputModel
            {
                Id = socio.Id,
                Tessera = socio.Tessera,
                Nominativo = socio.Nominativo,
                Indirizzo = socio.Indirizzo,
                Cap = socio.Cap,
                Comune = socio.Comune,
                Provincia = socio.Provincia,
                LuogoNascita = socio.LuogoNascita,
                DataNascita = socio.DataNascita,
                CodiceFiscale = socio.CodiceFiscale,
                Telefono = socio.Telefono,
                Email = socio.Email,
                DataTesseramento = socio.DataTesseramento,
                TrattamentoDati = socio.TrattamentoDati,
                Professione = socio.Provincia,
                Zona = socio.Zona
            };
        }
    }
}