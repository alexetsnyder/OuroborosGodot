using System.Collections.Generic;

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
        Pair = 1,
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
        
        if (IsFiveOfAKind(dice))
        {
            return DiceScoreCombo.FiveOfAKind;
        }
        else if (IsFourOfAKind(dice))
        {
            return DiceScoreCombo.FourOfAKind;
        }
        else if (IsFullHouse(dice))
        {
            return DiceScoreCombo.FullHouse;
        }
        else if (IsStraight(dice))
        {
            return DiceScoreCombo.Straight;
        }
        else if (IsThreeOfAKind(dice))
        {
            return DiceScoreCombo.ThreeOfAKind;
        }
        else if (IsTwoPair(dice))
        {
            return DiceScoreCombo.TwoPair;
        }
        else if (IsPair(dice))
        {
            return DiceScoreCombo.Pair;
        }
        else
        {
            return DiceScoreCombo.None;
        }
    }

    public static bool IsFiveOfAKind(List<DiceType> dice)
    {
        for (int i = 0; i < dice.Count - 4; i++)
        {
            int match = 1;
            for (int j = i + 1; j < dice.Count; j++)
            {
                if (dice[i] == dice[j])
                {
                    match++;
                }
            }

            if (match == 5)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsFourOfAKind(List<DiceType> dice)
    {
        for (int i = 0; i < dice.Count - 3; i++)
        {
            int match = 1;
            for (int j = i + 1; j < dice.Count; j++)
            {
                if (dice[i] == dice[j])
                {
                    match++;
                }
            }

            if (match == 4)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsFullHouse(List<DiceType> dice)
    {
        var threeOfAKindValue = DiceType.None;

        for (int i = 0; i < dice.Count - 2; i++)
        {
            for (int j = i + 1; j < dice.Count; j++)
            {
                for (int k = j + 1; k < dice.Count; k++)
                {
                    if (dice[i] == dice[j] && dice[j] == dice[k])
                    {
                        threeOfAKindValue = dice[i];
                        break;
                    }
                }
            }
        }

        if (threeOfAKindValue != DiceType.None)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = i + 1; j < dice.Count; j++)
                {
                    if (threeOfAKindValue != dice[i] && dice[i] == dice[j])
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool IsStraight(List<DiceType> dice)
    {
        if (dice.Count > 0)
        {
            var firstValue = dice[0];
            for (int i = 1; i < dice.Count; i++)
            {
                if (firstValue != dice[i] - i)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public static bool IsThreeOfAKind(List<DiceType> dice)
    {
        for (int i = 0; i < dice.Count - 2; i++)
        {
            int match = 1;
            for (int j = i + 1; j < dice.Count;  j++)
            {
                if (dice[i] == dice[j])
                {
                    match++;
                }
            }

            if (match == 3)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsTwoPair(List<DiceType> dice)
    {
        var firstPair = DiceType.None;

        for (int i = 0; i < dice.Count; i++)
        {
            for (int j = i + 1; j < dice.Count; j++)
            {
                if (dice[i] == dice[j])
                {
                    firstPair = dice[i];
                    break;
                }
            }
        }

        if (firstPair != DiceType.None)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = i + 1; j < dice.Count; j++)
                {
                    if (firstPair != dice[i] && dice[i] == dice[j])
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool IsPair(List<DiceType> dice)
    {
        for (int i = 0; i < dice.Count; i++)
        {
            for (int j = i + 1; j < dice.Count; j++)
            {
                if (dice[i] == dice[j])
                {
                    return true;
                }
            }
        }

        return false;
    }  
}
