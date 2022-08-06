using LanguageExt;
using System;
using System.Linq;

namespace Eldrow.Models
{
    public static class GamePlay
    {
        public static bool IsInDictionary(string word)
            => true;

        public static GameInProgress Start(this Word hiddenWord)
            => new GameInProgress(hiddenWord, new GuessWord[] { }, DateTime.UtcNow);

        public static Either<GameFinished, GameInProgress> Play(this GameInProgress game, Word word)
        {
            var guessWord = new GuessWord(game.HiddenWord, word);

            var previousGuessedWords = game.GuessedWords.ToList();
            previousGuessedWords.Insert(0, guessWord);
            var allGuessedWords = previousGuessedWords.ToArray();

            if (guessWord.GuessedWord.Text == guessWord.HiddenWord.Text)
                return Either<GameFinished, GameInProgress>.Left(
                    new GameFinished.Won(game.HiddenWord, allGuessedWords, game.StartedUtc, DateTime.UtcNow));

            return Either<GameFinished, GameInProgress>.Right(
                new GameInProgress(game.HiddenWord, allGuessedWords, game.StartedUtc));
        }
    }
}