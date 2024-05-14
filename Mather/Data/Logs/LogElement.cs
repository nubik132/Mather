using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Logs
{
    public class LogElement
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public string RightAnswer { get; set; }
        public bool IsRight { get; set; }
        public LogElement()
        {
            Text = string.Empty;
            Answer = string.Empty;
            RightAnswer = string.Empty;
            IsRight = false;
        }
        public LogElement(string text, string answer, string rightAnswer, bool isRight)
        {
            Text = text;
            Answer = answer;
            RightAnswer = rightAnswer;
            IsRight = isRight;
        }

        public override string? ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Text);
            builder.Append(": ");
            builder.Append(Answer);
            builder.Append(" (");
            builder.Append(IsRight ? "Верно" : "Неверно");
            builder.Append(')');
            if (!IsRight)
            {
                builder.Append(". Ответ: ");
                builder.Append(RightAnswer);
                builder.Append(';');
            }
            return builder.ToString();
        }
    }
}
