using AllProject.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace AllProject.Client.Pages.Modals
{
    public partial class MovieImageModal
    {
        [Parameter] public Movie movie { get; set; }

    }
}
