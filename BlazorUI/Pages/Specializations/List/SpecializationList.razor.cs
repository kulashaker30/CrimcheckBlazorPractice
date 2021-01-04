using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineClinic.Core.DTOs;

namespace BlazorUI.Pages.Specializations.List
{
    public partial class SpecializationList: ComponentBase
    {
        private IEnumerable<SpecializationGetDto> Specializations { get; set; }

        protected override void OnInitialized() => Specializations = SpecializationService.GetAll();
    }
}