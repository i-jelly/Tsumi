using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tsuki.Model
{
    public class Rand
    {
        /// <summary>
        /// Generate Random float[0,1] using RNG
        /// </summary>
        /// <returns><see cref="Int32"/></returns>
        public int Next()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return BitConverter.ToInt32(rno, 0) / Int32.MaxValue;
            }
        }

        /// <summary>
        /// returns an random INT [0,maxValue]
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns><see cref="Int32"/></returns>
        public int Next(Int32 maxValue)
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return BitConverter.ToInt32(rno, 0) * maxValue / Int32.MaxValue ;
            }
        }

        /// <summary>
        /// returns an random INT [minValue,maxValue]
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns><see cref="Int32"/></returns>
        public int Next(Int32 minValue, Int32 maxValue)
        {

            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return BitConverter.ToInt32(rno, 0) * (maxValue - minValue) / Int32.MaxValue + minValue;
            }
        }
    }
}
