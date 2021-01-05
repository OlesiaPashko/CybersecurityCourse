using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class MT19937
    {
        // Period parameters
        private const ulong N = 624;
        private const ulong M = 397;
        private const ulong MATRIX_A = 0x9908B0DFUL;		// constant vector a 
        private const ulong UPPER_MASK = 0x80000000UL;		// most significant w-r bits
        private const ulong LOWER_MASK = 0X7FFFFFFFUL;		// least significant r bits
        private const uint DEFAULT_SEED = 4357;

        private static ulong[] mt = new ulong[N + 1];	// the array for the state vector
        private static ulong mti = N + 1;			// mti==N+1 means mt[N] is not initialized

        public MT19937()
        {
            ulong[] init = new ulong[4];
            init[0] = 0x123;
            init[1] = 0x234;
            init[2] = 0x345;
            init[3] = 0x456;
            ulong length = 4;
            this.init_by_array(init, length);
        }

        // initializes mt[N] with a seed
        public void init_genrand(ulong s)
        {
            mt[0] = s & 0xffffffffUL;
            for (mti = 1; mti < N; mti++)
            {
                mt[mti] = (1812433253UL * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
                /* See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. */
                /* In the previous versions, MSBs of the seed affect   */
                /* only MSBs of the array mt[].                        */
                /* 2002/01/09 modified by Makoto Matsumoto             */
                mt[mti] &= 0xffffffffUL;
                /* for >32 bit machines */
            }
        }

        public void init_genrand(ulong[] states)
        {
            mt = states;
        }

        // initialize by an array with array-length
        // init_key is the array for initializing keys
        // key_length is its length
        public void init_by_array(ulong[] init_key, ulong key_length)
        {
            ulong i, j, k;
            this.init_genrand(19650218UL);
            i = 1; j = 0;
            k = (N > key_length ? N : key_length);
            for (; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525UL))
                        + init_key[j] + j;		// non linear 
                mt[i] &= 0xffffffffUL;	// for WORDSIZE > 32 machines
                i++; j++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
                if (j >= key_length) j = 0;
            }
            for (k = N - 1; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941UL))
                        - i;					// non linear
                mt[i] &= 0xffffffffUL;	// for WORDSIZE > 32 machines
                i++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
            }
            mt[0] = 0x80000000UL;		// MSB is 1; assuring non-zero initial array
        }

        // generates a random number on [0,0x7fffffff]-interval
        public long genrand_int31()
        {
            return (long)(this.genrand_int32() >> 1);
        }
        // generates a random number on [0,1]-real-interval
        public double genrand_real1()
        {
            return (double)this.genrand_int32() * (1.0 / 4294967295.0); // divided by 2^32-1 
        }
        // generates a random number on [0,1)-real-interval
        public double genrand_real2()
        {
            return (double)this.genrand_int32() * (1.0 / 4294967296.0); // divided by 2^32
        }
        // generates a random number on (0,1)-real-interval
        public double genrand_real3()
        {
            return (((double)this.genrand_int32()) + 0.5) * (1.0 / 4294967296.0); // divided by 2^32
        }
        // generates a random number on [0,1) with 53-bit resolution
        public double genrand_res53()
        {
            ulong a = this.genrand_int32() >> 5;
            ulong b = this.genrand_int32() >> 6;
            return (double)(a * 67108864.0 + b) * (1.0 / 9007199254740992.0);
        }
        // These real versions are due to Isaku Wada, 2002/01/09 added 

        // generates a random number on [0,0xffffffff]-interval
        public ulong genrand_int32()
        {
            ulong y = 0;
            ulong[] mag01 = new ulong[2];
            mag01[0] = 0x0UL;
            mag01[1] = MATRIX_A;
            /* mag01[x] = x * MATRIX_A  for x=0,1 */

            if (mti >= N)
            {
                // generate N words at one time
                ulong kk;

                if (mti == N + 1)   /* if init_genrand() has not been called, */
                    this.init_genrand(5489UL); /* a default initial seed is used */

                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    //mt[kk] = mt[kk+(M-N)] ^ (y >> 1) ^ mag01[y & 0x1UL];
                    mt[kk] = mt[kk - 227] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1UL];

                mti = 0;
            }

            y = mt[mti++];

            /* Tempering */
            y = tempering(y);

            return y;
        }

        public ulong tempering(ulong y)
        {
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680UL;
            y ^= (y << 15) & 0xefc60000UL;
            y ^= (y >> 18);

            return y;
        }

        public ulong untempering(ulong y)
        {
            y ^= (y >> 18);
            y ^= (y << 15) & 0xefc60000UL;
            y ^=
                ((y << 7) & 0x9d2c5680UL) ^
                ((y << 14) & 0x94284000UL) ^
                ((y << 21) & 0x14200000UL) ^
                ((y << 28) & 0x10000000UL);
            y ^= (y >> 11) ^ (y >> 22);

            return y;
        }

        public int RandomRange(int lo, int hi)
        {
            return (Math.Abs((int)this.genrand_int32() % (hi - lo + 1)) + lo);
        }
    }
}
