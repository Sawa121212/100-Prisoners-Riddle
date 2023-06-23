using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Common.Core;

namespace PrisonersRiddle{

/// <summary>
/// Локатор View по ViewModel
/// </summary>
public class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        var viewModelType = data.GetType();

        var viewName = viewModelType.FullName?.Replace("ViewModel", "View");
        var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;

        if (viewName is null)
        {
            return new TextBlock { Text = "Invalid Data Type" };
        }

        var type = Type.GetType($"{viewName}, {viewModelAssemblyName}");
        if (type is { })
        {
            var instance = Activator.CreateInstance(type);
            if (instance is { })
            {
                return (Control)instance;
            }
            else
            {
                return new TextBlock { Text = "Create Instance Failed: " + type.FullName };
            }
        }
        else
        {
            return new TextBlock { Text = "Not Found: " + viewName };
        }
    }

    public bool Match(object data)
    {
        return data is ViewModelBase;
    }
} }