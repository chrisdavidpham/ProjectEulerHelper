using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ProjectEuler
{
    public class PrimeNumbers
    {
        private readonly int[] FIRST_FIVE_PRIMES = new int[5] { 2, 3, 5, 7, 11 };

        public List<int> List { get; protected set; }

        public PrimeNumbers()
        {
            List = new List<int>();
        }

        public PrimeNumbers(int primeCount)
        {
            List = GetPrimesBySieve(primeCount);
        }

        public List<int> GetPrimesByFile(string fileFullName, int primeCount)
        {
            List<int> primes = new List<int>();

            using (FileStream fileStream = new FileStream(fileFullName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length && primes.Count < primeCount)
                    {
                        primes.Add(binaryReader.ReadInt32());
                    }
                }
            }

            if (primes.Count < primeCount)
            {
                throw new PrimeNumbersException();
            }

            return primes;
        }

        public List<int> GetPrimesBySieve(int primeCount)
        {
            int limit = GetNthPrimeApproximation(primeCount);
            BitArray bitArray = SieveOfEratosthenes(limit);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0; i < limit && found < primeCount; i++)
            {
                if (bitArray[i])
                {
                    primes.Add(i);
                    found++;
                }
            }
            return primes;
        }

        private int GetNthPrimeApproximation(int atN)
        {
            double approximatePrime = 0;
            switch (Convert.ToDouble(atN))
            {
                case double n when n >= 7022:
                    approximatePrime = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
                    break;
                case double n when n >= 6 && n < 7022:
                    approximatePrime = n * Math.Log(n) + n * Math.Log(Math.Log(n));
                    break;
                case double n when n >= 1 && n < 6:
                    approximatePrime = FIRST_FIVE_PRIMES[atN - 1];
                    break;
                case double n when n < 1:
                    throw new PrimeNumbersException();
            }

            return Convert.ToInt32(approximatePrime);
        }

        // Find all primes up to and including the limit
        private System.Collections.BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true);
            bits[0] = false;
            bits[1] = false;
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }
            return bits;
        }

    }

    [Serializable()]
    public class PrimeNumbersException : Exception
    {
        public PrimeNumbersException() { }
        public PrimeNumbersException(string message) : base(message) { }
        public PrimeNumbersException(string message, Exception inner) : base(message, inner) { }
    }
}
