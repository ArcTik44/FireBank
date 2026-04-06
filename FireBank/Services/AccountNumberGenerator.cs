using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FireBank.Services
{

    /// <summary>
    /// Generátor čísel bankovních účtů ve formátu IBAN (CZ) nebo českém národním formátu.
    /// </summary>
    public class AccountNumberGenerator
    {
        private readonly HashSet<string> _generatedNumbers = [];
        private static readonly string[] BankCodes =
        [
        "0100", "0300", "0600", "0710", "0800", "2010", "2020", "2060",
        "2070", "2100", "2200", "2250", "2600", "2700", "3030", "3500",
        "4000", "4300", "5500", "5800", "6000", "6100", "6200", "6210",
        "6300", "6700", "6800", "7910", "7940", "7960", "7970", "7980",
        "7990", "8030", "8040", "8060", "8090", "8150", "8200", "8215",
        "8220", "8225", "8230", "8240", "8250", "8255", "8260", "8265",
        "8270", "8280", "8283", "8291", "8292", "8293", "8294", "8296"
    ];

        // ── Národní formát ──────────────────────────────────────────────

        /// <summary>Vygeneruje číslo účtu v národním formátu: [předčíslí-]čísloúčtu/kód.</summary>
        public string GenerateNational(bool includePrefix = false)
        {
            string bankCode = GetRandomBankCode();
            string prefix = includePrefix ? GeneratePrefix() : string.Empty;
            string accountNum = GenerateAccountNumber();

            return includePrefix
                ? $"{prefix}-{accountNum}/{bankCode}"
                : $"{accountNum}/{bankCode}";
        }

        // ── IBAN ────────────────────────────────────────────────────────

        /// <summary>Vygeneruje platný český IBAN (CZ).</summary>
        public string GenerateIBAN()
        {
            string bankCode = GetRandomBankCode();
            string prefix = GeneratePrefix().PadLeft(6, '0');
            string accountNum = GenerateAccountNumber().PadLeft(10, '0');

            // BBAN = kód banky (4) + předčíslí (6) + číslo účtu (10)
            string bban = bankCode + prefix + accountNum;
            string checkDigits = CalculateIBANCheckDigits("CZ", bban);

            return $"CZ{checkDigits}{bban}";
        }

        // ── Hromadné generování ─────────────────────────────────────────

        /// <summary>Vygeneruje zadaný počet unikátních národních čísel účtů.</summary>
        public IEnumerable<string> GenerateMultipleNational(int count, bool includePrefix = false)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            return GenerateUnique(count, () => GenerateNational(includePrefix));
        }

        /// <summary>Vygeneruje zadaný počet unikátních IBANů.</summary>
        public IEnumerable<string> GenerateMultipleIBAN(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            return GenerateUnique(count, GenerateIBAN);
        }

        // ── Validace ────────────────────────────────────────────────────

        /// <summary>Ověří, zda je IBAN formálně správný.</summary>
        public static bool ValidateIBAN(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban)) return false;

            iban = iban.Replace(" ", "").ToUpper();
            if (iban.Length < 4) return false;

            // Přesuň první 4 znaky na konec a převeď písmena na čísla
            string rearranged = iban[4..] + iban[..4];
            string numeric = ConvertIBANToNumeric(rearranged);

            return ModuloCheck(numeric) == 1;
        }

        /// <summary>Ověří číslo účtu v národním formátu pomocí modulo-11 algoritmu.</summary>
        public static bool ValidateNational(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber)) return false;

            var parts = accountNumber.Split('/');
            if (parts.Length != 2) return false;

            string numberPart = parts[0];
            var segments = numberPart.Split('-');

            if (segments.Length == 2)
                return ValidateMod11(segments[0]) && ValidateMod11(segments[1]);

            return ValidateMod11(segments[0]);
        }

        // ── Privátní pomocné metody ─────────────────────────────────────

        private static string GeneratePrefix() => GetRandomNumber(1, 999999).ToString();
        private static string GenerateAccountNumber() => GetRandomNumber(1000000, 9999999999).ToString();

        private static string GetRandomBankCode() =>
            BankCodes[RandomNumberGenerator.GetInt32(BankCodes.Length)];

        private static long GetRandomNumber(long min, long max)
        {
            // Kryptograficky bezpečné náhodné číslo v rozsahu
            long range = max - min + 1;
            long result = (long)(RandomNumberGenerator.GetInt32(int.MaxValue) / (double)int.MaxValue * range) + min;
            return result;
        }

        private IEnumerable<string> GenerateUnique(int count, Func<string> generator)
        {
            var results = new List<string>(count);
            int attempts = 0;

            while (results.Count < count)
            {
                if (++attempts > count * 10)
                    throw new InvalidOperationException("Nelze vygenerovat dostatek unikátních čísel.");

                string number = generator();
                if (_generatedNumbers.Add(number))
                    results.Add(number);
            }

            return results;
        }

        private static string CalculateIBANCheckDigits(string countryCode, string bban)
        {
            // Přesuň kód země s "00" na konec a spočítej mod 97
            string rearranged = bban + countryCode + "00";
            string numeric = ConvertIBANToNumeric(rearranged);
            long remainder = 98 - ModuloCheck(numeric);
            return remainder.ToString().PadLeft(2, '0');
        }

        private static string ConvertIBANToNumeric(string input)
        {
            var sb = new System.Text.StringBuilder();
            foreach (char c in input)
                sb.Append(char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString());
            return sb.ToString();
        }

        private static long ModuloCheck(string numericString)
        {
            long remainder = 0;
            foreach (char c in numericString)
                remainder = (remainder * 10 + (c - '0')) % 97;
            return remainder;
        }

        private static bool ValidateMod11(string number)
        {
            if (!long.TryParse(number, out _)) return false;

            int[] weights = [6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
            string padded = number.PadLeft(10, '0');
            int sum = 0;

            for (int i = 0; i < 10; i++)
                sum += (padded[i] - '0') * weights[i];

            return sum % 11 == 0;
        }
    }
}