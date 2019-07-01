using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            List<int> indexes = new List<int>();
            int[] ans = new int[dietPlans.Length];
            int[] total = new int[protein.Length];

            // compute total calories from protien, carbs and fat
            for (int index = 0; index < protein.Length; index++)
            {
                total[index] = 5 * protein[index] + 5 * carbs[index] + 9 * fat[index];
            }

            // iterate through dietplan
            for (int index = 0; index < dietPlans.Length; index++)
            {
                indexes.Clear();
                
                // append indexes for each item
                for (int index2 = 0; index2 < protein.Length; index2++)
                {
                    indexes.Add(index2);
                }

                // iterate through each item of each diet plan
                for (int index2 = 0; index2 < dietPlans[index].Length; index2++)
                {
                    switch (dietPlans[index][index2])
                    {
                        case 'C':
                            indexes = Maximum(carbs, indexes);
                            break;
                        case 'c':
                            indexes = Minimum(carbs, indexes);
                            break;
                        case 'P':
                            indexes = Maximum(protein, indexes);
                            break;
                        case 'p':
                            indexes = Minimum(protein, indexes);
                            break;
                        case 'F':
                            indexes = Maximum(fat, indexes);
                            break;
                        case 'f':
                            indexes = Minimum(fat, indexes);
                            break;
                        case 'T':
                            indexes = Maximum(total, indexes);
                            break;
                        case 't':
                            indexes = Minimum(total, indexes);
                            break;
                    }
                    // break if exact one match occurs
                    if (indexes.Count == 1)
                    {
                        break;
                    }
                }
                // final value appended is either the one that matches
                // or it is the default one when no match occurs
                ans[index] = indexes[0];
            }
            return ans;
        }
        private static List<int> Minimum(int[] arr, List<int> indexes)
        {
            int minimum = arr[indexes[0]];
            List<int> temp = new List<int>();

            for (int index = 0; index < indexes.Count; index++)
            {
                if (arr[indexes[index]] < minimum)
                {
                    minimum = arr[indexes[index]];
                }
            }
            for (int index = 0; index < indexes.Count; index++)
            {
                if (arr[indexes[index]] == minimum)
                {
                    temp.Add(indexes[index]);
                }
            }

            return temp;
        }
        private static List<int> Maximum(int[] arr, List<int> indexes)
        {
            int maximum = arr[indexes[0]];
            List<int> temp = new List<int>();

            for (int index = 0; index < indexes.Count; index++)
            {
                if (arr[indexes[index]] > maximum)
                {
                    maximum = arr[indexes[index]];
                }
            }
            for (int index = 0; index < indexes.Count; index++)
            {
                if (arr[indexes[index]] == maximum)
                {
                    temp.Add(indexes[index]);
                }
            }

            return temp;
        }
    }
}
