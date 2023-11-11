using AllProject.Shared.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using AllProject.Client.Services;

namespace AllProject.Client.Pages
{
	public partial class MovieAddPage
	{
		[Inject] protected NavigationManager navigationManager { get; set; }
		[Inject] protected MovieService _movieService { get; set; }

		Movie movie = new();

		string errorMessage;
		private async Task MovieAdd()
		{
			try
			{
				var result = await _movieService.MovieAdd(movie);
				if (result == true)
				{
					navigationManager.NavigateTo("/");
				}
			}
			catch (Exception ex)
			{

				errorMessage = ex.Message;
			}


		}

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
			}
		}

	}


}

