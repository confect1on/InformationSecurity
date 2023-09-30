using System.Diagnostics;
using System.Text;
using InformationSecurity.Alphabets;

namespace InformationSecurity.Ciphers;

public class VigenereCipher
{
    private readonly BaseAlphabet _alphabet;
    
    public VigenereCipher(BaseAlphabet alphabet)
    {
        _alphabet = alphabet;
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
        return _alphabet.Alphabet[(_alphabet.GetAlphabetIndex(cipherChar) - _alphabet.GetAlphabetIndex(keyChar) + _alphabet.Alphabet.Length) % _alphabet.Alphabet.Length];
    }
    
    private char EncodeCharacterByTable(char sourceChar, char keyChar)
    {
        return _alphabet.Alphabet[(_alphabet.GetAlphabetIndex(sourceChar) + _alphabet.GetAlphabetIndex(keyChar)) % _alphabet.Alphabet.Length];
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