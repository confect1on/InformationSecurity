using System.Collections.Immutable;

namespace InformationSecurity.Alphabets;

public abstract class BaseAlphabet
{
    protected BaseAlphabet()
    {
        AlphabetCharToIndex = GenerateReverseDictionary();
    }

    public abstract string Alphabet { get; }

    public int GetAlphabetIndex(char character)
    {
        return AlphabetCharToIndex[character];
    }

    private ImmutableDictionary<char, int> AlphabetCharToIndex { get; }

    private ImmutableDictionary<char, int> GenerateReverseDictionary()
    {
        return Alphabet.Select((c, i) => new { Character = c, Index = i })
            .ToImmutableDictionary(arg => arg.Character, arg => arg.Index);
    }
}