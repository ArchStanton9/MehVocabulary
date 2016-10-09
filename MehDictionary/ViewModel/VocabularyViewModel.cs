using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using MehVocabulary.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using GalaSoft.MvvmLight;

namespace MehVocabulary.ViewModel
{
    class VocabularyViewModel : ViewModelBase
    {
        static FileInfo path = new FileInfo("Data\\Vocabulary.json");
        Notebook data;

        public VocabularyViewModel()
        {
            data = Serialization.LoadNotebook(path.ToString());

            Items = CollectionViewSource.GetDefaultView(data.Notes);
        }

        #region DependecyProperty
        private ICollectionView items;

        public ICollectionView Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }


        private string newWord;

        public string NewWord
        {
            get { return newWord; }
            set
            {
                newWord = value;
                RaisePropertyChanged("NewWord");
            }
        }

        #endregion

        #region AddCommand
        private ICommand addWordClick;

        public ICommand AddWordClick
        {
            get { return addWordClick ?? (addWordClick = new RelayCommand(AddItem)); }
        }

        private ICommand addWord;

        public ICommand AddWord
        {
            get { return addWord ?? (addWord = new RelayCommand<string>(s => AddItem(s))); }
        }

        private void AddItem()
        {
            AddItem(NewWord);
        }
        private void AddItem(string word)
        {
            try
            {
                word = word.ToLower();
                if (word != null && word.Length != 0)
                {
                    data.Add(word);
                    Items.Refresh();
                }

                MessageBox.Show(data.GetFullInfo(word));
                NewWord = null;
            }
            catch (Exception e)
            {
                SaveTranslations();
                MessageBox.Show(e.Message);
            }

        }
        #endregion

        #region InfoCommand

        private ICommand info;
        public ICommand Info
        {
            get { return info ?? (info = new RelayCommand<int>(c => NoteInfo(c))); }
        }

        void NoteInfo(int id)
        {
            try
            {
                MessageBox.Show(data.GetFullInfo(id));
            }
            catch (Exception e)
            {
                SaveTranslations();
                MessageBox.Show(e.Message);
            }
            
        }
        #endregion

        #region RemoveCommand
        private ICommand removeItem;

        public ICommand RemoveItem
        {
            get { return removeItem ?? (removeItem = new RelayCommand<int>(p => RemoveItemCommand(p) )); }
        }

        private void RemoveItemCommand(int? itemID)
        {
            try
            {
                if (itemID != null)
                {
                    data.Remove(itemID.Value);
                    Items.Refresh();
                }
            }
            catch (Exception e)
            {
                SaveTranslations();
                MessageBox.Show(e.Message);
            }
            
        }
        #endregion

        #region ClosingCommand
        private ICommand closingCommand;

        public ICommand ClosingComand
        {
            get { return closingCommand ?? (closingCommand = new RelayCommand(SaveTranslations)); }
        }

        private void SaveTranslations()
        {
            try
            {
                Serialization.WriteTranslaionsToFile(data, path.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        #endregion

        #region ExportPDF
        private ICommand export;

        public ICommand Export
        {
            get { return export ?? (export = new RelayCommand(SaveFile)); }
        }

        void SaveFile()
        {
            data.Sort();
            try
            {
                PDFCreator.WritePDF(data.Notes, "Тысячи.pdf");
                MessageBox.Show("Файл \"Тысячи.pdf\" сохранен на рабочем столе");
            }
            catch (IOException e)
            {
                SaveTranslations();
                MessageBox.Show(e.Message);
            }
        }

        #endregion
    }
}
