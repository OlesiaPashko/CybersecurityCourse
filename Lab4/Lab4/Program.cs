using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4
{
    class Program
    {
        static void Main()
        {
            PasswordsGenerator passwordsGenerator = new PasswordsGenerator();

            //geterates 500 000 passwords where the weakest has 7%, 1M weakest - 75%, random - 4%, human like - 14%
            List<string> passwords = passwordsGenerator.GeneratePasswords(500000, 7, 75, 4, 14);

            Hasher hasher = new Hasher();
            hasher.StoreHashes(passwords);
        }
    }
}
