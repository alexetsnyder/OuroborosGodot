using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Dice
{
    public enum DiceType
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
    }

    public enum DiceScoreCombo
    {
        None = 0,
        TwoOfAKind = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7,
    }

    public static DiceScoreCombo Evaluate(List<DiceType> dice)
    {
        dice.Sort();
        //Regex fiveOfAKind = new Regex(@"[1-6]{5,}");
        //Regex fourOfAKind = new Regex(@"[1-6]{4,}");
        //Regex threeOfAKind = new Regex(@"[1-6]{3}");
        Regex twoOfAKind = new Regex(@"([1-6]+)(?=\1{1})");

        if (twoOfAKind.IsMatch(GetDiceString(dice)))
        {
            return DiceScoreCombo.TwoOfAKind;
        }

        return DiceScoreCombo.None;
    }

    private static string GetDiceString(List<DiceType> dice)
    {
        string diceStr = "";
        foreach (var die in dice)
        {
            diceStr += (int)die;
        }
        return diceStr;
    }
}
