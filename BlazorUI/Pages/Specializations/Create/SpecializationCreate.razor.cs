
using Microsoft.AspNetCore.Components;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.Specializations.Create
{
    public partial class SpecializationCreate: ComponentBase
    {
        [Inject]
        protected ISpecializationService specializationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected SpecializationCreateDto specializationCreateDto;

        protected override void OnInitialized()
        {
            specializationCreateDto = new SpecializationCreateDto(specializationService);
            base.OnInitialized();
        }
    
        private void SubmitForm()
        {
            specializationCreateDto.Save();
            NavigationManager.NavigateTo("Specializations");
        }   
    }
}