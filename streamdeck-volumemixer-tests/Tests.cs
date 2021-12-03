using System;
using System.Linq;
using System.Security.AccessControl;
using NUnit.Framework;
using streamdeck_volumemixer.Internal;

namespace streamdeck_volumemixer_tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestGetAudioApplicationNames()
        {
            foreach (var applicationName in WindowsCoreAudioWrapper.EnumerateApplications())
            {
                Assert.NotNull(applicationName);
                Assert.IsNotEmpty(applicationName);
                Console.WriteLine(applicationName);
            }
        }     
        
        [Test]
        public void TestAvailableApplicationName()
        {
            Assert.AreEqual("firefox.exe", new AudioApplication("C:\\Program Files\\Mozilla Firefox\\firefox.exe").ApplicationName);
        }
    }
}