using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Customizations.ModelBinders;
using RegistroServizi.Models.Options;

namespace RegistroServizi.Models.InputModels.SquadreFeriali
{
    [ModelBinder(BinderType = typeof(SquadraFerialeListInputModelBinder))]
    public class SquadraFerialeListInputModel
    {
        public SquadraFerialeListInputModel(string search, int page, string orderby, bool ascending, int limit, SquadraFerialeOrderOptions orderOptions)
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