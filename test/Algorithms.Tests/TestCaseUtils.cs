using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.Tests
{
    internal static class TestCaseUtils
    {
        private const int ONE_MEGABYTE = 1048576;

        [Obsolete("Deprecated. Please use GetTestCaseFiles(int, int, TestCaseFileConfig).")]
        public static IEnumerable<string> GetTestCaseFiles(
            int courseNumber,
            int assignmentNumber,
            double fileSizeLimitInBytes = ONE_MEGABYTE
        ) => GetTestCaseFiles(
            courseNumber,
            assignmentNumber,
            new TestCaseFileConfig { FileSizeLimitInBytes = fileSizeLimitInBytes }
        );

        public static IEnumerable<string> GetTestCaseFiles(
            int courseNumber,
            int assignmentNumber,
            TestCaseFileConfig config
        )
        {
            var testCasesDir = GetTestCasesDirectory();
            var courseDir = Directory.GetDirectories(testCasesDir, $"course{courseNumber}")[0];
            var assignmentDir = Directory.GetDirectories(courseDir, $"assignment{assignmentNumber}*")[0];
            if (!string.IsNullOrEmpty(config.AssignmentSubDirectory))
            {
                assignmentDir = Path.Combine(assignmentDir, config.AssignmentSubDirectory);
            }
            if (config.FileSizeLimitInBytes <= 0)
            {
                config.FileSizeLimitInBytes = ONE_MEGABYTE;
            }
            return Directory.GetFiles(assignmentDir, "input_*")
                .Where(x => new FileInfo(x).Length < config.FileSizeLimitInBytes);
        }

        private static string GetTestCasesDirectory()
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            while (currentDirectory.GetDirectories("test").Length == 0
                && currentDirectory != currentDirectory.Root)
            {
                currentDirectory = currentDirectory.Parent;
            }
            return Path.Combine(currentDirectory.FullName, "test\\testCases");
        }
    }
}
