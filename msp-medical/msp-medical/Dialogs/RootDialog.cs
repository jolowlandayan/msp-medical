using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using msp_medical.Util;
using System.Collections.Generic;
using msp_medical.Controllers;
using AdaptiveCards;
using msp_medical.Infrastructure.Entities;

namespace msp_medical.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        PatientInfo PatientDetails = new PatientInfo();

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await context.PostAsync("Hi! I’m the MSP-Medical Bot. I will be guiding you to your Appointment.");
            PromptDialog.Confirm(context, this.NameMessageReceivedAsync, "Are you already a registered patient?");

        }
        
        public async Task NameMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {

            var confirmed = await argument;
            await context.PostAsync("Thanks this is noted \U0001F600");
            PromptDialog.Text(context, this.FullNameMessageReceivedAsync, "What is your fullname? Ex. Jane D Doe"); 
        }

        public async Task FullNameMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
             
            this.PatientDetails.Name = Convert.ToString(await argument);
            await GenderMessageReceivedAsync(context);



        }
        public async Task GenderMessageReceivedAsync(IDialogContext context)
        {
            
            
            PromptDialog.Choice(context, this.missingGender, new List<string>() { "Male", "Female" }, $"What is your gender {this.PatientDetails.Name}?");

        }

        public async Task missingGender(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Sex = await argument;
            await AgeMessageReceivedAsync(context);
        }
        public async Task AgeMessageReceivedAsync(IDialogContext context)
        {
            
            PromptDialog.Text(context, this.MaritalStatusMessageReceivedAsync, "What is your age?");
        }
        public async Task MaritalStatusMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.MaritalStatus = await argument;
            this.PatientDetails.Age = "26";
            PromptDialog.Confirm(context, this.BirthdayStatusMessageReceivedAsync, $"Are you married {this.PatientDetails.Name}? \U0001F600 \U0001F600 \U0001F600");
        }
        public async Task BirthdayStatusMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirmed = await argument;
            if(confirmed)
            {
                this.PatientDetails.MaritalStatus = "Married";
                PromptDialog.Text(context, this.AddressMessageReceivedAsync, "Where do you live?");
            }
            else
            {
                this.PatientDetails.MaritalStatus = "Single";
                PromptDialog.Text(context, this.AddressMessageReceivedAsync, "Where do you live?");
            }

        }
        public async Task AddressMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Address = await argument;
            PromptDialog.Text(context, this.ContactMessageReceivedAsync, "Please provide your contact information? Ex. 091755833");
        }
        public async Task ContactMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
          this.PatientDetails.ContactNumber = await argument;
          await context.PostAsync( "Thank you, your initial information has been gathered \U0001F600");


            var text = $"Great! I'm going to set your appointment \"{this.PatientDetails.Name}\" " +
                        $"\n\n" +
                        $"The basic information I will use is " +
                        $"\n" +
                        $"Gender: \"{this.PatientDetails.Sex}\".  " +
                        $"\n" +
                        $"Age: \"{this.PatientDetails.Age}\".  " +
                        $"\n" +
                        $"Marital Status: \"{this.PatientDetails.MaritalStatus}\".  " +
                        $"\n" +
                        $"Birthday: \"{this.PatientDetails.Birthday}\".  " +
                        $"\n" +
                        $"Residence: \"{this.PatientDetails.Address}\".  " +
                        $"\n" +
                        $"Contact No: \"{this.PatientDetails.ContactNumber}\".  " +
                        $"\n" +
                        $"Date of Admission: \"{this.PatientDetails.DateOfAdmission}\".  " +
                        $"\n" +
                        $"Can you please confirm that these information are correct?";

            await context.PostAsync("We will now gather information related to your present illness that you are feeling.");


            PromptDialog.Text( context, this.DescriptionMessageReceivedAsync,"Please describe your current illness/feeling");

            
        }

        public async Task DescriptionMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Description = await argument;
            await context.PostAsync($" we hope {this.PatientDetails.Name} youre feeling fine :(");
            PromptDialog.Text(context, this.AggravatingFactorsMessageReceivedAsync, " Are there any aggravating factors to which may have cause your current illness?");
        }

        public async Task AggravatingFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.AggravatingFactors = await argument;
            PromptDialog.Text(context, this.RelievingFactorsMessageReceivedAsync, " Are there any relieving factors to which made you feel better?");

        }

        public async Task RelievingFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.RelievingFactors = await argument;
            PromptDialog.Text(context, this.IntensityFactorsMessageReceivedAsync, "Rate the pain you are experiencing from 1 - 10");

        }

        public async Task IntensityFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Intensity = await argument;
            PromptDialog.Text(context, this.TimingsFactorsMessageReceivedAsync, "Are there any timings wherein you experience the pain?");

        }

        public async Task TimingsFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Timing = await argument;
            PromptDialog.Text(context, this.medicationsFactorsMessageReceivedAsync, "Are you taking any medications to relieve the pain.");

        }

        public async Task medicationsFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Medications = await argument;
            PromptDialog.Text(context, this.previousHospitalizationFactorsMessageReceivedAsync, "Have you been recently hospitalied");

        }

        public async Task previousHospitalizationFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.PreviousHospitalization = await argument;
            PromptDialog.Text(context, this.medicationsTakenFactorsMessageReceivedAsync, "May you kindly list down the medications you are taking");

        }

        public async Task medicationsTakenFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.MedicationsTaken = await argument;
            PromptDialog.Text(context, this.diseasesFactorsMessageReceivedAsync, "May you kindly list down any disease you or your family members have?");

        }

        public async Task diseasesFactorsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Diseases = await argument;
            PromptDialog.Text(context, this.injuriesorAccidentsMessageReceivedAsync, "Did you have any recent injuries or accidents?");

        }

        public async Task injuriesorAccidentsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.InjuriesAccidents = await argument;
            PromptDialog.Text(context, this.operationsMessageReceivedAsync, "may you list down all operations that you have had");

        }

        public async Task operationsMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Operations = await argument;
            PromptDialog.Text(context, this.allergiesMessageReceivedAsync, "please list down all allergies you have");

        }

        public async Task allergiesMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.Allergies = await argument;
            
            PromptDialog.Choice(context, this.waterSupplyMessageReceivedAsync, new List<string>() { "Nawasa", "Maynilad", "Deepwell" }, "where do you get your water supply");

        }

        public async Task waterSupplyMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.WaterSupply = await argument;
            
            PromptDialog.Choice(context, this.drinkingwaterMessageReceivedAsync, new List<string>() { "Deepwell", "Filtered", "Boiledwater" }, "where do you get your drinking water");

        }

        public async Task drinkingwaterMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.HouseholdMembers = await argument;
           
            PromptDialog.Text(context, this.numberofhouseholdwaterMessageReceivedAsync,"Number of household members");

        }

        public async Task numberofhouseholdwaterMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.PatientDetails.HouseholdMembers = await argument;

            var text = $"Great! I'm going to set your appointment \"{this.PatientDetails.Name}\" " +
            $"\n\n" +
            $"The basic information I will use is " +
            $"\n" +
            $"Gender: \"{this.PatientDetails.Sex}\".  " +
            $"\n" +
            $"Age: \"{this.PatientDetails.Age}\".  " +
            $"\n" +
            $"Marital Status: \"{this.PatientDetails.MaritalStatus}\".  " +
            $"\n" +
            $"Birthday: \"{this.PatientDetails.Birthday}\".  " +
            $"\n" +
            $"Residence: \"{this.PatientDetails.Address}\".  " +
            $"\n" +
            $"Contact No: \"{this.PatientDetails.ContactNumber}\".  " +
            $"\n" +
            $"Date of Admission: \"{this.PatientDetails.DateOfAdmission}\".  " +
            $"\n" +
            $"Can you please confirm that these information are correct?";
            await context.PostAsync(text);
            PromptDialog.Confirm(context, this.IssueConfirmedMessageReceivedAsync, "Are all information correct, Please select  Yes if you would like to schedule an appointment and No if otherwise.");

        }


        public async Task IssueConfirmedMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirmed = await argument;

            if (confirmed)
            {
                var api = new TicketAPIClient();
                var ticketId = await api.PostTicketAsync(PatientDetails);

                if (ticketId != -1)
                {
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>
                    {
                        new Attachment
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = this.CreateCard(ticketId, this.PatientDetails.Name, this.PatientDetails.Age, this.PatientDetails.Sex,this.PatientDetails.MaritalStatus, this.PatientDetails.Birthday, this.PatientDetails.Address, this.PatientDetails.ContactNumber, this.PatientDetails.DateOfAdmission)
                        }
                    };

                    await context.PostAsync(message);
                }
                else
                {
                    await context.PostAsync("Ooops! Something went wrong while I was saving your ticket. Please try again later.");
                }
            }
            else
            {
                await context.PostAsync("Ok. The ticket was not created. You can start again if you want.");

                
            }

            context.Done<object>(null);
        }
        private AdaptiveCard CreateCard(int ticketId, string name ,string age,string gender, string maritalStatus, DateTime birthday, string residence, string contactNo, DateTime dateofAdmission)
        {
            AdaptiveCard card = new AdaptiveCard();

            var headerBlock = new TextBlock()
            {
                Text = $"Appointment #{ticketId}",
                Weight = TextWeight.Bolder,
                Size = TextSize.Large,
                Speak = $"<s>You've set a new Appointment #{ticketId}</s><s>We will contact you soon.</s>"
            };

            var columnsBlock = new ColumnSet()
            {
                Separation = SeparationStyle.Strong,
                Columns = new List<Column>
                {
                    new Column
                    {
                        Size = "1",
                        Items = new List<CardElement>
                        {
                            new FactSet
                            {
                                Facts = new List<AdaptiveCards.Fact>
                                {
                                    new AdaptiveCards.Fact("Name:", name),
                                    new AdaptiveCards.Fact("Gender:", gender),
                                    new AdaptiveCards.Fact("Age:", age.ToString()),
                                    new AdaptiveCards.Fact("Marital Status:", maritalStatus),
                                    new AdaptiveCards.Fact("Birthday:", birthday.ToString()),
                                    new AdaptiveCards.Fact("Residence:", residence),
                                    new AdaptiveCards.Fact("Contact No:", contactNo.ToString()),
                                   
                                }
                            }
                        }
                    },
                    new Column
                    {
                        Size = "auto",
                        Items = new List<CardElement>
                        {
                            new Image
                            {
                                Url = "https://raw.githubusercontent.com/GeekTrainer/help-desk-bot-lab/master/assets/botimages/head-smiling-medium.png",
                                Size = ImageSize.Small,
                                HorizontalAlignment = HorizontalAlignment.Right
                            }
                        }
                    }
                }
            };

            var doa = new TextBlock
            {
                Text = "Date of Admission:" + this.PatientDetails.DateOfAdmission.ToString(),
                Wrap = true
            };

            card.Body.Add(headerBlock);
            card.Body.Add(columnsBlock);
            card.Body.Add(doa);

            return card;
        }
    }
}