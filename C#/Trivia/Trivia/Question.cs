using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    class Question
    {
        public QuestionType Type { get; set; }
        public string QuestionBody { get; set; }
    }

    enum QuestionType
    {
        Pop = 0,
        Science = 1,
        Sports = 2,
        Rock = 3
    }
}
