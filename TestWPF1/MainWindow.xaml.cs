using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Configuration;
using Phonix;
using System;
using System.Data;
using Microsoft.Win32;

namespace DuplicateFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region variable

        Dictionary<int, Person> people;
        Dictionary<string, List<Person>> duplicates;
        Dictionary<int, Person> nonDuplicates;
        readonly DoubleMetaphone _generator = new DoubleMetaphone();

        #endregion 

        #region events

        private void Import_File_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    DefaultExt = ".png",
                    Filter = "CSV Files (*.csv)|*.csv"
                };
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    Init();
                    using (StreamReader sr = new StreamReader(dlg.FileName))
                    {
                        string headerline = sr.ReadLine();
                        int count = 1;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] data = line.Split(',');
                            people.Add(count, new Person(count, data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[3], data[3], data[3], data[3]));
                            count += 1;
                        }
                    }
                    MessageBox.Show("File imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FindDuplicates();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert" , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindDuplicates()
        {
            try
            {
                Dictionary<int, string> names = new Dictionary<int, string>();
                foreach (Person person in people.Values)
                {
                    names.Add(person.ID, person.FirstName + " " + person.LastName);
                }
                Dictionary<int, string> keys = KeyGenerator(names);

                foreach (string key in keys.Values)
                {
                    var matchingKeys = keys.Where(k => k.Value == key).Select(k => k.Key);
                    if (matchingKeys.ToList().Count > 1)
                    {
                        List<Person> matchingPeople = new List<Person>();
                        foreach (int id in matchingKeys)
                        {
                            matchingPeople.Add(people[id]);
                        }
                        if (!duplicates.ContainsKey(key))
                        {
                            duplicates.Add(key, matchingPeople);
                        }
                    }
                    else
                    {
                        nonDuplicates.Add(matchingKeys.First(), people[matchingKeys.FirstOrDefault()]);
                    }
                }

                ShowDuplicates();
                ShowNonDuplicates();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No file present for evaluation. Please import a CSV file.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion 

        #region methods

        private void Init()
        {
            try
            {
                people = new Dictionary<int, Person>();
                duplicates = new Dictionary<string, List<Person>>();
                nonDuplicates = new Dictionary<int, Person>();
                DuplicatesDataGrid.ItemsSource = null;
                PeopleDataGrid.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowDuplicates()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Count");
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Company");
                dt.Columns.Add("Email");
                dt.Columns.Add("Address1");
                dt.Columns.Add("Address2");
                dt.Columns.Add("Zip");
                dt.Columns.Add("City");
                dt.Columns.Add("StateLong");
                dt.Columns.Add("State");
                dt.Columns.Add("Phone");

                int count = 1;
                foreach (List<Person> list in duplicates.Values)
                {
                    foreach (Person p in list)
                    {
                        dt.Rows.Add(count, p.FirstName, p.LastName, p.Company, p.Email, p.Address1, p.Address2, p.Zip, p.City, p.StateLong, p.State, p.Phone);
                    }
                    count++;
                }
                DuplicatesDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowNonDuplicates()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Company");
                dt.Columns.Add("Email");
                dt.Columns.Add("Address1");
                dt.Columns.Add("Address2");
                dt.Columns.Add("Zip");
                dt.Columns.Add("City");
                dt.Columns.Add("StateLong");
                dt.Columns.Add("State");
                dt.Columns.Add("Phone");

                foreach (Person p in nonDuplicates.Values)
                {
                    dt.Rows.Add(p.ID, p.FirstName, p.LastName, p.Company, p.Email, p.Address1, p.Address2, p.Zip, p.City, p.StateLong, p.State, p.Phone);
                }
                PeopleDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public Dictionary<int, string> KeyGenerator(Dictionary<int, string> words)
        {
            try
            {
                Dictionary<int, string> keys = new Dictionary<int, string>();
                foreach (int item in words.Keys)
                {
                    keys[item] = _generator.BuildKey(words[item]);
                }
                return keys;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        #endregion
        
    }
}

