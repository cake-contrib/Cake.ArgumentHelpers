using NUnit.Framework;
using Cake.Core;
using Moq;

namespace Cake.ArgumentHelpers.Tests {
    [TestFixture()]
    public class ArgumentOrEnvironmentVariableAliasTests {
        Mock<ICakeContext> cakeContextMock;
        Mock<ICakeArguments> cakeArgumentsMock;
        Mock<ICakeEnvironment> cakeEnvironmentMock;

        [SetUp]
        public void Setup() {
            cakeContextMock = new Mock<ICakeContext>();
            cakeArgumentsMock = new Mock<ICakeArguments>();
            cakeEnvironmentMock = new Mock<ICakeEnvironment>();
            cakeContextMock.Setup(cakeContext => cakeContext.Arguments).Returns(cakeArgumentsMock.Object);
            cakeContextMock.Setup(cakeContext => cakeContext.Environment).Returns(cakeEnvironmentMock.Object);
        }

        void SetupVariables(string key, string environmentPrefix, bool? argumentValue, bool? environmentValue) {
            bool hasArgument = argumentValue != null;
            cakeArgumentsMock.Setup(x => x.HasArgument(key)).Returns(hasArgument);
            if (hasArgument) {
                cakeArgumentsMock.Setup(x => x.GetArgument(key)).Returns(argumentValue.ToString());
            }
            cakeEnvironmentMock.Setup(x => x.GetEnvironmentVariable(environmentPrefix + key)).Returns(environmentValue != null ? environmentValue.Value.ToString() : null);
        }

        [Test]
        public void TrueArgumentAndNullEnvironment_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find Argument variable.");
        }
        [Test]
        public void FalseArgumentAndNullEnvironment_ReturnsFalse() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = false;
            bool? testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find Argument variable.");
        }
        [Test]
        public void NullArgumentAndTrueEnvironment_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable.");
        }
        [Test]
        public void NullArgumentAndFalseEnvironment_ReturnsFalse() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = false;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable.");
        }
        [Test]
        public void NullArgumentAndTrueEnvironmentWithoutPrefix_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable without prefix.");
        }
        [Test]
        public void NullArgumentAndTrueEnvironmentNoPrefixOverload_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            bool? testArgumentValue = null;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, true);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable without prefix.");
        }
        [Test]
        public void TrueArgumentAndTrueEnvironment_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Didn't find variable value from either source.");
        }
        [Test]
        public void TrueArgumentAndFalseEnvironment_ReturnsTrue() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = true;
            bool? testEnvironmentValue = false;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = true;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Explicit argument variable didn't override opposite environment variable.");
        }
        [Test]
        public void FalseArgumentAndTrueEnvironment_ReturnsFalse() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            bool? testArgumentValue = false;
            bool? testEnvironmentValue = true;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = false;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, true);

            Assert.AreEqual(expected, actual, "Explicit argument variable didn't override opposite environment variable.");
        }
    }
}
