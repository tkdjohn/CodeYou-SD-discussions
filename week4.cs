using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discussions
{
    /**********************************************************************************************************************************
     * “Saruman believes that it is only great power that can hold evil in check. But that is not what I have found. I have found it is 
     * the small things, everyday deeds of ordinary folk, that keeps the darkness at bay. Simple acts of kindness and love.” 
     *  -Gandalf 
     *  
     *  This isn't just a timely quote, it's also true in software development. Small everyday changes made by ordinary devs will always 
     *  build stronger, better, more reliable, easier to maintain software than big sweeping changes made by wizards. 
     *  
     *  Or to put it another way, if you code isn't where you want it to be, make small changes to move it closer to what you want and 
     *  then re-evaulate. You'll often find that after a few such changes, that the code is in a better place than what you envisioned
     *  when starting those changes.
     *  
     *  Small, incremental chagnes, by the way, is the most fundamental principle of Agile Software Development. 
     **********************************************************************************************************************************/

    // switch/case statement - on more flow control option
    public static class SalesTaxCalculator
    {

        // UGH imagine how hard to read for all 50 states plus 40+ territories, protectorates, and Nations
        public static decimal CalculateSalesTax1(string stateAbbreviation, decimal purchaseAmount)
        {

            if (stateAbbreviation == "KY" || stateAbbreviation == "ky" || stateAbbreviation == "Ky" || stateAbbreviation == "kY")
            {
                return purchaseAmount * 0.06M;
            }
            // or else if.. 
            if (stateAbbreviation == "IN" || stateAbbreviation == "in" || stateAbbreviation == "In" || stateAbbreviation == "iN")
            {
                return purchaseAmount * 0.07M;
            }

            // ... TODO: add more states.
            return purchaseAmount * 0.01M;    
        }

        // Better! 
        public static decimal CalculateSalesTax2(string stateAbbreviation, decimal purchaseAmount)
        {
            decimal taxAmount = 0;
            switch (stateAbbreviation)
            {
                case "KY": 
                case "ky": // fallthroug intentional
                case "Ky": // fallthroug intentional
                case "kY": // fallthroug intentional
                case "WV": // fallthroug intentional
                case "wv": // fallthroug intentional
                case "Wv": // fallthroug intentional
                case "wV": // fallthroug intentional
                    taxAmount = purchaseAmount * 0.06M;
                    break; // don't forget break;
                case "IN":
                case "in":
                case "In":
                case "iN":
                    taxAmount = purchaseAmount * 0.07M;
                    break;
                    
                // TODO: moar states

            default: // switch should always have a default
                    taxAmount = purchaseAmount * 0.10M;
                    break;
            }

            return taxAmount;
        }


        // Best
        public static decimal CalculateSalesTax3(string stateAbbreviation, decimal purchaseAmount)
        {
            if (string.IsNullOrEmpty(stateAbbreviation))
            {
                throw new ArgumentNullException(nameof(stateAbbreviation));
            }

            switch (stateAbbreviation.ToUpper())
            {
                case "KY":
                case "WV": // fallthrough intentional
                    return purchaseAmount * 0.06M; // break not needed because return
                case "IN":
                    return purchaseAmount * 0.07M;
                // TODO: add more states
            default: // switch should always have a default
                return purchaseAmount * 0.10M;
            }
        }

    }

    // Understanding Object Oriented Programming is important - OOP is the basis of most modern programming
    // but don't worry if it's not all clicking just yet. It seems more complicated than it is.

    // really OOP is all about grouping related code/variables (data) together in ways that premote code reuse

    /****************** 

    We talked Object v/s Class last week. 
    Class is the code that defines an object type's methods and properties - clases are templates.
    Object is the actual thing created from the class template. 

    some types of classes:
        Business Objects 
            - actually are classes that are defined for solving a business problem d
            - might also be an Entity 

        Entity 
            - entities are classes that represent things  
            - often used to as a generic term for classes that are stored in a table in the database

        Service Class/Object 
            - a class that handles the business logic related to a group of Entities/Business objects
            - typically only one instance (hence why class/object become interchangable)

        Repository Class/Object
            - a class that handles the backed details of storing Entities (typically in a database)
            - generally single instance, though not necessairly

        Factory Class/Object
            - a class that is used to create and 'hydrate' and another class. (not so common anymore)

        POCO - Plain Old Common Object 
            - refers to a class that is mostly member variables (Entities are often POCOs)
            - usually very few or no methods
            - may be used to group other classes together to make them eaiser to pass to other class methods

        Container Class 
            - a class that contains a group of other classes - Collectctions (List, Dictionary, etc) are containers 
            - can be quite complicated (eg have multiple lists, and a mix of other clasess)
            - typically has no methods - just exists to group other classes together        

    Any qustions here before we move on?

    -------------
    Seperation of Concers 
        - fancy way of saying classes should only be responisbility for doing one thing

            - in PetShop segregating the user interface from the product logic is an example of this
        - that thing can be high level and abastract in nature, and then rely on other classes to  handle the details
            - for example, we could have a UserManagement Class that the depends on other classes to do the customer or employee specific (separate class for each)
    ---------
    Abstraction - Idenitfying classes, and limiting them to appropriate details as defined by the requirements. (don't care about user's pets)

    Encapsulation - hiding/containing data/implementatin within a class so external code doesn't need to worry about it. 
        - think of it as wrapping the data and the methods that operate on it in a black box we call the class



    ****************/


    // Layering - IS OOP (contrary to the video)!  it is part of abstraction and will define program structure
    // and therefore will define operational objects
   
    /// Use Auto implemented properties unless  you specifically need access to the backing field or want a custom setter or getter
    
    /// Snippits are great - esp for remembering syntax -- but not necessary for you to understand
    ///     ctor - create a constrctor
    ///     prop - create a property
    ///     -- can add your own - I have one for tests that includes // arrange // act // assert comments to remind me
    /// 


    /********** Unit tests
    * more in M3 week 2
    * arrange, act, assert are good reminders
    * only test one thing at a time
    * name well (describe what is being tested
    ***********/

    // Objects are reference types - assignment changes the reference!
    public class User { // HEY look a POCO that is also an Entity
        public required int Id { get; set; }
        public required string Name { get; set; }
    }

    class ReferenceTypesExample
    {
        private static void ReferenceTypes()
        {
            int i1 = 1;
            int i2 = i1;
            i2 = 2;
            // what is i1? it's 1.

            User o1 = new() { Id = 1, Name = "Samwise Gamgee" };
            User o2 = o1;
            o2.Id = 2;

            // what is o1.Id ?? it's 2.

        }
    }
    //*next week* static modifier for members, for classes. rare but useful. more later 
    //    static members exist across all instances of the class. access using class name, not instance name.

    // Method Overloading - same name but different parameters. Should return same type to avoid confusions.

    // outlining is useful, but latest Visual Studio does it for you

    //*next week* Constructor v/s initiazer syntax (see above)

    /************************ 
     * Separating Responsibilities
     *      note that we built the initial classes FIRST. Don't try to combine identifying classes with separating responsibilities steps right now. (Premature optimization) 
     *      as things get more complex, separate 
     *      
     *      Terms
     *          coupling - classes should not be tightly coupled, ie should not depend on each other 
     *              - unless it is a relationship dependancy - eg. customer has an address
     *              - 
     *          cohesion - group related things together, no need to track data in multiple classes, make an address class!
     *          
     *     YAGNI - You Aren't Going To Need It - 75-90% of all "we'll want that later" statements are false, even when made by the SMEs
     *     
     *     Design patterns - common "best practice" patterns that are accepted ways of orgainzing code by responsiblity 
     *          - repository pattern (or repo)   
     ******************/

    
}

