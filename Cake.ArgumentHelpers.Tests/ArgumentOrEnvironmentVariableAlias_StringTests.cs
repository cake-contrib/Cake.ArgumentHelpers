using NUnit.Framework;
using Cake.Core;
using Moq;

namespace Cake.ArgumentHelpers.Tests {
    [TestFixture()]
    public class ArgumentOrEnvironmentVariableAlias_StringTests {
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

        void SetupVariables(string key, string environmentPrefix, string argumentValue, string environmentValue) {
            bool hasArgument = argumentValue != null;
            cakeArgumentsMock.Setup(x => x.HasArgument(key)).Returns(hasArgument);
            if (hasArgument) {
                cakeArgumentsMock.Setup(x => x.GetArgument(key)).Returns(argumentValue.ToString());
            }
            bool hasEnvironmentVariable = environmentValue != null;
            if (hasEnvironmentVariable)
            {
                cakeEnvironmentMock.Setup(x => x.GetEnvironmentVariable(environmentPrefix + key)).Returns(environmentValue);
            }
        }

        [Test]
        public void SomeArgumentAndNullEnvironment_ReturnsSome() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = "Some";
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testArgumentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.AreEqual(expected, actual, "Didn't find Argument variable.");
        }
        [Test]
        public void NullArgumentAndNullEnvironmentAndNullDefault_ReturnsNull() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = (string)null;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.AreEqual(expected, actual, "Found unexpected variable value.");
        }
        [Test]
        public void NullArgumentAndSomeEnvironment_ReturnsSome() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = "Some";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testEnvironmentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable.");
        }
        [Test]
        public void NullArgumentAndSomeEnvironmentWithoutPrefix_ReturnsSome() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = (string)null;
            string testArgumentValue = null;
            string testEnvironmentValue = "Some";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testEnvironmentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.AreEqual(expected, actual, "Didn't find Environment variable without prefix.");
        }
        [Test]
        public void SomeArgumentAndOtherEnvironment_ReturnsSome() {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = "Some";
            string testEnvironmentValue = "Other";

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = testArgumentValue;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix, (string)null);

            Assert.AreEqual(expected, actual, "Didn't find correct variable value from Argument source.");
        }
        [Test]
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

            Assert.AreEqual(expected, actual, "Didn't fall back on default value.");
        }
        [Test]
        public void NullArgumentAndNullEnvironmentWithoutDefault_ReturnsNull()
        {
            var testKey = "someVariable";
            var testKeyEnvironmentPrefix = "somePrefix_";
            string testArgumentValue = null;
            string testEnvironmentValue = null;

            SetupVariables(testKey, testKeyEnvironmentPrefix, testArgumentValue, testEnvironmentValue);

            var expected = (string)null;
            var actual = cakeContextMock.Object.ArgumentOrEnvironmentVariable(testKey, testKeyEnvironmentPrefix);

            Assert.AreEqual(expected, actual, "Didn't fail to a null value.");
        }
    }
}
