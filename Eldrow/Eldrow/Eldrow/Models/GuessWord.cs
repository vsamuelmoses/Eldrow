using LanguageExt;
using System.Linq;

namespace Eldrow.Models
{
    public record GuessWord
    {
        public GuessWord(Word hiddenWord, Word guessWord)
        {
            HiddenWord = hiddenWord;
            GuessedWord = guessWord;

            Characters = GuessedWord
                .Text.ToCharArray()
                .Select((c, i) =>
                {

                    var hiddenWordChars = hiddenWord.Text.ToCharArray();
                    if (hiddenWordChars[i] == c)
                        return new Character(c, i, CharState.Green);

                    if (hiddenWord.Text.Contains(c))
                        return new Character(c, i, CharState.Yellow);

                    return new Character(c, i, CharState.Red);
                })
                .ToArray();


            Yellows = Characters.Count(c => c.Color == CharState.Yellow);
            Greens = Characters.Count(c => c.Color == CharState.Green);

        }

        public Word HiddenWord { get; }
        public Word GuessedWord { get; }
        public Character[] Characters { get; }

        public int Yellows { get; }
        public int Greens { get; }
    }
}