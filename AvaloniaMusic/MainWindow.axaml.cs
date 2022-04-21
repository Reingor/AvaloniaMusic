using Avalonia.Controls;
using System.Windows;
using System.Collections.Generic;
using System;
using Avalonia.Media.Imaging;

//using System.Windows.Media.Imaging;

namespace AvaloniaMusic
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load load = new Load();
            load.LoadHtml();
			List<Music> items = new List<Music>();



			List<Playlist> playlist = new List<Playlist>()
			{ new Playlist() { AvatarImage=load!.imageDraw, PlaylistName =load!.Data["albumName"][0], Genre=load!.Genre, RecordLabel = load!.RecordLabel, ReleaseDate=load!.ReleaseDate, } };


			
			for (int i = 0; i < load.Data["title"].Count; i++)
            {
				Music td = new Music();
				td.Title = load.Data["title"][i];
                td.Duration = load.Data["duration"][i];
				td.ArtistName = load.Data["artistName"][i];
				td.AlbumName = load.Data["albumName"][i];
				if (load.Data["genre"] != null)
                {
					td.Genre = load.Genre;
				}
				
                items.Add(td);
			}
			

			phonesList.Items = items;
			playlistList.Items = playlist;
		}
		




	}

	public class Music
	{
		public string Title { get; set; }
		public string Duration { get; set; }
		public string ArtistName { get; set; }
		public string AlbumName { get; set; }
		public string Genre { get; set; }
	}

	public class Playlist
	{
		public Bitmap AvatarImage { get; set; }
		public string PlaylistName { get; set; }
		public string ReleaseDate { get; set; }
		public string RecordLabel { get; set; }
		public string Genre { get; set; }
	}


}
