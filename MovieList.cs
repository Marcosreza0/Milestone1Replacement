using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Milestone
{
    public class MovieList
    {
        public string Title {get;set;}
        public List<string> Genre {get;set;}
        public List<Director> Director {get;set;}
        public List<string> StarActors {get;set;}
        public string  IMBD { get; set; }

        public MovieList()
        {
            Title = string.Empty;
            Genre = new List<string>();
            Director = new List<Director>();
            StarActors= new List<string>();
            IMBD = string.Empty;
        }

        public override string ToString()
        {
            string listG = string.Join("/", Genre);
            string listA = string.Join("; ", StarActors);
            string listD = string.Join("; ", Director);
            return $"{Title}, {listG}, {listD}, {listA}, {IMBD}";
        }
    }

    public class Director
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string DateOfBirth {get; set;}
        public string URl { get; set; }
       public Director()
       {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = string.Empty;
        URl = string.Empty;
       }

       public override string ToString()
       {
        return $"{FirstName}|{LastName}|{DateOfBirth}|{URl}";
       }


    }


    
    
   
}
