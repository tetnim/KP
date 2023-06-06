using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks; // Added for async Task

namespace MauiApp.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private string _currentDateTime;

		public string CurrentDateTime
		{
			get => _currentDateTime;
			set
			{
				_currentDateTime = value;
				OnPropertyChanged();
			}
		}

		// Added the DeviceInfo property
		public string DeviceInfo
		{
			get
			{
				var deviceInfo = new StringBuilder()
				  .AppendLine($"Model: {DeviceInfo.Model}")
				  .AppendLine($"Manufacturer: {DeviceInfo.Manufacturer}")
				  .AppendLine($"Platform: {DeviceInfo.Platform}")
				  .AppendLine($"OS Version: {DeviceInfo.VersionString}");

				return deviceInfo.ToString();
			}
		}

		public ICommand UpdateTimeCommand { get; }

		public MainViewModel()
		{
			UpdateTimeCommand = new Command(UpdateTime);
			CurrentDateTime = DateTime.Now.ToString("F");
		}

		private void UpdateTime()
		{
			CurrentDateTime = DateTime.Now.ToString("F");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Added FetchDataFromApiAsync method
		private async Task FetchDataFromApiAsync()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var todoItem = JsonSerializer.Deserialize<TodoItem>(json);
				// Process the todoItem as needed
			}
		}
	}
}