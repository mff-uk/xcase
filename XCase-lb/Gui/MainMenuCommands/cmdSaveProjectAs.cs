using System.Windows.Controls;
using Microsoft.Win32;
using XCase.Model.Serialization;

namespace XCase.Gui.MainMenuCommands
{
	/// <summary>
	/// Saves current project (with save as dialog)
	/// </summary>
	public class cmdSaveProjectAs : MainMenuCommandBase
	{
		public cmdSaveProjectAs(MainWindow mainWindow, Control control)
			: base(mainWindow, control)
		{

		}

		public override void Execute(object parameter)
		{
			SaveFileDialog dlg = new SaveFileDialog
			                     	{
                                     DefaultExt = ".XCase",
                                     Filter = "XCase files (*.XCase)|*.XCase|XML files (*.xml)|*.xml|All files (*.*)|*.*||"
			                     	};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				XmlSerializator serializator = CurrentProject.VersionManager != null ?
							new XmlSerializator(CurrentProject.VersionManager) :
							new XmlSerializator(CurrentProject);

				serializator.SerilizeTo(dlg.FileName);
				
                CurrentProject.FilePath = dlg.FileName;
				// HACK: should be somewhere else..
                MainWindow.Title = "XCase editor - " + CurrentProject.FilePath;
                MainWindow.HasUnsavedChanges = false;
			}
		}

		public override bool CanExecute(object parameter)
		{
			return true;
		}
	}
}