using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Discussions
{
    internal class MyClass
    {

        private void MyMethod()
        {
            /*
                === more collections ===
                stacks and queues 
                reiterate 90% of the time use a List<> unless it's obvious you need something else

               ==================== A quick lecture about style differences.  ================ 
        1) style is about readability and readability is somewhat subjective. But, code that is hard to read is hard to maintain.
        2) follow your workplace conventions or stated style 
        3) unless it's really hard to read, then work to change it for the better (bring examples and arguments)

        More on readable and understandable code in a later module. Clean Code!
        */
            // My works place does this. 
            if (1 == 1)
            {
                // ...
            }
            else
            {
                //   ...
            }

            ////         v/s

            // most local shops do this in C# (but the above in java/js/python. ugghh.)
            if (1 == 1)
            {
                //   ...
            }
            else
            {
                //...
            }

            /////////////// v/s 

            // (ewwww. no!)
            if (1 == 1) { /* ... */ } else { /* ... */ }

            ///  vs

            // for the love of all things holy or unholy, NO!
            if (1 == 1)
            {
                /* ... */
            }
            else
            {
                /* ... */
            }

            //============== conditional statements =========
            string color = String.Empty;
            // boolean operators: !, == ( different from = ), > , <  ,>=, <= !=, 
            // joining booleans   && , ||  (different from & and | !!!)
            // if, else if, and else dont get semicolons!

            if (color == "Blue") ;
            ThisLineAlwaysExecutesEvenWhenColorIsGreen();

            // curlys are optional for single line if/elses
            if (color == "Blue")
                Console.WriteLine("The color is Blue");
            else if (color == "Green")
                Console.WriteLine("The color is Green");
            else
                Console.WriteLine("The color is neither.");

            // but don't do this. because if someone adds a line and doesn't add the curlys things get awkward
            if (color == "Blue")
                Console.WriteLine("The color is Blue");
            Console.WriteLine("I like Blue"); // this line will always execute

        }

        private void ThisLineAlwaysExecutesEvenWhenColorIsGreen()
        {
            throw new NotImplementedException();
        }


        //============== Exceptions ==============
        private void Exceptions(object thing)
        {
            ///Don't be tempted to use try/catch all over the place. 
            //Exceptions are only for exceptional errors that are beyond your code's control.

            // instatiate an exception 
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing), "Thing cannot be null");
            }
            //inside catch throw (good) v/s throw ex (bad - loses call stack) or throw new ex (usually worse)

            try
            {
                //do something that opens a file here
            }
            catch
            {
                // useless catch just throws but doesn't handle.
                throw;
            }
            finally ///finally is for cleanup 
            {
                // close the file here!
            }


            //try /catch calls are expensive in terms of execution, keep outside loops when practical but ...
            List<object> things = [];
            // bad
            foreach (var thing2 in things)
            {
                try
                {
                    //...
                }
                catch (Exception ex)
                {
                    Log(ex);
                    throw;
                }
            }

            // better 
            try
            {
                foreach (var thin in things)
                {
                    //...
                }
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }

            // but what if you need to continue processing the loop? well you have to get fancy
            List<Exception> caughtExceptions = [];
            foreach (var thing3 in things)
            {
                try
                {
                    //...
                }
                catch (Exception ex)
                {
                    Log(ex);
                    //throw; // will abort the loop!!!
                    caughtExceptions.Add(ex); // lets loop control
                }
            }

            DoSomethingWithListOfExceptions(caughtExceptions);
        }

        //Some cases you might not want to throw, but instead just handle. Imagine a windows application
        void DoFileRelatedWork(string fileName)
        {
            try
            {
                var file = File.OpenText(fileName);
                //...	
            }
            catch (FileNotFoundException ex)
            {
                Log(ex);
                NotifyUserFileNotFound("Invalid File Name or File Not Found!");
            }

        }


        // BUT!! remember don't use exceptions for flow control - and avoid exceptions altogether when possible.

        void DoFileRelatedWorkABetterWay(string fileName)
        {
            if (!File.Exists(fileName))
            {
                NotifyUserFileNotFound("Invalid File Name or File Not Found!");
                return;
            }
            var file = File.OpenText(fileName);
            //...
        }

        // you can (and should) catch as specific an exception as possible - because your code should only 
        /* catch exceptions it knows how to handle 
        - there are hundreds of built in exceptions in C# 
        - exceptions can (and do) make use of inheritance - meaning that you can catch related types of exceptions by catching their common ancestor:

        catch (IOException) 

        catches FileNotFoundException as well as  
        DirectoryNotFoundException
        DriveNotFoundException
        EndOfStreamException
        FileLoadException

        */


        // dummmy methods to git rid of req squigglies
        // 

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
        public void Log(Exception exception)
        {
        }
        public void DoSomethingWithListOfExceptions(List<Exception> exceptions)
        {

        }

        private void NotifyUserFileNotFound(string v)
        {
            throw new NotImplementedException();
        }

    }

    // you can create your own exception classes by inheriting from existing exceptions classes
    public class MyException : Exception
    {
        //... implement minimum exception requirements here.
    }


    public class MyFileNotFoundException : FileNotFoundException
    {
        //... implement minimum exception requirements here.
    }

    ///======= LOOPS - where is the video on loops???? ======
    public class Loops {
        private static void Loopsx() {

            // counts 0 to 9
            for (int i = 0; i < 10; i++) { // try i+=2 rather than i++ 
                Console.WriteLine(i.ToString());
            }

            // you saw for and foreach in the collections video
            IList<string> colors = [];
            foreach (var myColor in colors)
            {
                Console.WriteLine(myColor + "is a pretty color.");
            }

            // there are also while() and do while() loops
            // condition must be true to enter the loop
            string color = "Blue";
            while (color == "Blue")
            { // careful if you don't alter the condition will loop forever
                color = "Red";
            }

            // loop always executes once
            int x = 10;
            do
            {
                x++;
            } while (x < 11);

        }
    }


    /** ===========

    who has at least started the object oriented programming videos?
    Don't be intimidated by all the technical terms she throws out in the introduction video, don't get too hung up on them. Having said that .. it's 3 hours plus, much more video than you've done so far.
    
    careful - folks often use object and class interchangeably but technically an object is a thing that is defined by it's class.  the class tells about the object, and defines the object, but a class is not an object because it's not a thing 
   
    
    */

    public class UserExampleCode
    {
        void UserExampleMethod()
        {

            //... in some code somewhere 

            User currentUser = new()
            {
                Name = "John",
                Id = 1
            };
            // User is the class and currentUser is the object (an instance of the class User).
        }   
    
    }
}

    