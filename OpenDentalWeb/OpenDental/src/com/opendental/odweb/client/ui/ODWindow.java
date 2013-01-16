package com.opendental.odweb.client.ui;

import com.google.gwt.user.client.ui.DialogBox;

/** ODWindow simply extends DialogBox.  Every window built in the web version will extend this class so that we can make a global change to all windows from here.  
 *  Every window should call a constructor first thing so that those global settings apply.  Otherwise, the window can simply extend DialogBox instead.  */
public class ODWindow extends DialogBox {
	/** Every window will have a result callback so that we can mimic C#'s DialogResult. */
	public DialogResultCallbackOkCancel DialogResultCallback;
	
	/**  */
	public ODWindow() {
		this("");
	}
	
	/** This constructor sets the title of the window. */
	public ODWindow(String title) {
		this.setText(title);
		//If another constructor is added, move the global settings to that constructor and have this constructor call that one.
		//Global settings-------------------------------------------------------------------------------------------------------
		this.setAnimationEnabled(true);
		this.setGlassEnabled(true);
	}
	
}
