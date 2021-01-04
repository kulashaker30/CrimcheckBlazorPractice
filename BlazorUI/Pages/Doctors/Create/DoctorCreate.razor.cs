

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Core.Services;
using OnlineClinic.Core.Utilities;

namespace BlazorUI.Pages.Doctors.Create
{
    public partial class DoctorCreate: ComponentBase
    {
        private DoctorCreateDto createDto;

        private EditContext editContext;

        private Dictionary<int, string> people { get; set; } = new Dictionary<int, string>();

        private Dictionary<int, string> specializations { get; set; } = new Dictionary<int, string>();

        [Inject]
        protected IPersonService PersonService { get; set; }

        [Inject]
        protected IDoctorService DoctorService { get; set; }

        [Inject]
        protected ISpecializationService SpecializationService { get; set; }

        [Inject]
        protected IPasswordEncryptionUtil PasswordEncryptionUtil { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            people = PersonService.GetPersonIdAndNames();
            specializations = SpecializationService.GetSpecializationIdAndNames();
            createDto = new DoctorCreateDto(DoctorService, PersonService, SpecializationService, PasswordEncryptionUtil);
            editContext = new EditContext(createDto);
            base.OnInitialized();
        }

        private int PersonId { get; set; }

        private void OnPersonChanged(ChangeEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                createDto.PersonId = Convert.ToInt32(e.Value.ToString());
            }
        }

        private int SpecializationId { get; set; }

        private void OnSpecializationChanged(ChangeEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                createDto.SpecializationId = Convert.ToInt32(e.Value.ToString());
            }
        }

        private async Task SubmitForm()
        {
            var isValid = editContext.Validate();
            if(isValid)
            {
                await createDto.Save();
                NavigationManager.NavigateTo("Doctors");
            }
        }

    }
}