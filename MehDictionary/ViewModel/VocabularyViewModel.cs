﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using MehDictionary.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace MehDictionary.ViewModel
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
            set { newWord = value; }
        }


        #endregion

        #region AddCommand
        private ICommand addClick;

        public ICommand AddClick
        {
            get { return addClick ?? (addClick = new RelayCommand(AddItem)); }
        }

        private void AddItem()
        {
            if (NewWord != null && NewWord.Length != 0)
            {
                data.Add(NewWord.ToLower());
                Items.Refresh();
            }

            NewWord = null;
        }
        #endregion

        #region EditCommand

        private ICommand edit;
        public ICommand Edit
        {
            get { return edit ?? (edit = new RelayCommand<int>(i => ShowOptions(i))); }
        }

        void ShowOptions(int ID)
        {
            var element = data.Notes
                .Find(c => c.ID == ID);
            List<string> options = element.Defenitions[0].Translations
                .Select(s => s.Text)
                .ToList();
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
            if (itemID != null)
            {
                data.Remove(itemID.Value);
                Items.Refresh();
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
            Serialization.WriteTranslaionsToFile(data, path.ToString());
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
            PDFCreator.WritePDF(data.Notes, "Тысячи.pdf");
        }

        #endregion
    }
}
