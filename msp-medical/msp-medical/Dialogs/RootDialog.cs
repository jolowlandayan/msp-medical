using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using msp_medical.Util;
using System.Collections.Generic;
using msp_medical.Controllers;
using AdaptiveCards;

namespace msp_medical.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string name;
        private string sex;
        private string age;
        private string maritalStatus;
        private DateTime birthday;
        private string residence;
        private double contactNo;
        private DateTime dateOfAdmission = DateTime.Now;
        
        
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await context.PostAsync("Hi! I’m the MSP-Medical Bot. I will be guiding you to your appointment.");
            PromptDialog.Confirm(context, this.NameMessageReceivedAsync, "Is already a registered patient?");
            
        }
        
        public async Task NameMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirmed = await argument;
            if (confirmed)
            {
                this.name = Convert.ToString(await argument);
                PromptDialog.Text(context, this.GenderMessageReceivedAsync, "What is your fullname?");
            }
            else
            {
                this.name = Convert.ToString(await argument);
                PromptDialog.Text(context, this.GenderMessageReceivedAsync, "What is your fullname?");
            }

        }
        public async Task GenderMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.sex = await argument;
            var gender = new string[] { "Male", "Female" };
            PromptDialog.Choice(context, this.AgeMessageReceivedAsync, gender, "What is your gender?");
        }
        public async Task AgeMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.age = await argument;
            PromptDialog.Text(context, this.MaritalStatusMessageReceivedAsync, "What is your age?");
        }
        public async Task MaritalStatusMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.maritalStatus = await argument;
            PromptDialog.Confirm(context, this.BirthdayStatusMessageReceivedAsync, "Are you married?");
        }
        public async Task BirthdayStatusMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirmed = await argument;
            if(confirmed)
            {
                this.maritalStatus = "Married";
                PromptDialog.Text(context, this.AddressMessageReceivedAsync, "Where do you live?");
            }
            else
            {
                this.maritalStatus = "Single";
                PromptDialog.Text(context, this.AddressMessageReceivedAsync, "Where do you live?");
            }

        }
        public async Task AddressMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.residence = await argument;
            PromptDialog.Text(context, this.ContactMessageReceivedAsync, "Can you provide your contact information?");
        }
        public async Task ContactMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.contactNo = Convert.ToDouble(await argument);
            PromptDialog.Text(context, this.CategoryMessageReceivedAsync, "Anything else?");
        }
        //public async Task DateofAdmissionReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        //{
        //    this.contactNo = Convert.ToInt32(await argument);
        //    PromptDialog.Text(context, this.CategoryMessageReceivedAsync, "Can you provide your contact information?");
        //}
        public async Task CategoryMessageReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            this.residence = await argument;
            var text = $"Great! I'm going to set your appointment \"{this.name}\" " +
                       
                       $"The basic information I will use is " + 
                       $"Gender: \"{this.sex}\".  " +
                       $"Age: \"{this.age}\".  " +
                       $"Marital Status: \"{this.maritalStatus}\".  " +
                       $"Birthday: \"{this.birthday}\".  " +
                       $"Residence: \"{this.residence}\".  " +
                       $"Contact No: \"{this.contactNo}\".  " +
                       $"Date of Admission: \"{this.dateOfAdmission}\".  " +
                       $"Can you please confirm that this information is correct?";

            PromptDialog.Confirm(context, this.IssueConfirmedMessageReceivedAsync, text);
        }

        public async Task IssueConfirmedMessageReceivedAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirmed = await argument;

            if (confirmed)
            {
                var api = new TicketAPIClient();
                var ticketId = await api.PostTicketAsync(this.name, this.age, this.sex,this.maritalStatus,this.birthday,this.residence, this.contactNo,this.dateOfAdmission);

                if (ticketId != -1)
                {
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>
                    {
                        new Attachment
                        {
                            ContentType = "application/vnd.microsoft.card.adaptive",
                            Content = this.CreateCard(ticketId, this.name, this.age, this.sex, this.maritalStatus, this.birthday, this.residence, this.contactNo, this.dateOfAdmission)
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
        private AdaptiveCard CreateCard(int ticketId, string name, string age, string gender, string maritalStatus, DateTime birthday, string residence, double contactNo, DateTime dateofAdmission)
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
                                    new AdaptiveCards.Fact("Gender:", sex),
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
                Text = "Date of Admission:" + dateOfAdmission.ToString(),
                Wrap = true
            };

            card.Body.Add(headerBlock);
            card.Body.Add(columnsBlock);
            card.Body.Add(doa);

            return card;
        }
    }
}