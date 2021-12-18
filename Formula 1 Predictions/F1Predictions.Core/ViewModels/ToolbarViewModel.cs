using System.Diagnostics.CodeAnalysis;
using F1Predictions.Core.Models;
using Microsoft.Extensions.Logging;
using MvvmCross.ViewModels;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : MvxViewModel
{
    private readonly ILogger logger;
    
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public ToolbarViewModel(ILogger<ToolbarViewModel> logger)
    {
        this.logger = logger;
    }

    private Toolbar toolbar;
    public Toolbar Toolbar
    {
        get => toolbar;
        set => SetProperty(ref toolbar, value);
    }


    public override async Task Initialize()
    {
        await base.Initialize();
        
        // TODO: Read properties from app settings
    }
}