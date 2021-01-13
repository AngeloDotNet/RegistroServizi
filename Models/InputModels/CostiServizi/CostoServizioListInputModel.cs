using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Customizations.ModelBinders;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Models.InputModels.CostiServizi
{
    [ModelBinder(BinderType = typeof(CostoServizioListInputModelBinder))]
    public class CostoServizioListInputModel
    {
        public CostoServizioListInputModel(string search, int page, string orderby, bool ascending, int limit, CostoServizioOrderOptions orderOptions)
        {
            if (!orderOptions.Allow.Contains(orderby))
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            Search = search ?? "";
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);
            OrderBy = orderby;
            Ascending = ascending;

            Offset = (Page - 1) * Limit;
        }
        
        public string Search { get; }
        public int Page { get; }
        public string OrderBy { get; }
        public bool Ascending { get; }
        
        public int Limit { get; }
        public int Offset { get; }
    }
}