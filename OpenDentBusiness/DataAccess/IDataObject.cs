using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenDental.DataAccess {
	public interface IDataObject : IXmlSerializable {
		bool IsDirty { get; }
		bool IsNew { get; set; }
		bool IsDeleted { get; }

		void OnSaved(EventArgs e);
		void OnModified(EventArgs e);
		void OnDeleted(EventArgs e);

		event EventHandler Saved;
		event EventHandler Modified;
		event EventHandler Deleted;
	}
}
