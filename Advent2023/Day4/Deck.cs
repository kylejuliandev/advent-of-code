namespace Advent2023.Day4;

public class Deck
{
    private readonly string _deck;
    private static readonly string[] separator = ["\r\n", "\n"];

    public Deck(string deck)
    {
        _deck = deck;
    }

    public int GetTotalScratchCardPoints()
    {
        var cards = _deck.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var sum = 0;
        foreach (var card in cards)
        {
            // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53

            var firstSpace = card.IndexOf(' ');
            var firstColon = card.IndexOf(':');
            var firstPipe = card.IndexOf('|');
            var cardNumber = int.Parse(card[firstSpace..firstColon]);

            var cardWinningNumbers = new List<int>();
            var cardNumbersToMatch = new List<int>();

            var afterColon = firstColon + 1;
            var winningNumbersStr = card[afterColon..firstPipe].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var winningNumberStr in winningNumbersStr)
            {
                cardWinningNumbers.Add(int.Parse(winningNumberStr));
            }

            var afterPipe = firstPipe + 1;
            var numbersToMatchStr = card[afterPipe..].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var numberToMatchStr in numbersToMatchStr)
            {
                cardNumbersToMatch.Add(int.Parse(numberToMatchStr));
            }

            var matches = cardWinningNumbers.Where(cardNumbersToMatch.Contains);
            var matchesCount = matches.Count();

            var cardMatchScore = matchesCount > 0 ? 1 : 0;
            for (var i = 0; i < matchesCount - 1; i++) { cardMatchScore *= 2; }

            sum += cardMatchScore;
        }

        return sum;
    }

    public int GetNumberOfScratchCards()
    {
        // Card number / Number of copies
        var cardCopies = new Dictionary<int, int>();

        var cards = _deck.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        for (var i = 0; i < cards.Length; i++)
        {
            // Include original in copies count
            var currentCardIndex = i + 1;
            if (!cardCopies.TryGetValue(currentCardIndex, out var _))
            {
                cardCopies.Add(currentCardIndex, 1);
            }
            else
            {
                cardCopies[currentCardIndex]++;
            }

            var card = cards[i];
            var firstSpace = card.IndexOf(' ');
            var firstColon = card.IndexOf(':');
            var firstPipe = card.IndexOf('|');
            var cardNumber = int.Parse(card[firstSpace..firstColon]);

            var cardWinningNumbers = new List<int>();
            var cardNumbersToMatch = new List<int>();

            var afterColon = firstColon + 1;
            var winningNumbersStr = card[afterColon..firstPipe].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var winningNumberStr in winningNumbersStr)
            {
                cardWinningNumbers.Add(int.Parse(winningNumberStr));
            }

            var afterPipe = firstPipe + 1;
            var numbersToMatchStr = card[afterPipe..].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var numberToMatchStr in numbersToMatchStr)
            {
                cardNumbersToMatch.Add(int.Parse(numberToMatchStr));
            }

            var matches = cardWinningNumbers.Where(cardNumbersToMatch.Contains);
            var matchCount = matches.Count();

            if (matchCount == 0) continue;
            
            // For every match ensure subsequent card copies count are incremented (for every original+copies on the card we're currently on)
            for (var l = 0; l < cardCopies[currentCardIndex]; l++)
            {
                for (var matchIndex = 1; matchIndex < matchCount + 1; matchIndex++)
                {
                    var matchedCardCopies = currentCardIndex + matchIndex;
                    if (cardCopies.TryGetValue(matchedCardCopies, out var copies))
                    {
                        cardCopies[matchedCardCopies] += 1;
                    }
                    else
                    {
                        cardCopies.Add(matchedCardCopies, 1);
                    }
                }
            }
        }

        return cardCopies.Values.Sum();
    }
}