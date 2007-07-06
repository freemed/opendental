using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.CustomControls
{
    /// <summary>
    /// A panel that retains its scroll position(s) across form submissions
    /// </summary>
    /// <author><a href="mailto:ikono@neurosoft.gr">Oikonomopoulos Spyros</a></author>
    [ToolboxData("<{0}:StatefullScrollPanel runat=server></{0}:StatefullScrollPanel>")]
    public class StatefullScrollPanel : System.Web.UI.WebControls.Panel
    {
        protected override void Render(HtmlTextWriter writer)
        {                        
            if (DesignMode)
            {
                base.Render(writer);
                return;
            }

            /*
             * Hidden inputs to persist scroll position(s)
             */
            writer.Write("<input type='hidden' name='" + ScrollXInputId + "' id='" +
                ScrollXInputId + "' value='" + ScrollXInputValue + "'/>");
            
            writer.Write("<input type='hidden' name='" + ScrollYInputId + "' id='" + 
                ScrollYInputId + "' value='" + ScrollYInputValue + "'/>");
            
            //Trace method that populates the hidden inputs
            writer.Write("<script language='Javascript'>function " + TraceMethod + 
                "{" + GetElementAccessor(ScrollXInputId) + ".value = " + 
                GetElementAccessor(ClientID) +  ".scrollLeft;" + 
                GetElementAccessor(ScrollYInputId) +  ".value = " + 
                GetElementAccessor(ClientID) + ".scrollTop;}" +
                "</script>");

            //Delegate core rendering to momma
            base.Render(writer);                      
        }
        
        protected override void OnLoad(EventArgs e)
        {
            //To enable scrolling in the first place
            base.Style.Add(HtmlTextWriterStyle.Overflow, "scroll");
            
            //Register our trace method
            base.Attributes.Add("onScroll", TraceMethod);            

            //On start up set scroll position(s) to the existing one
            Page.ClientScript.RegisterStartupScript(GetType(), AdjustScriptKey,                
                GetElementAccessor (ClientID) + ".scrollLeft = " + 
                        ScrollXInputValue + ";" + 
                    GetElementAccessor(ClientID) + ".scrollTop = " + 
                        ScrollYInputValue + ";", 
                true);
            
            base.OnLoad(e);
        }
        
        
        //X scroll position input control id
        private String ScrollXInputId {
            get { return base.ClientID + "ScrollX"; }
        }

        //Y scroll position input control id
        private String ScrollYInputId {
            get { return base.ClientID + "ScrollY"; }
        }
        
        //X scroll position input control value
        private String ScrollXInputValue
        {
            get { return (Page.Request.Params[ScrollXInputId] == null ? "0" 
                : Page.Request.Params[ScrollXInputId]); }
        }
        
        //Y scroll position input control value
        private String ScrollYInputValue
        {
            get { return (Page.Request.Params[ScrollYInputId] == null ? "0" 
                : Page.Request.Params[ScrollYInputId]);  }
        }

        //Scroll position adjustment startup script key
        private String AdjustScriptKey
        {
            get { return "Adjust" + ClientID + "ScrollPos"; }
        }

        /*
         * DRY helpers
         */
        private String GetElementAccessor(String id)
        {
            return "document.getElementById('" + id + "')";
        }
        
        private String TraceMethod
        {
            get { return "trace" + ClientID + "ScrollPosition()"; }
        }        
    }
}
