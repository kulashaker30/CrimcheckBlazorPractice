using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.Doctors.List
{
    public partial class DoctorList: ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        
        [Inject]
        protected IDoctorService DoctorService { get; set; }

        private IEnumerable<DoctorGetDto> Physicians { get; set; }

        protected override void OnInitialized() => Physicians = DoctorService.GetAllPhysicians();
    }
}