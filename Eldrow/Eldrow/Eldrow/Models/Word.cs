using Eldrow.ViewModels;
using LanguageExt.Common;

namespace Eldrow.Models
{
    public record Word
    {
        private Word(string word)
        {
            Text = word;
            Length = Text.Length();
        }

        public string Text { get; }
        public int Length { get; }

        public static Result<Word> Create(string word)
        {
            return string.IsNullOrEmpty(word) || word.Length < 4
                ? new Result<Word>(new EldrowException("Word should be atleast 4 characters long"))
                : new Word(word.ToUpper());
        }
    }
}