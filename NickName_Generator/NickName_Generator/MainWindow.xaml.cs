using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace NickName_Generator
{
    public partial class MainWindow : Window
    {
        
        // Global Variables

        // Create List of Nicknames
        List<string> nickNames = new List<string>()
        {
            "The Wizard", "The Knight", "The Pilot", "The Gunslinger", "The Herald", "The Mistborn", "The Windunner", "The Priest", "The Necromancer", "The Channeler"
        };

        // Create Random Object
        Random randomNickName = new Random();
        
        // The Methods

        // This will be loaded first
        public MainWindow()
        {
            // This is the main method
            InitializeComponent();

            // Display All Nicknames so user can see them
            DisplayAllNicknames displayEveryNickname = new DisplayAllNicknames();
            displayEveryNickname.DisplayTheNicknameList(nickNames, DisplayAllNickNames);

        }

        // When Click Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Create the Object using the class nickNameCreate
            nickNameCreate drawObject = new nickNameCreate();
            nickNameChange changeNicknameList = new nickNameChange();

            // Check what button is pushed
            // I am well aware that you can just make a string with System.Windows.Controls.Button: and then add the part that actually changes but I'm far too lazy to actually do that. There's probally better ways to check what button is clicked but I'm also too lazy to research more.
            if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Display a Random Nickname")
            {
                drawObject.createNickname(DisplayNickNames, inputFirstName, nickNames, inputLastName, randomNickName, randomNickName.Next(0, nickNames.Count));
            } else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Display All Nicknames")
            {
                // Run a check  based on how many items are in the nickname string and then output it
                for (int i = 0; i < nickNames.Count; i++)
                {
                    drawObject.createNickname(DisplayNickNames, inputFirstName, nickNames, inputLastName, randomNickName, i);
                }
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Add a Nickname")
            {

                changeNicknameList.addNickname(nickNames, inputAddNickName, DisplayAddedOrRemoved, DisplayAllNickNames, scrollDownAll, scrollAddOrRemove);
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Remove a Nickname")
            {

                changeNicknameList.removeNickname(nickNames, inputRemoveNickName, DisplayAddedOrRemoved, DisplayAllNickNames, scrollDownAll);
            }
            else // Shut Down Windows
            {
                System.Windows.Application.Current.Shutdown();
            }


            scroll.ScrollToEnd();
        }

        // I'm just doing this so the main window looks nicer
        // Displaying all Nicknames

    }

    class DisplayAllNicknames
    {
        public void DisplayTheNicknameList(List<string> nickNames, TextBlock DisplayAllNickNames)
        {
            // Set the text to nothing so it can be maniuplated later
            DisplayAllNickNames.Text = "";

            // Display the box with all the Current Nicknames
            DisplayAllNickNames.Text += $"Current Nicknames used: \n";

            for (int i = 0; i < nickNames.Count; i++)
            {
                DisplayAllNickNames.Text += $"{nickNames[i]} \n";
            }
        }
    }

    // Create a Nickname based on the textbox getting, the input from the first name box, the nickname list, the input from the last name, the random number generator, and the position of the nickname getting
    class nickNameCreate
    {
        // Create a nickname
        public bool createNickname(TextBlock DisplayNickNames, TextBox inputFirstName, List<string> nickNames, TextBox inputLastName, Random randomNickName, int NickNum)
        {
            // Show the user the nickname
            DisplayNickNames.Text += $"{inputFirstName.Text} \"{nickNames[NickNum]}\" {inputLastName.Text} \n";

            // Return since the method needs it for some reason... don't know why but I have to keep it here
            return true;
        }
    }

    // Add or remove nickname based on the list of nicknames, the input from the inputAddNickName box
    class nickNameChange
    {

        // Add A NickName
        public void addNickname(List<string> nickNames, TextBox inputAddNickName, TextBlock DisplayAddedOrRemoved, TextBlock DisplayAllNickNames, ScrollViewer scrollDownAll, ScrollViewer scrollAddOrRemove)
        {
            // Check if this nickname already exists
            if (nickNames.Contains(inputAddNickName.Text))
            {
                // Display to the user that this already exists
                DisplayAddedOrRemoved.Text += $"\"{inputAddNickName.Text}\" already exists \n";
            } else
            {
                // Show the user that this was added
                DisplayAddedOrRemoved.Text += $"Added \"{inputAddNickName.Text}\" \n";

                // Actually add it the list
                nickNames.Add(inputAddNickName.Text);

                // Show the user on the main list display that it exists
                DisplayAllNickNames.Text += $"{inputAddNickName.Text} \n";

                // Scroll to the end to see changes made
                scrollDownAll.ScrollToEnd();
                scrollAddOrRemove.ScrollToEnd();
            }

        }

        // Remove a NickName
        public void removeNickname(List<string> nickNames, TextBox inputRemoveNickName, TextBlock DisplayAddedOrRemoved, TextBlock DisplayAllNickNames, ScrollViewer scrollDownAll)
        {
            // Check if this nickname exists
            if (nickNames.Contains(inputRemoveNickName.Text))
            {

                // Display that this text is removed
                DisplayAddedOrRemoved.Text += $"Removed \"{inputRemoveNickName.Text}\" \n";

                // Actually remove it from the list
                nickNames.Remove(inputRemoveNickName.Text);

                // Display All Nicknames so user can see them
                DisplayAllNicknames displayEveryNickname = new DisplayAllNicknames();
                displayEveryNickname.DisplayTheNicknameList(nickNames, DisplayAllNickNames);

                // Scroll to the bottom to see the changes made
                scrollDownAll.ScrollToEnd();
            }
            else
            {
                // Doesn't exist so nothing is changed
                DisplayAddedOrRemoved.Text += $"\"{inputRemoveNickName.Text}\" doesn't exist \n";
            }
        }

        
    }
}
