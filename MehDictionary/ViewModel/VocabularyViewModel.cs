﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using MehDictionary.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;

namespace MehDictionary.ViewModel
{
    class VocabularyViewModel : DependencyObject
    {
        static FileInfo path = new FileInfo("Data\\Vocabulary.json");
        Vocabulary data;

        public VocabularyViewModel()
        {
            data = new Vocabulary(path.ToString());
            Items = CollectionViewSource.GetDefaultView(data.Translations);
        }

        #region DependecyProperty
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(VocabularyViewModel), new PropertyMetadata(null));

        public string NewWord
        {
            get { return (string)GetValue(NewWordProperty); }
            set { SetValue(NewWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewWord.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewWordProperty =
            DependencyProperty.Register("NewWord", typeof(string), typeof(VocabularyViewModel), new PropertyMetadata(null));
        #endregion

        #region AddCommand
        private ICommand addClick;

        public ICommand AddClick
        {
            get { return addClick ?? (addClick = new RelayCommand(AddItem)); }
        }

        private void AddItem()
        {
            if (NewWord != null && NewWord.Length != 0 )
            {
                data.Add(NewWord.ToLower());
                Items.Refresh();
            }
        }
        #endregion

        #region RemoveCommand
        private ICommand removeItem;

        public ICommand RemoveItem
        {
            get { return removeItem ?? (removeItem = new RelayCommand<string>(p => RemoveItemCommand(p) )); }
        }

        private void RemoveItemCommand(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                data.Remove(item);
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
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");

            Serialization.WriteTranslaionsToFile(data.Translations, path.ToString());
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
            PDFCreator.WritePDF(data.Translations, "Тысячи.pdf");
        }

        #endregion
    }
}
