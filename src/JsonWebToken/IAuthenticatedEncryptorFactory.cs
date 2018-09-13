﻿// Copyright (c) 2018 Yann Crumeyrolle. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace JsonWebToken
{
    /// <summary>
    /// Represents a factory used to creates <see cref="AuthenticatedEncryptor"/>.
    /// </summary>
    public interface IAuthenticatedEncryptorFactory : IDisposable
    {
        AuthenticatedEncryptor Create(JsonWebKey key, EncryptionAlgorithm encryptionAlgorithm);
    }
}