using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;

namespace BlazorUI.Pages.People.List
{
    public partial class PersonList: ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        
        [Inject]
        protected IPersonService PersonService { get; set; }

        private IEnumerable<PersonGetDto> People { get; set; }

        protected override void OnInitialized() => People = PersonService.GetAll();

        private void GoToEditPage(int id) => NavigationManager.NavigateTo($"people/{id}/edit");
    }
}