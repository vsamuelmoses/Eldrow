using System;

namespace Eldrow.Models
{
    public record GameFinished
    {
        private GameFinished(Word HiddenWord, GuessWord[] GuessedWords, DateTime StartedUtc, DateTime EndUtc)
        {
            this.HiddenWord = HiddenWord;
            this.GuessedWords = GuessedWords;
            this.StartedUtc = StartedUtc;
            this.EndUtc = EndUtc;
        }

        public Word HiddenWord { get; }
        public GuessWord[] GuessedWords { get; }
        public DateTime StartedUtc { get; }
        public DateTime EndUtc { get; }

        public record Won(Word HiddenWord, GuessWord[] GuessedWords, DateTime StartedUtc, DateTime EndUtc)
            : GameFinished(HiddenWord, GuessedWords, StartedUtc, EndUtc);

        public record Lost(Word HiddenWord, GuessWord[] GuessedWords, DateTime StartedUtc, DateTime EndUtc)
            : GameFinished(HiddenWord, GuessedWords, StartedUtc, EndUtc);

        public record Forfieted(Word HiddenWord, GuessWord[] GuessedWords, DateTime StartedUtc, DateTime EndUtc)
            : GameFinished(HiddenWord, GuessedWords, StartedUtc, EndUtc);

    }
}