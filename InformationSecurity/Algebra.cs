namespace InformationSecurity;

public record CanonicalMultiplier(int Multiplier, int Degree)
{
    public int Degree { get; set; } = Degree;
    
    public void Deconstruct(out int multiplier, out int degree)
    {
        multiplier = Multiplier;
        degree = Degree;
    }
}


public static class Algebra
{
    public static int Gcd(int a, int b)
    {
        var k = 0;
        if (a < b)
        {
            (a, b) = (b, a);
        }
        while (b != 0)
        {
            ++k;
            (a, b) = (b, a % b);
        }
        Console.WriteLine($"Gcd iteration's count: {k}");
        return a;
    }

    public static int BinaryGcd(int a, int b)
    {
        if (a == b && a == 0)
        {
            return a;
        }

        if (a == 0)
        {
            return b;
        }

        var bothEvenCount = 0;
        var k = 0;
        while (b != 0)
        {
            ++k;
            var aIsEven = false;
            var bIsEven = false;
            if (a % 2 == 0)
            {
                aIsEven = true;
                a /= 2;
            }
            if (b % 2 == 0)
            {
                bIsEven = true;
                b /= 2;
            }

            if (aIsEven && bIsEven)
            {
                ++bothEvenCount;
            };
            if (!aIsEven && !bIsEven)
            {
                if (a < b)
                {
                    (a, b) = (b, a);
                }

                (a, b) = (b, (a - b) / 2);
            }
            
        }

        for (var i = 0; i < bothEvenCount; i++)
        {
            a *= 2;
        }

        Console.WriteLine($"Binary gcd iteration's count: {k}");
        return a;
    }

    public static int Lcm(int a, int b)
    {
        return (a * b) / BinaryGcd(a, b);
    }

    public static IReadOnlyCollection<CanonicalMultiplier> CanonicalRepresentation(int x)
    {
        var canonical = new List<CanonicalMultiplier>();
        var originalX = x;
        for (var i = 2; i * i <= originalX; i++)
        {
            if (x % i != 0)
            {
                continue;
            }
            var multiplier = new CanonicalMultiplier(i, 0);
            while (x % i == 0)
            {
                multiplier.Degree++;
                x /= i;
            }
            canonical.Add(multiplier);
        }
        return canonical;
    }

    public static int EulerFunc(int n)
    {
        var result = n;
        for (var i = 2; i * i <= n; ++i)
        {
            if (n % i != 0)
            {
                continue;
            }
            while (n % i == 0)
            {
                n /= i;
            }

            result -= result / i;
        }

        if (n > 1)
            result -= result / n;
        return result;
    }
}