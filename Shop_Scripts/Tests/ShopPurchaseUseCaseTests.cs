using NUnit.Framework;

namespace CleanRefactor.Tests
{
    public class ShopPurchaseUseCaseTests
    {
        private ShopConfig CreateConfig()
        {
            return new ShopConfig(
                100, // bombCost
                3,   // bombMaxUses
                150, // shieldCost
                2,   // shieldMaxUses
                300, // doubleCoinsCost
                5    // doubleCoinsRequiredLevel
            );
        }

        [Test]
        public void When_BuyBombWithEnoughCoins_Expect_PurchaseSucceeds()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 1,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyBombUseCase useCase = new BuyBombUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.Purchased, result.Status);
            Assert.AreEqual(400, result.Coins);
            Assert.AreEqual(400, storage.Load().Coins);
            Assert.AreEqual(1, storage.Load().BombUses);
            Assert.IsTrue(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyBombWithoutEnoughCoins_Expect_NotEnoughCoins()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 50,
                PlayerLevel = 1,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyBombUseCase useCase = new BuyBombUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.NotEnoughCoins, result.Status);
            Assert.AreEqual(50, result.Coins);
            Assert.AreEqual(50, storage.Load().Coins);
            Assert.AreEqual(0, storage.Load().BombUses);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyBombAtMaxUses_Expect_MaxUsesReached()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 1,
                BombUses = 3,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyBombUseCase useCase = new BuyBombUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.MaxUsesReached, result.Status);
            Assert.AreEqual(500, result.Coins);
            Assert.AreEqual(500, storage.Load().Coins);
            Assert.AreEqual(3, storage.Load().BombUses);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyShieldWithEnoughCoins_Expect_PurchaseSucceeds()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 1,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyShieldUseCase useCase = new BuyShieldUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.Purchased, result.Status);
            Assert.AreEqual(350, result.Coins);
            Assert.AreEqual(350, storage.Load().Coins);
            Assert.AreEqual(1, storage.Load().ShieldUses);
            Assert.IsTrue(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyShieldWithoutEnoughCoins_Expect_NotEnoughCoins()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 100,
                PlayerLevel = 1,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyShieldUseCase useCase = new BuyShieldUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.NotEnoughCoins, result.Status);
            Assert.AreEqual(100, result.Coins);
            Assert.AreEqual(100, storage.Load().Coins);
            Assert.AreEqual(0, storage.Load().ShieldUses);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyShieldAtMaxUses_Expect_MaxUsesReached()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 1,
                BombUses = 0,
                ShieldUses = 2,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyShieldUseCase useCase = new BuyShieldUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.MaxUsesReached, result.Status);
            Assert.AreEqual(500, result.Coins);
            Assert.AreEqual(500, storage.Load().Coins);
            Assert.AreEqual(2, storage.Load().ShieldUses);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyDoubleCoinsWithEnoughCoinsAndRequiredLevel_Expect_PurchaseSucceeds()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 5,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyDoubleCoinsUseCase useCase = new BuyDoubleCoinsUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.Purchased, result.Status);
            Assert.AreEqual(200, result.Coins);
            Assert.AreEqual(200, storage.Load().Coins);
            Assert.IsTrue(storage.Load().HasDoubleCoins);
            Assert.IsTrue(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyDoubleCoinsWithoutEnoughCoins_Expect_NotEnoughCoins()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 200,
                PlayerLevel = 5,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyDoubleCoinsUseCase useCase = new BuyDoubleCoinsUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.NotEnoughCoins, result.Status);
            Assert.AreEqual(200, result.Coins);
            Assert.AreEqual(200, storage.Load().Coins);
            Assert.IsFalse(storage.Load().HasDoubleCoins);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyDoubleCoinsBelowRequiredLevel_Expect_RequiredLevelNotReached()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 4,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = false
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyDoubleCoinsUseCase useCase = new BuyDoubleCoinsUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.RequiredLevelNotReached, result.Status);
            Assert.AreEqual(500, result.Coins);
            Assert.AreEqual(500, storage.Load().Coins);
            Assert.IsFalse(storage.Load().HasDoubleCoins);
            Assert.IsFalse(storage.SaveWasCalled);
        }

        [Test]
        public void When_BuyDoubleCoinsAlreadyOwned_Expect_AlreadyOwned()
        {
            PlayerShopState initialState = new PlayerShopState
            {
                Coins = 500,
                PlayerLevel = 5,
                BombUses = 0,
                ShieldUses = 0,
                HasDoubleCoins = true
            };

            FakeShopStorage storage = new FakeShopStorage(initialState);
            BuyDoubleCoinsUseCase useCase = new BuyDoubleCoinsUseCase(storage, CreateConfig());

            PurchaseResultDto result = useCase.Execute();

            Assert.AreEqual(PurchaseStatus.AlreadyOwned, result.Status);
            Assert.AreEqual(500, result.Coins);
            Assert.AreEqual(500, storage.Load().Coins);
            Assert.IsTrue(storage.Load().HasDoubleCoins);
            Assert.IsFalse(storage.SaveWasCalled);
        }
    }
}