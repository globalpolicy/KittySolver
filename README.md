# KittySolver
A naive Kitty card game optimizer

Here's how it works, briefly:
- Creates a list of all possible arrangements/orderings of the cards in the given hand (that comes to be 9! i.e. `362,880`)
- Not all of these combinations are unique as far as the game is concerned
- Each hand contains 3 sets of cards, each set consisting of 3 cards
- The ordering of the cards in a set doesn't matter, so each set, when created automatically sorts the given cards in descending order
- Scores each set of each hand of the arrangements according to the rules of the game, the score is a vector of 5 different possible sequences: `[trial_score, falash_score, run_score, jute_score, badhi_score]`
- Care should be taken while calculating each member of this vector such that the priority order within the sequence in question is respected, for e.g. while calculating the `run_score` for a set, the score for a `A-K-Q` run must be higher than an `A-2-3` run; calculating the `jute_score` for a set, the score for a `2-2-A` should be lower than `3-3-5`, and so on.
- A scalar score is also calculated from this score vector wherein the elements of the vector are added with suitable weights such that the priority order of the different sequences (run vs. trial vs jute, etc.) are respected
- To get the highest scoring arrangement, the list of all possible arrangements (hands) are sorted by the simple sum of the scalar scores of all three sets of each hand
- For the best scoring hand, the highest scoring sets are placed toward the front, which is the final result

>Note: Requires .NET Framework 4.8
