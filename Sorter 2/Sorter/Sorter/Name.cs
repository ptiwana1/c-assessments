using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sorter
{
    public class Name
    {
        private string _firstname;
        private string _lastname;


        //Gets and Sets the FirstName
        public string FirstName
        {
            get
            {
                return this._firstname;
            }
            set
            {
                this._firstname = value;
            }
        }
        //Gets and Sets the LastName
        public string LastName
        {
            get
            {
                return this._lastname;
            }
            set
            {
                this._lastname = value;
            }
        }

        //Constructor with two arguments
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        //Overriding the toString() method to display the objects in string form
        public override string ToString()
        {
            return FirstName + " " + LastName;
                 
        }
    }
}
