using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace AllProject.Client.Pages
{
    public partial class ToastPage
    {
        [Inject] protected IToastService _toast { get; set; }


        private void ClickShoToast()
        {

            _toast.ShowSuccess("Nasilsin Iyimisin");
            _toast.ShowError("Hata Olustu");
            _toast.ShowToast(ToastLevel.Info, "nasilsin");
        }
    }
}
