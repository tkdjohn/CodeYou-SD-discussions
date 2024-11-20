using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Discussions
{
   /************* carryover from week4 ****/

    public class ClassWithStaticMember {
        // Define Instance 


        /* static modifier for members,  
        *    rare but useful. 
        *    static members exist across all instances of the class. 
        *    access using type name (class name), not instance name.
        */

        // only 1 InstanceCount member exists, all instances share
        public static int InstanceCount { get; private set; } = 0;

        // all instances will have their own id
        public int Id { get; private set; } = 0;
        public ClassWithStaticMember() {
            // will icrement for each instance created
            // access by StaticMembers.InstanceCount
            InstanceCount++;

            // will always be 1
            // cannot access by class name
            Id++; 
        }
    }

    public class StaticExample {
        public void ExampleMethod()
        {
            // we can access InstanceCount without creating an instance of the class
            var countTest = ClassWithStaticMember.InstanceCount; // will be 0
            var idTest = ClassWithStaticMember.Id; // oops!

            var one = new ClassWithStaticMember();
            // note the difference in how we access the variables
            countTest = ClassWithStaticMember.InstanceCount; // will be 1 
            idTest = one.Id; //will also be 1

            var two = new ClassWithStaticMember();
            countTest = ClassWithStaticMember.InstanceCount; // will be 2
            idTest = two.Id; // still 1

            var three = new StaticClass(); // nope
            var x = StaticClass.Id; // nope
            var y = StaticClass.InstanceCount;


        }
    }

    public static class StaticClass {
        /* static modifier for classes
         *     can't call new for the class
         *     use the class name to access members
         */
        // all member variables must be marked static
        public static int InstanceCount { get; private set; } = 0;
        public int Id { get; private set; } // no instance vars allowed in static class

        public StaticClass() { // static classes cannot have constructors
            
        }


    }

    public class ExampleClass {

        
        public int Id { get; private set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ExampleClass(int id)
        {
            Id = id;    
        }
        public ExampleClass()
        {
            
        }
        public ExampleClass(int id, string name, decimal value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }

    public class M1Week5
    {
        // Constructor v/s initializer syntax

        // constructor must have parameters, otherwise you  have to set
        // member AFTER constructor is called.

        void ExampleMethod()
        {
            var zero = new ExampleClass();
            zero.Id = 0; //nope
            zero.Name = "x";
            zero.Value = 0.00M;

            var one = new ExampleClass(1);
            one.Name = "a";
            one.Value = 1.11M;

            var two = new ExampleClass
            {
                Id = 2,// still nope
                Name = "b",
                Value = 222M
            };

            var three = new ExampleClass(3)
            {
                Name = "c",
                Value = 3.33M
            };

            var four = new ExampleClass(4, "c", 4.44M);



        }

        /***********************************
         * back to the videos *
         ************************************/
        // types of classes relationships:
        //   Collaboration - "uses a" 
        //   Composition - "Has a" - often properties of one object - have a 1:n relationship
        //     Aggregation - exists outside relationship (customer has an order)
        //     Composition - related objects are interdependent (order has a one or more order items)
        //   Inheritance - "Is a" (our User/Customer/Employee example. A Customer is a User, an Employee is a User.)

        // Classes can have one, or more of these types of relationships!

        // Services are almost always part of a collaboration relationship, same for Repositories
        // - both can employ Inheritance 
        // Entities are Composition classes but often employ Inheritance too.


        // good idea to initialize lists with an empty list
        // Constructor chaining is one way, but there is a better approach

        public class Customer : User
        {
            public Customer() : this(0)
            {
                //              ^^^ no need to chain constructors if you use inline initializers
            }

            public Customer(int id)
            {
                CustomerId = id;
            }

            public int CustomerId { get; set; } = 0;
            public List<Address> AddressList { get; private set; } = [];

        }

        // video used example of HomeAddress and BusinessAddress 
        //    she suggested a list of addresses would be better b/c than independent properties,
        //    because the list allows for more than 2 addresses. BUT be careful! You may have to 
        //    find a way to distinguish between the two addresses. (One way would be to define 
        //    an AddressType as a property of each address. FINALLY! I can show  you a good use
        //    for enumerated types!

        public enum AddressType
        {
            Home,
            Work,
            Shipping
        }
        public class Address
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }

            public string State { get; set; }

            public string PostalCode { get; set; }

            public string Country { get; set; }

            AddressType Type { get; set; }
        }

        // Most shops I've worked in prefer reference composition relationships over id composition.
        // I would agree, when using Entity Framework or other ORMs that automagically populate and
        // save all the entities in the relationships.

        // NOTE how she hardcoded data in the repositories to make it easier to work/test without
        // having to worry about implementing the Repository (or the database) just yet.
        // you can also just keep the code that has the test data and modify it to hydrate your database
        // (once the db added)

        //    THIS IS AN IMPORTANT TOOL - we regularly write code that we know we'll throw away so we
        //    can code pieces without worrying much about other pieces. Don't worry about writing code 
        //    you know or think you might throw away. It is not a waste of time. At a minimum it you
        //    have written out your current thinking on that part of the code. (I often do this just to 
        //    that part out of my head so I can focus on another part.)

        //    Expanding I often "stub out" things like repositories
        //       Stub out - adding the methods/properties I need to use in other classes without actually 
        //                  adding any real values or code. I just do this as I realize I need them.
        //    Then if I need to do something like add fake data, I'll do it something like in the video. 

        // One final word here. DO NOT try to make your code perfect as you first write it. You will 
        // get stuck on one part or another trying to 'do it right' get something written and then 
        // you can improve it later. Kind of like a rough draft. (This is why devs often start
        // with pseudo code) - this goes hand and hand with YAGNI

        // ** Inheritance ** we've discussed this before - seems harder than it actually is. 
        // love love love her statement about only using as much (or as little) inheritance as required 
        // by the requirements.
        //     "Only implement an inheritance relationship if the specific class type adds unique code."
        // This is a great example of YAGNI!





        // ****************  "I have nothing to say." sir Xavier the magnificent (my nephew)

        // you didn't really believe that did you?

        // but yeah, I am not going to spend time just restating these awesome videos on inheritance/reuse. But there is 
        // a lot of information here. I encourage you to re-watch all of these videos when you start your capstone







        // *** REUSE THROUGH INHERITANCE *** 
        // I generally argue that the only valid reason for using inheritance is if it provides code reuse!
        // Inheritance that doesn't provide reuse is just over complication.


        // But... questions or things that didn't quite make sense?????
        //    does inheritance make sense?
        //    does polymorphism make sense? (same function - multiple (poly) shapes (morph) ) 
        //    does an Abstract class make sense?  (abstract class can only be used as a base class)

        // Cool trick she used: write/run a unit test to execute some code and see what it does rather
        // than running the whole program.
        // I do this all the time! (and often delete the unit test code when I'm done.)

        // the EntityBase abstract class she builds is a common pattern - and a perfect example of inheritance/reuse.
        // as well as a great example of an abstract class.


        // *** Building Reusable Components *** 
        // YAAASS! love the Acme.Common project
        //     a perfect example of including a DLL (Dynamically Linked LIBRARY) project.   
        // and the StringHandler class. Perfect example of a utility class! 
        //     love how she showed it could (should) be a static class. This is generally 
        //     true of utility classes.
        //   
        // LOVE the testing approach here! 
        // Extension Methods - best approach for utility methods because they are the 
        //   most likely to be reused - in no small part b/c they show in intellisense



        // *** Interfaces!!!!! ***  specifically CLASS Interfaces

        // if your  base class doesn't have any concrete properties or methods
        // (if it's marked abstract it can't have concrete methods)
        // or if you want to leave the implementation details of the base properties
        // up to the descendant class
        // then it can be (and likely should be) an Interface instead.
        // This is the buttons/switches/plugs/etc on our eggshell

        // can use Ctrl-.  to automagically generate an interface.

        // dependency injection relies heavily on interfaces.

        // love "think of an interface as a contract"

        // The whole point is  you can use an Interface anywhere you would use a type,
        // which allows you to use different types of objects interchangeably, as long
        // as they implement the same interface! All with the benefits of Strong typing.



    }
}
