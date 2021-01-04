using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.People.Edit
{
    public partial class PersonEdit: ComponentBase 
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IPersonService PersonService { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        private PersonEditDto editDto;

        private PersonGetDto getDto;

        private EditContext editContext;

        protected override void OnInitialized()
        {
            getDto = PersonService.GetById(Id);
            editDto = Mapper.Map<PersonEditDto>(getDto);
            editDto.SetPersonService(PersonService);

            editContext = new EditContext(editDto);
            base.OnInitialized();
        }

        private async Task SubmitForm()
        {
            DateTime dobDate = DateTime.MinValue;
            if(!DateTime.TryParse(editDto.DOBString, out dobDate))
                editDto.DOB = DateTime.MinValue;

            var isValid = editContext.Validate();
            if(isValid)
            {
                await editDto.Save();
                NavigationManager.NavigateTo("People");
            }
        }  
    }   
}