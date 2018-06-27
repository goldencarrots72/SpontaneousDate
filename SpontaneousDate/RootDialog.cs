using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SpontaneousDate.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            var replyMessage = context.MakeMessage();
            replyMessage.Text = "Spontaneous date woo";

            await context.PostAsync(replyMessage);
            this.Menu(context);
        }

        private readonly IDictionary<string, string> options = new Dictionary<string, string>
        {
             { "1", "Inside" },
             { "2", "Outside" },
             {"3", "Home" }

        };

        private void Menu(IDialogContext context)
        {
            PromptDialog.Choice<string>(
             context,
             this.SelectedOptionAsync,
             this.options.Keys,
             "Do you want to be 1. inside, 2. outside, or 3. at home?",
             "Please select 1, 2, or 3",
             3,
             PromptStyle.PerLine,
             this.options.Values);
        }

        public async Task SelectedOptionAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var message = await argument;

            var replyMessage = context.MakeMessage();

            switch (message)
            {
                case "1":
                    Inside(replyMessage);
                    await context.PostAsync(replyMessage);
                    break;
                case "2":
                    Outside(replyMessage);
                    await context.PostAsync(replyMessage);
                    break;
                case "3":
                    Home(replyMessage);
                    await context.PostAsync(replyMessage);
                    break;
            }
            context.Wait(MessageReceivedAsync);
        }

        public void Inside(IMessageActivity replyMessage)
        {
            replyMessage.Text = "Should go to conversation, in carousel format";
            replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMessage.Attachments = new List<Attachment>();

            Dictionary<string, string> cardContentList = new Dictionary<string, string>();
            cardContentList.Add("Coffee/Bubble Tea Date", "https://<ImageUrl1>");
            cardContentList.Add("Museum", "https://<ImageUrl3>");
            cardContentList.Add("Aquarium", "https://<ImageUrl3>");

            foreach (KeyValuePair<string, string> cardContent in cardContentList)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: cardContent.Value));

                List<CardAction> cardButtons = new List<CardAction>();

                CardAction plButton = new CardAction()
                {
                    Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
                    Type = "openUrl",
                    Title = "WikiPedia Page"
                };

                cardButtons.Add(plButton);

                HeroCard plCard = new HeroCard()
                {
                    Title = $"{cardContent.Key}",
                    Subtitle = $"{cardContent.Key} Wikipedia Page",
                    Images = cardImages,
                    Buttons = cardButtons
                };

                Attachment plAttachment = plCard.ToAttachment();
                replyMessage.Attachments.Add(plAttachment);
            }
            return;
        }

        public void Outside(IMessageActivity replyMessage)
        {
            replyMessage.Text = "Should go to conversation, in carousel format";
            replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMessage.Attachments = new List<Attachment>();

            Dictionary<string, string> cardContentList = new Dictionary<string, string>();
            cardContentList.Add("Public Park", "https://<ImageUrl1>");
            cardContentList.Add("Build a Snowman/Go to the Beach", "https://<ImageUrl2>");
            cardContentList.Add("Amusement/Water Park", "https://<ImageUrl3>");

            foreach (KeyValuePair<string, string> cardContent in cardContentList)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: cardContent.Value));

                List<CardAction> cardButtons = new List<CardAction>();

                CardAction plButton = new CardAction()
                {
                    Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
                    Type = "openUrl",
                    Title = "WikiPedia Page"
                };

                cardButtons.Add(plButton);

                HeroCard plCard = new HeroCard()
                {
                    Title = $"{cardContent.Key}",
                    Subtitle = $"{cardContent.Key} Wikipedia Page",
                    Images = cardImages,
                    Buttons = cardButtons
                };

                Attachment plAttachment = plCard.ToAttachment();
                replyMessage.Attachments.Add(plAttachment);
            }

            return;
        }


        public void Home(IMessageActivity replyMessage)
        {
            replyMessage.Text = "Should go to conversation, in carousel format";
            replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMessage.Attachments = new List<Attachment>();

            Dictionary<string, string> cardContentList = new Dictionary<string, string>();
            cardContentList.Add("Stream a Movie", "https://<ImageUrl1>");
            cardContentList.Add("Video/Board games", "https://<ImageUrl3>");
            cardContentList.Add("Cook/Bake Together", "https://<ImageUrl3>");

            foreach (KeyValuePair<string, string> cardContent in cardContentList)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: cardContent.Value));

                List<CardAction> cardButtons = new List<CardAction>();

                CardAction plButton = new CardAction()
                {
                    Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
                    Type = "openUrl",
                    Title = "WikiPedia Page"
                };

                cardButtons.Add(plButton);

                HeroCard plCard = new HeroCard()
                {
                    Title = $"{cardContent.Key}",
                    Subtitle = $"{cardContent.Key} Wikipedia Page",
                    Images = cardImages,
                    Buttons = cardButtons
                };

                Attachment plAttachment = plCard.ToAttachment();
                replyMessage.Attachments.Add(plAttachment);
            }
            return;
        }

        /* replyMessage.Text = ("What's your budget?");
            replyMessage.Type = ActivityTypes.Message;
            replyMessage.TextFormat = TextFormatTypes.Plain;

           PromptDialog.Choice<string>(
           context,
           this.SelectedOptionAsync,
           this.options.Keys,
           "pick a button",
           "Please select 1 or 2",
           12,
           PromptStyle.PerLine,
           this.options.Values);

            replyMessage.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction(){ Title = "no money", Type=ActionTypes.ImBack, Value="4" },
                    new CardAction(){ Title = "10 bucks", Type=ActionTypes.ImBack, Value="5" },
                    new CardAction(){ Title = "20 buckeroos", Type=ActionTypes.ImBack, Value="6" }
                }
            };
        */

    }
}
