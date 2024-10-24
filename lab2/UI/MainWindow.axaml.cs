using Avalonia.Controls;
using Avalonia.Interactivity;
using lab2.Controller;
using lab2.Entity;
using lab2.Exception;

namespace lab2.UI
{
    public partial class MainWindow : Window
    {
        private readonly IMusicCatalogController _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MusicCatalogController();
            DataContext = _viewModel;
        }

        private async void OnSearchClick(object sender, RoutedEventArgs e)
        {
            await ExecuteSearchAsync();
        }

        private async void OnShowAllClick(object sender, RoutedEventArgs e)
        {
            await ShowAllAsync();
        }

        private void OnShowCreateArtistClick(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            addArtistPanel.IsVisible = true;
        }

        private void OnShowCreateAlbumClick(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            addAlbumPanel.IsVisible = true;
        }

        private void OnShowCreateTrackToPlaylistClick(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            addTrackToPlaylistPanel.IsVisible = true;
        }
        
        private void OnShowCreateTrackClick(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            addTrackPanel.IsVisible = true;
        }

        private void OnCreateArtistClick(object sender, RoutedEventArgs e)
        {
            var artistName = artistNameBox.Text;
            if (string.IsNullOrWhiteSpace(artistName))
            {
                ResultsTextBlock.Text = "Enter the artist's name.";
                return;
            }
            
            ResultsTextBlock.Text = _viewModel.CreateArtist(artistName);
            artistNameBox.Clear();
        }

        private void OnCreateAlbumClick(object sender, RoutedEventArgs e)
        {
            var artistName = albumArtistNameBox.Text;
            var albumName = albumNameBox.Text;
            var releaseDate = albumReleaseDateCalendar.SelectedDate ?? DateTime.Now;

            if (string.IsNullOrWhiteSpace(artistName) || string.IsNullOrWhiteSpace(albumName))
            {
                ResultsTextBlock.Text = "Fill in all fields.";
                return;
            }

            ResultsTextBlock.Text = _viewModel.CreateAlbumForArtist(artistName, albumName, releaseDate);
            albumArtistNameBox.Clear();
            albumNameBox.Clear();
            albumReleaseDateCalendar.SelectedDate = null;
        }

        private void OnCreateTrackClick(object sender, RoutedEventArgs e)
        {
            var artistName = trackArtistNameBox.Text;
            var albumName = trackAlbumNameBox.Text;
            var trackName = trackNameBox.Text;
            var durationText = trackDurationBox.Text;
            var genreText = ((ComboBoxItem)trackGenreBox.SelectedItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(artistName) || string.IsNullOrWhiteSpace(albumName) ||
                string.IsNullOrWhiteSpace(trackName) || string.IsNullOrWhiteSpace(durationText) || genreText == null)
            {
                ResultsTextBlock.Text = "Fill in all fields.";
                return;
            }

            if (!TimeSpan.TryParseExact(durationText, "m\\:ss", null, out TimeSpan duration))
            {
                ResultsTextBlock.Text = "Invalid duration format. Use the format (mm:ss).";
                return;
            }

            Genre genre;
            try
            {
                genre = (Genre)Enum.Parse(typeof(Genre), genreText);
            }
            catch (System.Exception)
            {
                ResultsTextBlock.Text = "Select a valid genre.";
                return;
            }

            try
            {
                ResultsTextBlock.Text = _viewModel.CreateTrackToAlbum(artistName, albumName, trackName, duration, genre);
            }
            catch (TrackNotFoundException ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            finally
            {
                trackArtistNameBox.Clear();
                trackAlbumNameBox.Clear();
                trackNameBox.Clear();
                trackDurationBox.Clear();
                trackGenreBox.SelectedIndex = -1;
            }
        }
        
        private async Task ExecuteSearchAsync()
        {
            SetLoadingState(true);
            string searchQuery = searchBox.Text;
            string searchType = ((ComboBoxItem)dropdownList.SelectedItem)?.Content.ToString();

            await Task.Delay(1000);
            
            ResultsTextBlock.Text = _viewModel.Search(searchType, searchQuery);
            SetLoadingState(false);
        }

        private async Task ShowAllAsync()
        {
            SetLoadingState(true);
            await Task.Delay(2000);
            
            ResultsTextBlock.Text = _viewModel.ShowAll();
            SetLoadingState(false);
        }

        private void SetLoadingState(bool isLoading)
        {
            LoadingIndicator.IsVisible = isLoading;
            ResultsTextBlock.IsVisible = !isLoading;
        }

        private void OnShowCreatePlaylistClick(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            addPlaylistPanel.IsVisible = true;
        }

        private void OnCreatePlaylistClick(object sender, RoutedEventArgs e)
        {
            var playlistName = playlistNameBox.Text;
            if (string.IsNullOrWhiteSpace(playlistName))
            {
                ResultsTextBlock.Text = "Enter the playlist name.";
                return;
            }

            try
            {
                ResultsTextBlock.Text = _viewModel.CreatePlaylist(playlistName);
            }
            catch (PlaylistNotFoundException ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            finally
            {
                playlistNameBox.Clear();
            }
        }

        private void OnCreateTrackToPlaylistClick(object sender, RoutedEventArgs e)
        {
            var artistName = trackArtistNameForPlaylistBox.Text;
            var trackName = trackNameForPlaylistBox.Text;
            var playlistName = playlistNameForTrackBox.Text;

            if (string.IsNullOrWhiteSpace(artistName) || string.IsNullOrWhiteSpace(trackName) || string.IsNullOrWhiteSpace(playlistName))
            {
                ResultsTextBlock.Text = "Fill in all fields.";
                return;
            }

            try
            {
                ResultsTextBlock.Text = _viewModel.CreateTrackToPlaylist(trackName, artistName, playlistName);
            }
            catch (TrackNotFoundException ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            
            catch (PlaylistNotFoundException ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                ResultsTextBlock.Text = ex.Message;
            }
            finally
            {
                trackArtistNameForPlaylistBox.Clear();
                trackNameForPlaylistBox.Clear();
                playlistNameForTrackBox.Clear();
            }
        }

        private void HideAllPanels()
        {
            addArtistPanel.IsVisible = false;
            addAlbumPanel.IsVisible = false;
            addTrackPanel.IsVisible = false;
            addPlaylistPanel.IsVisible = false;
            addTrackToPlaylistPanel.IsVisible = false;
        }
    }
}
