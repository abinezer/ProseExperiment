using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.Features;

namespace ProseTutorial
{
    public class RankingScore : Feature<double>
    {
        public RankingScore(Grammar grammar) : base(grammar, "Score")
        {
        }

        [FeatureCalculator(nameof(Semantics.Substring))]
        public static double Substring(double v, double start, double end)
        {
            return start * end;
        }

        public static double SubstringNew(double v, double start, double end)
        {
            return start * end;
        }

        [FeatureCalculator(nameof(Semantics.AbsPos))]
        public static double AbsPos(double v, double k)
        {
            return k;
        }

        [FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
        public static double K(int k)
        {
            return 1.0 / Math.Abs(k);
        }

        [FeatureCalculator("constStr", Method = CalculationMethod.FromLiteral)]
        public static double constStr(double k)
        {
            return Math.Abs(k);
        }



        [FeatureCalculator(nameof(Semantics.RelPos))]
        public static double RelPos(double x, double rr) //add double k. 
        {
            return rr;
        }

        [FeatureCalculator(nameof(Semantics.Concat))]
        public static double Concat(double v, double s, double s1)
        {
            return s * s1;
        }

        [FeatureCalculator(nameof(Semantics.Const))]
        public static double Const(double v, double s)
        {
            return s;
        }

        [FeatureCalculator(nameof(Semantics.CharAt))]
        public static double CharAt(double v, double s)
        {
            return 1.0 / Math.Abs(s);
        }

        [FeatureCalculator("rr", Method = CalculationMethod.FromLiteral)]
        public static double RR(Tuple<Regex, Regex> tuple)
        {
            return 1;
        }
    }
}