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
                "* and ^ had their #! date at a cat cafe. They had a great time petting lots of cats.",
                 },
            new List<string> {
                "* and ^ had their #! date at a cat cafe. A cat bit both of them. They were not happy.",
                } }
        },
        { "dogs", new List<List<string>> {
            new List<string> {
                "* and ^ passed by a dog shelter on their #! date. They realized they really liked dogs and each other.",},
            new List<string> {
                "* and ^ watched a dog movie together. One of them enjoyed it and adopted a dog; the other had PTSD." }
            }
        },
        { "star_wars", new List<List<string>> {
            new List<string> {
                "* and ^ watched Star Wars: The Empire Strikes Back. They Netflixed... and chilled." },
            new List<string> {
                "* and ^ watched Star Wars: The Empire Strikes Back. One of them fell asleep." }
            }
        },
        { "veganism", new List<List<string>> {
            new List<string> {
                "* and ^ went to a vegan restaurant for their #! date. It was so good they ordered kale for dessert.",},
            new List<string> {
                "* and ^ went to a vegan restaurant for their #! date. One of them got food poisoning.", }
            }
        },
        { "activism", new List<List<string>> {
            new List<string> {
                "* and ^ joined an environmental march for their #! date. They felt like they grew together.", },
            new List<string> {
                "* and ^ joined an environmental march for their #! date. One of them got bored and left early.", }
            }
        },
        { "politics", new List<List<string>> {
            new List<string> {
                "* and ^ watched the 2030 Presidential Election together for their #! date. They cheered when their candidate won.", },
            new List<string> {
                "* and ^ watched the 2030 Presidential Election together for their #! date. One of them fell asleep." }
            }
        },
        { "working", new List<List<string>> {
            new List<string> {
                "* and ^ came up with a business plan on their #! date. They received $1 million in investment funds the next day.", },
            new List<string> {
                "* and ^ had their #! date in a office building becuase one of them refused to stop working. It didn't go well." }
            }
        },
        { "pineapples_on_pizza", new List<List<string>> {
            new List<string> {
                "* and ^ had pineapples on pizza for their #! date. They ate the whole thing, and promised to order it more often.", },
            new List<string> {
                "* and ^ had pineapples on pizza for their #! date. They fought over whether it tasted good or disgusting.", }
            }
        },
        { "traveling", new List<List<string>> {
            new List<string> {
                "* and ^ travelled to a neighboring city for their #! date. They loved the area and went to local shops." },
            new List<string> {
                "* and ^ discussed travelling on their #! date. One of them wanted to go to the Arctic. The other one wanted to watch Netflix." }
            }
        },
        { "video_games", new List<List<string>> {
            new List<string> {
                "* and ^ played The First of Us by Obedient Cats on their #! date. They bonded over the characters." },
            new List<string> {
                "* and ^ played The First of Us by Obedient Cats on their #! date. One of them wasn't really interested."},
            }
        },
        { "kids", new List<List<string>> {
            new List<string> {
                "* and ^ realized they both wanted to have kids on their #! date. They talked about baby names." },
            new List<string> {
                "* and ^ talked about children on their #! date. They disagreed on whether children were demons or angels.", }
            }
        },
        { "long_term", new List<List<string>> {
            new List<string> {
                "* and ^ talked about their future plans on their #! date. They both mentioned each other in their plans." },
            new List<string> {
                "* and ^ talked about their future plans on their #! date. Neither mentioned the other.", }
            }
        },
        { "extroverted", new List<List<string>> {
            new List<string> {
                "* and ^ had a great conversation on their #! date. They both talked a lot and were willing to share.",  },
            new List<string> {
                "* and ^ tried to have a conversation on their #! date. But one of them was very shy.", }
            }
        },
        { "reading", new List<List<string>> {
            new List<string> {
                "* and ^ went to a book club for their #! date. They had deep discussions about the book.", },
            new List<string> {
                "* and ^ went to a book club for their #! date. One of them didn't read the book and felt awkward.", }
            }
        },
        { "cars", new List<List<string>> {
            new List<string> {
                "* and ^ went to a racetrack for their #! date. They cheered as the cars rounded each lap.", },
            new List<string> {
                "* and ^ went to a racetrack for their #! date. One of them hated how loud it was.", }
            }
        },
        { "college", new List<List<string>> {
            new List<string> {
                "* and ^ had their #! date during a programming lecture. They bonded over broken code.", },
            new List<string> {
                "* and ^ had their #! date during a programming lecture. One of them was annoyed by the other.", }
            }
        },
        { "sports", new List<List<string>> {
            new List<string> {
                "* and ^ watched the Super Bowl for their #! date. They both got really into the game.", },
            new List<string> {
                "* and ^ watched the Super Bowl for their #! date. Sadly, one of them didn't know the rules of football.", }
            }
        },
        { "rich", new List<List<string>> {
            new List<string> {
                "* and ^ went to a fancy hotel for their #! date. It was beautiful." },
            new List<string> {
                "* and ^ went to a fancy hotel for their #! date. It seemed really gaudy.", }
            }
        },
        { "weed", new List<List<string>> {
            new List<string> {
                "* and ^ smoked weed on their #! date. They felt, like, really connected, man.", },
            new List<string> {
                "* and ^ smoked weed on their #! date. Both of them were arrested.", }
            }
        },
        { "anime", new List<List<string>> {
            new List<string> {
                "* and ^  watched anime together for their #! date. They watched an entire season.", },
            new List<string> {
                "* and ^  watched anime together for their #! date. One of them felt uncomfortable.", }
            }
        }
    };
}
