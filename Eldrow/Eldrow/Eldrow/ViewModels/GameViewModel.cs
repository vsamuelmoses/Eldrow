using Eldrow.Extensions;
using Eldrow.Models;
using Eldrow.Services;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eldrow.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private GameInProgress play;

        public GameInProgress Play 
        { 
            get => play; 
            set => Set(ref play, value);
        }
        
        public GameViewModel(IDialogs dialogs, Word word)
        {
            Title = "About";

            Play = word.Start();

            GoCommand = new Command(() =>
            {

                Play = Guess
                .ToWord(GamePlay.IsInDictionary)
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