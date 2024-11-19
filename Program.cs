namespace CountNumberOfFairPairs
{
    internal class Program
    {
        //Problem Link: https://leetcode.com/problems/count-the-number-of-fair-pairs
        static void Main(string[] args)
        {
            Console.WriteLine("Testing CountFairPairs:");

            // Test cases
            int[] nums1 = { 0, 1, 7, 4, 4, 5 };
            int lower1 = 3, upper1 = 6;

            int[] nums2 = { 1, 7, 9, 2, 5 };
            int lower2 = 11, upper2 = 11;

            TestCountFairPairs(nums1, lower1, upper1);
            TestCountFairPairs(nums2, lower2, upper2);

            Console.WriteLine("Testing Completed.");
        }

        static void TestCountFairPairs(int[] nums, int lower, int upper)
        {
            Program program = new Program();

            // Brute force solution
            long bruteForceResult = program.CountFairPairsBrute(nums, lower, upper);

            // Optimized solution
            long optimizedResult = program.CountFairPairs(nums, lower, upper);

            // Print results
            Console.WriteLine("Input: nums = [" + string.Join(", ", nums) + $"], lower = {lower}, upper = {upper}");
            Console.WriteLine($"Brute Force Result: {bruteForceResult}");
            Console.WriteLine($"Optimized Result: {optimizedResult}");

            // Validate correctness
            if (bruteForceResult == optimizedResult)
            {
                Console.WriteLine("Results match! ✅");
            }
            else
            {
                Console.WriteLine("Results do not match! ❌");
            }

            Console.WriteLine();
        }

        //Brute Force Solution
        public long CountFairPairsBrute(int[] nums, int lower, int upper)
        {
            int n = nums.Length;
            long count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int sum = nums[i] + nums[j];
                    if (sum >= lower && sum <= upper)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public long CountFairPairs(int[] nums, int lower, int upper)
        {
            int n = nums.Length;
            Array.Sort(nums);
            long count = 0;

            for (int i = 0; i < n; i++)
            {
                //Binary search to find the valid indices
                int start = FindLeftBound(nums, i + 1, n - 1, lower - nums[i]);
                int end = FindRightBound(nums, i + 1, n - 1, upper - nums[i]);

                if (start != -1 && end != -1 && start <= end)
                {
                    count += (end - start + 1);
                }
            }
            return count;
        }

        private int FindLeftBound(int[] nums, int low, int high, int target)
        {
            int result = -1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (nums[mid] >= target)
                {
                    result = mid;
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return result;
        }

        private int FindRightBound(int[] nums, int low, int high, int target)
        {
            int result = -1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (nums[mid] <= target)
                {
                    result = mid;
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return result;
        }
    }
}
