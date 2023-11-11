using AllProject.Client.Services;
using AllProject.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace AllProject.Client.Pages
{
    public partial class DictionaryPage
    {
        [Inject] protected CommonService __commonService { get; set; }

        string WordName;
        private DictionaryDto.Root DictionaryList = new();

        protected override async Task OnInitializedAsync()
        {

        }

        public async Task WordChangeEvent(ChangeEventArgs e)
        {
            WordName = e.Value.ToString();

            try
            {
                var response = await __commonService.GetDictionaryList(WordName);
                if (response.success == true)
                {
                    DictionaryList = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
