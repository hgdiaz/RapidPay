namespace Tests
{
    public class CardServiceTest
    {
        private DataContext _context;

        [Fact]
        public async Task WhenGetAllCards_ThenConsolesAreReturnedSuccessfully()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("testDB1")
                .Options;

            _context = new DataContext(options);

            _context.Cards.Add(new Card
            {
                Number = "111111111111111",
                CardHolderName = "Person 1",
                ExpirationMonth = 1,
                ExpirationtYear = 2024,
                CVC = "123",
                Balance = 10000
            });

            await _context.SaveChangesAsync();

            //When
            var cards = await new CardService(_context).GetAllCardsAsync();

            //Then
            Assert.NotNull(cards);

            Assert.True(cards.Any());
        }

        [Fact]
        public async Task WhenGetCardByNumber_ThenSpecificCardsIsReturned()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("testDB2")
                .Options;

            _context = new DataContext(options);

            _context.Cards.Add(new Card
            {
                Number = "222222222222222",
                CardHolderName = "Person 2",
                ExpirationMonth = 1,
                ExpirationtYear = 2024,
                CVC = "123",
                Balance = 10000
            });

            await _context.SaveChangesAsync();

            //When
            var console = await new CardService(_context).GetByNumber("222222222222222");

            await _context.SaveChangesAsync();

            //Then
            Assert.NotNull(console);

            Assert.True(console.CardHolderName == "Person 2");
        }
    }
}