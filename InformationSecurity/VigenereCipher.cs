using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace InformationSecurity;

public class VigenereCipher
{
    private const string Alphabet = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЬЪЫЭЮЯ_";
    private readonly IImmutableDictionary<char, int> _characterToIndex;

    public VigenereCipher()
    {
        _characterToIndex = Alphabet
            .Select((c, i) => new { Character = c, Index = i })
            .ToImmutableDictionary(arg => arg.Character, arg => arg.Index);
    }
    
    public string Encode(string source, string key)
    {
        var extendedKey = ExtendKey(source.Length, key);
        Debug.Assert(extendedKey.Length == source.Length);
        return string.Join("", source
            .Select((c, i) => EncodeCharacterByTable(c, extendedKey[i])));
    }
    
    public string Decode(string source, string key)
    {
        var extendedKey = ExtendKey(source.Length, key);
        return string.Join("", source
            .Select((c, i) => DecodeCharacterByTable(c, extendedKey[i])));
    }

    private char DecodeCharacterByTable(char cipherChar, char keyChar)
    {
        return Alphabet[(_characterToIndex[cipherChar] - _characterToIndex[keyChar] + Alphabet.Length) % Alphabet.Length];
    }
    
    private char EncodeCharacterByTable(char sourceChar, char keyChar)
    {
        return Alphabet[(_characterToIndex[sourceChar] + _characterToIndex[keyChar]) % Alphabet.Length];;
    }

    private static string ExtendKey(int sourceLength, string key)
    {
        var repeatCount = sourceLength / key.Length;
        var builder = new StringBuilder();
        for (var i = 0; i < repeatCount; i++)
        {
            builder.Append(key);
        }
        
        var extendedKey = builder + key[..(sourceLength - builder.Length)];
        return extendedKey;
    }
    
}