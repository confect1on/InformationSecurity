using System.Diagnostics;
using System.Text;

namespace InformationSecurity.Ciphers;

public class TwoSquareCipher
{
    private readonly char[,] _firstSquare =
    {
        { 'Р', 'Е', 'С', '.', 'Щ', 'У', '/' },
        { '7', '1', 'К', 'И', ':', 'Л', 'Б' },
        { 'А', '*', '!', '-', '4', ';', '№' },
        { 'Д', '3', 'Ё', '5', 'Ю', ']', 'Й'},
        
        { '(', 'Ф', 'Я', 'З', 'Ч', '2', 'Ж'},
        { 'П', 'Т', '[', 'М', '0', 'Х', '6'},
        { '}', ',', 'Ь', '{', 'Э', '+', ')'},
        { 'Ъ', 'Ш', '8', 'О', 'Г', '9', 'Ы'},
        { '=', 'В', '_', 'Н', '?', 'Ц', '%'},
    };

    private readonly IReadOnlyDictionary<char, Tuple<int, int>> _charToIndexesFirstSquare;

    private readonly char[,] _secondSquare =
    {
        { '[', 'З', 'А', '(', 'И', '5', 'Д' },
        { 'Й', '6', 'Ъ', 'Р', 'Ш', 'Ф', ':' },
        { 'Я', 'О', '_', 'Ж', '3', '8', 'Л' },
        { 'Ё', '?', '9', '.', 'К', 'Щ', ']'},
        { '!', ',', 'Е', '1', '2', '*', 'М'},
        { 'С', 'Ы', '}', '4', 'В', ')', '='},
        { '{', ';', 'Г', '0', '%', 'У', '+'},
        { 'Н', 'Х', '№', 'Ю', '7', 'Ь', 'Т'},
        { 'Ц', 'Ч', 'Э', 'Б', '-', '/', 'П'},
    };

    private readonly IReadOnlyDictionary<char, Tuple<int, int>> _charToIndexesSecondSquare;

    private const char SeparateSymbol = 'Ъ';

    public TwoSquareCipher()
    {
        _charToIndexesFirstSquare = GenerateCharToIndexesMapping(_firstSquare);
        _charToIndexesSecondSquare = GenerateCharToIndexesMapping(_secondSquare);
    }
    
    public string Enciphering(string source)
    {
        var builder = new StringBuilder();
        foreach (var (a, b) in SplitByBigrams(source))
        {
            var (aI, aJ) = _charToIndexesFirstSquare[a];
            var (bI, bJ) = _charToIndexesSecondSquare[b];

            char encodedA;
            char encodedB;
            if (aI != bI)
            {
                encodedA = _secondSquare[aI, bJ];
                encodedB = _firstSquare[bI, aJ];
            }
            else
            {
                encodedA = _secondSquare[aI, aJ];
                encodedB = _firstSquare[bI, bJ];
            }
            

            builder.Append(encodedA);
            builder.Append(encodedB);
        }

        return builder.ToString();
    }

    public string Deciphering(string cipher)
    {
        Debug.Assert(cipher.Length % 2 == 0);
        var builder = new StringBuilder();
        foreach (var (a, b) in cipher.Chunk(2).Select(x => Tuple.Create(x[0], x[1])))
        {
            var (aI, aJ) = _charToIndexesSecondSquare[a];
            var (bI, bJ) = _charToIndexesFirstSquare[b];

            char encodedA;
            char encodedB;
            if (aI != bI)
            {
                encodedA = _firstSquare[aI, bJ];
                encodedB = _secondSquare[bI, aJ];
            }
            else
            {
                encodedA = _firstSquare[aI, aJ];
                encodedB = _secondSquare[bI, bJ];
            }
            

            builder.Append(encodedA);
            builder.Append(encodedB);
        }

        return builder.ToString();
    }
    
    private static IEnumerable<Tuple<char, char>> SplitByBigrams(string cipher)
    {
        var builder = new StringBuilder();
        builder.Append(cipher[0]);
        for (var i = 1; i < cipher.Length; i++)
        {
            if (cipher[i] == builder[^1] && builder.Length % 2 == 1)
            {
                builder.Append(SeparateSymbol);
            }
            builder.Append(cipher[i]);
        }

        if (builder.Length % 2 == 1)
        {
            builder.Append(SeparateSymbol);
        }

        return builder
            .ToString()
            .Chunk(2)
            .Select(x => new Tuple<char, char>(x[0], x[1]));
    }

    private IReadOnlyDictionary<char, Tuple<int, int>> GenerateCharToIndexesMapping(char[,] chars)
    {
        var charToIndexPair = new Dictionary<char, Tuple<int, int>>(_firstSquare.Length);
        for (var i = 0; i < chars.GetLength(0); i++)
        {
            for (var j = 0; j < chars.GetLength(1); j++)
            {
                charToIndexPair[chars[i, j]] = new Tuple<int, int>(i, j);
            }
        }

        return charToIndexPair;
    }
}