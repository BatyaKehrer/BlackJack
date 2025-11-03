# BlackJack Game

A console-based BlackJack game implementation in C# (.NET 8.0) featuring multiple players, customizable decks, and advanced gameplay options.

## Features

### Core Game Rules
- Standard BlackJack gameplay with hit, stand, and dealer logic
- Dealer hits on 16 or less, stands on 17 or higher
- BlackJack detection (21 with exactly 2 cards)
- Bust detection and automatic hand resolution
- Ace value calculation (11 or 1, whichever is more favorable)

### Additional Features

1. **Card Splitting**
   - Players can split pairs when they receive two cards of the same value
   - Split hands are played independently
   - Special rule: When splitting Aces, each hand receives one card and automatically stands

2. **Multiple Players Support**
   - Supports 1-9 players simultaneously
   - Each player has an individual bankroll and plays independently

3. **Configurable Deck Count**
   - Default: 6 decks
   - Customizable from 1-8 decks at game start
   - More decks reduce card counting effectiveness and provide longer gameplay sessions

4. **Automatic Deck Reshuffling**
   - Deck automatically reshuffles when it falls below 20% capacity
   - Ensures continuous gameplay without manual intervention

5. **Bankroll Management**
   - Each player starts with $1,000
   - Players can bet any amount up to their current bankroll
   - Players with depleted bankrolls are automatically removed from the game

6. **Enhanced Payouts**
   - BlackJack (natural 21): 2.5x payout (1.5x profit)
   - Regular win: 2x payout (1x profit)
   - Stand-off (push): Bet returned

7. **Multiple Hands Per Player**
   - Players can manage multiple hands simultaneously when splitting
   - Each hand is evaluated independently against the dealer

## Building and Running

### Prerequisites
- .NET 8.0 SDK or later

### Build Instructions
```bash
dotnet build
```

### Run Instructions
```bash
dotnet run
```

Alternatively, you can run the compiled executable directly:
```bash
dotnet bin/Debug/net8.0/BlackJack.dll
```

### Game Setup
1. Choose the number of decks (1-8) - default is 6
2. Enter the number of players (1-9)
3. Each player starts with $1,000

### Gameplay
1. Place your bet (must be between $1 and your current bankroll)
2. Cards are dealt: each player receives 2 cards, dealer receives 2 cards (one hidden)
3. Players take turns:
   - Hit (H) - Receive another card
   - Stand (S) - End your turn
   - Split (P) - Split pairs if available (requires sufficient bankroll)
4. Dealer plays automatically after all players complete their turns
5. Winnings/losses are calculated and added to/deducted from bankrolls
6. Game continues until all players leave (either voluntarily or by running out of money)

