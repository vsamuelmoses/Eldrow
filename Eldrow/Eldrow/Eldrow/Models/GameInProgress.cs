using System;

namespace Eldrow.Models
{
    public record GameInProgress(Word HiddenWord, GuessWord[] GuessedWords, DateTime StartedUtc);
}