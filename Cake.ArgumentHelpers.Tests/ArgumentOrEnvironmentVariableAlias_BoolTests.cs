using System;
using Cake.Core;
using Moq;
using Xunit;

namespace Cake.ArgumentHelpers.Tests
{
    public class ArgumentOrEnvironmentVariableAlias_BoolTests : IDisposable
    {
        private Mock<ICakeContext> cakeContextMock;
        private Mock<ICakeArguments> cakeArgumentsMock;
        private Mock<ICakeEnvironment> cakeEnvironmentMock;

        public ArgumentOrEnvironmentVariableAlias_BoolTests()
        {
            cakeContextMock = new Mock<ICakeContext>();
            cakeArgumentsMock = new Mock<ICakeArguments>();
            cakeEnvironmentMock = new Mock<ICakeEnvironment>();
            cakeContextMock.Setup(cakeContext => cakeContext.Arguments).Returns(cakeArgumentsMock.Object);
            cakeContextMock.Setup(cakeContext => cakeContext.Environment).Returns(cakeEnvironmentMock.Object);
        }

        private void SetupVariables(string key, string environmentPrefix, bool? argumentValue, bool? environmentValue)
        {
            bool hasArgument = argumentValue != null;
            cakeArgumentsMock.Setup(x => x.HasArgument(key)).Returns(hasArgument);
            if (hasArgument)
            {
                cakeArgumentsMock.Setup(x => x.GetArgument(key)).Returns(argumentValue.ToString());
            }
            cakeEnvironmentMock.Setup(x => x.GetEnvironmentVariable(environmentPrefix + key)).Returns(environmentValue != null ? environmentValue.Value.ToString() : null);
        }

        [Fact]
        public void TrueArgumentAndNullEnvironment_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void FalseArgumentAndNullEnvironment_ReturnsFalse()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = false;
            bool? testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndTrueEnvironment_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndFalseEnvironment_ReturnsFalse()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = false;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndTrueEnvironmentWithoutPrefix_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NullArgumentAndTrueEnvironmentNoPrefixOverload_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TrueArgumentAndTrueEnvironment_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TrueArgumentAndFalseEnvironment_ReturnsTrue()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = false;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void FalseArgumentAndTrueEnvironment_ReturnsFalse()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = false;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
        }
    }
}
