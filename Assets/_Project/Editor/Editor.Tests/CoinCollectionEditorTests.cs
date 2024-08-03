using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests
{
    public class CoinCollectionEditorTests
    {
        [Test]
        public void CoinCollectionEditorTestsSimplePasses()
        {
            string username = "user123";
            Assert.That(username.StartsWith("u"));
            Assert.That(username.EndsWith("3"));

            var list = new List<int>(5){1, 2, 3,4, 5};
            Assert.That(list,Contains.Item(1));
            Assert.That(list,Contains.Item(2));
        }
    }
}
