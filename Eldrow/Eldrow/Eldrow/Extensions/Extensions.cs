using Eldrow.Models;
using Eldrow.ViewModels;
using LanguageExt;
using LanguageExt.Common;
using System;
using System.Linq;

namespace Eldrow.Extensions
{
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
}