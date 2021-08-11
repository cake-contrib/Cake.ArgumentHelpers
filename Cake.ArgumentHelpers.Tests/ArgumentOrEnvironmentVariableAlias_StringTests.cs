using System;
using Cake.Core;
using Moq;
using Xunit;

namespace Cake.ArgumentHelpers.Tests
{
    public class ArgumentOrEnvironmentVariableAlias_StringTests : IDisposable
    {
        private Mock<ICakeContext> cakeContextMock;
        private Mock<ICakeArguments> cakeArgumentsMock;
        private Mock<ICakeEnvironment> cakeEnvironmentMock;

        public ArgumentOrEnvironmentVariableAlias_StringTests()
        {
            cakeContextMock = new Mock<ICakeContext>();
            cakeArgumentsMock = new Mock<ICakeArguments>();
            cakeEnvironmentMock = new Mock<ICakeEnvironment>();
            cakeContextMock.Setup(cakeContext => cakeContext.Arguments).Returns(cakeArgumentsMock.Object);
            cakeContextMock.Setup(cakeContext => cakeContext.Environment).Returns(cakeEnvironmentMock.Object);
        }

        private void SetupVariables(string key, string environmentPrefix, string argumentValue, string environmentValue)
        {
            bool hasArgument = argumentValue != null;
            cakeArgumentsMock.Setup(x => x.HasArgument(key)).Returns(hasArgument);
            if (hasArgument)
            {
                cakeArgumentsMock.Setup(x => x.GetArgument(key)).Returns(argumentValue.ToString());
            }
            bool hasEnvironmentVariable = environmentValue != null;
            if (hasEnvironmentVariable)
            {
                cakeEnvironmentMock.Setup(x => x.GetEnvironmentVariable(environmentPrefix + key)).Returns(environmentValue);
            }
        }

        [Fact]
        public void SomeArgumentAndNullEnvironment_ReturnsSome()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = "Some";
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testArgumentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndNullEnvironmentAndNullDefault_ReturnsNull()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = (string)null;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndSomeEnvironment_ReturnsSome()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = "Some";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testEnvironmentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndSomeEnvironmentWithoutPrefix_ReturnsSome()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            string testArgumentValue = null;
            string testEnvironmentValue = "Some";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testEnvironmentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SomeArgumentAndOtherEnvironment_ReturnsSome()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = "Some";
            string testEnvironmentValue = "Other";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testArgumentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndNullEnvironment_ReturnsDefault()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            var defaultValue = "Default";
            string testArgumentValue = null;
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = defaultValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, defaultValue);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndNullEnvironmentWithoutDefault_ReturnsNull()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = (string)null;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix);

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
        }
    }
}
