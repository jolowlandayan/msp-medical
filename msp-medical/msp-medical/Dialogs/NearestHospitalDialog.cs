using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace msp_medical.Dialogs
{
    [LuisModel("7155e047-5f1d-454a-85ac-828fb19119d0", "188b459951c04b4482c3650a75e9c874")]
    [Serializable]
    public class NearestHospitalDialog : LuisDialog<object>
    {
        public string city;
        public string hospital;

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"I'm sorry, I did not understand {result.Query}.\nType 'help' to know more about me :)");
            context.Done<object>(null);
        }

        public async Task LocationMessageReceived(IDialogContext context, string name)
        {
            await context.PostAsync($"Where are you located {name}?");
        }

        [LuisIntent("NearestHospital")]
        public async Task FindHospital(IDialogContext context, string argument, LuisResult results)
        {
            EntityRecommendation hospital;

            results.TryFindEntity("hospital", out hospital);
        }


    }
}