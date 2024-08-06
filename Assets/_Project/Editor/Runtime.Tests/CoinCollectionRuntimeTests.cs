using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Utils;

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
    // public class TestVisitor : MonoBehaviour, IVisitor{
    //     public bool Visited{get; private set;}
    //     public bool Visit<T>(T visitable) where T : Component, IVisitable{
    //         Visited = true;
    //     }
    // }
}
