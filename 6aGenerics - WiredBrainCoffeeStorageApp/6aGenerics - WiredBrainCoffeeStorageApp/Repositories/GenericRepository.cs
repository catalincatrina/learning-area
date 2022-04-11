﻿using _6aGenerics___WiredBrainCoffeeStorageApp.Entities;

namespace _6aGenerics___WiredBrainCoffeeStorageApp.Repositories
{
    // "where T : EntityBase" is called a type constraint, we can't call GenericRepository with another type other than EntityBase and its children
    // we use type constraints when we need to be able to access the type's properties
    public class GenericRepository<T> where T : class, IEntity
    {
        // can't use var for fields
        // private var _employees = new List<Employee>();

        private List<T> _items = new List<T>();

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public void Save()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item);

                // Same thing as
                // Console.WriteLine(item.ToString());
            }
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Theory examples non-related to the app below
    // Non-generic class inheriting from a generic class
    public class NonGenericInheritFromGeneric : GenericRepository<Employee> { }

    // Generic class inheriting from another generic class
    public class GenericInheritFromGeneric<T> : GenericRepository<T> where T : EntityBase { }

    // Class with multiple type generics
    public class CoolGenericRepository<TItem, TKey>
    {
        public TKey? key;
        public List<TItem> myList = new List<TItem>();

        public void Add(TItem item)
        {
            myList.Add(item);
        }

        public TItem Remove(TItem item)
        {
            myList.Remove(item);
            return item;
        }

        public void ShowAllItems()
        {
            foreach (TItem item in myList)
            {
                Console.WriteLine($"Item: {item}");
            }
        }
    }

    // Class inheriting from class with multiple type generics
    public class RepoWithMoreFeatures<TItem, TKey> : CoolGenericRepository<TItem, TKey> { }

    // Class inheriting from class with multiple type generics - Generic types names in the child class don't need to match the names in the base class
    public class RepoWithEvenMoreFeatures<T, TSuperKey> : CoolGenericRepository<T, TSuperKey> { }

    // Class inheriting from class with multiple type generics - Child classes can use more generic type parameters than the base class
    public class RepowithBadassFeatures<TItem, TKey, TValue> : CoolGenericRepository<TItem, TKey> { }

    // Class inheriting from class with multiple type generics - Child classes can use less generic type parameters than the base class as well
    public class RepowithReallyBadassFeatures<TItem> : CoolGenericRepository<TItem, String> { }

    // T must be of EntityBase type, and so it is a reference type
    public class GenericRepository2<T> where T : EntityBase, IEntity
    {
        public List<T> myList = new List<T>();
        public T Add(T item)
        {
            item.Id = myList.Count + 1;
            myList.Add(item);
            return item;
        }
    }

    // T must be a IEntity type, so it can be a value type or a reference type
    public class GenericRepository3<T> where T : IEntity
    {
        public List<T> myList = new List<T>();
        
        public T? GetById(int id)
        {
            //return myList.Single(item => item.Id == id);
            //Because T might be a value type or a reference type, we can't return null because it could be a non-nullable value type
            //return null;
            // So we have to return default(T), or straight up return default because it's specified in the method what type must be returned
            // In this case we also have to add ? to T (T?) to make it a nullable reference type
            // Now this default key will return 0 if T is a numeric type, and it will return null if T is a reference type
            return default(T);
        }

        // We can also specify a class constraint, which says T is a reference type, and so we can return null from the GetById method
        public class GenericRepository4<T> where T : class, IEntity
        {
            private List<T> myCoolList = new();

            public T? GetById(int id)
            {
                // We can also return null but we must specify that T is a class (reference type)
                return null;
            }
        }

        // We can also specify the struct constraint to specify that T must be a value type
        public class GenericRepository5<T> where T: struct, IEntity { }

        // We can also define other base classes like System.Enum to specify that T is an enum
        public class GenericRepository6<T> where T: System.Enum, IEntity { }
    }
}
