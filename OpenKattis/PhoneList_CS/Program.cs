using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneList
{
	class PhoneListProcessor
    {
        const int MaxCount = 10000;
        const int MaxDigits = 10;
        Dictionary<int, bool> phoneNumberInfo = new Dictionary<int, bool>(MaxCount * MaxDigits);
        public bool Process(IEnumerable<string> phoneNumbers, bool readToEnd)
        {
            phoneNumberInfo.Clear();
            using (var e = phoneNumbers.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (Process(e.Current)) continue;
                    if (readToEnd)
                    {
                        while (e.MoveNext()) { }
                    }
                    return false;
                }
            }
            return true;
        }

        bool Process(string phoneNumber)
        {
            var phoneNumberInfo = this.phoneNumberInfo;
            int phoneCode = 0;
            int digitPos = 0;
            bool hasSuffix = true;
            while (true)
            {
                phoneCode = 11 * phoneCode + (phoneNumber[digitPos] - '0' + 1);
                bool isLastDigit = ++digitPos >= phoneNumber.Length;
                bool isPhoneNumber;
                if (hasSuffix && phoneNumberInfo.TryGetValue(phoneCode, out isPhoneNumber))
                {
                    if (isPhoneNumber || isLastDigit) return false;
                }
                else
                {
                  //   phoneNumberInfo.Add(phoneCode, isLastDigit);
						  phoneNumberInfo[phoneCode] = isLastDigit;
                    if (isLastDigit) return true;
                    hasSuffix = false;
                }
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
			  	var processor = new PhoneListProcessor();
            int testCount = int.Parse(Console.ReadLine());
            for (int testIndex = 0; testIndex < testCount; testIndex++)
            {
                int count = int.Parse(Console.ReadLine());
                var numbers = Enumerable.Range(0, count).Select(_ => Console.ReadLine());
                bool readToEnd = testIndex + 1 < testCount;
                bool valid = processor.Process(numbers, readToEnd);
                Console.WriteLine(valid ? "YES" : "NO");
            }
        }
    }
}
