package com.opendental.odweb.client.ui;

import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.Widget;

/** A widget used to show the selected module in the ContentPanel. */
public abstract class ModuleWidget extends SimpleLayoutPanel {
	/** Whether the demo widget has been initialized. */
	private boolean moduleInitialized;
	/** Whether the module is (asynchronously) initializing. */
	private boolean moduleInitializing;
	/** The view that holds the name, description, and module. */
	private ModuleWidgetView moduleView;
  
	public ModuleWidget() {
		
	}
	
	/** When a module widget is first initialized, this method is called. If it returns a Widget, the widget will be added to the content panel. 
	 * Return null to disable it.  Example of null would be when the user logs off and we don't want info to display anymore.
	 * @return the widget to add to the content panel. */
	public abstract Widget onInitialize();
	
	/** Called when initialization has completed and the widget has been added to the page.  We will probably change the cursor back to normal here. */
	public void onInitializeComplete() {
		
	}
	
	protected void onLoad() {
		if(moduleView==null) {
			moduleView=new ModuleWidgetView(false, true);
			moduleView.setModule(getWidget());
			setWidget(moduleView);
		}
	    ensureModuleInitialized();
	    super.onLoad();
	}

	/** This is so that we only download the code for the module when the user wants it. */
	protected abstract void asyncOnInitialize(final AsyncCallback<Widget> callback);
  
	/** Ensure that the module widget has been initialized.  Note that initialization can fail if there is a network failure. */
	private void ensureModuleInitialized() {
		if(moduleInitializing || moduleInitialized) {
			return;
		}
		moduleInitializing=true;
		asyncOnInitialize(new AsyncCallback<Widget>() {
			public void onFailure(Throwable reason) {
				moduleInitializing=false;
				MsgBox.show("Failed to download code for this module ("+reason+")");
			}

			public void onSuccess(Widget result) {
				moduleInitializing=false;
				moduleInitialized=true;
				Widget widget=result;
				if(widget!=null) {
					moduleView.setModule(widget);
				}
				onInitializeComplete();
			}
		});
	}
	
}
