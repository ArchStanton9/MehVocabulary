using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehDictionary.ViewModel
{
    class EditViewModel : ViewModelBase
    {
        private string word;

        public string Word
        {
            get { return word; }
            set
            {
                word = value;
                RaisePropertyChanged("Items");
            }
        }

        public EditViewModel(Translator.Note note)
        {
            Word = note.Word;
        }

    }
}
