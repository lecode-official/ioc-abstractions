
#region Using Directives

using System.InversionOfControl.Abstractions;
using System.InversionOfControl.Abstractions.Ninject;
using System.InversionOfControl.Abstractions.SimpleIoc;

#endregion

namespace IocAbstractions.Samples.Console
{
    /// <summary>
    /// Represents the sample console application, which showcases the abilities of the IoC Abstractions framework.
    /// </summary>
    public class Program
    {
        #region Public Static Methods

        /// <summary>
        /// Represents the entry point to the application.
        /// </summary>
        /// <param name="args">The command line arguments, which should always be empty, since they are not used in the application.</param>
        public static void Main(string[] args)
        {
            // Asks the user to specifiy which IoC container shall be used
            System.Console.Write("Use 'SimpleIoC' or 'Ninject': ");
            IIocContainer iocContainer;
            if (System.Console.ReadLine().ToUpperInvariant() == "NINJECT")
                iocContainer = new NinjectIocContainer();
            else
                iocContainer = new SimpleIocContainer();

            // Binds the vehicles to the kernel
            iocContainer.RegisterType<IVehicle, Car>();
            iocContainer.RegisterType<IVehicle, Motorcycle>(typeof(SuperCoolPerson)); // Obviously super cool people drive motorcycles!

            // Creates some persons
            Person person = iocContainer.GetInstance<Person>();
            Person superCoolPerson = iocContainer.GetInstance<SuperCoolPerson>();
            Person namedPerson = iocContainer.GetInstance<NamedPerson>("Bob");

            // Prints out the personal information about the persons that were created
            System.Console.WriteLine(person);
            System.Console.WriteLine(superCoolPerson);
            System.Console.WriteLine(namedPerson);

            // Waits for a key stroke, before the application is quit
            System.Console.WriteLine("Press any key to quit...");
            System.Console.ReadKey();
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Represents a person.
        /// </summary>
        private class Person
        {
            #region Constructors

            /// <summary>
            /// Initializes a new <see cref="Person"/> instance.
            /// </summary>
            /// <param name="vehicle">The vehicle that the person is driving.</param>
            public Person(IVehicle vehicle)
            {
                this.Vehicle = vehicle;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the vehicle that the person is driving.
            /// </summary>
            public IVehicle Vehicle { get; set; }

            #endregion

            #region Object Implementation

            /// <summary>
            /// Generates a string out of the person object.
            /// </summary>
            /// <returns>Returns the textual representation of the person.</returns>
            public override string ToString() => $"The person is driving a {this.Vehicle.Name}.";

            #endregion
        }

        /// <summary>
        /// Represents a super cool person.
        /// </summary>
        private class SuperCoolPerson : Person
        {
            #region Constructors

            /// <summary>
            /// Initializes a new <see cref="SuperCoolPerson"/> instance.
            /// </summary>
            /// <param name="vehicle">The vehicle that the super cool person is driving.</param>
            public SuperCoolPerson(IVehicle vehicle)
                : base(vehicle)
            { }

            #endregion
        }

        /// <summary>
        /// Represents a person, which has a name, for whatever that is good for.
        /// </summary>
        private class NamedPerson : Person
        {
            #region Constructors

            /// <summary>
            /// Initializes a new <see cref="NamedPerson"/> instance (this constructor is only here to show off that the best matching constructor is selected when resolving a type).
            /// </summary>
            /// <param name="vehicle">The vehicle the person is driving.</param>
            public NamedPerson(IVehicle vehicle)
                : base(vehicle)
            { }

            /// <summary>
            /// Initializes a new <see cref="NamedPerson"/> instance.
            /// </summary>
            /// <param name="name">The name of the person.</param>
            /// <param name="vehicle">The vehicle the person is driving.</param>
            public NamedPerson(string name, IVehicle vehicle)
                : base(vehicle)
            {
                this.Name = name;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the name of the person.
            /// </summary>
            public string Name { get; set; }

            #endregion

            #region Object Implementation

            /// <summary>
            /// Generates a string out of the person object.
            /// </summary>
            /// <returns>Returns the textual representation of the person.</returns>
            public override string ToString() => $"{this.Name} is driving a {this.Vehicle.Name}.";

            #endregion
        }

        /// <summary>
        /// Represents an interface for vehicles.
        /// </summary>
        private interface IVehicle
        {
            #region Properties

            /// <summary>
            /// Gets the name of the vehicle.
            /// </summary>
            string Name { get; }

            #endregion
        }

        /// <summary>
        /// Represents a car.
        /// </summary>
        private class Car : IVehicle
        {
            #region IVehicle Implementation

            /// <summary>
            /// Gets the name of the vehicle.
            /// </summary>
            public string Name
            {
                get
                {
                    return "car";
                }
            }

            #endregion
        }

        /// <summary>
        /// Represents a motorcycle.
        /// </summary>
        private class Motorcycle : IVehicle
        {
            #region IVehicle Implementation

            /// <summary>
            /// Gets the name of the vehicle.
            /// </summary>
            public string Name
            {
                get
                {
                    return "motorcycle";
                }
            }

            #endregion
        }

        #endregion
    }
}