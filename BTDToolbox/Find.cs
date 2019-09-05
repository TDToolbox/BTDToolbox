using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    class Find
    {
        /*
        struct FindTextVariables
        {
            public int numPhraseFound;
            public int startPosition;
            public int endPosition;
            public int endEditor;
        }
        
        public bool isCurrentlySearching;
        public string previousSearchPhrase;
        public bool isReplacing;
        public bool isFinding;
        public bool findNextPhrase;
        
        
        public FindTextVariables FindText(string InputString, string FindString, int StartPosition, int IndexFirstFoundString)
        {
            var findTextVariables = new FindTextVariables();

            findTextVariables.numPhraseFound = 0;
            findTextVariables.startPosition = StartPosition + 1;
            findTextVariables.endEditor = InputString.Length;
            findTextVariables.endPosition = 0;

            isReplacing = true;

            if (previousSearchPhrase != FindString)
            {
                findTextVariables.numPhraseFound = 0;
                findTextVariables.endPosition = 0;
            }
            for (int i = 0; i < findTextVariables.endEditor; i = findTextVariables.startPosition)
            {
                previousSearchPhrase = FindString;
                isCurrentlySearching = true;
                if (i == -1)
                {
                    isCurrentlySearching = false;
                    return findTextVariables;
                }
                findTextVariables.startPosition = IndexFirstFoundString;  //Editor_TextBox.Find(searchPhrase, startPosition, endEditor, RichTextBoxFinds.None);
                if (findTextVariables.startPosition >= 0)
                {
                    findNextPhrase = false;
                    findTextVariables.numPhraseFound++;
                    //Editor_TextBox.SelectionColor = Color.Blue;       //saving this value for later use
                    findTextVariables.endPosition = FindString.Length;
                    findTextVariables.startPosition = findTextVariables.startPosition + findTextVariables.endPosition;
                    return findTextVariables;
                }

                if (findTextVariables.numPhraseFound == 0)
                {
                    MessageBox.Show("No Match Found!!!");
                }
            }
        }*/
    }
}
