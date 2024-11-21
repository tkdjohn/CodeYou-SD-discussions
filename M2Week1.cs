using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Diagnostics;


namespace Discussions {
    // y'all will be glad to know I finally installed Visual Studio Spell Checker.
    // example one
    // you can add words humungous - yes VSSpellChecker that is a real word 
    // Note how it DOESN'T FLAG VSSpellChecker nor VSSepllChceker 

    // GOTO: ************

    public static class Class1 {
        // example of enum and ternary operator
        public enum Color {
            Unknown = 0,
            Red,
            Blue,
            Green,
            Yellow,
            Black,
            White,
            Brown,
            Orange,
            Purple,
            Magenta,
            OsageOrange,
            BurntUmber
        }
        public static Color ToClor(this string inputColor) {
            var inputIsValid = Enum.TryParse(inputColor, true, out Color color);
            // ternary operator
            return inputIsValid ? color : Color.Unknown;
        }

        public static string ToColor(this Color color) {
            return color.ToString();
        }
    }

    // *** Interfaces again ***

    // SAVE a link to these videos.  He does half or more of the things needed for 
    // a capstone project. Great examples of the repository pattern, even though
    // we're not super worried about data storage just yet. These are great videos
    // but they are introducing quite a bit of stuff that isn't directly relevant
    // to the topic. Having said that, when he says "don't sweat the details, this
    // is just to show the power/usefulness of interfaces" - DON'T SWEAT THE DETAILS
    // just try to understand how interfaces were used.

    // "think of an interface as a contract"
    // "I guarantee I have these properties and methods"

    // The whole point is  you can use an Interface anywhere you would use a type,
    // which allows you to use different types of objects interchangeably, as long
    // as they implement the same interface! All with the benefits of Strong typing.

    // It is convention that interfaces start with a capital 'I'. This is not required,
    // but *everyone* does it this way. 

    // A class can implement multiple interfaces, this makes interfaces very, very useful.

    // Interfaces make it much easier to consume/share code from/with other teams - without
    // the consuming team being overly concerned with the details of implementation.

    //<Ctrl><K> && <Ctrl><C> shortcuts - I use these all the time.
    //<Ctrl><R><Ctrl><G> to remove unneeded using statements.
    //<Ctrl><.> to do all kinds of fun things.
    //  including generating interfaces from a class
    //  and adding code to implement an interface (after you've added the : <Interface>)
    public class UncommentMe : IPolite {
        // hit Ctrl . to extract an interface.
        private bool commented = false;
        public UncommentMe(bool bePolite) {
            BePolite = bePolite;
        }
        public bool BePolite { get; set; } = true;

        public void ToggleComment() {
            commented = !commented;
            if (BePolite && commented == false) {
                Console.WriteLine("Thank You");
            }
        }
        public bool AreYouUncommented() {
            return commented;
        }
    }

    // Generally interfaces live in their own files, just like classes, though some
    // shops don't follow this convention.
    public interface IPolite {
        public bool BePolite { get; set; }
        public void TogglePoliteness();
    }

    // NEW TERM: Feature Flag  (also just flag)
    //    a setting consumed at runtime to enable /disable
    //    (or just hide) a feature
    // GREAT example of using interfaces for configuration 
    //   (and the ASP.NEt built in configuration support)
    // all to support a feature flag! 

    // He mentions "reflection" - don't try to understand reflection right now,
    //   just understand that there are ways to use code to look at class details
    //   including things like finding classes by name, by interface, etc
    //   as well as listing their properties

    // He mentions "Dependency Injection" - we've talked about this before, but he
    //   shows good, understandable examples of how DI works and why it is useful
    //   and how interfaces become super important. AND how building tests can uncover
    //   tight coupling. (Classes are directly dependent on each other.)
    //   ANNDD how interfaces and DI allow you to "Fake" or "Mock" objects (so the
    //   test can control them when they are consumed by the object being tested.)

    // YAY he used '!'  to assert "I know that's not null here"

    // He mentions/uses "lambda expressions" fancy word for in line methods/functions
    //    the => syntax is a bit awkward but you'll eventually get used to it.

    // Explicit Implementation - not something I've have much occasion to use. The
    //   IEnumerable situation he covers (conflicting members/methods)  outlines the
    //   only situation where I've used this.

    // YES interfaces can inherit from other interfaces. In fact, more than one! 
    public interface IDoItFast {
        public void DoItFast();
    }
    public interface IDoItSlow {
        public void DoItSlow();
    }
    public interface IJustDoIt : IDoItFast, IDoItSlow {
        public void JustDoIt();
    }
    public class DoIt : IJustDoIt {
        public void DoItFast() {
            throw new NotImplementedException();
        }

        public void DoItSlow() {
            throw new NotImplementedException();
        }

        public void JustDoIt() {
            throw new NotImplementedException();
        }
    }

    // Default Implementation  Good set of videos, but don't worry too much about
    // understanding all the things he covers. Default Implementation is a very new
    // feature of the language and will take a while before most people adopt its use
    // with any regularity. Plus it is more of a necessary evil, than a feature that
    // promotes clean code.

    // access modifiers on interfaces - seek advice fro your team/senior when using anything
    // other than public in an interface.

    // He mentions you shouldn't remove things from an interface because it breaks 
    // the contract. Good point, in principle but this happens ALL THE TIME.

    // use a abstract class or an interface?  like I said last week, the rule of thumb is
    // if there is shared code, use an abstract class, otherwise use an Interface. An abstract
    // class without any shared code, is effectively a complicated interface.

    // ****
    // questions on Interfaces?  
    //      Dependency Injection?
    //      something else in these videos?
    // ****


    // **** LINQ **** 
    // so much to say so little time.
    // LINQ expressions work best for large sets of data. LINQ is WAY more efficient than 
    //   looping, except when it isn't. It's easy to misuse LINQ and cause it to iterate
    //   over the collection multiple times. 

    // Most devs I know don't use the SQL like LINQ code. But it is good to understand that 
    // these two LINQ statements are equivalent.

    public static class LINQExamples {
        static readonly int[] numbers = [9, 1, 5, 8, 6, 7, 3, 2, 0, 4];
        static readonly Dictionary<int, string> numbersWords = new Dictionary<int, string> {
            {2,"two" },
            {0, "zero"},
            {1, "one"},
            {3, "three" }
        };
        public static IEnumerable<int> QuerySyntax() {
            // this is just awkward to me b/c SQL would be "Select <x> From <table> OrderBy <x>"
            var query = from num in numbers
                        orderby num ascending
                        select num;
            return query;
        }

        public static IEnumerable<int> MethodSyntax() {
            // Select isn't needed in this simple case.
            var wordyQuery = numbers.OrderBy(number => number).Select(number => number);
            // returns integers in order 0 to 9
            var query = numbers.OrderBy(number => number);
            // returns integers in descending order 9 to 0
            var descendingQyery = numbers.OrderByDescending(number => number);

            // returns [ "zero", "one", "two", "three" ] 
            var aMoreUnderstandabQuery = numbersWords
                .OrderBy(nw => nw.Key)
                .Select(nw => nw.Value);

            return query;
        }
    }
    // common LINQ methods include
    //   .Any(x => ...)  .All(x => ...)  .Comtains(...)  .Count() .Sum() .Min()  .Max()
    //   .Average()  .Empty()  .Range()
    //   .Where(x => ...)  ForEach(x => ...) .First() and .FirstOrDefault()
    //   .Last() and .LastOrDefault()  .Select(x => ...)  .OrderBy( x => ...)  .Distinct() 
    //   .Reverse() .GroupBy(x => ...) .Join(...) 
    // a few that can convert between collections:
    //   .AsEnumerable() .ToArray() .ToList() .ToDictionary(...) 

    // lots of other LINQ expressions available see https://dotnettutorials.net/lesson/linq-operators/
    //  for a list. They are fairly well named so easy to understand what they do.

    // Search "LINQ .<methodname> Method Syntax" and click look at the results. 

    // Not always as easy to understand how to use them though. I recommend you
    //  spend some time on  your own exploring and learning a bit more about LINQ.
    // ****
    // questions on LINQ?
    // ****
    //  Do y'all know about Stack Overflow? don't just look at the top answer! 


    // **** Defensive Programming ****
    // I like the examples and approach here. Good stuff.
    // 1) break into separate methods, e.g. apply single responsibility!
    // 2) "predicable" null checks, consistency (follow existing patterns), and NAMING
    // 3) "testable" external resources make it hard to test, same with methods that do 
    //     more than one thing. if you find your code hard to test, you should be asking
    //     yourself if it needs breaking up and maybe even abstraction into multiple classes.


    // **** Extension Methods ****
    // 3h!? really? OK only 1.5h but still.  UGH. sorry didn't finish these videos.
    // Cool fact: most (all??) of the LINQ methods are extension methods.

    // must be static method in a static class
    // uses the 'this' keyword on the first parameter - which the class or type you are
    //   extending.
    public static class BookTitleExtensions {
        public readonly static string[] WordsNotToCapitalize = [
            "a", "an", "the", "of", "from" ];
        public static string ToTitleCase(this string title) {
            var words = title.Split(['_', ' '], StringSplitOptions.RemoveEmptyEntries);
           
            var firstWord = words.First().CapitalizeFirstLetter();
            words[0] = firstWord;
           
            words = words
                .Where(word => !WordsNotToCapitalize.Contains(word))
                .Select(word => word.CapitalizeFirstLetter())
                .ToArray();
            return string.Join(' ', [firstWord, words]);
        }

        public static string CapitalizeFirstLetter(this string wordToCapitalize) { 
            return $"{char.ToUpper(wordToCapitalize[0])}{wordToCapitalize[1..].ToLower()}"; 
        }
    }


    public class BookManager {
        public static string myToTitleCase(this string title) {
            var words = title.Split(['_', ' '], StringSplitOptions.RemoveEmptyEntries);

            var firstWord = words.First().CapitalizeFirstLetter();
            words[0] = firstWord;

            words = words
                .Where(word => !WordsNotToCapitalize.Contains(word))
                .Select(word => word.CapitalizeFirstLetter())
                .ToArray();
            return string.Join(' ', [firstWord, words]);
        }
        public void GetNewBookFromUser() {
            Console.WriteLine("Enter the book's Title");
            var title = Console.ReadLine();
            title = title.ToTitleCase();
            title = myToTitleCase (title);
        }

    }
    // The examples loading appsettings.json files is a pretty common pattern using
    //   extension methods. 

    // Best Practices:
    //   Don't make everything an extension method. Only if multiple consumers.
    //   Keep the target type as specific as possible (generally extensions for base classes
    //     are not a great idea.)
    //   Name your extension methods clearly, preferably with a name that isn't likely to 
    //     be used by someone adding to the target class.

    // ****
    // questions on Extension Methods?  
    //      Dependency Injection?
    //      something else in these videos?
    // ****


    // **** Coding Standards ****
    // The great thing about standards is that there are so many to choose from!

    // Mostly Good advice here, though much of what he says is really a matter of team/company 
    // preference. 

    // Seriously, do what works for you, or what your employer/team dictates you do. 
    // You will eventually develop your own preferences, and come to recognize some things
    // as good, clean, readable code. (Note there is a difference between READABLE code and 
    // UNDERSTANDABLE code. Code that is hard to read is likely hard to understand, but 
    // just because code is easy to read, doesn't mean it is easy to understand.


    // **** housekeeping **** 
    // !!!! QUICK example of using VS to do git things. include showing line by line diffs and staging
    // v/s auto stage/commit AND stash
    // !!!! Slowly working through PetShop. I have my cleaned up version of the first one up there.
    //    each one will be in its own commit so you can see the evolution if you want.
    //    Plan to get caught up on PetShop as well as get ahead these video discussions
    //    during the holiday break

    // What is a GUID?  

    // !!!!!! NO CLASS TGIVING WEEK! 
    // !!!!! I have not made formal plans to hold "Office Hours" at Ten20 this coming Sunday or the Sunday
    // after the holiday, but I'll be in town both days as far as I know so reach out if you want/need
    // and I can most likely get there. 
}