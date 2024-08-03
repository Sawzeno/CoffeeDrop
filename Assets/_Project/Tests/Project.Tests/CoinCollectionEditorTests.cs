using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
#region ?
// 1st level Is/Has/Does/Contains
// 2nd level All/Not/Some/Exactly
// Or/And/Not
// Is.Unique / Is.Ordered
// Assert.IsTrue
#endregion
public class CoinCollectionEditorTests
{
    [Test]
    public void CoinCollectionEditorTestsSimplePasses()
    {
        string username = "user123";
        Assert.That(username, Does.StartWith("u"));
        Assert.That(username, Does.EndWith("3"));
    }
}
