using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.Specializations.Edit
{
    public partial class SpecializationEdit: ComponentBase
    {
        [Inject]
        protected ISpecializationService SpecializationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        private SpecializationEditDto specializationEditDto;
        
        private SpecializationGetDto specializationGetDto;

        private EditContext editContext;

        [Parameter]
        public int Id { get; set; }

        protected override void OnInitialized()
        {
            specializationGetDto = SpecializationService.GetById(Id);
            specializationEditDto = Mapper.Map<SpecializationEditDto>(specializationGetDto);
            specializationEditDto.SetSpecializationService(SpecializationService);
            
            editContext = new EditContext(specializationEditDto);
            base.OnInitializedAsync();
        }

        private async Task Submit()
        {
            var isValid = editContext.Validate();
            if(isValid)
            {
                await specializationEditDto.Save();
                NavigationManager.NavigateTo("Specializations");
            }
        }
    }
}