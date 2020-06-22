using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioGenerator : MonoBehaviour
{

    public Dictionary<string, List<List<string>>> database = new Dictionary<string, List<List<string>>>() {
        { "cats", new List<List<string>> {
            new List<string> {
                "My house is covered in cat hair and hairballs. Just the way I like it.",
                "if i were an animal i'd be a cat so i could cuddle up to you and be warm :)",
                "Crazy cat person 4 lyfe",
                "You know your neighbor who has 7 cats and doesn't leave their house? That's me." },
            new List<string> {
                "Not a cat person. Please don't put Mr. Whiskers in my face.",
                "very allegic to cats, and cat videos",
                "My grandma's cat tried to kill me once",
                "I have been mortal enemies with your cat since its birth." }
            }
        },
        { "dogs", new List<List<string>> {
            new List<string> {
                "My dog Rex is my best friend.",
                "you better have puppies.",
                "If you have a dog I will pay for the first date",
                "Honestly, I'm on this app hoping that one of you has a dog." },
            new List<string> {
                "Dogs aren't my thing - got chased by a poodle one time. Almost killed me.",
                "swipe left if you have a dog",
                "Fan of Snoop Dogg, not doggies",
                "Why do all of you always make your first message a picture of your dog? Major turn off." }
            }
        },
        { "star_wars", new List<List<string>> {
            new List<string> {
                "Favorite movie: The Empire Strikes Back. Best movie of all time: The Empire Strikes Back.",
                "dm me nudes and ill dm you prequel memes",
                "*Obi Wan voice* HELLO THERE",
                "We can rule the galaxy together, but don't try to take my high ground." },
            new List<string> {
                "Huge Trekkie. Star Trek is NOT Star Wars.",
                "ill force you out of my house if you dm about sw",
                "Life is too short to watch dumb space movies about laser swords",
                "I've never seen Star Wars. It's dumb. Stars can't fight." }
            }
        },
        { "veganism", new List<List<string>> {
            new List<string> {
                "Vegan and proud. Join the green revolution!!!",
                "lips that touch meat will never touch mine.",
                "I became a vegan to get wizard powers",
                "I'm a vegan because I hate plants so much I'll eat all of them." },
            new List<string> {
                "I love cooking. Steak's my specialty - I hope you're not vegan!",
                "if youre a vegan i have beef with you >:(",
                "Vegans killed and ate my whole family",
                "Not a vegan. It's a fad, just like floppy disks, Moon Shoes, and the 90s." }
            }
        },
        { "activism", new List<List<string>> {
            new List<string> {
                "If you're not fighting for a cause why fight at all?",
                "activism, not slacktivism. save the planet",
                "Active activist",
                "I'm really into activism. For example, I signed some online petitions yesterday." },
            new List<string> {
                "I don't worry too much. Looking for a romantic, not an advocate.",
                "goes with the flow, doesn't worry about too many issues",
                "Activism is a lie perpetuated by Big Paper to make picket sales",
                "Activism is too much work. It's way too active." }
            }
        },
        { "politics", new List<List<string>> {
            new List<string> {
                "I talk a lot about politics - conversation is key.",
                "if you aren't willing to debate me, maybe you shouldn't date me.",
                "I like to stand on the podium and loudly shout my opinions",
                "Political and proud of it. Considering forming my own political party." },
            new List<string> {
                "Please don't bring up politics. It's not as important as you think.",
                "i use this app to escape reality, please don't bring it back in",
                "I get enough politics at Thanksgiving, dont bring it up with me",
                "Don't talk to me about politics. I don't even know who's running." }
            }
        },
        { "working", new List<List<string>> {
            new List<string> {
                "I work hard and I play hard. Then I work harder.",
                "i like to work but i want someone to greet me when i get home from it :)",
                "Working is cool because it give me money to do things",
                "I'm married to my work, but I'll have an affair with you. Don't think about that." },
            new List<string> {
                "I'd rather hustle 24/7 than slave 9-to-5. #entrepreneur",
                "would rather lay in bed all day than go to my crappy job",
                "You say work, I say shirk",
                "Currently unemployed. It's awesome." }
            }
        },
        { "pineapples_on_pizza", new List<List<string>> {
            new List<string> {
                "Pineapples are 1000% necessary on pizza. Fact.",
                "pineapples make everything taste better, like pizza!!",
                "I'll take you to Flavortown, just like pineapples on pizza ;)",
                "The moment a pineapple touched a slice of pizza, a new Renaissance began." },
            new List<string> {
                "Pineapples are gross. Don't @ me.",
                "pizza lover - not with pineapples though :)",
                "I like pineapples and I like pizza but DONT PUT THEM TOGEHTER",
                "Pizza on pineapples? Great. Pineapples on pizza? Nope." }
            }
        },
        { "traveling", new List<List<string>> {
            new List<string> {
                "I love to travel - I'd love even more to have a companion!",
                "would love to fly and see the northern lights one day",
                "Catch me on an airplane heading across the sea",
                "I'm looking for a road trip buddy. Nothing like a long time in a tiny box to bring two people together." },
            new List<string> {
                "I'm a homebody; I'd really rather stay in my hometown than go somewhere crazy.",
                "traveling? more like diseases and theft and confusion",
                "Home may be lame but there be monsters out there",
                "I'm afraid of the outdoors and of going anywhere. My ideal dates avoid both." }
            }
        },
        { "video_games", new List<List<string>> {
            new List<string> {
                "Playing video games is one of my biggest hobbies (board games are cool too).",
                "video games are my jam, lmk if you wanna be player 2 ;)",
                "Gamers rise up",
                "Video games are the peak of human expression. Frogger is the new War and Peace." },
            new List<string> {
                "I don't like video games - I'm much more of a movie person.",
                "i am NOT a gamer!! do not under any circumstances dm me about video games!!",
                "Video games made me go blind",
                "Video games are but an imitation of our world. The only game I play is real life." }
            }
        },
        { "kids", new List<List<string>> {
            new List<string> {
                "I'm gonna be honest here, I love kids. They're way nicer than adults.",
                "i am on this app for someone to have my children",
                "I have 3 children and I love them very much",
                "Kids are the best. I really feel like they understand me." },
            new List<string> {
                "I do not have kids, and I don't really want any. They smell.",
                "kids are loud and expensive. i dont get why anyone would want one.",
                "Can I refund my baby nephew",
                "A kid called me a poopyhead once. Since then I have hated childkind." }
            }
        },
        { "long_term", new List<List<string>> {
            new List<string> {
                "If you want someone who wants to be with you for awhile, let me know.",
                "not in this for a passing fling",
                "Looking for a commitment, because God I am so lonely",
                "I'm looking for someone committal. I'll draft a binding lifelong contract." },
            new List<string> {
                "Not interested in a long-term relationship. Just a good time.",
                "if youre fun and spunky hmu ;)",
                "I'm looking for a good time, not a long time (whoa-oh)",
                "I can't even pronounce 'soulmate'. Not in my vocabulary." }
            }
        },
        { "extroverted", new List<List<string>> {
            new List<string> {
                "So excited to meet new people!",
                "i love going to conventions and meeting new people",
                "I like going on adventures ~",
                "I love people who love other people. Hit me up if you're a people person." },
            new List<string> {
                "Part of the Anti-Social Social Club.",
                "~sleeps to the sound of rain~",
                "Other people scare me, except strangers on a dating app",
                "I hate people. I'm on this app specifically so I don't have to talk to anyone other than you." }
            }
        },
        { "reading", new List<List<string>> {
            new List<string> {
                "In my free time, I like to read books and experience their stories.",
                "avid reader and fan of harry potter",
                "My first crush was Sam from the Lord of the Rings",
                "Knowledge is power, so every day I read everything. Newspapers. Road signs. The room." },
            new List<string> {
                "I'd rather watch the movie than read the book.",
                "sooooo sick and tired of bookworms on this app. if you're not a nerd PLEASE TALK TO ME",
                "Fun fact: I can't read",
                "Books are dangerous. Do you know how many Americans are harmed by papercuts every year? Too many." }
            }
        },
        { "cars", new List<List<string>> {
            new List<string> {
                "I'm a total carhead. I know every Audi model made in the past decade!",
                "whether you need a mechanic or a cutie im your date ;D",
                "I love driving cars, especially stolen ones",
                "I don't know what a 'license' or 'insurance policy' is, but fast car go beep beep haha." },
            new List<string> {
                "Some of you really like cars. I'm sorry, but I just don't get it.",
                "why would you like cars? they're smelly and expensive and toxic???",
                "I walk everywhere so I don't have to drive #everydayislegday",
                "I hate driving. Instead, I will carry you while rolling in my Heelys." }
            }
        },
        { "college", new List<List<string>> {
            new List<string> {
                "Currently a student at Carnegie Mellon University. Loving every second!",
                "im in college rn! econ major in a lot of super fun clubs and classes!",
                "My wallet is small but my brain is big",
                "College is the most fun I've had since high school." },
            new List<string> {
                "I'm in college. I don't think I've slept since freshman year.",
                "i downloaded this app because school is slowly killing me LOL",
                "College gave me nothign but crippling anxiety and debt",
                "College sucks. I didn't think I'd have to learn anything." }
            }
        },
        { "sports", new List<List<string>> {
            new List<string> {
                "I love football. I'm a super big Steelers fan and love tailgating.",
                "swimming is life",
                "I love to watch the sportsball, yay team go",
                "Whoever you are, I hope you're really into bowling." },
            new List<string> {
                "I'm looking for someone different from all the football-obsessed weirdos.",
                "dont shoot your shot, because i am SICK of dating basketball players",
                "Little League gives me PTSD",
                "I've tried every sport - baseball, football, cooking, Frisbee - and I don't like any." }
            }
        },
        { "rich", new List<List<string>> {
            new List<string> {
                "Looking for someone who isn't afraid to spend a little to get a lot.",
                "high end shopping? fancy dinners? financial security? yes please :D",
                "Hello I like money",
                "You can't pay for love. But you can pay for me. I'm into that." },
            new List<string> {
                "Spend time, not money.",
                "my ideal evening isnt expensive. just happy and together.",
                "Money doesn't matter to me as long as you have a heart of gold :)",
                "All of my exes are rich jerks. I love jerks, but if you're rich, swipe left." }
            }
        },
        { "weed", new List<List<string>> {
            new List<string> {
                "I'm into lawnscaping, if you know what I mean. #Legalize",
                "yoooooo i totally typed this while on a wicked high my dude XD",
                "Getting absolutely LIT since 4/20",
                "I use weed a lot. For medical reasons. And other reasons." },
            new List<string> {
                "I'm totally clean, guys - never drank. smoked, or got high in my life!",
                "my friends once pressured me to smoke the mary jane but i only get high on life ;P",
                "Yeah I'm a narc so what",
                "My friend once offered me some weed. I had him arrested." }
            }
        },
        { "anime", new List<List<string>> {
            new List<string> {
                "I watch anime a lot. No, I don't own a body pillow, and stop asking.",
                "anime is the coolest!! hmu if you're into FMA. id love to talk!",
                "Omae wa mou shinderu",
                "To be fair, you have to have a high IQ to understand Boku no Hero Academia." },
            new List<string> {
                "I love classic cartoons, but don't talk to me about anime. Please. Not again.",
                "if i see ONE MORE PERSON dm me with an anime girl as their profile pic im gonna SCREAM",
                "Anime was a mistake",
                "I don't get anime. What is 'baka'?" }
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
