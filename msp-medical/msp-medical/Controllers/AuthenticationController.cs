using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using Microsoft.Bot.Connector;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.Bot.Builder.Dialogs;
using msp_medical.Dialogs;
using System.Web.UI;
using System.Web.Mvc;
using msp_medical.Infrastructure.Database;
using msp_medical.Infrastructure.Entities;

namespace msp_medical.Controllers
{
    [Serializable]
    public class AuthenticationController : ApiController
    {
        // GET: Authentication
        //public ActionResult Index()
        //{
        //    return ViewPage();
        //}
        State state = new State();

        [NonSerialized]
        private StateClient stateClient;
        [NonSerialized]
        private BotState botState;
        [NonSerialized]
        private BotData botData;

        public async Task<HttpResponseMessage> Posts([FromBody]Activity activity)
        {

            Request = new HttpRequestMessage();
            Configuration = new HttpConfiguration();


            if (activity.Type == ActivityTypes.Message)
            {

                if (activity.Text == "No" && (activity.Text != null || string.IsNullOrWhiteSpace(activity.Text)))
                {
                    
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    //ConnectorClient connector = new ConnectorClient(new Uri(@"https://authdemowebfinal20171204095202.azurewebsites.net"));

                    Activity replyToConversation = activity.CreateReply();
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";


                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction fbButton = new CardAction()
                    {
                        //Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/Home/Login?userid={HttpUtility.UrlEncode(activity.From.Id)}",
                        Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/.auth/login/facebook/callback",

                        Type = "signin",
                        Title = "Authentication Required",
                        Text = "Hello",
                        DisplayText = "Hi"


                    };
                    CardAction msButton = new CardAction()
                    {
                        //Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/Home/Login?userid={HttpUtility.UrlEncode(activity.From.Id)}",
                        Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/.auth/login/facebook/callback",
                        Type = "signin",
                        Title = "Authentication Required",
                        Text = "Hello",
                        DisplayText = "Hi"


                    };
                    CardAction twButton = new CardAction()
                    {
                        //Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/Home/Login?userid={HttpUtility.UrlEncode(activity.From.Id)}",
                        Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/.auth/login/facebook/callback",
                        Type = "signin",
                        Title = "Authentication Required",
                        Text = "Hello",
                        DisplayText = "Hi"


                    };
                    //HeroCard plCard = new HeroCard()
                    //{
                    //    Title = $"I'm a hero card about ",
                    //    Tap = ScriptManager.RegisterStartupScript(@"https://msp-medicalweb20171205093715.azurewebsites.net/", typeof(Page), "somekey", script, true),
                    //    //Subtitle = $"{cardContent.Key} Wikipedia Page",
                    //    //Images = cardImages,

                    //    Buttons = cardButtons
                    //};
                    cardButtons.Add(fbButton);
                    cardButtons.Add(msButton);
                    cardButtons.Add(twButton);

                    string fbT = "Login to Facebook";
                    string msT = "Login to Microsoft";
                    string twT = "Login to Twitter";
                    SigninCard fbCard = new SigninCard(fbT, new List<CardAction>() { fbButton });
                    SigninCard msCard = new SigninCard(msT, new List<CardAction>() { msButton });
                    SigninCard twCard = new SigninCard(twT, new List<CardAction>() { twButton });
                    Attachment fbAttachment = fbCard.ToAttachment();
                    Attachment msAttachment = msCard.ToAttachment();
                    Attachment twAttachment = twCard.ToAttachment();
                    replyToConversation.Attachments.Add(fbAttachment);
                    replyToConversation.Attachments.Add(msAttachment);
                    replyToConversation.Attachments.Add(twAttachment);
                    //var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                    //await connector.Conversations.ReplyToActivityAsync(replyToConversation);
                    
                    //await replyToConversation;

                    using (var bd = new DbConfiguration())
                    {
                        var insert = new State()
                        {
                            ETag = "auth",
                            Data = "true"

                        };
                        stateClient = activity.GetStateClient();
                        botState = new BotState(stateClient);
                        //botData = new BotData(); //update db
                       

                        bd.State.Add(insert);
                        bd.SaveChanges();

                    }
                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, "not null");
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
                if (message.Action == "add")
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    Activity reply = message.CreateReply("# Bot Help\n\nType the following command. (You need your Office 365 Exchange Online subscription.)\n\nlogin -- Login to Office 365\n\nget mail -- Get your e-mail from Office 365\n\nrevoke -- Revoke permissions for accessing your e-mail");
                    connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (message.Action == "remove")
                {

                }
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

    }
}