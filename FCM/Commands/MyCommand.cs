using EnvDTE; // DTE = Development Tools Environoment
using Microsoft;
using FCM.Generators;

namespace FCM
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override Task InitializeCompletedAsync()
        {
            Command.Supported = false;
            return base.InitializeCompletedAsync();
        }
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            //we need to use GetServiceAsync function to make DTE object
            //this function (GetServiceAsync) need this line before we use it 
            // becouse Accessing "DTE" should only be done on the main thread.

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dte = await FCMPackage.AsyncPackage
                .GetServiceAsync(typeof(DTE))
                as DTE;

            //check whether the result of GetService call is null
            Assumes.Present(dte);

            // when the user presses the Show on Functional Code Map button for CS file
            // here we put in the properties of this file
            // in the field of CustomTool = "DGMLFileGenerator"
            // this prosses for call our single file generator : DGMLFileGenerator 
            dte
            .SelectedItems
                .Item(1)
                .ProjectItem
                .Properties
                .Item("CustomTool")
                .Value = DGMLFileGenerator.Name;


            //this 2 lines of code those below ,
            //were suggested in the video : https://www.youtube.com/watch?v=w_ntVZb_26M&t=1s
            //but it dosen't work here and we dont't need thim 
            //becouse of what we wrote above

            //var file = await VS.Solutions.GetActiveItemAsync() as PhysicalFile;
            //await file.TrySetAttributeAsync("custom tool", SassToCss.Name);
        }
    }
}
