using AllProject.Shared.Entities;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using AllProject.Client.Services;

namespace AllProject.Client.Pages.Modals
{
    public partial class MovieAddModal
    {
        [Inject] protected MovieService _adminMovieService { get; set; }

        [Inject] protected SweetAlertService _tweetAlertService { get; set; }

        [CascadingParameter] BlazoredModalInstance _modal { get; set; }

        [Parameter] public EventCallback<bool> ModalCloseCallBack { get; set; }

        [Parameter] public Movie movie { get; set; }

        [Parameter] public bool MovieStatus { get; set; }

        private async Task ImageUpload(InputFileChangeEventArgs e)
        {
            try
            {
                var files = e.GetMultipleFiles(1);
                movie.ImageUrl = e.File.Name;
                var buf = new byte[e.File.Size];
                await using (var stream = e.File.OpenReadStream(10000000))
                {
                    var readAsync = await stream.ReadAsync(buf);
                    movie.ImageBase64 = Convert.ToBase64String(buf);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _tweetAlertService.FireAsync("Sistemde bir hata olustu,Lutfen daha sonra tekrar deneyiniz");
            }
        }

        private async Task MovieAdd()
        {
            try
            {
                if (MovieStatus == true)
                {
                    await _adminMovieService.MovieUpdate(movie);
                }
                else
                {
                    await _adminMovieService.MovieAdd(movie);
                }

                await _tweetAlertService.FireAsync("Movie Kaydedildi");
                movie = new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            finally
            {
                await ModalCloseCallBack.InvokeAsync(true);
                await _modal.CloseAsync();
            }
        }

    }
}
