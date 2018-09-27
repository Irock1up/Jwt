﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JsonWebToken
{
    internal class CryptographicStore<TCrypto> : IDisposable where TCrypto : IDisposable
    {
        private readonly ConcurrentDictionary<CryprographicFactoryKey, TCrypto> _store;
        private bool _disposed;

        public CryptographicStore()
        {
            _store = new ConcurrentDictionary<CryprographicFactoryKey, TCrypto>(JwkEqualityComparer.Default);
        }

        public bool TryGetValue(CryprographicFactoryKey key, out TCrypto value)
        {
            return _store.TryGetValue(key, out value);
        }

        public TCrypto AddValue(CryprographicFactoryKey key, TCrypto value)
        {
            if (!_store.TryAdd(key, value) && _store.TryGetValue(key, out var cachedValue))
            {
                value.Dispose();
                return cachedValue;
            }
            else
            {
                return value;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                foreach (var item in _store)
                {
                    item.Value.Dispose();
                }

                _disposed = true;
            }
        }
    }
}