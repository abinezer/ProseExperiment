using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace ProseTutorial
{
	public class WitnessFunctions : DomainLearningLogic
	{
		public WitnessFunctions(Grammar grammar) : base(grammar)
		{
		}

		// We will use this set of regular expressions in this tutorial 
		public static Regex[] UsefulRegexes =
		{
			new Regex(@"\w+"), // Word
			new Regex(@"\d+"), // Number
			new Regex(@"\s+"), // Space
			new Regex(@".+"), // Anything
			new Regex(@"$") // End of line
		};

		[WitnessFunction(nameof(Semantics.CharAt), 1)]
		public ExampleSpec WitnessCharAt(GrammarRule rule, ExampleSpec spec)
		{
			var result = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> example in spec.Examples)
			{
				State inputState = example.Key;
				string input = inputState[rule.Body[0]] as string;
				string output = example.Value as string;
				output = output[0].ToString();
				int pos = input.IndexOf(output);
				result[inputState] = pos;
			}
			return new ExampleSpec(result);
		}

		[WitnessFunction(nameof(Semantics.Const), 1)]
		public ExampleSpec WitnessConst(GrammarRule rule, ExampleSpec spec)
		{
			var result = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> example in spec.Examples)
			{
				State inputState = example.Key;
				string input = inputState[rule.Body[0]] as string;
				string output = example.Value as string;
				var occurrences = new List<string>();

				for (int i = 0; i <= input.Length; i++)
				{
					string s = input.Substring(i, i + 1);
					occurrences.Add(s);
				}

				if (occurrences.Count == 0) return null;
				result[inputState] = occurrences.Cast<object>();
			}
			return new ExampleSpec(result);
		}

		//[WitnessFunction(nameof(Semantics.Concat2), 0)]
		//public ExampleSpec WitnessConcatNew1(GrammarRule rule, ExampleSpec spec)
		//      {
		//	var result = new Dictionary<State, object>();
		//	foreach (KeyValuePair<State, object> example in spec.Examples)
		//	{
		//		State inputState = example.Key;
		//		string input = inputState[rule.Body[0]] as string;
		//		string output = example.Value as string;

		//		string endStr = input.Split(" ")[0];
		//		int endPos = output.IndexOf(endStr);
		//		if(endPos > 0)
		//              {
		//			string sep = output.Substring(1, endPos - 1);
		//			result[inputState] = sep;
		//		}
		//              else
		//              {
		//			return null;
		//              }
		//		//output = output[0].ToString();
		//		//int pos = input.IndexOf(output);

		//	}
		//	return new ExampleSpec(result);

		//}

		//[WitnessFunction(nameof(Semantics.Concat2), 1)]
		//public ExampleSpec WitnessConcatNew2(GrammarRule rule, ExampleSpec spec)
		//{
		//	var result = new Dictionary<State, object>();
		//	foreach (KeyValuePair<State, object> example in spec.Examples)
		//	{
		//		State inputState = example.Key;
		//		string input = inputState[rule.Body[0]] as string;
		//		string output = example.Value as string;

		//		string endStr = input.Split(" ")[0];
		//		//int pos = input.IndexOf(output);
		//		result[inputState] = endStr;
		//	}
		//	return new ExampleSpec(result);

		//}

		[WitnessFunction(nameof(Semantics.Concat), 1)]
		public ExampleSpec WitnessConcat1(GrammarRule rule, ExampleSpec spec)
		{
			var result = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> example in spec.Examples)
			{
				State inputState = example.Key;
				string input = inputState[rule.Body[0]] as string;
                //string output = example.Value as string;
                //output = output[0].ToString();

                var occurrences = new List<string>();

                for (int i = 0; i < input.Length; i++)
                {
                    string s = input.Substring(0, i + 1);
                    occurrences.Add(s);
                }
                if (occurrences.Count == 0) return null;
				result[inputState] = occurrences.Cast<object>();
			}
			return new ExampleSpec(result);
		}

		//[WitnessFunction(nameof(Semantics.Concat), 2, DependsOnParameters = new[] { 1 })]
		//public ExampleSpec WitnessConcat2(GrammarRule rule, ExampleSpec spec, ExampleSpec spec1)
		//{

		//	var result = new Dictionary<State, object>();

		//	foreach (KeyValuePair<State, object> example in spec.Examples)
		//	{
		//		State inputState = example.Key;
		//		string input = inputState[rule.Body[0]] as string;
		//		string output = example.Value as string;
  //              output = output.Substring(1);
  //              var occurrences = new List<string>();
		//		Console.WriteLine("input: {0}", input);
		//		Console.WriteLine("output: {0}", output);
  //              //if input is the starting of output:
  //              //return remaining part of output
  //              //else return null;
  //              if (input.IndexOf(output) == 0)
  //              {
  //                  int index = input.IndexOf(output);
  //                  int indexAfter = index + output.Length;
  //                  string s1 = input.Substring(index, indexAfter);
  //                  //occurrences.Add(s1);
  //                  result[inputState] = s1;
  //                  //occurrences.Cast<object>();
  //              }
  //              else
  //              {
  //                  return null;
  //              }
  //              if (occurrences.Count == 0) return null;
  //              //result[inputState] = occurrences.Cast<object>();
  //          }
		//	return new ExampleSpec(result);
		//}


        //keep the below 2 classes
        //[WitnessFunction(nameof(Semantics.Concat), 1)]
        //public DisjunctiveExamplesSpec WitnessConcat1(GrammarRule rule, ExampleSpec spec)
        //{
        //	var result = new Dictionary<State, IEnumerable<object>>();
        //	foreach (KeyValuePair<State, object> example in spec.Examples)
        //	{
        //		State inputState = example.Key;
        //		string input = inputState[rule.Body[0]] as string;
        //		string output = example.Value as string;
        //		//output = output[0].ToString();

        //              var occurrences = new List<string>();

        //              for (int i = 1; i < input.Length; i++)
        //              {
        //                  string s = input.Substring(0, i);
        //                  occurrences.Add(s);
        //              }
        //              if (occurrences.Count == 0) return null;
        //		result[inputState] = occurrences.Cast<object>();
        //	}
        //	return new DisjunctiveExamplesSpec(result); 
        //}


        [WitnessFunction(nameof(Semantics.Concat), 2, DependsOnParameters = new[] { 1 })]
        public ExampleSpec WitnessConcat2(GrammarRule rule, ExampleSpec spec, ExampleSpec spec1)
        {

            var result = new Dictionary<State, object>();

            foreach (KeyValuePair<State, object> example in spec.Examples)
            {
                State inputState = example.Key;
                //string input = inputState[rule.Body[0]] as string; 
                string output = example.Value as string;

                //State inputState = example.Key;
                //var output = example.Value as string;
                var s1 = (string)spec1.Examples[inputState];
                //result[inputState] = start + output.Length;




                //output=output.Substring(1);
                //var occurrences = new List<string>();


                //if input is the starting of output:
                //return remaining part of output
                //else return null;


                int index = output.IndexOf(s1);
                //int indexAfter = output.Length;
                string s2 = output.Substring(index + 1);
                //occurrences.Add(s1);
                result[inputState] = s2;
                //occurrences.Cast<object>();
                //if (occurrences.Count == 0) return null;
                //result[inputState] = occurrences.Cast<object>();
            }
            return new ExampleSpec(result);
        }

        //[WitnessFunction(nameof(Semantics.Concat), 2)]
        //public ExampleSpec WitnessConcat3(GrammarRule rule, ExampleSpec spec)
        //{

        //	var result = new Dictionary<State, object>();
        //	foreach (KeyValuePair<State, object> example in spec.Examples)
        //	{
        //		State inputState = example.Key;
        //		string input = inputState[rule.Body[0]] as string;
        //		string output = example.Value as string;
        //		output = output.Substring(1,3);
        //		result[inputState] = output;
        //	}
        //	return new ExampleSpec(result);
        //}


        //    [WitnessFunction(nameof(Semantics.Concat), 1)]
        //public ExampleSpec WitnessConcat(GrammarRule rule, ExampleSpec spec)
        //{
        //    var result = new Dictionary<State, object>();

        //    foreach (KeyValuePair<State, object> example in spec.Examples)
        //    {
        //        State inputState = example.Key;
        //        string input = inputState[rule.Body[0]] as string;
        //        string output = example.Value as string;
        //        var occurrences = new List<string>();


        //        //if input is the starting of output:
        //        //return remaining part of output
        //        //else return null;
        //        if (output.IndexOf(input) == 0)
        //        {
        //            int index = output.IndexOf(input);
        //            int indexAfter = index + input.Length;
        //            string s1 = output.Substring(indexAfter);
        //            //occurrences.Add(s1);
        //            result[inputState] = s1;
        //            //occurrences.Cast<object>();
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        //if (occurrences.Count == 0) return null;
        //        //result[inputState] = occurrences.Cast<object>();
        //    }
        //    return new ExampleSpec(result);
        //}

        [WitnessFunction(nameof(Semantics.Substring), 1)]
		public DisjunctiveExamplesSpec WitnessStartPosition1(GrammarRule rule, ExampleSpec spec)
		{
			var result = new Dictionary<State, IEnumerable<object>>();

			foreach (KeyValuePair<State, object> example in spec.Examples)
			{
				State inputState = example.Key;
				var input = inputState[rule.Body[0]] as string;
				var output = example.Value as string;
				var occurrences = new List<int>();
				Console.WriteLine("input: {0}", input);
				Console.WriteLine("output: {0}", output);

				for (int i = input.IndexOf(output); i >= 0; i = input.IndexOf(output, i + 1)) occurrences.Add(i);

				if (occurrences.Count == 0) return null;
				result[inputState] = occurrences.Cast<object>();
			}
			return new DisjunctiveExamplesSpec(result);
		}

		[WitnessFunction(nameof(Semantics.Substring), 2, DependsOnParameters = new[] {1})]
		public ExampleSpec WitnessEndPosition(GrammarRule rule, ExampleSpec spec, ExampleSpec startSpec)
		{
			var result = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> example in spec.Examples)
			{
				State inputState = example.Key;
				var output = example.Value as string;
				var start = (int) startSpec.Examples[inputState];
				result[inputState] = start + output.Length;
			}
			return new ExampleSpec(result);
		}


		[WitnessFunction(nameof(Semantics.AbsPos), 1)]
		public DisjunctiveExamplesSpec WitnessK(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			var kExamples = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> example in spec.DisjunctiveExamples)
			{
				State inputState = example.Key;
				var v = inputState[rule.Body[0]] as string;

				var positions = new List<int>();
				foreach (int pos in example.Value)
				{
					positions.Add(pos + 1);
					positions.Add(pos - v.Length - 1);
				}
				if (positions.Count == 0) return null;
				kExamples[inputState] = positions.Cast<object>();
			}
			return DisjunctiveExamplesSpec.From(kExamples);
		}

		[WitnessFunction(nameof(Semantics.RelPos), 1)]
		public DisjunctiveExamplesSpec WitnessRegexPair(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			var result = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> example in spec.DisjunctiveExamples)
			{
				State inputState = example.Key;
				var input = inputState[rule.Body[0]] as string;

				var regexes = new List<Tuple<Regex, Regex>>();
				foreach (int output in example.Value)
				{
					List<Regex>[] leftMatches, rightMatches;
					BuildStringMatches(input, out leftMatches, out rightMatches);


					List<Regex> leftRegex = leftMatches[output];
					List<Regex> rightRegex = rightMatches[output];
					if (leftRegex.Count == 0 || rightRegex.Count == 0)
						return null;
					regexes.AddRange(from l in leftRegex
						from r in rightRegex
						select Tuple.Create(l, r));
				}
				if (regexes.Count == 0) return null;
				result[inputState] = regexes;
			}
			return DisjunctiveExamplesSpec.From(result);
		}

		//witness function for k for relpos.
		//[WitnessFunction(nameof(Semantics.RelPos), 2, DependsOnParameters = new[] { 1 })]
		//public DisjunctiveExamplesSpec WitnessKthRegex(GrammarRule rule, ExampleSpec spec, DisjunctiveExamplesSpec rr)
		//{
		//    var result = new Dictionary<State, object>();
		//    foreach (KeyValuePair<State, IEnumerable<object>> example in rr.DisjunctiveExamples)
		//    {
		//        State inputState = example.Key;
		//        var input = inputState[rule.Body[0]] as string;

		//        var regexes = new List<Tuple<Regex, Regex>>();
		//        foreach (int output in example.Value)
		//        {
		//            List<Regex>[] leftMatches, rightMatches;
		//            BuildStringMatches(input, out leftMatches, out rightMatches);


		//            List<Regex> leftRegex = leftMatches[output];
		//            List<Regex> rightRegex = rightMatches[output];
		//            if (leftRegex.Count == 0 || rightRegex.Count == 0)
		//                return null;
		//            regexes.AddRange(from l in leftRegex
		//                             from r in rightRegex
		//                             select Tuple.Create(l, r));
		//        }
		//        int k = -1;
		//        for (int i = 0; i < regexes.Count; i++)
		//        {
		//            var regex = regexes[i];
		//            if (regex.Item1 ==  && regex.Item2 == rr[1])
		//                k = i;
		//        }

		//        if (k == -1) return null;
		//        result[inputState] = k;
		//    }
		//    return DisjunctiveExamplesSpec.From(result);
		//}


		private static void BuildStringMatches(string inp, out List<Regex>[] leftMatches,
			out List<Regex>[] rightMatches)
		{
			leftMatches = new List<Regex>[inp.Length + 1];
			rightMatches = new List<Regex>[inp.Length + 1];
			for (var p = 0; p <= inp.Length; ++p)
			{
				leftMatches[p] = new List<Regex>();
				rightMatches[p] = new List<Regex>();
			}
			foreach (Regex r in UsefulRegexes)
			foreach (Match m in r.Matches(inp))
			{
				leftMatches[m.Index + m.Length].Add(r);
				rightMatches[m.Index].Add(r);
			}
		}
	}
}