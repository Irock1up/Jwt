﻿// Copyright (c) 2018 Yann Crumeyrolle. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace JsonWebToken
{
    /// <summary>
    /// Represents a key used by the <see cref="CryptographicStore{TCrypto}"/>.
    /// </summary>
    public readonly struct CryptographicFactoryKey
    {
        /// <summary>
        /// The <see cref="Jwk"/>.
        /// </summary>
        public readonly Jwk Key;
        
        /// <summary>
        /// The algorithm.
        /// </summary>
        public readonly int Algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptographicFactoryKey"/> class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="algorithm"></param>
        public CryptographicFactoryKey(Jwk key, int algorithm)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Algorithm = algorithm;
        }
    }
}