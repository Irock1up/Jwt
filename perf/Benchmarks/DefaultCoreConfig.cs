﻿using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;
using BenchmarkDotNet.Validators;

namespace JsonWebToken.Performance
{
    internal class DefaultCoreConfig : ManualConfig
    {
        public DefaultCoreConfig()
        {
            Add(ConsoleLogger.Default);
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
            Add(StatisticColumn.OperationsPerSecond);
            Add(DefaultColumnProviders.Instance);

            Add(JitOptimizationsValidator.FailOnError);
            Add(BenchmarkLogicalGroupRule.ByCategory);

            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp30))
                .With(new GcMode { Server = true })
                .WithMinIterationTime(TimeInterval.FromSeconds(1)));
            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp21))
                .With(new GcMode { Server = true })
                .WithMinIterationTime(TimeInterval.FromSeconds(1)));
        }
    }

    internal class AllTfmCoreConfig : ManualConfig
    {
        public AllTfmCoreConfig()
        {
            Add(ConsoleLogger.Default);
            Add(MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
            Add(StatisticColumn.OperationsPerSecond);
            Add(DefaultColumnProviders.Instance);

            Add(JitOptimizationsValidator.FailOnError);
            Add(BenchmarkLogicalGroupRule.ByCategory);

            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp20))
                .With(new GcMode { Server = true })
                .WithMinIterationTime(TimeInterval.FromSeconds(1))
                .WithMinInvokeCount(10));

            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp21))
                .With(new GcMode { Server = true })
                .WithMinIterationTime(TimeInterval.FromSeconds(1))
                .WithMinInvokeCount(10));	         
            
            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp30))
                .With(new GcMode { Server = true })
                .WithMinIterationTime(TimeInterval.FromSeconds(1))
                .WithMinInvokeCount(10));
        }
    }

}