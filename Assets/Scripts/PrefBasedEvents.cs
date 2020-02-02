using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefBasedEvents : UnitySingletonPersistent<PrefBasedEvents>
{
    public Dictionary<string, List<List<string>>> PrefBasedEventsandOutcomes = new Dictionary<string, List<List<string>>>() {
        //string: preference, 
        // index 0: both like good outcome
        //index 1: one like one dislike, bad outcome 
        { "cats", new List<List<string>> {
            new List<string> {
                "* and ^ had their #! date at a cat cafe. They pet a lot of cats!!",
                 },
            new List<string> {
                "* and ^ had their #! date at a cat cafe. A cat bit both of them. They were not happy.",
                } }
        },
        { "dogs", new List<List<string>> {
            new List<string> {
                "* and ^ passed by a dog shelter on their #! date. They realized they really like dogs and each other.",},
            new List<string> {
                "* and ^ watched a dog movie together. One of them enjoyed it and adopted a dog, the other one had PTSD." }
            }
        },
        { "star_wars", new List<List<string>> {
            new List<string> {
                "* and ^ watched: Empire Strikes Back. They ate a lot of piazza. They had sex." },
            new List<string> {
                "* and ^ watched: Empire Strikes Back. One of them fell asleep." }
            }
        },
        { "veganism", new List<List<string>> {
            new List<string> {
                "* and ^ went to a vegan restaurant for their #! date.",},
            new List<string> {
                "* and ^ went to a vegan restaurant for their #! date.", }
            }
        },
        { "activism", new List<List<string>> {
            new List<string> {
                "* and ^ joined a environmental protection march for their #! date.", },
            new List<string> {
                "* and ^ joined a environmental protection march for their #! date" }
            }
        },
        { "politics", new List<List<string>> {
            new List<string> {
                "* and ^ watched to 2030 Presidential Election togther for their #! date. Their ideal candidate won.", },
            new List<string> {
                "* and ^ watched to 2030 Presidential Election togther for their #! date. One of them fell asleep." }
            }
        },
        { "working", new List<List<string>> {
            new List<string> {
                "* and ^ watched came up with a business plan on their #! date. They received $1 million dollar in investment funds next day.", },
            new List<string> {
                "* and ^ had their #! date in a office building becuase one of them refuse to stop working." }
            }
        },
        { "pineapples_on_pizza", new List<List<string>> {
            new List<string> {
                "* and ^ had Pineapples on pizza for their #! date.", },
            new List<string> {
                "* and ^ had Pineapples on pizza for their #! date.",}
            }
        },
        { "traveling", new List<List<string>> {
            new List<string> {
                "* and ^ travelled to a neighboring city for their #! date." },
            new List<string> {
                "* and ^ discuss future travelling plan on their #! date. One of them wanted to go to the Artic. The other one wanted to watch Netflex." }
            }
        },
        { "video_games", new List<List<string>> {
            new List<string> {
                "* and ^ played The First of Us by Obedient Cats on their #! date." },
            new List<string> {
                "* and ^ played The First of Us by Obedient Cats on their #! date."},
            }
        },
        { "kids", new List<List<string>> {
            new List<string> {
                "* and ^ realize they both wanted to have kids on their #! date." },
            new List<string> {
                "* and ^ ran in to a naughty boy on their #! date. He dropped a bucket of water on them.", }
            }
        },
        { "long_term", new List<List<string>> {
            new List<string> {
                "* and ^ talked about their future plan on their #! date. They both mentiond each other in their plan." },
            new List<string> {
                "* and ^ talked about their future plan on their #! date.", }
            }
        },
        { "extroverted", new List<List<string>> {
            new List<string> {
                "* and ^ had a great conversation on their #! date. They both talked a lot and are willing to share.",  },
            new List<string> {
                "* and ^ try to have a conversation on their #! date. But one of them is very shy.", }
            }
        },
        { "reading", new List<List<string>> {
            new List<string> {
                "* and ^ went to a book club for their #! date. ", },
            new List<string> {
                "* and ^ went to a book club for their #! date.",}
            }
        },
        { "cars", new List<List<string>> {
            new List<string> {
                "* and ^ went to see a car race for their #! date", },
            new List<string> {
                "* and ^ went to see a car race for their #! date", }
            }
        },
        { "college", new List<List<string>> {
            new List<string> {
                "* and ^ had their #! date during a programming lecture.", },
            new List<string> {
                 "* and ^ had their #! date during a programming lecture.",
                 }
            }
        },
        { "sports", new List<List<string>> {
            new List<string> {
                 "* and ^ watched Super Ball for their #! date.", },
            new List<string> {
                 "* and ^ watched Super Ball for their #! date. Sadly, one of them didn't know the rules of football.", }
            }
        },
        { "rich", new List<List<string>> {
            new List<string> {
                "* and ^ went to a fancy hotel for their #! date." },
            new List<string> {
                "* and ^ went to a fancy hotel for their #! date.", }
            }
        },
        { "weed", new List<List<string>> {
            new List<string> {
                "* and ^ smoked weed on their #! date.", },
            new List<string> {
                "* and ^ smoked weed on their #! date. Both of them are arrested.", }
            }
        },
        { "anime", new List<List<string>> {
            new List<string> {
                "* and ^  watched anime together for their #! date.", },
            new List<string> {
                 "* and ^  watched anime together for their #! date. One of them feel uncomfortable.", }
            }
        }
    };
}
