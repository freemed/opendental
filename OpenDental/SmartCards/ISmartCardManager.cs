using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ConsoleApplication1 {
	public interface ISmartCardManager : IDisposable {
		ReadOnlyCollection<string> Readers { get; }
		event SmartCardStateChangedEventHandler SmartCardChanged;
	}
}
