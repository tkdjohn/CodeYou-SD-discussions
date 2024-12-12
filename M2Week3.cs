using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Discussions {
    internal class M2Week3 {

        //                 Generics! 

        // Pretty good videos here. But as usual it covers more than you really need
        // right now. 
        // So, here's the things to pay attention (and the things to ignore).

        // Love the way he uses shortcuts to build the basic SimpleStack class and add
        // the push method.

        // The info on various ways to increment an integer is great, but irrelevant
        // to the topic of generics. here's the summary look it over and we'll move on
        // if t
        int i = -2;
        public void Increments() {
            i = i + 2; // sets i to -2 + 2 (i = 0) 
            i += 1; // sets i  to 0 + 2 ( i = 2) 
            i++; // returns 2 and then increments i to 3
            ++i; // increments i to 4 and returns 4;

            // because the ++ statements return the value, you can 
            // use them inline with other statements - you can send the 
            // statements with = signs as arguments too but no one does this b/c
            // it's hard to read/understand

            SomeMethod(i++); // i = 7 and sends 6 to SomeMehtod
            SomeMethod(++i); // i = 8 an sends 8 to SomeMethod
        }
        public void SomeMethod(int i) { }


        // OK back to generics. The short of it is that .Net has features that 
        // allow for the same code to work for different types. All the collections we 
        // discussed weeks ago are build with generics. This is why you can have
        // both List<string> and List<int> and List<object> etc.
        // 

        // Don't sweat the talk about boxing/unboxing and the ildasm tool. you don't need
        // to know this as an entry level dev.

        // Using the generic object to allow for different types isn't a great approach
        // because it breaks type safety and requires casting.

        // Obviously, having different code for different types results in code duplication
        // and is more complicated and therefore harder to maintain.

        // Finally we get actual generics. 
        // Note that the generic type parameter can be named anything, best practice and 
        // convention say it should start with a capital T. (That's the part inside the 
        // angle brackets eg <TItem>
        // 
    }
    public class GenericsTypeParamenter<TItem> {
        public TItem SomeOtherMethod(TItem input) {
            return input;
        }
    }

    // Can have generic classes, generic methods, generic interfaces, and 
    // even generic delegates (more on delegates in a bit)


    // Again the nullable stuff he goes over is great, but not directly relevant
    // to generics. Do we need to discuss nullables? 
    // note that Objects (which includes strings) can be null without the nullable
    // operator (?), but simple types can't. There is a project option you can turn
    // on to generate warnings to force you to declare objects as nullable too.

    public class Nullables {
        int i = null; // simple type not nullable
        int? iNullable = null;
        string s = null; // doesn't need the '?', though you can turn on a warning
        object o = null; // to require the ? or error if assigned null.
    }

    // The Repository pattern that he follows in section 3 "Implementing Generic
    // Classes" is a good pattern to know. A 'repository' is an object that handles
    // the details of data storage, including CRUD operations at the data layer.
    // It's a great way to separate the concerns of data storage/mgmt from 
    // other logic. Saying "repository" all the time is too much work, most devs
    // shorten this to "repo"  

    // here's my version of his generic repo class. Note that it can work
    // for ANY type, simple types or objects
    public class ListRepository<T> : IRepository<T> where T : IEntity {
        protected readonly List<T> items = [];

        public void Add(T item) => items.Add(item);

        public void DisplayItems() => items.ForEach(item => { Console.Write(item); });

        public T? Get(int id) => items.FirstOrDefault(x => x.Id == id);

        public void Remove(T item) => items.Remove(item);

        public void Save() => DisplayItems();

        public IEnumerable<T> GetAll() => items.ToList(); // return a copy
    }


    // you can create both generic and type specific (non-generic) sub classes
    // that inherit from a generic class.
    public class NonGenericRepo : ListRepository<Employee> {
        // do User related things.
    }

    public class InheritedGenericRepo<T> : ListRepository<T> where T : IEntity {
        // extend Generic Repository
    }

    // you can have multiple type parameters, but don't go overboard, this can get
    // quickly hard to understand what's going on.
    public class StringIndexedDictionary<T> where T : Employee {
        // let's just look at the Dictionary type to see an example of
        // multiple generic type parameters - but also note that 
        // in this case the value type of the dictionary is dependent 
        // on the type parameter to the containing class StringIndexedDictionary
        private Dictionary<string, T> myDictionary = [];
    }
    // see above where clause to see how to limit generic type, in this case
    // to a single class, but you could also use a parent or abstract class, 
    // or even a simple type.


    // The EntityBase pattern he shows is a common pattern too.

    public class EntityBase : IEntity { // has Id and maybe other fields
        public int Id { get; set; }
    }
    public interface IEntity {
        public int Id { get; set; }
    }
    // now all entity classes can inherit from Entity base and have the 
    // same code managing IDs.
    public class Employee : EntityBase { // inherits form Entity base 
        public string Name { get; set; }
        public void PrintMyId() {
            Console.Write(Id);
        }
    }

    // the where clause can also limit by interface - making the where clause
    // super powerful.
    // Most LINQ statements are generics that are limited to classes that 
    // implement the system interface IEnumberable. Likewise the foreach()
    // command is limited to classes that implement IEnumberable.

    // don't sweat the new constraint right now - just be aware it exits.

    // AND interfaces can have generic type parameters too, very similar
    // to classes.

    // LOVE LOVE the great examples he gives of using an in memory database
    // very useful for testing or building sample applications without 
    // actually having to have a database. (hint hint hint) 
    // strongly recommend you come back and rewatch module  4 of
    // these videos when you are ready to implement a database in your
    // capstone

    public interface IRepository<T> where T : IEntity {
        public T? Get(int id);
        public void Add(T item);
        public void Remove(T item);
        public void Save();

        IEnumerable<T> GetAll();
    }
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity {
        private readonly DbContext dbContext;
        private readonly DbSet<T> dbSet;
        public SqlRepository(DbContext dbContext) {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = dbContext.Set<T>();
        }
        public T? Get(int id) => dbSet.FirstOrDefault(x => x.Id == id);

        public void Add(T item) => dbSet.Add(item);

        public void Remove(T item) => dbSet.Remove(item);

        public void Save() => dbContext.SaveChanges();

        public IEnumerable<T> GetAll() => dbSet.OrderBy(x => x.Id).ToList();

        public T? GetAndRemove(T item) {
            var myItem = dbSet.Find(item);
            if (myItem != null) {
                Remove(myItem);
            }
            return myItem;
        }
    }

    // You don't have to remember Covariance and Contravariance. 
    // just remember that when using generics you can't use a type that is LESS
    // specific nor MORE specific than what is specified without jumping through hoops.
    // https://learn.microsoft.com/en-us/dotnet/standard/generics/covariance-and-contravariance
    // TBH I've seen this used twice in 20 years and I doubt many senior dot net 
    // engineers could use these features without looking them up.

    // also don't sweat all the various ways of inheriting with generics he covers.
    // worry about how generics work with inheritance when you encounter it in the wild.

    // Methods can take generic types as arguments and can return generic types.
    // it works pretty much like you would think after learning about generic classes.
    // Get and GetAll above both return a generic type, Add and Remove both take a 
    // generic parameter. Where clause works on methods too.

    // The power of generics really is obvious with generic extension methods. But
    // generic extension methods can be a bit intimidating to write, but they really
    // aren't that difficult. try writing it with a specific type and then adapt to
    // use a generic type. Be sure to limit the applicable types with a where clause.

    public static class EntityExtensions {
        public static T? Copy<T>(this T itemToCopy) where T : IEntity {
            var x = JsonSerializer.Serialize(itemToCopy);
            return JsonSerializer.Deserialize<T>(x);
        }
    }

    // delegates are a special kind of method that more or less allow objects to
    // "share methods" at runtime. Don't sweat delegates much right now. Just think
    // of it as a variable that is a pointer to a method. Which allows us to implement
    // an "event" that calls an "event handler" that is set at runtime.
    // 

    // the Special Cases With Generics module is interesting bonus material, but isn't
    // anything you need to know right now. 


    // ===============================================================
    // do we want to put this off until next week (w4). This may push SQL out until W5
    // where I was hoping to cover one of the "I hope to spend some time talking about
    // that later." topics :) (y'all's choice!)

    // ===============================================================




    //                Entities: Best practices (ref code in ModelsCourse JurisTemps.sln)

    // Don't sweat understanding all of the sample code from these videos. 
    // it looks like he's mixing html and C# (and he is) but we're going
    // build the UI differently (using Blazor instead of MVC) in a few weeks.


    // Remember an Entity is a noun, an object that represents something 
    // in the real world that we want to store in a database (or other 
    // stat storage)

    // Entities typically have a unique identifier (such as a person's name)
    // but generally we want them to have a natural key as well (usually numeric 
    // such as an EmployeeId .. often we just use an integer and call it Id.

    // Entities can reference other entities, this is called a foreign key and in 
    // the database is usually just the unique Id of the referenced object.
    // ref Cases and Clients and Invoices (in this case in collections )

    // likewise entires can own other entities, ref Client and Address. In this case
    // Address doesn't have an Id. that goes against common practice. "own" and
    // "reference" aren't really distinguished in the code. 

    // dotnet ef database update - don't sweat this yet. we will see Entity Framework
    // in few weeks. 

    // "ViewModels" - data as it's displayed or viewed by UIs or other software. Often
    // the shape of Entities or even the data stored with Entities may differ from 
    // the way we want to shape the data to be shown in the UI - for example we may 
    // always want to show a customer's zip code or state when we show their name. even
    // though those two bits of information are in different Entities, they can be 
    // in the same View Model.  (If you've heard of the MVC or "Model View Controller" 
    // pattern View Models are the M.) Sometimes view models are not necessary and we
    // can just use the Entities themselves.
    // They are also often called Data Transfer Objects or DTOs.

    // WHY? sounds like extra complication - and in simple cases it is! but 
    // it provides separation of concerns across obvious layers (UI vs Business Logic)

    // in the video's example, he copies some but not all of the fields from the client 
    // entity to the client view model.

    // Mappers - map between things like entities and view models. it's exactly what it 
    // sounds like. Pretty useful tool to avoid lots of code duplication, but frankly
    // a bit above entry level.

    // the takeaway for you from the "Why Models Matter" module is to recognize that 
    // View Models allow for a separation between the "View" or UI and the Business 
    // logic and lets us shape the data elements in a way that is friendly to the view
    // and is mappable back to our Entities (which are shaped in a way that is friendly to
    // our business logic).

    // again don't sweat the html and forms there.

    and OMG I gave up. too much here you dont need to know.
            hopefully the validation vidoes are better.
}
