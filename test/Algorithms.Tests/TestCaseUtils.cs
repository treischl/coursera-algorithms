using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.Tests
{
    public static class TestCaseUtils
    {
        private const int ONE_MEGABYTE = 1048576;

        public static IEnumerable<string> GetTestCaseFiles(
            int courseNumber,
            int assignmentNumber,
            double fileSizeLimitInBytes = ONE_MEGABYTE)
        {
            var testCasesDir = GetTestCasesDirectory();
            var courseDir = Directory.GetDirectories(testCasesDir, $"course{courseNumber}")[0];
            var assignmentDir = Directory.GetDirectories(courseDir, $"assignment{assignmentNumber}*")[0];
            if (fileSizeLimitInBytes <= 0)
            {
                fileSizeLimitInBytes = ONE_MEGABYTE;
            }
            return Directory.GetFiles(assignmentDir, "input_*")
                .Where(x => new FileInfo(x).Length < fileSizeLimitInBytes);
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
