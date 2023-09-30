using System.Collections.Immutable;

namespace InformationSecurity.Ciphers;

public class CaesarCipher 
{
    private readonly string _alphabet;
    private readonly ImmutableDictionary<char, int> _characterToAlphabetIndex;

    public CaesarCipher(string alphabet)
    {
        _alphabet = alphabet;
        _characterToAlphabetIndex = alphabet
            .Select((c, i) => new { Item = c, Index = i })
            .ToImmutableDictionary(arg => arg.Item, arg => arg.Index);
    }
    public string Encode(string source, int offset)
    {
        return string.Join("", source
            .Select(character => _alphabet[(_characterToAlphabetIndex[character] + offset) % _alphabet.Length]));
    }

    public string Decode(string source, int offset)
    {
        return string.Join("",
            source.Select(character =>
                _alphabet[(_characterToAlphabetIndex[character] + _alphabet.Length - offset) % _alphabet.Length]));
    }
}