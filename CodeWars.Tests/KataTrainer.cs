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

    #region Find the unique number
    
    // https://www.codewars.com/kata/585d7d5adb20cf33cb000235/train/csharp
    
    public static int GetUnique(IEnumerable<int> numbers) =>
        numbers.GroupBy(it => it).Select(it => new
        {
            it.Key,
            Count = it.Count()
        }).First(it => it.Count == 1).Key;

    #endregion

    #region RGB To Hex Conversion
    
    // https://www.codewars.com/kata/513e08acc600c94f01000001/train/csharp
    
    public static string Rgb(int r, int g, int b) => $"{r.RoundToRange():x2}{g.RoundToRange():x2}{b.RoundToRange():x2}".ToUpper();

    private static int RoundToRange(this int num, int start = 0, int end = 255) => num > end ? end : num < start ? start : num;

    #endregion

    #region PaginationHelper

    // https://www.codewars.com/kata/515bb423de843ea99400000a/train/csharp
    
    public class PagnationHelper<T>
    {
        // TODO: Complete this class

        private readonly IList<T> _collection;
        private readonly int _itemsPerPage;

        /// <summary>
        /// Constructor, takes in a list of items and the number of items that fit within a single page
        /// </summary>
        /// <param name="collection">A list of items</param>
        /// <param name="itemsPerPage">The number of items that fit within a single page</param>
        public PagnationHelper(IList<T> collection, int itemsPerPage)
        {
            _collection = collection;
            _itemsPerPage = itemsPerPage;
        }

        /// <summary>
        /// The number of items within the collection
        /// </summary>
        public int ItemCount => _collection.Count;

        /// <summary>
        /// The number of pages
        /// </summary>
        public int PageCount => (int)Math.Ceiling(ItemCount / (double)_itemsPerPage);

        /// <summary>
        /// Returns the number of items in the page at the given page index 
        /// </summary>
        /// <param name="pageIndex">The zero-based page index to get the number of items for</param>
        /// <returns>The number of items on the specified page or -1 for pageIndex values that are out of range</returns>
        public int PageItemCount(int pageIndex) =>
            !(pageIndex >= PageCount || pageIndex < 0) 
                ? _collection.Skip(pageIndex * _itemsPerPage).Take(_itemsPerPage).Count()
                : -1;

        /// <summary>
        /// Returns the page index of the page containing the item at the given item index.
        /// </summary>
        /// <param name="itemIndex">The zero-based index of the item to get the pageIndex for</param>
        /// <returns>The zero-based page index of the page containing the item at the given item index or -1 if the item index is out of range</returns>
        public int PageIndex(int itemIndex)
        {
            if (ItemCount <= itemIndex || itemIndex < 0) return -1;

            var element = _collection[itemIndex];
            var page = 0;
            while (!PageContainsElement(page, element)) page++;

            return page;
        }

        /// <summary>
        /// Gets slice of original collection by pageIndex
        /// </summary>
        /// <param name="pageIndex">Zero-based page index</param>
        /// <returns>Page of original collection</returns>
        private IList<T> GetPage(int pageIndex) =>
            _collection.Skip(pageIndex * _itemsPerPage).Take(_itemsPerPage).ToList();

        /// <summary>
        /// Indicates is element of collection is on passed page
        /// </summary>
        /// <param name="pageIndex">Page</param>
        /// <param name="element">Element of original collection</param>
        /// <returns>Boolean value indicates is passed element on passed page</returns>
        private bool PageContainsElement(int pageIndex, T element) => GetPage(pageIndex).Contains(element);
    }

    #endregion

    #region Valid Parentheses
    
    // https://www.codewars.com/kata/52774a314c2333f0a7000688

    public static bool ValidParentheses(string input)
    {
        if (input.Length == 0) return true;
        var clearString = string.Join("", input.Where(it => it is ')' or '('));

        var replaced = clearString.Replace("()", "");

        return clearString.Length != replaced.Length && ValidParentheses(replaced);
    }

    #endregion
}