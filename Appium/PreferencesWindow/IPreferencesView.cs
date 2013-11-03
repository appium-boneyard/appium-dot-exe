using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.PreferencesWindow
{
	public interface IPreferencesView
	{
		void BindPresentationModel(PreferencesPModel pModel);
		void Open();
	}
}
