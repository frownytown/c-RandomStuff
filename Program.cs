using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main function creates dictionary for storage, before trying to run program
            Dictionary<string, Member> dataStorage = new Dictionary<string, Member>();

            try
            {
                dataStorage = StartUp(dataStorage);
                foreach (KeyValuePair<string, Member> kvp in dataStorage)
                {
                    Console.WriteLine("@{0} @{1} @{2} @{3} @{4}", kvp.Key,
                        kvp.Value.Sport, kvp.Value.ExpirationMonth, kvp.Value.ExpirationYear, kvp.Value.ExpiredStatus);
                }
                ProgramInterface(dataStorage);
                Console.ReadKey();
            }
            catch
            {
                Console.Error.WriteLine("Something went wrong");
            }
        }
        public static void SaveAndExit(Dictionary<string, Member> dataStorage)
        {
            // saves the database to a text file when you select to exit the program
            try
            {
                // writer instance
                string relativeFilePath = @"..\..\members.txt";
                StreamWriter writer = new StreamWriter(relativeFilePath);

                using (writer)
                {
                    foreach (KeyValuePair<string, Member> kvp in dataStorage)
                    {
                        writer.WriteLine("@{0} @{1} @{2} @{3} @{4}", kvp.Key, 
                            kvp.Value.Sport, kvp.Value.ExpirationMonth, kvp.Value.ExpirationYear, kvp.Value.ExpiredStatus);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong, with the save and exit");
                Console.ReadKey();
            }
        }
        public static Dictionary<string, Member> StartUp(Dictionary<string, Member> dataStorage)
        {
            // Function that runs on startup to try and rebuild the saved database from the text file "members.txt"
            try
            {
                string relativeFilePath = @"..\resources\members.txt";
             
                var textLines = File.ReadAllLines(relativeFilePath);

                foreach (var line in textLines)
                {
                    string[] blah = line.Split('@');
                    // rebuilds data from text file
                    {
                        string newName = blah[1];
                        string newSport = blah[2];
                        int newMonth = Convert.ToInt32(blah[3]);
                        int newYear = Convert.ToInt32(blah[4]);
                        bool newStatus = (blah[5].Contains("r"));
                        
                        Member newMember = new Member(newName, newSport, newMonth, newYear, newStatus);
                        dataStorage.Add(newMember.Name, newMember);
                       
                    }
                }
                
                return dataStorage;
            }
            catch
            {
                Console.WriteLine("Something went wrong");
                Console.WriteLine("Starting without loading text file...");
                return dataStorage;
            }
        }
        public static Dictionary<string, Member> AddMember(Dictionary<string, Member> dataStorage)
        {
            // Create dictionary for storage
            Console.WriteLine("What is the Member's full name");
            string newName = Console.ReadLine();
            Console.WriteLine("What is the Member's preferred sport?");
            string newSport = Console.ReadLine();
            Console.WriteLine("What is the number of the month that this membership will expire (1 through 12)?");
            int newMonth = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What is the year number that this membership will expire?");
            int newYear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Is this person's membership expired?");
            string response = Console.ReadLine();
            bool newStatus = (response.Contains('y'));

            Console.WriteLine("Press Y to confirm this addition or N to exit");
            string confirm = Console.ReadLine();
            if (confirm.Contains('y'))
            {
                Member newMember = new Member(newName, newSport, newMonth, newYear, newStatus);
                dataStorage.Add(newMember.Name, newMember);
                newMember.PrintInformation();
            }
            else
            {
                Console.Error.WriteLine("You have chosen to exit");
                ProgramInterface(dataStorage);
            }
            return dataStorage;
        }
        public static Dictionary<string, Member> IsExpired(Dictionary<string, Member> dataStorage)
        {
            // Creates a dictionary of members that are expired
            Dictionary<string, Member> expiredDict = new Dictionary<string, Member>();
            foreach (KeyValuePair<string, Member> kvp in dataStorage)
            {
                if (kvp.Value.ExpiredStatus == true)
                {
                    Console.WriteLine("Name = {0}", kvp.Key);
                    expiredDict.Add(kvp.Key, kvp.Value);
                }
            }
            Console.WriteLine("Are you an admin of this database?");
            string adminStatus = Console.ReadLine();
            if (adminStatus.Contains('y'))
            {
                Console.WriteLine("Excellent, press any key to continue");
                Console.ReadKey();
                Console.WriteLine("Please write the name of the person who's information you wish to edit");
                string response = Console.ReadLine();
                Console.WriteLine(response);
                try
                {
                    foreach (KeyValuePair<string, Member> keyValue in dataStorage)
                    {

                        if (keyValue.Key.Contains(response))
                        {
                            Console.WriteLine("Press any key to confirm");
                            Console.ReadKey();
                            dataStorage.Remove(keyValue.Key);
                            return dataStorage;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("I couldn't find that person, Please try again");
                    ProgramInterface(dataStorage);
                }
            }
            else
            {
                Console.WriteLine("ERROR! You must be an admin to complete this operation!");
                ProgramInterface(dataStorage);
            }

            return dataStorage;
        }
        public static Dictionary<string, Member> NotExpired(Dictionary<string, Member> dataStorage)
        {
            // Creates a dictionary of members that are not expired
            Dictionary<string, Member> trimmedDict = new Dictionary<string, Member>();
            foreach(KeyValuePair<string, Member> kvp in dataStorage)
            {
                if (kvp.Value.ExpiredStatus == false)
                {
                    trimmedDict.Add(kvp.Key, kvp.Value);
                    
                }
            }
            return trimmedDict;
        }
        public static void PrefSports(Dictionary<string, Member> trimmedDict)
        {
            // THis function takes the dict of sports that are not expired and prints them.
            Dictionary <string, int> histogram = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Member> sports in trimmedDict)
            {
                if (!histogram.ContainsKey(sports.Value.Sport))
                {
                    histogram[sports.Value.Sport] = 1;
                }
                else
                {
                    histogram[sports.Value.Sport]++;
                }
            }
            foreach (KeyValuePair<string, int> item in histogram.OrderBy(key => key.Value))
            {
                int comparator = Convert.ToInt32(item.Value);
                if ( comparator > 4)
                {
                    Console.WriteLine("These sports have more than 4 non expired players");
                    Console.WriteLine("{0}", item);
                    Console.ReadKey();
                }
            }
           
        }
        public static Dictionary<string, Member> UpdateExpiration(Dictionary<string, Member> myDict)
        {
            foreach (KeyValuePair<string, Member> kvp in myDict)
            {
                Console.WriteLine("Name = {0}", kvp.Key);
            }
            Console.WriteLine("Are you an admin of this database?");
            string adminStatus = Console.ReadLine();
            if (adminStatus.Contains('y'))
            {
                Console.WriteLine("Excellent, press any key to continue");
                Console.ReadKey();
                Console.WriteLine("Please write the name of the person who's information you wish to edit");
                string response = Console.ReadLine();
                try
                {
                    foreach (KeyValuePair<string, Member> keyValue in myDict)
                    {

                        if (keyValue.Key.Contains(response))
                        {
                            Console.WriteLine("What is the number of the month that this membership will expire (1 through 12)?");
                            myDict[keyValue.Key].ExpirationMonth = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("What is the year number that this membership will expire?");
                            myDict[keyValue.Key].ExpirationYear = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Is this person's membership expired?");
                            string answer = Console.ReadLine();
                            myDict[keyValue.Key].ExpiredStatus = (answer.Contains('y'));
                            myDict[keyValue.Key].PrintInformation();
                            return myDict;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("I couldn't find that person, Please try again");
                    ProgramInterface(myDict);
                }
            }
            else
            {
                Console.WriteLine("ERROR, Only admin have access to this section!");
                ProgramInterface(myDict);
            }
            
            
            return myDict;
        }
        public static Dictionary<string, Member> ProgramInterface(Dictionary<string, Member> dataStorage)
        {
            // user interface
            Console.WriteLine("\n Welcome! How can I help you? Select a response and hit ENTER");
            Console.WriteLine("1. Insert Member");
            Console.WriteLine("2. Update a Member's Membership Expiration date");
            Console.WriteLine("3. Find Preferred Sports");
            Console.WriteLine("4. Delete Inactive Members");
            Console.WriteLine("5. Save and Exit");
            int choice = int.Parse(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You have chosen to 1. Insert Member");
                        AddMember(dataStorage);
                        Console.WriteLine("Thank you for that, you will now return to the main screen");
                        Console.ReadKey();
                        ProgramInterface(dataStorage);
                        break;
                    case 2:
                        Console.WriteLine("You have chosen to 2. Update a Member's Membership Expiration date");
                        Console.ReadKey();
                        UpdateExpiration(dataStorage);
                        ProgramInterface(dataStorage);
                        break;
                    case 3:
                        Console.WriteLine("You have chosen to 3. Find Preferred Sports.");
                        Console.ReadKey();
                        PrefSports(NotExpired(dataStorage));
                        ProgramInterface(dataStorage);
                        break;
                    case 4:
                        Console.WriteLine("You have chosen to 4. Delete Inactive members");
                        Console.WriteLine("These people are inactive and may be deleted");
                        IsExpired(dataStorage);
                        ProgramInterface(dataStorage);
                        break;
                    case 5:
                        Console.WriteLine("You have chosen to 5. Exit");
                        Console.ReadKey();
                        SaveAndExit(dataStorage);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("default case");
                        break;
                }
               
            }
            catch
            {
                Console.Error.WriteLine("Something went wrong");
            }
            return dataStorage;
        }
    }
}
