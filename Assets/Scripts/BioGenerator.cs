using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioGenerator : MonoBehaviour
{
    //TODO:need to be init
    Dictionary<string, List<List<string>>> database = new Dictionary<string, List<List<string>>>() {
        { "cats", new List<List<string>> {
            new List<string> {
                "My house is covered in cat hair and hairballs. Just the way I like it.",
                "if i were an animal i'd be a cat so i could cuddle up to you and be warm :)",
                "Crazy cat person 4 lyfe" },
            new List<string> {
                "Not a cat person. Please don't put Mr. Whiskers in my face.",
                "very allegic to cats, and cat videos",
                "My grandma's cat tried to kill me once" }
            }
        },
        { "dogs", new List<List<string>> {
            new List<string> {
                "My dog Rex is my best friend.",
                "you better have puppies.",
                "If you have a dog I will pay for the first date" },
            new List<string> {
                "Dogs aren't my thing - got chased by a poodle one time. Almost killed me.",
                "swipe left if you have a dog",
                "Fan of Snoop Dogg, not doggies" }
            }
        },
        { "star_wars", new List<List<string>> {
            new List<string> {
                "Favorite movie: Empire Strikes Back. Best movie of all time: Empire Strikes Back.",
                "dm me nudes and ill dm you prequel memes",
                "*Obi Wan voice* HELLO THERE" },
            new List<string> {
                "Huge Trekkie. Star Trek is NOT THE SAME as Star Wars.",
                "ill force you out of my house if you dm about sw",
                "Life is too short to watch dumb space movies" }
            }
        },
        { "veganism", new List<List<string>> {
            new List<string> {
                "Vegan and proud. Join the green revolution!!!",
                "lips that touch meat will never touch mine.",
                "I became a vegan to get wizard powers" },
            new List<string> {
                "I love cooking. Steak's my specialty - I hope you're not vegan!",
                "if youre a vegan i have beef with you >:(",
                "Vegans killed and ate my whole family" }
            }
        },
        { "activism", new List<List<string>> {
            new List<string> {
                "If you're not fighting for a cause why fight at all?",
                "activism, not slacktivism. save the planet",
                "Active activist" },
            new List<string> {
                "I don't worry too much. Looking for a romantic, not an advocate.",
                "goes with the flow",
                "Activism is a lie perpetuated by Big Paper to make picket sales" }
            }
        },
        { "politics", new List<List<string>> {
            new List<string> {
                "I talk a lot about politics - conversation is key.",
                "if you aren't willing to debate me, maybe you shouldn't date me.",
                "I like to stand on the podium and loudly shout my opinions" },
            new List<string> {
                "Please don't bring up politics. It's not as important as you think.",
                "i use this app to escape reality, please don't bring it back in",
                "I get enough politics at Thanksgiving, dont bring it up with me" }
            }
        },
        { "working", new List<List<string>> {
            new List<string> {
                "I work hard and I play hard. Then I work harder.",
                "i like to work but i want someone to greet me when i get home from it :)",
                "Working is cool because it give me money to do things" },
            new List<string> {
                "I'd rather hustle 24/7 than slave 9-to-5. #entrepreneur",
                "would rather lay in bed all day than go to my crappy job",
                "You say work, I say shirk" }
            }
        },
        { "pineapples_on_pizza", new List<List<string>> {
            new List<string> {
                "Pineapples are 1000% necessary on pizza. Fact.",
                "pineapples make everything taste better, like pizza!!",
                "I'll take you to flavortown, just like pineapples on pizza ;)" },
            new List<string> {
                "Pineapples are gross. Don't @ me.",
                "pizza lover - not with pineapples though :)",
                "I like pineapples and I like pizza but DONT PUT THEM TOGEHTER" }
            }
        },
        { "traveling", new List<List<string>> {
            new List<string> {
                "I love to travel - I'd love even more to have a companion!",
                "would love to see the northern lights one day",
                "Catch me on an airplane heading across the sea" },
            new List<string> {
                "I'm a homebody; I'd really rather stay in my hometown than go somewhere crazy.",
                "traveling? more like diseases and theft and confusion",
                "Home may be lame but there be monsters out there" }
            }
        },
        { "video_games", new List<List<string>> {
            new List<string> {
                "Playing video games is one of my biggest hobbies (board games are cool too).",
                "video games are my jam, lmk if you wanna be player 2 ;)",
                "Gamers rise up" },
            new List<string> {
                "I don't like video games - I'm much more of a movie person.",
                "i am NOT a gamer!! do not under any circumstances dm me about video games!!",
                "Video games made me go blind" }
            }
        },
        { "kids", new List<List<string>> {
            new List<string> {
                "I'm gonna be honest here, I love kids. They're way nicer than adults.",
                "i am on this app for someone to have my children",
                "I have 3 children and I love them very much" },
            new List<string> {
                "I do not have kids, and I don't really want any. They smell.",
                "kids are loud and expensive. i dont get why anyone would want one.",
                "Can I refund my baby nephew" }
            }
        },
        { "long_term", new List<List<string>> {
            new List<string> {
                "If you want someone who wants to be with you for awhile, let me know.",
                "not in this for a passing fling",
                "Looking for a commitment" },
            new List<string> {
                "Not interested in a long-term relationship. Am interested in a good time.",
                "if youre fun and spunky hmu ;)",
                "I'm looking for a good time, not a long time (whoa-oh)" }
            }
        },
        { "extroverted", new List<List<string>> {
            new List<string> {
                "So excited to meet new people!",
                "big city kid",
                "I like going on adventures ~" },
            new List<string> {
                "Part of the Anti-Social Social Club.",
                "~sleeps to the sound of rain~",
                "Other people scare me, except strangers on a dating app" }
            }
        },
        { "reading", new List<List<string>> {
            new List<string> {
                "In my free time, I like to read books and experience their stories.",
                "avid reader and fan of harry potter",
                "My first crush was Sam from the Lord of the Rings" },
            new List<string> {
                "I'd rather watch the movie than read the book.",
                "sooooo sick and tired of bookworms on this app. if you're not a nerd PLEASE TALK TO ME",
                "Fun fact: I can't read" }
            }
        },
        { "cars", new List<List<string>> {
            new List<string> {
                "I'm a total carhead. I know every Audi model made in the past decade!",
                "whether you need a mechanic or a cutie im your date ;D",
                "I love driving cars, especially stolen ones" },
            new List<string> {
                "Some of you really like cars. I'm sorry, but I just don't get it.",
                "why would you like cars? they're smelly and expensive and toxic???",
                "I walk everywhere so I don't have to drive #everydayislegday" }
            }
        },
        { "college", new List<List<string>> {
            new List<string> {
                "Currently a student at Carnegie Mellon University. Loving every second!",
                "im in college rn! econ major in a lot of super fun clubs and classes!",
                "My wallet is small but my brain is big" },
            new List<string> {
                "I'm in college. I don't think I've slept since freshman year.",
                "i downloaded this app because school is slowly killing me LOL",
                "College gave me crippling anxiety and debt" }
            }
        },
        { "sports", new List<List<string>> {
            new List<string> {
                "I love football. I'm a super big Steelers fan and love tailgating.",
                "swimming is life",
                "I love to watch the sportsball, yay team go" },
            new List<string> {
                "I'm looking for someone different from all the football-obsessed weirdos.",
                "dont shoot your shot, because i am SICK of dating basketball players",
                "Little League gives me PTSD" }
            }
        },
        { "rich", new List<List<string>> {
            new List<string> {
                "Looking for someone who isn't afraid to spend a little to get a lot.",
                "high end shopping? fancy dinners? financial security? yes please :D",
                "Hello I like money" },
            new List<string> {
                "Spend time, not money.",
                "my ideal evening isnt expensive. just happy and together.",
                "Money doesn't matter to me as long as you have a heart of gold :)" }
            }
        },
        { "weed", new List<List<string>> {
            new List<string> {
                "I'm into lawnscaping, if you know what I mean. #Legalize",
                "yoooooo i totally typed this while on a wicked high my dude XD",
                "Getting absolutely LIT since 4/20" },
            new List<string> {
                "I'm totally clean, guys - never drank. smoked, or got high in my life!",
                "my friends once pressured me to smoke the mary jane but i'm already high on life ;P",
                "Yeah I'm a narc so what" }
            }
        },
        { "anime", new List<List<string>> {
            new List<string> {
                "I watch anime a lot. No, I don't own a body pillow, and stop asking.",
                "anime is the coolest!! hmu if you're into FMA. id love to talk!",
                "Omae wa mou shinderu" },
            new List<string> {
                "I love classic cartoons, but don't talk to me about anime. Please. Not again.",
                "if i see ONE MORE PERSON dm me with an anime girl as their profile pic im gonna SCREAM",
                "Anime was a mistake" }
            }
        }
    };
    //key : preference name, 
    //Value : index 0 -high, index 1-low index 3- possibility neture? 
    //The list within the list, index 0,1,2 -writing style 


    public List<string> GenerateBio(Dictionary<string,float> prefsDict, int styleInt)
    {
        List<string> bioList = new List<string>();

        foreach (KeyValuePair<string, float> k in prefsDict)
        {
            //determined the high low value of this preference
            if (k.Value >= 0.5)
            {
                bioList.Add(database[k.Key.Trim()][0][styleInt] + "\n");
            } else if (k.Value <= -0.5)
            {
                bioList.Add(database[k.Key.Trim()][1][styleInt] + "\n");
            }
        }
        return bioList;
    }

   
}
