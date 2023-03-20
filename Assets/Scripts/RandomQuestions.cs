using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuestions : MonoBehaviour
{
    private List<GroupOfStrings> list_groupOS = new List<GroupOfStrings>();
    private GroupOfStrings currentGroup;

    private void Awake()
    {
        list_groupOS.Add(new GroupOfStrings());
        CreatNewListOfStrings();
    }
    public List<GroupOfStrings> ListGroupOS()
    {
        return list_groupOS;
    }
    public GroupOfStrings GroupOfStringsSelected(int index)
    {
        return list_groupOS[index];
    }
    private void CreatNewListOfStrings()
    {
        list_groupOS.Add(new GroupOfStrings());
        currentGroup = list_groupOS[list_groupOS.Count - 1];

        /////////////////////////// FIRST CONTENT //////////////////////////////
        currentGroup.titles = new string[] {
            " equation issues",
            " got numbers to deal with."
        };

        currentGroup.firstPart = new string[] {
            " his cat teacher has given the problem: ",
        " trying to get the answer of: ",
        " his friend asked the answer of: "
        };

        currentGroup.secondPart = new string[] {
            " the poor kitty don't know the result... "
        };

        currentGroup.conclusion = new string[] {
            " What is the result of this equation?"
        };

        currentGroup.difficulty = "Medium";

        ///////////////////////////////// FIRST CONTENT ///////////////////////////////
        
        list_groupOS.Add(new GroupOfStrings());
        currentGroup = list_groupOS[list_groupOS.Count - 1];

        /////////////////////////// SECOND CONTENT //////////////////////////////
        currentGroup.titles = new string[] {
            " gamin' issues",
            " has a gamin' problem."
        };

        currentGroup.firstPart = new string[] {
            " was playing with his friend, and scored ",
        " was trying to win his meow partner, but only scored ",
        " attempted to score more than his brother, he scored "
        };

        currentGroup.secondPart = new string[] {
            " points, but his brother had "
        };

        currentGroup.conclusion = new string[] {
            " how many does he have more than his brother?"
        };

        currentGroup.difficulty = "Easy";

        ///////////////////////////////// SECOND CONTENT ///////////////////////////////
        
        list_groupOS.Add(new GroupOfStrings());
        currentGroup = list_groupOS[list_groupOS.Count - 1];

        /////////////////////////// THIRD CONTENT //////////////////////////////
        currentGroup.titles = new string[] {
            " Physical issues",
            " is on high velocity",
            " meowmeter speed"
        };

        currentGroup.firstPart = new string[] {
            " is a physicist, saw a car and tried to calculate his velocity: ",
        " is a cop and is trying to analyze if a car has exceed speed: ",
        " was driving his meow meow car, and was on speed: "
        };

        currentGroup.secondPart = new string[] {
            " the kitty need some help. ",
            " this little cat can't guess what's the speed. "
        };

        currentGroup.conclusion = new string[] {
            " What is the car's velocity?"
        };

        currentGroup.difficulty = "Hard";

        ///////////////////////////////// THIRD CONTENT ///////////////////////////////
    }
}
public class GroupOfStrings // CREATE A GROUP OF PUBLIC LISTS WITH DIFFERENT CONTEXTS.
{
    public string[] catNames = {
        "Mr Meows a lot",
        "Bad Cat",
        "Kitty cat",
        "Sir Whiskerbottom III",
        "Princess Fluffybutt",
        "Captain Meowser",
        "Mister Fuzzums",
        "Baron von Scratchington",
        "Duchess Snugglesworth",
        "Emperor Purrfect",
        "Lady Meowington",
        "Countess Clawdia",
        "Lord Snuggles"
    };

    public string[] titles = {
        " problem!",
        " has a problem.",
        " is pissed off!",
        " is feeling cranky.",
        " is in a bad mood.",
        " is not happy.",
        " is upset.",
        " is grumpy today.",
        " is fuming."
    };

    public string[] firstPart = {
        " thought he could handle selling fruit, he had a total of ",
        " was trying to sell fruit, he had a total of ",
        " attempted to sell some fruit, but he only had ",
        " decided to sell some fruit, he had a grand total of ",
        " was selling fruit, he had ",
        " thought he could make some money selling fruit, he had a total of ",
        " tried to sell some fruit, he had a meager total of "
    };

    public string[] secondPart = {
        " in pears, but he had sold only ",
        " in apples, but he had sold only ",
        " in bananas, but he had sold only ",
        " in oranges, but he had sold only ",
        " in grapes, but he had sold only ",
        " in watermelons, but he had sold only ",
        " in peaches, but he had sold only ",
        " in cherries, but he had sold only ",
        " in strawberries, but he had sold only ",
        " in pineapples, but he had sold only "
    };

    public string[] conclusion = {
        " how many does he have now?",
        " how many does he still have?"
    };

    public string difficulty = "Medium";
}
