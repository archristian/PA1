using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PA1
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice; //Main menu choice

            
            Console.WriteLine("Welcome to Big Al Goes Social! Roll Tide!");
            Start:        //I know this is kind of sketchy, but can someone explain to me what's so bad about using goto for just a main menu. Seems harmless to me.
            Console.WriteLine("Please enter the number of the menu item you would like to select:");
            Console.WriteLine("1. Show all posts");
            Console.WriteLine("2. Add a post");
            Console.WriteLine("3. Delete a post");
            Console.WriteLine("4. Exit program");

            choice = int.Parse(Console.ReadLine());
            
            
            if (choice == 1) //Show posts
            {
                List<Post> socialPost = new List<Post>(); //Initialize list
                StreamReader inFile = null;

                try 
                {
                    inFile = new StreamReader("posts.txt");

                    string line = inFile.ReadLine(); //Read text
                    while(line != null) 
                    {
                        string[] temp = line.Split("#"); //Split text
                        int tempId = int.Parse(temp[0]);
                        DateTime tempTime = DateTime.Parse(temp[2]); //Parsing to get everything to the right property
                        
                        socialPost.Add(new Post(){id = tempId, sentence = temp[1], timeStamp = tempTime}); //Storing object
                        line = inFile.ReadLine(); //Next line
                    }

                    inFile.Close();
                    socialPost = socialPost.OrderByDescending(o=>o.timeStamp).ToList(); //Sort list
                    
                    foreach(Post post in socialPost) 
                    {
                        Console.WriteLine(post.id + "  " + post.sentence + "  " + post.timeStamp); //Type out list
                    }

                    goto Start;
                    
                    
                }
                catch (FileNotFoundException x)
                {
                    Console.WriteLine("Something went wrong" + x); //File couldn't be found
                    goto Start;
                }
            }

            if (choice == 2) //Add post
            {
                string writeHere;
                int id;
                DateTime currDate;

                Console.WriteLine("What would you like to post?");
                writeHere = Console.ReadLine(); //Post writing

                Random random = new Random(); //Randomize Id
                id = random.Next(0,1000);

                currDate = DateTime.Now; //Get current date and time

                
                using (StreamWriter outFile = File.AppendText("posts.txt")) 
                {
                    outFile.WriteLine(id + "#" + writeHere + "#" + currDate); //Write to file
                }

                goto Start;

            }

            if (choice == 3) //Remove post
            {
                List<Post> socialPost = new List<Post>(); //Initialize list
                StreamReader inFile = null;

                try 
                {
                    inFile = new StreamReader("posts.txt");

                    string line = inFile.ReadLine(); //Read text
                    while(line != null) 
                    {
                        string[] temp = line.Split("#"); //Split text
                        int tempId = int.Parse(temp[0]);
                        DateTime tempTime = DateTime.Parse(temp[2]); //Parsing to get everything to the right property
                        
                        socialPost.Add(new Post(){id = tempId, sentence = temp[1], timeStamp = tempTime}); //Storing object
                        line = inFile.ReadLine(); //Next line
                    }
                    inFile.Close();

                }  
                catch(FileNotFoundException x)
                {
                    Console.WriteLine("Something went wrong" + x); //File couldn't be found
                    goto Start;
                }
                Console.WriteLine("Enter ID of post you would like to delete: ");
                int tempId2 = int.Parse(Console.ReadLine()); //Read in id

                int foundIndex = socialPost.FindIndex(tempPost => tempPost.id == tempId2); //Find ID
                if (foundIndex != -1) //As long as post exists
                {
                    socialPost.RemoveAt(foundIndex); //Remove post
                    Console.WriteLine("Post removed");
                    using (StreamWriter outFile = new StreamWriter("posts.txt")) //Rewrite to file without post
                    {
                        foreach(Post post in socialPost) 
                        {
                            outFile.WriteLine(post.id + "#" + post.sentence + "#" + post.timeStamp);
                        }

                    }
                }
                else 
                {
                    Console.WriteLine("Post could not be found"); //Post doesn't exist
                }

                goto Start;


            }

            if (choice == 4) 
            {
                Console.WriteLine("Goodbye!");
                return; //Exit Application
            }

            else 
            {
                Console.WriteLine("Invalid choice."); //Exception handling
                goto Start;
            }
            
            
        }
    }
}
