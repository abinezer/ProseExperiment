using System;
using System.Text.RegularExpressions;

namespace ProseTutorial
{
    public static class Semantics
    {
        public static string Substring(string v, int start, int end)
        {
            return v.Substring(start, end - start);
        }

        public static int? AbsPos(string v, int k)
        {
            return k > 0 ? k - 1 : v.Length + k + 1;
        }

        public static string Concat(string v, string s1, string s2)
        {
            return s1 + s2;
        }

        public static string CharAt(string v, int s)
        {
            return v[s].ToString();
        }

        public static int? RelPos(string v, Tuple<Regex, Regex> rr)
        {
            Regex left = rr.Item1;
            Regex right = rr.Item2;
            MatchCollection rightMatches = right.Matches(v);
            MatchCollection leftMatches = left.Matches(v);
            foreach (Match leftMatch in leftMatches)
                foreach (Match rightMatch in rightMatches)
                    if (rightMatch.Index == leftMatch.Index + leftMatch.Length)
                        return leftMatch.Index + leftMatch.Length;
            return null;
        }

        //public static int? RelPos(string v, Tuple<Regex, Regex> rr, int k) // add the parameter for kth instance of rr
        //{
        //    //modify for kth instance of rr
        //    Regex left = rr.Item1;
        //    Regex right = rr.Item2;
        //    MatchCollection rightMatches = right.Matches(v);
        //    MatchCollection leftMatches = left.Matches(v);
        //    if (k == 0)
        //        return null;
        //    int TotalCount = 0;
        //    if (k < 0)
        //    {
        //      foreach (Match leftMatch in leftMatches)
        //            foreach (Match rightMatch in rightMatches)
        //                if (rightMatch.Index == leftMatch.Index + leftMatch.Length)
        //                    TotalCount++;
        //        k = TotalCount + k + 1;
        //    }
        //    TotalCount = 0;
        //    foreach (Match leftMatch in leftMatches)
        //          foreach (Match rightMatch in rightMatches)
        //              if (rightMatch.Index == leftMatch.Index + leftMatch.Length)
        //              {
        //                  TotalCount++;
        //                  if (TotalCount == k)
        //                      return leftMatch.Index + leftMatch.Length;
        //              }
        //    return null;
        //}
    }
}