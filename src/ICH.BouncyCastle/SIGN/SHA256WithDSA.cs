﻿using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace ICH.BouncyCastle
{
    public class SHA256WithDSA
    {
        public static string GenerateSignature(string data, AsymmetricKeyParameter parameter)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var signer = SignerUtilities.GetSigner("SHA256WithDSA");
            signer.Init(true, parameter);
            var bytes = Encoding.UTF8.GetBytes(data);
            signer.BlockUpdate(bytes, 0, bytes.Length);
            return Base64.ToBase64String(signer.GenerateSignature());
        }

        public static bool VerifySignature(string data, string sign, AsymmetricKeyParameter parameter)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrEmpty(sign))
            {
                throw new ArgumentNullException(nameof(sign));
            }

            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var verifier = SignerUtilities.GetSigner("SHA256WithDSA");
            verifier.Init(false, parameter);
            var bytes = Encoding.UTF8.GetBytes(data);
            verifier.BlockUpdate(bytes, 0, bytes.Length);
            return verifier.VerifySignature(Base64.Decode(sign));
        }
    }
}
