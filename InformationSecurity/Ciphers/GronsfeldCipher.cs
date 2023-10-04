using System.Diagnostics;
using System.Text;
using InformationSecurity.Alphabets;

namespace InformationSecurity.Ciphers;

public class GronsfeldCipher
{
    private readonly BaseAlphabet _alphabet;

    public GronsfeldCipher(BaseAlphabet alphabet)
    {
        _alphabet = alphabet;
    }
    public string Enciphering(string source, int[] key)
    {
        var extendKey = ExtendKeyCyclically(key, source.Length);
        var stringBuilder = new StringBuilder(source.Length);
        foreach (var (cipherChar, keyValue) in source.Zip(extendKey))
        {
            var cipherCharacterIndex = (_alphabet.GetAlphabetIndex(cipherChar) + keyValue) % _alphabet.Alphabet.Length;
            stringBuilder.Append(_alphabet.Alphabet[cipherCharacterIndex]);
        }

        return stringBuilder.ToString();
    }

    public string Deciphering(string cipher, int[] key)
    {
        var extendKey = ExtendKeyCyclically(key, cipher.Length);
        Debug.Assert(extendKey.Length == cipher.Length);
        var stringBuilder = new StringBuilder(cipher.Length);
        foreach (var (cipherChar, keyValue) in cipher.Zip(extendKey))
        {
            var cipherCharIndex = _alphabet.GetAlphabetIndex(cipherChar);
            var sourceCharIndex = (_alphabet.Alphabet.Length + cipherCharIndex - keyValue) % _alphabet.Alphabet.Length;
            stringBuilder.Append(_alphabet.Alphabet[sourceCharIndex]);
        }

        return stringBuilder.ToString();
    }

    private static int[] ExtendKeyCyclically(IReadOnlyList<int> key, int lenghtToExtend)
    {
        var extendedKey = new List<int>(lenghtToExtend);
        for (var i = 0; i < lenghtToExtend / key.Count; ++i)
        {
            extendedKey.AddRange(key);
        }

        for (var i = 0; i < lenghtToExtend % key.Count; ++i)
        {
            extendedKey.Add(key[i]);
        }

        var arr = extendedKey.ToArray();
        return extendedKey.ToArray();
    }
}