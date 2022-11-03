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

### Screenshots:

![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEia6JHJMtIGJfTqk6qSoMve6fY0jnzt_7_19GHuCQfd-QFRk3QDxEItQRhryB15Wr-7S-hPfhOMfKeZ33UAQ1hJuwAE_idtu2CNgF_Vd7j6lN1CdSeR3nD4w-Vb8sO1gRq5jorqWNzpdH2RFxcH4XO3p9sDvK-u11qPMNdPIyqvBhePiyzsdIEfUZYG/s752/1.png)
![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgOkcxRnOSm-2-OYYDCDr0Qn3HFU2IrQTf_PaW7V2kstukgO-h8gmk0Coj5Tg_Ky3GNaXnhl2D_7oofnt53S-jLTlSAMM8_3MI3_XRcMIJZ26PX7_soTfRD_XcL9X8cnprMIvo2gBrbTBgw4tSkcQ30C6x9vX23ix23gsYdDx_eX2DHl5V1t0wk4N0U/s752/2.png)
![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEjzXMu9praCJ-jy9-Nk_fcXu04XLdAWD25303M1zwOrIu3A6Ck9wvqzmz2_XPo1bpnjLZaeSfaQ1HBi5mleSYz-UlSzw6oD5RB5PDRPmVDqAMvGAxMaJ9WJ2VRcLiAmIImPrOXHw6mriff54rhzlPl9Y0A-gemtbxlUpHLT1SzN4BWLe3B-amyaZfmw/s752/3.png)
![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEibn22vQeiQRLkLxUhMNOF8IlHNAYAPNRn86B0V5B3Akr1y232jk2iPLkyT2ky8NH2z3CtduRawVng9XY0prfisY6qWd32Ihcq3Zy7VxR4A6p5vkkILlVIU69_bqiG0xQwGCdtWczpPocpzTjV0IuOk2OXyls0PEoIfu6ccVtsOgIL1f0doRy-HVtP1/s752/4.png)
>Note: Requires .NET Framework 4.8
