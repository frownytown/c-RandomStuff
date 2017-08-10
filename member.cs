using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Member
    {
        // Member Name
        private string name;
        // Member's preferred sport
        private string sport;
        // Member Expiration Month
        private int monthNumber;
        // Member Expiration Year
        private int yearNumber;
        // Member Expired Status'
        private bool expired;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Sport
        {
            get
            {
                return this.sport;
            }
            set
            {
                this.sport = value;
            }
        }
        public int ExpirationMonth
        {
            get
            {
                return this.monthNumber;
            }
            set
            {
                this.monthNumber = value;
            }
        }
        public int ExpirationYear
        {
            get
            {
                return this.yearNumber;
            }
            set
            {
                this.yearNumber = value;
            }
        }
        public bool ExpiredStatus
        {
            get
            {
                return this.expired;
            }
            set
            {
                this.expired = value;
            }
        }
        // Default version of constructer
        public Member()
        {
            this.name = "N/a";
            this.sport = "None Chosen";
            this.ExpirationMonth = 0;
            this.ExpirationYear = 0;
            this.ExpiredStatus = false;
        }
        // Constructer with parameters(preferred)
        public Member(string name, string sport, int monthNumber, int yearNumber, bool expired)
        {
            this.name = name;
            this.sport = sport;
            this.monthNumber = monthNumber;
            this.yearNumber = yearNumber;
            this.expired = expired;
        }
        public void PrintInformation()
        {
            Console.WriteLine("This member's name is {0}. Their preferred sport is {1}. Their membership expires at the end of {2}-{3} \nTheir expiration status is {4}",
                name, sport, ExpirationMonth.ToString(), ExpirationYear.ToString(), ExpiredStatus.ToString());
        }
    }
}
