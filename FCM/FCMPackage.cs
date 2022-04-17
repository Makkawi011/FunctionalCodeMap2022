global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using FCM.Generators;

namespace FCM
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.FCMString)]
    [ProvideCodeGenerator(typeof(DGMLFileGenerator), DGMLFileGenerator.Name, DGMLFileGenerator.Description, true, RegisterCodeBase = true)]

    [ProvideCodeGeneratorExtension(DGMLFileGenerator.Name, ".cs")]
    [ProvideUIContextRule(PackageGuids.CommandVisisiblityString,
        name: "Dgml files",
        expression: "cs",
        termNames: new[] { "cs" },
        termValues: new[] { "HierSingleSelectionName:.cs" })]
    public sealed class FCMPackage : ToolkitPackage
    {
        public static AsyncPackage AsyncPackage;
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            AsyncPackage = this;

            await this.RegisterCommandsAsync();
        }
    }
}