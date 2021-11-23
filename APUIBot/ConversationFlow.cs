using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APUIBot
{
    public class ConversationFlow
    {
        public enum Question
        {
            Name,
            Age,
            None // Our last action did not involve a question.
        }

        // The last question asked.
        public Question LastQuestionAsked { get; set; } = Question.None;
    }
}
