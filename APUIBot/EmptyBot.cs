// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace APUIBot
{
    public class EmptyBot : ActivityHandler
    {
        private readonly BotState _userState;
        private readonly BotState _conversationState;
        public EmptyBot(ConversationState conversationState, UserState userState)
        {
            _conversationState = conversationState;
            _userState = userState;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Pozdrav, ja sam APUI, virtualni razgovorni agent!"), cancellationToken);
                }
            }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            var conversationStateAccessors = _conversationState
                .CreateProperty<ConversationFlow>(nameof(ConversationFlow));
            var flow = await conversationStateAccessors
                .GetAsync(turnContext, () => new ConversationFlow(), cancellationToken);

            var userStateAccessors = _userState
                .CreateProperty<UserProfile>(nameof(UserProfile));
            var profile = await userStateAccessors
                .GetAsync(turnContext, () => new UserProfile(), cancellationToken);

            await FillOutUserProfileAsync(flow, profile, turnContext, cancellationToken);

            // Save changes.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        private static async Task FillOutUserProfileAsync(ConversationFlow flow, UserProfile profile, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var input = turnContext.Activity.Text?.Trim();
            string message;

            switch (flow.LastQuestionAsked)
            {
                case ConversationFlow.Question.None:
                    await turnContext
                        .SendActivityAsync("Kako se Vi zovete?", null, null, cancellationToken);
                    flow.LastQuestionAsked = ConversationFlow.Question.Name;
                    break;
                case ConversationFlow.Question.Name:
                    profile.Name = input;
                    await turnContext
                        .SendActivityAsync($"Pozdrav {profile.Name}.", null, null, cancellationToken);
                    await turnContext
                        .SendActivityAsync("Koliko imate godina?", null, null, cancellationToken);
                    flow.LastQuestionAsked = ConversationFlow.Question.Age;
                    break;
                case ConversationFlow.Question.Age:
                    profile.Age = int.Parse(input);
                    await turnContext
                        .SendActivityAsync($"Hvala {profile.Name} Vi imate {profile.Age} godina.", null, null, cancellationToken);
                    await turnContext
                        .SendActivityAsync("Doviđenja!", null, null, cancellationToken);
                    break;
            }
        }
    }
}
