using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenDental.DataAccess {
	/// <summary>
	/// The common interface for all objects stored in the database.
	/// </summary>
	public interface IDataObject : IXmlSerializable {
		/// <summary>
		/// Gets a value indicating if the object has been modified since it was last saved to the database.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if the object has been modified since it was last saved to the database, <see langword="false"/>
		/// otherwise.
		/// </value>
		/// <remarks>
		/// <see cref="IsDirty"/> is set to <see langword="true"/> if the object is a newly created object that hasn't been
		/// stored in the database before.
		/// </remarks>
		bool IsDirty { get; }

		/// <summary>
		/// Gets or sets a value indicating if the object is a newly created object, that hasn't been saved to the database.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if the object is a newly created object, that hasn't been saved to the database, otherwise,
		/// <see langword="false"/>.
		/// </value>
		bool IsNew { get; set; }

		/// <summary>
		/// Gets or sets a value indicating if the object existed in the database previously, but has been deleted.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if the object existed in the database previously, but has been deleted. Otherwise,
		/// <see langword="false"/>
		/// </value>
		bool IsDeleted { get; }

		/// <summary>
		/// Raises the <see cref="Saved"/> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> that contains the event data. </param>
		void OnSaved(EventArgs e);

		/// <summary>
		/// Raises the <see cref="Modified"/> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> that contains the event data. </param>
		void OnModified(EventArgs e);

		/// <summary>
		/// Raises the <see cref="Deleted"/> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> that contains the event data. </param>
		void OnDeleted(EventArgs e);

		/// <summary>
		/// Occurs when the object is saved to the database.
		/// </summary>
		event EventHandler Saved;

		/// <summary>
		/// Occurs when the value of the object is modified.
		/// </summary>
		event EventHandler Modified;

		/// <summary>
		/// Occurs when the object is deleted from the database.
		/// </summary>
		event EventHandler Deleted;
	}
}
