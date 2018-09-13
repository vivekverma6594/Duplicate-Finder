using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFinder
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string StateLong { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }



        public Person(int id, string firstName, string lastName, string company, string email, string address1, string address2, string zip, string city, string stateLong, string state, string phone)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Email = email;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
            City = city;
            StateLong = stateLong;
            State = state;
            Phone = phone;
        }
    }
}
