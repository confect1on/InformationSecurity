using System.Diagnostics;
using System.Text;

namespace InformationSecurity.Ciphers;

public class PlayfairCipher
{
    private readonly char[,] playfairMatrix =
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

    private readonly Dictionary<char, Tuple<int, int>> _charToIndexPair;
    public PlayfairCipher()
    {
        _charToIndexPair = new Dictionary<char, Tuple<int, int>>(playfairMatrix.Length);
        for (var i = 0; i < playfairMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < playfairMatrix.GetLength(1); j++)
            {
                _charToIndexPair[playfairMatrix[i, j]] = new Tuple<int, int>(i, j);
            }
        }
    }
    
    public string Encode(string source)
    {
        var stringLength = playfairMatrix.GetLength(1);
        var columnLenght = playfairMatrix.GetLength(0);
        var builder = new StringBuilder();
        foreach (var (a, b) in SplitByBigrams(source))
        {
            var (aI, aJ) = _charToIndexPair[a];
            var (bI, bJ) = _charToIndexPair[b];
            char encodedA;
            char encodedB;
            if (aI != bI && aJ != bJ)
            {
                encodedA = playfairMatrix[aI, bJ];
                encodedB = playfairMatrix[bI, aJ];
            }
            else if (aI == bI && aJ != bJ)
            {
                encodedA = playfairMatrix[aI, (aJ + 1) % stringLength];
                encodedB = playfairMatrix[bI, (bJ + 1) % stringLength];
            }
            else if (aI != bI && aJ == bJ)
            {
                encodedA = playfairMatrix[(aI + 1) % columnLenght, aJ];
                encodedB = playfairMatrix[(bI + 1) % columnLenght, bJ];
            }
            else
            {
                throw new InvalidOperationException("Invariant violated: every bigram mush have non-equal symbols.");
            }
            

            builder.Append(encodedA);
            builder.Append(encodedB);
        }

        return builder.ToString();
    }

    private const char SeparateSymbol = 'Ъ';
    public string Decode(string cipher)
    {
        Debug.Assert(cipher.Length % 2 == 0);
        var stringLength = playfairMatrix.GetLength(1);
        var columnLenght = playfairMatrix.GetLength(0);
        var builder = new StringBuilder();
        foreach (var (a, b) in cipher.Chunk(2).Select(x => Tuple.Create(x[0], x[1])))
        {
            var (aI, aJ) = _charToIndexPair[a];
            var (bI, bJ) = _charToIndexPair[b];
            char decodedA;
            char decodedB;
            
            if (aI != bI && aJ != bJ)
            {
                decodedA = playfairMatrix[aI, bJ];
                decodedB = playfairMatrix[bI, aJ];
            }
            else if (aJ != bJ)
            {
                decodedA = playfairMatrix[aI, (stringLength + aJ - 1) % stringLength];
                decodedB = playfairMatrix[bI, (stringLength + bJ - 1) % stringLength];
            }
            else if (aI != bI)
            {
                decodedA = playfairMatrix[(columnLenght + aI - 1) % columnLenght, aJ];
                decodedB = playfairMatrix[(columnLenght + bI - 1) % columnLenght, bJ];
            }
            else
            {
                throw new InvalidOperationException("Invariant violated: every bigram mush have non-equal symbols.");
            }
            

            builder.Append(decodedA);
            builder.Append(decodedB);

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
}