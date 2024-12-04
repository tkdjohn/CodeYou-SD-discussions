using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discussions {
    public class M2Week2 {

        /************************* Clean Coding Principles *******************/
        // It is about clear, readable, and UNDERSTANDABLE code. 

        // Tech Debt - dirty code is tech debt with a hight interest rate, and will lead to
        // 

        // Good devs are lazy, but smart. They work to make their jobs easier in the future.

        // Sloppy is slow - you likely won't have time to clean it up later. Businesses
        // want quick code - clean code leads to quick code - but isn't as quick up front
        // and businesses typically want. V1 of any software is often quick and dirty and
        // ends up being rewritten (costly!!!)

        // Reading long methods is tiring. Tired devs make mistakes. Don't write long methods!

        // ***** three important people/books clean code movement 
        // Steve McConnell "Code Complete" book https://stevemcconnell.com/  
        // Uncle Bob Martin https://blog.cleancoder.com/ 
        //      Bob is considered problematic by some for a extended argument on twitter
        //      years ago. I was a follower of his at the time, and witnessed the whole thing.
        //      My opinion is that Bob reacted poorly and without seeking to understand others
        //      perspectives. He still has brilliant things to say about coding. (I've never 
        //      (met him.)
        // Martin Fowler (Martin is one of the smartest people I've ever met - met him briefly
        //   at a conference years ago.) Read anything he's written. https://martinfowler.com/

        /**** 3 clean code principles 
         * Right tool for the job - this is good advice, but determining the right too can 
         *     be hard without experience. If you find yourself debating which tool, seek 
         *     advice from your seniors/mentors! 
         *     AND yes! Stay native. Don't blur the boundaries. 
         *          "Avoid using one language to write another language/format via strings!"
         *          "One language per file!"
         *   
         * High Signal to Noise Ratio - focus your efforts here! 
         *     This is what I mean by understandability (or part of it).
         *     T E D  Terse Expressive Does 1 thing
         *     Rule of 7 no more than 
         *          - 7 parameters to a method
         *          - 7 methods in a class (meh.. )
         *          - 7 variables in scope at a time (what? well sort of - means LOCAL variables
         *            in a method member variables and properties don't count.)
         *    DRY principle: Don't Repeat Yourself. 
         *      Great advice, but be wary of trying to consolidate code that is similar but not
         *      actually the same. 
         *      <TODO NEED EXAMPLE>
         *     
         * Self Documenting - 
         *     YES! THIS!
         *     (To me this is part of S/N Ratio)
         *     Clear intent - Good naming is a big part of this. So is single responsibility.
         *     Layers (of abstractions) this takes some experience to do well, but well the 
         *       general idea is that you should layer in such a way that only the relevant details
         *       are immediately visible. For example, adding product in Pet store - the main routine
         *       doesn't care about the details of how a product is stored (list or dictionary or 
         *       whatever), it just needs to add a product. So the AddProduct method should hide 
         *       the details about how the product is stored. The name should reflect this (Don't
         *       call the method "AddProductToList" just call it "AddProduct"
         *     Format for readability - we've discussed this some before. The key for you rn is to 
         *       format things consistently with the surrounding code.
         */
        /* ** ** ** ** ** NAMING! ** ** ** ** **
        *
        * Poorly named classes become a magnet for dirty code!! And will almost always violate 
        * Single Responsibility. 
        * 
        * Said it before, but worth repeating: 
        *      Classes are 'things' use nouns to name them. Be as specific as possible, but also
        *      avoid including unnecessary words. (ProductList vs Products)
        *      
        *      Methods 'do' things. use verbs. Name should state what the method does, but not how
        *      it does it (unless that detail becomes important to distinguish v/s another method)
        *      
        *  "Naming should be when you honor Single Responsibility" - "easy" is a pie in the sky 
        *  view, but the point is still valid. More to the point, if you're having a hard time 
        *  naming something, look to see if the thing you are naming has multiple responsibilities!
        *  
        *  And, or, if  in a name are red flags!
        *  
        *  So are abbreviations - especially industry or company specific ones! Same for acronyms.
        *          "Only su wants to chown root, luser!"
        *  
        *  Booleans should answer questions. if (IsOpen) vs if (Open)
        ***************/

        /*  
        *  REWATCH The module titled "Writing Conditionals That Convey Intent" I agree with all 
        *  of it but reiterating it all he would take valuable time.
        *  
        *  I learned a pattern.  Polymorphism over enums. repeated switch stmts can be solved
        *  with polymorphism 
        *  
        *  One other thing worth discussing "Table Driven Methods" ...  */
        public decimal GetInsuranceRateDirty(int age) {
            if (age < 20) {
                return 235.6m;
            } else if (age < 30) {
                return 419.5m;
            } else if (age < 40) {
                return 476.38m;
            } else if (age < 50) {
                return 516.25m;
            } else {
                return 1000m;
            }
        }
        public class InsuranceRateEntity {
            public int InsuranceRateId {  get; set; }
            public string InsuranceRateName { get; set; } = "";
            public int MinimumAge { get; set; }
            public decimal Rate { get; set; }
        }

        public List<InsuranceRateEntity> RatesFromDatabse = [
            new InsuranceRateEntity { InsuranceRateId = 1, MinimumAge = 0, Rate = 235.6m},
            new InsuranceRateEntity { InsuranceRateId = 1, MinimumAge = 20, Rate = 419.5m},
            new InsuranceRateEntity { InsuranceRateId = 1, MinimumAge = 30, Rate = 476.38m},
            new InsuranceRateEntity { InsuranceRateId = 1, MinimumAge = 40, Rate = 516.25m},
            new InsuranceRateEntity { InsuranceRateId = 1, MinimumAge = 50, Rate = 1000m},
            ];
            
        public InsuranceRateEntity GetInsuranceRateClean(int age) {
            var ratesByMinimumAge = RatesFromDatabse.OrderBy(r => r.MinimumAge);
            return ratesByMinimumAge.FirstOrDefault(r => age > r.MinimumAge) ?? ratesByMinimumAge.Last();
            // side discussion, this is a better approach than the age < above. 
            // first To the point easier to read, and manage, not hard coded.
            // but second, it handles edge cases like age > 50. 
        }

        //  REWATCH The module titled "Writing Clean Methods" I agree with all 
        //  of it but reiterating it all he would take valuable time.
        //  pay particular attention to the methods of reducing excessive indentation.
        //   - Extracting a method can be done with <ctrl><.>
        //       - it's ok if a method is only called once
        //   - Fail fast is good, similarly so is return early:

        //  REWATCH The module titled "Writing Clean Classes" I agree with all 
        //  of it but reiterating it all he would take valuable time.
        //  - big one to note.. don't pass pieces of data around (into methods)
        //    group like data into a class and use that as the method argument.

        // Imagine an entity with 20 properties!
        public void AddInsuranceRateEntityDirty(int id, string name, int minAge, decimal rate) {
            var newEntity = new InsuranceRateEntity {
                InsuranceRateId = id,
                MinimumAge = minAge,
                Rate = rate,
                InsuranceRateName = name
            };
            // ... add the new entity 
        }

        // much easier to read method signature
        public void AddInsuranceRateEntityClean(InsuranceRateEntity newEntity) {
            // ... add the new entity 
        }

        public void UseAddInsureanceRateExamples() {
            // dirty - which is id and which is age??
            AddInsuranceRateEntityDirty(1, "name", 20, 101.2m);
            
            // yes more lines but easier to see what is what 
            AddInsuranceRateEntityClean(new InsuranceRateEntity {
                InsuranceRateId = 1,
                InsuranceRateName = "name",
                MinimumAge = 20,
                Rate = 101.2m
            });

            // more commonly 

            var newEntity = new InsuranceRateEntity {
                InsuranceRateId = 1,
                InsuranceRateName = "name",
                MinimumAge = 20,
                Rate = 101.2m
            };

            //.. do something with newEntity
            AddInsuranceRateEntityClean(newEntity);
        }

        //  REWATCH The module titled "Writing Clean Comments" 
        // I HATE redundant comments. especially when they just restate the name!
        
        /// <summary>
        /// Gets new object.
        /// </summary>
        /// <returns>The object you got.</returns>
        public object GetNewObject() {
            // create a new object.
            return new object();
        }

        // DON'T CHECK-IN COMMENTED CODE!!! it is just noise. any useful code is in 
        // source control (or it wasn't useful). Surrounding code will change 
        // making commented code invalid. Worst it's hard to read/understand.

        // Lots of the bad example comments he covers used to be industry standards, 
        //  you will find some devs adhere to requiring these comments religiously.
        //  They are just creating work for themselves (and everyone else). Don't fall
        //  into that trap.

        // Avoid checking in TODO: comments either the TODO is important enough to do it now 
        // or YAGNI. I make a regular practice of doing a global search in my code for "TODO" 
        // to make sure I don't check them in.  (I actually use "//TODO: *jws*" - my initials
        // in order to avoid tripping over TODOs left by others.
        // (or tripping over ToDouble() calls.

        // Love the Outline Rule!! Encourage you to work through the Demo video where
        // he uses the Outline Rule to Refactor to cleaner code!!
        // Download his repo and work through it with him!


        /*****************************************************************************/
        // ^^ divider comment! maybe I should split this up.
        // 
        // .Net Class Libraries
        //
        //
        // Please please please use the Path class methods to build file paths instead
        // of using simple string concatenation or interpolation. You avoid OS dependent
        // issues, and don't have to manage slashes. The number of bugs I encounter 
        // that are the result of someone messing up their slashes is ridiculous.

        public void PathExamples(string baseFilename, string fileExtension) {
            // trouble with backslashes 
            const string tempFolder = "\temp";
            const string subFolder = "sub\"";
            
            baseFilename = "filename";
            fileExtension = ".txt"; // should it have . or not?

            // .ext is a windows convention that isn't consistently used in Windows
            // and even less so in other OSs - best to keep the filename whole 
            // and use Path methods to get the parts when needed.
            var betterFilename = "filename.txt"; 
            fileExtension = Path.GetExtension(betterFilename); // => .txt
            baseFilename = Path.GetFileName(betterFilename); // => filename

            // you *can* escape \ with another \ in string literals
            var drive = "C:\\";
            // what if extension doesn't have '.'? 
            // C:\tempsub"filename.txt   - hmm lots wrong here
            var badConcatenation = drive + tempFolder + subFolder + baseFilename + fileExtension;

            // @ in front of a string allows us to not have to escape \
            // \t and \s aren't what you may think!
            // C:\\temp\sub"filename..txt 
            var badInterplation = $@"{drive}{tempFolder}\{baseFilename}.{fileExtension}";

            string badTempSubFolder = tempFolder + "sub"; // Temp\\sub or is it? 
            string goodTempSubFolder = Path.Join(tempFolder, subFolder);

            var best = Path.Combine(drive, tempFolder, subFolder, betterFilename);
        }

        // Async programming - the point is to NOT block for ANYTHING (UI or OS or ??)
        //    async/await - lets the OS manage the code after await rather
        //      than blocking on it. Technically this is not muti-threading but...
        //      the OS may make use of separate threads for the await-ed code.
        //    Way easier that managing the threads in your code. (multi-threading)
        //  I think we discussed this before. didn't we? 
        // one thing to be aware of:

        // take note of syntax - good practice to end method name in Async
        public async Task<string> GetDummyStringStubAsync() {
            Thread.Sleep(1000);
            return "some string";
        }
        // not async Task<void> but still a 'void' method
        public async Task DoSomethingToStringAsync() {
            // runs in the background
            var dummy = await GetDummyStringStubAsync();
            
            // dummy may still empty until you use it
            // ... do stuff
            // code may block here if GetDummyStringStubAsync is still running
            var result = $"The string is: '{dummy}'";
            return;
        }

        // Take note of the "Communicating with the Web" module. This can help you
        // meet one or more of the requirements for your capstone.

        // Pay attention to the Reading Configuration Files video. I use appsettings.json
        // files and the built in configuration stuff at least once a week.

        // We talked about reflection a bit before. Questions? not necessary you understand
        // reflection right now, but good to know it exists.

        /*****************************************************************************/
        // S.O.L.I.D.  Principles - https://en.wikipedia.org/wiki/SOLID
        //
        // Single responsibility
        //   - states that "there should never be more than one reason for a class to
        //     change." In other words, every class should have only one responsibility.
        // Open-closed
        //   - states that "software entities ... should be open for extension, but
        //     closed for modification."
        //   - good theory, rare in practice except in writing libraries.
        // Liskov substitution principle
        //   - states that "functions that use pointers or references to base classes
        //     must be able to use objects of derived classes without knowing it."
        //   - I know senior devs who can't remember this. 
        //   - the point is that classes that inherit form other classes should not
        //     break the base class's functionality.
        // Interface segregation principle
        //   - states that "clients should not be forced to depend upon interfaces that
        //     they do not use."
        //   - In other words, maintain simple, single responsibility for interfaces, 
        //     not just classes
        // Dependency inversion principle
        //   - states to depend upon abstractions, not concretes.
        //   - Classes should depend on the abstracts in classes they consume, not the
        //     actual implementation (this is the other side of Liskov)
        //   - Dependency Injection is a good example.
        //
        // questions? more discussion?


        // check class schedules, don't get behind on videos/coursework. if you are, 
        // set aside some time during the holidays to not only get caught up, but get
        // ahead.

        // you should be thinking about  what you want to do for your capstone project


    }
}
