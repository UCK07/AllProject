using AllProject.Client.Pages.Modals;
using AllProject.Shared.Entities;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using AllProject.Client.Services;
using System.Reflection.Metadata;

namespace AllProject.Client.Pages
{
	public partial class MovieIndexPage
	{
		[Inject] protected MovieService _movieService { get; set; }
		IEnumerable<Movie> Movies;
		string SearchMovie;
		[CascadingParameter] public IModalService _modal { get; set; } = default!;
		protected async override Task OnInitializedAsync()
		{
			await GetMovieList();
		}

		private async Task GetMovieList()
		{
			var response = await _movieService.GetMovieList();
			if (response != null)
			{
				Movies = response.OrderByDescending(x => x.Id);
			}
		}

		private async Task GetSearchMovie()
		{
			if (SearchMovie.Length > 1)
			{
				var movies = Movies.Where(x => x.MovieName.ToLower().Contains(SearchMovie.ToLower()) || x.MovieYear.ToString().Contains(SearchMovie.ToLower()));
				Movies = movies;
			}
		}
		private Task OpenModal(Movie movie, bool movieStatus)
		{


			var parameters = new ModalParameters();
			var options = new ModalOptions()
			{
				DisableBackgroundCancel = true,
				HideHeader = false,
				Size = ModalSize.Automatic,
				HideCloseButton = false,
				AnimationType = ModalAnimationType.FadeInOut
			};

			// modala gonderdigimiz parametreler
			parameters.Add(nameof(MovieAddModal.movie), movie);
			parameters.Add(nameof(MovieAddModal.MovieStatus), movieStatus);


			// geri donen callback parametresi.
			parameters.Add(nameof(MovieAddModal.ModalCloseCallBack), EventCallback.Factory.Create<bool>(this, GetMovieList));


			_modal.Show<MovieAddModal>("", parameters, options);
            return Task.CompletedTask;
		}
        private Task OpenImageModal(Movie movie, bool movieStatus)
        {


            var parameters = new ModalParameters();
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideHeader = false,
                Size = ModalSize.Automatic,
                HideCloseButton = false,
                AnimationType = ModalAnimationType.FadeInOut
            };

            // modala gonderdigimiz parametreler
            parameters.Add(nameof(MovieImageModal.movie), movie);

            _modal.Show<MovieImageModal>("", parameters, options);
            return Task.CompletedTask;
        }

    }
}

