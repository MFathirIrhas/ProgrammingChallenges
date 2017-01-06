using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_CS
{
    public class Program
    {
        long Mod(long x)
        {
            return x % 1000000007;
        }

        long DoSomething(string s)
        {
            int len = s.Length;
            long[,] nums = new long[len + 1, len + 1];
            long sum = 0;
            nums[0, 0] = 1;

            for (int i = 0; i < len; ++i)
            {
                if(s[i] == '?')
                {
                    sum = Mod(sum * 2);
                }
                for (int j = 0; j <= i; ++j)
                {
                    if (s[i] == '0' || s[i] == '?')
                    {
                        nums[i + 1, j] = Mod(nums[i + 1, j] + nums[i, j]);
                        sum = Mod(sum + j * nums[i, j]);
                    }

                    if (s[i] == '1' || s[i] == '?')
                    {
                        nums[i + 1, j + 1] = nums[i, j];
                    }
                }
            }

            return sum;
        }

        public static void Main(string[] args)
        {
            Program p = new Program();
            string input = Console.ReadLine();
            long r = p.DoSomething(input);
            Console.WriteLine(r);
        }
    }
}