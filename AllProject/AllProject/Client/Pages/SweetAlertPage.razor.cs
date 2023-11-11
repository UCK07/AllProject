using Blazored.Toast.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

namespace AllProject.Client.Pages
{
    public partial class SweetAlertPage
    {
        [Inject] protected SweetAlertService AlertMessage { get; set; }
        [Inject] protected IToastService _toast { get; set; }
        private async Task ClickMessage()
        {
            // await AlertMessage.FireAsync("Hello world!");
            await AlertMessage.FireAsync("Merhaba", "Nasilsin", SweetAlertIcon.Info);
            _toast.ShowWarning("Merhaba Nasilsin");

        }
        private async Task ClickErrorMessage()
        {
            await AlertMessage.FireAsync("Oops...", "Something went wrong!", "success");
        }
    }
}