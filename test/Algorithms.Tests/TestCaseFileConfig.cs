namespace Algorithms.Tests
{
    internal class TestCaseFileConfig
    {
        private const int ONE_MEGABYTE = 1048576;

        public double FileSizeLimitInBytes { get; set; } = ONE_MEGABYTE;

        public string? AssignmentSubDirectory { get; set; }
    }
}
