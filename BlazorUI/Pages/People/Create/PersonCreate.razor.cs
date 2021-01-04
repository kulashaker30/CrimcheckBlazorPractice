
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.People.Create
{
    public partial class PersonCreate: ComponentBase
    {
        private PersonCreateDto createDto;

        private EditContext editContext;

        [Inject]
        protected IPersonService PersonService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        
        protected override void OnInitialized()
        {
            createDto = new PersonCreateDto(PersonService);
            editContext = new EditContext(createDto);
            base.OnInitialized();
        }
    
        private async Task SubmitForm()
        {
            DateTime dobDate = DateTime.MinValue;
            if(!DateTime.TryParse(createDto.DOBString,out dobDate))
                createDto.DOB = DateTime.MinValue;
            
            createDto.DOB = dobDate;
            var isValid = editContext.Validate();
            if(isValid)
            {
                await createDto.Save();
                NavigationManager.NavigateTo("People");
            }
        }   
    }
}