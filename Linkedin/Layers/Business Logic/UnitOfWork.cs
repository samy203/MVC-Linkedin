namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork
    {
        private static UnitOfWork instance = null;

        public ApplicationDbContext context;


        private static readonly object padlock = new object();

       UnitOfWork()
        {
            context = new ApplicationDbContext();
        }

        public static UnitOfWork Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UnitOfWork();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Gets a generic data type of an entity manager, this function will automatically detect any new managers added to the assembly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>()
        {
            List<object> listObj = new List<object>();

            foreach (var type in typeof(Repository<,>).Assembly.GetTypes())
            {
                //Checking if it is a derived class of the Generic base class
                //the simple .IsAssignable(derived type) will not work with 
                //Generic data types, so one must manually handle it.
                if (IsDerivedclassOfRawGeneric(typeof(Repository<,>), type)
                    && type != typeof(Repository<,>))
                {
                    //Creating an instance and passing the context to it.
                    listObj.Add(Activator.CreateInstance(type, context));
                }
            }
            //Casting the object the data type passed (Generic T) and returning it.
            return (T)listObj.Find(t => t is T);
        }

        /// <summary>
        /// Checks if the inquired type is a derived class of the generic base data type.
        /// </summary>
        /// <param name="genericBaseDataType"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool IsDerivedclassOfRawGeneric(Type genericBaseDataType, Type inquiredType)
        {
            //While the type is no null or object.
            while (inquiredType != null && inquiredType != typeof(object))
            {
                //Getting the generic type definition if it's a generic.
                var cur = inquiredType.IsGenericType ? inquiredType.GetGenericTypeDefinition() : inquiredType;
                //If this class is the same as the generic type then return true
                //if not get the base (Which should equal the generic).

                if (genericBaseDataType == cur)
                {
                    return true;
                }
                inquiredType = inquiredType.BaseType;
            }
            return false;
        }

    }
}