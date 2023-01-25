namespace CodeWars.Tests;

public static class KataTrainer
{
    #region Tribonacci Sequence 
    
    //https://www.codewars.com/kata/556deca17c58da83c00002db

    public static double[] Tribonacci(double[] signature, int n)
    {
        if (n == 0) return Array.Empty<double>();

        if (n < signature.Length) return signature[..n];

        var res = new double[n];
        signature.CopyTo(res, 0);

        for (var i = signature.Length; i < n; i++)
        {
            res[i] = res[i - 1] + res[i - 2] + res[i - 3];
        }

        return res;
    }    

    #endregion

    #region Persistent Bugger.

    // https://www.codewars.com/kata/55bf01e5a717a0d57e0000ec
    public static int Persistence(long n)
    {
        var counter = 0;

        while (n / 10 > 0)
        {
            counter++;
            
            n = MultiplyDigitsInInteger(n);
        }
        
        return counter;
    }

    private static int MultiplyDigitsInInteger(long n)
    {
        var splittedNumber = n.ToString()
            .ToCharArray()
            .Select(it => int.Parse(it.ToString()));

        var mul = splittedNumber.Aggregate((a, x) => a * x);
        return mul;
    }    

    #endregion

    #region Highest Scoring Word

    // https://www.codewars.com/kata/57eb8fcdf670e99d9b000272
    public static string High(string s)
    {
        var words = s.Split(' ').ToList();
        var scores = words.Select(GetScore).ToList();

        return words[scores.IndexOf(scores.Max())];
    }

    private static int GetScore(string word) =>
        word.Select(it => it % 32).Sum();
    
    #endregion
    
    #region First non-repeating character
    
    // https://www.codewars.com/kata/52bc74d4ac05d0945d00054e
    
    public static string FirstNonRepeatingLetter(string s)
    {
        var chars = s.ToCharArray().ToList();
        var mapped = chars.Select(it => new KeyValuePair<char, int>(it, 1));
        var reduced = mapped.GroupBy(p => char.ToLower(p.Key))
            .Select(g => new
            {
                Char = g.Key,
                Count = g.Count(),
                Position = chars.IndexOf(g.Key) == -1 ? chars.IndexOf(char.ToUpper(g.Key)) : chars.IndexOf(g.Key)
            }).OrderBy(it => it.Position);

        var firstNonRepeatedPosition = reduced.FirstOrDefault(it => it.Count == 1);
        return firstNonRepeatedPosition == null ? string.Empty : s[firstNonRepeatedPosition.Position].ToString();
    }    

    #endregion

    #region Counting Duplicates
    
    // https://www.codewars.com/kata/54bf1c2cd5b56cc47f0007a1

    public static int DuplicateCount(string str) => 
        str.ToLower()
            .GroupBy(it => it)
            .Select(it => it.Count())
            .Count(it => it > 1);    

    #endregion

    #region Rectangle into Squares
    
    // https://www.codewars.com/kata/55466989aeecab5aac00003e

    public static List<int> SqInRect(int lng, int wdth)
    {
        if (wdth == lng) return null!;
        var res = new List<int>();
        
        while (lng != 0 && wdth != 0)
        {
            if (wdth > lng) (wdth, lng) = (lng, wdth);

            var delta = lng - wdth;

            var sqSide = lng - delta;
            res.Add(sqSide);
            lng -= sqSide;
        }

        return res;
    }    

    #endregion

    
}