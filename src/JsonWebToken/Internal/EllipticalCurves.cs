// Copyright (c) 2018 Yann Crumeyrolle. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace JsonWebToken.Internal
{
    /// <summary>
    /// Constants for JsonWebKey Elliptical Curve Types
    /// https://tools.ietf.org/html/rfc7518#section-6.2.1.1
    /// </summary>
    public static class EllipticalCurves
    {
        public const string P256 = "P-256";
        public const string P384 = "P-384";
        public const string P521 = "P-521";
    }
}