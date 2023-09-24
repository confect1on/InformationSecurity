// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using System.Text.Unicode;
using InformationSecurity;

Console.OutputEncoding = Encoding.UTF8;
Playfair();

return;

static void Caesar()
{
    // arrange
    var caesar = new CaesarCipher(@"ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_");
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
    var vigenere = new VigenereCipher();
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

static void Playfair()
{
    string Separate(string source)
    {
        return string.Join(" ", source.Chunk(2)
            .Select<char[], string>(x => string.Join("", x)));
    }
    
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