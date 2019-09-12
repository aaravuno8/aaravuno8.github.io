// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using SampleEchoBot.Models;
using SampleEchoBot.States;

namespace SampleEchoBot.Bots
{
    // Represents a bot that processes incoming activities.
    // For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    // This is a Transient lifetime service. Transient lifetime services are created
    // each time they're requested. For each Activity received, a new instance of this
    // class is created. Objects that are expensive to construct, or have a lifetime
    // beyond the single turn, should be carefully managed.
    // For example, the "MemoryStorage" object and associated
    // IStatePropertyAccessor{T} object are created with a singleton lifetime.
    public class WelcomeUserBot : ActivityHandler
    {
        // Messages sent to the user.
        private const string WelcomeMessage = @"I am ReplyHelper Bot. My job is to assist you to help prepare the best possible reply for Customer queries based on the Priority, High Value Customers, Customer preferences and History.";

        private const string PatternMessage = @"You can type 'support', 'info'";

        private BotState _userState;

        // Initializes a new instance of the "WelcomeUserBot" class.
        public WelcomeUserBot(UserState userState)
        {
            _userState = userState;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            //await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }


        // Greet when users are added to the conversation.
        // Note that all channels do not send the conversation update activity.
        // If you find that this bot works in the emulator, but does not in
        // another channel the reason is most likely that the channel does not
        // send this activity.
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            ResponseHelper responseHelper = new ResponseHelper(turnContext, cancellationToken);
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await responseHelper.Show($"Hi - {member.Name}");
                    await responseHelper.ShowWithDelay(WelcomeMessage);
                    await responseHelper.ShowWithDelay($"Help me know you better.");
                    await responseHelper.ShowWithDelay($"What is your name?");
                }
            }
        }

        public class ResponseHelper
        {
            public ResponseHelper(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
            {
                _turnContext = turnContext;
                _cancellationToken = cancellationToken;

            }

            public async Task<ResourceResponse> Show(string strTextToSend)
            {
                return await _turnContext.SendActivityAsync(strTextToSend, cancellationToken: _cancellationToken);
            }

            public async Task<ResourceResponse> ShowWithDelay(string strTextToSend)
            {
                await Task.Delay(3000);
                return await _turnContext.SendActivityAsync(strTextToSend, cancellationToken: _cancellationToken);
            }

            public ITurnContext<IConversationUpdateActivity> _turnContext { get; private set; }
            public CancellationToken _cancellationToken { get; private set; }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeUserStateAccessor = _userState.CreateProperty<WelcomeUserState>(nameof(WelcomeUserState));
            var didBotWelcomeUser = await welcomeUserStateAccessor.GetAsync(turnContext, () => new WelcomeUserState());

            var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var userProfile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile());

            if (didBotWelcomeUser.DidBotWelcomeUser == false)
            {
                didBotWelcomeUser.DidBotWelcomeUser = true;

                // the channel should sends the user name in the 'From' object
                userProfile.Name = turnContext.Activity.Text?.Trim();
                await turnContext.SendActivityAsync($"Thanks {userProfile.Name}. Let me see how you day looks like...");

                await SendIntroCardAsync(turnContext, cancellationToken);
            }   
            else
            {
                // This example hardcodes specific utterances. You should use LUIS or QnA for more advance language understanding.
                var text = turnContext.Activity.Text.ToLowerInvariant();
                switch (text)
                {
                    case "info":
                        await turnContext.SendActivityAsync($"You said {text}.", cancellationToken: cancellationToken);
                        break;
                    case "support":
                        await SendIntroCardAsync(turnContext, cancellationToken);
                        break;
                    default:
                        await turnContext.SendActivityAsync(WelcomeMessage, cancellationToken: cancellationToken);
                        break;
                }
            }

            // Save any state changes.
            await _userState.SaveChangesAsync(turnContext);
        }

        private static async Task SendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard();
            card.Title = $"Happy {DateTime.Now.DayOfWeek}!";
            card.Text = @"These are the top support items for you today.";
            card.Images = new List<CardImage>() { new CardImage("https://aka.ms/bf-welcome-card-image") };
            card.Buttons = new List<CardAction>()
            {
                new CardAction(ActionTypes.OpenUrl, "Investor Solutions", null, "Investor Solutions", "Investor Solutions", "https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0"),
                new CardAction(ActionTypes.OpenUrl, "Creative Financial Advisory", null, "Creative Financial Advisory", "Creative Financial Advisory", "https://stackoverflow.com/questions/tagged/botframework"),
                new CardAction(ActionTypes.OpenUrl, "Alpha Advisory", null, "Alpha Advisory", "Alpha Advisory", "https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-deploy-azure?view=azure-bot-service-4.0"),
            };
            
            var response = MessageFactory.Attachment(card.ToAttachment());
            await turnContext.SendActivityAsync(response, cancellationToken);
        }
    }
}
