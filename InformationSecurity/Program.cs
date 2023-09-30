// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using InformationSecurity;
using InformationSecurity.Alphabets;
using InformationSecurity.Ciphers;

Console.OutputEncoding = Encoding.UTF8;
Vigenere();

return;

static void Caesar()
{
    // arrange
    var caesar = new CaesarCipher(new EnglishAlphabet().Alphabet);
    const string textToEncode = "SKORO_PASXA_XRISTOS_VOSKRES_PEKI_KYLICHI_MOLIS";
    const string textToDecode = @"MTSNBTMCISV[HCSUMWXHSCV[XBSDFX^FTGBCXSD_CLCSU\GSUFTJC";

    // act
    var firstEncoded = caesar.Encode(textToEncode, 19);
    var firstDecoded = caesar.Decode(firstEncoded, 19);

    var secondDecoded = caesar.Decode(textToDecode, 19);
    var secondEncoded = caesar.Encode(secondDecoded, 19);

    // assert
    Console.WriteLine($"Encoded: {firstEncoded}");
    Console.WriteLine($"Decoded: {secondDecoded}");
    Debug.Assert(textToEncode == firstDecoded);
    Debug.Assert(textToDecode == secondEncoded);
}

static void Vigenere()
{
    // arrange
    var vigenere = new VigenereCipher(new RussianAlphabet());
    const string textToEncode = "УЖ_Я_МАХНУЛ_НА_ВСЕ_РУКОИ_ТЫ_КАКОИ_ТО";
    const string textToDecode = "ЯШЕГКАБОЫОАЬШЧУЭСОБЪР_ФАЭРЗЬЯФЭЦКНЧЕБМ"; 
    const string key = "СИЛАКРИПТО";

    // act
    var decoded = vigenere.Decode(textToDecode, key);
    var encoded = vigenere.Encode(decoded, key);
    
    // assert
    Console.WriteLine(decoded);
    Debug.Assert(encoded == textToDecode);
}

static string Separate(string source)
{
    return string.Join(" ", source.Chunk(2)
        .Select<char[], string>(x => string.Join("", x)));
}

static void Playfair()
{
    var playfair = new PlayfairCipher();
    const string textToDecode = "ТРЕ=%ЖН8?Н7:КЦТ._ЗН?Г9Ы5=[8НВ1СВ=*С;";
    var decoded = playfair.Decode(textToDecode);
    var encoded = playfair.Encode(decoded);
    Console.WriteLine($"Encoded: {Separate(textToDecode)}");
    Console.WriteLine($"Decoded: {Separate(decoded)}");
    Console.WriteLine($"Encoded: {Separate(encoded)}");
    Console.WriteLine($"Decoded without spaces: {decoded}");
    Debug.Assert(encoded == textToDecode);
}

static void TwoSquare()
{
    const string toDecode = "ХН[ШГ!ИЁЮИ№-Д8ЯЪДШЦ[}ЕЗП}-ЭЕ№ФЦ87М№ЗЭ%Я№+8№-Ч*СФ";
    var twoSquare = new TwoSquareCipher();
    Console.WriteLine(Separate(toDecode));
    var decoded = twoSquare.Decode(toDecode);
    Console.WriteLine(Separate(decoded));
    var encoded = twoSquare.Encode(decoded);
    Console.WriteLine(Separate(encoded));
    Debug.Assert(encoded == toDecode);
    
}

static void Gronsfeld()
{
    var algorithm = new GronsfeldCipher(new EnglishAlphabet());
    const string source = @"AEBENZUVRTMXSVU\LPTQRAKLNYZYVWX^XQX[UMZU";
    var key = new[] { 13, 21, 11, 17, 9 };
    var decoded = algorithm.Decode(source, key);
    Console.WriteLine(decoded);
    var encoded = algorithm.Encode(decoded, key);
    Debug.Assert(encoded == source);
}

static void Loopback()
{
    var gronsfeld = new GronsfeldCipher(new EnglishAlphabet());
    var loopback = new LoopbackCipher(gronsfeld);
    var keys = new[]
    {
        new[] {15, 10, 24, 19, 9},
        new[] {13, 9, 7, 21, 4, 17, 11},
        new[] {22, 12, 16, 25}
    };
    const string source = @"WEGFL\TKM\L[U]LGQGHEAOSO^VMGWXHT\\";
    Console.WriteLine(loopback.Decode(source, keys));
}