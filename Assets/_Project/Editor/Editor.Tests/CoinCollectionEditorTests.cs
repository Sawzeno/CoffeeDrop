using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Utils;

using ToBeChanged;

// N substitute lib can work with any class or interface that has virtual methods
// Created substitute can be use as a mock or as a spy

// SPY -> A test Double which reords information about how it was used such as tracking method calls or property accesses
// MOCK-> An Object testing simulates the behaviour of a real object, desgined to meet certain expectations set in the test such as method calls and return values

namespace Editor.Tests
{
    public class CoinCollectionEditorTests
    {
        ICoinController Controller;
        ICoinModel Model;
        ICoinView View;
        ICoinService Service;

        [SetUp]
        public void SetUp()
        {
            View = Substitute.For<ICoinView>();
            Model = Substitute.For<ICoinModel>();
            Service = Substitute.For<ICoinService>();

            Assert.That(View, Is.Not.Null);
            Assert.That(Model, Is.Not.Null);
            Assert.That(Service, Is.Not.Null);

            Model.Coins.Returns(new Observable<int>(0));
            Assert.That(Model.Coins, Is.Not.Null);              // these both are same
            Assert.That(Model, Has.Property("Coins").Not.Null);  // these both are same
            Service.Load().Returns(Model);
            Controller = new CoinController.Builder().WithService(Service).Build(View);
        }
        [TearDown]
        public void TearDown() { }
        [Test]
        public void CoinControllerBuilder_Build_ShouldThrowArgumentNullException_WhenViewIsNull()
        {
            Assert.That(() => new CoinController.Builder().Build(null), Throws.ArgumentNullException);
        }
        [Test]
        public void CoinControllerBuilder_Build_ShouldThrowArgumentNullException_WhenServiceIsNull()
        {
            Assert.That(() => new CoinController.Builder().WithService(null).Build(View), Throws.ArgumentNullException);
        }
        [Test]
        public void UpdateView_ShouldUpdateCoinsDisplay_WhenCoinsAreCollected()
        {
            Controller.Collect(1);
            View.Received().UpdateCoinsDisplay(1);
        }
        [TestCase(5, 5, 10)]
        [TestCase(0, 5, 5)]
        [TestCase(0, 0, 0)]
        public void Collect_ShouldAddCoins_WhenCalledWithAPositiveNumber(int initialCoins, int CoinsToAdd, int expectedCoins)
        {
            Model.Coins.Returns(new Observable<int>(initialCoins));
            Controller.Collect(CoinsToAdd);
            Assert.That(Model.Coins.Value, Is.EqualTo(expectedCoins));
        }
    }
}
