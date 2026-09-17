/* IMPORT LIBRARIES */
using System;
// Libraries for Revit
using Autodesk.Revit.UI;



namespace ViewFiltersFactory
{
    public class RibbonTabPanelFactory
    {
        /* ATTRIBUTES */
        // Private Static Instance - SINGLETON PATTERN
        private static RibbonTabPanelFactory instance;

        /* CONSTRUCTORS */
        // Default Private - SINGLETON PATTERN
        public RibbonTabPanelFactory() { }

        /* METHODS */

        // Public Static .getInstance() Method - SINGLETON PATTERN
        public static RibbonTabPanelFactory getInstance()
        {
            // Create the instance the first time it's requested
            if (instance == null) { instance = new RibbonTabPanelFactory(); }
            // Return the existing (or just-created) instance
            return instance;                                                  
        }

        // Public .create Method - FACTORY PATTERN
        public RibbonPanel create(UIControlledApplication application, String tabName, String panelName)
        {
            // Create Ribbon Panel via call of corresponding Revit API method
            return application.CreateRibbonPanel(tabName, panelName);
        }

        // Public .getOrCreate Method - returns the existing panel, creating the tab/panel only if missing
        public RibbonPanel getOrCreate(UIControlledApplication application, String tabName, String panelName)
        {
            // CreateRibbonTab throws a Revit exception if the tab already exists (e.g. created by another add-in)
            // Attempt to create the tab
            try { application.CreateRibbonTab(tabName); }
            // Ignore the error if the tab is already there
            catch (Autodesk.Revit.Exceptions.ArgumentException) { }

            // Look for a panel with matching name among the ones already on the tab
            RibbonPanel ribbonPanel = application.GetRibbonPanels(tabName).Find(rbPanel => rbPanel.Name == panelName);
            // If cannot find the ribbon panel, just create it
            return ribbonPanel ?? create(application, tabName, panelName);
        }
    }


}
