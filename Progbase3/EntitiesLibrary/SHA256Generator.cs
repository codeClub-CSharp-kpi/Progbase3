using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
	public static class SHA256Generator
	{
		public static string ProduceSHA256Hash(string sourceStr)
		{
			string hash = null;
			using (SHA256 shaHash = SHA256.Create())
			{
				// Byte array representation of source string
				byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceStr);

				// Generate hash value(Byte Array) for input data
				byte[] hashBytes = shaHash.ComputeHash(sourceBytes);

				// Convert hash byte array to string
				hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
			}

			return hash;
		}
	}
}
