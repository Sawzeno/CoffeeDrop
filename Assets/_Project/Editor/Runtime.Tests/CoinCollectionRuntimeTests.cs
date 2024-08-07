using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Tests
{
    public class CoinCollectionRuntimeTests
    {
        [Test]
        public void VerifyApplicationPlaying()
        {
            Assert.That(Application.isPlaying, Is.True);
        }
        [Test]
        public void VerifyScene(){
            var go = GameObject.Find("Collection System");
            Assert.That(go, Is.Not.Null, $"Collection System not found in {0}",SceneManager.GetActiveScene().path);
        }
    }
}
