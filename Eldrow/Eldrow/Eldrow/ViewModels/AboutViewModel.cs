using LanguageExt;
using LanguageExt.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eldrow.ViewModels
{
    public static class FunctionalExtensions
    {
        public static Result<T> Join<T>(this Result<Result<T>> nested)
            => nested.Match(_ => _, e => new Result<T>(e));

        public static Result<TOut> MapJoin<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> map)
            => result.Map(map).Join();
    }

    public static class Extensions
    {
        public static Result<string> IsExpectedLength(this string word)
        {
            return string.IsNullOrEmpty(word) || word.Length < 4
                ? new Result<string>(new EldrowException("Word should be atleast 4 characters long"))
                : word;
        }

        public static Result<string> IsNotHaveDuplicateCharacters(this string word)
        {
            return string.IsNullOrEmpty(word) || word.Length == word.ToCharArray().Distinct().Count()
                ? new Result<string>(new EldrowException("Word should not contain duplicate characters"))
                : word;
        }

        public static Result<string> IsInDictionary(this string word, Func<string, bool> languageDictionary)
        {
            return string.IsNullOrWhiteSpace(word) || !languageDictionary(word)
                ? new Result<string>(new EldrowException("Word doesn't exist"))
                : word;
        }

        public static Result<Word> ToWord(this string word, Func<string, bool> languageDictionary)
        {
            return word
                .IsExpectedLength()
                .MapJoin(w => w.IsInDictionary(languageDictionary))
                .MapJoin(w => Word.Create(w));
        }
    }
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

    public enum CharState
    {
        Unknown,
        Red,
        Yellow,
        Green
    }

    public record Character(char Val, int Position, CharState Color);

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

    public record GameInProgress (Word HiddenWord, GuessWord[] GuessedWords,  DateTime StartedUtc);

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

    public static class Game
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

    public interface IDialogs
    {
        Task ShowMessageAsync(string title, string message);
    }

    public class AboutViewModel : BaseViewModel
    {
        private GameInProgress play;

        public GameInProgress Play 
        { 
            get => play; 
            set => Set(ref play, value);
        }
        
        public AboutViewModel(IDialogs dialogs, Word word)
        {
            Title = "About";

            Play = word.Start();

            GoCommand = new Command(() =>
            {

                Play = Guess
                .ToWord(Game.IsInDictionary)
                .Match(guessedWord =>
                {
                    return Play
                            .Play(guessedWord)
                            .Match(
                                inProgress => inProgress,
                                finished => {

                                    dialogs.ShowMessageAsync("", "You Won!");
                                    return Play;
                                });
                },
                    exception => Play);
            });
        }

        public string Guess { get; set; }
        public ICommand GoCommand { get; }
    }

    public class EldrowException : Exception
    {
        public EldrowException(string message)
            : base(message)
        {
        }
    }
}