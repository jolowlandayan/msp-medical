﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace msp_medical.Dialogs
{
    [LuisModel("7155e047-5f1d-454a-85ac-828fb19119d0", "188b459951c04b4482c3650a75e9c874")]
    [Serializable]
    public class NearestHospitalDialog : LuisDialog<object>
    {
        public string hosp;
        public string emp;

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"I'm sorry, I did not understand {result.Query}.\nType 'help' to know more about me :)");
            
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Sorry I did not understand, Please type in the city that you are in");

        }

        [LuisIntent("Thanks")]
        public async Task Thanks(IDialogContext context, LuisResult result)
        {
            
            PromptDialog.Confirm(context, DecisionMaker, $"If you have any concerns please type the location wherein you want to find a Hospital near you");
            
            
        }

        public async Task DecisionMaker(IDialogContext context, IAwaitable<bool> result) {
            var confirmation = await result;

            if (confirmation)
            {
                context.Done<object>(null);
            }
            else {

               

                var message = context.MakeMessage();

                var attachment = GetAnimationCard();
                message.Attachments.Add(attachment);

                await context.PostAsync(message);

                

                await context.Forward(new RootDialog(), null, context.Activity, CancellationToken.None);
            }
        }

        [LuisIntent("NearestHospital")]
        public async Task FindHospital(IDialogContext context, LuisResult results)
        {
            //await context.PostAsync("hello!");

            EntityRecommendation hospital;
            List<string> testlist = new List<string>();

            results.TryFindEntity("hospital", out hospital);
            hosp = JsonConvert.SerializeObject(hospital.Resolution["values"], Formatting.None);
            hosp = hosp.Replace("[\"", "");
            hosp = hosp.Replace("\"]", "");
            hosp = hosp.Replace('"', ' ').Trim();

            testlist = hosp.Split(',').ToList();

            await context.PostAsync($"The nearest hospital is {hosp}");
            context.Done(context);
        }

        [LuisIntent("EmployeeId")]
        public async Task FindEmployee(IDialogContext context, LuisResult results)
        {
            //await context.PostAsync("hello!");

            EntityRecommendation hospital;
            List<string> emplist = new List<string>();

            results.TryFindEntity("employee", out hospital);
            emp = JsonConvert.SerializeObject(hospital.Resolution["values"], Formatting.None);
            emp = emp.Replace("[\"", "");
            emp = emp.Replace("\"]", "");
            emp = emp.Replace('"', ' ').Trim();

            emplist = emp.Split(',').ToList();

            await context.PostAsync($"Hello, {emp}");
            context.Done(context);
        }

        private static Attachment GetAnimationCard()
        {
            var animationCard = new AnimationCard
            {
                Title = "Microsoft Bot Framework",
                Subtitle = "Animation Card",
                Image = new ThumbnailUrl
                {
                    Url = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png"
                },
                Media = new List<MediaUrl>
        {
            new MediaUrl()
            {
                Url = "https://m.popkey.co/9239e8/LlwQL.gif"
            }
        }
            };

            return animationCard.ToAttachment();
        }

    }
}