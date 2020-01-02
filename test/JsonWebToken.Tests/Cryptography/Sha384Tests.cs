﻿using System;
using Xunit;

namespace JsonWebToken.Tests.Cryptography
{
    public class Sha384Tests : ShaAlgorithmTest
    {
        protected override void ComputeHash(Span<byte> source, Span<byte> destination)
        {
            var sha384 = new Sha384();
            sha384.ComputeHash(source, destination);
        }

        [Fact]
        public void Sha384_Empty()
        {
            Verify(
                Array.Empty<byte>(),
                "38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B");
        }

        // These test cases are from http://csrc.nist.gov/groups/ST/toolkit/documents/Examples/SHA_All.pdf
        [Fact]
        public void Sha384_NistShaAll_1()
        {
            Verify(
                "abc",
                "CB00753F45A35E8BB5A03D699AC65007272C32AB0EDED1631A8B605A43FF5BED8086072BA1E7CC2358BAECA134C825A7");
        }

        [Fact]
        public void Sha384_NistShaAll_2()
        {
            Verify(
                "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu",
                "09330C33F71147E83D192FC782CD1B4753111B173B3B05D22FA08086E3B0F712FCC7C71A557E2DB966C3E9FA91746039");
        }
    }
}