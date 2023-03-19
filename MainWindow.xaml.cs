using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milestone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MovieList> movieList = new List<MovieList>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All Files (*.*)|*.*| CSV File (*.csv)|*.csv";

            string[] fileArray = new string[0];
            if(openFile.ShowDialog() == true ) 
            { 
                fileArray= File.ReadAllLines(openFile.FileName);
            }


            foreach (var item in fileArray.Skip(1))
            {
                MovieList m = new MovieList();
                string[] splitline = item.Split(',');
                m.Title = splitline[0];


                foreach (var genre in splitline[1].Split('/')) 
                {
                    string s = genre.Replace("Comedi", "Comedy");
                    s = s.Replace("Drame", "Drama");
                    s = s.Replace("Sci-Fo", "Sci-Fi");
                    s = s.Replace("Advinture", "Adventure");

                    m.Genre.Add(s); 


                    if( lstboxGenre.Items.Contains(s) == false ) 
                    { 
                        lstboxGenre.Items.Add(s);
                    }

                    List<string> genreList = new List<string>();

                    foreach (var items in lstboxGenre.Items)
                    {
                        genreList.Add(items.ToString());
                    }

                    var organizedGenre = genreList.OrderBy(t => t);
                    List<string> distinctGenre = organizedGenre.Distinct().ToList();    
                    lstboxGenre.Items.Clear();

                    foreach(var ite in distinctGenre)
                    {
                        lstboxGenre.Items.Add(ite);
                    }
                }

                foreach (var Director in splitline[2].Split(';'))
                {
                    Director d = new Director();
                    var segments = Director.Split("|");

                    d.FirstName= segments[0];
                    d.LastName= segments[1];
                    d.DateOfBirth = segments[2];
                    d.URl = segments[3];

                    m.Director.Add(d);

                    if(lstboxDirector.Items.Contains(d.FirstName) == false )
                    {
                        lstboxDirector.Items.Add(d);
                    }
                    List<string> directorList = new List<string>();

                    foreach (var items in lstboxDirector.Items)
                    {
                        directorList.Add(items.ToString());
                    }

                    var organizedDirector = directorList.OrderBy(t => t);
                    List<string> distinctDirectors = organizedDirector.Distinct().ToList();
                    lstboxDirector.Items.Clear();

                    foreach(var ite in distinctDirectors)
                    {
                        lstboxDirector.Items.Add(ite);
                    }
                }

                foreach( var StarActors in splitline[3].Split(';'))
                {
                    m.StarActors.Add(StarActors);
                }

                m.IMBD = splitline[4];
                movieList.Add(m);

                foreach(var movie in movieList)
                {
                    if(!lstboxMovie.Items.Contains(movie))
                    { 
                        lstboxMovie.Items.Add(movie);  
                    }
                }


            }
        }

        private void JsonBtn_Click(object sender, RoutedEventArgs e)
        {

            string JsonData = JsonConvert.SerializeObject(lstboxMovie.Items);
            File.WriteAllText("ALLData.json", JsonData);

            string JsonGenres = JsonConvert.SerializeObject(lstboxGenre.Items);
            File.WriteAllText("Genre.json", JsonGenres);

            string JsonDirectors = JsonConvert.SerializeObject(lstboxDirector.Items);
            File.WriteAllText("Director.json", JsonDirectors);

            MessageBox.Show("JSON File Saved");
        }

        private void CSV_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter csvOutput = new StreamWriter($"AllData.csv");
            csvOutput.WriteLine("Title, Genre, Director, Stars Actors, IMBD");

            foreach(var Item in lstboxMovie.Items)
            {
                csvOutput.Write(Item + " ");
                csvOutput.WriteLine();
            }

            csvOutput.Close();

            StreamWriter csvOutput2 = new StreamWriter($"Genres.csv");
            csvOutput.WriteLine("Genres");

            foreach (var genre in lstboxGenre.Items)
            {
                csvOutput.Write(genre + " ");
                csvOutput.WriteLine();
            }

            csvOutput.Close();


            StreamWriter csvOutput3 = new StreamWriter($"Directors.csv");
            csvOutput.WriteLine("Firstname, LastName, DateofB, URl");

            foreach (var director in lstboxDirector.Items)
            {
                string s = director.ToString();
                s = s.Replace("|", ",");

                csvOutput.Write(s + " ");
                csvOutput.WriteLine();
            }

            csvOutput.Close();
            MessageBox.Show("CSV File Saved");
        }
    }
}
