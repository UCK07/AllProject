using AllProject.Client.Services;
using AllProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace AllProject.Client.Pages
{
    public partial class PharmacyPage
    {
        [Inject] protected CommonService _commonService { get; set; }
        string Cityname;
        private PharmacyDto.Root PharmacyList = new();
        protected override async Task OnInitializedAsync()
        {

        }
        public async Task CityChangeEvent(ChangeEventArgs e)
        {
            Cityname = e.Value.ToString();

            try
            {
                var response = await _commonService.GetPharmacyList(Cityname);
                if (response.success == true)
                {
                    PharmacyList = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


    }
}
