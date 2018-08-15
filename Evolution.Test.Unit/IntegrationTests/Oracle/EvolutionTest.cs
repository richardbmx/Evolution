﻿using Evolution.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Evolution.Test.Unit.IntegrationTests.Oracle
{
    public class EvolutionTest : IDisposable
    {
        private const string _FilePath = "./IntegrationTests/TestSql/Oracle/";

        private readonly ITestOutputHelper _outputHelper;

        public EvolutionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait("Category", "integration")]
        public void Evolve_Success()
        {
            var fileList = Directory.GetFiles(_FilePath).OrderBy(file => file);
            string arguments;

            foreach (var fileName in fileList)
            {
                arguments = $"{ GetAddArguments(fileName) } { GetConnectionOptionsString() }";
                Assert.Equal(0, Run(arguments));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(".sql", string.Empty).Replace(_FilePath, string.Empty);
                arguments = $"{ GetExecArguments(fileName) } { GetConnectionOptionsString() }";
                _outputHelper.WriteLine(arguments);
                Assert.Equal(0, Run(arguments));
            }
        }

        private int Run(string arguments)
        {
            return Program.Main(arguments.Split(' '));
        }

        private string GetAddArguments(string fileName)
        {
            return string.Format("add --target {0} --source {1}",
                Path.GetFileName(fileName).Replace(".sql", string.Empty),
                fileName);
        }

        private string GetExecArguments(string targetName)
        {
            return string.Format("exec --target {0}",targetName);
        }

        private string GetConnectionOptionsString()
        {
            var connectionParams = string.Format("--user {0} --pwd {1} --server {2} --instance {3} --port {4} --type {5}",
                TestContext.Parameters["OracleUser"],
                TestContext.Parameters["OraclePassword"],
                TestContext.Parameters["OracleServer"],
                TestContext.Parameters["OracleInstance"],
                TestContext.Parameters["OraclePort"],
                ((int)DatabaseTypes.Oracle).ToString());

            return connectionParams;
        }

        public void Dispose()
        {
            foreach(var file in Directory.GetFiles("./", "*.sql", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }
        }
    }

    internal static class TestContext
    {
        public static Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>()
        {
            { "OracleUser", "c##appUser" },
            { "OraclePassword", "appPassword" },
            { "OracleServer", "localhost" },
            { "OracleInstance", "ORCLCDB.localdomain" },
            { "OraclePort", "6666" }
        };
    }
}
