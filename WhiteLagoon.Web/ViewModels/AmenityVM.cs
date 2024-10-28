﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WhiteLagoon.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WhiteLagoon.Web.ViewModels
{
    public class AmenityVM
    {
        public Amenity? Amenity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList { get; set; }
    }
}
