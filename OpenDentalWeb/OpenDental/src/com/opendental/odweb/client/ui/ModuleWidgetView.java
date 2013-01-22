package com.opendental.odweb.client.ui;

import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Element;
import com.google.gwt.dom.client.Style.Unit;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.ResizeComposite;
import com.google.gwt.user.client.ui.ScrollPanel;
import com.google.gwt.user.client.ui.SimpleLayoutPanel;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;

/** A view of a {@link ModuleWidget} */
public class ModuleWidgetView extends ResizeComposite {
	interface ModuleWidgetViewUiBinder extends UiBinder<Widget,ModuleWidgetView> {
	}
	private static ModuleWidgetViewUiBinder uiBinder=GWT.create(ModuleWidgetViewUiBinder.class);

  private final boolean hasMargins;
  @UiField(provided = true) SimplePanel modulePanel;
  @UiField Element nameElem;

  public ModuleWidgetView(boolean hasMargins,boolean scrollable) {
    this.hasMargins=hasMargins;
    modulePanel=scrollable ? new ScrollPanel() : new SimpleLayoutPanel();
    modulePanel.setSize("100%","100%");
    initWidget(uiBinder.createAndBindUi(this));
  }

  public void setModule(Widget widgetModule) {
    modulePanel.setWidget(widgetModule);
    if(hasMargins) {
      widgetModule.getElement().getStyle().setMarginLeft(10.0,Unit.PX);
      widgetModule.getElement().getStyle().setMarginRight(10.0,Unit.PX);
    }
  }

  public void setName(String text) {
    nameElem.setInnerText(text);
  }
}
