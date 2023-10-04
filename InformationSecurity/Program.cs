// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using InformationSecurity;
using InformationSecurity.Alphabets;
using InformationSecurity.Ciphers;

Console.OutputEncoding = Encoding.UTF8;
NumericTheory();

return;

static void NumericTheory()
{
    const int aForGcd = 715;
    const int bForGcd = 195;
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    Algebra.Gcd(aForGcd, bForGcd);
    Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}");
    var binaryStopWatch = new Stopwatch();
    binaryStopWatch.Start();
    var gcd = Algebra.BinaryGcd(aForGcd, bForGcd);
    Console.WriteLine($"Elapsed: {binaryStopWatch.ElapsedMilliseconds}");
    Console.WriteLine($"Great common divisor:{gcd}");
    
    const int x = 10800;
    Console.WriteLine($"Euler function: {Algebra.EulerFunc(x)}");

    const int aForLcm = 744;
    const int bForLcm = 198;
    
    Console.WriteLine($"Large common multiple: {Algebra.Lcm(aForLcm, bForLcm)}");

    const int aForCompare = 25;
    const int bForCompare = -1;
    const int modula = 13;
    const int aRemainder = aForCompare % modula;
    const int bRemainder = bForCompare % modula;
    Console.WriteLine(aRemainder);
    Console.WriteLine(bRemainder);
    Console.WriteLine(aRemainder == bRemainder);

    var aComp = 25;
    var bComp = -1;
    var modComp = 13;
    Console.WriteLine($"{aComp} = {bComp} (mod {modComp}) - {Algebra.IsTrueModulaComparison(aComp, bComp, modComp)}");

    var nForSystem = 9;
    Console.WriteLine($"Complete: {string.Join(',', Algebra.NonNegativeMinSystemOfResidues(nForSystem))} ");
    Console.WriteLine($"Reduced: {string.Join(',', Algebra.ReducedSystemOfResidues(nForSystem))}");

    var aForCalc = 90;
    var degreeForCalc = 42;
    var modulaForCalc = 41;
    Console.WriteLine($"Calculation: {Algebra.OptimizedByDegreeModula(aForCalc, degreeForCalc, modulaForCalc)}");

    var aForEq = 5;
    var bForEq = 3;
    var modForEq = 11;
    Console.WriteLine($"{aForEq} * x = {bForEq} (mod {modForEq})");
    Console.WriteLine($"x = {string.Join(',', Algebra.ModulaEquationLinear(aForEq, bForEq, modForEq))}");

    var aForResudial = -4;
    var modForResudial = 3;
    
    Console.WriteLine($"Absolute min residuals for {modForResudial}: {string.Join(',', Algebra.AbsoluteMinSystemOfResidues(modForResudial))}");
    Console.WriteLine($"Min absolute residual for {aForResudial} mod {modForResudial} : {aForResudial % modForResudial}");
    Console.WriteLine($"Min non-negative residuals for {modForResudial}: {string.Join(',', Algebra.NonNegativeMinSystemOfResidues(modForResudial))}");
    Console.WriteLine($"Min non-negative residual for {aForResudial} mod {modForResudial} : {Algebra.Modula(aForResudial, modForResudial)}");
}


static void Canonical()
{
    var x = 17;
    var a = Algebra.CanonicalRepresentation(x);
    foreach (var (multiplier, degree) in a)
    {
        Console.WriteLine($"{multiplier} in {degree} degree");
    }
}


static void Caesar()
{
    // arrange
    var caesar = new CaesarCipher(new EnglishAlphabet().Alphabet);
    const string textToEncode = "SKORO_PASXA_XRISTOS_VOSKRES_PEKI_KYLICHI_MOLIS";
    const string textToDecode = @"MTSNBTMCISV[HCSUMWXHSCV[XBSDFX^FTGBCXSD_CLCSU\GSUFTJC";

    // act
    var firstEncoded = caesar.Enciphering(textToEncode, 19);
    var firstDecoded = caesar.Deciphering(firstEncoded, 19);

    var secondDecoded = caesar.Deciphering(textToDecode, 19);
    var secondEncoded = caesar.Enciphering(secondDecoded, 19);

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
    var decoded = vigenere.Deciphering(textToDecode, key);
    var encoded = vigenere.Enciphering(decoded, key);
    
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
    var decoded = playfair.Deciphering(textToDecode);
    var encoded = playfair.Enciphering(decoded);
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
    var decoded = twoSquare.Deciphering(toDecode);
    Console.WriteLine(Separate(decoded));
    var encoded = twoSquare.Enciphering(decoded);
    Console.WriteLine(Separate(encoded));
    Debug.Assert(encoded == toDecode);
    
}

static void Gronsfeld()
{
    var algorithm = new GronsfeldCipher(new EnglishAlphabet());
    const string source = @"AEBENZUVRTMXSVU\LPTQRAKLNYZYVWX^XQX[UMZU";
    var key = new[] { 13, 21, 11, 17, 9 };
    var decoded = algorithm.Deciphering(source, key);
    Console.WriteLine(decoded);
    var encoded = algorithm.Enciphering(decoded, key);
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
    Console.WriteLine(loopback.Deciphering(source, keys));
}