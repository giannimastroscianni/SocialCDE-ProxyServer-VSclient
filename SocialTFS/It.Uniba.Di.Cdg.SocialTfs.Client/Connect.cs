using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using System.Reflection;
using System.Windows.Forms.Integration;
using Microsoft.VisualStudio.CommandBars;

namespace It.Uniba.Di.Cdg.SocialTfs.Client
{
	/// <summary>
    /// The object for implementing an Add-in.
    /// </summary>
	/// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        #region Attributes

        /// <summary>
        /// Reference to the application object.
        /// </summary>
        private DTE2 _applicationObject;

        /// <summary>
        /// Reference to an add-in instance.
        /// </summary>
        private AddIn _addInInstance;

        #endregion

        #region Methods

        /// <summary>
        /// Implements the constructor for the Add-in object.
        /// Place your initialization code within this method.
        /// </summary>
		public Connect()
		{            
		}

		/// <summary>
        /// Implements the OnConnection method of the IDTExtensibility2 interface.
        /// Receives notification that the Add-in is being loaded.
        /// </summary>
		/// <param term='application'>Root object of the host application</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded</param>
		/// <param term='addInInst'>Object representing this Add-in</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;

            if(connectMode == ext_ConnectMode.ext_cm_UISetup)
                AddButtonInMenu();

            UIController.Initialize(_applicationObject, _addInInstance);
		}

        private void AddButtonInMenu()
        {
            Commands2 commands = (Commands2)_applicationObject.Commands;

            //Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
            CommandBar menuBarCommandBar = ((CommandBars)_applicationObject.CommandBars)["MenuBar"];

            //Find the Tools command bar on the MenuBar command bar:
            CommandBarControl toolsControl = menuBarCommandBar.Controls["View"];
            CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

            try
            {
                object[] contextGUIDS = new object[] { };

                //Add a command to the Commands collection:
                Command command = commands.AddNamedCommand2(_addInInstance, "SocialTFS", "SocialTFS", "Open SocialTFS", false, 1, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                //Add a control for the command to the tools menu:
                if ((command != null) && (toolsPopup != null))
                {
                    command.AddControl(toolsPopup.CommandBar, 1);
                }
            }
            catch (System.ArgumentException)
            {
                //  safely ignore the exception.
            }

        }

		/// <summary>
        /// Implements the OnDisconnection method of the IDTExtensibility2 interface.
        /// Receives notification that the Add-in is being unloaded.
        /// </summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded</param>
		/// <param term='custom'>Array of parameters that are host application specific</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>
        /// Implements the OnAddInsUpdate method of the IDTExtensibility2 interface.
        /// Receives notification when the collection of Add-ins has changed.
        /// </summary>
		/// <param term='custom'>Array of parameters that are host application specific</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>
        /// Implements the OnStartupComplete method of the IDTExtensibility2 interface.
        /// Receives notification that the host application has completed loading.
        /// </summary>
		/// <param term='custom'>Array of parameters that are host application specific</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
            UIController.ShowToolWindow();
		}

		/// <summary>
        /// Implements the OnBeginShutdown method of the IDTExtensibility2 interface.
        /// Receives notification that the host application is being unloaded.
        /// </summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
        }

        public void Exec(string CmdName, vsCommandExecOption ExecuteOption, ref object VariantIn, ref object VariantOut, ref bool Handled)
        {
            UIController.ShowToolWindow();
            Handled = true;
        }

        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object CommandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "It.Uniba.Di.Cdg.SocialTfs.Client.Connect.SocialTFS")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
            }
        }

        #endregion
    }
}